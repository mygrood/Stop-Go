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
    /// Логика взаимодействия для startSign.xaml
    /// </summary>
    public partial class startSign : Page
    {
        //Page 1
        public startSign()
        {
            
            InitializeComponent();
           
            GLOBAL.CurPage = 1;
            GLOBAL.CurRole = -1;
            GLOBAL.CurUser = -1;
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new newAcc());
        }

       
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            int pass = pxbPass.Password.GetHashCode();
            log logObj = BaseConnect.BaseModel.log.FirstOrDefault(u => (u.login == txbLog.Text||u.email==txbLog.Text) && u.pass == pass);
            if (logObj == null)
            {
                MessageBox.Show("Нет такого пользователя или пароль не верен.");
            }
            else
            {
                GLOBAL.CurUser = logObj.idPers;
                GLOBAL.CurRole = logObj.idRole;
                NavigationService.Navigate(new capcha());
            }
        }

        private void btnWriteUs_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new write());
        }

        private void lblNewPass_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new refreshPass());
        }
    }
}
