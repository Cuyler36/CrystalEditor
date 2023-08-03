using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace CrystalEditor.WPF.Controls
{
    public class NumericTextBox : TextBox
    {
        [Category("Behavior")] public int MinValue = int.MinValue;
        [Category("Behavior")] public int MaxValue = int.MaxValue;
        public int Value { get; private set; }

        public NumericTextBox()
        {
            Masking.SetMask(this, "^[0-9]+$");
            TextChanged += (_, __) => OnTextChanged();
        }

        private void OnTextChanged()
        {
            if (!int.TryParse(Text, out var value)) return;

            if (value < MinValue) Text = MinValue.ToString();
            else if (value > MaxValue) Text = MaxValue.ToString();
            else Value = value;
        }
    }
}
