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

namespace Prac3
{
    /// <summary>
    /// Логика взаимодействия для Administration.xaml
    /// </summary>
    public partial class Administration : Window
    {
        string connectionString = null;
        int LenTable,index;
        SqlCommand Com;
        SqlDataAdapter Data;
        SqlConnection sqlConn;
        DataTable dT;
        int count;
        public Administration()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            LenTable = 0;
            index = 0;
            count = 0;
            dT = new DataTable();
            RealAdminPasswd.Password = ""; RealAdminPasswd.IsEnabled = false;
            NewAdminPasswd.Password = ""; NewAdminPasswd.IsEnabled = false;
            NewAdminPasswd2.Password = ""; NewAdminPasswd2.IsEnabled = false;
            Prev.IsEnabled = false; Next.IsEnabled = false;
            UpdatePasswd.IsEnabled = false;
            AddUser.IsEnabled = false;
            CorrectStatusBtn.IsEnabled = false;
            CorrectRestrictionBtn.IsEnabled = false;
            dT.Clear();
            dataGrid.ItemsSource = dT.DefaultView;
            AdminPasswd.Password = "";
            UsersLogins.ItemsSource = "";
        }

        private void ExitFromSystem_Click(object sender, RoutedEventArgs e)
        {
            RealAdminPasswd.Password = ""; RealAdminPasswd.IsEnabled = false;
            NewAdminPasswd.Password = ""; NewAdminPasswd.IsEnabled = false;
            NewAdminPasswd2.Password = ""; NewAdminPasswd2.IsEnabled = false;
            Prev.IsEnabled = false; Next.IsEnabled = false;
            UpdatePasswd.IsEnabled = false;
            AddUser.IsEnabled = false;
            CorrectStatusBtn.IsEnabled = false;
            CorrectRestrictionBtn.IsEnabled = false;
            dT.Clear();
            dataGrid.ItemsSource = dT.DefaultView;
            AdminPasswd.Password = "";
            UsersLogins.Items.Clear();
            UsersLogins.ItemsSource = "";
        }

        private void UpdatePasswd_Click(object sender, RoutedEventArgs e)
        {
            sqlConn = new SqlConnection(connectionString);
            sqlConn.Open();
            String strQ;
            String RealPass = RealAdminPasswd.Password.ToString();
            String NewPass = NewAdminPasswd.Password.ToString();
            String NewPass2 = NewAdminPasswd2.Password.ToString();
            if ((RealPass == AdminPasswd.Password.ToString()) && (NewPass != "")
            && (NewPass == NewPass2))

            {
                if (sqlConn.State == System.Data.ConnectionState.Open)
                {
                    strQ = "UPDATE MainTable SET Password ='" + NewPass + "'WHERE Login = 'ADMIN'; ";
                    Com = new SqlCommand(strQ, sqlConn);
                    Com.ExecuteNonQuery();
                }
            }
            sqlConn.Close();
        }

        void UpdateDataTable()
        {
            sqlConn = new SqlConnection(connectionString);
            sqlConn.Open();
            if (sqlConn.State == System.Data.ConnectionState.Open)
            {
                Data = new SqlDataAdapter("SELECT Name, Surname, Login, Status, Restriction FROM MainTable", sqlConn);
                dT = new DataTable("Користувачі системи");
                Data.Fill(dT);
                UsersLogins.ItemsSource = null;
                UsersLogins.Items.Clear();
                for (int i = 0;i<dT.Rows.Count;++i)
                {
                    UsersLogins.Items.Add(dT.Rows[i][2]);
                }
                LenTable = dT.Rows.Count;
                dataGrid.ItemsSource = dT.DefaultView;
                UsersLogins.SelectedItem = UsersLogins.Items[index];
                
            }
            sqlConn.Close();
        }


