using DigitMethodsMauiApp.Numbers;
using OxyPlot;
using OxyPlot.Series;

namespace DigitMethodsMauiApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            InitNumbersButtons();
            InitContent(numbers.First());
            OxyPlotView_Main.Model = plotModel;
        }

        private List<Number6> numbers = new List<Number6>()
        {
            new I_I(),
            new I_II(),
            new I_III(),
            new I_IV(),
            new I_V(),
            new I_VI(),
            new I_VII(),
            new I_VIII(),
            new II_I(),
            new II_II(),
        };

        public PlotModel plotModel { get; set; } = new PlotModel() { Title = "Экспериментальный график" };

        public void AddFuncToPlotModel(string title, Func<double,double> f, double from, double to, double dx)
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
            foreach (var item in numbers)
            {
                item.ChangeFixedn(temp_n);
            }
            InitContent(lastInitedNumber);
        }
    }



    public class I_I : Number61
    {
        
        public I_I()
        {
            this.Num = "6.1.1";
            this.UI_StartInfo = new object[]
            {
                new Image()
                {
                    Source = ImageSource.FromFile("n6_1.png"),
                },
                new Label {
                    Text = "Определите число узлов для нахождения значения интеграла с точностью = 10^-3 по формулам прямоугольников. Вычислите с данной точностью.",
                    FontAttributes = FontAttributes.Bold,
                },
                new Label {
                    Text = $"a = {a}, b = {b}",
                    FontAttributes = FontAttributes.None,
                },
            };
        }

        public override string[] Execute()
        {
            List<string> consoleStack = new List<string>();
            double ideal = 0.4244010922027635;
            consoleStack.Add($"eps = {eps} => n = {GetN()}:");
            consoleStack.Add($"I = {Count(false)}");
            consoleStack.Add("");
            consoleStack.Add($"n = {fixedn} => eps = {Math.Abs(ideal - Count(true, fixedn))}:");
            consoleStack.Add($"I = {Count(true, fixedn)}");
            consoleStack.Add("");
            consoleStack.Add($"Идеал ~E-15:");
            consoleStack.Add($"I = {ideal}");
            return consoleStack.ToArray();
        }

        public int GetN()
        {
            double approxh = Math.Pow((24.0 * eps) / ((b - a) * M2), 0.5);
            int n = (int)Math.Ceiling((b - a) / approxh);
            return n;
        }
        public double Count(bool useFixedn, int n = 10)
        {
            if (!useFixedn)
            {
                n = GetN();
            }

            double h = (b - a) / n;
            double sum = f(a);

            for (int i = 1; i < n; i++)
            {
                double xi = a + (i - 0.5) * h; // вычисление i-го узла
                double fxi = f(xi); // вычисление значения функции в узле xi
                sum += fxi; // добавление значения функции к сумме
            }
            double I = h * sum;
            return I;
        }

        public override List<FunctionSeries> GetFunctionsToShow()
        {
            return new List<FunctionSeries>()
            {
                new FunctionSeries(f,a,b,0.1,"f(x)"),
                new FunctionSeries(f,a,b,fixedn,"f(x)"),
                new FunctionSeries(f,a,b,0.1,"f'(x)"),
                new FunctionSeries(f_double_prime,a,b,0.1,"f''(x)"),
                new FunctionSeries(f_fourth_prime,a,b,0.1,"f''''(x)"),
            };
        }
    }

    public class I_II : Number61
    {
        public I_II()
        {
            this.Num = "6.1.2";
            this.UI_StartInfo = new object[]
            {
                new Image()
                {
                    Source = ImageSource.FromFile("n6_1.png"),
                },
                new Label {
                    Text = "Определите число узлов для нахождения значения интеграла с точностью = 10^-3 по формулам трапеций. Вычислите с данной точностью." ,
                    FontAttributes = FontAttributes.Bold,
                },
                new Label {
                    Text = $"a = {a}, b = {b}",
                    FontAttributes = FontAttributes.None,
                },
            };
        }
        public int GetN()
        {
            double approxh = Math.Pow((12.0 * eps) / ((b - a) * M2), 0.5);
            int n = (int)Math.Ceiling((b - a) / approxh);
            return n;
        }
        public override string[] Execute()
        {
            List<string> consoleStack = new List<string>();

            double ideal = 0.4244010922027635;
            consoleStack.Add($"eps = {eps} => n = {GetN()}:");
            consoleStack.Add($"I = {Count(false)}");
            consoleStack.Add("");
            consoleStack.Add($"n = {fixedn} => eps = {Math.Abs(ideal - Count(true, fixedn))}:");
            consoleStack.Add($"I = {Count(true, fixedn)}");
            consoleStack.Add("");
            consoleStack.Add($"Идеал ~E-15:");
            consoleStack.Add($"I = {ideal}");
            return consoleStack.ToArray();
        }

        public double Count(bool useFixedn, int n = 10)
        {
            if (!useFixedn)
            {
                n = GetN();
            }

            double h = (b - a) / n;
            double sum = f(a) + f(b);

            for (int i = 1; i < n - 1; i++)
            {
                double xi = a + i * h; // вычисление i-го узла
                double fxi = f(xi); // вычисление значения функции в узле xi
                sum += 2 * fxi; // добавление значения функции к сумме
            }

            double I = h * (sum / 2.0);
            return I;
        }

        public override List<FunctionSeries> GetFunctionsToShow()
        {
            return new List<FunctionSeries>()
            {
                new FunctionSeries(f,a,b,0.1,"f(x)"),
                new FunctionSeries(f,a,b,fixedn,"f(x)"),
                new FunctionSeries(f,a,b,0.1,"f'(x)"),
                new FunctionSeries(f_double_prime,a,b,0.1,"f''(x)"),
                new FunctionSeries(f_fourth_prime,a,b,0.1,"f''''(x)"),
            };
        }
    }

    public class I_III : Number61
    {
        public I_III()
        {
            this.Num = "6.1.3";
            this.UI_StartInfo = new object[]
            {
                new Image()
                {
                    Source = ImageSource.FromFile("n6_1.png"),
                },
                new Label {
                    Text = "Определите число узлов для нахождения значения интеграла с точностью = 10^-3 по формулам Симпсона. Вычислите с данной точностью." ,
                    FontAttributes = FontAttributes.Bold,
                },
                new Label {
                    Text = $"a = {a}, b = {b}",
                    FontAttributes = FontAttributes.None,
                },
            };
        }

        public override string[] Execute()
        {
            List<string> consoleStack = new List<string>();

            double ideal = 0.4244010922027635;
            consoleStack.Add($"eps = {eps} => n = {GetN()}:");
            consoleStack.Add($"I = {Count(false)}");
            consoleStack.Add("");
            consoleStack.Add($"n = {fixedn} => eps = {Math.Abs(ideal - Count(true, fixedn))}:");
            consoleStack.Add($"I = {Count(true, fixedn)}");
            consoleStack.Add("");
            consoleStack.Add($"Идеал ~E-15:");
            consoleStack.Add($"I = {ideal}");
            return consoleStack.ToArray();
        }
        public int GetN()
        {
            double approxh = Math.Pow((2880.0 * eps) / ((b - a) * M4), 0.25);
            int n = (int)Math.Ceiling((b - a) / approxh);
            n = n % 2 == 0 ? n : n + 1;
            return n;
        }
        public double Count(bool useFixedn, int n = 10)
        {
            if (!useFixedn)
            {
                n = GetN();
            }

            double h = (b - a) / n;
            double sum = f(a) + f(b);

            for (int i = 1; i < n - 1; i++)
            {
                double xi = a + i * h; // вычисление i-го узла
                double fxi = f(xi); // вычисление значения функции в узле xi
                sum += (i % 2 == 0 ? 2 : 4) * fxi; // добавление значения функции к сумме
            }

            double I = (h / 3.0) * sum;
            return I;
        }

        public override List<FunctionSeries> GetFunctionsToShow()
        {
            return new List<FunctionSeries>()
            {
                new FunctionSeries(f,a,b,0.1,"f(x)"),
                new FunctionSeries(f,a,b,fixedn,"f(x)"),
                new FunctionSeries(f,a,b,0.1,"f'(x)"),
                new FunctionSeries(f_double_prime,a,b,0.1,"f''(x)"),
                new FunctionSeries(f_fourth_prime,a,b,0.1,"f''''(x)"),
            };
        }
    }

    public class I_IV : Number61
    {
        public I_IV()
        {
            this.Num = "6.1.4";
            this.UI_StartInfo = new object[]
            {
                new Image()
                {
                    Source = ImageSource.FromFile("n6_1.png"),
                },
                new Label {
                    Text = "Используя формулы интерполяционного типа, вычислите интеграл. Оцените погрешность." ,
                    FontAttributes = FontAttributes.Bold,
                },
                new Label {
                    Text = $"a = {a}, b = {b}",
                    FontAttributes = FontAttributes.None,
                },
            };
        }

        public override string[] Execute()
        {
            List<string> consoleStack = new List<string>();

            int n = fixedn;
            double h = (b - a) / n;
            double[] xarr = new double[n];
            double[] yarr = new double[n];
            for (int i = 0; i < n; i++)
            {
                double x = a + i * h;
                xarr[i] = x;
                double y = f(x);
                yarr[i] = y;
            }

            double sum = 0;
            for (int i = 0; i < n; i++)
            {
                double xi = a + i * h; // вычисление i-го узла
                double fxi = f(xi); // вычисление значения функции в узле xi
                double wx = 1;
                for (int k = 0; k < n; k++)
                {
                    wx *= (xarr[i] - xarr[k]);
                }
                sum += (fxi 
                    * wx 
                    / ((xi - xarr[i]) * f_prime(xarr[i]))
                    ); // добавление значения функции к сумме
            }

            double ideal = 0.4244010922027635;
            consoleStack.Add($"n = {fixedn}:");
            consoleStack.Add($"I = {sum}");
            consoleStack.Add("");
            consoleStack.Add($"Идеал ~E-15:");
            consoleStack.Add($"I = {ideal}");
            return consoleStack.ToArray();
        }
    }
    public class I_V : Number61
    {
        public I_V()
        {
            this.Num = "6.1.5";
            this.UI_StartInfo = new object[]
            {

            };
        }

        public override string[] Execute()
        {
            return new string[] { };
        }

        public override List<FunctionSeries> GetFunctionsToShow()
        {
            return new List<FunctionSeries>()
            {
                new FunctionSeries(f,a,b,0.1,"f(x)"),
                new FunctionSeries(f,a,b,fixedn,"f(x)"),
                new FunctionSeries(f,a,b,0.1,"f'(x)"),
                new FunctionSeries(f_double_prime,a,b,0.1,"f''(x)"),
                new FunctionSeries(f_fourth_prime,a,b,0.1,"f''''(x)"),
            };
        }
    }

    public class I_VI : Number61
    {
        public I_VI()
        {
            this.Num = "6.1.6";
            this.UI_StartInfo = new object[]
            {

            };
        }

        public override string[] Execute()
        {
            return new string[] { };
        }

        public override List<FunctionSeries> GetFunctionsToShow()
        {
            return new List<FunctionSeries>()
            {
                new FunctionSeries(f,a,b,0.1,"f(x)"),
                new FunctionSeries(f,a,b,fixedn,"f(x)"),
                new FunctionSeries(f,a,b,0.1,"f'(x)"),
                new FunctionSeries(f_double_prime,a,b,0.1,"f''(x)"),
                new FunctionSeries(f_fourth_prime,a,b,0.1,"f''''(x)"),
            };
        }
    }

    public class I_VII : Number61
    {
        public I_VII()
        {
            this.Num = "6.1.7";
            this.UI_StartInfo = new object[]
            {

            };
        }

        public override string[] Execute()
        {
            return new string[] { };
        }

        public override List<FunctionSeries> GetFunctionsToShow()
        {
            return new List<FunctionSeries>()
            {
                new FunctionSeries(f,a,b,0.1,"f(x)"),
                new FunctionSeries(f,a,b,fixedn,"f(x)"),
                new FunctionSeries(f,a,b,0.1,"f'(x)"),
                new FunctionSeries(f_double_prime,a,b,0.1,"f''(x)"),
                new FunctionSeries(f_fourth_prime,a,b,0.1,"f''''(x)"),
            };
        }
    }
    public class I_VIII : Number61
    {
        public I_VIII()
        {
            this.Num = "6.1.8";
            this.UI_StartInfo = new object[]
            {

            };
        }

        public override string[] Execute()
        {
            return new string[] { };
        }
    }

    public class II_I : Number62
    {
        public II_I()
        {
            this.Num = "6.2.1";
            this.UI_StartInfo = new object[]
            {
                new Image()
                {
                    Source = ImageSource.FromFile("n6_2.png"),
                },
                new Label {
                    Text = "Решите задачу Коши методом последовательных приближений (Пикара)" ,
                    FontAttributes = FontAttributes.Bold,
                },
                new Label {
                    Text = $"a = {a}, b = {b}",
                    FontAttributes = FontAttributes.None,
                },
            };
        }

        public override string[] Execute()
        {
            int n = 10;
            double h = (b-a) / n;
            double[] x = new double[n]; 
            for (int i = 0; i < n; i++)
            {
                x[i] = a + i * h;
            }
            double[] y = new double[n];
            y[0] = 0;
            for (int j = 1; j < n; j++)
            {
                y[j] = y0 + Math.Ceiling(f(x[j], y[j - 1]));
            }
            return new string[] { };
        }
    }

    public class II_II : Number62
    {
        public II_II()
        {
            this.Num = "6.2.2";

            this.UI_StartInfo = new object[]
            {

            };

        }

        public override string[] Execute()
        {
            return new string[] { };
        }
    }
}
