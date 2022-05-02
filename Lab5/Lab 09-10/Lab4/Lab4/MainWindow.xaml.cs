﻿using System;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string connectionString = null;
        SqlCommand Com;
        SqlDataAdapter Data;
        SqlConnection sqlConn;

        private void UpdateFun()
        {
            try
            {
                ShowData("SELECT * FROM dbo.Person;", alpinistGrid);
                ShowData("SELECT * FROM dbo.Mountain", mountainGrid);
                ShowData("SELECT * FROM dbo.GroupAlp;", groupGrid);
                ShowData("SELECT * FROM dbo.Ascend", ascendGrid);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            UpdateFun();
        }
        
        public void ShowData(string SqlQuery, DataGrid dataGrid)
        {
            sqlConn = new SqlConnection(connectionString);
            sqlConn.Open();
            Com = new SqlCommand(SqlQuery, sqlConn);
            Data = new SqlDataAdapter(Com);
            DataTable dT = new DataTable();
            Data.Fill(dT);
            dataGrid.ItemsSource = dT.DefaultView;
            sqlConn.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string mountainName = MountainBox.Text.Trim();
            string query = "SELECT DISTINCT dbo.GroupAlp.NameGroup, dbo.Mountain.NameMountain, dbo.Ascend.StartDate " +
            "FROM dbo.PersonInGroup INNER JOIN " +
            "dbo.Person ON dbo.PersonInGroup.IDPerson = dbo.Person.IDPerson INNER JOIN " +
            "dbo.GroupAlp ON dbo.PersonInGroup.IDGroup = dbo.GroupAlp.IDGroup INNER JOIN " +
            "dbo.Ascend INNER JOIN " +
            "dbo.AscendInMountain ON dbo.Ascend.IDAscend = dbo.AscendInMountain.IDAscend INNER JOIN " +
            "dbo.GroupInAscend1 ON dbo.Ascend.IDAscend = dbo.GroupInAscend1.IDAscend INNER JOIN " +
            "dbo.Mountain ON dbo.AscendInMountain.IDMountain = dbo.Mountain.IDMountain ON dbo.GroupAlp.IDGroup = dbo.GroupInAscend1.IDGroup " +
            "WHERE dbo.Mountain.NameMountain LIKE " + "'" + mountainName + "%'" + " ORDER BY dbo.Ascend.StartDate ";
            try
            {
                ShowData(query, outputGrid);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string startDateText = startDateBox.Text.Trim();
            string endDateText = endDateBox.Text.Trim();
            string query = "SELECT DISTINCT TOP (100) PERCENT dbo.Mountain.NameMountain, dbo.Ascend.StartDate, dbo.Person.SurnamePerson, dbo.Person.NamePerson, dbo.Person.IDPerson "+
                   "FROM dbo.PersonInGroup INNER JOIN "+
                  "dbo.Person ON dbo.PersonInGroup.IDPerson = dbo.Person.IDPerson INNER JOIN "+
                  "dbo.GroupAlp ON dbo.PersonInGroup.IDGroup = dbo.GroupAlp.IDGroup INNER JOIN "+
                  "dbo.Ascend INNER JOIN "+
                  "dbo.AscendInMountain ON dbo.Ascend.IDAscend = dbo.AscendInMountain.IDAscend INNER JOIN "+
                  "dbo.GroupInAscend1 ON dbo.Ascend.IDAscend = dbo.GroupInAscend1.IDAscend INNER JOIN "+
                  "dbo.Mountain ON dbo.AscendInMountain.IDMountain = dbo.Mountain.IDMountain ON dbo.GroupAlp.IDGroup = dbo.GroupInAscend1.IDGroup "+
                  "WHERE(dbo.Ascend.StartDate BETWEEN '"+startDateText+"' AND '"+endDateText+"')"+
                  "ORDER BY dbo.Ascend.StartDate";
            try
            {
                ShowData(query, outputGrid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string personBoxText = personBox.Text.Trim();
            string query = "SELECT DISTINCT TOP (100) PERCENT dbo.Mountain.NameMountain, dbo.Ascend.StartDate "+
                  "FROM dbo.PersonInGroup INNER JOIN "+
                  "dbo.Person ON dbo.PersonInGroup.IDPerson = dbo.Person.IDPerson INNER JOIN "+
                  "dbo.GroupAlp ON dbo.PersonInGroup.IDGroup = dbo.GroupAlp.IDGroup INNER JOIN "+
                  "dbo.Ascend INNER JOIN "+
                  "dbo.AscendInMountain ON dbo.Ascend.IDAscend = dbo.AscendInMountain.IDAscend INNER JOIN "+
                  "dbo.GroupInAscend1 ON dbo.Ascend.IDAscend = dbo.GroupInAscend1.IDAscend INNER JOIN "+
                  "dbo.Mountain ON dbo.AscendInMountain.IDMountain = dbo.Mountain.IDMountain ON dbo.GroupAlp.IDGroup = dbo.GroupInAscend1.IDGroup "+
                  "WHERE dbo.Person.IDPerson = '"+personBoxText+"' "+
                  "ORDER BY dbo.Ascend.StartDate";
            try
            {
                ShowData(query, outputGrid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            string startDateText = startDateBox.Text.Trim();
            string endDateText = endDateBox.Text.Trim();
            string query = "SELECT DISTINCT TOP (100) PERCENT dbo.Mountain.NameMountain, dbo.Ascend.StartDate, dbo.GroupAlp.NameGroup, dbo.GroupAlp.IDGroup " +
                   "FROM dbo.PersonInGroup INNER JOIN " +
                  "dbo.Person ON dbo.PersonInGroup.IDPerson = dbo.Person.IDPerson INNER JOIN " +
                  "dbo.GroupAlp ON dbo.PersonInGroup.IDGroup = dbo.GroupAlp.IDGroup INNER JOIN " +
                  "dbo.Ascend INNER JOIN " +
                  "dbo.AscendInMountain ON dbo.Ascend.IDAscend = dbo.AscendInMountain.IDAscend INNER JOIN " +
                  "dbo.GroupInAscend1 ON dbo.Ascend.IDAscend = dbo.GroupInAscend1.IDAscend INNER JOIN " +
                  "dbo.Mountain ON dbo.AscendInMountain.IDMountain = dbo.Mountain.IDMountain ON dbo.GroupAlp.IDGroup = dbo.GroupInAscend1.IDGroup " +
                  "WHERE(dbo.Ascend.StartDate BETWEEN '" + startDateText + "' AND '" + endDateText + "')" +
                  "ORDER BY dbo.Ascend.StartDate";
            try
            {
                ShowData(query, outputGrid);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            AddPersonWindow wind = new AddPersonWindow();
            wind.Show();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            UpdateFun();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            GroupAlpWindow wind = new GroupAlpWindow();
            wind.Show();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            AscendWindow wind = new AscendWindow();
            wind.Show();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            MountainWindow wind = new MountainWindow();
            wind.Show();
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            ShowData("SELECT * FROM dbo.PersonInGroup",outputGrid);
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            ShowData("SELECT * FROM dbo.GroupInAscend1", outputGrid);
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            ShowData("SELECT * FROM dbo.AscendInMountain", outputGrid);
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            PersonInGroupWindow wind = new PersonInGroupWindow();
            wind.Show();
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            GroupInAscend wind = new GroupInAscend();
            wind.Show();
        }

        private void Button_Click_14(object sender, RoutedEventArgs e)
        {
            AscendInMountain wind = new AscendInMountain();
            wind.Show();
        }
    }
}
