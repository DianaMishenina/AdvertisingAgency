using Npgsql;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using System.Xml.Linq;

namespace AdvertisingAgency
{
    public partial class AddUser : Window
    {
        string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=root;Database=AdvertisingAgency;";

        public AddUser()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                string surname = tbSurname.Text;
                string name = tbName.Text;
                string patronymic = tbPatronymic.Text;
                string email = tbEmail.Text;
                string phone_number = tbPhoneNumber.Text;
                string role = cbRole.Text;
                string login = tbLogin.Text;
                string password = tbPassword.Text;
                DateTime? birthday = dpBirthday.SelectedDate;

                string formattedDate = birthday.Value.Date.ToString("yyyy-MM-dd");
                //MessageBox.Show(formattedDate);

                if (birthday == null)
                {
                    MessageBox.Show("Пожалуйста, укажите дату рождения.");
                    return;
                }

                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "CALL add_user(\'@surname\', \'@name\', \'@patronymic\', \'@email\', \'@phone_number\', \'birthday\', \'@role\', \'@login\', \'@password\')";

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@surname", surname);
                        command.Parameters.AddWithValue("@name", name);
                        command.Parameters.AddWithValue("@patronymic", patronymic);
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@phone_number", phone_number);
                        command.Parameters.AddWithValue("@birthday", formattedDate);
                        command.Parameters.AddWithValue("@role", role);
                        command.Parameters.AddWithValue("@login", login);
                        command.Parameters.AddWithValue("@password", password);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    }
}
