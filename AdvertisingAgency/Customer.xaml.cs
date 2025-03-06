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
    public partial class Customer : Window
    {
        private string _surname;
        private string _name;
        private string _patronymic;
        public Customer(string surname, string name, string patronymic)
        {
            InitializeComponent();
            _surname = surname;
            _name = name;
            _patronymic = patronymic;
            tblGreeting.Text = $"Здравствуйте, {_surname} {_name} {_patronymic}!";
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
