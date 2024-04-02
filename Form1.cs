using Microsoft.Win32;
using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Runtime.Remoting.Channels;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;


namespace VU1_Control
{
    public partial class Form1 : Form
    {
        // Lotsa globals cause that's how we roll.
        const int NR_SETUP = 3;

        Setup [] setup = new Setup[NR_SETUP];
        int CurrentSetupIndex;

        List<AudioDevice> AudioDeviceList = new List<AudioDevice>();

        bool running = false;
        SerialPort SP { get; set; }
        StreamWriter DebugStream;
        
        DateTime firstZeroTime, lastAutoSwitchTime;
        bool zeroFound = false;
        bool zeroTimeoutStarted = false;
        
        int LeftDialNr = 0;
        int RightDialNr = 1;
        int LeftCalibrateValue;
        int RightCalibrateValue;
        int AutoOffTimeout = 10;
        bool AutoSwitch = true;

        bool ResetDeviceNeeded = false;

        public Form1()
        {
            Debug("Program start");

            InitializeComponent();

            FillInputSelector();

            if (!FindVUMeters())
            {
                MessageBox.Show("No VU meters found.");
                Environment.Exit(0);
            }

            ReadFromRegistry();
            CurrentSetupIndex = 0;    // 1st setup takes preference on startup

            SetEasing(true, 100, 1);
            SetEasing(false, 30, 500);
            UpdateUI();
            SetVUMeterValues(0, 0);
            SetColor();

            InitAudioDevices();

            // Start when all is setup well
            if (setup[CurrentSetupIndex].SelectedDeviceIdx >= 0 && cbOutputs.Items.Count >= setup[CurrentSetupIndex].SelectedDeviceIdx)
            {
                ResetDeviceNeeded = true;
                btnStart.Text = "Stop";
                running = true;
            }

            Task.Run(UpdateVU);
        }


        private void Form1_Close(object sender, EventArgs e)
        {
            running = false;
            WriteToRegistry();
            SetColor(0, 0, 0);
            SetVUMeterValues(0, 0);
            Thread.Sleep(500);
        }

        private void UpdateUI()
        {
            sbSensitivity.Value = (int)(setup[CurrentSetupIndex].Sensitivity * 50);
            txtSensitiviy.Text = (sbSensitivity.Value - 50).ToString();

            sbSmoothness.Value = (int)(setup[CurrentSetupIndex].Smoothness * 100);
            txtSmoothness.Text = setup[CurrentSetupIndex].Smoothness.ToString();

            sbRed.Value = setup[CurrentSetupIndex].RedValue;
            sbGreen.Value = setup[CurrentSetupIndex].GreenValue;
            sbBlue.Value = setup[CurrentSetupIndex].BlueValue;

            tbLeftDial.Text = (LeftDialNr + 1).ToString();
            tbRightDial.Text = (RightDialNr + 1).ToString();

            tbLeftCalibration.Text = LeftCalibrateValue.ToString();
            tbRightCalibration.Text = RightCalibrateValue.ToString();

            cbOutputs.SelectedIndex = setup[CurrentSetupIndex].SelectedDeviceIdx;

            tbAutoOffTime.Text = AutoOffTimeout.ToString();
            cbAutoSwitch.Checked = AutoSwitch;
            tbAutoSwitchPerc.Text = setup[CurrentSetupIndex].AutoSwitchThreshold.ToString();
        }

