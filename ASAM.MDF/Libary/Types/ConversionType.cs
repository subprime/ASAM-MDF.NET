
namespace ASAM.MDF.Libary.Types
{
    public enum ConversionType : ushort
    {
        Linear = 0,
        TabularInterpolated,
        Tabular,
        Polynomial,
        Exponential,
        Logarithmic,
        RationalConversion,
        AsamMcd2Fomular,
        AsamMcd2Table,
        AsamMcd2RangeTable = 12,
        Date = 132,
        Time = 133,
        OneToOne = 65535,
    }
}
