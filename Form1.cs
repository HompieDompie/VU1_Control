using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Timers;
using System.Runtime.Remoting.Messaging;
using Microsoft.Win32;
using System.Runtime.CompilerServices;
using System.IO;
using System.IO.Ports;
using System.Management;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Reflection;
using System.Diagnostics.Eventing.Reader;



struct ComPort // custom struct with our desired values
{
    public string name;
    public string vid;
    public string pid;
    public string description;
}



namespace VU1_Control
{   
    public partial class Form1 : Form
    {
        double Sensitivity = 1.0;
        double Smoothness = 0.2;
        WaveInEvent waveIn;
        WasapiLoopbackCapture loopback_capture;
        static int MaxLeftValueInt { get; set; }
        static int MaxRightValueInt { get; set; }
        static float MaxLeftValueFloat { get; set;  }
        static float MaxRightValueFloat { get; set; }
        
        bool running = false;
        static SerialPort SP { get; set; }
        string DebugFileName = @"C:\temp\vu_debug.txt";
        StreamWriter DebugStream;
        int RedValue, GreenValue, BlueValue;
        DateTime firstZeroTime;
        bool zeroFound = false;
        bool zeroTimeoutStarted = false;
        RegistryKey RegKey;
        int LeftDialNr = 0;
        int RightDialNr = 1;
        int SelectedDeviceIdx = -1;
        int SelectedInput = -1;     // 0 = output, 1 = input
        bool ResetDeviceNeeded = false;

        public Form1()
        {
            DebugStream = new StreamWriter(DebugFileName);
            DebugStream.WriteLine("Program start");
            DebugStream.Flush();

            InitializeComponent();

            FillInputSelector();

            DebugStream.WriteLine("Input selectors filled");
            DebugStream.Flush();
            if (!FindUVMeters())
            {
                MessageBox.Show("No VU meters found.");
                Environment.Exit(0);
            }

            SetUVMeterValues(0, 0);
            SetEasing(true, 100, 10);
            SetEasing(false, 100, 1);

            DebugStream.WriteLine("After find uv meters");
            DebugStream.Flush();

            ReadFromRegistry();
            UpdateUI();
            SetColor(); 

            // Start when all is setup well
            if (SelectedDeviceIdx >= 0 && 
                ((SelectedInput == 1 && cbInputs.Items.Count >= SelectedDeviceIdx) ||
                 (SelectedInput == 0 && cbOutputs.Items.Count >= SelectedDeviceIdx)))
            {
                ResetDeviceNeeded = true;
                btnStart.Text = "Stop";
                running = true;
            }

            Task.Run(UpdateVU);
        }

        private void Form1_Close(object sender, EventArgs e)
        {
            WriteToRegistry();
            SetColor(0, 0, 0);
            SetUVMeterValues(0, 0);
            Thread.Sleep(500);
        }

        private void UpdateUI()
        {
            sbSensitivity.Value = (int)(Sensitivity * 50);
            txtSensitiviy.Text = (sbSensitivity.Value - 50).ToString();

            sbSmoothness.Value = (int)(Smoothness * 100);
            txtSmoothness.Text = Smoothness.ToString();

            sbRed.Value = RedValue;
            sbGreen.Value = GreenValue;
            sbBlue.Value = BlueValue;

            tbLeftDial.Text = (LeftDialNr + 1).ToString();
            tbRightDial.Text = (RightDialNr + 1).ToString();

            if (SelectedInput == 1)
            {
                cbOutputs.SelectedIndex = -1;
                cbInputs.SelectedIndex = SelectedDeviceIdx;
            }
            else
            {
                cbOutputs.SelectedIndex = SelectedDeviceIdx;
                cbInputs.SelectedIndex = -1;
            }

        }

        private void ReadFromRegistry()
        {
            RegKey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\VU1_Control");

            RedValue = (int)RegKey.GetValue("Red", 0);
            GreenValue = (int)RegKey.GetValue("Green", 0);
            BlueValue = (int)RegKey.GetValue("Blue", 0);

            Sensitivity = (int)RegKey.GetValue("Sensitivity", 50) / 50.0;
            Smoothness = (int)RegKey.GetValue("Smoothness", 20) / 100.0;

            SelectedDeviceIdx = (int)RegKey.GetValue("SelectedIndex", -1);
            SelectedInput = (int)RegKey.GetValue("SelectedInput", -1);
        }