        private void ReadFromRegistry()
        {
            RegistryKey regKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\VU1_Control");

            for (int i = 0; i < NR_SETUP; i++)
            {
                setup[i] = new Setup
                {
                    RedValue = (int)regKey.GetValue("Red" + i, 0),
                    GreenValue = (int)regKey.GetValue("Green" + i, 0),
                    BlueValue = (int)regKey.GetValue("Blue" + i, 0),

                    Sensitivity = (int)regKey.GetValue("Sensitivity" + i, 50) / 50.0,
                    Smoothness = (int)regKey.GetValue("Smoothness" + i, 20) / 100.0,

                    SelectedDeviceIdx = (int)regKey.GetValue("SelectedIndex" + i, -1),
                    AutoSwitchThreshold = (int)regKey.GetValue("AutoSwitchThreshold" + i, 0)
            };
            }

            LeftDialNr = (int)regKey.GetValue("LeftDialNr", 0);
            RightDialNr = (int)regKey.GetValue("RightDialNr", 1);

            LeftCalibrateValue = (int)regKey.GetValue("LeftCalibrate", 0);
            RightCalibrateValue = (int)regKey.GetValue("RightCalibrate", 0);

            AutoOffTimeout = (int)regKey.GetValue("AutoOffTimeout", 10);
            AutoSwitch = ((int)regKey.GetValue("AutoSwitch", 1)) == 1 ? true : false;
            
            regKey.Close();
        }

        private void WriteToRegistry()
        {
            RegistryKey regKey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\VU1_Control");

            for (int i = 0; i < NR_SETUP; i++)
            {
                regKey.SetValue("Red" + i, setup[i].RedValue);
                regKey.SetValue("Green" + i, setup[i].GreenValue);
                regKey.SetValue("Blue" + i, setup[i].BlueValue);

                regKey.SetValue("Sensitivity" + i, (int)(setup[i].Sensitivity * 50));
                regKey.SetValue("Smoothness" + i, (int)(setup[i].Smoothness * 100));

                regKey.SetValue("SelectedIndex" + i, setup[i].SelectedDeviceIdx);
                regKey.SetValue("AutoSwitchThreshold" + i, setup[i].AutoSwitchThreshold);
            }

            regKey.SetValue("LeftDialNr", LeftDialNr);
            regKey.SetValue("RightDialNr", RightDialNr);

            regKey.SetValue("LeftCalibrate", LeftCalibrateValue);
            regKey.SetValue("RightCalibrate", RightCalibrateValue);

            regKey.SetValue("AutoOffTimeout", AutoOffTimeout);
            regKey.SetValue("AutoSwitch", AutoSwitch ? 1 : 0);
            
            regKey.Close();
        }



        /// GetSerialPorts: Return list of all serial ports (USB) that have a PNP device connected.

        private const string vidPattern = @"VID_([0-9A-F]{4})";
        private const string pidPattern = @"PID_([0-9A-F]{4})";
        private List<ComPort> GetSerialPorts()
        {
            List<ComPort> list = new List<ComPort>();

            Debug("   In GetSerialPorts");

            using (var searcher = new ManagementObjectSearcher
                ("SELECT * FROM Win32_PnPEntity WHERE Name LIKE '%COM%' AND PNPClass = 'Ports'"))
            {
                Debug("   Before ManagementBaseObject");
                var ports = searcher.Get().Cast<ManagementBaseObject>().ToList();

                Debug("  GetSerialPorts: found devices:  " + ports.Count.ToString());
                foreach (ManagementBaseObject p in ports)
                {
                    ComPort c = new ComPort();
                    try
                    {
                        c.name = p.GetPropertyValue("DeviceID").ToString();
                        c.vid = p.GetPropertyValue("PNPDeviceID").ToString();
                        c.description = p.GetPropertyValue("Caption").ToString();

                        Match mVID = Regex.Match(c.vid, vidPattern, RegexOptions.IgnoreCase);
                        Match mPID = Regex.Match(c.vid, pidPattern, RegexOptions.IgnoreCase);

                        if (mVID.Success)
                            c.vid = mVID.Groups[1].Value;
                        if (mPID.Success)
                            c.pid = mPID.Groups[1].Value;

                        list.Add(c);

                        Debug("  Found device :" + c.name + " " + c.description + " " + c.vid + " " + c.pid);
                    }
                    catch
                    {
                        // ignore ports I can't open or error otherwise.
                        Debug("  Error checking device :" + c.name + " " + c.description);
                    }

                }
                return list;
            }
        }

