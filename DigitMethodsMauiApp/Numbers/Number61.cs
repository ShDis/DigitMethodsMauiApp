﻿using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitMethodsMauiApp.Numbers
{
    public class Number61 : Number6
    {
        /// <summary>
        /// Инициализация 6.1.x
        /// </summary>
        /// <param name="variant"></param>
        /// <param name="stepsCount"></param>
        /// <param name="eps"></param>
        public Number61(int variant = 1, int stepsCount = 2, double eps = 0.001)
        {
            this.Variant = variant;
            this.StepsCount = stepsCount;
            this.Eps = eps;
        }
        /// <summary>
        /// Номер
        /// </summary>
        public virtual string NumberName { get; private set; } = "6.1.X";
        /// <summary>
        /// Вариант
        /// </summary>
        public int Variant { get; set; }
        /// <summary>
        /// Количество шагов
        /// </summary>
        public int StepsCount { get; set; }
        /// <summary>
        /// Необходимая точность
        /// </summary>
        public double Eps { get; set; }
        /// <summary>
        /// Левый предел интегрирования
        /// </summary>
        public virtual double LeftLimitA { get { return Variant % 2 == 0 ? 0.1 * (Variant + 3) : 0.2 * (Variant + 6); } }
        /// <summary>
        /// Правый предел интегрирования
        /// </summary>
        public virtual double RightLimitB { get { return Variant % 2 == 0 ? 0.25 * (Variant + 3) : 0.3 * (Variant + 6); } }
        /// <summary>
        /// Функция для первых семи номеров (6.1.1 - 6.1.7)
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public virtual double NumberFxFunction(double x)
        {
            if (Variant % 2 == 0)
                return (Math.Pow(x, 3.0) * Math.Pow(Math.E, (-0.01 * (double)Variant * Math.Pow(x, 3.0 / 2.0))));
            else
                return (1.0 / (x + Math.Sin(0.1 * (double)Variant * x)));
        }
        /// <summary>
        /// Функция для графика
        /// </summary>
        public virtual Func<double, double> NumberFxFunctionForOxyPlot(int v)
        {
            return x => v % 2 == 0
                ? (Math.Pow(x, 3.0) * Math.Pow(Math.E, (-0.01 * (double)v * Math.Pow(x, 3.0 / 2.0))))
                : (1.0 / (x + Math.Sin(0.1 * (double)v * x)));
        }
        /// <summary>
        /// Функция для визуала интеграла формулой
        /// </summary>
        public virtual string NumberFxFunctionLatex
        {
            get
            {
                if (Variant % 2 == 0)
                    return "\\int_{" + LeftLimitA.ToString(DoubleFixedPoint) + "}^{" + RightLimitB + "} x^3 e^{" + (-0.01 * (double)Variant).ToString(DoubleFixedPoint) + " x^{\\frac{3}{2}}} dx";
                else
                    return "\\int_{" + LeftLimitA.ToString(DoubleFixedPoint) + "}^{" + RightLimitB + "} \\frac{1}{x + \\sin(" + (0.1 * (double)Variant).ToString(DoubleFixedPoint) + " x)} dx";
            }
        }
        /// <summary>
        /// Вычесленный с адекватной точностью интеграл
        /// </summary>
        public double IntegrateIdeal
        {
            get
            {
                //double romberg = RombergMethod.Integrate(NumberFxFunction, LeftLimitA, RightLimitB);
                //return romberg;
                return SolveIntegralSimpsonIdeal();
            }
        }

        public double SolveIntegralSimpsonIdeal()
        {
            int n = 10000;
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
        }

        public override List<FunctionSeries> GetFunctionsToShow()
        {
            return new List<FunctionSeries>()
            {
                new FunctionSeries(NumberFxFunctionForOxyPlot(Variant),LeftLimitA,RightLimitB,(RightLimitB - LeftLimitA) / 5.0,"f(x) 5 точек"),
                new FunctionSeries(NumberFxFunctionForOxyPlot(Variant),LeftLimitA,RightLimitB,(RightLimitB - LeftLimitA) / 25.0,"f(x) 25 точек"),
            };
        }

        public const string DoubleFixedPoint = "0.###################################################################################################################################################################################################################################################################################################################################################";

        [Obsolete]
        public double f_prime(double x)
        {
            return (-9 * Math.Cos(9 * x / 10) + 10) / (10 * Math.Pow(x + Math.Sin(9 * x / 10), 2));
        }

        [Obsolete]
        public double f_double_prime(double x)
        {
            return (81 * Math.Sin(9 * x / 10) + (2 * Math.Pow(9 * Math.Cos(9 * x / 10) + 10, 2) / (x + Math.Sin(9 * x / 10))))
                / (100 * Math.Pow(x + Math.Sin(9 * x / 10), 2));
        }

        [Obsolete]
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

        [Obsolete]
        public double M1
        {
            get
            {
                int n = 100000;
                double h = (RightLimitB - LeftLimitA) / n;
                double max = f_prime(LeftLimitA);
                for (int i = 1; i < n; i++)
                {
                    double newpossiblemax = Math.Abs(f_prime(LeftLimitA + i * h));
                    if (newpossiblemax > max)
                        max = newpossiblemax;
                }
                return max;
            }
        }

        [Obsolete]
        public double M2
        {
            get
            {
                int n = 100000;
                double h = (RightLimitB - LeftLimitA) / n;
                double max = f_double_prime(LeftLimitA);
                for (int i = 1; i < n; i++)
                {
                    double newpossiblemax = Math.Abs(f_prime(LeftLimitA + i * h));
                    if (newpossiblemax > max)
                        max = newpossiblemax;
                }
                return max;
            }
        }

        [Obsolete]
        public double M4
        {
            get
            {
                int n = 100000;
                double h = (RightLimitB - LeftLimitA) / n;
                double max = f_fourth_prime(LeftLimitA);
                for (int i = 1; i < n; i++)
                {
                    double newpossiblemax = Math.Abs(f_fourth_prime(LeftLimitA + i * h));
                    if (newpossiblemax > max)
                        max = newpossiblemax;
                }
                return max;
            }
        }
    }
}