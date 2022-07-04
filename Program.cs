using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlockWindowKey
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
       // [STAThread]
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private static HookProc proc = HookCallback;

        private static IntPtr HookCallback(int nCode, IntPtr wParam,IntPtr lParam)
        {
            if ((nCode >= 0) && (wParam == (IntPtr)WM_KEYDOWN))
            {
                int vkCode = Marshal.ReadInt32(lParam);
                if (((Keys)vkCode == Keys.LWin) || ((Keys)
                vkCode == Keys.RWin))
                {
                    Console.WriteLine("{0} blocked!", (Keys)
                    vkCode);
                    return (IntPtr)1;
                }
                return CallNextHookEx(hook, nCode, wParam,lParam);
            }
        }

        private static IntPtr hook = IntPtr.Zero;
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            hook = SetHook(proc);
            Application.Run();
            UnhookWindowsHookEx(hook);
        }
        private delegate IntPtr HookProc(int nCode,)
        private static IntPtr SetHook(HookProc proc)
        {
            using (Process curProcess = Process.
            GetCurrentProcess())
            using (ProcessModule curModule = curProcess.
            MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL,proc,GetModuleHandle(curModule.ModuleName),0);
            }
        }

        private static IntPtr SetWindowsHookEx(int wH_KEYBOARD_LL, HookProc proc, object p, int v)
        {
            throw new NotImplementedException();
        }
    }
}
