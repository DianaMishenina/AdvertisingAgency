using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Npgsql;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AdvertisingAgency
{
    public partial class UsersList : Window
    {
        string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=root;Database=AdvertisingAgency;";

        private List<User> _users;
        private int _user_id;
        private int _autho_id;

        public UsersList(List<User> users)
        {
            InitializeComponent();

            _users = users;
            lbUsers.ItemsSource = _users;
        }



        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void lbUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbUsers.SelectedItem is User selectedUser)
            {
                try
                {
                    int authorizationId = selectedUser.AuthoID;
                    var authoInfo = GetAuthorizationById(authorizationId);

                    EditUser editUser = new EditUser(selectedUser, authoInfo.Value.Login, authoInfo.Value.Password);
                    editUser.Show();
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    this.Close();
                }

            }
        }
        private (string Login, string Password)? GetAuthorizationById(int authorizationId)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand($"SELECT login, password FROM public.\"Authorizations\" WHERE authorization_id = {authorizationId};", connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return (Login: reader["login"].ToString(), Password: reader["password"].ToString());

                        }
                    }
                }
            }
            throw new InvalidOperationException($"Authorization with ID {authorizationId} not found.");
        }
    }
}
