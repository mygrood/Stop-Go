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
    /// Логика взаимодействия для newAcc.xaml
    /// </summary>
    public partial class newAcc : Page
    {
        //Page 2
        public newAcc()
        {
            InitializeComponent();
            GLOBAL.CurPage = 2;
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            log logObj = BaseConnect.BaseModel.log.FirstOrDefault(u => u.login == txbLog.Text);
            if (logObj != null)
            {
                MessageBoxResult result = MessageBox.Show("Пользователь с этим логином уже существует. Хотите войти в систему?", "", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        NavigationService.Navigate(new startSign());
                        break;
                    case MessageBoxResult.No:
                        break;

                }
            }
            else
            {

                if (GLOBAL.isValidEmail(txbEmail.Text))
                {
                    if (pxbPass.Password == pxbPass2.Password)
                    {

                        string checkCode = GLOBAL.generateCode();
                        string etext = "Здравствуйте, " + txbLog.Text + ". Ваш код для регистрации: " + checkCode;
                        GLOBAL.SendMail("mail.inbox.lv", GLOBAL.fromE, GLOBAL.fromPass, txbEmail.Text, "Регистрация в Stop&Go", etext, null);

                        MessageBox.Show("На ваш E-Mail отправлен код для завершения регистрации. Введите его в следующем окне.");

                        string result = Microsoft.VisualBasic.Interaction.InputBox("Введите код:");
                        if (checkCode==result)
                        {
                            log logNewObj = new log()
                            {
                                login = txbLog.Text,
                                pass = pxbPass.Password.GetHashCode(),
                                email = txbEmail.Text,
                                idRole = 4
                            };
                            BaseConnect.BaseModel.log.Add(logNewObj);

                            MC mcNewObj = new MC()
                            {
                                idPers = logNewObj.idPers,
                                name = txbName.Text,
                                xp = 10
                            };

                            BaseConnect.BaseModel.MC.Add(mcNewObj);
                            BaseConnect.BaseModel.SaveChanges();
                            MessageBox.Show("Регистрация пройдена, ура! А теперь войдите!");
                            NavigationService.Navigate(new startSign());
                        }
                        else
                        {
                            MessageBox.Show("Код неверен или E-Mail не существует. Проверьте введённые данные и попробуйте снова.");
                        }
                                           
                           
                    }
                    else
                    {
                        MessageBox.Show("Пароли не совпадают.");
                        pxbPass.Password = null;
                        pxbPass2.Password = null;
                    }

                }
                else
                {
                    MessageBox.Show("Введенный E-mail некорректен.");
                }

                
            }
            
            
           
          
        }

        private void btMain_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new startSign());
        }
    }
}
