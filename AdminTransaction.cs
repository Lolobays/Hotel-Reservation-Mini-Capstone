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
        private void AdminTransaction_Load(object sender, EventArgs e)
        {
            loadDataTable();
            dataViewer.ReadOnly = true;
            dataViewer.AllowUserToAddRows = true;
            dataViewer.AllowUserToDeleteRows = true;
            dataViewer.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataViewer.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }


        // Function Over here

        private void dataViewerPlaces_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == 0)
            {
                DataGridViewRow row = dataViewer.Rows[e.RowIndex];
                string roomID = row.Cells["Room_ID"].Value.ToString();
                string bookID = row.Cells["Booking_ID"].Value.ToString();

                GetRoomDetails(roomID);
                GetBookerDetails(bookID);
            }
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
                    lblEmailAdd.Text = reader["EmailAddress"].ToString();
                    lblContactNum.Text = reader["ContactNumber"].ToString();
                    lblAddress.Text = reader["Address"].ToString();
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
                string query = "SELECT * FROM TransactionList";

                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();

                dataAdapter.Fill(dt);
                conn.Close();

                dataViewer.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        

        
    }
}
