using System.Text.RegularExpressions;

namespace Mask.BlurX;

public static class BlurXMaskEngine
{
    public static string Apply(string value, MaskOptions opt)
    {
        if (string.IsNullOrWhiteSpace(value))
            return value;

        if (opt == null)
            return value;

        opt.BlurCharCount = opt.BlurCharCount <= 0 ? DefaultBlurCharCount(value) : opt.BlurCharCount;

        return opt.Style switch
        {
            BlurStyle.Prefix => MaskPrefix(value, opt),
            BlurStyle.Suffix => MaskSuffix(value, opt),
            BlurStyle.Middle => MaskMiddle(value, opt),
            BlurStyle.Email => MaskEmail(value, opt),
            BlurStyle.Regex => MaskRegex(value, opt),
            BlurStyle.Default => MaskByRatio(value, 0.2, opt.BlurCharCount, 0.2, opt.BlurChar),
            _ => MaskFull(value, opt)
        };
    }

    // ----------------------------------------

    private static string MaskPrefix(string value, MaskOptions opt)
    {
        int visible = Clamp(opt.VisibleCharCount, value.Length);
        int blur = Clamp(opt.BlurCharCount, value.Length - visible);

        var prefix = value[..visible];
        var blurred = new string(opt.BlurChar, blur);
        var remaining = value[(visible + blur)..];

        return prefix + blurred + remaining;
    }

    private static string MaskSuffix(string value, MaskOptions opt)
    {
        int visible = Clamp(opt.VisibleCharCount, value.Length);
        int blur = Clamp(opt.BlurCharCount, value.Length - visible);

        var suffix = value[^visible..];
        var blurred = new string(opt.BlurChar, blur);
        var remaining = value[..(value.Length - visible - blur)];

        return remaining + blurred + suffix;
    }

    private static string MaskMiddle(string value, MaskOptions opt)
    {
        int visible = Clamp(opt.VisibleCharCount, value.Length / 2);

        int availableBlurs =
            value.Length - (visible * 2);

        int blur = Clamp(opt.BlurCharCount, availableBlurs);

        string prefix = value[..visible];
        string suffix = value[(value.Length - visible)..];

        int midStart = visible + ((availableBlurs - blur) / 2);
        string untouchedMiddlePrefix = value[visible..midStart];
        string untouchedMiddleSuffix = value[(midStart + blur)..(value.Length - visible)];

        string blurred = new string(opt.BlurChar, blur);

        return prefix + untouchedMiddlePrefix + blurred + untouchedMiddleSuffix + suffix;
    }

    private static string MaskFull(string value, MaskOptions opt)
    {
        int visible = Clamp(opt.VisibleCharCount, value.Length);

        int blur = value.Length - visible;

        return new string(opt.BlurChar, blur) + value[^visible..];
    }

    private static string MaskEmail(string value, MaskOptions opt)
    {
        if (!value.Contains('@'))
            return MaskFull(value, opt);

        var parts = value.Split('@', 2);
        var local = parts[0];
        var domain = parts[1];

        int visible = Clamp(opt.VisibleCharCount, local.Length);
        int blur = Clamp(opt.BlurCharCount, local.Length - visible);

        var prefix = local[..visible];
        var masked = new string(opt.BlurChar, blur);
        var rest = local[(visible + blur)..];

        return $"{prefix}{masked}{rest}@{domain}";
    }

    private static string MaskRegex(string value, MaskOptions opt)
    {
        if (string.IsNullOrWhiteSpace(opt.RegexPattern))
            return FinishFull(value, opt);

        return Regex.Replace(value, opt.RegexPattern, opt.BlurChar.ToString());
    }

    public static string MaskByRatio(string value, double prefixVisibleRatio, double blurRatio,
                                     double suffixVisibleRatio, char blurChar = '*')
    {
        if (string.IsNullOrWhiteSpace(value))
            return value;

        int len = value.Length;

        int prefixVisible = (int)Math.Floor(len * prefixVisibleRatio);
        int blurCount = (int)Math.Floor(len * blurRatio);
        int suffixVisible = (int)Math.Floor(len * suffixVisibleRatio);

        // ✅ Ensure we don't exceed string bounds
        prefixVisible = Math.Clamp(prefixVisible, 0, len);
        suffixVisible = Math.Clamp(suffixVisible, 0, len);

        int maxBlurAvailable = len - (prefixVisible + suffixVisible);
        blurCount = Math.Clamp(blurCount, 0, maxBlurAvailable);

        // ✅ Build result
        string prefix = value[..prefixVisible];
        string masked = new string(blurChar, blurCount);
        string suffix = value[(len - suffixVisible)..];

        int untouchedMiddleStart = prefixVisible + blurCount;
        int untouchedMiddleEnd = len - suffixVisible;

        string middleUnchanged =
            untouchedMiddleStart < untouchedMiddleEnd
                ? value[untouchedMiddleStart..untouchedMiddleEnd]
                : string.Empty;

        return prefix + masked + middleUnchanged + suffix;
    }


    private static string FinishFull(string value, MaskOptions opt)
    {
        int blur = Clamp(opt.BlurCharCount, value.Length);
        return new string(opt.BlurChar, blur) + value[blur..];
    }

    private static int Clamp(int n, int max)
        => Math.Min(Math.Max(n, 0), max);

    private static int DefaultBlurCharCount(string value, double blurCount = 0.6)
        => (int)Math.Ceiling(value.Length * 0.6);
}