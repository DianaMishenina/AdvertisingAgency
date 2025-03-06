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
    public partial class EditUser : Window
    {
        string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=root;Database=AdvertisingAgency;";
        int _user_id;
        public EditUser(User user, string login, string password)
        {
            InitializeComponent();
            _user_id = user.UserId;
            tbSurname.Text = user.Surname;
            tbName.Text = user.Name;
            tbPatronymic.Text = user.Patronymic;
            tbEmail.Text = user.Email;
            tbPhoneNumber.Text = user.PhoneNumber;
            tbBirthday.Text = user.Birthday.Date.ToString();
            tbRole.Text = user.Role;
            tbLogin.Text = login;
            tbPassword.Text = password;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        { 
            this.Close();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    MessageBoxResult result = MessageBox.Show("Вы уверенны, что хотите удалить пользователя?", "Подтвердите удаление", MessageBoxButton.YesNo);
                    
                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            string query = "CALL delete_user(@user_id)";

                            using (var command = new NpgsqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@user_id", _user_id);
                                command.ExecuteNonQuery();
                            }
                            MessageBox.Show("Пользователь успешно удален");
                            this.Close();
                            break;
                        case MessageBoxResult.No:
                            break;
                    }                   
                }
            } 
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }           
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string query = $"UPDATE public.\"Users\" SET surname = \'{tbSurname.Text}\', name = \'{tbName.Text}\', patronymic = \'{tbPatronymic.Text}\', email = \'{tbEmail.Text}\', phone_number = \'{tbPhoneNumber.Text}\' WHERE user_id = {_user_id};";

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Пользователь успешно обновлен");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    }
}
