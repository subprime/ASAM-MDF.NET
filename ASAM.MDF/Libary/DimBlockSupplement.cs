using System;
using System.Text;

namespace ASAM.MDF.Libary
{
  public class DimBlockSupplement
  {
    public UInt16 NumberOfModule;
    public UInt32 Address;
    public string Description;
    public string IdentificationOfEcu;
    
    public DimBlockSupplement(Mdf mdf)
    {
      byte[] data = new byte[118];
      int read = mdf.Data.Read(data, 0, data.Length);

      if (read != data.Length)
        throw new FormatException();

      NumberOfModule = BitConverter.ToUInt16(data, 0);
      Address = BitConverter.ToUInt32(data, 2);
      Description = Encoding.GetEncoding(mdf.IDBlock.CodePage).GetString(data, 6, 80);
      IdentificationOfEcu = Encoding.GetEncoding(mdf.IDBlock.CodePage).GetString(data, 86, 32);
    }

  }
}
