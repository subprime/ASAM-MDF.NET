
using System;
namespace ASAM.MDF.Libary
{
    public class ProgramBlock : Block
    {
        private byte[] m_Data;
        public byte[] Data
        {
            get
            {
                return m_Data;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                m_Data = value;
            }
        }

        public ProgramBlock(Mdf mdf)
            : base(mdf)
        {
            if (Identifier != "PR")
                throw new FormatException();

            byte[] data = new byte[Size - 4];
            int read = Mdf.Data.Read(data, 0, data.Length);

            if (read != data.Length)
                throw new FormatException();

            m_Data = data;
        }
    }
}
