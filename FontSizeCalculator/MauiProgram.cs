using Microsoft.Extensions.Logging;

namespace FontSizeCalculator;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("AvenirNextWorld-Bold.otf", "AvenirNextWorld-Bold");
                fonts.AddFont("AvenirNextWorld-Regular.otf", "AvenirNextWorld");
                fonts.AddFont("BogueSlab-Bold.otf", "BogueSlab-Bold");
                fonts.AddFont("BogueSlab-Regular.ttf", "BogueSlab");
                fonts.AddFont("Elante-Bold.ttf", "Elante-Bold");
                fonts.AddFont("Elante-Regular.ttf", "Elante");
                fonts.AddFont("FrutigerNeueLTPro-Bold.otf", "FrutigerNeueLTPro-Bold");
                fonts.AddFont("FrutigerNeueLTPro-Regular.ttf", "FrutigerNeueLTPro");
                fonts.AddFont("KlintPro-Bold.ttf", "KlintPro-Bold");
                fonts.AddFont("KlintPro-Regular.ttf", "KlintPro");
                fonts.AddFont("NeueHaasUnicaPro-Bold.otf", "NeueHaasUnicaPro-Bold");
                fonts.AddFont("NeueHaasUnicaPro-Regular.ttf", "NeueHaasUnicaPro");
                fonts.AddFont("NotoSans-Bold.ttf", "NotoSans-Bold");
                fonts.AddFont("NotoSans-Regular.ttf", "NotoSans");
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSans");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSans-Bold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}