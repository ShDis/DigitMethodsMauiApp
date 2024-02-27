using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Foldable;
using OxyPlot.Maui.Skia;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace DigitMethodsMauiApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Initialise the .NET MAUI Community Toolkit
            builder.UseMauiApp<App>().UseMauiCommunityToolkit();
            // Init TwoPaneView
            builder.UseFoldable();
            // Init Plots
            builder.UseSkiaSharp();
            builder.UseOxyPlotSkia();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
