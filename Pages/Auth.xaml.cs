using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp.Pages
{
    public partial class Auth : Page
    {
        int countUnsuccessful = 0;

        public Auth()
        {
            InitializeComponent();
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(tbLogin.Text) && !String.IsNullOrEmpty(pbPassword.Password))
            {
                LoginUser();
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите логин и пароль", "Предупреждение");
                countUnsuccessful++;
                GenerateCaptcha();
                if (countUnsuccessful >= 2)
                {
                    TimerBLock();
                    return;
                }
            }
        }

        private void btnGuest_Click(object sender, RoutedEventArgs e)
        {
            LoadForm("4");
        }

        private void LoginUser()
        {
            string login = tbLogin.Text.Trim();
            string pass = pbPassword.Password.Trim();

            var user = Helper.GetContext().User.Where(p => p.UserLogin == login).FirstOrDefault();
            if (user != null)
            {
                if (user?.UserPassword == pass)
                {
                    if (!CheckingCaptcha())
                    {
                        MessageBox.Show("неверная капча", "Предупреждение");
                        countUnsuccessful++;
                        GenerateCaptcha();
                        if (countUnsuccessful >= 2)
                        {
                            TimerBLock();
                            return;
                        }
                        return;
                    }
                    LoadForm(user.Role.RoleID.ToString());
                    tbLogin.Text = "";
                    pbPassword.Password = "";
                    tboxCaptcha.Text = "";
                    countUnsuccessful = 0;
                    panelCaptcha.Visibility = Visibility.Collapsed;
                }
                else
                {
                    MessageBox.Show("неверный пароль", "Предупреждение");
                    GenerateCaptcha();
                    countUnsuccessful++;
                    if (countUnsuccessful >= 2)
                        TimerBLock();
                }
            }
            else
            {
                MessageBox.Show("пользователя с логином '" + login + "' не существует", "Предупреждение");
                GenerateCaptcha();
                countUnsuccessful++;
                if (countUnsuccessful >= 2)
                    TimerBLock();
                return;
            }
        }

        private async void TimerBLock()
        {
            timePanel.Visibility = Visibility.Visible;
            panel.IsEnabled = false;
            await Task.Factory.StartNew(() =>
            {
                for (int i = 10; i > 0; i--)
                {
                    tblockTimer.Dispatcher.Invoke(() =>
                    {
                        tblockTimer.Text = $"{i} сек";
                    });
                    Task.Delay(1000).Wait();
                }
            });

            timePanel.Visibility = Visibility.Hidden;
            panel.IsEnabled = true;
        }

        private void GenerateCaptcha()
        {
            panelCaptcha.Visibility = Visibility.Visible;

            char[] letters = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();
            Random rand = new Random();

            string word = "";
            for (int j = 1; j <= 4; j++)
            {
                int letter_num = rand.Next(0, letters.Length - 1);
                word += letters[letter_num];
            }

            tblockCaptcha.Text = word;
            tblockCaptcha.TextDecorations = TextDecorations.Strikethrough;
            tboxCaptcha.Text = "";
        }
        private bool CheckingCaptcha() => tblockCaptcha.Text == tboxCaptcha.Text.Trim();
        private void LoadForm(string _role)
        {
            switch (_role)
            {
                // админ
                case "1":
                    //NavigationService.Navigate(new Client(user));
                    MessageBox.Show("успех", "успех");

                    break;
                // клиент 
                case "2":
                    //NavigationService.Navigate(new Admin(user));
                    MessageBox.Show("успех", "успех");

                    break;
                // менедж
                case "3":
                    MessageBox.Show("успех", "успех");

                    break;
                // гость
                case "4":
                    MessageBox.Show("успех", "успех");

                    break;
            }
        }
    }
}

