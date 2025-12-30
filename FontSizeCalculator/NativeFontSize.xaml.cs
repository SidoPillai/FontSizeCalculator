#if __IOS__ || __MACCATALYST__
	using UIKit;
#endif


using Microsoft.Maui.Platform;
#if __ANDROID__
using Android.Util;
#endif

#if __WINDOWS__
#endif

namespace FontSizeCalculator;

public partial class NativeFontSize : ContentPage
{
    public NativeFontSize()
    {
        InitializeComponent();
        
        var verticalStackLayout = new StackLayout()
        {
            Spacing = 10,
            Padding = 20,
        };
        
        Microsoft.Maui.Handlers.LabelHandler.Mapper.AppendToMapping("CustomLabelHandler", (handler, view) =>
		{
			if (view is CustomLabel)
			{
#if __IOS__ || __MACCATALYST__
				var newValue = view.Text switch
				{
					"PreferredBody" => UIFont.PreferredBody,
					"PreferredCallout" => UIFont.PreferredCallout,
					"PreferredCaption1" => UIFont.PreferredCaption1,
					"PreferredCaption2" => UIFont.PreferredCaption2,
					"PreferredFootnote" => UIFont.PreferredFootnote,
					"PreferredHeadline" => UIFont.PreferredHeadline,
					"PreferredSubheadline" => UIFont.PreferredSubheadline,
					"PreferredTitle1" => UIFont.PreferredTitle1,
					"PreferredTitle2" => UIFont.PreferredTitle2,
					"PreferredTitle3" => UIFont.PreferredTitle3,
					_ => null,
				};

				if (newValue != null)
				{
					handler.PlatformView.Font = newValue;
				}
#elif __ANDROID__
				var newValue = view.Text switch
				{
					"Body1" => Resource.Style.TextAppearance_AppCompat_Body1,
					"Body2" => Resource.Style.TextAppearance_AppCompat_Body2,
					"Button" => Resource.Style.TextAppearance_AppCompat_Button,
					"Caption" => Resource.Style.TextAppearance_AppCompat_Caption,
					"Display1" => Resource.Style.TextAppearance_AppCompat_Display1,
					"Display2" => Resource.Style.TextAppearance_AppCompat_Display2,
					"Display3" => Resource.Style.TextAppearance_AppCompat_Display3,
					"Display4" => Resource.Style.TextAppearance_AppCompat_Display4,
					"Headline" => Resource.Style.TextAppearance_AppCompat_Headline,
					"Inverse" => Resource.Style.TextAppearance_AppCompat_Inverse,
					"Large" => Resource.Style.TextAppearance_AppCompat_Large,
					"Large_Inverse" => Resource.Style.TextAppearance_AppCompat_Large_Inverse,
					"Medium" => Resource.Style.TextAppearance_AppCompat_Medium,
					"Medium_Inverse" => Resource.Style.TextAppearance_AppCompat_Medium_Inverse,
					"Small" => Resource.Style.TextAppearance_AppCompat_Small,
					"Small_Inverse" => Resource.Style.TextAppearance_AppCompat_Small_Inverse,
					"Subhead" => Resource.Style.TextAppearance_AppCompat_Subhead,
					"Subhead_Inverse" => Resource.Style.TextAppearance_AppCompat_Subhead_Inverse,
					"Title" => Resource.Style.TextAppearance_AppCompat_Title,
					"Title_Inverse" => Resource.Style.TextAppearance_AppCompat_Title_Inverse,
					"Tooltip" => Resource.Style.TextAppearance_AppCompat_Tooltip,
					_ => 0,
				};

				if (newValue > 0)
				{
					handler.PlatformView.SetTextAppearance(newValue);


					// Via https://github.com/xamarin/Xamarin.Forms/blob/b2e8af675bdc2db562b3071e7da346c371dc4cac/Xamarin.Forms.Platform.Android/Forms.cs#L919
					try
					{
						using (var typedValue = new TypedValue())
						{
							if (handler.PlatformView.Context.Theme.ResolveAttribute(newValue, typedValue, true)) // doesnt seem to get past this
							{
								var textSizeAttr = new int [] { Android.Resource.Attribute.TextSize };
								const int indexOfAttrTextSize = 0;
								using (var array = handler.PlatformView.Context.ObtainStyledAttributes(typedValue.Data, textSizeAttr))
								{
									var fontSizePx = handler.PlatformView.Context.FromPixels(array.GetDimensionPixelSize(indexOfAttrTextSize, -1));
									handler.PlatformView.SetTextSize(ComplexUnitType.Px, (float)fontSizePx);
								}
							}
						}
					}
					catch (Exception err)
					{
						System.Diagnostics.Debug.WriteLine($"Error retrieving text appearance: {err.Message}");
					}
				}
#elif __WINDOWS__

#endif
			}
		});

#if __IOS__ || __MACCATALYST__
		verticalStackLayout.Children.Add(new CustomLabel() { Text = "PreferredBody" });
		verticalStackLayout.Children.Add(new CustomLabel() { Text = "PreferredCallout" });
		verticalStackLayout.Children.Add(new CustomLabel() { Text = "PreferredCaption1" });
		verticalStackLayout.Children.Add(new CustomLabel() { Text = "PreferredCaption2" });
		verticalStackLayout.Children.Add(new CustomLabel() { Text = "PreferredFootnote" });
		verticalStackLayout.Children.Add(new CustomLabel() { Text = "PreferredHeadline" });
		verticalStackLayout.Children.Add(new CustomLabel() { Text = "PreferredSubheadline" });
		verticalStackLayout.Children.Add(new CustomLabel() { Text = "PreferredTitle1" });
		verticalStackLayout.Children.Add(new CustomLabel() { Text = "PreferredTitle2" });
		verticalStackLayout.Children.Add(new CustomLabel() { Text = "PreferredTitle3" });
#elif __ANDROID__
		verticalStackLayout.Children.Add(new CustomLabel() { Text = "Body1" });
		verticalStackLayout.Children.Add(new CustomLabel() { Text = "Body2" });
		verticalStackLayout.Children.Add(new CustomLabel() { Text = "Button" });
		verticalStackLayout.Children.Add(new CustomLabel() { Text = "Caption" });
		verticalStackLayout.Children.Add(new CustomLabel() { Text = "Display1" });
		verticalStackLayout.Children.Add(new CustomLabel() { Text = "Display2" });
		verticalStackLayout.Children.Add(new CustomLabel() { Text = "Display3" });
		verticalStackLayout.Children.Add(new CustomLabel() { Text = "Display4" });
		verticalStackLayout.Children.Add(new CustomLabel() { Text = "Headline" });
		verticalStackLayout.Children.Add(new CustomLabel() { Text = "Inverse" });
		verticalStackLayout.Children.Add(new CustomLabel() { Text = "Large" });
		verticalStackLayout.Children.Add(new CustomLabel() { Text = "Large_Inverse" });
		verticalStackLayout.Children.Add(new CustomLabel() { Text = "Medium" });
		verticalStackLayout.Children.Add(new CustomLabel() { Text = "Medium_Inverse" });
		verticalStackLayout.Children.Add(new CustomLabel() { Text = "Small" });
		verticalStackLayout.Children.Add(new CustomLabel() { Text = "Small_Inverse" });
		verticalStackLayout.Children.Add(new CustomLabel() { Text = "Subhead" });
		verticalStackLayout.Children.Add(new CustomLabel() { Text = "Subhead_Inverse" });
		verticalStackLayout.Children.Add(new CustomLabel() { Text = "Title" });
		verticalStackLayout.Children.Add(new CustomLabel() { Text = "Title_Inverse" });
		verticalStackLayout.Children.Add(new CustomLabel() { Text = "Tooltip" });
#elif __WINDOWS__

#endif

		Content = new ScrollView()
		{
			Content = verticalStackLayout,
		};
    }
}