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
    /// Логика взаимодействия для GroupInAscend.xaml
    /// </summary>
    public partial class GroupInAscend : Window
    {
        string connectionString = null;
        SqlCommand Com;
        SqlDataAdapter Data;
        SqlConnection sqlConn;
        public GroupInAscend()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            string IDAscend= IDAscendBox.Text.Trim();
            string IDGroup = IDGroupBox.Text.Trim();
            string query = "INSERT INTO dbo.GroupInAscend1 (IDGroup, IDAscend) " +
            "VALUES(" + IDGroup + ", " + IDAscend + ");";
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
            string IDGroup = IDGroupBox.Text.Trim();
            string query = "DELETE FROM dbo.GroupInAscend1 WHERE dbo.GroupInAscend1.IDGroup = " + IDGroup + " AND " + "dbo.GroupInAscend1.IDAscend = " + IDAscend + ";";
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
