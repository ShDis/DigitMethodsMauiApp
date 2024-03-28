using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;

namespace DigitMethodsMauiApp.Numbers
{
    public class Number6
    {
        public virtual object[] Content
        {
            get
            {
                return new object[] { new Label() { Text = "no content" } };
            }
        }
        protected virtual object TaskAndAnswerSplitter
        {
            get
            {
                //var b = new Border()
                //{
                //    BackgroundColor = Color.FromRgb(255, 230, 230),
                //};
                var l = new Label()
                {
                    Text = "Результат",
                    FontSize = 32,
                    Padding = new Thickness(0.0, 16.0, 0.0, 16.0),
                };
                return l;
            }
        }
        protected virtual Label[] TextToUI(string[] text)
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

        protected virtual Label TextToUI(string text)
        {
            return new Label() { Text = text };
        }

        public virtual List<FunctionSeries> GetFunctionsToShow() { return new List<FunctionSeries>(); }
    }
}
