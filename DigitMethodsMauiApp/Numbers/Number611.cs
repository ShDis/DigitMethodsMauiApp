using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitMethodsMauiApp.Numbers
{
    public class Number611 : Number61
    {
        public Number611(int variant = 1, int stepsCount = 2, double eps = 0.001) : base(variant, stepsCount, eps) {}
        public override string NumberName => "6.1.1";
        public override object[] Content
        {
            get
            {
                List<string> consoleStack = new List<string>();
                /*double ideal = 0.4244010922027635;
                consoleStack.Add($"eps = {Eps} => n = {StepsCount}:");
                consoleStack.Add($"I = {Count(false)}");
                consoleStack.Add("");
                consoleStack.Add($"n = {fixedn} => eps = {Math.Abs(ideal - Count(true, fixedn))}:");
                consoleStack.Add($"I = {Count(true, fixedn)}");
                consoleStack.Add("");
                consoleStack.Add($"Идеал ~E-15:");
                consoleStack.Add($"I = {ideal}");
                */
                return new object[] 
                {
                    new Label {
                    Text = "Определите число узлов для нахождения значения интеграла с точностью = 10^-3 по формулам прямоугольников. Вычислите с данной точностью.",
                    FontAttributes = FontAttributes.Bold,
                    },
                    TaskAndAnswerSplitter,
                    TextToUI(consoleStack.ToArray()),
                };
            }
        }

        public override List<FunctionSeries> GetFunctionsToShow()
        {
            return new List<FunctionSeries>()
            {
                new FunctionSeries(NumberFxFunction,LeftLimitA,RightLimitB,0.1,"f(x)"),
                new FunctionSeries(NumberFxFunction,LeftLimitA,RightLimitB,0.01,"f(x)"),
            };
        }
        /*
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
            double approxh = Math.Pow((24.0 * eps) / ((b - a) * M2), 1.0 / 2.0);
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
        */

        /*
        public I_I_2()
        {
            this.Num = "6.1.1 (v2)";
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

            int n = 2;
            double delta = 1.0;
            while (delta > eps)
            {
                double res1 = Count(n);
                n *= 2;
                double res2 = Count(n);
                delta = Math.Abs(res1 - res2);
                consoleStack.Add($"n = {n} => delta = {delta}:");
                consoleStack.Add($"I = {res2 + delta}");
                consoleStack.Add("");
            }

            consoleStack.Add($"Идеал ~E-15:");
            consoleStack.Add($"I = {ideal}");
            return consoleStack.ToArray();
        }

        public double Count(int n)
        {
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
        */
    }
}
