using PrizivaNet.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
using System.Windows.Threading;

namespace PrizivaNet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DispatcherTimer Timer;
        public DateTime timerEnd;

        private void Start()
        {
            timerEnd = new DateTime(2023, 12, 1);
        }

        private void Update()
        {
            //TimeSpan delta = timerEnd - DateTime.Now;
            DateTime now = DateTime.Now;

            TimeSpan delta = timerEnd.Subtract(now);
            dateText.Text = (delta.Days.ToString("00" + " дней ") + delta.Hours.ToString("00" + " часов ") + delta.Minutes.ToString("00" + " минут ") + delta.Seconds.ToString("00" + " секунд ") + "до конца осеннего призыва!");
            if (delta.TotalSeconds <= 0)
            {
                dateText.Text = ("The END");
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            Update();
        }

        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new StartPage(MainFrame));
            backBtn.IsEnabled = true;
            Timer = new DispatcherTimer();
            Timer.Interval = new TimeSpan(0, 0, 1);
            Timer.Tick += Timer_Tick;
            Timer.Start();
            Start();

            DataClass data = new DataClass();
            List<Product> products = new List<Product>();
            products = data.LoadProducts(products, "products.json");
            foreach (Product product in products)
            {
                product.CountBasket = 0;
            }
            FileStream fs2 = new FileStream("products.json", FileMode.Truncate);
            {
                JsonSerializer.Serialize(fs2, products);
                fs2.Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                MainFrame.GoBack();
            }

        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            backBtn.IsEnabled = MainFrame.CanGoBack;
            if (MainFrame.Content.GetType() == typeof(LoginUser) || MainFrame.Content.GetType() == typeof(StartPage))
            {
                userFIO.Text = "";
            }
        }
    }
}
