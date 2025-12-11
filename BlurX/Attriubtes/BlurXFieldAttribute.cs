namespace Mask.BlurX;

/// <summary>
/// Defines how a string property should be masked by the BlurX engine.
/// Apply this attribute to string properties to automatically hide,
/// blur, or partially mask sensitive text values at runtime.
///
/// <para>
/// Masking behavior is determined by <see cref="Style"/> and optional parameters
/// such as <see cref="MaskLength"/> and <see cref="VisibleCharCount"/>.
/// </para>
///
/// <example>
/// Mask only 3 characters before the suffix and show the last 3 characters:
/// <code>
/// [BlurXField(BlurStyle.Suffix, maskLength: 3, visibleCharCount: 3)]
/// </code>
///
/// Mask everything except last 4 characters:
/// <code>
/// [BlurXField(BlurStyle.Full, visibleCharCount: 4)]
/// </code>
///
/// Mask digits using regex:
/// <code>
/// [BlurXField(BlurStyle.Regex, regexPattern: @"\d")]
/// </code>
/// </example>
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class BlurXFieldAttribute : Attribute
{
    /// <summary>
    /// Determines the masking strategy applied to the property.
    /// </summary>
    /// <remarks>
    /// <para>Available styles:</para>
    /// <list type="bullet">
    ///   <item><see cref="BlurStyle.Default"/> – Uses engine default mask behavior.</item>
    ///   <item><see cref="BlurStyle.Full"/> – Masks the entire string except visible characters if specified.</item>
    ///   <item><see cref="BlurStyle.Prefix"/> – Masks <see cref="MaskLength"/> characters immediately after the visible prefix.</item>
    ///   <item><see cref="BlurStyle.Suffix"/> – Masks <see cref="MaskLength"/> characters immediately before the visible suffix.</item>
    ///   <item><see cref="BlurStyle.Middle"/> – Keeps both ends visible and masks the middle section.</item>
    ///   <item><see cref="BlurStyle.Email"/> – Applies built-in safe email masking rules.</item>
    ///   <item><see cref="BlurStyle.Regex"/> – Applies masking to text matching <see cref="RegexPattern"/>.</item>
    /// </list>
    /// </remarks>
    public BlurStyle Style { get; }

    /// <summary>
    /// Number of characters to replace with <see cref="MaskChar"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Used by <see cref="BlurStyle.Prefix"/>, <see cref="BlurStyle.Suffix"/>,
    /// <see cref="BlurStyle.Middle"/>, and <see cref="BlurStyle.Default"/>.
    /// </para>
    ///
    /// <para>
    /// This value controls how *many characters are blurred in total*, independent
    /// of <see cref="VisibleCharCount"/>.
    /// </para>
    ///
    /// <example>
    /// Mask 3 characters just before last 2 visible ones:
    /// <code>[BlurXField(BlurStyle.Suffix, maskLength: 3, visibleCharCount: 2)]</code>
    /// </example>
    /// </remarks>
    public int MaskLength { get; }

    /// <summary>
    /// Number of characters that remain **fully visible** after masking.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This typically represents either:
    /// </para>
    /// <list type="bullet">
    ///   <item>The visible **prefix** or **suffix**</item>
    ///   <item>The combined ends when using <see cref="BlurStyle.Middle"/></item>
    /// </list>
    ///
    /// <para>
    /// Used by all styles except <see cref="BlurStyle.Regex"/>.
    /// </para>
    ///
    /// <example>
    /// Keep last 4 characters visible while masking everything else:
    /// <code>[BlurXField(BlurStyle.Full, visibleCharCount: 4)]</code>
    /// </example>
    /// </remarks>
    public int VisibleCharCount { get; }

    /// <summary>
    /// Character used to replace masked text.
    /// </summary>
    /// <remarks>
    /// <para>Default is '*'.</para>
    /// <para>Common alternatives include '#' or 'X'.</para>
    /// </remarks>
    public char MaskChar { get; }

    /// <summary>
    /// Regular expression pattern used when <see cref="Style"/> is
    /// <see cref="BlurStyle.Regex"/>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Only the matched segments will be masked.
    /// </para>
    ///
    /// <para>
    /// This value is <b>required</b> when using <see cref="BlurStyle.Regex"/>.
    /// </para>
    ///
    /// <example>
    /// Mask all digits:
    /// <code>[BlurXField(BlurStyle.Regex, regexPattern: @"\d")]</code>
    ///
    /// Mask lowercase letters:
    /// <code>[BlurXField(BlurStyle.Regex, regexPattern: "[a-z]")]</code>
    /// </example>
    /// </remarks>
    public string RegexPattern { get; }

    /// <summary>
    /// Initializes a new instance of the BlurXField attribute.
    /// </summary>
    /// <param name="style">
    /// The masking behavior to apply.
    /// </param>
    /// <param name="maskLength">
    /// Number of characters to blur using <see cref="MaskChar"/>.
    /// Only applies to selected styles.
    /// </param>
    /// <param name="visibleCharCount">
    /// Number of characters to keep visible after masking.
    /// </param>
    /// <param name="maskChar">
    /// Character used for masking. Defaults to '*'.
    /// </param>
    /// <param name="regexPattern">
    /// Regular expression used when <see cref="Style"/> is set to <see cref="BlurStyle.Regex"/>.
    /// </param>
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

        Validate();
    }


    private void Validate()
    {
        // Regex style MUST have pattern
        if (Style == BlurStyle.Regex && string.IsNullOrWhiteSpace(RegexPattern))
        {
            throw new ArgumentException(
                "regexPattern is required when using BlurStyle.Regex");
        }

        // Validate regex is actually valid
        if (!string.IsNullOrWhiteSpace(RegexPattern))
        {
            try
            {
                _ = System.Text.RegularExpressions.Regex.IsMatch("", RegexPattern);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(
                    $"Invalid regex pattern: '{RegexPattern}'",
                    ex);
            }
        }
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
