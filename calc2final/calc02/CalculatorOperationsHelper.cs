using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calc02
{
    public class CalculatorOperationsHelper
    {
        public static double PerformOperation(double operand1, double operand2, string operation)
        {
            switch (operation)
            {
                case "+":
                    return operand1 + operand2;
                case "-":
                    return operand1 - operand2;
                case "×":
                    return operand1 * operand2;
                case "÷":
                    return operand1 / operand2;
                default:
                    throw new ArgumentException("Neznámá operace");
            }
        }

        public static string GetOperationResultText(string fstNum, string secNum, double result, string operation, string txtDisplay1Text)
        {
            switch (operation)
            {
                case "+":
                case "-":
                case "×":
                case "÷":
                    return $"{fstNum}{secNum} = {txtDisplay1Text}";
                default:
                    return $"{txtDisplay1Text} =";
            }
        }
    }

}
