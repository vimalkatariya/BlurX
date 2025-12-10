using Mask.BlurX.Helpers;
using System.Reflection;

namespace Mask.BlurX;

public static class BlurXHelper
{
    public static T Mask<T>(T obj, MaskOptions overrideOpt = null)
    {
        if (obj == null)
            return obj;

        try
        {
            MaskInternal(obj, overrideOpt, new HashSet<object>(Helpers.ReferenceEqualityComparer.Instance));
            return obj;
        }
        catch
        {
            // Any unexpected error → return original object safely
            return obj;
        }
    }


    private static void MaskInternal(object obj, MaskOptions options, HashSet<object> visited)
    {
        if (obj == null)
            return;

        if (visited.Contains(obj))
            return;

        visited.Add(obj);

        var type = obj.GetType();

        // Skip anonymous objects
        if (type.IsAnonymousType())
            return;

        // Handle List<string>
        if (obj is IList<string> stringList)
        {
            for (int i = 0; i < stringList.Count; i++)
            {
                var value = stringList[i];
                if (!string.IsNullOrWhiteSpace(value))
                {
                    stringList[i] = BlurXMaskEngine.Apply(value, options);
                }
            }
            return;
        }

        // Handle collections of objects: List<T>, IEnumerable<T>
        if (obj is System.Collections.IEnumerable enumerable && obj is not string)
        {
            foreach (var item in enumerable)
                MaskInternal(item, options, visited);

            return;
        }

        // Process single object properties using cache
        var props = MaskPropertyCache.GetProperties(type);

        foreach (var prop in props)
        {
            var value = prop.GetValue(obj);

            if (value == null)
                continue;

            // Mask string property
            if (prop.PropertyType == typeof(string))
            {
                var str = value as string;

                if (string.IsNullOrWhiteSpace(str))
                    continue;

                var fieldAttr = prop.GetCustomAttribute<BlurXFieldAttribute>();

                var opt = fieldAttr?.ToOptions() ?? options;

                if (opt == null)
                    continue;

                prop.SetValue(obj, BlurXMaskEngine.Apply(str, opt));
            }
            else
            {
                // Recurse into nested objects
                MaskInternal(value, options, visited);
            }
        }
    }
}