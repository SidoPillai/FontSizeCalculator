namespace FontSizeCalculator;

// FontSize = DeviceBase × RoleMultiplier × min(widthFit, heightFit) × Accessibility
public static class TypographyScale
{
    // Device base scales (absolute pixel sizes for roles)
    private static readonly (double xxlarge, double xlarge, double large, double medium, double small) PhoneScale = (36, 20, 14, 12, 10);
    private static readonly (double xxlarge, double xlarge, double large, double medium, double small) TabletScale = (58, 32, 24, 20, 16);
    private static readonly (double xxlarge, double xlarge, double large, double medium, double small) DesktopScale = (90, 52, 36, 30, 24);
    
    // Reference viewports per device idiom (in dp)
    private static readonly (double width, double height) PhoneViewport = (360, 640);
    private static readonly (double width, double height) TabletViewport = (768, 1024);
    private static readonly (double width, double height) DesktopViewport = (1280, 800);

    // Role multipliers derived from each device’s base scale
    // We normalize all roles against the device’s Medium (body) size to create consistent multipliers.
    // That way: final = deviceMediumBase × roleMultiplier × containerScale.
    private static (double mediumBase, SizeMultipliers multipliers, (double w, double h) viewport) GetDeviceBase()
    {
        var idiom = DeviceInfo.Current.Idiom;
        var scale = DesktopScale;
        var vp = DesktopViewport;

        if (idiom == DeviceIdiom.Phone)
        {
            scale = PhoneScale;
            vp = PhoneViewport;
        }
        else if (idiom == DeviceIdiom.Tablet)
        {
            scale = TabletScale;
            vp = TabletViewport;
        }

        // Normalize against Medium to get clean role multipliers
        var medium = scale.medium;
        var multipliers = new SizeMultipliers(
            xxlarge: SafeDivide(scale.xxlarge, medium),
            xlarge: SafeDivide(scale.xlarge, medium),
            large: SafeDivide(scale.large, medium),
            medium: 1.0,
            small: SafeDivide(scale.small, medium)
        );

        return (medium, multipliers, vp);
    }

    // Container scale clamp and weights
    private const double MinContainerScale = 0.6; // allow stronger shrink
    private const double MaxContainerScale = 2.0; // allow grow when there is room
    private const double WidthWeight = 0.7;
    private const double HeightWeight = 0.3;

    private static double SizeResponsiveness(TextSize size) => size switch
    {
        TextSize.XXLarge => 1.15,
        TextSize.XLarge  => 1.10,
        TextSize.Large   => 1.05,
        TextSize.Medium  => 1.00,
        TextSize.Small   => 0.90,
        _ => 1.0
    };
    
    
    // Accessibility multiplier (hook for platform user font scale if you use it)
    // For now return 1.0; you can integrate platform text scale here.
    private static double AccessibilityScale() => 1.0;
    
    public static double GetFontSize(TextRole role, TextSize size = TextSize.Default, double containerWidthDp = Double.MaxValue, double containerHeightDp = Double.MaxValue)
    {
        var deviceInfo = DeviceDisplay.MainDisplayInfo;
        var availableWidthDp = double.IsFinite(containerWidthDp) ? containerWidthDp : deviceInfo.Width / deviceInfo.Density;
        var availableHeightDp = double.IsFinite(containerHeightDp) ? containerHeightDp : deviceInfo.Height / deviceInfo.Density;

        switch (role)
        {
            case TextRole.Heading:
                return GetFontSizeForHeading(availableWidthDp, availableHeightDp);
            case TextRole.Paragraph:
                return GetFontSize(size, availableWidthDp, availableHeightDp);
            case TextRole.BulletList:
                return GetFontSize(size, availableWidthDp, availableHeightDp);
            case TextRole.NumberedList:
                return GetFontSize(size, availableWidthDp, availableHeightDp);
            case TextRole.Quote:
                return GetFontSizeForQuote(availableWidthDp, availableHeightDp);
            case TextRole.QuoteAttribution:
                return GetFontSizeForQuoteAttribution(availableWidthDp, availableHeightDp);
            default:
                return GetFontSize(size, availableWidthDp, availableHeightDp);
        }
    }

