using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Dec2Hex.Resources;

namespace Dec2Hex
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Конструктор
        public MainPage()
        {
            InitializeComponent();
        }

        private void Dec2HexButton_Click(object sender, RoutedEventArgs e)
        {
            string dec = Dec2HexField.Text;
            string hex = "";

            // Control
            for (int i = 0; i < dec.Length; i++)
                if (!Char.IsDigit(dec[i]))
                {
                    MessageBox.Show("Строка содержит неверные символы", "Ошибка ввода данных", MessageBoxButton.OK);
                    return;
                }

            try
            {
                hex = DecToBase(Convert.ToInt32(dec), 16);
            }
            catch
            {
                MessageBox.Show("Строка содержит неверные символы", "Ошибка ввода данных", MessageBoxButton.OK);
            }

            if (Convert.ToInt32(dec) == 0)
                Hex2DecField.Text = "0";
            else
                Hex2DecField.Text = hex;
        }

        private void Hex2DecButton_Click(object sender, RoutedEventArgs e)
        {
            string hex = Hex2DecField.Text;
            string dec = "";

            // Control

            try
            {
                dec = Convert.ToInt32(hex, 16).ToString();
            }
            catch
            {
                MessageBox.Show("Строка содержит неверные символы", "Ошибка ввода данных", MessageBoxButton.OK);
            }

            Dec2HexField.Text = dec;
        }

        static string DecToBase(int num_value, int base_value)
        {
            int max_bit = 32;
            int dec_base = 10;
            char[] hexchars = new[] { 'A', 'B', 'C', 'D', 'E', 'F' };
            string result = string.Empty;
            int[] result_array = new int[32];

	    // Преобразование числа
            for (; num_value > 0; num_value /= base_value)
            {
                int i = num_value % base_value;
                result_array[--max_bit] = i;
            }

	    // Преобразование числа к общепринятому стандарту
            for (int i = 0; i < result_array.Length; i++)
            {
                if (result_array[i] >= dec_base)
                    result += hexchars[(int)result_array[i] % dec_base].ToString();
                else
                    result += result_array[i].ToString();
            }

	    // Удаление предворяющих строку нулей
            result = result.TrimStart(new char[] { '0' });
            return result;
        }
    }
}
