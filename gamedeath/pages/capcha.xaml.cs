using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
    /// Логика взаимодействия для capcha.xaml
    /// </summary>
    public partial class capcha : Page
    {
        string text;
        public capcha()
        {
            InitializeComponent();
            Bitmap bit = this.CreateImage(Convert.ToInt32(img1.Width), Convert.ToInt32(img1.Height));
            img1.Source = convertbitmap(bit);
        }

        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            if (txbcap.Text == this.text)
                switch (GLOBAL.CurPage)
                {
                    case 1:
                        switch (GLOBAL.CurRole)
                        {
                            case 1: //главный админ                                
                                MessageBox.Show("Вы вошли как главный администратор");
                                NavigationService.Navigate(new mainAdmin());
                                break;
                            case 2: //главный админ                                
                                MessageBox.Show("Вы вошли как администратор");
                                NavigationService.Navigate(new mainAdmin());
                                break;
                            case 3: //главный админ                                
                                MessageBox.Show("Вы вошли как редактор");
                                NavigationService.Navigate(new mainAdmin());
                                break;
                            case 4: //Пользователь
                                MC User = BaseConnect.BaseModel.MC.FirstOrDefault(u => u.idPers == GLOBAL.CurUser);
                                string uRin = "Вы вошли как " + User.name;
                                MessageBox.Show(uRin);
                                NavigationService.Navigate(new TrueGamePage());
                                break;
                            default:
                                break;
                        }

                        
                        break;
                    case 2:
                        
                        NavigationService.Navigate(new startSign());
                        break;
                    default:
                        break;
                }
            else
                MessageBox.Show("Ошибка!");
        }

        
        private void btnFresh_Click(object sender, RoutedEventArgs e)
        {
            Bitmap bit = this.CreateImage(Convert.ToInt32(img1.Width), Convert.ToInt32(img1.Height));
            img1.Source = convertbitmap(bit);
        }
        private BitmapImage convertbitmap(Bitmap bit)
        {
            Bitmap BM = bit;
            BitmapImage BMI = new BitmapImage();
            using (MemoryStream memory = new MemoryStream())
            {
                BM.Save(memory, ImageFormat.Bmp);
                memory.Position = 0;
                BMI.BeginInit();
                BMI.StreamSource = memory;
                BMI.CacheOption = BitmapCacheOption.OnLoad;
                BMI.EndInit();
            }
            return BMI;
        }
        private Bitmap CreateImage(int Width, int Height) //создание капчи
        {
            Random rnd = new Random();

            //Создадим изображение
            Bitmap result = new Bitmap(Width, Height);

            //Вычислим позицию текста
            int Xpos = 30;
            int Ypos = 18;

            //Добавим различные цвета ддя текста
            System.Drawing.Brush[] colors = {
                        System.Drawing.Brushes.Black,
                        System.Drawing.Brushes.Red,
                        System.Drawing.Brushes.RoyalBlue,
                        System.Drawing.Brushes.Green,
                        System.Drawing.Brushes.Yellow,
                        System.Drawing.Brushes.White,
                        System.Drawing.Brushes.Tomato,
                        System.Drawing.Brushes.Sienna,
                        System.Drawing.Brushes.Pink };

            //Добавим различные цвета линий
            System.Drawing.Pen[] colorpens = {
                    System.Drawing.Pens.Black,
                    System.Drawing.Pens.Red,
                    System.Drawing.Pens.RoyalBlue,
                    System.Drawing.Pens.Green,
                    System.Drawing.Pens.Yellow,
                    System.Drawing.Pens.White,
                    System.Drawing.Pens.Tomato,
                    System.Drawing.Pens.Sienna,
                    System.Drawing.Pens.Pink };

            //Делаем случайный стиль текста
            System.Drawing.FontStyle[] fontstyle = {
                System.Drawing.FontStyle.Bold,
                System.Drawing.FontStyle.Italic,
                System.Drawing.FontStyle.Regular,
                System.Drawing.FontStyle.Strikeout,
                System.Drawing.FontStyle.Underline};

            //Добавим различные углы поворота текста
            Int16[] rotate = { 1, -1, 2, -2, 3, -3, 4, -4, 5, -5, 6, -6 };

            //Укажем где рисовать
            Graphics g = Graphics.FromImage((System.Drawing.Image)result);

            //Пусть фон картинки будет серым
            g.Clear(System.Drawing.Color.Gray);

            //Делаем случайный угол поворота текста
            g.RotateTransform(rnd.Next(rotate.Length));

            //Генерируем текст
            text = String.Empty;
            string ALF = "7890QWERTYUIOPASDFGHJKLZXCVBNM";
            for (int i = 0; i < 5; ++i)
                text += ALF[rnd.Next(ALF.Length)];

            //Нарисуем сгенирируемый текст
            g.DrawString(text,
            new Font("MV Boli", 60, fontstyle[rnd.Next(fontstyle.Length)]),
            colors[rnd.Next(colors.Length)],
            new PointF(Xpos, Ypos));

            //Добавим немного помех
            //Линии из углов
            g.DrawLine(colorpens[rnd.Next(colorpens.Length)],
            new System.Drawing.Point(0, 0),
            new System.Drawing.Point(Width - 1, Height - 1));
            g.DrawLine(colorpens[rnd.Next(colorpens.Length)],
            new System.Drawing.Point(0, Height - 1),
            new System.Drawing.Point(Width - 1, 0));

            //Белые точки
            for (int i = 0; i < Width; ++i)
                for (int j = 0; j < Height; ++j)
                    if (rnd.Next() % 20 == 0)
                        result.SetPixel(i, j, System.Drawing.Color.White);

            return result;
        }



        
    }
}
