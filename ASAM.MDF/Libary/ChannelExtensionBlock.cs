using System;
using System.Text;

namespace ASAM.MDF.Libary
{
    public class ChannelExtensionBlock : Block
    {
        public ExtensionType Type { get; private set; }
        public DimBlockSupplement DimBlockSupplement { get; private set; }
        public VectorCanBlockSupplement VectorCanBlockSupplement { get; private set; }

        public ChannelExtensionBlock(Mdf mdf)
          : base(mdf)
        {
          byte[] data = new byte[Size - 4];
          int read = Mdf.Data.Read(data, 0, data.Length);

          if (read != data.Length)
            throw new FormatException();

          Type = (ExtensionType)BitConverter.ToUInt16(data, 0);

          if (Type == ExtensionType.DIM)
          {
            DimBlockSupplement = new DimBlockSupplement(mdf);
          }
          else if (Type == ExtensionType.VectorCAN)
          {
            VectorCanBlockSupplement = new VectorCanBlockSupplement(mdf);
          }
        }
    }
}
