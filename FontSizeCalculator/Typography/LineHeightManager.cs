namespace FontSizeCalculator;

public static class LineHeightManager
{
    private const double Heading = 1.4;
    private const double Quote = 1.65;
    private const double QuoteAttribution = 1.65;

    private static double GetLineHeightMultiplier(TextSize size) => size switch
    {
        TextSize.Small => 1.65,
        TextSize.Medium => 1.4,
        TextSize.Large => 1.4,
        TextSize.XLarge => 1.2,
        TextSize.XXLarge => 1.2,
        _ => 1.3
    };
    
    public static double GetLineHeightMultiplier(TextRole role, TextSize size = TextSize.Default)
    {
        return role switch
        {
            TextRole.Heading => Heading,
            TextRole.Quote => Quote,
            TextRole.QuoteAttribution => QuoteAttribution,
            _ => GetLineHeightMultiplier(size)
        };
    }
}