        // FindVUMeters: From the list of USB ports find the one with the right vid and pid of the VU meters.
        // Note: Other devices that use the same UART can be found as well. E.g. a Conbee III stick. 

        public bool FindVUMeters()
        {
            // VID: 1027 (0x0403) and PID:24597 (0x6015)
            Debug("Find uv meters");

            List<ComPort> ports = GetSerialPorts();

            foreach (ComPort com in ports)
            {
                if (com.description != null && com.vid.Equals("0403") && com.pid.Equals("6015"))
                {
                    Match mCom = Regex.Match(com.description, @"COM[0-9]{1}", RegexOptions.IgnoreCase);
                    if (!mCom.Success)
                    {
                        mCom = Regex.Match(com.description, @"COM[0-9]{2}", RegexOptions.IgnoreCase);
                    }
                    if (mCom.Success)
                    {
                        string comPortName = mCom.Value;

                        Debug("  Found: Open port " + comPortName);

                        if (SP != null)
                        {
                            SP.Close();
                            SP.Dispose();
                        }

                        using (SP = new System.IO.Ports.SerialPort(comPortName, 115200, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One))
                        {
                            SP.ReadTimeout = 250; // SerialPort.InfiniteTimeout;
                            SP.WriteTimeout = 250;
                            SP.NewLine = "\r\n";
                            try
                            {
                                SP.Open();
                                return true;
                            }
                            catch (Exception e)
                            {
                                Debug("  Error opening port " + comPortName + ": " + e.Message);
                            }
                        }
                    }
                }
            }
            return false;
        }

 
        private void FillInputSelector()
        {
            Debug("Input selectors fill");
            AudioDevice device;

            cbOutputs.Items.Clear();
            for (int i = 0; i < NAudio.Wave.WaveOut.DeviceCount; i++)
            {
                var caps = NAudio.Wave.WaveOut.GetCapabilities(i);
                cbOutputs.Items.Add("Output: " + caps.ProductName);
                device = new AudioDevice(caps.ProductName, false);
                AudioDeviceList.Add(device);
                Debug("  found audio output device " + caps.ProductName);
            }

            for (int i = 0; i < NAudio.Wave.WaveIn.DeviceCount; i++)
            {
                var caps = NAudio.Wave.WaveIn.GetCapabilities(i);
                cbOutputs.Items.Add("Input: " + caps.ProductName);
                device = new AudioDevice(caps.ProductName, true);
                AudioDeviceList.Add(device);
                Debug("  found audio input device " + caps.ProductName);
            }
        }

        private void InitAudioDevices()
        {
            for (int i = 0; i < NR_SETUP; i++)
            {
                resetDevice(i);
            }
            for (int i = 0; i < NR_SETUP; i++)
            {
                handleStartStop(i, true);
            }
        }


        private void CheckInputsForAudio()
        {
            for (int i = 0; i < NR_SETUP; i++)
            {
                if (setup[i].SelectedDeviceIdx >= 0 && (setup[i].MaxLeftValueInt > 0 || setup[i].MaxRightValueInt > 0))
                {
                    CurrentSetupIndex = i;
                    ResetDeviceNeeded = true;
                    zeroFound = false;
                    zeroTimeoutStarted = false;
                    SetColor();
                    break;
                }
            }
        }



