namespace FontSizeCalculator.Typography;

[Flags]
public enum InlineStyle
{
    None = 0,
    Bold = 1 << 0,
    Italic = 1 << 1,
    Subscript = 1 << 2,      // not directly supported in MAUI Label; kept for parity
    Superscript = 1 << 3,    // not directly supported in MAUI Label; kept for parity
    Code = 1 << 4,           // demo only; maps to monospace font if available
    Strikethrough = 1 << 5
}