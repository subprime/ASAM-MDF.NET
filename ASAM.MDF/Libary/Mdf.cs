using System;
using System.IO;

namespace ASAM.MDF.Libary
{
    public class Mdf
    {
        internal Stream Data { get; private set; }
        internal BlockCache Cache { get; private set; }
        public bool ReadOnly { get { return !Data.CanRead; } }
        public IdentificationBlock IDBlock { get; private set; }
        public HeaderBlock HDBlock { get; private set; }

        public Mdf(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");
            if (!stream.CanSeek)
                throw new ArgumentException("stream");

            Data = stream;
            Data.Position = 0;
            IDBlock = new IdentificationBlock(this);
            HDBlock = new HeaderBlock(this);
        }

      
    }
}
