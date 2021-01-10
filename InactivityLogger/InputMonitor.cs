using System;
using System.Runtime.InteropServices;

namespace InactivityLogger
{
    // Monitors input.
    public class InputMonitor : IDisposable
    {
        public event EventHandler<EventType> InputChanged;

        enum HookID
        {
           // Low level mouse hook.
            LowLevelMouse = 14,

            // Low level keyboard hook.
            LowLevelKeyboard = 13
        }

        // Window messages we care to listen for.
        enum WindowMessage
        {
            // Message for pressing the left mouse button down.
            MouseLeftButtonDown = 0x0201,

            // Message for pressing the right mouse button down.
            MouseRightButtonDown = 0x0204,

            // Message for when the cursor moves.
            MouseMove = 0x0200,

            // Message for when the mouse wheel is rotated.
            MouseWheel = 0x020A,

            // Message for when the mouse's horizontal scroll wheel is tilted or rotated.
            MouseHorizontalWheel = 0x020E,

            // Message for when a nonsystem key is pressed.
            NonsystemKeyDown = 0x0100,

            // Message for when a system key is pressed.
            SystemKeyDown = 0x0104
        }

        // The code for when wParam and lParam parameters contain information about a mouse or keyboard message.
        protected const int hookCodeAction = 0;

        // Delegate for HOOKPROC.
        protected delegate IntPtr HookProc(int nCode, UIntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        protected static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hmod, uint threadID);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        protected static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, UIntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        protected static extern bool UnhookWindowsHookEx(IntPtr hhk);

        // Hook for the mouse.
        protected IntPtr mouseHook = IntPtr.Zero;

        // Hook for the keyboard.
        protected IntPtr keyboardHook = IntPtr.Zero;

        // Delegate for low level mouse messages.
        protected HookProc lowLevelMouseMessageDelegate;

        // Delegate for low level keyboard messages.
        protected HookProc lowLevelKeyboardMessageDelegate;

        // The time the previous OnInputChanged event fired.
        protected DateTime previousInputChangedTime;

        // Whether Dispose() has been called.
        private bool disposed = false;

        public InputMonitor()
        {
        }

        // Starts the input monitoring.
        public void Start()
        {
            if (mouseHook != IntPtr.Zero || keyboardHook != IntPtr.Zero)
            {
                throw new Exception("Called Start() twice without calling Stop() first.");
            }

            lowLevelMouseMessageDelegate = HandleLowLevelMouseMessage;
            lowLevelKeyboardMessageDelegate = HandleLowLevelKeyboardMessage;

            mouseHook = SetWindowsHookEx((int)HookID.LowLevelMouse, lowLevelMouseMessageDelegate, IntPtr.Zero, 0);
            keyboardHook = SetWindowsHookEx((int)HookID.LowLevelKeyboard, lowLevelKeyboardMessageDelegate, IntPtr.Zero, 0);
        }

        // Stops the input monitoring.
        public void Stop()
        {
            RemoveHooks();
        }
        
        // Implement IDisposable.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Overridable dispose method.
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Cleanup managed objects.
                    InputChanged = null;
                    lowLevelMouseMessageDelegate = null;
                    lowLevelKeyboardMessageDelegate = null;
                }

                // Cleanup unmanaged objects.
                RemoveHooks();
            }
        }

        // Removes the hooks the monitor uses.
        protected void RemoveHooks()
        {
            if (mouseHook != IntPtr.Zero)
            {
                UnhookWindowsHookEx(mouseHook);
                mouseHook = IntPtr.Zero;
            }

            if (keyboardHook != IntPtr.Zero)
            {
                UnhookWindowsHookEx(keyboardHook);
                keyboardHook = IntPtr.Zero;
            }

            lowLevelMouseMessageDelegate = null;
            lowLevelKeyboardMessageDelegate = null;
        }

        // Event raiser for input changed.
        protected virtual void OnInputChanged(EventType type)
        {
            InputChanged?.Invoke(this, type);
        }

        // Event handler for mouse messages.
        protected IntPtr HandleLowLevelMouseMessage(int nCode, UIntPtr wParam, IntPtr lParam)
        {
            if (nCode == hookCodeAction)
            {
                // wParam and lParam contain information.
                EventType type = EventType.None;
                switch ((WindowMessage)wParam)
                {
                    case WindowMessage.MouseLeftButtonDown:
                    {
                        type = EventType.MouseLeftButtonDown;
                        break;
                    }
                    case WindowMessage.MouseRightButtonDown:
                    {
                        type = EventType.MouseRightButtonDown;
                        break;
                    }
                    case WindowMessage.MouseMove:
                    {
                        type = EventType.MouseMove;
                        break;
                    }
                    case WindowMessage.MouseWheel:
                    {
                        type = EventType.MouseWheel;
                        break;
                    }
                    case WindowMessage.MouseHorizontalWheel:
                    {
                        type = EventType.MouseHorizontalWheel;
                        break;
                    }
                }

                if (type != EventType.None)
                {
                    // An event we care about.
                    OnInputChanged(type);
                }
            }

            return CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
        }

        protected IntPtr HandleLowLevelKeyboardMessage(int nCode, UIntPtr wParam, IntPtr lParam)
        {
            if (nCode == hookCodeAction)
            {
                // wParam and lParam contain information.
                EventType type = EventType.None;
                switch ((WindowMessage)wParam)
                {
                    case WindowMessage.NonsystemKeyDown:
                    case WindowMessage.SystemKeyDown:
                    {
                        type = EventType.KeyDown;
                        break;
                    }
                }

                if (type != EventType.None)
                {
                    // An event we care about.
                    OnInputChanged(type);
                }
            }

            return CallNextHookEx(IntPtr.Zero, nCode, wParam, lParam);
        }
    }
}
