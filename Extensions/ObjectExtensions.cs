using System;

namespace Extensions
{
    public static class ObjectExtensions
    {
        public static bool IsNull(this object value)
        {
            if (value == null)
            {
                return true;
            }
            return false;
        }
        public static bool IsNotNull(this object value)
        {
            if (value == null)
            {
                return false;
            }
            return true;
        }
        public static bool IsNumericType(this object o)
        {
            switch (Type.GetTypeCode(o.GetType()))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }
    }
}
