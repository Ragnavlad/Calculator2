using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calc02
{
    public partial class Form1 : Form
    {
        Double result = 0;
        string operation = string.Empty;
        string fstNum, secNum;
        bool enterValue = false;

        private readonly KeyHandler keyHandler;

        public Form1()
        {
            InitializeComponent();
            keyHandler = new KeyHandler(Btn0, Btn1, Btn2, Btn3, Btn4, Btn5, Btn6, Btn7, Btn8, Btn9,
                                BtnDesimal, BtnAdd, BtnSubtraction, BtnMultiply, BtnDivision,
                                BtnBackSpace, BtnHistory);
        }

        private void BtnMathOperation_Click(object sender, EventArgs e)
        {
            //Nastavení reakce základních matematických operací a vymezení nuly
            if (result != 0) BtnEquals.PerformClick();
            else result = Double.Parse(TxtDisplay1.Text);

            Button button = (Button)sender;
            operation= button.Text;
            enterValue= true;
            if(TxtDisplay1.Text != "0")
            {
                TxtDisplay2.Text = fstNum = $"{result}{operation}";
                TxtDisplay1.Text = string.Empty;
            }
        }

        private void BtnEquals_Click(object sender, EventArgs e)
        {
            // Nastevení tlačítka Rovná se a zajištění základních matematických funkcí a jejich správné prezentování
            secNum = TxtDisplay1.Text;
            TxtDisplay2.Text = $"{TxtDisplay2.Text}{TxtDisplay1.Text} =";

            if (!string.IsNullOrEmpty(TxtDisplay1.Text))
            {
                if (TxtDisplay1.Text == "0") TxtDisplay2.Text = string.Empty;

                try
                {
                    double secondOperand = Double.Parse(TxtDisplay1.Text);

                    if (!string.IsNullOrEmpty(operation))
                    {
                        result = CalculatorOperationsHelper.PerformOperation(result, secondOperand, operation);

                        TxtDisplay1.Text = result.ToString();
                        RtBoxDisplayHistory.AppendText(CalculatorOperationsHelper.GetOperationResultText(fstNum, secNum, result, operation, TxtDisplay1.Text) + "\n");

                        operation = string.Empty;
                    }
                    else
                    {
                        TxtDisplay2.Text = $"{TxtDisplay1.Text} =";
                    }
                }
                catch (FormatException)
                {
                    //Obsluha chyby při konverzi na double (pokud vstup není číslo)
                    Console.WriteLine("Chyba při konverzi na double.");
                }
                catch (ArgumentException ex)
                {
                    //Obsluha chyby v případě neznámé operace
                    Console.WriteLine(ex.Message);
                }
            }
        }


        private void BtnHistory_Click(object sender, EventArgs e)
        {
            //Nastavení tlačítka Historie
            PnlHistory.Height = (PnlHistory.Height == 5) ? PnlHistory.Height = 440 : 5;
        }

        private void BtnClearHistory_Click(object sender, EventArgs e)
        {
            //Nastavení tlalačítka na mazání historie
            RtBoxDisplayHistory.Clear();
            if (RtBoxDisplayHistory.Text == string.Empty)
            {
                RtBoxDisplayHistory.Text = "Historie příkladů:\n";
            }               
        }

        private void BtnBackSpace_Click(object sender, EventArgs e)
        {
            //Nastavení tlačítka BackSpace
            if (TxtDisplay1.Text.Length > 0)
                TxtDisplay1.Text = TxtDisplay1.Text.Remove(TxtDisplay1.Text.Length - 1, 1);
            if (TxtDisplay1.Text == string.Empty) TxtDisplay1.Text = "0";
        }

        private void BtnC_Click(object sender, EventArgs e)
        {
            //Nastavení tlačítka Clear
            TxtDisplay1.Text = "0";
            TxtDisplay2.Text = string.Empty;
            result = 0;
        }

        private void BtnCE_Click(object sender, EventArgs e)
        {
            //Nastavení tlačítka CE (Clear Entry)
            TxtDisplay1.Text = "0";
        }

        private void BtnOperations_Click(object sender, EventArgs e)
        {
            //Fungování a prezentování speciálních matematických operací
            Button button = (Button)sender;
            operation = button.Text;

            string resultText = "";
            switch (operation)
            {
                case "√x":
                    resultText = CalculatorOperations.PerformSquareRoot(TxtDisplay1.Text);
                    TxtDisplay1.Text = Convert.ToString(Math.Sqrt(Double.Parse(TxtDisplay1.Text)));
                    break;
                case "x²":
                    resultText = CalculatorOperations.PerformSquare(TxtDisplay1.Text);
                    TxtDisplay1.Text = Convert.ToString(Convert.ToDouble(TxtDisplay1.Text) * Convert.ToDouble(TxtDisplay1.Text));
                    break;
                case "⅟x":
                    resultText = CalculatorOperations.PerformReciprocal(TxtDisplay1.Text);
                    TxtDisplay1.Text = Convert.ToString(1.0 / Convert.ToDouble(TxtDisplay1.Text));
                    break;
                case "%":
                    resultText = CalculatorOperations.PerformPercentage(TxtDisplay1.Text);
                    TxtDisplay1.Text = Convert.ToString(Convert.ToDouble(TxtDisplay1.Text) / Convert.ToDouble(100));
                    break;
                case "±":
                    resultText = CalculatorOperations.PerformNegation(TxtDisplay1.Text);
                    TxtDisplay1.Text = Convert.ToString(-1 * Convert.ToDouble(TxtDisplay1.Text));
                    break;
            }

            TxtDisplay2.Text = resultText;
            RtBoxDisplayHistory.AppendText($"{resultText}={TxtDisplay1.Text} \n");
        }

        private void TxtDisplay1_KeyP(object sender, KeyPressEventArgs e)
        {
            //Určuje funkčnost vstupů přes klávesnici do TxtDisaplaye1
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
            {
                e.Handled = true;
            }

            //Povolí pouze jednu desetinnou čárku
            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
        }

        private void TxtDisplay_TextChanged(object sender, EventArgs e)
        {
            //Zajišťuje aby se přes copypaste dali vkládat pouze čísla(a +-E a ∞) 
            if (System.Text.RegularExpressions.Regex.IsMatch(TxtDisplay1.Text, "[^0-9,E+∞-]"))
            {
                TxtDisplay1.Text = "Neplatné zadání";
                MessageBox.Show("Prosím vložte pouze čísla.");
                TxtDisplay1.Text = string.Empty;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //Nastasvení responzivity většiny klávesnice
            keyHandler.HandleKeyPress(e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            //Nastavení responsivity Entru
            if (keyData == Keys.Enter)
            {
                BtnEquals.PerformClick();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }


        private void BtnNum_Click(object sender, EventArgs e)
        {
            //Když TxtDisplay1 prezentuje 0 při zadání další hodnoty zmizí 
            if (TxtDisplay1.Text == "0" || enterValue) TxtDisplay1.Text = string.Empty;

            enterValue = false;
            Button button = (Button)sender;

            //Definuje tlačítka 0-9 a desetinnou čárku 
            if (button.Text == ",")
            {
                if (!TxtDisplay1.Text.Contains(","))
                    TxtDisplay1.Text = TxtDisplay1.Text + button.Text;
            }
            else TxtDisplay1.Text = TxtDisplay1.Text + button.Text;


        }
    }
}