        private void resetDevice(int SetupIndex)
        {
            int deviceIndex = setup[SetupIndex].SelectedDeviceIdx;

            if (deviceIndex < 0) return;

            if (setup[SetupIndex].waveIn != null) setup[SetupIndex].waveIn.Dispose();
            if (setup[SetupIndex].loopback_capture != null) setup[SetupIndex].loopback_capture.Dispose();
            setup[SetupIndex].waveIn = null;
            setup[SetupIndex].loopback_capture = null;

            if (AudioDeviceList[deviceIndex].isInput)
            {
                setup[SetupIndex].waveIn = new NAudio.Wave.WaveInEvent
                {
                    DeviceNumber = deviceIndex - NAudio.Wave.WaveOut.DeviceCount, // indicates which microphone to use 
                    WaveFormat = new NAudio.Wave.WaveFormat(rate: 22050, bits: 16, channels: 2),
                    BufferMilliseconds = 100
                };
                setup[SetupIndex].waveIn.DataAvailable += (object2, sender2) => WaveIn_DataAvailable(object2, sender2, SetupIndex);

                var caps = NAudio.Wave.WaveIn.GetCapabilities(deviceIndex - NAudio.Wave.WaveOut.DeviceCount);
                Debug("Listening to input device " + caps.ProductName);
            }
            else
            {
                try
                {
                    MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
                    //here I see my sound card
                    var mm_dev = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active)[deviceIndex];

                    setup[SetupIndex].loopback_capture = new WasapiLoopbackCapture(mm_dev);
                    setup[SetupIndex].loopback_capture.DataAvailable += (object2, sender2) => Loopback_capture_DataAvailable(object2, sender2, SetupIndex);

                    Debug("Listening to output device " + mm_dev.DeviceFriendlyName);
                }
                catch (Exception ex)
                {
                    Debug("Error: " + ex.ToString());
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            handleStartStop(CurrentSetupIndex, !running);

            if (!running)
            {
                btnStart.Text = "Start";
            }
            else
            {
                btnStart.Text = "Stop";
            }
        }

        private void handleStartStop(int SetupIndex, bool run)
        {
            try
            {
                if (setup[SetupIndex].waveIn != null)
                {
                    if (run)
                    {
                        setup[SetupIndex].waveIn.StartRecording();
                    }
                    else
                    {
                        setup[SetupIndex].waveIn.StopRecording();
                    }
                    running = run;
                }
                else if (setup[SetupIndex].loopback_capture != null)
                {
                    if (run)
                    {
                        setup[SetupIndex].loopback_capture.StartRecording();
                    }
                    else
                    {
                        setup[SetupIndex].loopback_capture.StopRecording();
                    }
                    running = run;
                }
            }
            catch{ }

            if (!running)
            {
                SetVUMeterValues(0, 0);
            }
        }

       
        // WaveIn_DataAvailable: Event handler that gets called when audio is available from the input device (like a microphone).

        private void WaveIn_DataAvailable(object sender, NAudio.Wave.WaveInEventArgs e, int SetupIndex)
        {
            if (e.BytesRecorded == 0) return;
            
            int bytesPerSample = ((WaveInEvent)sender).WaveFormat.BitsPerSample / 8;
            int samplesRecorded = e.BytesRecorded / bytesPerSample;
            int leftValue = 0, rightValue = 0;

            for (int indexSample = 0; indexSample < samplesRecorded; indexSample += 2)
            {
                if (bytesPerSample == 2)
                {
                    leftValue = BitConverter.ToInt16(e.Buffer, indexSample * bytesPerSample);
                    rightValue = BitConverter.ToInt16(e.Buffer, (indexSample + 1) * bytesPerSample);
                }
                else if (bytesPerSample == 4)
                {
                    leftValue = BitConverter.ToInt32(e.Buffer, indexSample * bytesPerSample);
                    rightValue = BitConverter.ToInt32(e.Buffer, (indexSample + 1) * bytesPerSample);
                }
                setup[SetupIndex].MaxLeftValueInt = Math.Max(setup[SetupIndex].MaxLeftValueInt, Math.Abs((int)(leftValue / 327.68)));
                setup[SetupIndex].MaxRightValueInt = Math.Max(setup[SetupIndex].MaxRightValueInt, Math.Abs((int)(rightValue / 327.68)));
            }
        }


        // Loopback_capture_DataAvailable: Event handler that gets called when audio is available from the output device.

        private void Loopback_capture_DataAvailable(object sender, WaveInEventArgs e, int SetupIndex)
        {
            if (e.BytesRecorded == 0) return;

            int bytesPerSample = setup[SetupIndex].loopback_capture.WaveFormat.BitsPerSample / 8;
            int samplesRecorded = e.BytesRecorded / bytesPerSample;
            
            for (int indexSample = 0; indexSample < samplesRecorded; indexSample += 2)
            {
                if (bytesPerSample == 4)
                {
                    float value = BitConverter.ToSingle(e.Buffer, indexSample * bytesPerSample);
                    setup[SetupIndex].MaxLeftValueInt = (int)Math.Max(setup[SetupIndex].MaxLeftValueInt, Math.Abs(value * 100.0F));
                    value = BitConverter.ToSingle(e.Buffer, (indexSample + 1) * bytesPerSample);
                    setup[SetupIndex].MaxRightValueInt = (int)Math.Max(setup[SetupIndex].MaxRightValueInt, Math.Abs(value * 100.0F));
                }
            }

            //Debug("In:" + e.BytesRecorded.ToString() + " byte. L=" + MaxLeftValueInt.ToString() + " R=" + MaxRightValueInt.ToString() );
        }

        private int LastLeftValue = -1;
        private int LastRightValue = -1;

        // UpdateVU: Seperate task function that periodically sends the current max left and right values to the meters.
        
        private void UpdateVU()
        {
            for (; ; )
            {
                if (ResetDeviceNeeded)
                {
                    ResetDeviceNeeded = false;
                    resetDevice(CurrentSetupIndex);
                    handleStartStop(CurrentSetupIndex, running);
                }

                if (running)
                {
                    handleAutoOff();

                    if (LastLeftValue != setup[CurrentSetupIndex].MaxLeftValueInt || LastRightValue != setup[CurrentSetupIndex].MaxRightValueInt)
                    {
                        //double newValueL = (Smoothness * LastLeftValue) + (1.0 - Smoothness) * (MaxLeftValueInt * MaxLeftValueInt);
                        //double newValueR = (Smoothness * LastRightValue) + (1.0 - Smoothness) * (MaxRightValueInt * MaxRightValueInt);

                        double newValueL = LastLeftValue * setup[CurrentSetupIndex].Smoothness + setup[CurrentSetupIndex].MaxLeftValueInt * (1 - setup[CurrentSetupIndex].Smoothness);
                        double newValueR = LastRightValue * setup[CurrentSetupIndex].Smoothness + setup[CurrentSetupIndex].MaxRightValueInt * (1 - setup[CurrentSetupIndex].Smoothness);

                        LastLeftValue = (int)(newValueL + 0.5);
                        LastRightValue = (int)(newValueR + 0.5);

                        SetVUMeterValues(LastLeftValue, LastRightValue);
                    }
                    setup[CurrentSetupIndex].MaxLeftValueInt = setup[CurrentSetupIndex].MaxRightValueInt = 0;  // Reset max after use
                }
                Thread.Sleep(100);
            }
        }

        // SetVUMeterValues: Actually write VU values (left and right) to the meters' USB port.

        public void SetVUMeterValues(int value1, int value2)
        {
            if (SP == null)
            {
                return;
            }

            Thread.BeginCriticalRegion();

            if (!SP.IsOpen)
            {
                SP.Open();
            }

            int db1 = (int)((50.0 * Math.Log10(value1)) * setup[CurrentSetupIndex].Sensitivity);
            if (db1 > 100) db1 = 100;
            else if (db1 < 0) db1 = 0;
            db1 += (LeftCalibrateValue * db1) / 100;
            if (db1 > 100) db1 = 100;
            else if (db1 < 0) db1 = 0;

            int db2 = (int)((50.0 * Math.Log10(value2)) * setup[CurrentSetupIndex].Sensitivity);
            if (db2 > 100) db2 = 100;
            else if (db2 < 0) db2 = 0;
            db2 += (RightCalibrateValue * db2) / 100;
            if (db2 > 100) db2 = 100;
            else if (db2 < 0) db2 = 0;

            string vu1 = ">030400020" + LeftDialNr.ToString();
            string vu2 = ">030400020" + RightDialNr.ToString();

            try
            {
                SP.WriteLine(vu1 + db1.ToString("X2"));
            }
            catch (Exception e)
            {
                Debug("SetVUMeterValue 1: " + e.Message);
                Thread.EndCriticalRegion();
                return;
            }

            try
            {
                var readData = SP.ReadLine();
            }
            catch (Exception e)
            {
                Debug("SetVUMeterValue 2: " + e.Message);
                Thread.EndCriticalRegion();
                return;
            }

            Thread.Sleep(1);    // Needs some sleep otherwise the next readline will timeout often.

            try
            {
                SP.WriteLine(vu2 + db2.ToString("X2"));
            }
            catch (Exception e)
            {
                Debug("SetVUMeterValue 3: " + e.Message);
                Thread.EndCriticalRegion();
                return;
            }

            try
            {
                var readData2 = SP.ReadLine();
            }
            catch (Exception e)
            {
                Debug("SetVUMeterValue 4: " + e.Message);
                Thread.EndCriticalRegion();
                return;
            }

            Thread.EndCriticalRegion();
        }


        // handleAutoOff: Check if no input has been detected for a certain time. If so turn off the meters.

        private void handleAutoOff()
        {
            if (AutoSwitch && (DateTime.Now - lastAutoSwitchTime > TimeSpan.FromSeconds(AutoOffTimeout)))
            {
                CheckInputsForAudio();
                lastAutoSwitchTime = DateTime.Now;
            }

            if (AutoOffTimeout > 0)
            {
                if (setup[CurrentSetupIndex].MaxLeftValueInt < setup[CurrentSetupIndex].AutoSwitchThreshold && 
                    setup[CurrentSetupIndex].MaxRightValueInt < setup[CurrentSetupIndex].AutoSwitchThreshold)
                {
                    if (!zeroFound)
                    {
                        zeroFound = true;
                        firstZeroTime = DateTime.Now;
                    }
                    else if (!zeroTimeoutStarted)
                    {
                        if (DateTime.Now - firstZeroTime > TimeSpan.FromSeconds(AutoOffTimeout))
                        {
                            zeroTimeoutStarted = true;
                            SetColor(0, 0, 0);
                        }
                    }
                }
                else
                {
                    zeroFound = false;
                    if (zeroTimeoutStarted)
                    {
                        zeroTimeoutStarted = false;
                        SetColor();
                    }
                }
            }
        }

   
        private void cbOutputs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbOutputs.SelectedIndex >= 0)
            {
                setup[CurrentSetupIndex].SelectedDeviceIdx = cbOutputs.SelectedIndex;
                ResetDeviceNeeded = true;
            }
        }


