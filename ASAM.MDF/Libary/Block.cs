using System;
using System.Text;

namespace ASAM.MDF.Libary
{
    /// <summary>
    /// The abstract block class
    /// </summary>
    public abstract class Block
    {
        public Mdf Mdf { get; private set; }

        public ushort Size { get; private set; }
        public uint BlockAddress { get; private set; }
        public string Identifier { get; protected set; }
  
        public Block(Mdf mdf)
        {
            if (mdf == null)
                throw new ArgumentNullException("mdf");

            Mdf = mdf;
            BlockAddress = (uint)Mdf.Data.Position;

            byte[] data = new byte[4];
            int read = Mdf.Data.Read(data, 0, data.Length);

            if (read != data.Length)
                throw new FormatException();

            Identifier = Encoding.GetEncoding(Mdf.IDBlock.CodePage).GetString(data, 0, 2);
            Size = BitConverter.ToUInt16(data, 2);

            if (Size <= 4)
                throw new FormatException();
        }

        /// <summary>
        /// Sets the string value.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="value">The value.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">value</exception>
        protected void SetStringValue(ref string target, string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value))
            {
                target = value;
            }
            else
            {
                if (value.Length > maxLength)
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                target = value;
            }
        }
    }
}
