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
    public partial class EditCampaign : Window
    {
        string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=root;Database=AdvertisingAgency;";
        int _campaign_id;
        public EditCampaign(Campaigns campaign)
        {
            InitializeComponent();
            _campaign_id = campaign.CampaignID;
            tbName.Text = campaign.Name;
            tbBudget.Text = campaign.Budget.ToString();
            tbStartDate.Text = campaign.StartDate.ToString();
            tbEndDate.Text = campaign.EndDate.ToString();
            tbStatus.Text = campaign.Status;
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

                    string query = $"CALL update_campaign_status({_campaign_id}, \'{tbStatus.Text}\')";

                    using (var command = new NpgsqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                    MessageBox.Show("Кампания успешно обновлена");
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

                    MessageBoxResult result = MessageBox.Show("Вы уверенны, что хотите удалить кампанию?", "Подтвердите удаление", MessageBoxButton.YesNo);

                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            string query1 = "DELETE FROM public.\"Designs\" WHERE campaign_id = @campaign_id;";
                            string query2 = "DELETE FROM public.\"Payments\" WHERE campaign_id = @campaign_id;";
                            string query3 = "DELETE FROM public.\"ChannelsCampaigns\" WHERE campaign_id = @campaign_id;";
                            string query4 = "DELETE FROM public.\"AudienceCampaigns\" WHERE campaign_id = @campaign_id;";
                            string query5 = "DELETE FROM public.\"Reports\" WHERE campaign_id = @campaign_id;";
                            string query6 = "DELETE FROM public.\"UsersCampaigns\" WHERE campaign_id = @campaign_id;";
                            string query7 = "DELETE FROM public.\"AdvertisingCampaigns\" WHERE campaign_id = @campaign_id";

                            using (var command = new NpgsqlCommand(query1, connection))
                            {
                                command.Parameters.AddWithValue("@campaign_id", _campaign_id);
                                command.ExecuteNonQuery();
                            }
                            using (var command = new NpgsqlCommand(query2, connection))
                            {
                                command.Parameters.AddWithValue("@campaign_id", _campaign_id);
                                command.ExecuteNonQuery();
                            }
                            using (var command = new NpgsqlCommand(query3, connection))
                            {
                                command.Parameters.AddWithValue("@campaign_id", _campaign_id);
                                command.ExecuteNonQuery();
                            }
                            using (var command = new NpgsqlCommand(query4, connection))
                            {
                                command.Parameters.AddWithValue("@campaign_id", _campaign_id);
                                command.ExecuteNonQuery();
                            }
                            using (var command = new NpgsqlCommand(query5, connection))
                            {
                                command.Parameters.AddWithValue("@campaign_id", _campaign_id);
                                command.ExecuteNonQuery();
                            }
                            using (var command = new NpgsqlCommand(query6, connection))
                            {
                                command.Parameters.AddWithValue("@campaign_id", _campaign_id);
                                command.ExecuteNonQuery();
                            }
                            using (var command = new NpgsqlCommand(query7, connection))
                            {
                                command.Parameters.AddWithValue("@campaign_id", _campaign_id);
                                command.ExecuteNonQuery();
                            }
                            MessageBox.Show("Кампания успешно удалена");
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

        private void btnDesign_Click(object sender, RoutedEventArgs e)
        {
            var designs = new List<Designs>();

            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string query = $"SELECT * FROM public.\"Designs\" WHERE campaign_id = {_campaign_id};";
                    using (var command = new NpgsqlCommand(query, connection))
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var design = new Designs
                            {
                                DesignID = reader.GetInt32(0),
                                Type = reader.GetString(1),
                                Description = reader.GetString(2),
                                CampaignID = reader.GetInt32(3),
                            };
                            designs.Add(design);
                        }
                    }

                    CampaignsDesign campaignsDesign = new CampaignsDesign(designs);
                    campaignsDesign.Show();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        private void btnCreateDesign_Click(object sender, RoutedEventArgs e)
        {
            CreateDesign create = new CreateDesign(_campaign_id);
            create.Show();
        }
    }
}
