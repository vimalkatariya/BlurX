namespace BlurX
{
    [AttributeUsage(AttributeTargets.Property)]
    public class BlurXFieldAttribute : Attribute
    {
        public BlurStyle Style { get; }
        public int MaskLength { get; }
        public int VisibleCharCount { get; }
        public char MaskChar { get; }
        public string RegexPattern { get; }

        public BlurXFieldAttribute(
            BlurStyle style,
            int maskLength = 0,
            int visibleCharCount = 0,
            char maskChar = '*',
            string regexPattern = null)
        {
            Style = style;
            MaskLength = maskLength;
            VisibleCharCount = visibleCharCount;
            MaskChar = maskChar;
            RegexPattern = regexPattern;
        }

        public MaskOptions ToOptions()
            => new()
            {
                Style = Style,
                BlurCharCount = MaskLength,
                VisibleCharCount = VisibleCharCount,
                BlurChar = MaskChar,
                RegexPattern = RegexPattern
            };
    }

}