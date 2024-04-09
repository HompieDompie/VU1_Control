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
        int CurrentSetupIndex = 0;
        int CurrentInputIndex = 0;

        bool running = false;
        SerialPort SP { get; set; }
        StreamWriter DebugStream;
        
        DateTime firstZeroTime, lastAutoSwitchTime;
        bool zeroFound = false;
        bool zeroTimeoutStarted = false;
        MMDeviceCollection audioDevices;

        int LeftDialNr = 0;
        int RightDialNr = 1;
        int LeftCalibrateValue;
        int RightCalibrateValue;
        int AutoOffTimeout = 10;
        bool AutoSwitch = true;

        Semaphore SendSemaphore = new Semaphore(1, 1);

        public Form1()
        {
            Debug("Program start");

            InitializeComponent();

            InitProgram();

            // Start when all is setup well
            if (setup[CurrentSetupIndex].SelectedDeviceIdx >= 0 && cbOutputs.Items.Count >= setup[CurrentSetupIndex].SelectedDeviceIdx)
            {
                btnStart.Text = "Stop";
                running = true;
            }

            Task.Run(UpdateVU);
            Task.Run(CheckAutoSwitch);
        }

        private void InitProgram()
        {
            running = false;
            FillInputSelector();
            if (!FindVUMeters())
            {
                MessageBox.Show("No VU meters found.");
                Environment.Exit(0);
            }
            ReadFromRegistry();
            CurrentSetupIndex = CurrentInputIndex = 0;    // 1st setup takes preference on startup

            SetEasing(true, 100, 1);
            SetEasing(false, 30, 500);
            UpdateUI();
            SetVUMeterValues(0, 0);
            SetSetupColor();

            InitAudioDevices();
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
            if (CurrentSetupIndex != tcSetup.SelectedIndex)
            {
                tcSetup.SelectedIndex = CurrentSetupIndex;  
            }
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

            cbAutoScale.Checked = setup[CurrentSetupIndex].AutoScale;
            tbAutoScaleTime.Text = setup[CurrentSetupIndex].AutoScaleTime.ToString();  

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
                    AutoSwitchThreshold = (int)regKey.GetValue("AutoSwitchThreshold" + i, 0),

                    AutoScale = (int)regKey.GetValue("AutoScale" + i, 0) == 1,
                    AutoScaleTime = (int)regKey.GetValue("AutoScaleTime" + i, 60)
                };
            }

            LeftDialNr = (int)regKey.GetValue("LeftDialNr", 0);
            RightDialNr = (int)regKey.GetValue("RightDialNr", 1);

            LeftCalibrateValue = (int)regKey.GetValue("LeftCalibrate", 0);
            RightCalibrateValue = (int)regKey.GetValue("RightCalibrate", 0);

            AutoOffTimeout = (int)regKey.GetValue("AutoOffTimeout", 10);
            AutoSwitch = (int)regKey.GetValue("AutoSwitch", 1) == 1;
            
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

                regKey.SetValue("AutoScale" + i, setup[i].AutoScale ? 1 : 0);
                regKey.SetValue("AutoScaleTime" + i, (int)setup[i].AutoScaleTime);
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

            cbOutputs.Items.Clear();

            MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
            audioDevices = enumerator.EnumerateAudioEndPoints(DataFlow.All, DeviceState.Active);
            enumerator.Dispose();

            for (int i = 0; i < audioDevices.Count; i++)
            {
                MMDevice mm_dev = audioDevices[i];

                if (mm_dev.DataFlow == DataFlow.Render)
                {
                    cbOutputs.Items.Add("Output: "+ mm_dev.FriendlyName);
                    Debug("  found audio output device " + mm_dev.FriendlyName);
                }
                else
                {
                    cbOutputs.Items.Add("Input: " + mm_dev.FriendlyName);
                    Debug("  found audio input device " + mm_dev.FriendlyName);
                }
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
                if (setup[i].SelectedDeviceIdx >= 0 && setup[i].HasInput) 
                {
                    if (i != CurrentInputIndex)
                    {
                        CurrentInputIndex = i;
                        zeroFound = false;
                        zeroTimeoutStarted = false;
                        setup[i].MaxLeftValueInt = setup[i].MaxRightValueInt = 0;
                        SetInputColor();
                    }
                    break;
                }
            }
        }



        private void resetDevice(int SetupIndex)
        {
            int deviceIndex = setup[SetupIndex].SelectedDeviceIdx;

            if (deviceIndex < 0) return;

            if (setup[SetupIndex].loopback_capture != null) setup[SetupIndex].loopback_capture.Dispose();
            setup[SetupIndex].loopback_capture = null;

            if (setup[SetupIndex].capture != null) setup[SetupIndex].capture.Dispose();
            setup[SetupIndex].capture = null;

            try
            {
                //here I see my sound card
                MMDevice mm_dev = audioDevices[deviceIndex];

                if (mm_dev.DataFlow == DataFlow.Render)
                {
                    setup[SetupIndex].loopback_capture = new WasapiLoopbackCapture(mm_dev);
                    setup[SetupIndex].loopback_capture.DataAvailable += (object2, sender2) => Loopback_capture_DataAvailable(object2, sender2, SetupIndex);
                    setup[SetupIndex].BytesPerSample = setup[SetupIndex].loopback_capture.WaveFormat.BitsPerSample / 8;
                }
                else
                {
                    setup[SetupIndex].capture = new WasapiCapture(mm_dev);
                    setup[SetupIndex].capture.DataAvailable += (object2, sender2) => Loopback_capture_DataAvailable(object2, sender2, SetupIndex);
                    setup[SetupIndex].BytesPerSample = setup[SetupIndex].capture.WaveFormat.BitsPerSample / 8;
                }

                Debug("Listening to output device " + mm_dev.FriendlyName);
            }
            catch (Exception ex)
            {
                Debug("Error: " + ex.ToString());
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < NR_SETUP; i++)
            {
                handleStartStop(i, !running);
            }

            if (running)
            {
                btnStart.Text = "Start";
            }
            else
            {
                btnStart.Text = "Stop";
            }

            running = !running;
        }

        private void handleStartStop(int SetupIndex, bool run)
        {
            try
            {
                if (setup[SetupIndex].loopback_capture != null)
                {
                    if (run)
                    {
                        setup[SetupIndex].loopback_capture.StartRecording();
                    }
                    else
                    {
                        setup[SetupIndex].loopback_capture.StopRecording();
                    }
                }
                else if (setup[SetupIndex].capture != null)
                {
                    if (run)
                    {
                        setup[SetupIndex].capture.StartRecording();
                    }
                    else
                    {
                        setup[SetupIndex].capture.StopRecording();
                    }
                }
            }
            catch{ }

            if (!run)
            {
                SetVUMeterValues(0, 0);
            }
        }

       
        // Loopback_capture_DataAvailable: Event handler that gets called when audio is available from the input or output device.

        private void Loopback_capture_DataAvailable(object sender, WaveInEventArgs e, int SetupIndex)
        {
            float leftValue = 0, rightValue = 0;
            int bytesPerSample = setup[SetupIndex].BytesPerSample;

            if (e.BytesRecorded == 0 || bytesPerSample == 0)
            {
                setup[SetupIndex].HasInput = false;
                return;
            }

            int samplesRecorded = e.BytesRecorded / bytesPerSample;

            for (int indexSample = 0; indexSample < samplesRecorded; indexSample += 2)
            {
                if (bytesPerSample == 4)
                {
                    leftValue = BitConverter.ToSingle(e.Buffer, indexSample * bytesPerSample);
                    rightValue = BitConverter.ToSingle(e.Buffer, (indexSample + 1) * bytesPerSample);
                }
                //else if (bytesPerSample == 8)
                //{
                //    leftValue = (float)BitConverter.ToDouble(e.Buffer, indexSample * bytesPerSample);
                //    rightValue = (float)BitConverter.ToDouble(e.Buffer, (indexSample + 1) * bytesPerSample);
                //}

                setup[SetupIndex].MaxLeftValueInt = (int)Math.Max(setup[SetupIndex].MaxLeftValueInt, Math.Abs(leftValue * 100.0F));
                setup[SetupIndex].MaxRightValueInt = (int)Math.Max(setup[SetupIndex].MaxLeftValueInt, Math.Abs(rightValue * 100.0F));

                setup[SetupIndex].HasInput = setup[SetupIndex].MaxLeftValueInt >= setup[CurrentInputIndex].AutoSwitchThreshold ||
                                             setup[SetupIndex].MaxRightValueInt >= setup[CurrentInputIndex].AutoSwitchThreshold;
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
                if (running)
                {
                    handleAutoOff();

                    if (LastLeftValue != setup[CurrentInputIndex].MaxLeftValueInt || LastRightValue != setup[CurrentInputIndex].MaxRightValueInt)
                    {
                        //double newValueL = (Smoothness * LastLeftValue) + (1.0 - Smoothness) * (MaxLeftValueInt * MaxLeftValueInt);
                        //double newValueR = (Smoothness * LastRightValue) + (1.0 - Smoothness) * (MaxRightValueInt * MaxRightValueInt);
                        double smoothness = setup[CurrentInputIndex].Smoothness;

                        double newValueL = LastLeftValue * smoothness + setup[CurrentInputIndex].MaxLeftValueInt * (1 - smoothness);
                        double newValueR = LastRightValue * smoothness + setup[CurrentInputIndex].MaxRightValueInt * (1 - smoothness);

                        LastLeftValue = (int)(newValueL + 0.5);
                        LastRightValue = (int)(newValueR + 0.5);

                        SetVUMeterValues(LastLeftValue, LastRightValue);
                    }
                   
                    setup[CurrentInputIndex].MaxLeftValueInt = setup[CurrentInputIndex].MaxRightValueInt = 0;  // Reset max after use
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

            SendSemaphore.WaitOne();

            if (!SP.IsOpen)
            {
                SP.Open();
            }

            int db1 = (int)((50.0 * Math.Log10(value1)) * setup[CurrentInputIndex].Sensitivity);
            if (db1 > 100) db1 = 100;
            else if (db1 < 0) db1 = 0;
            db1 += (LeftCalibrateValue * db1) / 100;
            if (db1 > 100) db1 = 100;
            else if (db1 < 0) db1 = 0;

            int db2 = (int)((50.0 * Math.Log10(value2)) * setup[CurrentInputIndex].Sensitivity);
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
                SendSemaphore.Release();
                return;
            }

            try
            {
                var readData = SP.ReadLine();
            }
            catch (Exception e)
            {
                Debug("SetVUMeterValue 2: " + e.Message);
                SendSemaphore.Release();
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
                SendSemaphore.Release();
                return;
            }

            try
            {
                var readData2 = SP.ReadLine();
            }
            catch (Exception e)
            {
                Debug("SetVUMeterValue 4: " + e.Message);
                SendSemaphore.Release();
                return;
            }
                
            SendSemaphore.Release();
        }


        // handleAutoOff: Check if no input has been detected for a certain time. If so turn off the meters.

        private void handleAutoOff()
        {
            if (AutoOffTimeout > 0)
            {
                if (setup[CurrentInputIndex].MaxLeftValueInt < setup[CurrentInputIndex].AutoSwitchThreshold && 
                    setup[CurrentInputIndex].MaxRightValueInt < setup[CurrentInputIndex].AutoSwitchThreshold)
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
                        SetInputColor();
                    }
                }
            }
        }

        private void CheckAutoSwitch()
        {
            for (; ; )
            {
                try
                {
                    if (running && AutoSwitch && (DateTime.Now - lastAutoSwitchTime > TimeSpan.FromSeconds(AutoOffTimeout)))
                    {
                        CheckInputsForAudio();
                        lastAutoSwitchTime = DateTime.Now;
                    }
                }
                catch { }

                Thread.Sleep(1000);
            }
        }



        private void cbOutputs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbOutputs.SelectedIndex >= 0)
            {
                setup[CurrentSetupIndex].SelectedDeviceIdx = cbOutputs.SelectedIndex;
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
            SetSetupColor();
        }

        private void sbGreen_Scroll(object sender, ScrollEventArgs e)
        {
            setup[CurrentSetupIndex].GreenValue = sbGreen.Value;
            SetSetupColor();
        }

        private void sbBlue_Scroll(object sender, ScrollEventArgs e)
        {
            setup[CurrentSetupIndex].BlueValue = sbBlue.Value;
            SetSetupColor();
        }

        private void SetSetupColor()
        {
            SetColor(setup[CurrentSetupIndex].RedValue, setup[CurrentSetupIndex].GreenValue, setup[CurrentSetupIndex].BlueValue);

            paColor.BackColor = Color.FromArgb(255, setup[CurrentSetupIndex].RedValue * 2, setup[CurrentSetupIndex].GreenValue * 2, setup[CurrentSetupIndex].BlueValue * 2);
        }

        private void SetInputColor()
        {
            SetColor(setup[CurrentInputIndex].RedValue, setup[CurrentInputIndex].GreenValue, setup[CurrentInputIndex].BlueValue);
        }

        private void SetColor(int R, int G, int B)
        {
            if (SP == null)
            {
                return;
            }

            SendSemaphore.WaitOne();

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

            Thread.Sleep(1);    // Needs some sleep otherwise the next readline will timeout often.

            try { SP.WriteLine(vu2 + color); } catch { }
            try { var readData2 = SP.ReadLine(); } catch { }

            running = wasRunning;
            SendSemaphore.Release();
        }

        private void SetEasing(bool dial, int step, int period)
        {
            if (SP == null)
            {
                return;
            }

            SendSemaphore.WaitOne();

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
            Thread.Sleep(1);    // Needs some sleep otherwise the next readline will timeout often.

            try { SP.WriteLine(step2); } catch { }
            try { var readData = SP.ReadLine(); } catch { }
            Thread.Sleep(1);    // Needs some sleep otherwise the next readline will timeout often.

            try { SP.WriteLine(period1); } catch { }
            try { var readData = SP.ReadLine(); } catch { }
            Thread.Sleep(1);    // Needs some sleep otherwise the next readline will timeout often.

            try { SP.WriteLine(period2); } catch { }
            try { var readData = SP.ReadLine(); } catch { }
            Thread.Sleep(1);    // Needs some sleep otherwise the next readline will timeout often.

            SendSemaphore.Release();
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

        private void tcSetup_Selected(object sender, TabControlEventArgs e)
        {
            CurrentSetupIndex = e.TabPageIndex;

            UpdateUI();
            SetSetupColor();
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
            InitProgram();
            if (!(SP is null)) SP.Close();
        }

        private void cbAutoScale_CheckedChanged(object sender, EventArgs e)
        {
            setup[CurrentSetupIndex].AutoScale = cbAutoScale.Checked;  
        }

        private void tbAutoScaleTime_TextChanged(object sender, EventArgs e)
        {
            if (tbAutoScaleTime.Text.Length > 0)
            {
                try
                {
                    setup[CurrentSetupIndex].AutoScaleTime = int.Parse(tbAutoScaleTime.Text);
                }
                catch { }
            }
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

    
    class Setup
    {
        public double Sensitivity { get; set; } = 1.0;
        public double Smoothness { get; set; } = 0.2;

        public int RedValue { get; set; }
        public int GreenValue { get; set; }
        public int BlueValue { get; set; }

        public int SelectedDeviceIdx { get; set; } = -1;

        //public MMDevice device;                         // naudio device
        public WasapiCapture capture;                   // for input devices
        public WasapiLoopbackCapture loopback_capture;  // for output devices

        public int MaxLeftValueInt { get; set; }
        public int MaxRightValueInt { get; set; }
        public bool HasInput { get; set;}

        public int AutoSwitchThreshold { get; set; } = 0;
        public int BytesPerSample { get; set; } = 0;

        public bool AutoScale { get; set; }
        public int AutoScaleTime { get; set; } = 0;
    }
}





