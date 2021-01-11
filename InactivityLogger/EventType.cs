using System;

namespace InactivityLogger
{
    // Events the logger cares about.
    public enum EventType
    {
        None,   // Initial state.
        Started,
        Stopped,
        MouseLeftButtonDown,
        MouseRightButtonDown,
        MouseMove,
        MouseWheel,
        MouseHorizontalWheel,
        KeyDown,
        WentIdle,
        IdlePeriodChanged
    }
}
