## Project name: Client - Order
### Description: Desktop application for accounting customers and their orders with the ability to add, edit, delete customers / orders
---
A list of all customers is displayed on the left side of the application, when selecting any of the customers, a list of his orders is displayed on the right side.
</br>
<div align="center"><img src="https://github.com/de4rbe4r/ClientOrder/blob/master/Files/1.PNG" width="700"/></div>
</br>
When you click on the "Добавить клиента" button, a new window is displayed with the input of information about the client. When a new client is added, a check is made for the existence of the client in the database.
</br>
<div align="center"><img src="https://github.com/de4rbe4r/ClientOrder/blob/master/Files/2.PNG" width="200"/></div>
</br>
When a customer is deleted, all customer orders are deleted.</br>
<div align="center"><img src="https://github.com/de4rbe4r/ClientOrder/blob/master/Files/3.PNG" width="700"/></div>
</br>
When editing a client/order, a new window opens, into which all information about the object being edited is transmitted.</br>
<div align="center"><img src="https://github.com/de4rbe4r/ClientOrder/blob/master/Files/4.PNG" width="700"/></div>

#### Language and tools:
* C#
* Visual Studio
* .NET Framework
* Entity Framework
* MSSQL
* WPF
* XAML
---
P.S.The SQLQueryToCreateDB.sql script was used to create the database. The AddRandomClients, AddRandomOrder methods were created to quickly fill the table
