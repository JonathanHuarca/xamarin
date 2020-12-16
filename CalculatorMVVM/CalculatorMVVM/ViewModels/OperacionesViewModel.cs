using CalculatorMVVM.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace CalculatorMVVM.ViewModels
{
    public class OperacionesViewModel : ViewModelBase
    {
        #region Propiedades

     
        int currentstate = 1;
        public int CurrentState
        {
            get { return currentstate; }
            set
            {
                if (currentstate != value)
                {
                    currentstate = value;
                    OnPropertyChanged();
                }
            }
        }

        string mathoperator;
        public string MathOperator
        {
            get { return mathoperator; }
            set
            {
                if (mathoperator != value)
                {
                    mathoperator = value;
                    OnPropertyChanged();
                }
            }
        }

        double firstNumber;

        public double FirstNumber
        {
            get { return firstNumber; }
            set
            {
                if (firstNumber != value)
                {
                    firstNumber = value;
                    OnPropertyChanged();
                }
            }
        }
        double secondNumber;

        public double SecondNumber
        {
            get { return secondNumber; }
            set
            {
                if (secondNumber != value)
                {
                    secondNumber = value;
                    OnPropertyChanged();
                }
            }
        }

         string resulttext ="0";
        public string resultText
        {
            get { return resulttext; }
            set
            {
                if (resulttext != value)
                {
                    resulttext = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Comandos 
        public ICommand OnSelectOperator { protected set; get; }
        public ICommand OnSelectNumber { protected set; get; }
        public ICommand OnClear { protected set; get; }
        public ICommand OnCalculate { protected set; get; }

        #endregion

        #region Methods
        void SelectNumber(String pressed)
        {
            if (resultText == "0" || CurrentState < 0)
            {
                resultText = "";
                if (CurrentState < 0)
                    CurrentState *= -1;

            }
            resultText += pressed;

            double number;
            if (double.TryParse(resultText, out number))
            {
                resultText = number.ToString("N0");
                if (CurrentState == 1)
                {
                    FirstNumber = number;
                }
                else
                {
                    SecondNumber = number;
                }
            }
        }

        void SelectOperator(String pressed)
        {

            CurrentState = -2;

            MathOperator = pressed;
            Console.WriteLine(MathOperator);
            resultText += pressed;

        }

        void Clear()
        {
            FirstNumber = 0;
            SecondNumber = 0;
            CurrentState= 1;
            resultText = "0";
        }


        void Calculate()
        {
            if (CurrentState == 2)
            {

                var result = SimpleCalculator.Calculate(FirstNumber, SecondNumber, MathOperator);

                resultText = result.ToString();
                FirstNumber = result;
                CurrentState = -1;
            }
        }
        #endregion

        #region Constructor
        public OperacionesViewModel()
        {
            OnSelectNumber = new Command<string>(
             execute: (string param) =>
             {
                 SelectNumber(param);
             });
            OnSelectOperator = new Command<string>(
             execute: (string param) =>
             {
                 SelectOperator(param);
             });

            OnClear = new Command(() =>
            {
                Clear();
            });
            OnCalculate = new Command(() =>
            {
                Calculate();
            });
        }
        #endregion
       
    }
}
