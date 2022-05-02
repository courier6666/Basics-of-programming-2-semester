using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Data.SqlClient;
using System.Windows.Media.Imaging;
using System.Data;
using System.Configuration;
using System.Windows.Shapes;

namespace Lab4
{
    /// <summary>
    /// Логика взаимодействия для AddPersonWindow.xaml
    /// </summary>
    public partial class AddPersonWindow : Window
    {
        string connectionString = null;
        SqlCommand Com;
        SqlDataAdapter Data;
        SqlConnection sqlConn;
        public AddPersonWindow()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
      
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string ID = IDText.Text.Trim();
            string Name = NameText.Text.Trim();
            string Surname = SurnameText.Text.Trim();
            string Address = AddressText.Text.Trim();
            string query = "INSERT INTO dbo.Person (IDPerson,SurnamePerson,NamePerson,AddressPerson) " +
            "VALUES( "+ID+", '" +Name+"', '" + Surname+ "', '" + Address+"');";
            try
            {
                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                Com = new SqlCommand(query, sqlConn);
                Com.ExecuteNonQuery();
                sqlConn.Close();
                MessageBox.Show("Person added!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            string ID = IDText.Text.Trim();
            string query = "DELETE FROM dbo.Person WHERE dbo.Person.IDPerson = '" + ID+"';";
            try
            {
                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                Com = new SqlCommand(query, sqlConn);
                Com.ExecuteNonQuery();
                sqlConn.Close();
                MessageBox.Show("Person deleted!");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            string ID = IDText.Text.Trim();
            string Name = NameText.Text.Trim();
            string Surname = SurnameText.Text.Trim();
            string Address = AddressText.Text.Trim();
            try
            {
                bool showMessage = false;
                if (Name.Length > 0)
                {
                    string query = "UPDATE dbo.Person "+
                    "SET dbo.Person.NamePerson = +'"+Name+"'"+
                    "WHERE dbo.Person.IDPerson = "+ ID +";";
                    sqlConn = new SqlConnection(connectionString);
                    sqlConn.Open();
                    Com = new SqlCommand(query, sqlConn);
                    Com.ExecuteNonQuery();
                    sqlConn.Close();
                    showMessage = true;
                }
                if (Surname.Length > 0)
                {
                    string query = "UPDATE dbo.Person " +
                    "SET dbo.Person.NamePerson = +'" + Surname + "'" +
                    "WHERE dbo.Person.IDPerson = " + ID + ";";
                    sqlConn = new SqlConnection(connectionString);
                    sqlConn.Open();
                    Com = new SqlCommand(query, sqlConn);
                    Com.ExecuteNonQuery();
                    sqlConn.Close();
                    showMessage = true;
                }
                if (Address.Length > 0)
                {
                    string query = "UPDATE dbo.Person " +
                    "SET dbo.Person.NamePerson = +'" + Address + "'" +
                    "WHERE dbo.Person.IDPerson = " + ID + ";";
                    sqlConn = new SqlConnection(connectionString);
                    sqlConn.Open();
                    Com = new SqlCommand(query, sqlConn);
                    Com.ExecuteNonQuery();
                    sqlConn.Close();
                    showMessage = true;
                }
                if (showMessage) MessageBox.Show("Data updated!"); 
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
