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
            //BindingContext = model;

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

        private List<Number61> numbers = new List<Number61>()
        {
            new Number611(),
            new Number612(),
            new Number613(),
            new Number614(),
            new Number615(),
            new Number616(),
            new Number617(),
            new Number618(),
        };

        private void InitNumbersButtons()
        {
            HorizontalStackLayout_Buttons.Clear();
            foreach (var number in numbers)
            {
                var b = new Button();
                b.Text = number.NumberName;
                b.Clicked += (s, e) => { InitContent(number); };
                HorizontalStackLayout_Buttons.Add(b);
            }
        }
        private Number61 lastInitedNumber = null;
        private void InitContent(Number61 number)
        {
            if (number == null) { return; }

            Image_FormulaImage.Source = ImageSource.FromUri(new Uri("https://latex.codecogs.com/gif.latex?\\dpi{300}" + number.NumberFxFunctionLatex));
            Label_NumberName.Text = number.NumberName;
            Label_CurrentNumCode.Text = number.NumberName;
            number.Variant = lastInitedNumber.Variant;
            number.Eps = lastInitedNumber.Eps;
            number.StepsCount = lastInitedNumber.StepsCount;

            VerticalStackLayout_NumberItems.Clear();
            foreach (var item in number.Content)
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
    }
}
