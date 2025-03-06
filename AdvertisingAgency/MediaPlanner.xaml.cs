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
    public partial class MediaPlanner : Window
    {
        string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=root;Database=AdvertisingAgency;";
        public MediaPlanner()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnUsersList_Click(object sender, RoutedEventArgs e)
        {
            var campaigns = new List<Campaigns>();

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM public.\"AdvertisingCampaigns\";";

                    using (var command = new NpgsqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var campaign = new Campaigns
                            {
                                CampaignID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Budget = reader.GetDecimal(2),
                                StartDate = reader.GetDateTime(3).Date,
                                EndDate = reader.GetDateTime(4).Date,
                                Status = reader.GetString(5)
                            };
                            campaigns.Add(campaign);
                        }
                    }
                }
                DesignsList designsList = new DesignsList(campaigns);
                designsList.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    }
}
