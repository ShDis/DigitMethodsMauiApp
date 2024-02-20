using DigitMethodsMauiApp.Numbers;

namespace DigitMethodsMauiApp
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            InitNumbersButtons();
            InitContent(numbers.First());
        }

        private List<Number> numbers = new List<Number>()
        {
            new I_I(),
            new I_II(),
            new I_III(),
            new I_IV(),
            new I_V(),
            new I_VI(),
            new I_VII(),
            new I_VIII(),
        };

        private void InitNumbersButtons()
        {
            HorizontalStackLayout_Buttons.Clear();
            foreach (Number number in numbers)
            {
                var b = new Button();
                b.Text = number.Num;
                b.Clicked += (s, e) => { InitContent(number); };
                HorizontalStackLayout_Buttons.Add(b);
            }
        }

        private void InitContent(Number number)
        {
            if (number == null) { return; }

            Label_CurrentNum.Text = number.Num;

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
        }
    }



    public class I_I : Number
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
                    Text = "Определите число узлов для нахождения значения интеграла с точностью = 10^-3 по формулам прямоугольников. Вычислите с данной точностью." ,
                    FontAttributes = FontAttributes.Bold,
                },
            };
        }

        public override string[] Execute()
        {
            List<string> consoleStack = new List<string>();

            double a = 3.0; // левый предел интегрирования
            double b = 4.5; // правый предел интегрирования
            double eps = 1e-3; // требуемая точность
            double M2 = 4.0;
            int n = (int)Math.Ceiling((b - a) * Math.Sqrt((M2 * (b - a)) / (2.0 * eps))); // определение числа узлов
            double h = (b - a) / n; // шаг разбиения
            double sum = 0.0; // сумма значений функции на отрезке [a, b]

            for (int i = 0; i < n; i++)
            {
                double xi = a + i * h; // вычисление i-го узла
                double fxi = f(xi); // вычисление значения функции в узле xi
                sum += fxi; // добавление значения функции к сумме
            }

            double result = h * sum; // вычисление значения интеграла
            consoleStack.Add($"Значение интеграла с точностью {eps} = {result:.3f}");
            consoleStack.Add($"Значение интеграла = {result}");
            consoleStack.Add($"Число узлов: {n}");
            return consoleStack.ToArray();
        }

        private double f(double x)
        {
            return (1.0 / (x + Math.Sin(0.9 * x)));
        }
    }

    public class I_II : Number
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
                    Text = "1) Оценить вторую производную подынтегральной функции на заданном интервале интегрирования." ,
                    FontAttributes = FontAttributes.Italic,
                },
                new Label {
                    Text = "2) Вычислить значение интеграла при помощи формулы трапеций с использованием числа узлов, рассчитанного на основе оценки второй производной." ,
                    FontAttributes = FontAttributes.Italic,
                },
                new Label {
                    Text = "3) Проверить достаточность точности вычисления интеграла. Если точность не достигнута, увеличить число узлов и повторить шаги 2 и 3." ,
                    FontAttributes = FontAttributes.Italic,
                },
            };
        }

        public override string[] Execute()
        {
            return new string[] { };
        }

        private double f(double x)
        {
            return (1.0 / (x + Math.Sin(0.9 * x)));
        }
    }

    public class I_III : Number
    {
        public I_III()
        {
            this.Num = "6.1.3";
            this.UI_StartInfo = new object[]
            {

            };
        }

        public override string[] Execute()
        {
            return new string[] { };
        }
    }

    public class I_IV : Number
    {
        public I_IV()
        {
            this.Num = "6.1.4";
            this.UI_StartInfo = new object[]
            {

            };
        }

        public override string[] Execute()
        {
            return new string[] { };
        }
    }
    public class I_V : Number
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
    }

    public class I_VI : Number
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
    }

    public class I_VII : Number
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
    }
    public class I_VIII : Number
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
}
