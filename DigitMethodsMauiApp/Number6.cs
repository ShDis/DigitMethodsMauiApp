using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;

namespace DigitMethodsMauiApp
{
    public class Number6
    {
        public double eps { get; private set; } = 1e-3; // требуемая точность
        public void ChangeEps(double eps)
        {
            this.eps = eps;
        }
        public static bool CheckEps(double eps)
        {
            if (eps <= 0.1 && eps >= double.E-15)
                return true;
            return false;
        }
        public int fixedn { get; private set; } = 10;
        public void ChangeFixedn(int fixedn)
        {
            this.fixedn = fixedn;
        }
        public virtual string[] Execute()
        {
            return new string[] { "Function is uncompleted" };
        }
        public Label[] TextToUI(string[] text)
        {
            var labels = new Label[text.Length];
            for (int i = 0; i < text.Length; i++)
            {
                labels[i] = new Label()
                {
                    Text = text[i],
                };
            }
            return labels;
        }

        public virtual List<FunctionSeries> GetFunctionsToShow() { return new List<FunctionSeries>(); }

        public Label[] ExecuteUIResult => TextToUI(this.Execute());
        public string Num { get; set; }
        public object[] UI_StartInfo { get; set; }
    }
}
