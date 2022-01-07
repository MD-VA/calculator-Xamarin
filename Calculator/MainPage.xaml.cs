using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Calculator
{

    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new Ynov.SimpleApp.MainPageViewModel();

        }

        private System.Drawing.Color DarkerColor(System.Drawing.Color color, float correctionfactory = 50f)
        {
            const float hundredpercent = 100f;
            return System.Drawing.Color.FromArgb((int)(((float)color.R / hundredpercent) * correctionfactory),
                (int)(((float)color.G / hundredpercent) * correctionfactory), (int)(((float)color.B / hundredpercent) * correctionfactory));
        }


        private decimal firstNumber;
        private string operatorName; 
        private bool isClickedOperator = false;
        private List<string> history;


        private void OperationButton(System.Object sender, System.EventArgs e)
        {
            var button = sender as Button;
            isClickedOperator = true;
            operatorName = button.Text;
            firstNumber = Convert.ToDecimal(result.Text);

        }

        private void btnEqual_clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                decimal secondNumber = Convert.ToDecimal(result.Text);
                string fnResult = caluclate(firstNumber, secondNumber, operatorName).ToString("0.##");
                result.Text = fnResult;
            }
            catch(Exception ex)
            {
                DisplayAlert("Error", ex.Message, "OK");
            }
        }


        private decimal caluclate(decimal firstNumber, decimal secondNumber, string operatorName)
        {
            decimal fnResult = 0;
            switch (operatorName)
            {
                case "+":
                    fnResult = firstNumber + secondNumber;
                    break;
                case "-":
                    fnResult = firstNumber - secondNumber;
                    break;
                case "X":
                    fnResult = firstNumber * secondNumber;
                    break;
                case "/":
                    fnResult = firstNumber / secondNumber;
                    break;
            }

            return fnResult;
        }

        void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            var button = sender as Button;
            if (result.Text =="0" || isClickedOperator)
            {
                isClickedOperator = false;
                result.Text = button.Text;
            }
            else
            {
                result.Text += button.Text;
            }
        }

        void clearBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            result.Text = "0";
            isClickedOperator = false;
            firstNumber = 0;

        }

        void suppBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            string number = result.Text;
            if(number != "0")
            {
                number = number.Remove(number.Length - 1, 1);
                Console.WriteLine(number);
                if (string.IsNullOrEmpty(number))
                {
                    result.Text = "0";
                }else
                {
                    result.Text = number;
                }
            }
        }

        private async void pourcentageBtn_Clicked(System.Object sender, System.EventArgs e)
        {
            try
            {
                string number = result.Text;
                if (number!= "0") {
                    decimal percentValue = Convert.ToDecimal(number);
                    string value = (percentValue / 100).ToString("0.##");
                    result.Text = value;
                }
            }catch(Exception ex)
            {
                await DisplayAlert("Error",ex.Message,"OK");
            }
        }

        private void addHistory()
        {
            history.Add("element");
        }

    }
}
