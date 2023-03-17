using SubscriptionEditionsPrEP.Classes;
using SubscriptionEditionsPrEP.Models;
using System;
using System.Reflection;
using System.Windows;
using System.Windows.Navigation;

namespace SubscriptionEditionsPrEP.WPF
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        int secs = 10;
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        public Authorization()
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Tick += new EventHandler(timer_Tick);
        }

        /// <summary>
        /// Обработчик нажатия кнопки просмотра пароля
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void aPicture_MouseDown(object sender, RoutedEventArgs e)
        {
            if (PassTextBox.Visibility == Visibility.Hidden)
            {
                PassTextBox.Text = PassBox.Password; // скопируем в TextBox из PasswordBox
                PassTextBox.Visibility = Visibility.Visible; // TextBox - отобразить
                PassBox.Visibility = Visibility.Hidden; // PasswordBox - скрыть
                PassTextBox.SelectionStart = PassTextBox.Text.Length;
                PassTextBox.Focus();
            }
            else
            {
                PassBox.Password = PassTextBox.Text; // скопируем в PasswordBox из TextBox 
                PassTextBox.Visibility = Visibility.Hidden; // TextBox - скрыть
                PassBox.Visibility = Visibility.Visible; // PasswordBox - отобразить
                PassBox.Focus();
                PassBox.GetType().GetMethod("Select", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(PassBox, new object[] { PassTextBox.Text.Length, 0 });
            }
        }
        /// <summary>
        /// Обработчик кнопки "Войти"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnterButton(object sender, RoutedEventArgs e)
        {
            string password;
            if (CaptchaView.Visibility == Visibility.Visible && CaptchaTB.Text != CaptchaView.CaptchaValue)
            {
                WrongCaptcha();
                WrongPassLabel.Visibility = Visibility.Hidden;
                return;
            }
            switch (PassTextBox.Visibility)
            {
                case Visibility.Hidden:
                    password = PassBox.Password;
                    break;
                default:
                    password = PassTextBox.Text;
                    break;
            }

            Recipients user = DataBaseInteraction.CheckUser(LogTB.Text, password);

            if (user == null)
            {
                PassBox.Password = string.Empty;
                PassTextBox.Text = string.Empty;
                switch (CaptchaView.Visibility)
                {
                    case Visibility.Visible:
                        WrongCaptcha();
                        EntBut.IsEnabled = false;
                        WrongPassLabel.Visibility = Visibility.Visible;
                        timer.Start();
                        break;
                    default:
                        CaptchaView.Visibility = Visibility.Visible;
                        CaptchaLabel.Visibility = Visibility.Visible;
                        CaptchaTB.Visibility = Visibility.Visible;
                        WrongPassLabel.Visibility = Visibility.Visible;
                        break;
                }
            }
            else
                OpenProfile(user);
        }
        /// <summary>
        /// Открывает новую форму
        /// </summary>
        /// <param name="user"></param>
        private void OpenProfile(Recipients user)
        {
            //this.Hide();
            switch (user.isAdmin)
            {
                case true:
                    break;
                default:
                    UserPanel userPanel = new UserPanel(user);
                    userPanel.Show();
                    this.Close();
                    break;
            }
        }
        /// <summary>
        /// Метод обновления капчи
        /// </summary>
        private void WrongCaptcha()
        {
            CaptchaTB.Text = string.Empty;
            CaptchaView.ResetCaptcha();
        }
        /// <summary>
        /// Запускает таймер, блокирующий на 10 сек кнопку "Войти"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            secs--;
            if (secs == 0)
            {
                EntBut.IsEnabled = true;
                secs = 10;
                timer.Stop();
            }
        }
    }
}