        private void sbSensitivity_Scroll(object sender, ScrollEventArgs e)
        {
            setup[CurrentSetupIndex].Sensitivity = Math.Max(1, sbSensitivity.Value) / 50.0;
            txtSensitiviy.Text = (sbSensitivity.Value - 50).ToString();
        }

        private void sbSmoothness_Scroll(object sender, ScrollEventArgs e)
        {
            setup[CurrentSetupIndex].Smoothness = Math.Max(1, sbSmoothness.Value) / 100.0;
            txtSmoothness.Text = setup[CurrentSetupIndex].Smoothness.ToString();
        }

        private void tbLeftDial_TextChanged(object sender, EventArgs e)
        {
            if (tbLeftDial.Text.Length > 0)
            {
                LeftDialNr = int.Parse(tbLeftDial.Text) - 1;
            }
        }

        private void tbRightDial_TextChanged(object sender, EventArgs e)
        {
            if (tbRightDial.Text.Length > 0)
            {
                RightDialNr = int.Parse(tbRightDial.Text) - 1;
            }
        }

        private void tbLeftCalibration_TextChanged(object sender, EventArgs e)
        {
            if (tbLeftCalibration.Text.Length > 0)
            {
                try
                {
                    LeftCalibrateValue = int.Parse(tbLeftCalibration.Text);
                    if (LeftCalibrateValue < -99) LeftCalibrateValue = -99;
                    if (LeftCalibrateValue > 99) LeftCalibrateValue = 99;
                }
                catch { }
            }
        }


