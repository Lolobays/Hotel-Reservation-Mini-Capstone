using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TRABYAHE
{
    public partial class AdminDashboard : Form
    {
        public AdminDashboard()
        {
            InitializeComponent();
        }
        public static string consString = "Data Source=DESKTOP-3SPCRJ0\\SQLEXPRESS;Initial Catalog=Trabyahe;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        SqlConnection conn = new SqlConnection(consString);



        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminAddPlace adminAddPlace = new AdminAddPlace();
            adminAddPlace.ShowDialog();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.Hide();
            LogIn log = new LogIn();
            log.ShowDialog();
        }

        private void AdminDashboard_Load(object sender, EventArgs e)
        {
            loadDataTable();

            dataViewer.ReadOnly = true;
            dataViewer.AllowUserToAddRows = true;
            dataViewer.AllowUserToDeleteRows = true;
            dataViewer.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataViewer.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataViewer.Columns["Room_Description"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;


            IsThereAValue();

            conn.Open();
            lblAvRooms.Text = GetTotalRooms().ToString();
            lblBookRooms.Text = GetTotalBooked().ToString();
            lblTotalProfit.Text = "₱ " + GetTotalSales().ToString();
            lblTotalUsers.Text = GetTotalUser().ToString();
            conn.Close();

        }

        //Check if there is no value inside the column

        private void IsThereAValue()
        {
            try
            {
                conn.Open();
                string query = "SELECT COUNT(*) FROM AdminStatistics";
                SqlCommand cmd = new SqlCommand(query, conn);

                int checkValue = (int)cmd.ExecuteScalar();

                if (checkValue == 0)
                {
                    string query1 = "INSERT INTO AdminStatistics(TotalRooms, TotalBooked, TotalSales, TotalUser)VALUES (0,0,0,0)";
                    cmd.CommandText = query1;
                    cmd.ExecuteNonQuery();
                }
                else if (checkValue > 0)
                {
                    // Separate UPDATE statements for better efficiency
                    string query2 = "UPDATE AdminStatistics SET TotalRooms = (SELECT COUNT(*) FROM Rooms WHERE Room_Availability = 1)";
                    cmd.CommandText = query2;
                    cmd.ExecuteNonQuery();

                    query2 = "UPDATE AdminStatistics SET TotalBooked = (SELECT COUNT(*) FROM TransactionList)";
                    cmd.CommandText = query2;
                    cmd.ExecuteNonQuery();

                    query2 = "UPDATE AdminStatistics SET TotalSales = COALESCE((SELECT SUM(ISNULL(TotalAmount, 0)) FROM TransactionList), 0)";
                    cmd.CommandText = query2;
                    cmd.ExecuteNonQuery();

                    query2 = "UPDATE AdminStatistics SET TotalUser = (SELECT COUNT(*) FROM Accounts)";
                    cmd.CommandText = query2;
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("IsthereVlaue " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchBar = txtSearchBar.Text.Trim();

            if (string.IsNullOrEmpty(searchBar))
            {
                MessageBox.Show("Please enter a search term.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string query = @"
                                SELECT * 
                                FROM Rooms
                                WHERE 
                                    Room_ID LIKE @Search OR
                                    Room_Name LIKE @Search OR
                                    Room_Type LIKE @Search OR
                                    Room_Number LIKE @Search OR
                                    Room_Price LIKE @Search LIKE @Search";


                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@Search", searchBar);

                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);

                // Bind the filtered data to the DataGridView
                dataViewer.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Search error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClearSearch_Click(object sender, EventArgs e)
        {
            loadDataTable();
            txtSearchBar.Clear();
        }





        // Methods Below Here

        //Get TotalRooms 
        private int GetTotalRooms()
        {
            int totalRooms = 0;
            string query = "SELECT TotalRooms FROM AdminStatistics ";

            try
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                totalRooms = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("GetTotalRooms " + ex.Message);
            }
            finally
            {
            }

            return totalRooms;
        }
        //Get Total Booked 
        private int GetTotalBooked()
        {
            int totalBooked = 0;
            string query = "SELECT TotalBooked FROM AdminStatistics ";

            try
            {

                SqlCommand cmd = new SqlCommand(query, conn);
                totalBooked = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("GetTotalBooked " + ex.Message);
            }
            finally
            {
            }

            return totalBooked;
        }
        //Get Total Sales
        private double GetTotalSales()
        {
            double totalSales = 0;
            string query = "SELECT TotalSales FROM AdminStatistics ";

            try
            {

                SqlCommand cmd = new SqlCommand(query, conn);
                totalSales = (double)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("GetTotalSales " + ex.Message);
            }
            finally
            {
            }

            return totalSales;
        }
        //Get TotalUser
        private int GetTotalUser()
        {
            int totalUser = 0;
            string query = "SELECT TotalUser FROM AdminStatistics";

            try
            {

                SqlCommand cmd = new SqlCommand(query, conn);
                totalUser = (int)cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("GetTotalUser " + ex.Message);
            }
            finally
            {
            }

            return totalUser;
        }

        private void loadDataTable()
        {
            try
            {
                string query = "SELECT * FROM Rooms";

                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();

                dataAdapter.Fill(dt);

                dataViewer.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("LoadTable" + ex.Message);
            }

        }

        private void btnTransactions_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminTransaction adminTransaction = new AdminTransaction();
            adminTransaction.ShowDialog();
        }


        //Total User Viewer
        private void pictureBox5_Click(object sender, EventArgs e)
        {

            ViewTotalUsers viewTotalUsers = new ViewTotalUsers();
            viewTotalUsers.ShowDialog();
        }

        //Available Rooms Viewer
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ViewAvailableRooms viewAvailableRooms = new ViewAvailableRooms();
            viewAvailableRooms.ShowDialog();
        }

        //View Book Rooms
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ViewBookedRooms viewBookRooms = new ViewBookedRooms();
            viewBookRooms.ShowDialog();
        }

        private void btnValidateAccount_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminValidateAccounts adminValidateAccounts = new AdminValidateAccounts();
            adminValidateAccounts.ShowDialog();
        }

        private void dataViewer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            ViewTotalSales viewTotalSales = new ViewTotalSales();
            viewTotalSales.ShowDialog();
        }
    }
}
