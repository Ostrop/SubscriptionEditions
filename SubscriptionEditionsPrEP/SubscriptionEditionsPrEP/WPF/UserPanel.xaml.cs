using Microsoft.Win32;
using SubscriptionEditionsPrEP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
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
        FollowAdd followAddWindow;
        DeleteFollow followDeleteWindow;
        public UserPanel(Recipients user)
        {
            InitializeComponent();
            this.ResizeMode = ResizeMode.NoResize;
            User = user;
            UserPhoto.Source = new BitmapImage(new Uri(User.Image, UriKind.Relative));
            UserPhoto.Stretch = Stretch.UniformToFill;
            SurnameLabel.Content = user.surname;
            NameLabel.Content = user.name;
            PatronymicLabel.Content = user.patronymic;
            AddressLabel.Content = user.address;
            FollowsGrid.ItemsSource = EducPracEntities1.GetContext().Follows.ToList();
            List<string> list = new List<string>();
            list.Add("");
            list.Add("Газета");
            list.Add("Журнал");
            CMB.ItemsSource = list;
            Amount.Text = FAmount.Text = EducPracEntities1.GetContext().Follows.ToList().Count.ToString();
        }

        private void FormUpdate()
        {
            FollowsGrid.ItemsSource = EducPracEntities1.GetContext().Follows.ToList();
            CMB.SelectedIndex = 0;
            CheckB.IsChecked = false;
            TBSearch.Text = string.Empty;

        }

        private void Search()
        {
            var currentFollows = EducPracEntities1.GetContext().Follows.ToList();

            if (CMB.SelectedIndex > 0)
                currentFollows = currentFollows.Where(p => p.Publications.publication_type.Contains(CMB.SelectedItem.ToString())).ToList();

            if (CheckB.IsChecked == true)
                currentFollows = currentFollows.Where(p => DateTime.Now <= (Convert.ToDateTime(p.start_date).AddMonths((int)p.period))).ToList();

            currentFollows = currentFollows.Where(p => p.Publications.publication_name.Contains(TBSearch.Text)).ToList();
            if (currentFollows.Count == 0)
                NoFindTB.Visibility = Visibility.Visible;
            else
                NoFindTB.Visibility = Visibility.Hidden;
            FollowsGrid.ItemsSource = currentFollows;
            Amount.Text = currentFollows.Count.ToString(); 
            FAmount.Text = EducPracEntities1.GetContext().Follows.ToList().Count.ToString();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Search();
        }
        private void CMB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Search();
        }

        private void NewFollowButton(object sender, RoutedEventArgs e)
        {
            followAddWindow = new FollowAdd(User);
            followAddWindow.ShowDialog();
            FormUpdate();
        }
        
        private void DeleteFollowButton(object sender, RoutedEventArgs e)
        {
            followDeleteWindow = new DeleteFollow(User);
            followDeleteWindow.ShowDialog();
            FormUpdate();
        }

        private void CheckB_Click(object sender, RoutedEventArgs e)
        {
            Search();
        }

        private void UserPhoto_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Изменить фотографию?", "Изменение фотографии", MessageBoxButton.YesNoCancel) == MessageBoxResult.Yes)
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                "Portable Network Graphic (*.png)|*.png";

                if (openFileDialog.ShowDialog() == true)
                {
                    try
                    {
                        SaveFileDialog save = new SaveFileDialog();
                        BitmapImage bi = new BitmapImage();
                        bi.BeginInit();
                        bi.UriSource = new Uri(openFileDialog.FileName, UriKind.RelativeOrAbsolute);
                        bi.EndInit();

                        JpegBitmapEncoder jpg = new JpegBitmapEncoder();
                        jpg.Frames.Add(BitmapFrame.Create(bi));
                        using (FileStream stm = new FileStream($@"{Environment.CurrentDirectory}\..\..\Photos\{User.recipient_id}.jpg", FileMode.Create))
                        {
                            jpg.Save(stm);

                        }
                        UserPhoto.Source = bi;
                    }
                    catch (System.Exception c) { Console.Write("Exception " + c); }

                }
            }
        }
    }
}
