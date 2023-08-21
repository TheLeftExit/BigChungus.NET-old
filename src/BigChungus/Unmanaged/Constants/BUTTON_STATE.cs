namespace BigChungus.Unmanaged;

public enum BUTTON_STATE : uint
{
    BST_UNCHECKED = 0x0000,
    BST_CHECKED = 0x0001,
    BST_INDETERMINATE = 0x0002,
    BST_PUSHED = 0x0004,
    BST_FOCUS = 0x0008,
    BST_HOT = 0x0200,
    BST_DROPDOWNPUSHED = 0x0400,
}