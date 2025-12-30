using System.Diagnostics;

namespace FontSizeCalculator;

public partial class MainPage : ContentPage
{
    // TypographyService typographyService;

    int count = 0;

    public MainPage()
    {
        InitializeComponent();
        // typographyService = MauiProgram.CreateMauiApp().Services.GetService<TypographyService>();
        SizeChanged += (_, __) =>
        {
            if (Width > 0)
            {
                // EvaluateText(Width);
            }
        };

    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

        if (width <= 0)
        {
            return;
        }
    }
    
    // private void AnySizeChanged(object sender, EventArgs e)
    // {
    //     if (sender is Label label)
    //     {
    //         double minFontSize = 1;
    //         double maxFontSize = 100;
    //         double tolerance = 0.1;
    //         double targetHeight = label.Height * 0.9;
    //         double targetWidth = label.Width * 0.8; 
    //
    //         while (minFontSize < maxFontSize)
    //         {
    //             double midFontSize = (minFontSize + maxFontSize) / 2;
    //             label.FontSize = midFontSize;
    //             Size textSize = label.Measure(double.PositiveInfinity, double.PositiveInfinity);
    //             bool isHeightWithinTolerance = Math.Abs(textSize.Height - targetHeight) <= targetHeight * tolerance;
    //             bool isWidthWithinLabel = textSize.Width <= targetWidth;
    //
    //             if (isHeightWithinTolerance && isWidthWithinLabel)
    //             {
    //                 break;
    //             }
    //             else if (textSize.Height > targetHeight || textSize.Width > targetWidth)
    //             {
    //                 maxFontSize = midFontSize - 0.1;
    //             }
    //             else
    //             {
    //                 minFontSize = midFontSize + 0.1; 
    //             }
    //         }
    //         label.FontSize = (minFontSize + maxFontSize) / 2;
    //     }
    // }
    
