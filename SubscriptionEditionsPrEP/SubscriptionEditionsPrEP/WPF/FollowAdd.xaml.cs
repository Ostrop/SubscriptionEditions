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
    /// Логика взаимодействия для FollowAdd.xaml
    /// </summary>
    public partial class FollowAdd : Window
    {
        Recipients User;
        public FollowAdd(Recipients user)
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            User = user;
            var publlist = EducPracEntities1.GetContext().Publications.ToList();
            PublicationsGrid.ItemsSource = publlist;
            List<string> publnames = new List<string>();
            foreach (var item in publlist)
                publnames.Add(item.publication_name);
            PublicationCMB.ItemsSource = publnames;
            PublicationCMB.SelectedIndex = 0;
        }

        private void PriceUpdate()
        {

            if (PeriodCMB.Text != string.Empty)
            {
                ConfBut.IsEnabled = true;
                var publlist = EducPracEntities1.GetContext().Publications.ToList();
                publlist = publlist.Where(p => p.publication_name.Contains(PublicationCMB.SelectedItem.ToString())).ToList();
                PriceTB.Text = (publlist[0].price * Convert.ToInt32(PeriodCMB.Text)).ToString();
            }
            else
            {
                ConfBut.IsEnabled = false;
                PriceTB.Text = string.Empty;
            }
        }

        private void ConfirmButton(object sender, RoutedEventArgs e)
        {
            try
            {
                var currentFollows = EducPracEntities1.GetContext().Follows.ToList();
                int amount = currentFollows.Count;
                currentFollows = currentFollows.Where(p => DateTime.Now <= (Convert.ToDateTime(p.start_date).AddMonths((int)p.period))).ToList();
                currentFollows = currentFollows.Where(p => p.Publications.publication_name.Contains(PublicationCMB.Text)).ToList();
                if (currentFollows.Count >= 1)
                {
                    MessageBox.Show("У вас активна подписка на данное издание", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                using (EducPracEntities1 db = new EducPracEntities1())
                {
                    Follows newfollow = new Follows();
                    newfollow.follow_id = amount;
                    newfollow.period = Convert.ToByte(PeriodCMB.Text);
                    newfollow.publication_id = PublicationCMB.SelectedIndex;
                    newfollow.recipient_id = User.recipient_id;
                    newfollow.start_date = DateTime.Today;
                    db.Follows.Add(newfollow);
                    db.SaveChanges();
                    this.Close();
                }
            }
            catch(Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void PeriodCMB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void PublicationCMB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PriceUpdate();
        }

        private void PeriodCMB_TextChanged(object sender, TextChangedEventArgs e)
        {
            PriceUpdate();
        }
    }
}
