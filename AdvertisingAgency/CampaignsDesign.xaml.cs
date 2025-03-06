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
    public partial class CampaignsDesign : Window
    {
        private List<Designs> _designs;
        public CampaignsDesign(List<Designs> designs)
        {
            InitializeComponent();
            _designs = designs;
            lbDesigns.ItemsSource = _designs;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void lbDesigns_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbDesigns.SelectedItem is Designs selectedDesign)
            {
                try
                {
                    int authorizationId = selectedDesign.DesignID;

                    CurrentDesigns curDesign = new CurrentDesigns(selectedDesign);
                    curDesign.Show();
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
