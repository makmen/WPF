using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ExchangeClient
{
    static class ErrorControls
    {

        static public bool NotEmptyPasswordBox(PasswordBox tb)
        {
            bool check = true;
            if (tb.Password == "")
            {
                check = false;
                tb.Background = Brushes.Red;
            }
            else
            {
                tb.Background = (SolidColorBrush)tb.FindResource("LightBrush");
            }

            return check;
        }

        static public bool NotEmptyTextBox(TextBox tb)
        {
            bool check = true;
            if (tb.Text == "")
            {
                check = false;
                tb.Background = Brushes.Red;
            }
            else
            {
                tb.Background = (SolidColorBrush)tb.FindResource("LightBrush");
            }

            return check;
        }

        static public bool NotEmptyDataPicker(DatePicker dp)
        {
            bool check = true;
            if (dp.Text == "")
            {
                check = false;
                dp.Background = Brushes.Red;
            }
            else
            {
                dp.Background = (SolidColorBrush)dp.FindResource("LightBrush");
            }

            return check;
        }

        static public bool RegexTextBox(TextBox tb, string pattern)
        {
            bool check = true;
            if (!Regex.IsMatch(tb.Text, pattern))
            {
                check = false;
                tb.Background = Brushes.Red;
            }
            else
            {
                tb.Background = (SolidColorBrush)tb.FindResource("LightBrush");
            }

            return check;
        }

        static public bool EmptyComboBox(ComboBox cmb)
        {
            bool check = true;
            if (cmb.Text == "")
            {
                check = false;
                cmb.Background = Brushes.Red;
            }
            else
            {
                cmb.Background = (SolidColorBrush)cmb.FindResource("LightBrush");
            }

            return check;
        }

        static public bool TextBoxToDouble(TextBox tb)
        {
            bool check = true;
            double res = 0;
            try
            {
                res = double.Parse(tb.Text);
                tb.Background = (SolidColorBrush)tb.FindResource("LightBrush");
            }
            catch(Exception)
            {
                check = false;
                tb.Background = Brushes.Red;
            }

            return check;
        }

        static public bool TextBoxToInt(TextBox tb, int min = 0, int max = 0)
        {
            bool check = true;
            double res = 0;
            try
            {
                res = int.Parse(tb.Text);
                tb.Background = (SolidColorBrush)tb.FindResource("LightBrush");
                if (min == 0 && min != max) // если заданы параметры min и max
                {
                    if (res < min || res > max)
                    {
                        tb.Background = Brushes.Red;
                        check = false;
                    }
                }
            }
            catch (Exception)
            {
                check = false;
                tb.Background = Brushes.Red;
            }

            return check;
        }

        static public void CountErrors(ref int i, bool check)
        {
            if (!check)
            {
                ++i;
            }
        }
    }
}
