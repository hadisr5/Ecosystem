using System;
using System.Collections;

namespace Extensions
{
    public static class TypeExtensions
    {
        public static bool IsTypeOf(this object obj, Type type)
        {
            if (type == obj.GetType())
                return true;
            return false;
        }

        public static bool IsNumericType(this Type o)
        {
            if (o == typeof(Byte) || o == typeof(SByte)
            || o == typeof(UInt16) || o == typeof(UInt32)
            || o == typeof(UInt64) || o == typeof(Int16)
            || o == typeof(Int32) || o == typeof(Int64)
            || o == typeof(Decimal))
            {
                return true;
            }
            return false;
        }
        public static bool IsCollectionType(this Type type)
        {
            return (type.GetInterface(nameof(ICollection)) != null);
        }
        public static bool IsEnumerableType(this Type type)
        {
            return (type.GetInterface(nameof(IEnumerable)) != null);
        }
        public static bool IsNullableType(this Type type)
        {
            if (Nullable.GetUnderlyingType(type) != null)
            {
                return true;
            }
            return false;
        }
        public static bool IsListType(this Type type)
        {
            return (type.GetInterface(nameof(IList)) != null);
        }
    }
}
