using DigitMethodsMauiApp.Numbers;
using OxyPlot;
using OxyPlot.Series;
using AutoDiff;
using System.Dynamic;
using System.Reflection;
using System.Net;
using System.ComponentModel;
using System.Reflection.Metadata;

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
            BindingContext = model;

            //var etw = new Number611(1, 2);
            //Image_Test.Source = ImageSource.FromUri(new Uri("https://latex.codecogs.com/gif.latex?\\dpi{400}" + etw.NumberFxFunctionLatex));

            //InitContent(numbers.First());
            //OxyPlotView_Main.Model = plotModel;
        }

        private MainPageViewModel61 model = new MainPageViewModel61();

        //private List<>

        /*
        private List<Type> numbers = new List<Type>()
        {
            Number611.Instance();
        };
        */

        public PlotModel plotModel { get; set; } = new PlotModel() { Title = "Экспериментальный график" };

        public void AddFuncToPlotModel(string title, Func<double, double> f, double from, double to, double dx)
        {
            plotModel.Series.Add(new FunctionSeries(f, from, to, dx));
        }

        public void AddFuncToPlotModel(FunctionSeries functionSeries)
        {
            plotModel.Series.Add(functionSeries);
        }

        public void ClearPlotModel()
        {
            plotModel.Series.Clear();
        }
        private void InitNumbersButtons()
        {
            /*
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
            };*/
            for (int i = 0; i < 8; i++)
            {
                var bt = new Button();
                //bt.Text = ((Number61)Activator.CreateInstance(types[i])).NumberName;
                bt.Text = "6.1." + (i + 1).ToString();
                bt.Clicked += (s, e) => {
                    Number61 nb = null;
                    int parsedVariant = int.Parse(Entry_Variant.Text);
                    int parsedStepsCount = int.Parse(Entry_Fixedn.Text);
                    double parsedEps = double.Parse(Entry_Eps.Text);
                    switch (i)
                    {
                        case 0:
                            nb = new Number611(parsedVariant, parsedStepsCount, parsedEps);
                            break;
                        case 1:
                            nb = new Number612(parsedVariant, parsedStepsCount, parsedEps);
                            break;
                        case 2:
                            nb = new Number613(parsedVariant, parsedStepsCount, parsedEps);
                            break;
                        case 3:
                            nb = new Number614(parsedVariant, parsedStepsCount, parsedEps);
                            break;
                        case 4:
                            nb = new Number615(parsedVariant, parsedStepsCount, parsedEps);
                            break;
                        case 5:
                            nb = new Number616(parsedVariant, parsedStepsCount, parsedEps);
                            break;
                        case 6:
                            nb = new Number617(parsedVariant, parsedStepsCount, parsedEps);
                            break;
                        case 7:
                            nb = new Number618(parsedVariant, parsedStepsCount, parsedEps);
                            break;
                    }
                    model.Number = nb;
                };
                HorizontalStackLayout_Buttons.Add(bt);
            }
        }

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

        /*
        private void InitNumbersButtons()
        {
            HorizontalStackLayout_Buttons.Clear();
            foreach (Number6 number in numbers)
            {
                var b = new Button();
                b.Text = number.Num;
                b.Clicked += (s, e) => { InitContent(number); };
                HorizontalStackLayout_Buttons.Add(b);
            }
        }
        private Number6 lastInitedNumber = null;
        private void InitContent(Number6 number)
        {
            if (number == null) { return; }

            Label_CurrentNum.Text = number.Num;
            Label_CurrentNumCode.Text = number.Num;

            VerticalStackLayout_NumberInfo.Clear();
            foreach (var item in number.UI_StartInfo)
            {
                VerticalStackLayout_NumberInfo.Add((View)item);
            }

            VerticalStackLayout_NumberItems.Clear();
            var executeRes = number.ExecuteUIResult;
            foreach (var item in executeRes)
            {
                VerticalStackLayout_NumberItems.Add((View)item);
            }

            lastInitedNumber = number;

            ClearPlotModel();
            foreach (var item in number.GetFunctionsToShow())
            {
                AddFuncToPlotModel(item);
            }
        }
        */
        private void Button_Plot_Clicked(object sender, EventArgs e)
        {
            twoPaneView.Pane1Length = new GridLength(0, GridUnitType.Absolute);
            twoPaneView.Pane2Length = new GridLength(1, GridUnitType.Star);
        }

        private void Button_Task_Clicked(object sender, EventArgs e)
        {
            twoPaneView.Pane2Length = new GridLength(0, GridUnitType.Absolute);
            twoPaneView.Pane1Length = new GridLength(1, GridUnitType.Star);
        }

        private void Entry_Eps_Completed(object sender, EventArgs e)
        {
            /*
            double temp_e = 0.001;
            try
            {
                temp_e = double.Parse(Entry_Eps.Text);
                if (!Number6.CheckEps(temp_e))
                    throw new Exception();
            }
            catch
            {
                temp_e = 0.001;
            }
            Entry_Eps.Text = temp_e.ToString();
            foreach (var item in numbers)
            {
                item.ChangeEps(temp_e);
            }
            InitContent(lastInitedNumber);
            */
        }

        private void Entry_Fixedn_Completed(object sender, EventArgs e)
        {
            /*
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
            Entry_Fixedn.Text = temp_n.ToString();
            foreach (var item in numbers)
            {
                item.ChangeFixedn(temp_n);
            }
            InitContent(lastInitedNumber);
            */
        }
    }
}
