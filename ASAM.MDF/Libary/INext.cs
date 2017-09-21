using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ASAM.MDF.Libary
{
    public interface INext<T>
        where T : INext<T>
    {
        T Next { get; }
    }
}
