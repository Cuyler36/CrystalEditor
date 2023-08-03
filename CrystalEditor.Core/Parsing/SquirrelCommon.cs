namespace CrystalEditor.Core.Parsing
{
    public enum SQObjectType
    {
        OT_NULL = 0x01000001,
        OT_BOOL = 0x01000008,
        OT_INTEGER = 0x05000002,
        OT_FLOAT = 0x05000004,
        OT_STRING = 0x08000010,
        OT_ARRAY = 0x08000040,
        OT_TABLE = 0x0A000020
    }
}