        private void tbRightCalibration_TextChanged(object sender, EventArgs e)
        {
            if (tbRightCalibration.Text.Length > 0)
            {
                try
                {
                    RightCalibrateValue = int.Parse(tbRightCalibration.Text);
                    if (RightCalibrateValue < -100) RightCalibrateValue = -100;
                    if (RightCalibrateValue > 100) RightCalibrateValue = 100;
                }
                catch { }
            }
        }



        private void sbRed_Scroll(object sender, ScrollEventArgs e)
        {
            setup[CurrentSetupIndex].RedValue = sbRed.Value;
            SetColor();
        }

        private void sbGreen_Scroll(object sender, ScrollEventArgs e)
        {
            setup[CurrentSetupIndex].GreenValue = sbGreen.Value;
            SetColor();
        }

        private void sbBlue_Scroll(object sender, ScrollEventArgs e)
        {
            setup[CurrentSetupIndex].BlueValue = sbBlue.Value;
            SetColor();
        }

        private void SetColor()
        {
            SetColor(setup[CurrentSetupIndex].RedValue, setup[CurrentSetupIndex].GreenValue, setup[CurrentSetupIndex].BlueValue);
        }

        private void SetColor(int R, int G, int B)
        {
            if (SP == null)
            {
                return;
            }

            Thread.BeginCriticalRegion();

            bool wasRunning = running;
            running = false;

            if (!SP.IsOpen)
            {
                SP.Open();
            }

            // rgb ;  '>1303000500 1E322800'    '>1303000500 00640000'   1303000501 00006400

            string vu1 = ">130300050" + LeftDialNr.ToString();
            string vu2 = ">130300050" + RightDialNr.ToString();
            string color = R.ToString("X2") + G.ToString("X2") + B.ToString("X2") + "00";

            try { SP.WriteLine(vu1 + color); } catch { }
            try { var readData = SP.ReadLine(); } catch { }

            try { SP.WriteLine(vu2 + color); } catch { }
            try { var readData2 = SP.ReadLine(); } catch { }

            running = wasRunning;
            Thread.EndCriticalRegion();
        }

