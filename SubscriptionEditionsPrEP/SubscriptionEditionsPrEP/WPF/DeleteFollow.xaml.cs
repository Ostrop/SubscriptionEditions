using SubscriptionEditionsPrEP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace SubscriptionEditionsPrEP.WPF
{
    /// <summary>
    /// Логика взаимодействия для DeleteFollow.xaml
    /// </summary>
    public partial class DeleteFollow : Window
    {
        Follows deletefollow = new Follows();
        public DeleteFollow(Recipients User)
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            var currentFollows = EducPracEntities1.GetContext().Follows.ToList();
            currentFollows = currentFollows.Where(p => DateTime.Now <= (Convert.ToDateTime(p.start_date).AddMonths((int)p.period))).ToList();
            currentFollows = currentFollows.Where(p => p.recipient_id == User.recipient_id).ToList();
            List<string> publnames = new List<string>();
            foreach (var item in currentFollows)
                publnames.Add(item.Publications.publication_name);
            CBM.ItemsSource = publnames;
            deletefollow.recipient_id = User.recipient_id;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (CBM.Text != string.Empty)
            if (MessageBox.Show("Отменить подписку?", "Внимание!", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
                {
                    var currentFollows = EducPracEntities1.GetContext().Follows.ToList().Where(p => p.recipient_id == deletefollow.recipient_id).ToList();
                    currentFollows = currentFollows.Where(p => DateTime.Now <= (Convert.ToDateTime(p.start_date).AddMonths((int)p.period))).ToList();
                    deletefollow = currentFollows.Where(p => p.Publications.publication_name == CBM.Text).FirstOrDefault();
                    EducPracEntities1.GetContext().Follows.Remove(deletefollow);
                EducPracEntities1.GetContext().SaveChanges();
                this.Close();
            }
        }


        private void CBM_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}
