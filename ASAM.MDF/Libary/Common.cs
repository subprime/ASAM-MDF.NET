using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASAM.MDF.Libary
{
    internal static class Common
    {
        internal static List<T> BuildBlockList<T>(List<T> list, T first)
            where T : INext<T>
        {
            if (list == null)
            {
                list = new List<T>();
                T current = first;
                while (current != null)
                {
                    list.Add(current);
                    current = current.Next;
                }
            }

            return list;
        }
    }
}
