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

namespace gamedeath.pages
{
    /// <summary>
    /// Логика взаимодействия для mainAdmin.xaml
    /// </summary>
    public partial class mainAdmin : Page
    {
        public mainAdmin()
        {
            InitializeComponent();
            
            switch (GLOBAL.CurRole)
            {
                case 1:
                    txbName.Text = "Главный администратор";
                    break;
                case 2: //админ         
                    txbName.Text = "Администратор";
                    showAll.Visibility = Visibility.Collapsed;
                    showAdmins.Visibility = Visibility.Collapsed;
                    showEditors.Visibility = Visibility.Collapsed;
                    showQuests.Visibility = Visibility.Collapsed;
                    break;
                case 3: //редактор          
                    txbName.Text = "Редактор";
                    showAll.Visibility = Visibility.Collapsed;
                    showAdmins.Visibility = Visibility.Collapsed;
                    showEditors.Visibility = Visibility.Collapsed;
                    showUsers.Visibility = Visibility.Collapsed;
                    break;
                
                default:
                    break;
            }
        }

        private void showAll_Click(object sender, RoutedEventArgs e)
        {
            dgAccountList.Visibility = Visibility.Visible;
            dgQuests.Visibility = Visibility.Hidden;

            dgAccountList.Columns[3].Visibility = Visibility.Hidden;
            dgAccountList.Columns[4].Visibility = Visibility.Hidden;
            dgAccountList.ItemsSource = BaseConnect.BaseModel.log.ToList();
            log l = new log();
        }

        private void showUsers_Click(object sender, RoutedEventArgs e)
        {
            dgAccountList.Visibility = Visibility.Visible;
            dgQuests.Visibility = Visibility.Hidden;

            List<log> users = BaseConnect.BaseModel.log.Where(u => u.idRole == 4).ToList();            
            dgAccountList.ItemsSource = users;
            dgAccountList.Columns[3].Visibility = Visibility.Visible;
            dgAccountList.Columns[4].Visibility = Visibility.Visible;
        }

        private void showEditors_Click(object sender, RoutedEventArgs e)
        {
            dgAccountList.Visibility = Visibility.Visible;
            dgQuests.Visibility = Visibility.Hidden;

            dgAccountList.Columns[3].Visibility = Visibility.Hidden;
            dgAccountList.Columns[4].Visibility = Visibility.Hidden;
            List<log> users = BaseConnect.BaseModel.log.Where(u => u.idRole == 3).ToList();
            dgAccountList.ItemsSource = users;
        }

        private void showAdmins_Click(object sender, RoutedEventArgs e)
        {
            dgAccountList.Visibility = Visibility.Visible;
            dgQuests.Visibility = Visibility.Hidden;


            dgAccountList.Columns[3].Visibility = Visibility.Hidden;
            dgAccountList.Columns[4].Visibility = Visibility.Hidden;
            List<log> users = BaseConnect.BaseModel.log.Where(u => u.idRole == 2).ToList();
            dgAccountList.ItemsSource = users;
        }

        private void showQuests_Click(object sender, RoutedEventArgs e)
        {
            dgAccountList.Visibility = Visibility.Hidden;
            dgQuests.Visibility = Visibility.Visible;
            List<quest> q = BaseConnect.BaseModel.quest.ToList();
            dgQuests.ItemsSource = q;
        }

        private void saveIt_Click(object sender, RoutedEventArgs e)
        {
            BaseConnect.BaseModel.SaveChanges();
            MessageBox.Show("Изменения сохранены");
        }

        private void addIt_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteIt_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
