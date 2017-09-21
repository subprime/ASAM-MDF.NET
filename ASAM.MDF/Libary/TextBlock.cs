using System;
using System.Text;

namespace ASAM.MDF.Libary
{
    public class TextBlock : Block
    {
        private string m_Text;
        public string Text
        {
            get
            {
                return m_Text;
            }
            set
            {
                SetStringValue(ref m_Text, value, ushort.MaxValue - 4);
            }
        }
        
        public static implicit operator string(TextBlock textBlock)
        {
            if (textBlock == null)
                return null;

            return textBlock.Text;
        }
        public static implicit operator TextBlock(string text)
        {
            if (text == null)
                return null;

			return text;
        }

        public TextBlock(Mdf mdf)
            : base(mdf)
        {
            byte[] data = new byte[Size - 4];
            int read = Mdf.Data.Read(data, 0, data.Length);

            if (read != data.Length)
                throw new FormatException();

            m_Text = Encoding.GetEncoding(Mdf.IDBlock.CodePage).GetString(data, 0, data.Length);
        }
    }
}
