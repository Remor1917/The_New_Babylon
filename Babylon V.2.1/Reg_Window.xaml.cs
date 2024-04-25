using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Babylon_V._2._1
{
    /// <summary>
    /// Логика взаимодействия для Reg_Window.xaml
    /// </summary>
    public partial class Reg_Window : Window
    {
        SqlConnection BabylonDB = new SqlConnection(ConfigurationManager.ConnectionStrings["BabylonDB"].ConnectionString);

        private double value1 = 0;
        private double valueNC = 0;
        public DateTime date1;

        public int NumberOfComputers
        {
            get { return ValueNC; }
        }

        public int TimeForGame
        {
            get { return Value; }
        }

        public DateTime Date1
        {
            get { return date1; }
        }



        public Reg_Window()
        {
            InitializeComponent();

            Nigger_Time.IsEnabled = false;
            Time_Now.IsEnabled = false;
            Nigger_Time.Text = "0";
            NumberOfComputer.Text = "0";

            Task.Run(() =>
            {
                while (true)
                {
                    date1 = DateTime.Now;

                    Dispatcher.Invoke(() =>
                    {

                        Time_Now.Text = date1.ToLongTimeString();

                    });

                    Thread.Sleep(1000);

                }

            });


        }




        public int Value
        {
            get
            {
                return (int)value1;
            }
            set
            {
                if (value > 10)
                    value = 10;
                if (value < 0)
                    value = 0;
                value1 = value;
            }
        }

        public int ValueNC
        {
            get
            {
                return (int)valueNC;
            }
            set
            {
                if (value > 10)
                    value = 10;
                if (value < 0)
                    value = 0;
                valueNC = value;
            }
        }

        private void Nigger_Time_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void NumberOfComputer_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void Button_Down_TimeOfComputer_Click(object sender, RoutedEventArgs e)
        {
            if (value1 <= 0)
            {
                MessageBox.Show("Данный ввод запрещен! Ограничение от 1 до 10 часов на клиента!");

                value1 = 0;

                Button_Down_TimeOfComputer.IsEnabled = false;
            }

            if (double.TryParse(Nigger_Time.Text, out value1))
            {
                --value1;

                Nigger_Time.Text = value1.ToString();
            }


        }

        private void Button_Up_TimeOfComputer_Click(object sender, RoutedEventArgs e)
        {

            if (value1 >= 10)
            {
                MessageBox.Show("Данный ввод запрещен! Ограничение от 1 до 10 часов на клиента!");

                value1 = 10;

                Button_Up_TimeOfComputer.IsEnabled = false;
            }

            if (double.TryParse(Nigger_Time.Text, out value1))
            {
                ++value1;

                Nigger_Time.Text = value1.ToString();

            }


        }

        private void Button_Up_NumberOfComputer_Click(object sender, RoutedEventArgs e)
        {

            if (valueNC >= 10)
            {
                MessageBox.Show("Данный ввод запрещен! В каждом зале по 10 компьютеров!");

                valueNC = 10;
            }

            if (double.TryParse(NumberOfComputer.Text, out valueNC))
            {
                ++valueNC;

                NumberOfComputer.Text = valueNC.ToString();
            }


        }

        private void Button_Down_NumberOfComputer_Click(object sender, RoutedEventArgs e)
        {
            if (valueNC <= 0)
            {
                MessageBox.Show("Данный ввод запрещен! В каждом зале по 10 компьютеров!");

                valueNC = 10;
            }


            if (double.TryParse(NumberOfComputer.Text, out valueNC))
            {
                --valueNC;

                NumberOfComputer.Text = valueNC.ToString();
            }


        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string Time_of_game = Time_Now.Text;
            string Time_for_games = Nigger_Time.Text;
            string Number_of_computer = NumberOfComputer.Text;
            string Name = NameClient.Text;
            string SubName = SubnameClient.Text;
            string Otchestvo = OtchestvoClient.Text;
            string EMail = EMailClient.Text;
            string Number = NumberOfPhone.Text;

            string query = $"INSERT INTO ClientDB (Familiya, Imya, Otchestvo, Number, EMail, Time_of_game, Time_for_game, Number_of_computer) VALUES ('{SubName}','{Name}','{Otchestvo}','{Number}','{EMail}','{Time_of_game}','{Time_for_games}','{Number_of_computer}')";
            SqlCommand com = new SqlCommand(query, BabylonDB);

            BabylonDB.Open();

            using (SqlCommand command = new SqlCommand(query, BabylonDB))
            {
                int affectedRows = command.ExecuteNonQuery();

                if (affectedRows > 0)
                {
                    MessageBox.Show("Бронь установлена!");
                }
                else
                {
                    MessageBox.Show("Не удалось установить бронь!");
                }
            }

            RegistrationClosed?.Invoke(this, EventArgs.Empty);
            this.Close();
        }

        public event EventHandler<EventArgs> RegistrationClosed;

        private void NameClient_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void NameClient_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (int.TryParse(e.Text, out int i))
            {
                e.Handled = true;
            }
        }

        private void SubnameClient_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (int.TryParse(e.Text, out int i))
            {
                e.Handled = true;
            }
        }

        private void OtchestvoClient_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (int.TryParse(e.Text, out int i))
            {
                e.Handled = true;
            }
        }

        private void NumberOfPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }
    }
}
