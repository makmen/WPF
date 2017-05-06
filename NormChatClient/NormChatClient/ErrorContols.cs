using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NormChatClient
{
    static class ErrorControls
    {

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
                tb.Background = (LinearGradientBrush)tb.FindResource("LightBrush");
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
                tb.Background = (LinearGradientBrush)tb.FindResource("LightBrush");
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
