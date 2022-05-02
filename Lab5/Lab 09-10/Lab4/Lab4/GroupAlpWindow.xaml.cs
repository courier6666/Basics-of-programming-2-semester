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
    /// Логика взаимодействия для GroupAlpWindow.xaml
    /// </summary>
    public partial class GroupAlpWindow : Window
    {
        string connectionString = null;
        SqlCommand Com;
        SqlDataAdapter Data;
        SqlConnection sqlConn;
        public GroupAlpWindow()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string ID = IDBox.Text.Trim();
            string Name = NameBox.Text.Trim();
            string query = "INSERT INTO dbo.GroupAlp (IDGroup, NameGroup) " +
            "VALUES("+ID+", '"+Name+"');";
            try
            {
                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                Com = new SqlCommand(query, sqlConn);
                Com.ExecuteNonQuery();
                sqlConn.Close();
                MessageBox.Show("Group added!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            string ID = IDBox.Text.Trim();
            string query = "DELETE FROM dbo.GroupAlp WHERE dbo.GroupAlp.IDGroup = '" + ID + "';";
            try
            {
                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                Com = new SqlCommand(query, sqlConn);
                Com.ExecuteNonQuery();
                sqlConn.Close();
                MessageBox.Show("Group deleted!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            string Name = NameBox.Text.Trim();
            string ID = IDBox.Text.Trim();
            try
            {
                bool showMessage = false;
                if (Name.Length > 0)
                {
                    string query = "UPDATE dbo.GroupAlp " +
                    "SET dbo.GroupAlp.NameGroup = +'" + Name + "'" +
                    "WHERE  dbo.GroupAlp.IDGroup = " + ID + ";";
                    sqlConn = new SqlConnection(connectionString);
                    sqlConn.Open();
                    Com = new SqlCommand(query, sqlConn);
                    Com.ExecuteNonQuery();
                    sqlConn.Close();
                    showMessage = true;
                }
                if (showMessage)MessageBox.Show("Data updated!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
