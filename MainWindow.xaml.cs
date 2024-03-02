using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace BitAdder
{
    public partial class MainWindow : Window
    {
        private Adder adder;
        Brush signedBrush;
        Brush unsignedBrush;
        #region Bindings
        Binding firstNumberSignBinding;
        Binding firstNumberBinding;
        Binding secondNumberSignBinding;
        Binding secondNumberBinding;
        Binding resultSignBinding;
        Binding resultBinding;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            adder = (Adder)DataContext;
            adder.SetSignMode(false);
            adder.PropertyChanged += ChangeSwitchPanel;

            unsignedBrush = ((CheckBox)FirstNumberSwitchPanel.Children[0]).Background;
            signedBrush = Brushes.DeepPink;

            #region Bindings initializing 
            firstNumberSignBinding = new Binding("SignFirstNumber") { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged }; 
            secondNumberSignBinding = new Binding("SignSecondNumber") { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
            resultSignBinding = new Binding("SignResult") { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };


            firstNumberBinding = new Binding("FirstNumber") { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
            secondNumberBinding = new Binding("SecondNumber") { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
            resultBinding = new Binding("Result") { UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged };
            #endregion

            foreach (var switcher in FirstNumberSwitchPanel.Children) ((CheckBox)switcher).Click += adder.OnSwitch;
            foreach (var switcher in SecondNumberSwitchPanel.Children) ((CheckBox)switcher).Click += adder.OnSwitch;
            foreach (var switcher in ResultNumberSwitchPanel.Children) ((CheckBox)switcher).Click += adder.OnSwitch;

        }



        private void SignModeSwitch(object sender, RoutedEventArgs e)
        {
            if (((CheckBox)sender).IsChecked == true)
            {
                FirstNumberTextBox.SetBinding(TextBox.TextProperty, firstNumberSignBinding);
                SecondNumberTextBox.SetBinding(TextBox.TextProperty, secondNumberSignBinding);
                ResultTextBlock.SetBinding(TextBlock.TextProperty, resultSignBinding);
                adder.SetSignMode(true);
                ((CheckBox)FirstNumberSwitchPanel.Children[0]).Background = signedBrush;
                ((CheckBox)SecondNumberSwitchPanel.Children[0]).Background = signedBrush;
                ((CheckBox)ResultNumberSwitchPanel.Children[0]).Background = signedBrush;
            }
            else
            {
                FirstNumberTextBox.SetBinding(TextBox.TextProperty, firstNumberBinding);
                SecondNumberTextBox.SetBinding(TextBox.TextProperty, secondNumberBinding);
                ResultTextBlock.SetBinding(TextBlock.TextProperty, resultBinding);
                adder.SetSignMode(false);
                ((CheckBox)FirstNumberSwitchPanel.Children[0]).Background = unsignedBrush;
                ((CheckBox)SecondNumberSwitchPanel.Children[0]).Background = unsignedBrush;
                ((CheckBox)ResultNumberSwitchPanel.Children[0]).Background = unsignedBrush;
            }

        }

        private void Add(object sender, RoutedEventArgs e)
        {
            adder.Add();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void ChangeSwitchPanel(object? sender, PropertyChangedEventArgs e)
        {
            string number;
            UIElementCollection switches;
            
            if (e.PropertyName.Contains("First"))
            {
                switches = FirstNumberSwitchPanel.Children;
                number = ((Adder)sender).FirstNumber.ToString("B8");

                for (int i = 0;  i < number.Length; i++)
                {
                    if (number[number.Length-i-1] == '1') ((CheckBox)switches[switches.Count-i-1]).IsChecked = true;
                    else ((CheckBox)FirstNumberSwitchPanel.Children[switches.Count-i-1]).IsChecked = false;
                }
            }
            else if (e.PropertyName.Contains("Second"))
            {
                switches = SecondNumberSwitchPanel.Children;
                number = ((Adder)sender).SecondNumber.ToString("B8");

                for (int i = 0; i < number.Length; i++)
                {
                    if (number[number.Length-i-1] == '1') ((CheckBox)switches[switches.Count-i-1]).IsChecked = true;
                    else ((CheckBox)switches[switches.Count-i-1]).IsChecked = false;
                }
            }
            else if (e.PropertyName.Contains("Result"))
            {
                switches = ResultNumberSwitchPanel.Children;
                number = ((Adder)sender).Result.ToString("B8");
                for (int i = 0; i < number.Length; i++)
                {
                    if (number[number.Length-i-1] == '1') ((CheckBox)switches[switches.Count-i-1]).IsChecked = true;
                    else ((CheckBox)switches[switches.Count-i-1]).IsChecked = false;
                }
            }
        }   
    }
}