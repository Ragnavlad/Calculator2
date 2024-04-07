using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace calc02
{
    public class KeyHandler
    {
        private readonly Dictionary<Keys, Button> keyButtonMappings;

        public KeyHandler(Button btn0, Button btn1, Button btn2, Button btn3, Button btn4,
                          Button btn5, Button btn6, Button btn7, Button btn8, Button btn9,
                          Button btnDesimal, Button btnAdd, Button btnSubtraction,
                          Button btnMultiply, Button btnDivision, Button btnBackSpace,
                          Button btnHistory)
        {
            keyButtonMappings = new Dictionary<Keys, Button>
        {
            { Keys.NumPad0, btn0 },
            { Keys.D0, btn0 },
            { Keys.NumPad1, btn1 },
            { Keys.D1, btn1 },
            { Keys.NumPad2, btn2 },
            { Keys.D2, btn2 },
            { Keys.NumPad3, btn3 },
            { Keys.D3, btn3 },
            { Keys.NumPad4, btn4 },
            { Keys.D4, btn4 },
            { Keys.NumPad5, btn5 },
            { Keys.D5, btn5 },
            { Keys.NumPad6, btn6 },
            { Keys.D6, btn6 },
            { Keys.NumPad7, btn7 },
            { Keys.D7, btn7 },
            { Keys.NumPad8, btn8 },
            { Keys.D8, btn8 },
            { Keys.NumPad9, btn9 },
            { Keys.D9, btn9 },
            { Keys.Decimal, btnDesimal },
            { Keys.Add, btnAdd },
            { Keys.Subtract, btnSubtraction },
            { Keys.Multiply, btnMultiply },
            { Keys.Divide, btnDivision },
            { Keys.Back, btnBackSpace },
            { Keys.H, btnHistory }
        };
        }

        public void HandleKeyPress(KeyEventArgs e)
        {
            if (keyButtonMappings.TryGetValue(e.KeyCode, out Button button))
            {
                button?.PerformClick();
            }
        }
    }


}
