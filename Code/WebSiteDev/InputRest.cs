using System;
using System.Linq;
using System.Windows.Forms;

namespace WebSiteDev
{
    public static class InputRest
    {
        public static void FirstLetter(TextBox textBox)
        {
            if (textBox.Text.Length > 0)
            {
                string text = textBox.Text;
                textBox.Text = char.ToUpper(text[0]) + text.Substring(1);
                textBox.SelectionStart = textBox.Text.Length;
            }
        }

        public static void EmailInput(KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= 'A' && e.KeyChar <= 'Z') || (e.KeyChar >= 'a' && e.KeyChar <= 'z') || char.IsDigit(e.KeyChar) || e.KeyChar == '.' || e.KeyChar == '-' ||e.KeyChar == '_' ||e.KeyChar == '\b'))
            {
                e.Handled = true;
            }
        }

        public static void OnlyRussian(KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= 'А' && e.KeyChar <= 'я') || e.KeyChar == 'Ё' || e.KeyChar == 'ё' || e.KeyChar == '\b'))
            {
                e.Handled = true;
            }
        }

        public static void OnlyRussianAndDash(KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= 'А' && e.KeyChar <= 'я') || e.KeyChar == 'Ё' || e.KeyChar == 'ё' || e.KeyChar == '-' || e.KeyChar == '\b'))
            {
                e.Handled = true;
            }    
        }

        public static void OnlyEnglishAndSpecial(KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= 'A' && e.KeyChar <= 'z') || char.IsPunctuation(e.KeyChar) || e.KeyChar == '\b'))
            {
                e.Handled = true;
            }
        }

        public static void OnlyEnglish(KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= 'A' && e.KeyChar <= 'z') || e.KeyChar == '\b'))
            {
                e.Handled = true;
            }
        }

        public static void EnglishDigitsAndSpecial(KeyPressEventArgs e)
        {
            const string allowedSpecial = "!@#$%^&*()_+-=[]{}|;:,.<>?";
            if (!((e.KeyChar >= 'A' && e.KeyChar <= 'z') || char.IsDigit(e.KeyChar) || allowedSpecial.Contains(e.KeyChar) || e.KeyChar == '\b'))
            {
                e.Handled = true;
            }
        }

        public static void AllowAll(KeyPressEventArgs e)
        {
            e.Handled = false;
        }

        public static void OnlyNumbers(KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar) || e.KeyChar == '\b'))
            {
                e.Handled = true;
            }
        }

        public static void RussianEnglishAndDigits(KeyPressEventArgs e)
        {
            if (!((e.KeyChar >= 'А' && e.KeyChar <= 'я') || e.KeyChar == 'Ё' || e.KeyChar == 'ё' || (e.KeyChar >= 'A' && e.KeyChar <= 'z') || char.IsDigit(e.KeyChar) || e.KeyChar == '\b'))
            {
                e.Handled = true;
            }
        }
    }
}
