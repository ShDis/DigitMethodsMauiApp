using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DigitMethodsMauiApp.Numbers;

namespace DigitMethodsMauiApp
{
    public class MainPageViewModel61 : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        Number61 number = new Number611();

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public string NumberName
        {
            get => NumberName;
            set
            {
                NumberName = value;
                OnPropertyChanged();
            }
        }
        
        public Number61 Number
        {
            get => number;
            set
            {
                number = value;
                NumberName = number.NumberName;
                OnPropertyChanged();
            } 
        }

        public ImageSource FormulaImageSource
        {
            get => ImageSource.FromUri(new Uri("https://latex.codecogs.com/gif.latex?\\dpi{300}" + number.NumberFxFunctionLatex));
            set
            {
                FormulaImageSource = value;
                OnPropertyChanged();
            }
        }

        public List<object> MainStackPanelContent
        {
            get
            {
                return new List<object>()
                {
                    new Label
                    {
                        Text = "Определите число узлов для нахождения значения интеграла с точностью = 10^-3 по формулам трапеций. Вычислите с данной точностью. fdsf",
                        FontAttributes = FontAttributes.Bold,
                    },
                };
            }
            set
            {
                OnPropertyChanged();
            }
        }
    }
}
