using System.Reflection;

namespace BlurX
{
    public static class BlurXHelper
    {
        public static T Mask<T>(T obj, MaskOptions overrideOpt = null)
        {
            if (obj == null)
                return obj;

            // HANDLE COLLECTIONS
            if (obj is System.Collections.IEnumerable enumerable && obj is not string)
            {
                foreach (var item in enumerable)
                    Mask(item, overrideOpt);

                return obj;
            }

            var type = obj.GetType();

            var classAttr = type.GetCustomAttribute<BlurXAttribute>();

            var props = type.GetProperties()
                            .Where(p =>
                                p.CanRead &&
                                p.CanWrite &&
                                p.PropertyType == typeof(string))
                            .ToList();

            // Check property-level attributes
            bool hasFieldAttributes =
                props.Any(p => p.GetCustomAttribute<BlurXFieldAttribute>() != null);


            // Exit only when NOTHING applies
            if (classAttr == null && !hasFieldAttributes && overrideOpt == null)
                return obj;

            // Determine default options
            var classOptions = overrideOpt ?? classAttr?.ToOptions();

            foreach (var prop in props)
            {
                var value = prop.GetValue(obj) as string;
                if (string.IsNullOrWhiteSpace(value))
                    continue;

                var fieldAttr = prop.GetCustomAttribute<BlurXFieldAttribute>();

                if (fieldAttr != null)
                {
                    var options =
                        fieldAttr.ToOptions()
                        ?? classOptions
                        ?? new MaskOptions();   // fallback safety

                    value = BlurXMaskEngine.Apply(value, options);
                }

                prop.SetValue(obj, value);
            }

            return obj;
        }
    }
}