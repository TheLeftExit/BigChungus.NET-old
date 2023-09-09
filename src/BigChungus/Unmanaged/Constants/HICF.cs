namespace BigChungus.Unmanaged;

[Flags]
public enum HICF : uint
{
    OTHER = 0U,
    MOUSE = 1U,
    ARROWKEYS = 2U,
    ACCELERATOR = 4U,
    DUPACCEL = 8U,
    ENTERING = 16U,
    LEAVING = 32U,
    RESELECT = 64U,
    LMOUSE = 128U,
    TOGGLEDROPDOWN = 256U,
}
