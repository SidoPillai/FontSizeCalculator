namespace FontSizeCalculator.Typography;

public sealed class TextConfig(
    TextRole role,
    TextSize size,
    string fontFamily,
    TextAlignment alignment,
    InlineStyle styles)
{
    public string FontFamily { get; } = fontFamily;
    public TextRole Role { get; } = role;
    public TextSize Size { get; } = size;
    public TextAlignment Alignment { get; } = alignment;
    public InlineStyle Styles { get; } = styles;
}