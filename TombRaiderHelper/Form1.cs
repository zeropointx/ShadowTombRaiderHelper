using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TombRaiderHelper
{
    public partial class Form1 : Form
    {
        [DllImport("kernel32.dll")]
        static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, uint nSize, out int lpNumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        public static extern Int32 CloseHandle(IntPtr hProcess);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool ReadProcessMemory(IntPtr process, IntPtr baseAddress, [Out] byte[] buffer, int size,
           out IntPtr bytesRead);

        [Flags]
        public enum ProcessAccessFlags : uint
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VMOperation = 0x00000008,
            VMRead = 0x00000010,
            VMWrite = 0x00000020,
            DupHandle = 0x00000040,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            Synchronize = 0x00100000
        }

        public static void WriteMem(Process p, IntPtr address, float v)
        {
            var hProc = OpenProcess(ProcessAccessFlags.All, false, (int)p.Id);
            var val = BitConverter.GetBytes(v);
            int wtf = 0;
            WriteProcessMemory(hProc, address, val, (UInt32)val.LongLength, out wtf);

            CloseHandle(hProc);
        }

        public static float ReadFloat(Process p, IntPtr address)
        {
            var buffer = new byte[4];
            IntPtr bytesRead;
            var hProc = OpenProcess(ProcessAccessFlags.All, false, (int)p.Id);
            ReadProcessMemory(hProc, address, buffer, 4, out bytesRead);
            return BitConverter.ToSingle(buffer,0);
        }

        public static ulong ReadUnsignedInt64(Process p, IntPtr address)
        {
            var buffer = new byte[8];
            IntPtr bytesRead;
            var hProc = OpenProcess(ProcessAccessFlags.All, false, (int)p.Id);
            ReadProcessMemory(hProc, address, buffer,8, out bytesRead);
            return BitConverter.ToUInt64(buffer, 0);
        }

        void addOrReplaceHotkey(string keyName, int keyCode)
        {
            if (!hotKeys.ContainsKey(keyName))
                hotKeys.Add(keyName, keyCode);
            else
                hotKeys[keyName] = keyCode;
        }

        int getHotkey(string keyName)
        {
            if (hotKeys.ContainsKey(keyName))
                return hotKeys[keyName];
            else
                return -1;
        }

        Dictionary<string, int> hotKeys = new Dictionary<string, int>();
        KeyboardHook hook = new KeyboardHook();
        public Form1()
        {
            addOrReplaceHotkey("saveKey", Properties.Settings.Default.SaveKey);
            addOrReplaceHotkey("loadKey", Properties.Settings.Default.LoadKey);
            addOrReplaceHotkey("noclipKey", Properties.Settings.Default.NoclipKey);


            InitializeComponent();
            KeyboardHook.Initialize();

            KeyboardHook.KeyPressed += hook_KeyPressed;
            var values = Enum.GetValues(typeof(Keys));
            for (int i = 0; i < values.Length; i++)
            {
                Keys key = (Keys)values.GetValue(i);
                if (key == Keys.Modifiers)
                    continue;
                savePositionComboBox.Items.Add(key.ToString());
                loadPositionComboBox.Items.Add(key.ToString());
                noclipComboBox.Items.Add(key.ToString());
                if (key == (Keys)getHotkey("saveKey"))
                    savePositionComboBox.SelectedIndex = savePositionComboBox.Items.Count-1;
                if (key == (Keys)getHotkey("loadKey"))
                    loadPositionComboBox.SelectedIndex = loadPositionComboBox.Items.Count-1;
                if (key == (Keys)getHotkey("noclipKey"))
                    noclipComboBox.SelectedIndex = noclipComboBox.Items.Count-1;
            }
        }

        void hook_KeyPressed(object o, int vkCode)
        {
            // show the keys pressed in a label.
            //label1.Text = e.Modifier.ToString() + " + " + e.Key.ToString();
            Keys test = (Keys)vkCode;
            if (test == (Keys)getHotkey("saveKey"))
                saveLocation();
            if (test == (Keys)getHotkey("loadKey"))
                loadLocation();
            if (test == (Keys)getHotkey("noclipKey"))
                noclip();
            if(noclipping)
            {
                if(test == Keys.W)
                {
                    x += noclipSpeed;
                }
                if (test == Keys.S)
                {
                    x -= noclipSpeed;
                }
                if (test == Keys.D)
                {
                    z += noclipSpeed;
                }
                if (test == Keys.A)
                {
                    z -= noclipSpeed;
                }
                if (test == Keys.Space)
                {
                    y += noclipSpeed;
                }
                if (test == Keys.LControlKey)
                {
                    y -= noclipSpeed;
                }
            }
        }

        static float x = 0;
        static float y = 0;
        static float z = 0;
        static float noclipSpeed = 15;
        bool noclipping = false;

        Process getTombRaiderProcess()
        {
            var p = Process.GetProcessesByName("SOTTR").FirstOrDefault();
            return p;
        }

        IntPtr getXAddr()
        {
            Process proc = getTombRaiderProcess();
            if (proc == null)
                return new IntPtr();
            IntPtr startOffset = proc.MainModule.BaseAddress;
            long baseAddress = startOffset.ToInt64();
            long randomOffset = 0x3522EC8;
            IntPtr firstBaseAddr = new IntPtr(baseAddress + randomOffset);
            ulong firstBasePointer = ReadUnsignedInt64(proc, firstBaseAddr);
            ulong randomOffset2 = 0x8;
            IntPtr secondBaseAddr = new IntPtr((long)(firstBasePointer + randomOffset2));
            ulong secondBasePointer = ReadUnsignedInt64(proc, secondBaseAddr);
            return new IntPtr((long)(secondBasePointer + 0x14));
        }

        IntPtr getYAddr()
        {
            Process proc = getTombRaiderProcess();
            if (proc == null)
                return new IntPtr();
            IntPtr startOffset = proc.MainModule.BaseAddress;
            long baseAddress = startOffset.ToInt64();
            long randomOffset = 0x1E6C298;
            IntPtr firstBaseAddr = new IntPtr(baseAddress + randomOffset);
            ulong firstBasePointer = ReadUnsignedInt64(proc, firstBaseAddr);
            ulong randomOffset2 = 0x8;
            IntPtr secondBaseAddr = new IntPtr((long)(firstBasePointer + randomOffset2));
            ulong secondBasePointer = ReadUnsignedInt64(proc, secondBaseAddr);
            return new IntPtr((long)(secondBasePointer + 0x18));
        }

        IntPtr getZAddr()
        {
            Process proc = getTombRaiderProcess();
            if (proc == null)
                return new IntPtr();
            IntPtr startOffset = proc.MainModule.BaseAddress;
            long baseAddress = startOffset.ToInt64();
            long randomOffset = 0x1E6C298;
            IntPtr firstBaseAddr = new IntPtr(baseAddress + randomOffset);
            ulong firstBasePointer = ReadUnsignedInt64(proc, firstBaseAddr);
            ulong randomOffset2 = 0x8;
            IntPtr secondBaseAddr = new IntPtr((long)(firstBasePointer + randomOffset2));
            ulong secondBasePointer = ReadUnsignedInt64(proc, secondBaseAddr);
            return new IntPtr((long)(secondBasePointer + 0x10));
        }

        void saveLocation()
        {
            var p = getTombRaiderProcess();
            if (p == null)
                return;
            var xPtr = getXAddr();
            var yPtr = getYAddr();
            var zPtr = getZAddr();
            x = ReadFloat(p, xPtr);
            y = ReadFloat(p, yPtr);
            z = ReadFloat(p, zPtr);
        }

        void loadLocation()
        {
            var p = getTombRaiderProcess();
            if (p == null)
                return;
            var xPtr = getXAddr();
            var yPtr = getYAddr();
            var zPtr = getZAddr();
            if (x == 0 && y == 0)
                loadLocation();
            WriteMem(p, xPtr, x);
            WriteMem(p, yPtr, y);
            WriteMem(p, zPtr, z);
        }

        void noclip()
        {
            noclipping = !noclipping;
            if(noclipping)
            {
                saveLocation();
                new Thread(() =>
                {
                    Thread.CurrentThread.IsBackground = true;
                    var p = getTombRaiderProcess();
                    var xPtr = getXAddr();
                    var yPtr = getYAddr();
                    var zPtr = getZAddr();
                    while (noclipping && p != null && !p.HasExited)
                    {
                        WriteMem(p, xPtr, x);
                        WriteMem(p, yPtr, y);
                        WriteMem(p, zPtr, z);
                        Thread.Sleep(10);
                    }
                }).Start();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Keys saveKey = (Keys)Enum.Parse(typeof(Keys),(savePositionComboBox.GetItemText(savePositionComboBox.SelectedItem)));
            Keys loadKey = (Keys)Enum.Parse(typeof(Keys), (loadPositionComboBox.GetItemText(loadPositionComboBox.SelectedItem)));
            Keys noclipKey = (Keys)Enum.Parse(typeof(Keys), (noclipComboBox.GetItemText(noclipComboBox.SelectedItem)));

            addOrReplaceHotkey("saveKey", (int)saveKey);
            addOrReplaceHotkey("loadKey", (int)loadKey);
            addOrReplaceHotkey("noclipKey", (int)noclipKey);

            Properties.Settings.Default.SaveKey = (int)saveKey;
            Properties.Settings.Default.LoadKey = (int)loadKey;
            Properties.Settings.Default.NoclipKey = (int)noclipKey;
            Properties.Settings.Default.Save();
            KeyboardHook.UnInitialize();
            KeyboardHook.Initialize();

        }

        private void savePositionButton_Click(object sender, EventArgs e)
        {

        }
    }
}
