using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trabyahe;

namespace TRABYAHE
{
    public partial class UserBookingHistory : Form
    {
        public UserBookingHistory()
        {
            InitializeComponent();
        }

        public static string consString = "Data Source=DESKTOP-3SPCRJ0\\SQLEXPRESS;Initial Catalog=Trabyahe;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        SqlConnection conn = new SqlConnection(consString);

        private void btnBookRoom_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            UserReservation userReservation = new UserReservation();
            userReservation.ShowDialog();
        }

        private void dashboard_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            UserDashboard userDashboard = new UserDashboard();
            userDashboard.ShowDialog();
        }

        private void BookingHistory_Load(object sender, EventArgs e)
        {
            loadDataTable();

            txtRoomType.Items.Clear();
            string[] items =
            {
                "Single Room",
                "Double Room",
                "Deluxe King",
                "Deluxe Twin",
                "Grand Deluxe King",
                "Grand Deluxe Twin",
                "Junior Suite",
                "Premium Suite King",
                "Premium Suite Double",
                "Executive Suite King",
                "Executive Suite Double",
                "Celebrity Suite",
                "Presidential Suite"
            };
            foreach (string item in items)
            {
                txtRoomType.Items.Add(item);
            }

        }

        private void loadDataTable()
        {

            try
            {
                conn.Open();
                string query = @"
                        SELECT 
                            TransactionList.Transaction_ID, 
                            TransactionList.Booking_ID, 
                            Rooms.Room_Name, 
                            Rooms.Room_Type
                        FROM 
                            TransactionList
                        INNER JOIN 
                            Rooms ON TransactionList.Room_ID = Rooms.Room_ID
                        WHERE 
                            Account_ID = @AccountID"
                ;

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@AccountID", CurrentUser.AccountID);
                cmd.ExecuteNonQuery();

                SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                dataAdapter.Fill(dt);
                conn.Close();

                dataViewer.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("HA" + ex.Message);
            }
        }

        private void dataViewer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataViewer.Rows[e.RowIndex];

                string TransactionID = row.Cells["Transaction_ID"].Value.ToString();
                string RoomName = row.Cells["Room_Name"].Value.ToString();

                GetRoomDetails(RoomName);
                GetTransactionDetails(TransactionID);
                GetQRCode(TransactionID);
            }
        }


        //Method Below Here

        private void GetQRCode(string TransactionID)
        {
            string query = "SELECT QRImage FROM TransactionList WHERE Transaction_ID = @TransactionID";

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TransactionID", TransactionID);

                // Retrieve the data
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    
                    if (reader["QRImage"] != DBNull.Value)
                    {
                        byte[] qrCodeBytes = (byte[])reader["QRImage"];

                        
                        using (MemoryStream ms = new MemoryStream(qrCodeBytes))
                        {
                            Bitmap qrImage = new Bitmap(ms);

                           
                            qrcode.Image = qrImage;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving QR Code: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }


        private void GetRoomDetails(string input)
        {
            string query = "SELECT Room_Name, Room_Type, Room_Number, Room_Price FROM Rooms WHERE Room_Name = @RoomName";

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@RoomName", input);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    lblRoomName.Text = reader["Room_Name"].ToString();
                    lblRoomType.Text = reader["Room_Type"].ToString();
                    lblRoomNumber.Text = reader["Room_Number"].ToString();
                    lblRoomPrice.Text = reader["Room_Price"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void GetTransactionDetails(string input)
        {
            string query = "SELECT CheckIn, CheckOut, TotalAmount FROM TransactionList WHERE Transaction_ID = @TransactionID";

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TransactionID", input);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    lblIn.Text = reader["CheckIn"].ToString();
                    lblOut.Text = reader["CheckOut"].ToString();
                    lblTotalAmount.Text = reader["TotalAmount"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.Hide();
            LogIn log = new LogIn();
            log.ShowDialog();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRoomType.Text))
            {
                MessageBox.Show("Search term field is empty.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string query = @"
                                SELECT 
                                    TransactionList.Transaction_ID, 
                                    TransactionList.Booking_ID, 
                                    Rooms.Room_Name, 
                                    Rooms.Room_Type
                                FROM 
                                    TransactionList
                                INNER JOIN 
                                    Rooms ON TransactionList.Room_ID = Rooms.Room_ID
                                WHERE 
                                    TransactionList.Account_ID = @AccountID AND Rooms.Room_Type LIKE @SearchText
        ";

                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@AccountID", CurrentUser.AccountID);
                    cmd.Parameters.AddWithValue("@SearchText", txtRoomType.Text );

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        dataViewer.DataSource = dt;
                    }
                    else
                    {
                        MessageBox.Show("No rooms match the search criteria.", "No Results", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dataViewer.DataSource = null; // Clear previous results
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error during search: {ex.Message}", "Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
