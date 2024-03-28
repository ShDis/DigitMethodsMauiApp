using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitMethodsMauiApp.Numbers
{
    public class Number613 : Number61
    {
        public Number613(int variant = 1, int stepsCount = 2, double eps = 0.001) : base(variant, stepsCount, eps) { }
        public override string NumberName => "6.1.3";
        public override object[] Content
        {
            get
            {
                List<string> consoleStack = new List<string>();
                double ideal = IntegrateIdeal;

                consoleStack.Add($"Расчет с удвоением шагов:");
                consoleStack.Add("");

                int n = 1;
                double delta = 1.0;
                while (delta > Eps)
                {
                    double res1 = SolveIntegralSimpson(n);
                    n *= 2;
                    double res2 = SolveIntegralSimpson(n);
                    delta = Math.Abs((res1 - res2) / 7.0);
                    consoleStack.Add($"n = {n} => delta = {delta}:");
                    consoleStack.Add($"I = {res2 + delta}");
                    consoleStack.Add("");
                }

                consoleStack.Add($"Идеал для сравнения:");
                consoleStack.Add($"I = {ideal}");
                consoleStack.Add("");
                consoleStack.Add("Расчет с конкретным числом шагов:");
                consoleStack.Add($"n = {StepsCount} => I = {SolveIntegralSimpson(StepsCount)}");

                var elemsInStack = 2 + consoleStack.Count;
                var returnStack = new object[elemsInStack];
                returnStack[0] = new Label
                {
                    Text = "Определите число узлов для нахождения значения интеграла с точностью = 10^-3 по формулам Симпсона. Вычислите с данной точностью.",
                    FontAttributes = FontAttributes.Bold,
                };
                returnStack[1] = TaskAndAnswerSplitter;
                for (int i = 2; i < elemsInStack; i++)
                {
                    returnStack[i] = TextToUI(consoleStack[i - 2]);
                }

                return returnStack;
            }
        }
        public double SolveIntegralSimpson(int n)
        {
            double h = (RightLimitB - LeftLimitA) / n;
            var sum1 = 0.0;
            var sum2 = 0.0;
            for (var k = 1; k <= n; k++)
            {
                var xk = LeftLimitA + k * h;
                if (k <= n - 1)
                {
                    sum1 += NumberFxFunction(xk);
                }

                var xk_1 = LeftLimitA + (k - 1) * h;
                sum2 += NumberFxFunction((xk + xk_1) / 2);
            }

            double I = h / 3.0 * (1.0 / 2.0 * NumberFxFunction(LeftLimitA) + sum1 + 2.0 * sum2 + 1.0 / 2.0 * NumberFxFunction(RightLimitB));
            return I;

            /*
            double h = (RightLimitB - LeftLimitA) / n;
            double sum = NumberFxFunction(LeftLimitA) + NumberFxFunction(RightLimitB);

            for (int i = 1; i < n / 2 - 1; i++)
            {
                double xi = LeftLimitA + 2 * i * h; // вычисление i-го узла
                double fxi = NumberFxFunction(xi); // вычисление значения функции в узле xi
                sum += 2 * fxi; // добавление значения функции к сумме
            }
            for (int i = 1; i < n / 2; i++)
            {
                double xi = LeftLimitA + (2 * i - 1) * h; // вычисление i-го узла
                double fxi = NumberFxFunction(xi); // вычисление значения функции в узле xi
                sum += 4 * fxi; // добавление значения функции к сумме
            }

            double I = (h / 3.0) * sum;
            return I;
            */
        }
        /*
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
        */

        /*
        public I_III_2()
        {
            this.Num = "6.1.3 (v2)";
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
            double sum = f(a) + f(b);

            for (int i = 1; i < n / 2 - 1; i++)
            {
                double xi = a + 2 * i * h; // вычисление i-го узла
                double fxi = f(xi); // вычисление значения функции в узле xi
                sum += 2 * fxi; // добавление значения функции к сумме
            }
            for (int i = 1; i < n / 2; i++)
            {
                double xi = a + (2 * i - 1) * h; // вычисление i-го узла
                double fxi = f(xi); // вычисление значения функции в узле xi
                sum += 4 * fxi; // добавление значения функции к сумме
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
        */

    }
}
