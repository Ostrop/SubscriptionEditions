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
    /// Логика взаимодействия для UserPanel.xaml
    /// </summary>
    public partial class UserPanel : Window
    {
        Recipients User;
        public UserPanel(Recipients user)
        {
            InitializeComponent();
            User = user;
            UserPhoto.Source = new BitmapImage(new Uri($"/Photos/{user.recipient_id}.png", UriKind.Relative));
            UserPhoto.Stretch = Stretch.UniformToFill;
            SurnameLabel.Content = user.surname;
            NameLabel.Content = user.name;
            PatronymicLabel.Content = user.patronymic;
            AddressLabel.Content = user.address;
            FollowsGrid.ItemsSource = EducPracEntities1.GetContext().Follows.ToList();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            var currentFollows = EducPracEntities1.GetContext().Follows.ToList();
            //currentFollows = currentFollows.Where(p => p.Publications.publication_name.Concat(p.Publications.publication_name)(tb.Text.ToLower())).ToList();

            FollowsGrid.ItemsSource = currentFollows;
        }
    }
}
