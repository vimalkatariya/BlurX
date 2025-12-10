namespace Mask.BlurX;

/// <summary>
/// Defines all supported masking strategies for BlurX.
/// </summary>
public enum BlurStyle
{
    /// <summary>
    /// Automatic ratio-based masking.
    /// Defaults to 20% visible prefix, 60% masked content,
    /// and 20% visible suffix if no custom values are supplied.
    ///
    /// Example:
    /// "SensitiveData" → "Se******ta"
    /// </summary>
    Default,

    /// <summary>
    /// Fully masks the value except any explicitly configured 
    /// visible trailing characters.
    ///
    /// Example:
    /// "Secret" → "******"
    /// </summary>
    Full,

    /// <summary>
    /// Keeps the first N characters visible and masks the remainder.
    ///
    /// Example:
    /// "Secret" → "Se****"
    /// </summary>
    Prefix,

    /// <summary>
    /// Keeps the last N characters visible and masks all preceding characters.
    ///
    /// Example:
    /// "Secret" → "****et"
    /// </summary>
    Suffix,

    /// <summary>
    /// Keeps characters visible at both ends while masking a centered section.
    ///
    /// Example:
    /// "Sensitive" → "Se****ve"
    /// </summary>
    Middle,

    /// <summary>
    /// Masks only the local part of an email address (before the '@' symbol),
    /// leaving the domain fully visible.
    ///
    /// Example:
    /// "john.doe@example.com" → "jo****@example.com"
    /// </summary>
    Email,

    /// <summary>
    /// Masks characters based on a custom regular expression.
    /// Requires the regexPattern parameter in <see cref="BlurXFieldAttribute"/>.
    ///
    /// Example:
    /// Pattern: "\d"
    /// "Card 1234" → "Card ****"
    /// </summary>
    Regex
}