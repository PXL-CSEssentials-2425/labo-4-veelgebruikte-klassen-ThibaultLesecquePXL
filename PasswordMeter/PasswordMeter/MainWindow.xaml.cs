using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PasswordMeter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Input velden: userNameTextBox en passwordTextBox
        /// Output veld: resultTextBlock
        /// </summary>

        string username;
        string password;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void passwordMeterButton_Click(object sender, RoutedEventArgs e)
        {
            username = userNameTextBox.Text;
            password = passwordTextBox.Text;

            username = username.Trim();
            password = password.Trim();

            bool containsUsername = password.Contains(username);
            bool containsDigit = false;
            bool contains10Char = password.Length >= 10;

            for (int i = 0; i < password.Length; i++)
            {
                char letter;
                string eerstVolgendeLetter = password.Substring(i, 1);
                letter = char.Parse(eerstVolgendeLetter);

                if (char.IsDigit(letter))
                {
                    containsDigit = true;
                    break;
                }
            }

            if (!containsUsername && containsDigit && contains10Char)
            {
                resultTextBlock.Text = "Wachtwoord OK!";
                resultTextBlock.Foreground = Brushes.Green;
            }
            else if ((!containsUsername && containsDigit) || (!containsUsername && contains10Char) || (containsDigit && contains10Char))
            {
                resultTextBlock.Text = "Wachtwoord bijna OK!";
                resultTextBlock.Foreground = Brushes.Orange;
            }
            else
            {
                resultTextBlock.Text = "Wachtwoord niet OK";
                resultTextBlock.Foreground = Brushes.Red;
            }
        }
    }
}
