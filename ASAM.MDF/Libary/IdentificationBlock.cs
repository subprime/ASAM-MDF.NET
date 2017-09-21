using System;
using System.Text;

namespace ASAM.MDF.Libary
{
    /// <summary>
    /// The IDBLOCK always begins at file position 0 and has a constant length 
    /// of 64 Bytes. It contains information to identify the file. This 
    /// includes information about the source of the file and general 
    /// format specifications.
    /// </summary>
    public class IdentificationBlock
    {
        private string m_FileIdentifier;
        private string m_FormatIdentifier;
        private string m_ProgramIdentifier;
        private string m_Reserved1;
        private string m_Reserved2;

        public Mdf Mdf { get; private set; }

        /// <summary>
        /// The file identifier always contains "MDF". ("MDF" followed by five spaces)
        /// </summary>
        /// <value>
        /// The file identifier.
        /// </value>
        public string FileIdentifier
        {
            get { return m_FileIdentifier; }
            set { SetStringValue(ref m_FileIdentifier, value, 8); }
        }

        /// <summary>
        /// The format identifier is a textual representation of the format 
        /// version for display, e.g. "3.30 " for version 3.3 revision 0
        /// (see section 2 in specification MDF Version).
        /// </summary>
        /// <value>
        /// The format identifier.
        /// </value>
        public string FormatIdentifier
        {
            get { return m_FormatIdentifier; }
            set { SetStringValue(ref m_FormatIdentifier, value, 8); }
        }

        /// <summary>
        /// Program identifier, to identify the program which generated the MDF file
        /// </summary>
        /// <value>
        /// The program identifier.
        /// </value>
        public string ProgramIdentifier
        {
            get { return m_ProgramIdentifier; }
            set { SetStringValue(ref m_ProgramIdentifier, value, 8); }
        }

        /// <summary>
        /// Default Byte order used for this file 
        /// (can be overruled for values of a signal in CNBLOCK)
        /// 0 = Little Endian (Intel order)
        /// Any other value = Big Endian (Motorola order)
        /// </summary>
        /// <value>
        /// The byte order.
        /// </value>
        public ByteOrder ByteOrder { get; private set; }

        /// <summary>
        /// Gets the floating point format.
        /// (can be overruled for values of a signal in CNBLOCK)
        /// 0 = Floating-point format compliant with IEEE 754 standard
        /// 1 = Floating-point format compliant with G_Float (VAX architecture) (obsolete)
        /// 2 = Floating-point format compliant with D_Float (VAX architecture) (obsolete)
        /// </summary>
        /// <value>
        /// The floating-point format.
        /// </value>
        public FloatingPointFormat FloatingPointFormat { get; private set; }
        
        /// <summary>
        /// Version number of MDF format, i.e. 330 for this version
        /// </summary>
        /// <value>
        /// The version.
        /// </value>
        public ushort Version { get; private set; }
        
        // TODO: Add supported CodePages
        /// <summary>
        /// Code Page number
        /// The code page used for all strings in the MDF file except of strings in IDBLOCK and string signals (string encoded in a record).
        /// Value = 0: code page is not known.
        /// Value > 0: identification number of extended ASCII code page (includes all ANSI and OEM code pages) as specified by Microsoft, see http://msdn.microsoft.com/en-us/library/dd317756(VS.85).aspx.
        /// The code page number can be used to choose the correct character set for displaying special characters (usually ASCII code ≥ 128) if the writer of the file used a different code page than the reader. Reading tools might not support the display of strings stored with a different code page, or they might only support a selection of (common) code pages.
        /// Valid since version 3.30. Default value: 0 Note: the code page is only for documentation and not required information. It might be ignored by tools reading the file, especially if they support only a MDF version lower than 3.30.
        /// </summary>
        /// <value>
        /// The code page.
        /// </value>
        [MdfVersion(330, 0)]
        public ushort CodePage { get; private set; }
        
        /// <summary>
        /// Gets or sets the reserved1.
        /// </summary>
        /// <value>
        /// The reserved1.
        /// </value>
        public string Reserved1
        {
            get { return m_Reserved1; }
            set { SetStringValue(ref m_Reserved1, value, 2); }
        }

        /// <summary>
        /// Gets or sets the reserved2.
        /// </summary>
        /// <value>
        /// The reserved2.
        /// </value>
        public string Reserved2
        {
            get { return m_Reserved2; }
            set { SetStringValue(ref m_Reserved2, value, 30); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentificationBlock"/> class.
        /// </summary>
        /// <param name="mdf">The MDF.</param>
        /// <exception cref="System.ArgumentNullException">No mdf input file</exception>
        /// <exception cref="System.FormatException"></exception>
        public IdentificationBlock(Mdf mdf)
        {
            if (mdf == null)
                throw new ArgumentNullException("No mdf input file");

            Mdf = mdf;

            byte[] data = new byte[64];
            int read = Mdf.Data.Read(data, 0, data.Length);

            if (read != data.Length)
                throw new FormatException();

            m_FileIdentifier = UTF8Encoding.UTF8.GetString(data, 0, 8);
            m_FormatIdentifier = UTF8Encoding.UTF8.GetString(data, 8, 8);
            m_ProgramIdentifier = UTF8Encoding.UTF8.GetString(data, 16, 8);
            ByteOrder = (ByteOrder)BitConverter.ToUInt16(data, 24);
            FloatingPointFormat = (FloatingPointFormat)BitConverter.ToUInt16(data, 26);
            Version = BitConverter.ToUInt16(data, 28);
            CodePage = BitConverter.ToUInt16(data, 30);
            m_Reserved1 = UTF8Encoding.UTF8.GetString(data, 32, 2);
            m_Reserved2 = UTF8Encoding.UTF8.GetString(data, 34, 30);
        }

        /// <summary>
        /// Sets the string value.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="value">The value.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <exception cref="System.ArgumentOutOfRangeException">value</exception>
        private void SetStringValue(ref string target, string value, int maxLength)
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
