using Npgsql;
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
using System.Windows.Shapes;

namespace AdvertisingAgency
{
    /// <summary>
    /// Логика взаимодействия для Administrator.xaml
    /// </summary>
    public partial class Administrator : Window
    {
        string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=root;Database=AdvertisingAgency;";
        public Administrator()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnUsersList_Click(object sender, RoutedEventArgs e)
        {
            var users = new List<User>();

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM public.\"Users\" WHERE role = \'аналитик\' OR role = 'медиапланер';";
                    using (var command = new NpgsqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var user = new User
                            {
                                UserId = reader.GetInt32(0),
                                Role = reader.GetString(7),
                                Surname = reader.GetString(1),
                                Name = reader.GetString(2),
                                Patronymic = reader.GetString(3),
                                Birthday = reader.GetDateTime(6).Date,
                                Email = reader.GetString(4),
                                PhoneNumber = reader.GetString(5),
                                AuthoID = reader.GetInt32(8)
                            };
                            users.Add(user);
                        }
                    }

                    UsersList usersList = new UsersList(users);
                    usersList.Show();
                }

            } catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: { ex.Message}");
            }
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            AddUser addUser = new AddUser();
            addUser.Show();
        }
    }
}
