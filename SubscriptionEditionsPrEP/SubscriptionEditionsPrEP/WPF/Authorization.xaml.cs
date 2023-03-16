using SubscriptionEditionsPrEP.Models;
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
using System.Windows.Shapes;

namespace SubscriptionEditionsPrEP.WPF
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Просмотр и скрытие пароля
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
            }
            else
            {
                PassBox.Password = PassTextBox.Text; // скопируем в PasswordBox из TextBox 
                PassTextBox.Visibility = Visibility.Hidden; // TextBox - скрыть
                PassBox.Visibility = Visibility.Visible; // PasswordBox - отобразить
            }
        }
        private void EnterButton(object sender, RoutedEventArgs e)
        {   
        }
    }
}
