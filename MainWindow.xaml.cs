using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Test_ClientOrder
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public CustomerModel cm;

        public MainWindow()
        {
            InitializeComponent();
            //AddRandomClients();
            //AddRandomOrders();
            GetClients();
            DisableOrderButtons();
            btnAddOrder.IsEnabled = false;
            btnEditClient.IsEnabled = false;
            btnDeleteClient.IsEnabled = false;
        }


        private void GetClients()
        {
            cm = new CustomerModel();
            lbClients.Items.Clear();
            if (cm.CustomerDBs.Count() == 0)
            {
                lbClients.Items.Add("Список клиентов пуст!");
                lbClients.IsEnabled = false;
                btnEditClient.IsEnabled = false;
                btnDeleteOrder.IsEnabled = false;
                return;
            }

            lbClients.IsEnabled = true;

            List<CustomerDB> customers = cm.CustomerDBs.ToList();

            foreach (CustomerDB customer in cm.CustomerDBs.ToList())
            {
                lbClients.Items.Add(customer);
            }
        }
        private void GetClientOrders()
        {
            cm = new CustomerModel();
            lbOrders.Items.Clear();
            CustomerDB selectedCustomer = (CustomerDB)lbClients.SelectedItem;
            tbClient.Text = selectedCustomer.Name;
            EnableCustomersButtons();

            List<OrderDB> orders = (List<OrderDB>)cm.OrderDBs.Where(o => o.CustomerId == selectedCustomer.Id).ToList();

            if (orders.Count() == 0)
            {
                lbOrders.Items.Add("Список заказов пуст!");
                btnAddOrder.IsEnabled = true;
                lbOrders.IsEnabled = false;
                btnEditOrder.IsEnabled = false;
                btnDeleteOrder.IsEnabled = false;
                return;
            }

            lbOrders.IsEnabled = true;
            foreach (OrderDB order in orders)
            {
                lbOrders.Items.Add(order);
            }

            btnAddOrder.IsEnabled = true;
        }
        private void ChangeSelection(object sender, SelectionChangedEventArgs e)
        {
            if (lbClients.SelectedIndex == -1) return;
            GetClientOrders();
        }

        private void AddRandomClients()
        {
            string[] name = { "Иван", "Петр", "Максим", "Сергей", "Андрей", "Геннадий" };
            string[] sename = { "Иванов", "Петров", "Сидоров", "Сергеев", "Максименко", "Васильев", "Гузеев" };
            string[] midname = { "Иванович", "Сергеевич", "Петрович", "Максимович", "Сергеевич", "Андреевич", "Геннадьевич", "Афанасьевич" };
            string[] state = { "г. Москва", "г. Самара", "г. Химки", "г. Ижевск", "г. Альметьевск", "г. Толятти", "г. Харабовск" };

            Random rnd = new Random();

            for (int i = 0; i < 50; i++)
            {
                CustomerDB newCustomer = new CustomerDB
                {
                    Name = name[rnd.Next(0, name.Length - 1)] + " " + sename[rnd.Next(0, sename.Length - 1)] + " " + midname[rnd.Next(0, midname.Length - 1)],
                    Address = state[rnd.Next(0, state.Length - 1)],
                    PhoneNum = "+79" + rnd.Next(0, 999999999)
                };
                cm.CustomerDBs.Add(newCustomer);
                cm.SaveChanges();
            }
        }
        private void AddRandomOrders()
        {
            Random rnd = new Random();

            string[] text = { "Простой заказ на выполнение", "Срочный заказ! Нужно выполнить его быстро!!!", "Этот заказ с низким приоритетом",
            "Вот этот заказ хороший, даем клиенту на него скидку", "Заказ не оплачен"};

            foreach (CustomerDB customer in cm.CustomerDBs.ToList())
            {
                for (int i = 0; i < rnd.Next(0, 100); i++)
                {
                    DateTime temp1 = DateTime.Now;
                    temp1 = temp1.AddDays(rnd.Next(1, 7));
                    DateTime temp2 = DateTime.Now;
                    temp2 = temp2.AddDays(rnd.Next(10, 60));
                    OrderDB order = new OrderDB
                    {
                        CustomerId = customer.Id,
                        Number = rnd.Next(1111, 9999).ToString(),
                        Amount = rnd.Next(10, 999),
                        DueTime = temp1,
                        ProcessedTime = temp2,
                        Description = text[rnd.Next(0, text.Length - 1)]
                    };
                    cm.OrderDBs.Add(order);
                }
            }
            cm.SaveChanges();
        }

        private void AddEditClient(object sender, RoutedEventArgs e)
        {
            AddClientWindow clientWindow = new AddClientWindow(sender, (CustomerDB)lbClients.SelectedItem);
            if ((bool)!clientWindow.ShowDialog()) return;
            GetClients();
            tbClient.Text = "";
            DisableOrderButtons();
            btnAddOrder.IsEnabled = false;
            DisableCustomerButtons();
        }

        private void DeleteClient(object sender, RoutedEventArgs e)
        {
            CustomerDB customer = (CustomerDB)lbClients.SelectedItem;
            string message = "";
            bool haveOrders = false;
            if (customer.OrderDBs.Count > 0)
            {
                message = $"\nУ клиента {customer.OrderDBs.Count} заказов. Все заказы будут также удалены!";
                haveOrders = true;
            }

            if ((DialogResult)System.Windows.MessageBox.Show(
                $"Вы действительно хотите удалить клиента {customer.Name}?{message}",
                "Удаление клиента", MessageBoxButton.YesNo,
                MessageBoxImage.Warning) == System.Windows.Forms.DialogResult.Yes)
            {
                try
                {
                    if (haveOrders)
                    {
                        foreach (OrderDB order in cm.OrderDBs.Where(o => o.CustomerId == customer.Id))
                        {
                            cm.OrderDBs.Remove(order);
                        }
                    }
                    CustomerDB customerDB = cm.CustomerDBs.FirstOrDefault(c => c.Id == customer.Id);
                    cm.CustomerDBs.Remove(customerDB);
                    cm.SaveChanges();
                } catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Ошибка: " + ex.Message);
                    return;
                }
                GetClients();
                DisableOrderButtons();
                btnAddOrder.IsEnabled = false;
                DisableCustomerButtons();
                tbClient.Text = "";
            }
        }

        private void AddEditOrder(object sender, RoutedEventArgs e)
        {
            Order order = new Order(sender, (CustomerDB)lbClients.SelectedItem, (OrderDB)lbOrders.SelectedItem);
            if (!(bool)order.ShowDialog()) return;
            int temp = lbClients.SelectedIndex;
            lbClients.SelectedIndex = -1;
            GetClients();
            lbClients.SelectedIndex = temp;
        }
        
        private void DeleteOrder(object sender, RoutedEventArgs e)
        {
            OrderDB order = (OrderDB)lbOrders.SelectedItem;

        }

        private void ActivateOrderButtons(object sender, SelectionChangedEventArgs e)
        {
            if (lbOrders.SelectedIndex == -1) DisableOrderButtons();
            else EnableOrderButtons();
        }
        private void DisableOrderButtons()
        {
            btnEditOrder.IsEnabled = false;
            btnDeleteOrder.IsEnabled = false;
        }
        private void EnableOrderButtons()
        {
            btnEditOrder.IsEnabled = true;
            btnDeleteOrder.IsEnabled = true;
        }
        private void DisableCustomerButtons()
        {
            btnEditClient.IsEnabled = false;
            btnDeleteClient.IsEnabled = false;
        }
        private void EnableCustomersButtons()
        {
            btnEditClient.IsEnabled = true;
            btnDeleteClient.IsEnabled = true;
        }
    }   
}       
        