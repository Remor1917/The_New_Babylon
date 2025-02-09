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

namespace Babylon_V._2._1
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        private TextBlock[] _timeOfGame;
        private TextBlock[] _timeForGame;
        private TextBlock[] _ostatok;
        private Rectangle[] _rectangle;


        public MainPage()
        {
            InitializeComponent();

            Reg_Window reg_Window = new Reg_Window();


            _timeOfGame = new TextBlock[]
            {
                TimeOfGame1, TimeOfGame2, TimeOfGame3,
            };

            _timeForGame = new TextBlock[]
            {
                TimeForGame1, TimeForGame2, TimeForGame3,
            };

            _ostatok = new TextBlock[]
            {
                Ostatok1, Ostatok2, Ostatok3,
            };

        }

        private void UP_Button(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void Reg_Baton(object sender, RoutedEventArgs e)
        {
            var reg = new Reg_Window();
            reg.RegistrationClosed += Reg_Window_Closed;
            reg.Show();

        }

        private void Reg_Window_Closed(object sender, EventArgs e)
        {
            var reg_Window = sender as Reg_Window;
            HandleRegData(reg_Window.ValueNC, reg_Window.Value, reg_Window.Date1);
        }

        private void UnReg_Baton(object sender, RoutedEventArgs e)
        {
            var unreg = new UnReg_Window();

            unreg.Show();
        }

        // Метод для обработки данных из Reg_Window и передачи их в соответствующий GroupBox
        private void HandleRegData(int computerNumber, int timeOfGame, DateTime timeForGames)
        {
            // Проверяем, что номер компьютера в допустимом диапазоне
            if (computerNumber >= 1 && computerNumber <= _timeOfGame.Length)
            {

                DateTime dateTime = timeForGames.AddHours(timeOfGame);
                TimeSpan ost = dateTime - DateTime.Now;

                /*DateTime dateTime = timeForGames;
                dateTime = dateTime.AddHours(timeOfGame);
                TimeSpan ost = DateTime.Parse(Convert.ToString(timeOfGame)) - dateTime;*/

                TextBlock timeOfComputer = _timeOfGame[computerNumber - 1];
                TextBlock timeForComputer = _timeForGame[computerNumber - 1];
                TextBlock ostatok = _ostatok[computerNumber - 1];
                Rectangle rectangle = _rectangle[computerNumber - 1];

                ;

                timeOfComputer.Text = "Время брони: " + timeForGames;

                timeForComputer.Text = "Время окончания брони: " + dateTime;

                ostatok.Text = "Остаток времени: " + ost;

                if (ost > TimeSpan.FromMinutes(10))
                {
                    rectangle.Fill = Brushes.Red;
                }
                else
                {
                    rectangle.Fill = Brushes.White;
                }
                // В этом месте можно установить нужные значения в TextBlock или другие элементы GroupBox
                // Например:
                // TextBlock_TimeOfGame.Text = timeOfGame;
                // TextBlock_TimeForGames.Text = timeForGames;
            }
            else
            {
                MessageBox.Show("Неверный номер компьютера.");
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void UpdateTimeRemaining(int computerIndex, DateTime endTime)
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (sender, e) =>
            {
                TimeSpan remainingTime = endTime - DateTime.Now;
                _ostatok[computerIndex].Text = "Остаток времени: " + remainingTime.ToString(@"hh\:mm\:ss");

                if (remainingTime <= TimeSpan.Zero)
                {
                    timer.Stop();
                    
    }

                
            };
            timer.Start();

}

        private void UPed_Button(object sender, RoutedEventArgs e)
        {
            NavigationService.GoForward();
        }
    }
}