        private void SetEasing(bool dial, int step, int period)
        {
            if (SP == null)
            {
                return;
            }

            Thread.BeginCriticalRegion();

            if (!SP.IsOpen)
            {
                SP.Open();
            }

            int stepCmd = dial ? 4 : 6;
            int periodCmd = dial ? 5 : 7;

            // easing: dial step: >140200050000000064, dial period: >150200050000000001
            // backlight easing: >160200050000000063'
            string start1 = "0200050" + LeftDialNr.ToString();
            string start2 = "0200050" + RightDialNr.ToString();
            string step1 = ">1" + stepCmd.ToString() + start1 + "000000" + step.ToString("X2");
            string step2 = ">1" + stepCmd.ToString() + start2 + "000000" + step.ToString("X2"); ;
            string period1 = ">1" + periodCmd.ToString() + start1 + "000000" + period.ToString("X2"); ;
            string period2 = ">1" + periodCmd.ToString() + start2 + "000000" + period.ToString("X2"); ;

            try { SP.WriteLine(step1); } catch { }
            try { var readData = SP.ReadLine(); } catch { }

            try { SP.WriteLine(step2); } catch { }
            try { var readData = SP.ReadLine(); } catch { }

            try { SP.WriteLine(period1); } catch { }
            try { var readData = SP.ReadLine(); } catch { }

            try { SP.WriteLine(period2); } catch { }
            try { var readData = SP.ReadLine(); } catch { }

            Thread.EndCriticalRegion();
        }


