using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace BitAdder
{
    public class Adder : INotifyPropertyChanged
    {
        private bool signMode { get; set; }
        #region FirstNumber
        byte firstNumber;
        public byte FirstNumber 
        { 
            get { return firstNumber; }
            set { firstNumber = value; OnPropertyChanged(nameof(FirstNumber)); } 
        }
        public sbyte SignFirstNumber
        {
            get { return (sbyte)firstNumber; }
            set { FirstNumber = (byte)value; OnPropertyChanged(nameof(SignFirstNumber)); }
        }
        #endregion

        #region SecondNumber
        byte secondNumber;
        public byte SecondNumber
        {
            get { return secondNumber; }
            set { secondNumber = value; OnPropertyChanged(nameof(SecondNumber)); }
        }
        public sbyte SignSecondNumber
        {
            get { return (sbyte)secondNumber; }
            set { SecondNumber = (byte)value; OnPropertyChanged(nameof(SignSecondNumber)); }
        }
        #endregion

        #region Result
        byte result;
        public byte Result
        {
            get { return result; }
            set { result = value; OnPropertyChanged(nameof(Result)); }
        }
        public sbyte SignResult
        {
            get { return (sbyte)result; }
            set { Result = (byte)value; OnPropertyChanged(nameof(SignResult)); }
        }
        #endregion

        public void Add()
        {
            SignResult = (sbyte)(firstNumber + secondNumber);
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public void OnSwitch(object sender, RoutedEventArgs e)
        {
            var stackPanel = (StackPanel)((CheckBox)sender).Parent;
            var switches = stackPanel.Children;
            sbyte tempNumber = 0;
            for (int i = 0; i < switches.Count; i++)
            {
                if ( ((CheckBox)switches[switches.Count - i - 1]).IsChecked == true )
                {
                    tempNumber += (sbyte)Math.Pow(2, i);
                }
            }
            if (stackPanel.Name.Contains("First")) SignFirstNumber = tempNumber;
            else if (stackPanel.Name.Contains("Second")) SignSecondNumber = tempNumber;
            else if (stackPanel.Name.Contains("Result")) SignResult = tempNumber;
        }

        public string MinValue 
        { 
            get
            {
                if (signMode) return sbyte.MinValue.ToString();
                return byte.MinValue.ToString();
            } 
        }
        public string MaxValue
        {
            get
            {
                if (signMode) return sbyte.MaxValue.ToString();
                return byte.MaxValue.ToString();
            }
        }
        public void SetSignMode(bool signMode)
        {
            this.signMode = signMode;
            OnPropertyChanged(nameof(MinValue));
            OnPropertyChanged(nameof(MaxValue));
        }
    }
}
