using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TRABYAHE
{
    public partial class AdminTransaction : Form
    {
        public AdminTransaction()
        {
            InitializeComponent();
        }
        public static string consString = "Data Source=DESKTOP-3SPCRJ0\\SQLEXPRESS;Initial Catalog=Trabyahe;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        SqlConnection conn = new SqlConnection(consString);

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void dashboard_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminDashboard adminDashboard = new AdminDashboard();
            adminDashboard.ShowDialog();
        }
        private void btndashboard_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminDashboard adminDashboard = new AdminDashboard();
            adminDashboard.ShowDialog();
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
        private void AdminTransaction_Load_1(object sender, EventArgs e)
        {
            loadDataTable();
            dataViewerPlaces.ReadOnly = true;
            dataViewerPlaces.AllowUserToAddRows = true;
            dataViewerPlaces.AllowUserToDeleteRows = true;
            dataViewerPlaces.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataViewerPlaces.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }

        //Function 

        private void dataViewerPlaces_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataViewerPlaces.Rows[e.RowIndex];
                string roomID = row.Cells["Room_ID"].Value.ToString();
                string bookID = row.Cells["Booking_ID"].Value.ToString();

                GetRoomDetails(roomID);
                GetBookerDetails(bookID);
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
                                FROM TransactionList
                                WHERE 
                                    Transaction_ID LIKE @Search OR
                                    Room_ID LIKE @Search OR
                                    Booking_ID LIKE @Search OR
                                    Account_ID LIKE @Search";


                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                dataAdapter.SelectCommand.Parameters.AddWithValue("@Search", searchBar);

                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);

                // Bind the filtered data to the DataGridView
                dataViewerPlaces.DataSource = dt;
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

        private void GetBookerDetails(string bookID)
        {
            string query = "SELECT FullName, EmailAddress, ContactNumber, Address FROM Bookings WHERE Booking_ID = @bookID";

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@bookID", bookID);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    lblFullName.Text = reader["FullName"].ToString();
                    lblEmailAddress.Text = reader["EmailAddress"].ToString();
                    lblContactNumber.Text = reader["ContactNumber"].ToString();
                    lblAddres.Text = reader["Address"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("GetBookerDetails: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void GetRoomDetails(string roomID)
        {
            string query = "SELECT Room_ID, Room_Name, Room_Type, Room_Number, Room_Price FROM Rooms WHERE Room_ID = @roomID";

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@roomID", roomID);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    lblRoomID.Text = reader["Room_ID"].ToString();
                    lblRoomName.Text = reader["Room_Name"].ToString();
                    lblRoomType.Text = reader["Room_Type"].ToString();
                    lblRoomNumber.Text = reader["Room_Number"].ToString();
                    lblRoomPrice.Text = reader["Room_Price"].ToString();

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("GetRoomDetails: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void loadDataTable()
        {
            try
            {
                string query = "SELECT Transaction_ID, Room_ID, Booking_ID, Account_ID, CheckIn, CheckOut, TotalAmount FROM TransactionList";

                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();

                dataAdapter.Fill(dt);
                conn.Close();

                dataViewerPlaces.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnValidateAccount_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminValidateAccounts adminValidateAccounts = new AdminValidateAccounts();
            adminValidateAccounts.ShowDialog();
        }
    }
}
