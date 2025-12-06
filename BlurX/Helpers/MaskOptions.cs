namespace BlurX
{
    public class MaskOptions
    {
        public BlurStyle Style { get; set; } = BlurStyle.Default;

        public int BlurCharCount { get; set; } = 0;

        public int VisibleCharCount { get; set; } = 0;

        public char BlurChar { get; set; } = '*';

        public string RegexPattern { get; set; }
    }
}