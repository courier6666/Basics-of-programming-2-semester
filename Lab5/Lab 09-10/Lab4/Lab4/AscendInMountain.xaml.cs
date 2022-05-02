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
    /// Логика взаимодействия для AscendInMountain.xaml
    /// </summary>
    public partial class AscendInMountain : Window
    {
        string connectionString = null;
        SqlCommand Com;
        SqlDataAdapter Data;
        SqlConnection sqlConn;
        public AscendInMountain()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string IDAscend = IDAscendBox.Text.Trim();
            string IDMountain = IDMountainBox.Text.Trim();
            string query = "INSERT INTO dbo.AscendInMountain (IDAscend, IDMountain) " +
            "VALUES(" + IDAscend + ", " + IDMountain + ");";
            try
            {
                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                Com = new SqlCommand(query, sqlConn);
                Com.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            string IDAscend = IDAscendBox.Text.Trim();
            string IDMountain = IDMountainBox.Text.Trim();
            string query = "DELETE FROM dbo.AscendInMountain WHERE dbo.AscendInMountain.IDAscend = " + IDAscend + " AND " + "dbo.AscendInMountain.IDMountain = " + IDMountain + ";";
            try
            {
                sqlConn = new SqlConnection(connectionString);
                sqlConn.Open();
                Com = new SqlCommand(query, sqlConn);
                Com.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
