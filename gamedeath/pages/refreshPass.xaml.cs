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
    /// Логика взаимодействия для refreshPass.xaml
    /// </summary>
    public partial class refreshPass : Page
    {
        log logObj;
        public refreshPass()
        {
            
            InitializeComponent();
            lblWho.Visibility = Visibility.Hidden;
            lblName.Visibility = Visibility.Hidden;
            dcpYesNo.Visibility = Visibility.Hidden;
            spNewPass.Visibility = Visibility.Hidden;

        }

        private void btnEmail_Click(object sender, RoutedEventArgs e)
        {
            if (GLOBAL.isValidEmail(txbEmail.Text))
            {
                logObj = BaseConnect.BaseModel.log.FirstOrDefault(u => u.email == txbEmail.Text);
                if (logObj != null)
                {
                    lblWho.Content = "Это Ваш персонаж?";
                    lblName.Content = logObj.MC.name;
                    lblWho.Visibility = Visibility.Visible;
                    lblName.Visibility = Visibility.Visible;
                    dcpYesNo.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("Пользователь не найден. Проверьте правильность введенных данных.");
                }
            }
            else
            {
                MessageBox.Show("Введенный E-mail некорректен.");
            }
            
        }

        private void btnY_Click(object sender, RoutedEventArgs e)
        {
            string checkCode = GLOBAL.generateCode();
            string etext = "Здравствуйте, "+ logObj.login+". Ваш код для восстановления пароля: " + checkCode;
            GLOBAL.SendMail("mail.inbox.lv", GLOBAL.fromE, GLOBAL.fromPass, txbEmail.Text, "Восстановление пароля Stop&Go", etext, null);

            MessageBox.Show("На ваш E-Mail отправлен код для восстановления пароля. Введите его в следующем окне.");

            string result = Microsoft.VisualBasic.Interaction.InputBox("Введите код:");
            if (checkCode == result)
            {
                MessageBox.Show("Код верный!");
                spNewPass.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Код неверен. Проверьте введённые данные и попробуйте снова.");
            }
        }

        private void btnN_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Проверьте правильность введенных данных.");
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            logObj.pass = pxbPass.Password.GetHashCode();
            BaseConnect.BaseModel.SaveChanges();
            string etext = "Пароль успешно изменен";
            GLOBAL.SendMail("mail.inbox.lv", GLOBAL.fromE, GLOBAL.fromPass, txbEmail.Text, "Восстановление пароля Stop&Go", etext, null);
            MessageBox.Show("Пароль изменен, ура! А теперь войдите!");
            NavigationService.Navigate(new startSign());
        }

        

        private void btMain_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new startSign());
        }
    }
}
