using System;
using System.Runtime.InteropServices;

namespace InactivityLogger
{
    // Events the logger cares about.
    public enum EventType
    {
        None,   // Initial state.
        MouseLeftButtonDown,
        MouseRightButtonDown,
        MouseMove,
        MouseWheel,
        MouseHorizontalWheel
    }

    // Monitors input.
    public class InputMonitor
    {
        public event EventHandler<EventType> InputChanged;

        enum HookID: int
        {
           // Low level mouse hook.
            LowLevelMouse = 14,
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
            MouseHorizontalWheel = 0x020E
        }


        // The code for when wparam and lparam parameters contain information about a mouse message.
        protected const int HookCodeAction = 0;

        // Delegate for HOOKPROC.
        protected delegate IntPtr HookProc(IntPtr code, UIntPtr wparam, IntPtr lparam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        protected static extern IntPtr SetWindowsHookExW(IntPtr idHook, HookProc lpfn, IntPtr hmod, UIntPtr threadID);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        protected static extern IntPtr CallNextHookEx(IntPtr hhk, IntPtr nCode, UIntPtr wparam, IntPtr lparam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        protected static extern bool UnhookWindowsHookEx(IntPtr hhk);

        protected IntPtr mouseHook = IntPtr.Zero;

        public InputMonitor()
        {
        }

        ~InputMonitor()
        {
            // Cleanup.
            Stop();
        }

        // Starts the input monitoring.
        public void Start()
        {
            if (mouseHook != IntPtr.Zero)
            {
                throw new Exception("Called Start() twice without calling Stop() first.");
            }

            mouseHook = SetWindowsHookExW((IntPtr)HookID.LowLevelMouse, HandleMouseMessage, IntPtr.Zero, UIntPtr.Zero);
        }

        // Stops the input monitoring.
        public void Stop()
        {
            if (mouseHook != IntPtr.Zero)
            {
                UnhookWindowsHookEx(mouseHook);
                mouseHook = IntPtr.Zero;
            }
        }

        // Event raiser for input changed.
        protected virtual void OnInputChanged(EventType type)
        {
            InputChanged?.Invoke(this, type);
        }

        // Event handler for mouse messages.
        protected IntPtr HandleMouseMessage(IntPtr code, UIntPtr wparam, IntPtr lparam)
        {
            if ((int)code != HookCodeAction)
            {
                // Nothing we want.
                return CallNextHookEx(IntPtr.Zero, code, wparam, lparam);
            }

            EventType type = EventType.None;
            switch ((WindowMessage)wparam)
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
                // Not an event we care about.
                OnInputChanged(type);
            }

            return CallNextHookEx(IntPtr.Zero, code, wparam, lparam);
        }
    }
}