        // Simple and easy to use debug function

        string DebugFileName = @"C:\temp\vu_debug.txt";

        private void Debug(string Message)
        {
            if (DebugStream == null)
            {
                DebugStream = new StreamWriter(DebugFileName);
            }
            DebugStream.WriteLine(DateTime.Now + ": " + Message);
            DebugStream.Flush();
        }

        private void btnSetup1_Click(object sender, EventArgs e)
        {
            CurrentSetupIndex = 0;
            UpdateUI();
            SetColor();
            btnSetup1.BackColor = Color.PaleGreen;
            btnSetup2.BackColor = Color.WhiteSmoke;
            btnSetup3.BackColor = Color.WhiteSmoke;
        }

        private void btnSetup2_Click(object sender, EventArgs e)
        {
            CurrentSetupIndex = 1;
            UpdateUI();
            SetColor();
            btnSetup1.BackColor = Color.WhiteSmoke;
            btnSetup2.BackColor = Color.PaleGreen;
            btnSetup3.BackColor = Color.WhiteSmoke;
        }


        private void btnSetup3_Click(object sender, EventArgs e)
        {
            CurrentSetupIndex = 2;
            UpdateUI();
            SetColor();
            btnSetup1.BackColor = Color.WhiteSmoke;
            btnSetup2.BackColor = Color.WhiteSmoke;
            btnSetup3.BackColor = Color.PaleGreen;
        }

        private void tbAutoOffTime_TextChanged(object sender, EventArgs e)
        {
            if (tbAutoOffTime.Text.Length > 0)
            {
                try
                {
                    AutoOffTimeout = int.Parse(tbAutoOffTime.Text);
                }
                catch { }
            }
        }

        private void tbAutoSwitchPerc_TextChanged(object sender, EventArgs e)
        {
            if (tbAutoSwitchPerc.Text.Length > 0)
            {
                try
                {
                    setup[CurrentSetupIndex].AutoSwitchThreshold = int.Parse(tbAutoSwitchPerc.Text);
                }
                catch { }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            running = false;
            ResetDeviceNeeded = true;
            FillInputSelector();
            if (!FindVUMeters())
            {
                MessageBox.Show("No VU meters found.");
            }
            CurrentSetupIndex = 0;
            UpdateUI();
            InitAudioDevices();
            SP.Close();
        }

        private void cbAutoSwitch_CheckedChanged(object sender, EventArgs e)
        {
            AutoSwitch = cbAutoSwitch.Checked;
        }
    }

    class ComPort // custom struct with our desired values
    {
        public string name;
        public string vid;
        public string pid;
        public string description;
    }

    class AudioDevice
    {
        public string name;
        public bool isInput;    // 1 if Windows audio input device, else output :-)

        public AudioDevice(string productName, bool IsInput)
        {
            name = productName;
            isInput = IsInput;
        }
    }

    class Setup
    {
        public double Sensitivity { get; set; } = 1.0;
        public double Smoothness { get; set; } = 0.2;

        public int RedValue { get; set; }
        public int GreenValue { get; set; }
        public int BlueValue { get; set; }

        public int SelectedDeviceIdx { get; set; } = -1;

        public WaveInEvent waveIn;
        public WasapiLoopbackCapture loopback_capture;

        public int MaxLeftValueInt { get; set; }
        public int MaxRightValueInt { get; set; }

        public int AutoSwitchThreshold { get; set; } = 0;
    }
}





