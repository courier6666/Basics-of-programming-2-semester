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
    /// Логика взаимодействия для AscendWindow.xaml
    /// </summary>
    public partial class AscendWindow : Window
    {
        string connectionString = null;
        SqlCommand Com;
        SqlDataAdapter Data;
        SqlConnection sqlConn;
        public AscendWindow()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string start = StartDateBox.Text.Trim();
            string ID = IDBox.Text.Trim();
            string end = EndDateBox.Text.Trim();
            string query = "INSERT INTO dbo.Ascend (IDAscend, StartDate, FinishDate) " +
            "VALUES(" + ID + ", '" + start + "', '"+ end +"');";
            try
            {
                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                Com = new SqlCommand(query, sqlConn);
                Com.ExecuteNonQuery();
                sqlConn.Close();
                MessageBox.Show("Ascend added!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            string ID = IDBox.Text.Trim();
            string query = "DELETE FROM dbo.Ascend WHERE dbo.Ascend.IDAscend = '" + ID + "';";
            try
            {
                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                Com = new SqlCommand(query, sqlConn);
                Com.ExecuteNonQuery();
                sqlConn.Close();
                MessageBox.Show("Ascend deleted!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            string start = StartDateBox.Text.Trim();
            string end = EndDateBox.Text.Trim();
            string ID = IDBox.Text.Trim();
            try
            {
                bool showMessage = false;
                if (start.Length > 0)
                {
                    string query = "UPDATE dbo.Ascend " +
                    "SET dbo.Ascend.StartDate = +'" + start + "'" +
                    "WHERE  dbo.Ascend.IDAscend = " + ID + ";";
                    sqlConn = new SqlConnection(connectionString);
                    sqlConn.Open();
                    Com = new SqlCommand(query, sqlConn);
                    Com.ExecuteNonQuery();
                    sqlConn.Close();
                    showMessage = true;
                }
                if(end.Length>0)
                {
                    string query = "UPDATE dbo.Ascend " +
                    "SET dbo.Ascend.FinishDate = +'" +end + "'" +
                    "WHERE  dbo.Ascend.IDAscend = " + ID + ";";
                    sqlConn = new SqlConnection(connectionString);
                    sqlConn.Open();
                    Com = new SqlCommand(query, sqlConn);
                    Com.ExecuteNonQuery();
                    sqlConn.Close();
                    showMessage = true;
                }
                if(showMessage) MessageBox.Show("Data updated!");

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
    }
}
