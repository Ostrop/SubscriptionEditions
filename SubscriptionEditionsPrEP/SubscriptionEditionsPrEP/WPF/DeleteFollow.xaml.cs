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
    /// Логика взаимодействия для DeleteFollow.xaml
    /// </summary>
    public partial class DeleteFollow : Window
    {
        Follows deletefollow;
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
            CBM.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Отменить подписку?", "Внимание!", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
            {
                EducPracEntities1.GetContext().Follows.Remove(deletefollow);
                EducPracEntities1.GetContext().SaveChanges();
                this.Close();
            }
        }


        private void CBM_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var currentFollows = EducPracEntities1.GetContext().Follows.ToList();
            currentFollows = currentFollows.Where(p => DateTime.Now <= (Convert.ToDateTime(p.start_date).AddMonths((int)p.period))).ToList();
            deletefollow = currentFollows.Where(p => p.Publications.publication_name == CBM.Text).FirstOrDefault();
        }
    }
}