    private void OnFontFamilyChanged(object sender, EventArgs e)
    {
        // var selectedFamily = FontFamilyPicker.SelectedItem as string;
        // if (string.IsNullOrEmpty(selectedFamily))
        // {
        //     return;
        // }

        // HeadingLargeLabel.FontFamily = selectedFamily;
        // HeadingMediumLabel.FontFamily = selectedFamily;
        // HeadingSmallLabel.FontFamily = selectedFamily;
        // SubtitleLargeLabel.FontFamily = selectedFamily;
        // SubtitleSmallLabel.FontFamily = selectedFamily;
        // BodyLargeLabel.FontFamily = selectedFamily;
        // BodySmallLabel.FontFamily = selectedFamily;
        // LinkLargeLabel.FontFamily = selectedFamily;
        // LinkSmallLabel.FontFamily = selectedFamily;
        // CaptionLabel.FontFamily = selectedFamily;
        // LabelLargeLabel.FontFamily = selectedFamily;
        // LabelSmallLabel.FontFamily = selectedFamily;
        //
        //
        //     H1.FontFamily = selectedFamily;
        //
        //     var labelWidth = H1.DesiredSize.Width;
        //     var measurement = H1.Measure(double.PositiveInfinity, double.PositiveInfinity);
        //     int totalChars = H1.Text.Length;
        //     int left = 0, right = totalChars, visibleChars = 0;
        //     double availableWidth = H1.Width;
        //
        //     while (left <= right)
        //     {
        //         int mid = (left + right) / 2;
        //         var subText = H1.Text.Substring(0, mid);
        //         var tempLabel = new Label
        //         {
        //             Text = subText,
        //             FontSize = H1.FontSize,
        //             FontFamily = H1.FontFamily
        //         };
        //         double subTextWidth = tempLabel.Measure(double.PositiveInfinity, double.PositiveInfinity).Width;
        //
        //         if (subTextWidth <= availableWidth)
        //         {
        //             visibleChars = mid;
        //             left = mid + 1;
        //         }
        //         else
        //         {
        //             right = mid - 1;
        //         }
        //     }
        //     
        //     var style = typographyService.Resolve(type: TextType.H1, TextSize.Medium, AppFont.AvenirNextWorld);
        //
        //     // MeasuredSizeLabel.Text = $"Font Family: {style.FontFamily} FontSize: {style.FontSize} LineHeight: {style.LineHeight}" +
        //     //                          $"Font Family: {style.FontFamily} FontSize: {style.FontSize} LineHeight: {style.LineHeight}";
        //
        //     // visibleChars now holds the maximum number of characters that fit without truncation
        //
        //
        //     var noOfCharsIn1200 = (int)Math.Round((visibleChars / labelWidth) * 1200);
        //     var noOfLinesIn675 = (int)Math.Round((3 / H1.DesiredSize.Height) * 675);
        //
        //     var ratio = visibleChars / totalChars;
        //     var newFontSize = H1.FontSize * ratio;
        //     
        //     MeasuredSizeLabel.Text = $"Measured : Width: {measurement.Width:F2} Height: {measurement.Height:F2} \n" +
        //                              $"Visible Chars: {visibleChars} \n" +
        //                              $"Total Chars: {totalChars} \n" +
        //                              $"Number of Chars in 1200px: {noOfCharsIn1200} \n" +
        //                              $"Number of Lines in 675px: {noOfLinesIn675}\n" +
        //                              $"Ratio: {ratio:F2} \n" +
        //                              $"New Font Size to fit: {newFontSize:F2}";
        //
        //
        //     // double targetHeight = TestLabel.Height * 0.9;
        //     // double targetWidth = TestLabel.Width * 0.8; 
        //     // double tolerance = 0.1;
        //
        //     // Size textSize = TestLabel.Measure(double.PositiveInfinity, double.PositiveInfinity);
        //     // bool isHeightWithinTolerance = Math.Abs(textSize.Height - targetHeight) <= targetHeight * tolerance;
        //     // bool isWidthWithinLabel = textSize.Width <= targetWidth;
        //     //
        //     // // Just right
        //     // if (isHeightWithinTolerance && isWidthWithinLabel)
        //     // {
        //     //     MeasuredSizeLabel.Text = "Just right!";
        //     //     return;
        //     // }
        //     //
        //     // // Too big to fit
        //     // if (textSize.Height > targetHeight || textSize.Width > targetWidth)
        //     // {
        //     //     MeasuredSizeLabel.Text = "Too big!";
        //     //     return;
        //     // }
        //     //
        //     // // Too small, can be bigger
        //     // if (textSize.Height < targetHeight && textSize.Width < targetWidth)
        //     // {
        //     //     MeasuredSizeLabel.Text = "Too small!";
        //     //     return;
        //     // }
        //
        //     // Console.WriteLine($"Desired Size : {TestLabel.DesiredSize.Width:F2} x {TestLabel.DesiredSize.Height:F2}");
        //
        //     // var textMeasurer = new SimpleTextMeasurer();
        //     // var textProps = new TextProperties(TestLabel.Text, (float)TestLabel.FontSize);
        //     // var fontProps = new FontProperties(new FontFamily(selectedFamily));
        //     // var measurement = textMeasurer.MeasureText(textProps, fontProps);
        //
        //     // double minFontSize = 1;
        //     // double maxFontSize = 100;
        //     // double tolerance = 0.1;
        //     // double targetHeight = TestLabel.Height * 0.9;
        //     // double targetWidth = TestLabel.Width * 0.8; 
        //     //
        //     // while (minFontSize < maxFontSize)
        //     // {
        //     //     var midFontSize = (minFontSize + maxFontSize) / 2;
        //     //     TestLabel.FontSize = midFontSize;
        //     //     var textSize = TestLabel.Measure(double.PositiveInfinity, double.PositiveInfinity);
        //     //     bool isHeightWithinTolerance = Math.Abs(textSize.Height - targetHeight) <= targetHeight * tolerance;
        //     //     bool isWidthWithinLabel = textSize.Width <= targetWidth;
        //     //
        //     //     if (isHeightWithinTolerance && isWidthWithinLabel)
        //     //     {
        //     //         break;
        //     //     }
        //     //     else if (textSize.Height > targetHeight || textSize.Width > targetWidth)
        //     //     {
        //     //         maxFontSize = midFontSize - 0.1;
        //     //     }
        //     //     else
        //     //     {
        //     //         minFontSize = midFontSize + 0.1; 
        //     //     }
        //     // }
        //     //
        //     // var avgFontSize = (minFontSize + maxFontSize) / 2;
        //     //
        //
        //     // MeasuredSizeLabel.Text = $"Measured size: {measurement.Size.Width:F2} x {measurement.Size.Height:F2} \n" +
        //     // MeasuredSizeLabel.Text = $"Font Size : {TestLabel.FontSize:F2} \n" +
        //     //                          $"Desired Size : {TestLabel.DesiredSize.Width:F2} x {TestLabel.DesiredSize.Height:F2} \n" +
        //     //                          $"Device Density : {DeviceDisplay.MainDisplayInfo.Density:F2}\n" +
        //     //                          // $"Baseline Offset: {measurement.BaselineOffset:F2}\n" +
        //     //                          $"Avg Font Size to fit: {avgFontSize:F2}";
    }
    //
    // private void OnFontSizeChanged(object sender, ValueChangedEventArgs e)
    // {
    //     var newSize = e.NewValue;
    //     H1.FontSize = newSize;
    //
    //     // Optionally re-measure if needed
    //     OnFontFamilyChanged(sender, EventArgs.Empty);
    // }
    
    // private void AnySizeChanged(object sender, EventArgs e)
    // {
    //     if (sender is Label label)
    //     {
    //         double minFontSize = 1;
    //         double maxFontSize = 100;
    //         double tolerance = 0.1;
    //         double targetHeight = label.Height * 0.9;
    //         double targetWidth = label.Width * 0.8; 
    //
    //         while (minFontSize < maxFontSize)
    //         {
    //             var midFontSize = (minFontSize + maxFontSize) / 2;
    //             label.FontSize = midFontSize;
    //             var textSize = label.Measure(double.PositiveInfinity, double.PositiveInfinity);
    //             bool isHeightWithinTolerance = Math.Abs(textSize.Height - targetHeight) <= targetHeight * tolerance;
    //             bool isWidthWithinLabel = textSize.Width <= targetWidth;
    //
    //             if (isHeightWithinTolerance && isWidthWithinLabel)
    //             {
    //                 break;
    //             }
    //             else if (textSize.Height > targetHeight || textSize.Width > targetWidth)
    //             {
    //                 maxFontSize = midFontSize - 0.1;
    //             }
    //             else
    //             {
    //                 minFontSize = midFontSize + 0.1; 
    //             }
    //         }
    //         // label.FontSize = (minFontSize + maxFontSize) / 2;
    //     }
    // }
}