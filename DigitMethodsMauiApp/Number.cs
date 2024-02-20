using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitMethodsMauiApp.Numbers
{
    public class Number
    {
        public Number() { }

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

        public Label[] ExecuteUIResult => TextToUI(this.Execute());
        public string Num { get; set; }
        public object[] UI_StartInfo { get; set; }
    }
}