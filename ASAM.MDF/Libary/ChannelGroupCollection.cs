using System;
using System.Collections.Generic;

namespace ASAM.MDF.Libary
{
    public class ChannelGroupCollection : IList<ChannelGroupBlock>
    {
        public Mdf Mdf { get; private set; }
        private ChannelGroupBlock first;
        private List<ChannelGroupBlock> lChannelGroupBlock;

        public ChannelGroupCollection(Mdf mdf, ChannelGroupBlock cgBlock)
        {
          if (mdf == null)
            throw new ArgumentNullException("mdf");

          if (cgBlock == null)
            throw new ArgumentNullException("cgBlock");
          Mdf = mdf;

		  first = cgBlock;
		  lChannelGroupBlock = Common.BuildBlockList(this.lChannelGroupBlock, this.first);
        }

        public int IndexOf(ChannelGroupBlock item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, ChannelGroupBlock item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public ChannelGroupBlock this[int index]
        {
            get
            {
                
                return this.lChannelGroupBlock[index];
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Add(ChannelGroupBlock item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(ChannelGroupBlock item)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(ChannelGroupBlock[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(ChannelGroupBlock item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<ChannelGroupBlock> GetEnumerator()
        {
			return this.lChannelGroupBlock.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
