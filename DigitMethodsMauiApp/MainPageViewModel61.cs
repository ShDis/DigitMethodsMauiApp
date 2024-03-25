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
            get => number.NumberName;
            set
            {
                OnPropertyChanged();
            }
        }
        
        public Number61 Number
        {
            get => number;
            set => number = value;
        }
    }
}
