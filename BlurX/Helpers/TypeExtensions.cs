namespace Mask.BlurX.Helpers
{
    public static class TypeExtensions
    {
        public static bool IsAnonymousType(this Type type)
        {
            return Attribute.IsDefined(type, typeof(System.Runtime.CompilerServices.CompilerGeneratedAttribute))
                   && type.IsGenericType
                   && type.Name.Contains("AnonymousType")
                   && (type.Name.StartsWith("<>") || type.Name.StartsWith("VB$"));
        }
    }
}
