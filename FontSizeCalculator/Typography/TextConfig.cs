namespace FontSizeCalculator;

public sealed class TextConfig(string fontFamily, TextType type, TextSize size, TextAlignment alignment, InlineStyle styles, IReadOnlyList<string>? contentItems)
{
    public string FontFamily { get; } = fontFamily;
    public TextType Type { get; } = type;

    public TextRole Role => Type switch
    {
        TextType.Paragraph => TextRole.Paragraph,
        TextType.Heading => TextRole.Heading,
        TextType.BulletList => TextRole.BulletList,
        TextType.NumberedList => TextRole.NumberedList,
        TextType.Quote => TextRole.Quote,
        _ => TextRole.QuoteAttribution
    };
    public TextSize Size { get; } = size;
    public TextAlignment Alignment { get; } = alignment;
    public InlineStyle Styles { get; } = styles;
    public IReadOnlyList<string> ContentItems { get; } = contentItems ?? Array.Empty<string>();
}
