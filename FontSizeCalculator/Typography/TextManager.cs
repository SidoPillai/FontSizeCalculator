namespace FontSizeCalculator;

public sealed class TextManager
{
    private TextConfig Create(string fontFamily, string textType, string textSize, string? alignment = null, IEnumerable<string>? contentItems = null, InlineStyle styles = InlineStyle.None, string? attribution = null)
    {
        var type = ParseTextType(textType);
        var size = ParseTextSize(textSize);
        var align = ParseAlignment(alignment);
        var items = new List<string>();
        if (contentItems != null) items.AddRange(contentItems);
        return new TextConfig(fontFamily, type, size, align, styles, items);
    }

    public TextConfig CreateBlock(string fontFamily, string textType, string textSize, string content, string? alignment = null, InlineStyle styles = InlineStyle.None, string? attribution = null)
    {
        return Create(fontFamily, textType, textSize, alignment, new[] { content }, styles, attribution);
    }

    // Apply TextConfig to a MAUI Label
    public void ApplyToLabel(Label label, TextConfig cfg)
    {
        
        // Content
        label.Text = cfg.ContentItems.Count > 0 ? cfg.ContentItems[0] : string.Empty;

        // Size
        label.FontSize = TypographyScale.GetFontSize(cfg.Size, 50, 40);

        // Line Height
        label.LineHeight = LineHeightManager.GetLineHeightMultiplier(cfg.Role, cfg.Size);
        
        // Alignment
        label.HorizontalTextAlignment = ToMauiAlignment(cfg.Alignment);
        
        // Styles
        var attrs = FontAttributes.None;
        if (cfg.Styles.HasFlag(InlineStyle.Bold)) attrs |= FontAttributes.Bold;
        if (cfg.Styles.HasFlag(InlineStyle.Italic)) attrs |= FontAttributes.Italic;

        if (attrs == FontAttributes.Bold)
        {
            // check if ios or macos
            if (DeviceInfo.Platform == DevicePlatform.iOS || DeviceInfo.Platform == DevicePlatform.MacCatalyst)
            {
                label.FontFamily = $"{cfg.FontFamily}-Bold";
            }
            else
            {
                label.FontFamily = cfg.FontFamily;
            }
        }
        else
        {
            label.FontFamily = cfg.FontFamily;
        }
        
        label.FontAttributes = attrs;

        // Strikethrough (via TextDecorations)
        label.TextDecorations = cfg.Styles.HasFlag(InlineStyle.Strikethrough)
            ? TextDecorations.Strikethrough
            : TextDecorations.None;

        // "Code" style → monospace font if available
        if (cfg.Styles.HasFlag(InlineStyle.Code))
        {
            // Use a common monospace if present; otherwise leave default
            label.FontFamily = "Courier New";
        }

        // Heading weight/size tweak
        if (cfg.Type is TextType.Heading)
        {
            label.FontAttributes |= FontAttributes.Bold;
        }
    }

    // Helpers
    private static TextType ParseTextType(string type) => (type ?? "").ToLowerInvariant() switch
    {
        "paragraph" => TextType.Paragraph,
        "heading" => TextType.Heading,
        "bullet-list" => TextType.BulletList,
        "list" => TextType.BulletList,
        "numbered-list" => TextType.NumberedList,
        "quote" => TextType.Quote,
        _ => TextType.Paragraph
    };

    private static TextSize ParseTextSize(string size) => (size ?? "").ToLowerInvariant() switch
    {
        "small" => TextSize.Small,
        "medium" => TextSize.Medium,
        "large" => TextSize.Large,
        "xlarge" => TextSize.XLarge,
        "xxlarge" => TextSize.XXLarge,
        _ => TextSize.Medium
    };

    private static TextAlignment ParseAlignment(string? align) => (align ?? "").ToLowerInvariant() switch
    {
        "" => TextAlignment.Start,
        "start" or "left" => TextAlignment.Start,
        "center" => TextAlignment.Center,
        "end" or "right" => TextAlignment.End,
        _ => TextAlignment.Start
    };

    private Microsoft.Maui.TextAlignment ToMauiAlignment(TextAlignment alignment) => alignment switch
    {
        TextAlignment.Start => Microsoft.Maui.TextAlignment.Start,
        TextAlignment.Center => Microsoft.Maui.TextAlignment.Center,
        TextAlignment.End => Microsoft.Maui.TextAlignment.End,
        _ => Microsoft.Maui.TextAlignment.Start
    };
}