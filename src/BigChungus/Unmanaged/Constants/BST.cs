namespace BigChungus.Unmanaged;

[Flags]
public enum BST : uint
{
    UNCHECKED = 0x0000,
    CHECKED = 0x0001,
    INDETERMINATE = 0x0002,
    PUSHED = 0x0004,
    FOCUS = 0x0008,
    HOT = 0x0200,
    DROPDOWNPUSHED = 0x0400,
}
