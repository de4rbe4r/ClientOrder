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
    /// Логика взаимодействия для Order.xaml
    /// </summary>
    public partial class Order : Window
    {
        CustomerModel cm = new CustomerModel();
        CustomerDB customer;
        OrderDB oldOrder;
        public Order(object sender, CustomerDB customerDB, OrderDB order)
        {
            
            InitializeComponent();
            if (sender.ToString() == "System.Windows.Controls.Button: Редактировать заказ")
            {
                this.Title = "Редактирование заказа";
                btnAdd.Content = "Изменить данные";
                oldOrder = order;
                textNumber.Text = order.Number;
                textAmount.Text = order.Amount.ToString();
                dateDueTime.SelectedDate = order.DueTime;
                dateProcessedTime.SelectedDate = order.ProcessedTime;
                textDescription.Text = order.Description;
            }
            customer = customerDB;
            foreach (CustomerDB c in cm.CustomerDBs)
            {
                cbClient.Items.Add(c);
                if (c.Id == customer.Id) cbClient.SelectedIndex = cbClient.Items.Count - 1;
            }


        }

        private void AddEdit(object sender, RoutedEventArgs e)
        {
            if (CheckInputs())
            {
                MessageBox.Show("Поля заполнены не верно!");
                return;
            }
            try
            {
                if (this.Title == "Добавление нового заказа") AddOrder();
                else EditOrder();
            } catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
                return;
            }
            DialogResult = true;
            this.Close();
        }
        private void AddOrder()
        {

            OrderDB newOrder = new OrderDB
            {
                CustomerId = customer.Id,
                Number = textNumber.Text,
                Amount = Convert.ToInt32(textAmount.Text),
                DueTime = (DateTime)dateDueTime.SelectedDate,
                ProcessedTime = (DateTime)dateProcessedTime.SelectedDate,
                Description = textDescription.Text
            };
            cm.OrderDBs.Add(newOrder);
            cm.SaveChanges();
            MessageBox.Show("Заказ добавлен!");
        }
        private void EditOrder()
        {
            OrderDB order = cm.OrderDBs.FirstOrDefault(o => o.Id == oldOrder.Id);
            customer = (CustomerDB)cbClient.SelectedItem;
            order.CustomerId = customer.Id;
            order.Number = textNumber.Text;
            order.Amount = Convert.ToInt32(textAmount.Text);
            order.DueTime = (DateTime)dateDueTime.SelectedDate;
            order.ProcessedTime = (DateTime)dateProcessedTime.SelectedDate;
            order.Description = textDescription.Text;
            cm.Entry(order).State = System.Data.Entity.EntityState.Modified;
            cm.SaveChanges();
            MessageBox.Show("Данные по заказу изменены!");
        }
        private bool CheckInputs()
        {
            int temp = 0;
            if (textNumber.Text == "" || !Int32.TryParse(textAmount.Text, out temp) || dateDueTime.SelectedDate > dateProcessedTime.SelectedDate ||
                dateDueTime.SelectedDate == null || dateProcessedTime.SelectedDate == null) return true;
            return false;
    }
    }
}
