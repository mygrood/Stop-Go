using gamedeath.pages;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace gamedeath
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 
    
    public partial class MainWindow : Window
    {
       
        public MainWindow()
        {
            InitializeComponent();
            FrameLoad.FrameM = mainfr;
            FrameLoad.FrameM.Navigate(new startSign());
            BaseConnect.BaseModel = new dpmkStopGoEntities();
            
        }

        void Window_Closing(object sender, CancelEventArgs e)
        {
            Application.Current.Shutdown();

        }
    }
}
