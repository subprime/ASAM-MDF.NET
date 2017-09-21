using System;

namespace ASAM.MDF.Libary
{
    /// <summary>
    /// TODO: Complete TriggerBlock
    /// </summary>
    public class TriggerBlock : Block
    {
        public TextBlock Comment { get; private set; }
        public UInt16 Events { get; private set; }

        public TriggerBlock(Mdf mdf)
          : base(mdf)
        {
        }
    }
}
