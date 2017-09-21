using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ASAM.MDF.Libary
{
    public class DataGroupCollection : IList<DataGroupBlock>
    {
        public Mdf Mdf { get; private set; }
        private DataGroupBlock first;
        private List<DataGroupBlock> lDataGroupBlock;

        public DataGroupCollection(Mdf mdf, DataGroupBlock dgBlock)
        {
            if (mdf == null)
                throw new ArgumentNullException("mdf");

            if (dgBlock == null)
                throw new ArgumentNullException("dgBlock");
            Mdf = mdf;

			first = dgBlock;
			lDataGroupBlock = Common.BuildBlockList(this.lDataGroupBlock, this.first);
        }

        public int IndexOf(DataGroupBlock item)
        {
            throw new NotImplementedException();
        }
        public void Insert(int index, DataGroupBlock item)
        {
            throw new NotImplementedException();
        }
        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }
        public DataGroupBlock this[int index]
        {
            get
            {
                return this.lDataGroupBlock[index];
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public void Add(DataGroupBlock item)
        {
            throw new NotImplementedException();
        }
        public void Clear()
        {
            throw new NotImplementedException();
        }
        public bool Contains(DataGroupBlock item)
        {
            throw new NotImplementedException();
        }
        public void CopyTo(DataGroupBlock[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }
        public int Count
        {
            get { return this.lDataGroupBlock.Count; }
        }
        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }
        public bool Remove(DataGroupBlock item)
        {
            throw new NotImplementedException();
        }
        public IEnumerator<DataGroupBlock> GetEnumerator()
        {
			return this.lDataGroupBlock.GetEnumerator();
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
     
    }
}
