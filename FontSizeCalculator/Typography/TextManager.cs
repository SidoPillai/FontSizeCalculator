namespace FontSizeCalculator.Typography;

public sealed class TextManager
{
    public TextConfig Create(
        string fontFamily, 
        TextRole role,
        TextSize size,
        TextAlignment alignment = TextAlignment.Start,
        InlineStyle styles = InlineStyle.None)
    {
        return new TextConfig(role, size, fontFamily, alignment, styles);
    }
    
    // Helpers
    public static TextAlignment ParseAlignment(string? align) => (align ?? "").ToLowerInvariant() switch
    {
        "" => TextAlignment.Start,
        "start" or "left" => TextAlignment.Start,
        "center" => TextAlignment.Center,
        "end" or "right" => TextAlignment.End,
        _ => TextAlignment.Start
    };

    public static TextRole ParseRoleType(string type) => (type ?? "").ToLowerInvariant() switch
    {
        "paragraph" => TextRole.Paragraph,
        "heading" => TextRole.Heading,
        "bullet-list" => TextRole.BulletList,
        "list" => TextRole.BulletList,
        "numbered-list" => TextRole.NumberedList,
        "quote" => TextRole.Quote,
        _ => TextRole.QuoteAttribution
    };
    
    public static Microsoft.Maui.TextAlignment ToMauiAlignment(TextAlignment alignment) => alignment switch
    {
        TextAlignment.Start => Microsoft.Maui.TextAlignment.Start,
        TextAlignment.Center => Microsoft.Maui.TextAlignment.Center,
        TextAlignment.End => Microsoft.Maui.TextAlignment.End,
        _ => Microsoft.Maui.TextAlignment.Start
    };
}