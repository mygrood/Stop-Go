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
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace gamedeath.pages
{
    /// <summary>
    /// Логика взаимодействия для write.xaml
    /// </summary>
    public partial class write : Page
    {
       
        public write()
        {
            GLOBAL.CurPage = 1;
            InitializeComponent();
        }
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {

            if (txName.Text == null || txEmail == null || txQ == null)
            {
                MessageBox.Show("Заполните все поля");
            }
            else
            {
                string name = txName.Text;
                string toE = txEmail.Text;
                string question = txQ.Text;
                string text = "Спасибо за ваш вопрос, " + name + "! Мы обязательно ответим вам позже.";
                string iui = "Обратная связь";
                

                

                if (GLOBAL.isValidEmail(toE))
                {
                    review r = new review()
                    {
                        Name = name,
                        Email = toE,
                        text = question
                    };
                    BaseConnect.BaseModel.review.Add(r);
                    BaseConnect.BaseModel.SaveChanges();
                    GLOBAL.SendMail("mail.inbox.lv", GLOBAL.fromE, GLOBAL.fromPass, toE, iui, text, null);
                    GLOBAL.SendMail("mail.inbox.lv", GLOBAL.fromE, GLOBAL.fromPass, GLOBAL.fromE, iui, name+" " +toE+" : "+question, null);
                    MessageBox.Show("Ваш вопрос успешно отправлен.");


                    txName.Text = null;
                    txEmail.Text = null;
                    txQ.Text = null;


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
