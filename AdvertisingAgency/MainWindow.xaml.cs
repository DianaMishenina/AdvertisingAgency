using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
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
using Npgsql;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AdvertisingAgency
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=root;Database=AdvertisingAgency;";

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            string login = tbLogin.Text;
            string password = tbPassword.Text;

            try { 
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Подключение успешно!");

                    int? autho_id = GetAuthorizationId(connection, login, password);

                    if (autho_id != null)
                    {
                        var userInfo = GetUserInfo(connection, autho_id.Value);

                        if (userInfo != null)
                        {
                            if (userInfo.Value.Role == "клиент")
                            {
                                Customer customer = new Customer(userInfo.Value.Surname, userInfo.Value.Name, userInfo.Value.Patronymic);
                                customer.Show();
                            }
                            if (userInfo.Value.Role == "администратор")
                            {
                                Administrator admin = new Administrator();
                                admin.Show();
                            }  
                            if (userInfo.Value.Role == "аналитик")
                            {
                                Analyst analyst = new Analyst();
                                analyst.Show();
                            }
                            if (userInfo.Value.Role == "медиапланер")
                            {
                                MediaPlanner md = new MediaPlanner();
                                md.Show();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Пользователь не найден.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Неверный логин или пароль.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
        private int? GetAuthorizationId(NpgsqlConnection connection, string login, string password)
        {
            using (var command = new NpgsqlCommand($"SELECT authorization_id FROM public.\"Authorizations\" WHERE login = @login AND password = @password;", connection))
            {
                command.Parameters.AddWithValue("login", login);
                command.Parameters.AddWithValue("password", password);

                var result = command.ExecuteScalar();
                return result != null ? (int?)Convert.ToInt32(result) : null;
            }
        }

        private (string Surname, string Name, string Patronymic, string Role)? GetUserInfo(NpgsqlConnection connection, int autho_id)
        {
            using (var command = new NpgsqlCommand($"SELECT surname, name, patronymic, role FROM public.\"Users\" WHERE authorization_id = {autho_id};", connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return (
                            Surname: reader["surname"].ToString(),
                            Name: reader["name"].ToString(),
                            Patronymic: reader["patronymic"].ToString(),
                            Role: reader["role"].ToString()
                        );
                    }
                }
            }

            return null;
        }
    
        private void btnReg_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
