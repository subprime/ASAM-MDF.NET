
namespace ASAM.MDF.Libary.Types
{
    public enum SignalType : ushort
    {
        UInt = 0,
        Int = 1,
        IEEE754Float = 2,
        IEEE754Double = 3,
        FFloat = 4,
        GFloat = 5,
        DFloat = 6,
        String = 7,
        Bytes = 8,
        UIntBigEndian = 9,
        IntBigEndian = 10,
        IEEE754FloatBigEndian = 11,
        IEEE754DoubleBigEndian = 12,
        UIntLittleEndian = 13,
        IntLittleEndian = 14,
        IEEE754FloatLittleEndian = 15,
        IEEE754DoubleLittleEndian = 16,
    }
}
