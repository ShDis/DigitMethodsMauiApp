using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitMethodsMauiApp.Numbers
{
    public class Number614 : Number61
    {
        public Number614(int variant = 1, int stepsCount = 2, double eps = 0.001) : base(variant, stepsCount, eps) { }
        public override string NumberName => "6.1.4";
        /*
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
        */


        /*
        public I_IV_2()
        {
            this.Num = "6.1.4 (v2)";
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
            *//*double ideal = 0.4244010922027635;

            int n = 4;
            double[] xi = new double[n];
            xi[0] = a;
            double h = (b - a) / n;
            for (int i = 1; i < n; i++)
            {
                xi[i] = xi[i - 1] + h;
            }
            Variable x = new Variable();
            Variable y = new Variable();
            Term func = TermBuilder.Product(x - xi[k]) * TermBuilder.Log(TermBuilder.Exp(x) + TermBuilder.Exp(y));
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
            consoleStack.Add($"I = {ideal}");*//*
            return consoleStack.ToArray();
        }

        public double GetW(double x, int n, double[] xi)
        {
            double p = 1;
            for (int k = 0; k < n; k++)
            {
                p *= (x - xi[k]);
            }
            return p;
        }

        public double Count(int n = 55555)
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
        */
    }
}
