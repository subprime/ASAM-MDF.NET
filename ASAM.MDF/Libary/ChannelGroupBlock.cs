using System;

namespace ASAM.MDF.Libary
{
    public class ChannelGroupBlock : Block, INext<ChannelGroupBlock>
    {
        private ChannelGroupBlock m_Next;
        public ChannelGroupBlock Next
        {
          get
          {
            if (m_Next == null && m_ptrNextChannelGroup != 0)
            {
              Mdf.Data.Position = m_ptrNextChannelGroup;
              m_Next = new ChannelGroupBlock(Mdf);
            }

            return m_Next;
          }
        }

        public ChannelCollection Channels { get; private set; }
        public TextBlock Comment { get; private set; }
        public UInt16 RecordID { get; private set; }
        public UInt16 NumChannels { get; private set; }
        public UInt16 RecordSize { get; private set; }
        public UInt32 NumRecords { get; private set; }
        public SampleReductionCollection SampleReductions { get; private set; }

        private uint m_ptrNextChannelGroup;
        private uint m_ptrFirstChannelBlock;
        private uint m_ptrTextBlock;
        private uint m_ptrFirstSampleReductionBlock;

        public ChannelGroupBlock(Mdf mdf)
          : base(mdf)
        {
            byte[] data = new byte[Size - 4];
            int read = Mdf.Data.Read(data, 0, data.Length);

            if (read != data.Length)
              throw new FormatException();

            m_Next = null;
            Channels = null;
            Comment = null;
            SampleReductions = null;

            m_ptrNextChannelGroup = BitConverter.ToUInt32(data, 0);
            m_ptrFirstChannelBlock = BitConverter.ToUInt32(data, 4);
            m_ptrTextBlock = BitConverter.ToUInt32(data, 8);
            RecordID = BitConverter.ToUInt16(data, 12);
            NumChannels = BitConverter.ToUInt16(data, 14);
            RecordSize = BitConverter.ToUInt16(data, 16);
            NumRecords = BitConverter.ToUInt32(data, 18);
            
            if(data.Length >= 26)
                m_ptrFirstSampleReductionBlock = BitConverter.ToUInt32(data, 22);

            if (m_ptrTextBlock != 0)
            {
                mdf.Data.Position = m_ptrTextBlock;
                Comment = new TextBlock(mdf);
            }

            if (m_ptrFirstChannelBlock != 0)
            {
                mdf.Data.Position = m_ptrFirstChannelBlock;
                Channels = new ChannelCollection(mdf, new ChannelBlock(mdf));
            }

            //if (m_ptrFirstSampleReductionBlock != 0)
            //{
            //    mdf.Data.Position = m_ptrFirstSampleReductionBlock;
            //    SampleReductions = new SampleReductionCollection(mdf, new SampleReductionBlock(mdf));
            //}
        }
    }
}