        private void Prev_Click(object sender, RoutedEventArgs e)
        {
            if (index > 0)
            {
                index--;
                UserNameSelected.Content = dT.Rows[index][0].ToString();
                UserSurnameSelected.Content = dT.Rows[index][1].ToString();
                UserLoginSelected.Content = dT.Rows[index][2].ToString();
                UserStatusSelected.Content = dT.Rows[index][3].ToString();
                UserRestrictionSelected.Content = dT.Rows[index][4].ToString();
                UsersLogins.SelectedItem = UsersLogins.Items[index];
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (index < LenTable - 1)
            {
                index++;
                UserNameSelected.Content = dT.Rows[index][0].ToString();
                UserSurnameSelected.Content = dT.Rows[index][1].ToString();
                UserLoginSelected.Content = dT.Rows[index][2].ToString();
                UserStatusSelected.Content = dT.Rows[index][3].ToString();
                UserRestrictionSelected.Content = dT.Rows[index][4].ToString();
                UsersLogins.SelectedItem = UsersLogins.Items[index];
            }
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            String strQ;
            String UserLogin = AddingUserLogin.Text;
            UserLogin.Trim();
            if (UserLogin.Length <= 0) return;
            sqlConn = new SqlConnection(connectionString);
            sqlConn.Open();
            
            try
            {
                if (sqlConn.State == System.Data.ConnectionState.Open)
                {
                    strQ = "INSERT INTO MainTable (Name, Surname, Login, Status, Restriction) values('', '', '" + UserLogin + "', 1, 0); ";
                
                    Com = new SqlCommand(strQ, sqlConn);
                    Com.ExecuteNonQuery();
                }
                UpdateDataTable();
            }
            catch
            {
                MessageBox.Show("Користувача не додано! Можливо такий в базі вже є!");
            }
            sqlConn.Close();
        }

        private void CorrectRestrictionBtn_Click(object sender, RoutedEventArgs e)
        {
            sqlConn = new SqlConnection(connectionString);
            sqlConn.Open();
            String strQ;
            bool UserRestriction = (bool)ChangeRestriction.IsChecked;
            if (sqlConn.State == System.Data.ConnectionState.Open)
            {
                strQ = "UPDATE MainTable SET Restriction ='" + UserRestriction + "' WHERE Login = '" + UsersLogins.SelectedValue.ToString() + "'; ";
            
                Com = new SqlCommand(strQ, sqlConn);
                Com.ExecuteNonQuery();
            }
            sqlConn.Close();
            UpdateDataTable();
        }

        private void CloseWindow_Click(object sender, RoutedEventArgs e)
        {

            this.Close();
        }

        private void AutorBtn_Click(object sender, RoutedEventArgs e)
        {
            sqlConn = new SqlConnection(connectionString);
            sqlConn.Open();
            String login = "ADMIN";
            String passwd = AdminPasswd.Password;
            if (sqlConn.State == System.Data.ConnectionState.Open)
            {
                String strQ = "SELECT Password FROM MainTable WHERE Login='" + login + "';";
                Data = new SqlDataAdapter(strQ, sqlConn);
                dT = new DataTable("Користувачі системи");
                Data.Fill(dT);
                if (dT.Rows.Count == 0)
                    MessageBox.Show("Такого користувача на знайдено");
                else
                {
                        if (dT.Rows[0][0].ToString()==passwd)
                        {
                            RealAdminPasswd.Password = ""; RealAdminPasswd.IsEnabled = true;
                            NewAdminPasswd.Password = ""; NewAdminPasswd.IsEnabled = true;
                            NewAdminPasswd2.Password = ""; NewAdminPasswd2.IsEnabled = true;
                            Prev.IsEnabled = true; Next.IsEnabled = true;
                            UpdatePasswd.IsEnabled = true;
                            AddUser.IsEnabled = true;
                            CorrectStatusBtn.IsEnabled = true;
                            CorrectRestrictionBtn.IsEnabled = true;
                            dT.Clear();
                            dataGrid.ItemsSource = dT.DefaultView;
                            AdminPasswd.Password = "";
                            UsersLogins.ItemsSource = "";
                            UpdateDataTable();
                        }
                        else
                        {
                            count++;

                            String s = "Невірно введений пароль! " +

                            "Помилкове введення No" + count.ToString();

                            MessageBox.Show(s);
                            if (count == 3)
                                System.Windows.Application.Current.Shutdown();
                        }
                }
            }
            sqlConn.Close();
        }

        private void UsersLogins_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            sqlConn = new SqlConnection(connectionString);
            sqlConn.Open();
            if (sqlConn.State == System.Data.ConnectionState.Open)
            {
                Data = new SqlDataAdapter("SELECT Name, Surname, Login, Status, Restriction FROM MainTable", sqlConn);
                dT = new DataTable("Користувачі системи");
                Data.Fill(dT);
                if (!UsersLogins.Items.IsEmpty)
                {
                    for (int i = 0; i < LenTable; ++i)
                    {
                        if (dT.Rows[i][2].ToString() == UsersLogins.SelectedItem.ToString())
                        {
                            index = i;
                            UserNameSelected.Content = dT.Rows[i][0].ToString();
                            UserSurnameSelected.Content = dT.Rows[i][1].ToString();
                            UserLoginSelected.Content = dT.Rows[i][2].ToString();
                            UserStatusSelected.Content = dT.Rows[i][3].ToString();
                            break;
                        }
                    }
                }
            }
            sqlConn.Close();
        }

        private void CorrectStatusBtn_Click(object sender, RoutedEventArgs e)
        {
            sqlConn = new SqlConnection(connectionString);
            sqlConn.Open();
            String strQ;
            bool UserStatus = (bool)ChangeActive.IsChecked;
            if (sqlConn.State == System.Data.ConnectionState.Open)
            {
                strQ = "UPDATE MainTable SET Status ='" + UserStatus + "' WHERE Login='" +
                UsersLogins.SelectedValue.ToString() + "';";
                Com = new SqlCommand(strQ, sqlConn);
                Com.ExecuteNonQuery();
            }
            sqlConn.Close();
            UpdateDataTable();
        }

    }
}
