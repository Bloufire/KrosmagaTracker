using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using Rectangle = System.Windows.Shapes.Rectangle;
namespace AddOn_Krosmaga___Blou_fire.Utility
{
	public class User32
	{
		[Flags]
		public enum MouseEventFlags : uint
		{
			LeftDown = 0x00000002,
			LeftUp = 0x00000004,
			RightDown = 0x00000008,
			RightUp = 0x00000010,
			Wheel = 0x00000800
		}
		public static double DpiScalingX = 1.0, DpiScalingY = 1.0;

		public const int WsExTransparent = 0x00000020;
		public const int WsExToolWindow = 0x00000080;
		public const int WsExTopmost = 0x00000008;
		private const int GwlExstyle = (-20);
		private const int GwlStyle = -16;
		private const int WsMinimize = 0x20000000;
		private const int WsMaximize = 0x1000000;
		public const int SwRestore = 9;
		public const int SwShow = 5;
		private const int Alt = 0xA4;
		private const int ExtendedKey = 0x1;
		private const int KeyUp = 0x2;
		private static DateTime _lastCheck;
		private static IntPtr _krWindow;

		private static readonly string[] WindowNames = { "Krosmaga" };

		[DllImport("user32.dll")]
		public static extern IntPtr GetClientRect(IntPtr hWnd, ref Rect rect);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool GetWindowRect(IntPtr hwnd, out Rect lpRect);

		[DllImport("user32.dll")]
		public static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll")]
		private static extern int GetWindowLong(IntPtr hwnd, int index);

		[DllImport("user32.dll")]
		private static extern int SetWindowLong(IntPtr hwnd, int index, int newStyle);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		[DllImport("user32.dll")]
		public static extern bool SetForegroundWindow(IntPtr hWnd);

		[DllImport("user32.dll")]
		public static extern void mouse_event(uint dwFlags, uint dx, uint dy, int dwData, UIntPtr dwExtraInfo);

		[DllImport("user32.dll")]
		public static extern bool ClientToScreen(IntPtr hWnd, ref Point lpPoint);

		[DllImport("user32.dll")]
		private static extern bool FlashWindow(IntPtr hwnd, bool bInvert);

		[DllImport("user32.dll")]
		public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

		[DllImport("user32.dll")]
		public static extern bool IsWindow(IntPtr hWnd);

		[DllImport("user32.dll", CharSet = CharSet.Unicode)]
		private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		[DllImport("user32.dll")]
		private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

		[DllImport("user32.dll")]
		public static extern bool PrintWindow(IntPtr hwnd, IntPtr hdcBlt, uint nFlags);

		[DllImport("user32.dll")]
		public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

		public static void SetWindowExStyle(IntPtr hwnd, int style) => SetWindowLong(hwnd, GwlExstyle, GetWindowLong(hwnd, GwlExstyle) | style);

		public static bool IsKrosmagaInForeground() => GetForegroundWindow() == GetKrosmagaWindow();

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool GetCursorPos(out MousePoint lpPoint);

		public static Point GetMousePos() => GetCursorPos(out var p) ? new Point(p.X, p.Y) : new Point();

		public static WindowState GetKrosmagaWindowState()
		{
			var krosmagaWindow = GetKrosmagaWindow();
			var state = GetWindowLong(krosmagaWindow, GwlStyle);
			if ((state & WsMaximize) == WsMaximize)
				return WindowState.Maximized;
			if ((state & WsMinimize) == WsMinimize)
				return WindowState.Minimized;
			return WindowState.Normal;
		}

		public static bool IsTopmost(IntPtr hwnd) => (GetWindowLong(hwnd, GwlExstyle) & WsExTopmost) != 0;

		public static IntPtr GetKrosmagaWindow()
		{
			if (DateTime.Now - _lastCheck < new TimeSpan(0, 0, 5) && _krWindow == IntPtr.Zero)
				return _krWindow;
			if (_krWindow != IntPtr.Zero && IsWindow(_krWindow))
				return _krWindow;
			_krWindow = FindWindow("UnityWndClass", "Krosmaga");
			if (_krWindow != IntPtr.Zero)
				return _krWindow;
			foreach (var windowName in WindowNames)
			{
				_krWindow = FindWindow("UnityWndClass", windowName);
				if (_krWindow == IntPtr.Zero)
					continue;
				
				break;
			}
			_lastCheck = DateTime.Now;
			return _krWindow;
		}

		public static Process GetKrosmagaProc()
		{
			if (_krWindow == IntPtr.Zero)
				return null;
			try
			{
				GetWindowThreadProcessId(_krWindow, out uint procId);
				return Process.GetProcessById((int)procId);
			}
			catch
			{
				return null;
			}
		}

