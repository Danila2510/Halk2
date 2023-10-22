using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Halk2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string currentInput = "";
        private string currentOperator = "";
        private double firstOperand = 0;
        private double secondOperand = 0;
        private bool isNewCalculation = true;
        private int length_num_str = 0; 
        public MainWindow()
        {
            InitializeComponent();
        }

        private void b_Num_Click(object sender, RoutedEventArgs e)
        {
            if (isNewCalculation)
            {
                currentInput = "";
                isNewCalculation = false;
            }

            Button button = (Button)sender;
            currentInput += button.Content;
            txt_box_Math.Text += button.Content;
            string[] substrings = currentInput.Split(' ');
            length_num_str = substrings[substrings.Length - 1].Length;
        }

        private void OperatorButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(currentInput))
            {

                if (!string.IsNullOrEmpty(currentOperator))
                {
                    secondOperand = double.Parse(currentInput);
                    currentInput = Calculate(firstOperand, secondOperand, currentOperator).ToString();
                    txt_Result.Text = currentInput;
                }
                else
                {
                    firstOperand = double.Parse(currentInput);
                }
                txt_box_Math.Text += $" {currentOperator = ((Button)sender).Content.ToString()} ";

                currentOperator = ((Button)sender).Content.ToString();
                isNewCalculation = true;
            }
        }

        private double Calculate(double firstOperand, double secondOperand, string operatorSymbol)
        {
            switch (operatorSymbol)
            {
                case "+":
                    return firstOperand + secondOperand;
                case "-":
                    return firstOperand - secondOperand;
                case "*":
                    return firstOperand * secondOperand;
                case "/":
                    if (secondOperand != 0)
                        return firstOperand / secondOperand;
                    else
                    {
                        MessageBox.Show("Деление на ноль невозможно!");
                        ClearButton_Click(null, null);
                        return 0;
                    }
                case "^":
                    return Math.Pow(firstOperand, secondOperand);
                default:
                    return 0;
            }
        }

        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(currentInput) && !string.IsNullOrEmpty(currentOperator))
            {
                secondOperand = double.Parse(currentInput);
                currentInput = Calculate(firstOperand, secondOperand, currentOperator).ToString();
                txt_Result.Text = currentInput;
                currentOperator = "";
                isNewCalculation = true;
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            currentInput = "";
            currentOperator = "";
            firstOperand = 0;
            secondOperand = 0;
            txt_Result.Text = "";
            txt_box_Math.Text = "";
        }

        private void b_c_Click(object sender, RoutedEventArgs e)
        {
            currentInput = "";
            secondOperand = 0;
            if (txt_box_Math.Text.Length >= 2)
            {
                txt_box_Math.Text = txt_box_Math.Text.Substring(0, txt_box_Math.Text.Length - length_num_str);
            }
            else
            {
                txt_box_Math.Text = string.Empty;
            }
        }
        private void DecimalPointButton_Click(object sender, RoutedEventArgs e)
        {
            if (!currentInput.Contains("."))
            {
                currentInput += ",";
                txt_box_Math.Text += ",";
            }
        }
    }
}
