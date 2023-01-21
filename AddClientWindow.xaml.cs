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

namespace Test_ClientOrder
{
    /// <summary>
    /// Логика взаимодействия для AddClientWindow.xaml
    /// </summary>
    public partial class AddClientWindow : Window
    {
        CustomerDB oldCustomer;
        CustomerModel cm = new CustomerModel();
        public AddClientWindow(object sender, CustomerDB customer)
        {
            InitializeComponent();
            if (sender.ToString () == "System.Windows.Controls.Button: Редактировать клиента")
            {
                oldCustomer = customer;
                this.Title = "Редактирование клиента";
                btnAdd.Content = "Изменить данные";
                textName.Text = customer.Name;
                textAddress.Text = customer.Address;
                textPhone.Text = customer.PhoneNum;
            }
        }

        private void AddEditClient(object sender, RoutedEventArgs e)
        {
            if (textName.Text != ""  && textAddress.Text != "" && textPhone.Text != "")
            {
                try
                {
                    if (this.Title == "Редактирование клиента")
                    {
                        EditClient();
                    }
                    else
                    {
                        AddNew();
                    }
                } catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                    return;
                }
                
            } else
            {
                MessageBox.Show("Не все поля заполнены!");
                return;
            }
            DialogResult = true;
            this.Close();
        }

        private void AddNew()
        {
            CustomerDB newCustomer = new CustomerDB
            {
                Name = textName.Text,
                Address = textAddress.Text,
                PhoneNum = textPhone.Text
            };
            cm.CustomerDBs.Add(newCustomer);
            cm.SaveChanges();
            MessageBox.Show("Клиент добавлен!");
        }
        private void EditClient()
        {
            CustomerDB customer = cm.CustomerDBs.FirstOrDefault(c => c.Id == oldCustomer.Id);
            customer.Name = textName.Text;
            customer.Address = textAddress.Text;
            customer.PhoneNum = textPhone.Text;
            cm.SaveChanges();
            MessageBox.Show("Данные клиента изменены!");
        }
    }
}
