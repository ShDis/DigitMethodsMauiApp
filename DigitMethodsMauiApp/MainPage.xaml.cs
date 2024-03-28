using DigitMethodsMauiApp.Numbers;
using OxyPlot;
using OxyPlot.Series;
using AutoDiff;
using System.Dynamic;
using System.Reflection;
using System.Net;
using System.ComponentModel;
using System.Reflection.Metadata;
using OxyPlot.Legends;

namespace DigitMethodsMauiApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            // инициализация кнопок номеров
            InitNumbersButtons();

            // привязка к ViewModel
            //BindingContext = model;

            //var etw = new Number611(1, 2);
            //Image_Test.Source = ImageSource.FromUri(new Uri("https://latex.codecogs.com/gif.latex?\\dpi{400}" + etw.NumberFxFunctionLatex));

            //InitContent(numbers.First());

            // модель графика
            //OxyPlotView_Main.Model = oxyPlotModel;
        }

        //private MainPageViewModel61 model = new MainPageViewModel61();

        //public PlotModel oxyPlotModel { get; set; } = new PlotModel() { Title = "Выберите номер для просмотра графика" };

        /// <summary>
        /// Номера
        /// </summary>
        private List<Number61> numbers = new List<Number61>()
        {
            new Number611left(),
            new Number611right(),
            new Number611central(),
            new Number612(),
            new Number613(),
            new Number614(),
            new Number615(),
            new Number616(),
            new Number617(),
            new Number618(),
        };
        /// <summary>
        /// Инициализая кнопок номеров
        /// </summary>
        private void InitNumbersButtons()
        {
            HorizontalStackLayout_Buttons.Clear();
            foreach (var number in numbers)
            {
                var b = new Button();
                b.Style = (Style)this.Resources["Style_BasicNumberButton"];
                b.Text = number.NumberName;
                b.Clicked += 
                    (s, e) => { 
                        InitContent(number); 
                        Button? b = s as Button;
                        if (b != null) {
                            ResetButtonsColor();
                            b.Style = (Style)this.Resources["Style_PressedNumberButton"];
                        }; 
                    };
                buttons.Add(b);
                HorizontalStackLayout_Buttons.Add(b);
            }
        }

        private List<Button> buttons = new List<Button>();
        private void ResetButtonsColor()
        {
            foreach (var b in buttons)
                b.Style = (Style)this.Resources["Style_BasicNumberButton"];
        }
        private Number61 lastInitedNumber = new Number611left();
        private void InitContent(Number61 number)
        {
            if (number == null) { return; }
           
            Label_NumberName.Text = number.NumberName;
            Label_CurrentNumCode.Text = number.NumberName;
            number.Variant = lastInitedNumber.Variant;
            number.Eps = lastInitedNumber.Eps;
            number.StepsCount = lastInitedNumber.StepsCount;
            InitIntegralImage(number);

            VerticalStackLayout_NumberItems.Clear();
            foreach (var item in number.Content)
            {
                VerticalStackLayout_NumberItems.Add((View)item);
            }

            lastInitedNumber = number;

            var oxyPlotModel = new PlotModel();
            oxyPlotModel.DefaultColors = new List<OxyColor>() { OxyColor.FromArgb(180, 225, 175, 209), OxyColor.FromArgb(220, 116, 105, 182) };
            //oxyPlotModel.Series.Add(new FunctionSeries(x => x * x, -5.0, 5.0, 0.5, "test"));
            foreach (var item in number.GetFunctionsToShow())
            {
                oxyPlotModel.Series.Add(item);
                
            }
            var l = new Legend
            {
                LegendPlacement = LegendPlacement.Inside,
                LegendPosition = LegendPosition.RightTop,
                LegendBackground = OxyColor.FromAColor(200, OxyColors.White),
                LegendBorder = OxyColors.Black,
            };
            oxyPlotModel.Legends.Add(l);
            OxyPlotView_Main.Model = oxyPlotModel;
        }

        private static async Task<bool> DoesUrlExists(String url)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromMilliseconds(10000);
                    var cts = new CancellationTokenSource();
                    try
                    {
                        var x = await client.GetAsync(url, cts.Token);
                        return true;
                    }
                    catch (WebException ex)
                    {
                        // handle web exception
                    }
                    catch (TaskCanceledException ex)
                    {
                        if (ex.CancellationToken == cts.Token)
                        {
                            // a real cancellation, triggered by the caller
                        }
                        else
                        {
                            // a web request timeout (possibly other things!?)
                        }
                    }
                }
            }
            catch
            {

            }
            return false;
        }

        private async void InitIntegralImage(Number61 number)
        {
            Image_FormulaImage.Source = ImageSource.FromFile("n6_1.png");
            bool exists = await DoesUrlExists("https://latex.codecogs.com");
            if (exists)
            {
                Image_FormulaImage.Source = ImageSource.FromUri(new Uri("https://latex.codecogs.com/gif.latex?\\dpi{350}" + number.NumberFxFunctionLatex));
            }
        }
        
        private void Button_Plot_Clicked(object sender, EventArgs e)
        {
            twoPaneView.Pane1Length = new GridLength(0, GridUnitType.Absolute);
            twoPaneView.Pane2Length = new GridLength(1, GridUnitType.Star);
            twoPaneView.Pane1.IsVisible = false;
            twoPaneView.Pane2.IsVisible = true;
        }

        private void Button_Task_Clicked(object sender, EventArgs e)
        {
            twoPaneView.Pane2Length = new GridLength(0, GridUnitType.Absolute);
            twoPaneView.Pane1Length = new GridLength(1, GridUnitType.Star);
            twoPaneView.Pane1.IsVisible = true;
            twoPaneView.Pane2.IsVisible = false;
        }

        private void Entry_Eps_Completed(object sender, EventArgs e)
        {
            double temp_e = 0.001;
            try
            {
                temp_e = double.Parse(Entry_Eps.Text);
                if (temp_e > 0.1 || temp_e < 10E-15)
                    throw new Exception();
            }
            catch
            {
                temp_e = 0.001;
            }
            Entry_Eps.Text = temp_e.ToString();
            lastInitedNumber.Eps = temp_e;
            InitContent(lastInitedNumber);
        }

        private void Entry_Fixedn_Completed(object sender, EventArgs e)
        {
            int temp_n = 10;
            try
            {
                temp_n = int.Parse(Entry_Fixedn.Text);
                if (temp_n < 1)
                    throw new Exception();
            }
            catch
            {
                temp_n = 10;
            }

            Label_Steps.Text = (temp_n % 10 == 1 && temp_n % 100 != 11) ? "шаг" : (temp_n % 10 > 1 && temp_n % 10 < 5 && (temp_n % 100 > 14 || temp_n % 100 < 12)) ? "шага" : "шагов";

            Entry_Fixedn.Text = temp_n.ToString();
            lastInitedNumber.StepsCount = temp_n;
            InitContent(lastInitedNumber);
        }

        private void Entry_Variant_Completed(object sender, EventArgs e)
        {
            int temp_v = 1;
            try
            {
                temp_v = int.Parse(Entry_Variant.Text);
                if (temp_v < 1 || temp_v > 30)
                    throw new Exception();
            }
            catch
            {
                temp_v = 1;
            }
            Entry_Variant.Text = temp_v.ToString();
            lastInitedNumber.Variant = temp_v;
            InitContent(lastInitedNumber);
        }

        /*
private void InitNumbersButtons()
{
    List<Type> types = new List<Type>() 
    { 
        typeof(Number611), 
        typeof(Number612),
        typeof(Number613),
        typeof(Number614),
        typeof(Number615),
        typeof(Number616),
        typeof(Number617),
        typeof(Number618),
    };
    for (int i = 0; i < types.Count; i++)
    {
        var bt = new Button();
        bt.Text = ((Number61)Activator.CreateInstance(types[i])).NumberName;
        //bt.Text = "6.1." + (i + 1).ToString();
        bt.Clicked += (s, e) => {
            int parsedVariant = int.Parse(Entry_Variant.Text);
            int parsedStepsCount = int.Parse(Entry_Fixedn.Text);
            double parsedEps = double.Parse(Entry_Eps.Text);
            model.Number = (Number61)Activator.CreateInstance(types[i], parsedVariant, parsedStepsCount, parsedEps);
        };

        HorizontalStackLayout_Buttons.Add(bt);
    }
}*/

        /*
        private object? GetInstanceOfType(Type type)
        {
            return Activator.CreateInstance(
                    type,
                    new object[]
                    {
                        int.Parse(Entry_Variant.Text),
                        int.Parse(Entry_Fixedn.Text),
                        double.Parse(Entry_Eps.Text)
                    }
                    );
        }

        private void InitContent(Type numType)
        {
            try
            {
                model.Number = (Number61)CreateInstance();
            }
            catch (Exception ex)
            {
                
            }
        }
        */
    }
}
