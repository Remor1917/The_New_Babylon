using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using System.Configuration;

namespace Babylon_V._2._1
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        SqlConnection BabylonDB = new SqlConnection(ConfigurationManager.ConnectionStrings["BabylonDB"].ConnectionString);

        public static class Data
        {
            public static string Login { get; set; }

        }
        public Page1()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.WindowState = WindowState.Minimized;
        }

        private void Register_autorization_click(object sender, RoutedEventArgs e)
        {
            Data.Login = NiggerLogin.Text;
            string password = NiggerPassword.Text;

            if (string.IsNullOrEmpty(NiggerLogin.Text) || string.IsNullOrEmpty(NiggerPassword.Text))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }
            else if (NiggerLogin.Text == "Admin" && NiggerPassword.Text == "qwerty123")
            {
                NavigationService.Navigate(new MainPage());
            }
            else if (NiggerLogin.Text != "Admin" && NiggerPassword.Text != "qwerty123")
            {
                SqlDataAdapter adapter = new SqlDataAdapter();
                DataTable table = new DataTable();

                string zapros = $"SELECT Login , Password FROM Administrator WHERE Login = {Data.Login} and Password = '{password}'";
                SqlCommand com = new SqlCommand(zapros, BabylonDB);

                adapter.SelectCommand = com;
                adapter.Fill(table);

                if (table.Rows.Count == 1)
                {
                    NavigationService.Navigate(new MainPage());

                }
                else
                {
                    MessageBox.Show("Ошибка авторизации! Неверно введён логин или пароль!!!");
                }
            }

        }

        private void NiggerPassword_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
