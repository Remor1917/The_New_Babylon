using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        SqlConnection BabylonDB = new SqlConnection(ConfigurationManager.ConnectionStrings["BabylonDB"].ConnectionString);
        public RegistrationPage()
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

        private void Autorization_Click(object sender, RoutedEventArgs e)
        {

            string username = NiggerLogin.Text;
            string password = NiggerPassword.Text;
            string Name = NiggerName.Text;
            string familya = NiggerFamilya.Text;
            string subname = NiggerSubname.Text;



            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(familya) || string.IsNullOrEmpty(subname))
            {
                MessageBox.Show("Заполните все обязательные поля.");
                return;
            }

            string query = $"INSERT INTO Administrator (Login, Password, Imya, Subimya, Otchestvo) VALUES ('{username}','{password}','{Name}','{familya}','{subname}')";
            SqlCommand com = new SqlCommand(query, BabylonDB);

            BabylonDB.Open();

            using (SqlCommand command = new SqlCommand(query, BabylonDB))
            {
                int affectedRows = command.ExecuteNonQuery();

                if (affectedRows > 0)
                {
                    MessageBox.Show("Пользователь успешно зарегистрирован!");
                }
                else
                {
                    MessageBox.Show("Не удалось зарегистрировать пользователя!");
                }
            }
        }
    }
}