    private static double GetFontSizeForHeading(double containerWidthDp, double containerHeightDp)
    {
        var idiom = DeviceInfo.Current.Idiom;
        var baseSize = 44;
        if (idiom == DeviceIdiom.Phone)
        {
            baseSize = 18;
        }
        else if (idiom == DeviceIdiom.Tablet)
        {
            baseSize = 36;
        }
        
        return baseSize;
    }

    private static double GetFontSizeForQuote(double containerWidthDp, double containerHeightDp)
    {
        var idiom = DeviceInfo.Current.Idiom;
        var baseSize = 25;
        if (idiom == DeviceIdiom.Phone)
        {
            baseSize = 12;
        }
        else if (idiom == DeviceIdiom.Tablet)
        {
            baseSize = 21;
        }
        
        return baseSize;
    }

    private static double GetFontSizeForQuoteAttribution(double containerWidthDp, double containerHeightDp)
    {
        var idiom = DeviceInfo.Current.Idiom;
        var baseSize = 21;
        if (idiom == DeviceIdiom.Phone)
        {
            baseSize = 18;
        }
        else if (idiom == DeviceIdiom.Tablet)
        {
            baseSize = 10;
        }
        
        return baseSize;
    }
    
    // Public API: purely device-base + role multiplier + container-driven scale.
    // Use containerWidthDp/containerHeightDp in dp (View.Width/Height).
    public static double GetFontSize(TextSize size, double containerWidthDp, double containerHeightDp)
    {
        var (deviceMediumBase, multipliers, viewport) = GetDeviceBase();

        // 1) Base unit (device medium)
        var baseUnit = deviceMediumBase;

        // 2) Role multiplier
        var roleMultiplier = size switch
        {
            TextSize.XXLarge => multipliers.XXLarge,
            TextSize.XLarge => multipliers.XLarge,
            TextSize.Large => multipliers.Large,
            TextSize.Medium => multipliers.Medium,
            TextSize.Small => multipliers.Small,
            _ => multipliers.Medium
        };

        // 3) Container scale (vs device viewport), blended width/height and clamped
        var containerScale = ComputeContainerScale(containerWidthDp, containerHeightDp, viewport.w, viewport.h);

        // Optional: role responsiveness adjusts how strongly container scale applies per role
        containerScale = Math.Clamp(containerScale * SizeResponsiveness(size), MinContainerScale, MaxContainerScale);

        // 4) Accessibility
        var a11y = AccessibilityScale();

        // Final size
        return baseUnit * roleMultiplier * containerScale * a11y;
    }


    private static double ComputeContainerScale(
        double containerWidthDp,
        double containerHeightDp,
        double refWidthDp,
        double refHeightDp)
    {
        var w = Math.Max(1, containerWidthDp);
        var h = Math.Max(1, containerHeightDp);
        var rw = Math.Max(1, refWidthDp);
        var rh = Math.Max(1, refHeightDp);

        var widthRatio = w / rw;
        var heightRatio = h / rh;

        // When shrinking, obey the tightest constraint
        if (widthRatio < 1.0 || heightRatio < 1.0)
        {
            return Math.Clamp(
                Math.Min(widthRatio, heightRatio),
                MinContainerScale,
                1.0
            );
        }

        // When growing, allow blended expansion
        var blended =
            (widthRatio * WidthWeight) +
            (heightRatio * HeightWeight);

        return Math.Clamp(blended, 1.0, MaxContainerScale);
    }

    
    private static double SafeDivide(double a, double b) => b > 0 ? a / b : 1.0;

    private readonly struct SizeMultipliers(double xxlarge, double xlarge, double large, double medium, double small)
    {
        public double XXLarge { get; } = xxlarge;
        public double XLarge { get; } = xlarge;
        public double Large { get; } = large;
        public double Medium { get; } = medium;
        public double Small { get; } = small;
    }
    
    public static double FitFontSizeToBounds(
        string text,
        double initialFontSize,
        double maxWidth,
        double maxHeight,
        Func<double, Size> measure,
        double minFontSize = 6)
    {
        var size = initialFontSize;

        while (size > minFontSize)
        {
            var measured = measure(size);

            if (measured.Width <= maxWidth && measured.Height <= maxHeight)
            {
                break;
            }

            size *= 0.92; // smooth shrink step
        }

        return size;
    }
}