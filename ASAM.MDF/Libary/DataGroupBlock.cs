using System;

namespace ASAM.MDF.Libary
{
    public class DataGroupBlock : Block, INext<DataGroupBlock>
    {
        private DataGroupBlock m_Next;

        public DataGroupBlock Next
        {
            get
            {
                if (m_Next == null && m_ptrNextDataGroup != 0)
                {
                    Mdf.Data.Position = m_ptrNextDataGroup;
                    m_Next = new DataGroupBlock(Mdf);
                }

                return m_Next;
            }
        }
        public ChannelGroupCollection ChannelGroups { get; private set; }
        public TriggerBlock Trigger;
        
        public UInt16 NumChannelGroups { get; private set; }
        public UInt16 NumRecordIds { get; private set; }
        public UInt32 Reserved { get; private set; }

        private uint m_ptrNextDataGroup;
        private uint m_ptrFirstChannelGroupBlock;
        private uint m_ptrTriggerBlock;
        private uint m_ptrDataBlock;

        public DataGroupBlock(Mdf mdf)
          : base(mdf)
        {
            byte[] data = new byte[Size - 4];
            int read = Mdf.Data.Read(data, 0, data.Length);

            if (read != data.Length)
              throw new FormatException();

            m_Next = null;
            ChannelGroups = null;
            Trigger = null;
            Reserved = 0;

            m_ptrNextDataGroup = BitConverter.ToUInt32(data, 0);
            m_ptrFirstChannelGroupBlock = BitConverter.ToUInt32(data, 4);
            m_ptrTriggerBlock = BitConverter.ToUInt32(data, 8);
            m_ptrDataBlock = BitConverter.ToUInt32(data, 12);
            NumChannelGroups = BitConverter.ToUInt16(data, 16);
            NumRecordIds = BitConverter.ToUInt16(data, 18);
            
            if(data.Length >= 24)
              Reserved = BitConverter.ToUInt32(data, 20);

            Mdf.Data.Position = m_ptrFirstChannelGroupBlock;
            ChannelGroups = new ChannelGroupCollection(mdf, new ChannelGroupBlock(mdf));

            /// TODO: Call Trigger Blocks
            //if (m_ptrTriggerBlock != 0)
            //{
            //    Mdf.Data.Position = m_ptrTriggerBlock;
            //    Trigger = new TriggerBlock(mdf);
            //}

            /// TODO: Call ProgramsBlock ?
            //if (ptrProgramBlock != 0)
            //{
            //    Mdf.Data.Position = ptrProgramBlock;
            //    ProgramBlock = new ProgramBlock(mdf);
            //}
        }
    }
}
