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
    public partial class DesignsList : Window
    {
        List<Campaigns> _campaigns;
        public DesignsList(List <Campaigns> campaigns)
        {
            InitializeComponent();

            _campaigns = campaigns;
            lbCampaigns.ItemsSource = _campaigns;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void campaignsListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbCampaigns.SelectedItem is Campaigns selectedCampaign)
            {
                try
                {
                    int campaignId = selectedCampaign.CampaignID;

                    EditCampaign editDesign = new EditCampaign(selectedCampaign);
                    editDesign.Show();
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
    }
}