		public static List<double> GetKrosmagaRect(bool dpiScaling)
		{
			// Returns the co-ordinates of Krosmaga's client area in screen co-ordinates
			var hsHandle = GetKrosmagaWindow();
			var rect = new Rect();
			var ptUL = new Point();
			var ptLR = new Point();

			GetClientRect(hsHandle, ref rect);

			ptUL.X = rect.left;
			ptUL.Y = rect.top;

			ptLR.X = rect.right;
			ptLR.Y = rect.bottom;

			ClientToScreen(hsHandle, ref ptUL);
			ClientToScreen(hsHandle, ref ptLR);

			if (dpiScaling)
			{
				ptUL.X = (int)(ptUL.X / DpiScalingX);
				ptUL.Y = (int)(ptUL.Y / DpiScalingY);
				ptLR.X = (int)(ptLR.X / DpiScalingX);
				ptLR.Y = (int)(ptLR.Y / DpiScalingY);
			}

			return new List<double>(){ptUL.X, ptUL.Y, ptLR.X - ptUL.X, ptLR.Y - ptUL.Y};

		}

		public static void BringKrosmagaToForeground()
		{
			var krosmagaHandle = GetKrosmagaWindow();
			if (krosmagaHandle == IntPtr.Zero)
				return;
			ActivateWindow(krosmagaHandle);
			SetForegroundWindow(krosmagaHandle);
		}

		public static void FlashHs() => FlashWindow(GetKrosmagaWindow(), false);

		//http://www.roelvanlisdonk.nl/?p=4032
		public static void ActivateWindow(IntPtr mainWindowHandle)
		{
			// Guard: check if window already has focus.
			if (mainWindowHandle == GetForegroundWindow())
				return;

			// Show window maximized.
			ShowWindow(mainWindowHandle, GetKrosmagaWindowState() == WindowState.Minimized ? SwRestore : SwShow);

			// Simulate an "ALT" key press.
			keybd_event(Alt, 0x45, ExtendedKey | 0, 0);

			// Simulate an "ALT" key release.
			keybd_event(Alt, 0x45, ExtendedKey | KeyUp, 0);

			// Show window in forground.
			SetForegroundWindow(mainWindowHandle);
		}


		//http://joelabrahamsson.com/detecting-mouse-and-keyboard-input-with-net/
		public class MouseInput : IDisposable
		{
			private const int WH_MOUSE_LL = 14;
			private const int WM_LBUTTONDOWN = 0x201;
			private const int WM_LBUTTONUP = 0x0202;
			private readonly WindowsHookHelper.HookDelegate _mouseDelegate;
			private readonly IntPtr _mouseHandle;
			private bool _disposed;

			public MouseInput()
			{
				_mouseDelegate = MouseHookDelegate; //crashes application if directly used for some reason
				_mouseHandle = WindowsHookHelper.SetWindowsHookEx(WH_MOUSE_LL, _mouseDelegate, IntPtr.Zero, 0);
			}

			public void Dispose() => Dispose(true);

			public event EventHandler<EventArgs> LmbDown;
			public event EventHandler<EventArgs> LmbUp;
			public event EventHandler<EventArgs> MouseMoved;

			private IntPtr MouseHookDelegate(int code, IntPtr wParam, IntPtr lParam)
			{
				if (code < 0)
					return WindowsHookHelper.CallNextHookEx(_mouseHandle, code, wParam, lParam);


				switch (wParam.ToInt32())
				{
					case WM_LBUTTONDOWN:
						LmbDown?.Invoke(this, new EventArgs());
						break;
					case WM_LBUTTONUP:
						LmbUp?.Invoke(this, new EventArgs());
						break;
					default:
						MouseMoved?.Invoke(this, new EventArgs());
						break;
				}

				return WindowsHookHelper.CallNextHookEx(_mouseHandle, code, wParam, lParam);
			}

			protected virtual void Dispose(bool disposing)
			{
				if (_disposed)
					return;
				if (_mouseHandle != IntPtr.Zero)
					WindowsHookHelper.UnhookWindowsHookEx(_mouseHandle);
				_disposed = true;
			}

			~MouseInput()
			{
				Dispose(false);
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		private struct MousePoint
		{
			public readonly int X;
			public readonly int Y;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct Rect
		{
			public int left;
			public int top;
			public int right;
			public int bottom;
		}

		public class WindowsHookHelper
		{
			public delegate IntPtr HookDelegate(int code, IntPtr wParam, IntPtr lParam);

			[DllImport("User32.dll")]
			public static extern IntPtr CallNextHookEx(IntPtr hHook, int nCode, IntPtr wParam, IntPtr lParam);

			[DllImport("User32.dll")]
			public static extern IntPtr UnhookWindowsHookEx(IntPtr hHook);

			[DllImport("User32.dll")]
			public static extern IntPtr SetWindowsHookEx(int idHook, HookDelegate lpfn, IntPtr hmod, int dwThreadId);
		}
	}
}
