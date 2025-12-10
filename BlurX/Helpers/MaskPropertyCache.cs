using System.Collections.Concurrent;
using System.Reflection;

namespace Mask.BlurX.Helpers
{
    internal static class MaskPropertyCache
    {
        private static readonly ConcurrentDictionary<Type, List<PropertyInfo>> _cache = new();

        public static List<PropertyInfo> GetProperties(Type type)
        {
            return _cache.GetOrAdd(type, t =>
                t.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                 .Where(p =>
                        p.CanRead &&
                        p.CanWrite &&
                        // allow string or complex objects – skip bytes, enums, streams etc.
                        (IsString(p) || IsComplexAllowed(p)))
                 .ToList());
        }

        private static bool IsString(PropertyInfo p) => p.PropertyType == typeof(string);

        private static bool IsComplexAllowed(PropertyInfo p)
        {
            var t = p.PropertyType;

            // Skip non-maskable types
            return !t.IsPrimitive &&
                   !t.IsEnum &&
                   t != typeof(decimal) &&
                   t != typeof(DateTime) &&
                   t != typeof(DateTimeOffset) &&
                   t != typeof(TimeSpan) &&
                   t != typeof(byte[]) &&
                   !typeof(Stream).IsAssignableFrom(t);
        }
    }

}
