using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
    public partial class CurrentDesigns : Window
    {
        string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=root;Database=AdvertisingAgency;";
        int _design_id;
        public CurrentDesigns(Designs designs)
        {
            InitializeComponent();
            tbType.Text = designs.Type;
            tbDescription.Text = designs.Description;
            _design_id = designs.DesignID;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string query = $"UPDATE public.\"Designs\" SET type = \'{tbType.Text}\', description = \'{tbDescription.Text}\'";

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Дизайн успешно обновлен");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    MessageBoxResult result = MessageBox.Show("Вы уверенны, что хотите удалить дизайн?", "Подтвердите удаление", MessageBoxButton.YesNo);

                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            string query1 = "DELETE FROM public.\"Designs\" WHERE design_id = @design_id;";

                            using (var command = new NpgsqlCommand(query1, connection))
                            {
                                command.Parameters.AddWithValue("@design_id", _design_id);
                                command.ExecuteNonQuery();
                            }
                            MessageBox.Show("Дизайн успешно удален");
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
    }
}
