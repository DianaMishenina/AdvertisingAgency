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
    public partial class CreateDesign : Window
    {
        string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=root;Database=AdvertisingAgency;";
        string type;
        string description;
        int _campaign_id;
        public CreateDesign(int campaign_id)
        {
            InitializeComponent();
            type = tbType.Text;
            description = tbDescription.Text;
            _campaign_id = campaign_id;
        }

        private void btnAddDesign_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "CALL add_design(@type, @description, @campaign_id)";

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("type", type);
                        command.Parameters.AddWithValue("description", description);
                        command.Parameters.AddWithValue("campaign_id", _campaign_id);
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Дизайн успешно создан");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
