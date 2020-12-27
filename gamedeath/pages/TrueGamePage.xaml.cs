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
using System.Windows.Media.Animation;
using System.Timers;
using System.Windows.Threading;

namespace gamedeath.pages
{
    /// <summary>
    /// Логика взаимодействия для TrueGamePage.xaml
    /// </summary>
    public partial class TrueGamePage : Page
    {

        //Page 3: TRUEGAMEPAGE

        QQQxaml QQQ = new QQQxaml();

        MC CurPers = BaseConnect.BaseModel.MC.Find(GLOBAL.CurUser);
        
        int CurLVL;
        int NextLVLxp;

        DispatcherTimer generateQuest = new DispatcherTimer();
        DispatcherTimer timer;
        TimeSpan time;

        Color clr = Color.FromRgb(255, 255, 255);

        public TrueGamePage()
        {
            CurPers.xp = 20;
            GLOBAL.CurPage = 3;
            InitializeComponent();

            int xp = CurPers.xp;

            progressBarAnim(0);

            progrXP.Background = new SolidColorBrush(clr);

            //текущая дата
            var datenow = new DispatcherTimer();
            datenow.Interval = new TimeSpan(0, 0, 1);
            datenow.IsEnabled = true;
            datenow.Tick += (o, t) =>
            {
                lblDateNow.Content = DateTime.Now.ToString(); 
                if (CurPers.xp!=xp)
                {
                    progressBarAnim((int)progrXP.Value);
                    xp = CurPers.xp;
                }
            };
            datenow.Start();

            spQ1.Visibility = Visibility.Hidden;
            spQ2.Visibility = Visibility.Hidden;
            spQ2.Visibility = Visibility.Hidden;
            spQ3.Visibility = Visibility.Hidden;
            spQ4.Visibility = Visibility.Hidden;
            spQ5.Visibility = Visibility.Hidden;
            spQ6.Visibility = Visibility.Hidden;


            txbName.Text = CurPers.name;

            




            
            generateQuest.Interval = new TimeSpan(0, 0, 5);
            generateQuest.IsEnabled = true;

            generateQuest.Tick += (o, t) =>
            {
                if (spQ1.Visibility == Visibility.Hidden) //1 задание
                {
                    QuestUp(spQ1,tbWhen1,tbTQuest1,tbDeadline1,tbGO1);
                    
                }
                else if (spQ2.Visibility == Visibility.Hidden) //2 задание
                {
                    QuestUp(spQ2, tbWhen2, tbTQuest2, tbDeadline2, tbGO2);
                }
                else if (spQ3.Visibility == Visibility.Hidden) //3 задание
                {
                    QuestUp(spQ3, tbWhen3, tbTQuest3, tbDeadline3, tbGO3);
                }
                else if (spQ4.Visibility == Visibility.Hidden) //4 задание
                {
                    QuestUp(spQ4, tbWhen4, tbTQuest4, tbDeadline4, tbGO4);
                }
                else if (spQ5.Visibility == Visibility.Hidden) //5 задание
                {
                    QuestUp(spQ5, tbWhen5, tbTQuest5, tbDeadline5, tbGO5);
                }
                else if (spQ6.Visibility == Visibility.Hidden) //6 задание
                {
                    QuestUp(spQ6, tbWhen6, tbTQuest6, tbDeadline6, tbGO6);
                }
                
                //Random rndT = new Random();
                //int rndH = rndT.Next(0, 3);
                //int rndM = rndT.Next(0, 58);
                generateQuest.Interval = new TimeSpan(0, 0, 30);//поменять на часы

            };
            generateQuest.Start();
                             
        }

        public static quest newQuest() //генерация случайного задания
        {
            
            Random rnd = new Random();

            int k = BaseConnect.BaseModel.quest.Count(u => u.idQ > 0);
            int id;

            while (true)
            {
                id = rnd.Next(1, k + 1);
                quest nQ = BaseConnect.BaseModel.quest.FirstOrDefault(q => q.idQ == id);
                if (nQ!=null)
                {
                    return nQ;
                }
            }
           
            
           

           
        }
        private timet QuestToTT() //приклепление задания к персонажу
        {
            quest nQ = newQuest();

            timet tt = new timet()
            {
                idPers = GLOBAL.CurUser,
                idQ = nQ.idQ,
                process = 0
            };

            BaseConnect.BaseModel.timet.Add(tt);
            
            return tt;
        }

        private void QuestUp(StackPanel sp, TextBlock tbWhen, TextBlock tbQuest, TextBlock tbDeadline, Button tbGO) //игра началась
        {
            timet Q = QuestToTT();        
            
            
            sp.Visibility = Visibility.Visible;
            tbWhen.Text = DateTime.Now.ToString("HH:mm:ss");
            
            tbQuest.Text = Q.quest.text;


            
            time = TimeSpan.FromMinutes((double)Q.quest.time);

            timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                tbDeadline.Text = time.ToString("c");
                if (time == TimeSpan.Zero)
                {
                    timer.Stop();
                    MessageBox.Show("Время вышло. Увы! Вы потеряли " + Q.quest.reward / 2 + " очков.", "ауч", MessageBoxButton.OK, MessageBoxImage.Error);
                    CurPers.xp -= Q.quest.reward / 2;
                    progressBarAnim((int)progrXP.Value);
                    tbWhen.Text = null;
                    tbDeadline.Text = null;
                    tbQuest = null;
                    sp.Visibility = Visibility.Hidden;
                }
                time = time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);
            timer.Start();

            


            tbGO.Click += (o, t) =>
            {
                QQQxaml.Q = Q;
                QQQ.textQ.Text = Q.quest.text;
                QQQ.Show();
                timer.Stop();
                sp.Visibility = Visibility.Hidden;
            };


        }

        private void progressBarAnim(int fromXP) //анимация хп
        {
            CurLVL = GLOBAL.curLVL(CurPers.xp);
            NextLVLxp = GLOBAL.nextLVL(CurLVL);

            progrXP.Maximum = NextLVLxp;

            DoubleAnimation progressAnim = new DoubleAnimation();
            progressAnim.From = fromXP;
            progressAnim.To = CurPers.xp;
            progressAnim.Duration = TimeSpan.FromSeconds(2);

            progrXP.BeginAnimation(ProgressBar.ValueProperty, progressAnim);
            progrXP.Value = CurPers.xp;

            lblLVL.Content = CurLVL;
            lblcurXP.Content = CurPers.xp;
            lblNextLVL.Content = CurLVL + 1;
            lblneedXP.Content = NextLVLxp;
        }

        private void btMain_Click(object sender, RoutedEventArgs e)
        {
            generateQuest.Stop();
            timer.Stop();
            CurPers = null;
            NavigationService.Navigate(new startSign());
        }
    }
}
