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
                string generatedPassword = generatePassword(username, password);

                resultTextBlock.Text = "Wachtwoord niet OK - " + generatedPassword;

                MessageBoxResult answer = MessageBox.Show($"Uw voorgesteld ww: {generatedPassword}. Wil je deze gebruiken?", "Generated Password", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.Yes);
                if (answer == MessageBoxResult.Yes)
                {
                    passwordTextBox.Text = generatedPassword;
                }
                
                resultTextBlock.Foreground = Brushes.Red;
            }
        }

        private string generatePassword(string username, string password)
        {
            // Aanmaken Objects
            StringBuilder sb = new StringBuilder();
            Random rnd = new Random();

            // Pak 5 random letters uit username
            for (int i = 0; i < 5; i++)
            {
                int randomInt = rnd.Next(0, username.Length);
                string randomString = username.Substring(randomInt, 1);

                sb.Append(randomString.ToLower());
            }

            // Pak 5 random getallen
            for (int i = 0; i < 5; i++)
            {
                int randomInt = rnd.Next(1, 10);
                sb.Append(randomInt);
            }

            // Voeg random (tussen 1 en 5) x een uitroepteken toe
            int random = rnd.Next(1, 6);
            for (int i = 0; i < random; i++)
            {
                sb.Append("!");
            }

            // StringBuilder omzetten naar string en retourneren
            return sb.ToString();
        }
    }
}
