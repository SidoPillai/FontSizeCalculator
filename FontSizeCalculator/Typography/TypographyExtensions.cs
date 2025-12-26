namespace FontSizeCalculator;

public static class TypographyExtensions
{
    public static void BindResponsiveFont(this Label label, TextRole role, TextSize size, VisualElement container)
    {
        container.SizeChanged += (_, _) =>
        {
            if (container.Width <= 0 || container.Height <= 0)
            {
                return;
            }

            ApplyFit(label, role, size, container.Width, container.Height);
        };
    }

    private static void ApplyFit(Label label, TextRole role, TextSize size, double width, double height)
    {
        var baseSize = role switch
        {
            TextRole.Paragraph or TextRole.BulletList or TextRole.NumberedList => TypographyScale.GetFontSize(size, width, height),
            TextRole.Heading => TypographyScale.GetFontSize(TextRole.Heading),
            TextRole.Quote => TypographyScale.GetFontSize(TextRole.Quote),
            TextRole.QuoteAttribution => TypographyScale.GetFontSize(TextRole.QuoteAttribution),
            _ => 16.0
        };

        label.FontSize = TypographyScale.FitFontSizeToBounds(
            label.Text,
            baseSize,
            width - 24,   // padding
            height / 3,   // row budget
            fontSize =>
            {
                label.FontSize = fontSize;
                return label.Measure(double.PositiveInfinity, double.PositiveInfinity);
            }
        );
    }
}