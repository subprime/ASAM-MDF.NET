using System;
using System.Collections.Generic;
using System.Linq;

namespace ASAM.MDF.Libary
{
    public class BlockCache
    {
        private int m_Capacity;
        private Dictionary<uint, CacheItem> m_Cache;

        public IList<Block> CachedBlocks
        {
            get
            {
                return m_Cache.Values.Select(ci => ci.Block).ToList();
            }
        }
        public int Count
        {
            get
            {
                return m_Cache.Count;
            }
        }
        public int Capacity
        {
            get
            {
                return m_Capacity;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("value");


            }
        }

        public void Clear()
        {
            m_Cache.Clear();
        }

        private class CacheItem
        {
            private Block m_Block;
            public Block Block
            {
                get
                {
                    LastAccessTime = DateTime.Now;

                    return m_Block;
                }
            }
            public DateTime LastAccessTime { get; private set; }

            public CacheItem(Block block)
            {
                if (block == null)
                    throw new ArgumentNullException("block");

                m_Block = block;
                LastAccessTime = DateTime.Now;
            }
        }
    }
}
