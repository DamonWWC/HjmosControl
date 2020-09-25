using System;
using System.Runtime.InteropServices;


namespace Hjmos.CommonControls
{
   public class WinApiHelper
    {
        public delegate int WindowEnumProc(IntPtr hwnd, IntPtr lparam);
       
        [DllImport("user32.dll")]
        public static extern bool EnumChildWindows(IntPtr hwnd, WindowEnumProc func, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hwnd, out uint processId);
        [DllImport("user32.dll", EntryPoint = "GetWindowLong")] // TODO: 32/64?
        public static extern IntPtr GetWindowLongPtr(IntPtr hWnd, Int32 nIndex);
        public const Int32 GWLP_USERDATA = -21;
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr PostMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetWindow(IntPtr hWnd, GetWindowCmd uCmd);

        [DllImport("user32.dll", EntryPoint = "CreateWindowEx", CharSet = CharSet.Unicode)]
        public static extern IntPtr CreateWindowEx(int dwExStyle,
            string lpszClassName,
            string lpszWindowName,
            int style,
            int x, int y,
            int width, int height,
            IntPtr hwndParent,
            IntPtr hMenu,
            IntPtr hInst,
            [MarshalAs(UnmanagedType.AsAny)] object pvParam);

        [DllImport("user32.dll", EntryPoint = "ShowWindow")]
        [return:MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindow([System.Runtime.InteropServices.InAttribute()] System.IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern long SetParent(IntPtr hWndChild, IntPtr hWndNewParent); //该api用于嵌入到窗口中运行

        [DllImport("User32.dll")]
       public static extern bool MoveWindow(IntPtr handle, int x, int y, int width, int height, bool redraw);

        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SetCursorPos(int x, int y);


        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetCursorPos(out POINT pt);

        [DllImport("user32.dll", EntryPoint = "DestroyWindow", CharSet = CharSet.Unicode)]
        public static extern bool DestroyWindow(IntPtr hwnd);

        [DllImport("user32.dll")]
       public static extern IntPtr WindowFromPoint(POINT Point);

        [DllImport("user32.dll")]
       public static extern IntPtr WindowFromPoint(int xPoint, int yPoint);


        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess(ProcessAccess desiredAccess,bool inheritHandle,int processId);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle([In] IntPtr handle);

        [DllImport("ntdll.dll")]
        public static extern uint NtResumeProcess([In] IntPtr processHandle);

        [DllImport("ntdll.dll")]
        public static extern uint NtSuspendProcess([In] IntPtr processHandle);

        /// <summary>
        /// 暂停进程
        /// </summary>
        /// <param name="processId"></param>
        public static void SuspendProcess(int processId)
        {
            IntPtr hProc = IntPtr.Zero;
            try
            {
                // Gets the handle to the Process
                hProc = OpenProcess(ProcessAccess.SuspendResume, false, processId);
                if (hProc != IntPtr.Zero)
                    NtSuspendProcess(hProc);
            }
            finally
            {
                // Don't forget to close handle you created.
                if (hProc != IntPtr.Zero)
                    CloseHandle(hProc);
            }
        }

        /// <summary>
        /// 恢复进程
        /// </summary>
        /// <param name="processId"></param>
        public static void ResumeProcess(int processId)
        {
            IntPtr hProc = IntPtr.Zero;
            try
            {
                // Gets the handle to the Process
                hProc = OpenProcess(ProcessAccess.SuspendResume, false, processId);
                if (hProc != IntPtr.Zero)
                    NtResumeProcess(hProc);
            }
            finally
            {
                // Don't forget to close handle you created.
                if (hProc != IntPtr.Zero)
                    CloseHandle(hProc);
            }
        }


        public struct POINT
        {
            public int X;
            public int Y;
            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
        }


        /// <summary>
        /// The process-specific access rights.
        /// </summary>
        [Flags]
        public enum ProcessAccess : uint
        {
            /// <summary>
            /// Required to terminate a process using TerminateProcess.
            /// </summary>
            Terminate = 0x1,

            /// <summary>
            /// Required to create a thread.
            /// </summary>
            CreateThread = 0x2,

            /// <summary>
            /// Undocumented.
            /// </summary>
            SetSessionId = 0x4,

            /// <summary>
            /// Required to perform an operation on the address space of a process (see VirtualProtectEx and WriteProcessMemory).
            /// </summary>
            VmOperation = 0x8,

            /// <summary>
            /// Required to read memory in a process using ReadProcessMemory.
            /// </summary>
            VmRead = 0x10,

            /// <summary>
            /// Required to write to memory in a process using WriteProcessMemory.
            /// </summary>
            VmWrite = 0x20,

            /// <summary>
            /// Required to duplicate a handle using DuplicateHandle.
            /// </summary>
            DupHandle = 0x40,

            /// <summary>
            /// Required to create a process.
            /// </summary>
            CreateProcess = 0x80,

            /// <summary>
            /// Required to set memory limits using SetProcessWorkingSetSize.
            /// </summary>
            SetQuota = 0x100,

            /// <summary>
            /// Required to set certain information about a process, such as its priority class (see SetPriorityClass).
            /// </summary>
            SetInformation = 0x200,

            /// <summary>
            /// Required to retrieve certain information about a process, such as its token, exit code, and priority class (see OpenProcessToken, GetExitCodeProcess, GetPriorityClass, and IsProcessInJob).
            /// </summary>
            QueryInformation = 0x400,

            /// <summary>
            /// Undocumented.
            /// </summary>
            SetPort = 0x800,

            /// <summary>
            /// Required to suspend or resume a process.
            /// </summary>
            SuspendResume = 0x800,

            /// <summary>
            /// Required to retrieve certain information about a process (see QueryFullProcessImageName). A handle that has the PROCESS_QUERY_INFORMATION access right is automatically granted PROCESS_QUERY_LIMITED_INFORMATION.
            /// </summary>
            QueryLimitedInformation = 0x1000,

            /// <summary>
            /// Required to wait for the process to terminate using the wait functions.
            /// </summary>
            Synchronize = 0x100000
        }

        public enum GetWindowCmd : uint
        {
            /// <summary>
            /// 返回的句柄标识了在Z序最高端的相同类型的窗口。
            /// 如果指定窗口是最高端窗口，则该句柄标识了在Z序最高端的最高端窗口；
            /// 如果指定窗口是顶层窗口，则该句柄标识了在z序最高端的顶层窗口：
            /// 如果指定窗口是子窗口，则句柄标识了在Z序最高端的同属窗口。
            /// </summary>
            GW_HWNDFIRST = 0,
            /// <summary>
            /// 返回的句柄标识了在z序最低端的相同类型的窗口。
            /// 如果指定窗口是最高端窗口，则该柄标识了在z序最低端的最高端窗口：
            /// 如果指定窗口是顶层窗口，则该句柄标识了在z序最低端的顶层窗口；
            /// 如果指定窗口是子窗口，则句柄标识了在Z序最低端的同属窗口。
            /// </summary>
            GW_HWNDLAST = 1,
            /// <summary>
            /// 返回的句柄标识了在Z序中指定窗口下的相同类型的窗口。
            /// 如果指定窗口是最高端窗口，则该句柄标识了在指定窗口下的最高端窗口：
            /// 如果指定窗口是顶层窗口，则该句柄标识了在指定窗口下的顶层窗口；
            /// 如果指定窗口是子窗口，则句柄标识了在指定窗口下的同属窗口。
            /// </summary>
            GW_HWNDNEXT = 2,
            /// <summary>
            /// 返回的句柄标识了在Z序中指定窗口上的相同类型的窗口。
            /// 如果指定窗口是最高端窗口，则该句柄标识了在指定窗口上的最高端窗口；
            /// 如果指定窗口是顶层窗口，则该句柄标识了在指定窗口上的顶层窗口；
            /// 如果指定窗口是子窗口，则句柄标识了在指定窗口上的同属窗口。
            /// </summary>
            GW_HWNDPREV = 3,
            /// <summary>
            /// 返回的句柄标识了指定窗口的所有者窗口（如果存在）。
            /// GW_OWNER与GW_CHILD不是相对的参数，没有父窗口的含义，如果想得到父窗口请使用GetParent()。
            /// 例如：例如有时对话框的控件的GW_OWNER，是不存在的。
            /// </summary>
            GW_OWNER = 4,
            /// <summary>
            /// 如果指定窗口是父窗口，则获得的是在Tab序顶端的子窗口的句柄，否则为NULL。
            /// 函数仅检查指定父窗口的子窗口，不检查继承窗口。
            /// </summary>
            GW_CHILD = 5,
            /// <summary>
            /// （WindowsNT 5.0）返回的句柄标识了属于指定窗口的处于使能状态弹出式窗口（检索使用第一个由GW_HWNDNEXT 查找到的满足前述条件的窗口）；
            /// 如果无使能窗口，则获得的句柄与指定窗口相同。
            /// </summary>
            GW_ENABLEDPOPUP = 6
        }
    }
}
