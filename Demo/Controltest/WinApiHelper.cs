using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    /// <summary>
    /// Win32Api帮助类
    /// </summary>
    public class WinApiHelper
    {
        /// <summary>
        /// 窗体消息的回调
        /// </summary>
        /// <param name="hwnd">窗体的句柄</param>
        /// <param name="lparam">消息句柄</param>
        /// <returns></returns>
        public delegate int WindowEnumProc(IntPtr hwnd, IntPtr lparam);

        /// <summary>
        /// 函数通过将每个子窗口的句柄传递给应用程序定义的回调函数来枚举属于指定父窗口的子窗口
        /// </summary>
        /// <param name="hwnd">标识要窗口子窗口的父窗口。</param>
        /// <param name="func">指向应用程序定义的回调函数。</param>
        /// <param name="lParam">指定要传递给回调函数的32位应用程序定义值。</param>
        /// <returns>如果函数成功，返回值不为零。如果函数失败，返回值为零。</returns>
        [DllImport("user32.dll")]
        public static extern bool EnumChildWindows(IntPtr hwnd, WindowEnumProc func, IntPtr lParam);

        /// <summary>
        /// 检索创建指定窗口的线程的标识符，以及可选地，创建窗口的进程的标识符
        /// </summary>
        /// <param name="hwnd">标识窗口。</param>
        /// <param name="processId">指向接收进程标识符的32位值。</param>
        /// <returns>返回值是创建窗口的线程的标识符。</returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hwnd, out uint processId);

        /// <summary>
        /// 检索有关指定窗口的信息。
        /// </summary>
        /// <param name="hWnd">标识窗口，间接地标识窗口所属的类。</param>
        /// <param name="nIndex">指定要检索的值的从零开始的偏移量。</param>
        /// <returns>如果函数成功，则返回值是所请求的32位值。如果函数失败，返回值为零。</returns>
        [DllImport("user32.dll", EntryPoint = "GetWindowLong")] // TODO: 32/64?
        public static extern IntPtr GetWindowLong(IntPtr hWnd, Int32 nIndex);

        /// <summary>
        /// GWLP_USERDATA参数
        /// </summary>
        public const Int32 GWLP_USERDATA = -21;

        /// <summary>
        /// 将指定的消息发送到窗口或窗口
        /// </summary>
        /// <param name="hWnd">目标窗口的句柄</param>
        /// <param name="Msg">要发送的消息</param>
        /// <param name="wParam">指定附加的消息特定信息。</param>
        /// <param name="lParam">指定附加的消息特定信息。</param>
        /// <returns>返回值指定消息处理的结果，并取决于发送的消息。</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// 消息放置在与创建指定窗口的线程相关联的消息队列中，然后返回，而不等待线程处理消息
        /// </summary>
        /// <param name="hWnd">目标窗口的句柄</param>
        /// <param name="Msg">指定要发布的消息。</param>
        /// <param name="wParam">指定附加的消息特定信息。</param>
        /// <param name="lParam">指定附加的消息特定信息。</param>
        /// <returns>如果函数成功，返回值不为零。如果函数失败，返回值为零。</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr PostMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        /// <summary>
        /// 具有指定关系的窗口的句柄到指定的窗口
        /// </summary>
        /// <param name="hWnd">原始窗口的句柄</param>
        /// <param name="uCmd">关系标志</param>
        /// <returns>如果函数成功，则返回值是窗口句柄。如果与指定窗口的指定关系不存在窗口，返回值为NULL。</returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetWindow(IntPtr hWnd, GetWindowCmd uCmd);

        /// <summary>
        /// 创建一个具有扩展样式的重叠，弹出窗口或子窗口;否则，此函数与CreateWindow功能相同。
        /// </summary>
        /// <param name="dwExStyle">扩展窗口样式</param>
        /// <param name="lpszClassName">指向注册类名的指针</param>
        /// <param name="lpszWindowName">指向窗口名称的指针</param>
        /// <param name="style">窗口样式</param>
        /// <param name="x">窗口的水平位置</param>
        /// <param name="y">窗口的垂直位置</param>
        /// <param name="width">窗口宽度</param>
        /// <param name="height">窗口高度</param>
        /// <param name="hwndParent">处理父或所有者窗口</param>
        /// <param name="hMenu">处理菜单或子窗口标识符</param>
        /// <param name="hInst">处理应用程序实例</param>
        /// <param name="pvParam">指向窗口创建数据的指针</param>
        /// <returns></returns>
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

        /// <summary>
        /// 设置指定窗口的显示状态。
        /// </summary>
        /// <param name="hWnd">窗口的句柄</param>
        /// <param name="nCmdShow">显示窗口状态</param>
        /// <returns>如果窗口以前可见，则返回值不为零。如果窗口以前被隐藏，返回值为零。</returns>
        [DllImport("user32.dll", EntryPoint = "ShowWindow")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindow([System.Runtime.InteropServices.InAttribute()] System.IntPtr hWnd, int nCmdShow);

        /// <summary>
        /// 更改指定子窗口的父窗口。
        /// </summary>
        /// <param name="hWndChild">父窗口正在改变的窗口的句柄</param>
        /// <param name="hWndNewParent">处理新的父窗口</param>
        /// <returns>如果函数成功，则返回值是上一个父窗口的句柄。如果函数失败，返回值为NULL。</returns>
        [DllImport("user32.dll", SetLastError = true)]
        public static extern long SetParent(IntPtr hWndChild, IntPtr hWndNewParent); //该api用于嵌入到窗口中运行

        /// <summary>
        /// 可以更改指定窗口的位置和尺寸。对于顶级窗口，位置和尺寸相对于屏幕的左上角。对于子窗口，它们相对于父窗口的客户区域的左上角。
        /// </summary>
        /// <param name="handle">窗口的句柄</param>
        /// <param name="x">水平位置</param>
        /// <param name="y">垂直位置</param>
        /// <param name="width">指定窗口的新宽度。</param>
        /// <param name="height">指定窗口的新高度。</param>
        /// <param name="redraw">指定窗口是否要重新绘制。</param>
        /// <returns>如果函数成功，返回值不为零。如果函数失败，返回值为零。</returns>
        [DllImport("User32.dll")]
        public static extern bool MoveWindow(IntPtr handle, int x, int y, int width, int height, bool redraw);

        [DllImport("user32.dll")]
        public static extern bool SetWindowRgn(IntPtr handle, IntPtr hRgn, bool redraw);

        /// <summary>
        /// 创建指定窗口的线程放入前台并激活该窗口。键盘输入指向窗口，并为用户更改各种视觉提示。
        /// </summary>
        /// <param name="hWnd">标识应该被激活并被带到前台的窗口。</param>
        /// <returns>如果函数成功，返回值不为零。如果函数失败，返回值为零。</returns>
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// 检索顶级窗口的句柄，其类名和窗口名称与指定的字符串相匹配。此函数不搜索子窗口。
        /// </summary>
        /// <param name="lpClassName">指向类名的指针</param>
        /// <param name="lpWindowName">指向窗口名称的指针</param>
        /// <returns>如果函数成功，则返回值是具有指定类名和窗口名称的窗口的句柄。如果函数失败，返回值为NULL。</returns>
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// 可以合成鼠标移动和按钮点击。
        /// </summary>
        /// <param name="dwFlags">指定各种运动/点击变体的标志</param>
        /// <param name="dx">水平鼠标位置或位置更改</param>
        /// <param name="dy">垂直鼠标位置或位置更改</param>
        /// <param name="cButtons">车轮运动量</param>
        /// <param name="dwExtraInfo">32位应用程序定义的信息</param>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        /// <summary>
        /// 将光标移动到指定的屏幕坐标。如果新坐标不在最新的ClipCursor功能设置的屏幕矩形内，Windows会自动调整坐标，使光标停留在矩形内。
        /// </summary>
        /// <param name="x">水平位置</param>
        /// <param name="y">垂直位置</param>
        /// <returns>如果函数成功，返回值不为零。如果函数失败，返回值为零。</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int SetCursorPos(int x, int y);

        /// <summary>
        /// 以屏幕坐标取得光标的位置。
        /// </summary>
        /// <param name="pt">光标位置结构的地址</param>
        /// <returns>如果函数成功，返回值不为零。如果函数失败，返回值为零。</returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetCursorPos(out POINT pt);

        /// <summary>
        /// 会破坏指定的窗口。该函数将WM_DESTROY和WM_NCDESTROY消息发送到窗口以停用它并从中删除键盘焦点。该函数还会破坏窗口的菜单，刷新线程消息队列，破坏定时器，删除剪贴板所有权，并打破剪贴板查看器链（如果窗口位于查看器链的顶部）。如果指定的窗口是父窗口或所有者窗口，则DestroyWindow会在销毁父窗口或所有者窗口时自动销毁相关联的子窗口或拥有的窗口。该函数首先销毁子窗口或拥有的窗口，然后破坏父窗口或所有者窗口。
        /// </summary>
        /// <param name="hwnd">处理窗口以销毁</param>
        /// <returns>如果函数成功，返回值不为零.如果函数失败，返回值为零。</returns>
        [DllImport("user32.dll", EntryPoint = "DestroyWindow", CharSet = CharSet.Unicode)]
        public static extern bool DestroyWindow(IntPtr hwnd);

        /// <summary>
        /// 检索包含指定点的窗口的句柄。
        /// </summary>
        /// <param name="Point">指定一个定义要检查点的POINT结构。</param>
        /// <returns>如果函数成功，则返回值是包含该点的窗口的句柄。如果给定点没有窗口，则返回值为NULL。</returns>
        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(POINT Point);

        /// <summary>
        /// 检索包含指定点的窗口的句柄。
        /// </summary>
        /// <param name="xPoint">x轴对应的点</param>
        /// <param name="yPoint">y轴对应的点</param>
        /// <returns>如果函数成功，则返回值是包含该点的窗口的句柄。如果给定点没有窗口，则返回值为NULL。</returns>
        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(int xPoint, int yPoint);

        /// <summary>
        /// 返回现有进程对象的句柄。
        /// </summary>
        /// <param name="desiredAccess">访问标志</param>
        /// <param name="inheritHandle">处理继承标志</param>
        /// <param name="processId">进程标识符</param>
        /// <returns>如果函数成功，则返回值是指定进程的打开句柄。如果函数失败，返回值为NULL。</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess(ProcessAccess desiredAccess, bool inheritHandle, int processId);

        /// <summary>
        /// 关闭一个打开的对象句柄。
        /// </summary>
        /// <param name="handle">标识一个打开的对象句柄。</param>
        /// <returns>如果函数成功，返回值不为零。如果函数失败，返回值为零。</returns>
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool CloseHandle([In] IntPtr handle);

        /// <summary>
        /// 恢复进程
        /// </summary>
        /// <param name="processHandle">对应的进程句柄</param>
        /// <returns>如果函数成功，返回值不为零。如果函数失败，返回值为零</returns>
        [DllImport("ntdll.dll")]
        public static extern uint NtResumeProcess([In] IntPtr processHandle);

        /// <summary>
        /// 挂起进程
        /// </summary>
        /// <param name="processHandle">对应的进程句柄</param>
        /// <returns>如果函数成功，返回值不为零。如果函数失败，返回值为零</returns>
        [DllImport("ntdll.dll")]
        public static extern uint NtSuspendProcess([In] IntPtr processHandle);

        /// <summary>
        /// 暂停进程
        /// </summary>
        /// <param name="processId">进程Id</param>
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
        /// <param name="processId">进程Id</param>
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

        /// <summary>
        /// 定义点的结构体
        /// </summary>
        public struct POINT
        {
            /// <summary>
            /// X坐标
            /// </summary>
            public int X;

            /// <summary>
            /// Y坐标
            /// </summary>
            public int Y;

            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="x">X坐标</param>
            /// <param name="y">Y坐标</param>
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