using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitMethodsMauiApp.Numbers
{
    public class Number61 : Number6
    {
        public Number61() { }

        public double a { get; } = 3.0; // левый предел интегрирования
        public double b { get; } = 4.5; // правый предел интегрирования

        public double f(double x)
        {
            return (1.0 / (x + Math.Sin(0.9 * x)));
        }

        public double f_prime(double x)
        {
            return (-9 * Math.Cos(9 * x / 10) + 10) / (10 * Math.Pow(x + Math.Sin(9 * x / 10), 2));
        }

        public double f_double_prime(double x)
        {
            return (81 * Math.Sin(9 * x / 10) + (2 * Math.Pow(9 * Math.Cos(9 * x / 10) + 10, 2) / (x + Math.Sin(9 * x / 10))))
                / (100 * Math.Pow(x + Math.Sin(9 * x / 10), 2));
        }

        public double f_fourth_prime(double x)
        {
            return -0.65610e0 * Math.Sin(0.9e0 * x) * Math.Pow(x + Math.Sin(0.9e0 * x), -0.2e1) - 0.43740e1 
                * Math.Cos(0.9e0 * x) * Math.Pow(x + Math.Sin(0.9e0 * x), -0.3e1) * (0.1e1 + 0.9e0 * Math.Cos(0.9e0 * x)) 
                + 0.14580e2 * Math.Sin(0.9e0 * x) * Math.Pow(x + Math.Sin(0.9e0 * x), -0.4e1) * Math.Pow(0.1e1 + 0.9e0 
                * Math.Cos(0.9e0 * x), 0.2e1) + 0.393660e1 * Math.Pow(Math.Sin(0.9e0 * x), 0.2e1) * Math.Pow(x + Math.Sin(0.9e0 * x), -0.3e1) 
                - (0.14580e1 + 0.131220e1 * Math.Cos(0.9e0 * x)) * Math.Cos(0.9e0 * x) * Math.Pow(x + Math.Sin(0.9e0 * x), -0.3e1) 
                + (0.4860e1 + 0.43740e1 * Math.Cos(0.9e0 * x)) * Math.Sin(0.9e0 * x) * (0.1e1 + 0.9e0 * Math.Cos(0.9e0 * x)) 
                * Math.Pow(x + Math.Sin(0.9e0 * x), -0.4e1) + (0.9720e1 + 0.87480e1 * Math.Cos(0.9e0 * x)) * (0.1e1 + 0.9e0 * Math.Cos(0.9e0 * x)) 
                * Math.Sin(0.9e0 * x) * Math.Pow(x + Math.Sin(0.9e0 * x), -0.4e1) + (0.240e2 + 0.2160e2 * Math.Cos(0.9e0 * x)) 
                * Math.Pow(0.1e1 + 0.9e0 * Math.Cos(0.9e0 * x), 0.3e1) * Math.Pow(x + Math.Sin(0.9e0 * x), -0.5e1);
        }

        public double M1
        {
            get
            {
                int n = 100000;
                double h = (b - a) / n;
                double max = f_prime(a);
                for (int i = 1; i < n; i++)
                {
                    double newpossiblemax = Math.Abs(f_prime(a + i * h));
                    if (newpossiblemax > max)
                        max = newpossiblemax;
                }
                return max;
            }
        }

        public double M2
        {
            get
            {
                int n = 100000;
                double h = (b - a) / n;
                double max = f_double_prime(a);
                for (int i = 1; i < n; i++)
                {
                    double newpossiblemax = Math.Abs(f_prime(a + i * h));
                    if (newpossiblemax > max)
                        max = newpossiblemax;
                }
                return max;
            }
        }

        public double M4
        {
            get
            {
                int n = 100000;
                double h = (b - a) / n;
                double max = f_double_prime(a);
                for (int i = 1; i < n; i++)
                {
                    double newpossiblemax = Math.Abs(f_fourth_prime(a + i * h));
                    if (newpossiblemax > max)
                        max = newpossiblemax;
                }
                return max;
            }
        }
    }
}