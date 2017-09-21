using System;

namespace ASAM.MDF.Libary
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class MdfVersionAttribute : Attribute
    {
        public UInt16 Version { get; private set; }
        public object DefaultValue { get; private set; }

        public MdfVersionAttribute(UInt16 version, object defaultValue)
        {
            Version = version;
            DefaultValue = defaultValue;
        }
    }
}
