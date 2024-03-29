﻿using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitMethodsMauiApp.Numbers
{
    public class Number612 : Number61
    {
        public Number612(int variant = 1, int stepsCount = 2, double eps = 0.001) : base(variant, stepsCount, eps) { }
        public override string NumberName => "6.1.2";
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
                    double res1 = SolveIntegralTrapez(n);
                    n *= 2;
                    double res2 = SolveIntegralTrapez(n);
                    delta = Math.Abs((res1 - res2) / 3.0);
                    consoleStack.Add($"n = {n} => delta = {delta}:");
                    consoleStack.Add($"I = {res2 + delta}");
                    consoleStack.Add("");
                }
                consoleStack.Add($"Идеал для сравнения:");
                consoleStack.Add($"I = {ideal}");
                consoleStack.Add("");
                consoleStack.Add("Расчет с конкретным числом шагов:");
                consoleStack.Add($"n = {StepsCount} => I = {SolveIntegralTrapez(StepsCount)}");

                var elemsInStack = 2 + consoleStack.Count;
                var returnStack = new object[elemsInStack];
                returnStack[0] = new Label
                {
                    Text = "Определите число узлов для нахождения значения интеграла с точностью = 10^-3 по формулам трапеций. Вычислите с данной точностью.",
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

        public double SolveIntegralTrapez(int n) // Trapez
        {
            double h = (RightLimitB - LeftLimitA) / n;
            double sum = 0.5 * (NumberFxFunction(LeftLimitA) + NumberFxFunction(RightLimitB));

            for (int i = 1; i < n; i++)
            {
                sum += NumberFxFunction(LeftLimitA + (double)i * h);
            }

            double I = h * sum;
            return I;
        }

        /*
        public Number611()
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
            double approxh = Math.Pow((12.0 * eps) / ((b - a) * M2), 1.0 / 2.0);
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
        */

        /*
        public I_II_2()
        {
            this.Num = "6.1.2 (v2)";
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
        */
    }
}
