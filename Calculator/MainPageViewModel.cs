using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Ynov.SimpleApp
{
    internal class MainPageViewModel : ViewModelBase
    {

        
        private string _result;

        private decimal firstNumber;
        private string operatorName;
        private bool isClickedOperator = false;
        public List<string> _history = new List<string>();
        private string history_result;

        public string Result
        {
            get => _result;
            set => SetProperty<String>(ref _result, value);
        }

        public string HistoryRes
        {
            get => history_result;
            set => SetProperty<String>(ref history_result, value);
        }

        public List<string> History
        {
            get => _history;
            set => SetProperty<List<string>>(ref _history, value);
        }


    public Command<string> ClickNumberCommand { get; }
        public Command ClearCommand { get; }
        public Command SuppCommand { get; }
        public Command PourcentCommand { get; }
        public Command<string> OperationCommand { get; }
        public Command EqualCommand { get; }






        public Command Clear { get; }

        public MainPageViewModel()
        {
            ClickNumberCommand = new Command<string>(OnClickNumber);
            ClearCommand = new Command(clearBtn_Clicked);
            SuppCommand = new Command(suppBtn_Clicked);
            PourcentCommand = new Command(pourcentageBtn_Clicked);
            OperationCommand = new Command<string>(OperationButton);
            EqualCommand = new Command(btnEqual_clicked);

        }

        private void clearBtn_Clicked()
        {
            Result = "0";
            isClickedOperator = false;
            firstNumber = 0;
        }

        private void OnClickNumber(string number)
        {
            if (Result == "0" || isClickedOperator || Result == null)
            {
                isClickedOperator = false;
                Result = number;
            }
            else
            {
                Result += number;
            }
        }

  

        private void suppBtn_Clicked()
        {
            string number = Result;
            if (number != "0")
            {
                number = number.Remove(number.Length - 1, 1);
                Console.WriteLine(number);
                if (string.IsNullOrEmpty(number))
                {
                    Result = "0";
                }
                else
                {
                    Result = number;
                }
            }
        }


        private async void pourcentageBtn_Clicked()
        {
            try
            {
                string number = Result;
                if (number != "0")
                {
                    decimal percentValue = Convert.ToDecimal(number);
                    string value = (percentValue / 100).ToString("0.##");
                    Result = value;
                }
            }
            catch (Exception ex)
            {
                Result = "ERROR";
            }
        }

        private void OperationButton(string op)
        {
            isClickedOperator = true;
            operatorName = op;
            firstNumber = Convert.ToDecimal(Result);

        }


        private void btnEqual_clicked()
        {
            try
            {
                decimal secondNumber = Convert.ToDecimal(Result);
                string fnResult = caluclate(firstNumber, secondNumber, operatorName).ToString("0.##");
                Result = fnResult;
                addHistory(firstNumber + operatorName + secondNumber + "=" + fnResult);
            }
            catch (Exception ex)
            {
                Result = ex.Message;
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

        private void addHistory(string result)
        {
            History.Add(result);
            HistoryRes = "";
            foreach (Object obj in History)
            {
                Console.Write("   {0}", obj);
                Console.WriteLine();
                HistoryRes += obj + "\n";
            }
              
        }
    

    }
}
