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

namespace WPF_Calculator
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {

        private List<double> NumbersStack;
        private List<string> OperationStack;

        private bool resultVisible;

        public MainWindow()
        {
            InitializeComponent();

            NumbersStack = new List<double>();
            OperationStack = new List<string>();
            resultVisible = false;
        }

        private void AddNewNumber(object sender, RoutedEventArgs e)
        {
            if (resultVisible)
            {
                lblVisor.Clear();
                resultVisible = false;
            }
            String num = (sender as Button).Content.ToString();
            lblVisor.Text += num;
        }

        private void ClearAction(object sender, RoutedEventArgs e)
        {
            lblVisor.Clear();
            NumbersStack.Clear();
            OperationStack.Clear();
            lblPilha.Clear();
        }

        private void AddNewOperation(object sender, RoutedEventArgs e)
        {
            if(lblVisor.Text.Length > 0)
            {
                double value = Convert.ToDouble(lblVisor.Text);
                string operation = (sender as Button).Content.ToString();
                NumbersStack.Add(value);
                OperationStack.Add(operation);
                lblPilha.Text += $" {value} {operation}";
                lblVisor.Clear();
            }
        }

        private void CalculateAction(object sender, RoutedEventArgs e)
        {
            NumbersStack.Add(Convert.ToDouble(lblVisor.Text));
            
            lblVisor.Clear();
            lblPilha.Clear();
            CalculateStack();
            NumbersStack.Clear();
            OperationStack.Clear();

        }

        private void CalculateStack()
        {
            double result = Calculate(0);
            NumbersStack.Add(result);
            resultVisible = true;
            lblVisor.Text = Convert.ToString(result);
        }

        private double Calculate(int i)
        {
            double res = 0;
            double n1 = NumbersStack[i];
            double n2 = NumbersStack[i+1];
            string op = OperationStack[i];

            switch (op)
            {
                case "+":
                    res = n1 + n2;
                    break;
                case "-":
                    res = n1 - n2;
                    break;
                case "*":
                    res = n1 * n2;
                    break;
                case "/":
                    res = n1 / n2;
                    break;
            }

            i+=2;
            if (i < OperationStack.Count)
                return res + Calculate(i);

            return res;
        }
    }
}
