namespace FontSizeCalculator.Typography;

public static class TypographyExtensions
{
    extension(Label label)
    {
        public void BindResponsiveFont(TextConfig cfg, VisualElement container)
        {
            ResponsiveBinder.Bind(label, cfg, container, ApplyFit);

            // Apply immediately in case container already has a size
            // ApplyResponsiveFont(label, cfg, container);
            //
            // container.SizeChanged += (_, _) =>
            // {
            //     if (container.Width <= 0 || container.Height <= 0)
            //     {
            //         return;
            //     }
            //
            //     ApplyFit(label, cfg, container.Width, container.Height);
            // };
        }

        public void ApplyResponsiveFont(TextConfig cfg, VisualElement container)
        {
            ApplyFit(label, cfg, container.Width, container.Height);
        }
    }

    private static void ApplyFit(Label label, TextConfig cfg, double width, double height)
    {
        // Resolve base size using your device/base/container method
        var baseSize = cfg.Role switch
        {
            TextRole.Paragraph or TextRole.BulletList or TextRole.NumberedList => TypographyScale.GetFontSize(cfg.Size, width, height),
            TextRole.Heading => TypographyScale.GetFontSize(TextRole.Heading),
            TextRole.Quote => TypographyScale.GetFontSize(TextRole.Quote),
            TextRole.QuoteAttribution => TypographyScale.GetFontSize(TextRole.QuoteAttribution),
            _ => 16.0
        };

        // Assign requested font family first
        label.FontFamily = cfg.FontFamily;

        // Compute attributes
        var attrs = FontAttributes.None;
        if (cfg.Styles.HasFlag(InlineStyle.Bold) || cfg.Role is TextRole.Heading)
        {
            attrs |= FontAttributes.Bold;
        }
        if (cfg.Styles.HasFlag(InlineStyle.Italic))
        {
            attrs |= FontAttributes.Italic;
        }
        label.FontAttributes = attrs;

        // Handle strikethrough
        label.TextDecorations = cfg.Styles.HasFlag(InlineStyle.Strikethrough)
            ? TextDecorations.Strikethrough
            : TextDecorations.None;
        
        // "Code" style → monospace font if available
        if (cfg.Styles.HasFlag(InlineStyle.Code))
        {
            // Use a common monospace if present; otherwise leave default
            label.FontFamily = "Courier New";
        }

        // Special handling for Bold on iOS/MacCatalyst with custom font families
        if (attrs == FontAttributes.Bold)
        {
            // check if ios or macos
            if (DeviceInfo.Platform == DevicePlatform.iOS || DeviceInfo.Platform == DevicePlatform.MacCatalyst)
            {
                label.FontFamily = $"{cfg.FontFamily}-Bold";
            }
        }
        
        // Alignment
        label.HorizontalTextAlignment = TextManager.ToMauiAlignment(cfg.Alignment);
        
        // Font Size
        label.FontSize = TypographyScale.FitFontSizeToBounds(
            label.Text,
            baseSize,
            width - 24, // padding
            height / 3, // row budget
            fontSize =>
            {
                label.FontSize = fontSize;
                return label.Measure(double.PositiveInfinity, double.PositiveInfinity);
            }
        );

        // Line Height
        label.LineHeight = LineHeightManager.GetLineHeightMultiplier(cfg.Role, cfg.Size);
    }
}