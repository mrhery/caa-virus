using System.Diagnostics;
using System.Runtime.InteropServices;

namespace KeyboardJammer
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc callback, IntPtr hInstance, uint threadId);

        [DllImport("user32.dll")]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential)]
        private struct KBDLLHOOKSTRUCT
        {
            public Keys key;
            public int scanCode;
            public int flags;
            public int time;
            public IntPtr dwExtraInfo;
        }

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;

        private LowLevelKeyboardProc keyboardProc;
        private IntPtr keyboardHook = IntPtr.Zero;

        public Form1()
        {
            InitializeComponent();

            keyboardProc = HookCallback;
            keyboardHook = SetHook(keyboardProc);
        }

        IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (ProcessModule curModule = Process.GetCurrentProcess().MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc, GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                KBDLLHOOKSTRUCT kbStruct = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));

                if (wParam == (IntPtr)WM_KEYDOWN)
                {
                    // Ignore the key down event
                    return (IntPtr)1;
                }
                else if (wParam == (IntPtr)WM_KEYUP)
                {
                    // Ignore the key up event
                    return (IntPtr)1;
                }
            }

            return CallNextHookEx(keyboardHook, nCode, wParam, lParam);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnhookWindowsHookEx(keyboardHook);
        }
    }
}
