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
    /// Логика взаимодействия для MountainWindow.xaml
    /// </summary>
    public partial class MountainWindow : Window
    {
        string connectionString = null;
        SqlCommand Com;
        SqlDataAdapter Data;
        SqlConnection sqlConn;
        public MountainWindow()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string ID = IDText.Text.Trim();
            string Name = NameText.Text.Trim();
            string Country = CountryText.Text.Trim();
            string Region = RegionText.Text.Trim();
            string Height = HeightText.Text.Trim();
            string query = "INSERT INTO dbo.Mountain (IDMountain,NameMountain,Country,Region, Height) " +
            "VALUES( " + ID + ", '" + Name + "', '" + Country + "', '" + Region + "', '"+Height+"');";
            try
            {
                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                Com = new SqlCommand(query, sqlConn);
                Com.ExecuteNonQuery();
                sqlConn.Close();
                MessageBox.Show("Mounatin added!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            string ID = IDText.Text.Trim();
            string query = "DELETE FROM dbo.Mountain WHERE dbo.Mountain.IDMountain = '" + ID + "';";
            try
            {
                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                Com = new SqlCommand(query, sqlConn);
                Com.ExecuteNonQuery();
                sqlConn.Close();
                MessageBox.Show("Mountain deleted!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            string ID = IDText.Text.Trim();
            string Name = NameText.Text.Trim();
            string Country = CountryText.Text.Trim();
            string Region = RegionText.Text.Trim();
            string Height = HeightText.Text.Trim();
            try
            {
                bool showMessage = false;
                if (Name.Length > 0)
                {
                    string query = "UPDATE dbo.Mountain " +
                    "SET dbo.Mountain.NameMountain = +'" + Name + "'" +
                    "WHERE dbo.Mountain.IDMountain = " + ID + ";";
                    sqlConn = new SqlConnection(connectionString);
                    sqlConn.Open();
                    Com = new SqlCommand(query, sqlConn);
                    Com.ExecuteNonQuery();
                    sqlConn.Close();
                    showMessage = true;
                }
                if (Country.Length > 0)
                {
                    string query = "UPDATE dbo.Mountain " +
                    "SET dbo.Mountain.Country = +'" + Country + "'" +
                    "WHERE dbo.Mountain.IDMountain = " + ID + ";";
                    sqlConn = new SqlConnection(connectionString);
                    sqlConn.Open();
                    Com = new SqlCommand(query, sqlConn);
                    Com.ExecuteNonQuery();
                    sqlConn.Close();
                    showMessage = true;
                }
                if (Region.Length > 0)
                {
                    string query = "UPDATE dbo.Mountain " +
                    "SET dbo.Mountain.Region = +'" + Region + "'" +
                    "WHERE dbo.Mountain.IDMountain = " + ID + ";";
                    sqlConn = new SqlConnection(connectionString);
                    sqlConn.Open();
                    Com = new SqlCommand(query, sqlConn);
                    Com.ExecuteNonQuery();
                    sqlConn.Close();
                    showMessage = true;
                }
                if(Height.Length>0)
                {
                    string query = "UPDATE dbo.Mountain " +
                    "SET dbo.Mountain.Height = +'" + Height + "'" +
                    "WHERE dbo.Mountain.IDMountain = " + ID + ";";
                    sqlConn = new SqlConnection(connectionString);
                    sqlConn.Open();
                    Com = new SqlCommand(query, sqlConn);
                    Com.ExecuteNonQuery();
                    showMessage = true;
                    sqlConn.Close();
                }
                if (showMessage) MessageBox.Show("Data updated!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
