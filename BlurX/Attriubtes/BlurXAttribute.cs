namespace BlurX
{
    [Obsolete("Class-level [BlurX] is under development and currently not supported. " +
        "Please use [BlurXField] on individual properties.", false)]
    [AttributeUsage(AttributeTargets.Class)]
    public class BlurXAttribute : Attribute
    {
        public BlurStyle Style { get; }
        public int MaskLength { get; }
        public char MaskChar { get; }
        public string RegexPattern { get; }

        public BlurXAttribute(BlurStyle style = BlurStyle.Prefix,
            int maskLength = 2,
            char maskChar = '*',
            string regexPattern = null)
        {
            Style = style;
            MaskLength = maskLength;
            MaskChar = maskChar;
            RegexPattern = regexPattern;
        }

        public MaskOptions ToOptions()
            => new()
            {
                Style = Style,
                BlurCharCount = MaskLength,
                BlurChar = MaskChar,
                RegexPattern = RegexPattern
            };
    }
}