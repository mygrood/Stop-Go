using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace gamedeath
{
    /// <summary>
    /// Логика взаимодействия для QQQxaml.xaml
    /// </summary>
    public partial class QQQxaml : Window
    {
        
        public static timet Q;
        public QQQxaml()
        {
            InitializeComponent();
           
            
        }

        private void submit_Click(object sender, RoutedEventArgs e)
        {
            if (Q.quest.answ==null && answQ.Text.Length < 200)
            {                
                    MessageBox.Show("Ваш ответ должен быть длиннее.");                
            }
            else if (Q.quest.answ == answQ.Text)
            {
                MessageBox.Show("Отлично! Вы заработали "+Q.quest.reward+" очков.");
                Q.MC.xp += Q.quest.reward;
                Q.process = 1;
                BaseConnect.BaseModel.SaveChanges();
                textQ.Text = Q.quest.text;
                this.Close();
                
            }
            else if (Q.quest.answ != answQ.Text && answQ.Text!=null)
            {
                MessageBox.Show("Увы! Вы потеряли " + Q.quest.reward/2 + " очков.", "как грустно",MessageBoxButton.OK, MessageBoxImage.Error);
                Q.MC.xp -= Q.quest.reward/2;
                BaseConnect.BaseModel.SaveChanges();
                textQ.Text = Q.quest.text;
                this.Close();
            }
            else if(answQ.Text != null)
            {
                MessageBox.Show("Введите ответ!");
            }
        }
        void Window_Closing(object sender, CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Вы уверены? Вы можете потерять " + Q.quest.reward / 2 + " очков.", "да всмысле", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                Q.MC.xp -= Q.quest.reward / 2;
                BaseConnect.BaseModel.SaveChanges();
                e.Cancel = true;
                textQ.Text = Q.quest.text;


                this.Hide();


            }
            else
            {
                MessageBox.Show("Введите ответ!");
                e.Cancel = true;

            }

            
           
        }
    }
}