        private void WriteToRegistry()
        {
            RegKey.SetValue("Red", RedValue);
            RegKey.SetValue("Green", GreenValue);
            RegKey.SetValue("Blue", BlueValue);

            RegKey.SetValue("Sensitivity", (int)(Sensitivity * 50));
            RegKey.SetValue("Smoothness", (int)(Smoothness * 100));

            RegKey.SetValue("SelectedIndex", SelectedDeviceIdx);
            RegKey.SetValue("SelectedInput", SelectedInput);
        }

        
        private const string vidPattern = @"VID_([0-9A-F]{4})";
        private const string pidPattern = @"PID_([0-9A-F]{4})";
        private List<ComPort> GetSerialPorts()
        {
            List<ComPort> list = new List<ComPort>();

            DebugStream.WriteLine("   In GetSerialPorts");
            DebugStream.Flush();

            using (var searcher = new ManagementObjectSearcher
                ("SELECT * FROM Win32_PnPEntity WHERE Name LIKE '%COM%' AND PNPClass = 'Ports'"))
            {
                DebugStream.WriteLine("   Before ManagementBaseObject");
                DebugStream.Flush();
                var ports = searcher.Get().Cast<ManagementBaseObject>().ToList();

                DebugStream.WriteLine("  GetSerialPorts: found devices:  " + ports.Count.ToString());
                DebugStream.Flush();
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

                        DebugStream.WriteLine("  Found device :" + c.name + " " + c.description + " " + c.vid + " " + c.pid);
                        DebugStream.Flush();
                    }
                    catch
                    {
                        // ignore ports I can't open or error otherwise.
                        DebugStream.WriteLine("  Error checking device :" + c.name + " " + c.description);
                        DebugStream.Flush();
                    }
                    
                }
                return list;
            }
        }

        public bool FindUVMeters()
        {
            // VID: 1027 (0x0403) and PID:24597 (0x6015)

            List<ComPort> ports = GetSerialPorts();
            //if we want to find one device
            ComPort com = ports.FindLast(c => c.vid.Equals("0403") && c.pid.Equals("6015"));

            DebugStream.WriteLine("  FindUVMeters : Find UV meter: " + com.description);
            DebugStream.Flush();

            if (com.description != null)
            {

                Match mCom = Regex.Match(com.description, @"COM[0-9]{1}", RegexOptions.IgnoreCase);
                if (!mCom.Success)
                {
                    mCom = Regex.Match(com.description, @"COM[0-9]{2}", RegexOptions.IgnoreCase);
                }
                if (mCom.Success)
                {
                    string comPortName = mCom.Value;

                    DebugStream.WriteLine("  FindUVMeters : Open port " + comPortName);
                    DebugStream.Flush();

                    if (SP != null)
                    {
                        SP.Close();
                        SP.Dispose();
                    }

                    using (SP = new System.IO.Ports.SerialPort(comPortName, 115200, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One))
                    {
                        SP.ReadTimeout = 1000; // SerialPort.InfiniteTimeout;
                        SP.WriteTimeout = 1000;  
                        SP.NewLine = "\r\n";

                        try
                        {
                            SP.Open();
                        }
                        catch(Exception e)
                        {
                            DebugStream.WriteLine(" Error opening port " + comPortName + ": " + e.Message);
                            DebugStream.Flush();
                            return false;
                        }
                    }
                }
            }
            else
            {
                DebugStream.WriteLine("  FindUVMeters : com.description empty");
                DebugStream.Flush();
                return false;
            }

            return true;
        }


        public void SetUVMeterValues(int value1, int value2)
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

            int db1 = (int)((20 * Math.Log10(value1)) * Sensitivity);
            if (db1 > 100) db1 = 100;
            if (db1 < 0) db1 = 0;

            int db2 = (int)((20 * Math.Log10(value2)) * Sensitivity);
            if (db2 > 100) db2 = 100;
            if (db2 < 0) db2 = 0;

  

            string vu1 = ">030400020" + LeftDialNr.ToString();
            string vu2 = ">030400020" + RightDialNr.ToString();

            //DebugStream.WriteLine(db1.ToString() + " " + db2.ToString());
            //DebugStream.Flush();
            SP.DiscardInBuffer();

            try
            {
                SP.WriteLine(vu1 + db1.ToString("X2"));
            }
            catch(Exception e)
            {
                DebugStream.WriteLine("SetUVMeterValue 1: " + e.Message);
                DebugStream.Flush();
                Thread.EndCriticalRegion();
                return;
            }
 
            try
            {
                var readData = SP.ReadLine();
               // DebugStream.WriteLine("Rec1: " + readData);
               // DebugStream.Flush();
            }
            catch (Exception e)
            {
                DebugStream.WriteLine("SetUVMeterValue 2: " + e.Message);
                DebugStream.Flush();
                Thread.EndCriticalRegion();
                return;
            }

            try
            {
                SP.WriteLine(vu2 + db2.ToString("X2"));
            }
            catch (Exception e)
            {
                DebugStream.WriteLine("SetUVMeterValue 3: " + e.Message);
                DebugStream.Flush();
                Thread.EndCriticalRegion();
                return;
            }

            try
            {
                var readData2 = SP.ReadLine();
                //DebugStream.WriteLine("Rec2: " + readData2);
                //DebugStream.Flush();
            }
            catch (Exception e)
            {
                DebugStream.WriteLine("SetUVMeterValue 4: " + e.Message);
                DebugStream.Flush();
                Thread.EndCriticalRegion();
                return;
            }

            Thread.EndCriticalRegion();
        }


        private void FillInputSelector()
        {
            for (int i = 0; i < NAudio.Wave.WaveIn.DeviceCount; i++)
            {
                var caps = NAudio.Wave.WaveIn.GetCapabilities(i);
                cbInputs.Items.Add(caps.ProductName);
                DebugStream.WriteLine("  found audio input device " + caps.ProductName);
            }

            for (int i = 0; i < NAudio.Wave.WaveOut.DeviceCount; i++)
            {
                var caps = NAudio.Wave.WaveOut.GetCapabilities(i);
                cbOutputs.Items.Add(caps.ProductName);
                DebugStream.WriteLine("  found audio output device " + caps.ProductName);
            }
        }

        private void resetDevice(int Index, bool input)
        {
            if (waveIn != null) waveIn.Dispose();
            if (loopback_capture != null) loopback_capture.Dispose();
            waveIn = null;
            loopback_capture = null;

            if (input)
            {
                waveIn = new NAudio.Wave.WaveInEvent
                { 
                    DeviceNumber = Index, // indicates which microphone to use
                    WaveFormat = new NAudio.Wave.WaveFormat(rate: 22050, bits: 16, channels: 2),
                    BufferMilliseconds = 100
                };
                waveIn.DataAvailable += WaveIn_DataAvailable;

                var caps = NAudio.Wave.WaveIn.GetCapabilities(Index);
                DebugStream.WriteLine("Listening to input device " + caps.ProductName);
                DebugStream.Flush();
            }
            else
            {
                try
                {
                    MMDeviceEnumerator enumerator = new MMDeviceEnumerator();
                    //here I see my sound card
                    var mm_dev = enumerator.EnumerateAudioEndPoints(DataFlow.Render, DeviceState.Active)[Index];


                    loopback_capture = new WasapiLoopbackCapture(mm_dev);
                    loopback_capture.DataAvailable += Loopback_capture_DataAvailable;

                    //loopback_capture.RecordingStopped += Loopback_capture_RecordingStopped;
                    DebugStream.WriteLine("Listening to output device " + mm_dev.DeviceFriendlyName);
                    DebugStream.Flush();
                }
                catch (Exception ex)
                {
                    DebugStream.WriteLine("Error: " + ex.ToString());
                    DebugStream.Flush();
                }
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            handleStartStop(!running);
   
            if (!running)
            {
                btnStart.Text = "Start";
            }
            else
            {
                btnStart.Text = "Stop";
            }
        }

        private void handleStartStop(bool run)
        {
            if (waveIn != null)
            {
                if (run)
                {
                    waveIn.StartRecording();
                }
                else
                {
                    waveIn.StopRecording();
                }
                running = run;
            }
            if (loopback_capture != null)
            {
                if (run)
                {
                    loopback_capture.StartRecording();
                }
                else
                {
                    loopback_capture.StopRecording();
                }
                running = run;
            }
            if (!running)
            {
                SetUVMeterValues(0, 0);
            }
        }

//        static int dCnt = 0;

        private void WaveIn_DataAvailable(object sender, NAudio.Wave.WaveInEventArgs e)
        {
            if (e.BytesRecorded == 0) return;

            int bytesPerSample = waveIn.WaveFormat.BitsPerSample / 8;
            int samplesRecorded = e.BytesRecorded / bytesPerSample;
            int leftValue = 0, rightValue = 0, l = 0, r = 0;
            MaxLeftValueInt = MaxRightValueInt = 0;

            //if (dCnt % 100 == 0) { DebugStream.WriteLine("In:" + e.BytesRecorded.ToString() + " byte"); ; DebugStream.Flush(); }
            //dCnt++;

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
                l = Math.Max(MaxLeftValueInt, Math.Abs(leftValue));
                r = Math.Max(MaxRightValueInt, Math.Abs(rightValue));
            }

            MaxLeftValueInt = l ;
            MaxRightValueInt = r ;

        }

        private void Loopback_capture_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (e.BytesRecorded == 0) return;

            int bytesPerSample = loopback_capture.WaveFormat.BitsPerSample / 8;
            int samplesRecorded = e.BytesRecorded / bytesPerSample;
            //MaxLeftValueFloat = MaxRightValueFloat = 0.0F;

            for (int indexSample = 0; indexSample < samplesRecorded; indexSample += 2)
            {
                if (bytesPerSample == 4)
                {
                    float value = BitConverter.ToSingle(e.Buffer, indexSample * bytesPerSample);
                    MaxLeftValueFloat = Math.Max(MaxLeftValueFloat, Math.Abs(value));
                    value = BitConverter.ToSingle(e.Buffer, (indexSample + 1) * bytesPerSample);
                    MaxRightValueFloat = Math.Max(MaxRightValueFloat, Math.Abs(value));
                }
            }

            MaxLeftValueInt = (int)(100F * (MaxLeftValueFloat)) ; // calcPercentage((int)(MaxLeftValueFloat * 32768F), 0);
            MaxRightValueInt = (int)(100F * (MaxRightValueFloat)) ; // calcPercentage((int)(MaxRightValueFloat * 32768F), 0);

            //DebugStream.WriteLine("In:" + e.BytesRecorded.ToString() + " byte. L=" + MaxLeftValueInt.ToString() + " R=" + MaxRightValueInt.ToString() ); DebugStream.Flush();
        }

        private int LastLeftValue = -1;
        private int LastRightValue = -1;
        //private void UpdateVU(object sender, ElapsedEventArgs e)

        private void UpdateVU()
        {
            for (; ; )
            {
                if (ResetDeviceNeeded)
                {
                    ResetDeviceNeeded = false;
                    resetDevice(SelectedDeviceIdx, SelectedInput == 1);
                    handleStartStop(running);
                }

                if (running)
                {
                    handleAutoOff();

                    double newValueL = (Smoothness * LastLeftValue) + (1.0 - Smoothness) * (MaxLeftValueInt * MaxLeftValueInt);
                    double newValueR = (Smoothness * LastRightValue) + (1.0 - Smoothness) * (MaxRightValueInt * MaxRightValueInt);
                    MaxLeftValueFloat = MaxRightValueFloat = 0.0F;

                    if (LastLeftValue != (int)(newValueL + 0.5) || LastRightValue != (int)(newValueR + 0.5))
                    {
                        LastLeftValue = (int)(newValueL + 0.5);
                        LastRightValue = (int)(newValueR + 0.5);
                        SetUVMeterValues(LastLeftValue, LastRightValue);
                    }
                }
                Thread.Sleep(100);
            }
        }

        private void handleAutoOff()
        {
            if (MaxLeftValueInt == 0 && MaxRightValueInt == 0)
            {
                if (!zeroFound)
                {
                    zeroFound = true;
                    firstZeroTime = DateTime.Now;
                }
                else if (!zeroTimeoutStarted)
                {
                    if (DateTime.Now - firstZeroTime > TimeSpan.FromSeconds(10))
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

        private void cbInputs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbInputs.SelectedIndex >= 0)
            {
                cbOutputs.SelectedIndex = -1;
                SelectedDeviceIdx = cbInputs.SelectedIndex;
                SelectedInput = 1;  // 0 = output, 1 = input
                ResetDeviceNeeded = true;
            }
        }


        private void cbOutputs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbOutputs.SelectedIndex >= 0)
            {
                cbInputs.SelectedIndex = -1;
                SelectedDeviceIdx = cbOutputs.SelectedIndex;
                SelectedInput = 0;  // 0 = output, 1 = input
                ResetDeviceNeeded = true;
            }
        }


        private void sbSensitivity_Scroll(object sender, ScrollEventArgs e)
        {
            Sensitivity = Math.Max(1, sbSensitivity.Value) / 50.0;
            txtSensitiviy.Text = (sbSensitivity.Value - 50).ToString();
        }

        private void sbSmoothness_Scroll(object sender, ScrollEventArgs e)
        {
            Smoothness = Math.Max(1, sbSmoothness.Value) / 100.0;
            txtSmoothness.Text = Smoothness.ToString();
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

        private void sbRed_Scroll(object sender, ScrollEventArgs e)
        {
            RedValue = sbRed.Value;
            SetColor();
        }

        private void sbGreen_Scroll(object sender, ScrollEventArgs e)
        {
            GreenValue = sbGreen.Value;
            SetColor();
        }

        private void sbBlue_Scroll(object sender, ScrollEventArgs e)
        {
            BlueValue = sbBlue.Value;
            SetColor();
        }

        private void SetColor()
        {
            SetColor(RedValue, GreenValue, BlueValue);
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


    }
}





