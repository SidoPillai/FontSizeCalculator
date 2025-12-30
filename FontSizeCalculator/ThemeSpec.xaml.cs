using FontSizeCalculator.Typography;
using Microsoft.Maui.Controls.Shapes;

namespace FontSizeCalculator;

public partial class ThemeSpec : ContentPage
{
    private readonly TextManager _manager = new();

    // Debounce state
    private bool _applyPending;
    private DateTime _lastApplyRequest;
    
    public ThemeSpec()
    {
        InitializeComponent();
        
        FontFamilyPicker.SelectedIndex = 0; // AvenirNextWorld
        TextTypePicker.SelectedIndex = 0; // Paragraph
        AlignmentPicker.SelectedIndex = 0; // start

        // Hook pickers to debounced apply
        FontFamilyPicker.SelectedIndexChanged += OnPickerChanged;
        TextTypePicker.SelectedIndexChanged += OnPickerChanged;
        AlignmentPicker.SelectedIndexChanged += OnPickerChanged;

        // Hook switches to debounced apply
        BoldSwitch.Toggled += OnToggleChanged;
        ItalicSwitch.Toggled += OnToggleChanged;
        StrikeSwitch.Toggled += OnToggleChanged;

        
        var title = new Label
        {
            Text = "Card Title", 
            FontAttributes = FontAttributes.Bold, 
            LineHeight = 1.1,
            MaxLines = 1,
        };
        var body = new Label
        {
            Text = "Body copy adapts to the card size.",
            LineHeight = 1.3,
            MaxLines = 1,
        };
        var caption = new Label
        {
            Text = "Caption",
            LineHeight = 1.1,
            MaxLines = 1,
        };
        
        // Add a border to visualize the card area
        var card = new Grid
        {
            Padding = 12,
            RowDefinitions =
            {
                new RowDefinition(GridLength.Star),
                new RowDefinition(GridLength.Star),
                new RowDefinition(GridLength.Star),
            }
        };
        card.Add(title, 0, 0);
        card.Add(body, 0, 1);
        card.Add(caption, 0, 2);
        
        var cardFrame = new Border()
        {
            WidthRequest = 240,
            HeightRequest = 120,
            Padding = 0,
            StrokeThickness = 1,
            Stroke = new SolidColorBrush(Color.FromArgb("#FFD3D3D3")), // light grey
            StrokeShape = new RoundRectangle { CornerRadius = 8 },
            Content = card,
        };
        
        var titleConfig = _manager.Create("AvenirNextWorld", TextRole.Paragraph, TextSize.XXLarge, TextAlignment.Start);
        title.BindResponsiveFont(titleConfig, cardFrame);
        var bodyConfig = _manager.Create("AvenirNextWorld", TextRole.Paragraph, TextSize.XLarge, TextAlignment.Center);
        body.BindResponsiveFont(bodyConfig, cardFrame);
        var captionConfig = _manager.Create("AvenirNextWorld", TextRole.Paragraph, TextSize.Large, TextAlignment.End);
        caption.BindResponsiveFont(captionConfig, cardFrame);
        
        Layout.Children.Add(cardFrame);
    }

    private void OnPickerChanged(object sender, EventArgs e) => DebouncedApplyAll(100);
    private void OnToggleChanged(object sender, ToggledEventArgs e) => DebouncedApplyAll(100);
    
    private void ApplyAll()
    {
        var fontFamily = FontFamilyPicker.SelectedItem as string ?? "AvenirNextWorld";
        var type = TextTypePicker.SelectedItem as string ?? "Paragraph";
        var align = AlignmentPicker.SelectedItem as string ?? "start";

        var styles = InlineStyle.None;
        if (BoldSwitch.IsToggled) styles |= InlineStyle.Bold;
        if (ItalicSwitch.IsToggled) styles |= InlineStyle.Italic;
        if (StrikeSwitch.IsToggled) styles |= InlineStyle.Strikethrough;
        
        var headingConfig = _manager.Create(fontFamily, TextRole.Heading, TextSize.Default, TextManager.ParseAlignment(align));
        var quoteConfig = _manager.Create(fontFamily, TextRole.Quote, TextSize.Default, TextManager.ParseAlignment(align));
        var attributionConfig = _manager.Create(fontFamily, TextRole.QuoteAttribution, TextSize.Default, TextManager.ParseAlignment(align));
        
        // Force re-apply using current container size
        HeadingLabel.ApplyResponsiveFont(headingConfig, Layout);
        QuoteLabel.ApplyResponsiveFont(quoteConfig, Layout);
        AttributionLabel.ApplyResponsiveFont(attributionConfig, Layout);
        
        var roleType = TextManager.ParseRoleType(type);
        Largest.ApplyResponsiveFont(_manager.Create(fontFamily, roleType, TextSize.XXLarge, TextManager.ParseAlignment(align), styles), Layout);
        ExtraLarge.ApplyResponsiveFont(_manager.Create(fontFamily, roleType, TextSize.XLarge, TextManager.ParseAlignment(align), styles), Layout);
        Large.ApplyResponsiveFont(_manager.Create(fontFamily, roleType, TextSize.Large, TextManager.ParseAlignment(align), styles), Layout);
        Medium.ApplyResponsiveFont(_manager.Create(fontFamily, roleType, TextSize.Medium, TextManager.ParseAlignment(align), styles), Layout);
        Small.ApplyResponsiveFont(_manager.Create(fontFamily, roleType, TextSize.Small, TextManager.ParseAlignment(align), styles), Layout);
    }
    
    protected override void OnAppearing()
    {
        base.OnAppearing();
        // Initial debounced apply to ensure containers are fully laid out before reapplying fonts
        DebouncedApplyAll(80);
    }
    
    private void DebouncedApplyAll(int delayMs = 80)
    {
        _lastApplyRequest = DateTime.UtcNow;

        if (_applyPending) return;
        _applyPending = true;

        Device.StartTimer(TimeSpan.FromMilliseconds(delayMs), () =>
        {
            // If another change happened within the delay window, wait one more interval
            var elapsed = DateTime.UtcNow - _lastApplyRequest;
            if (elapsed.TotalMilliseconds < delayMs)
            {
                return true; // continue the timer
            }

            _applyPending = false;
            ApplyAll();
            return false; // stop the timer
        });
    }

}