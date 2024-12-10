using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using TRABYAHE;

namespace Trabyahe
{
    public partial class UserReservation : Form
    {
        public static string consString = "Data Source=DESKTOP-3SPCRJ0\\SQLEXPRESS;Initial Catalog=Trabyahe;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        SqlConnection conn = new SqlConnection(consString);
        public UserReservation()
        {
            InitializeComponent();
        }

        private void btnBook_Click(object sender, EventArgs e)
        {
            if (lblType.Text == "----------" || lblTotal.Text == "----------")
            {
                MessageBox.Show("Select a package");
            }
            else
            {
                BookingArea bookingArea = new BookingArea();
                bookingArea.ShowDialog();
            }

        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.Hide();
            LogIn logIn = new LogIn();
            logIn.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dashboard_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserDashboard userDashboard = new UserDashboard();
            userDashboard.ShowDialog();
        }

        private void UserReservation_Load(object sender, EventArgs e)
        {
            loadDataTable();
            ConfigureDataViewer();

            dtpnCheckIn.MinDate = DateTime.Today;
            dtpnCheckOut.MinDate = DateTime.Today;

            dtpnCheckIn.Format = DateTimePickerFormat.Custom;
            dtpnCheckIn.CustomFormat = "dddd, MMMM dd, yyyy hh:mm tt";
            dtpnCheckOut.Format = DateTimePickerFormat.Custom;
            dtpnCheckOut.CustomFormat = "dddd, MMMM dd, yyyy hh:mm tt";

            dtpnCheckIn.ValueChanged += dtpnCheckIn_ValueChanged;
            dtpnCheckOut.ValueChanged += dtpnCheckOut_ValueChanged;

            btnBook.Enabled = false;

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

        // Method to get room details and store in Placeholder
        private void dataViewerPlaces_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Get the clicked row
                DataGridViewRow row = dataViewerPlaces.Rows[e.RowIndex];
                string roomName = row.Cells["Room_Name"].Value.ToString();
                GetRoomDetails(roomName);
            }
        }

        // Method to clear room details
        private void Clear_Click(object sender, EventArgs e)
        {
            lblName.Text = "----------";
            lblType.Text = "----------";
            lblNumber.Text = "----------";
            lblPrice.Text = "----------";
            lblDesc.Text = "----------";
            lblTotal.Text = "----------";
            pictureBox1.Image = null;
        }

        // Method to fetch room details from the database and update the Placeholder
        private void GetRoomDetails(string roomName)
        {
            string query = "SELECT Room_ID, Room_Name, Room_Type, Room_Number, Room_Price, Room_Description, Photo FROM Rooms WHERE Room_Name = @RoomName";

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@RoomName", roomName);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // Assign room details to the Placeholder
                    Placeholder.RoomID = reader["Room_ID"].ToString();
                    Placeholder.RoomName = reader["Room_Name"].ToString();
                    Placeholder.RoomType = reader["Room_Type"].ToString();
                    Placeholder.RoomNumber = reader["Room_Number"].ToString();
                    Placeholder.RoomPrice = reader["Room_Price"].ToString();
                    Placeholder.RoomDescription = reader["Room_Description"].ToString();

                    // Update labels with the fetched details
                    lblType.Text = Placeholder.RoomType;
                    lblName.Text = Placeholder.RoomName;
                    lblNumber.Text = Placeholder.RoomNumber;
                    lblPrice.Text = Placeholder.RoomPrice;
                    lblDesc.Text = Placeholder.RoomDescription;

                    // Load the room image
                    byte[] photoBytes = (byte[])reader["Photo"];
                    MemoryStream ms = new MemoryStream(photoBytes);
                    pictureBox1.Image = Image.FromStream(ms);

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching room details: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
            }
        }

        // Method to calculate total cost based on room price and stay duration
        private void CalculateTotalCost()
        {
            try
            {
                if (!string.IsNullOrEmpty(lblPrice.Text) && decimal.TryParse(lblPrice.Text, out decimal roomPrice))
                {
                    TimeSpan stayDuration = dtpnCheckOut.Value.Date - dtpnCheckIn.Value.Date;
                    int days = (int)stayDuration.TotalDays;

                    if (days > 0)
                    {
                        decimal total = roomPrice * days;
                        lblTotal.Text = $" ₱{total:F2}";
                        Placeholder.TotalAmount = total.ToString();
                    }
                    else
                    {
                        lblTotal.Text = "₱0.00";
                        MessageBox.Show("Check-out date must be later than check-in date.", "Invalid Date Range", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    lblTotal.Text = "Total: ₱0.00";
                    MessageBox.Show("Invalid room price. Please select a room.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while calculating the total: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event to update total amount when check-in date changes
        private void dtpnCheckIn_ValueChanged(object sender, EventArgs e)
        {
            CalculateTotalCost();
        }

        // Event to update total amount when check-out date changes
        private void dtpnCheckOut_ValueChanged(object sender, EventArgs e)
        {
            CalculateTotalCost();
        }

        // Method to configure the DataGridView
        private void ConfigureDataViewer()
        {
            dataViewerPlaces.ReadOnly = true;
            dataViewerPlaces.AllowUserToAddRows = false;
            dataViewerPlaces.AllowUserToDeleteRows = false;
            dataViewerPlaces.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataViewerPlaces.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            if (dataViewerPlaces.Columns.Contains("Room_Description"))
            {
                dataViewerPlaces.Columns["Room_Description"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            }
        }

        // Method to load data into the DataGridView
        private void loadDataTable()
        {
            try
            {
                string query = "SELECT Room_Name, Room_Type, Photo FROM Rooms WHERE Room_Availability = 1";

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

        private void btnBookingHistory_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserBookingHistory userBookingHistory = new UserBookingHistory();
            userBookingHistory.ShowDialog();
        }

        private void btnDummy_Click(object sender, EventArgs e)
        {
            Placeholder.CheckIn = dtpnCheckIn.Value;
            Placeholder.CheckOut = dtpnCheckOut.Value;

            if (lblTotal.Text == "--------")
            {
                MessageBox.Show("Choose a date first");
            }
            else
            {
                DialogResult result = MessageBox.Show($"Room Type: {Placeholder.RoomType}" +
                $"\nRoom Name: {Placeholder.RoomName}" +
                $"\nRoom Number: {Placeholder.RoomNumber}" +
                $"\nTotal Price: {Placeholder.TotalAmount}" +
                $"\nCheck In: {Placeholder.CheckIn}" +
                $"\nCheck Out: {Placeholder.CheckOut}", "Confirm Booking", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                

                if (result == DialogResult.Yes)
                {
                    btnBook.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Booking was not confirmed.", "Booking Canceled", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
                

                
                
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchBar = txtRoomType.Text.Trim();

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
                                    Room_Type LIKE @Search
                                    AND Room_Availability = 1";



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
            
        }




        //public void ResetRoomAvailability()
        //{

        //    if (DateTime.Now.Day == 1)
        //    {
        //            string query = "UPDATE Rooms SET active_status = 1 WHERE active_status = 0";
        //            SqlCommand command = new SqlCommand(query, conn);

        //            conn.Open();
        //            int rowsAffected = command.ExecuteNonQuery();

        //            if (rowsAffected > 0)
        //            {
        //                Console.WriteLine($"Successfully reset {rowsAffected} rooms to available.");
        //            }
        //            else
        //            {
        //                Console.WriteLine("No rooms to reset.");
        //            }

        //    }
        //    else
        //    {
        //        Console.WriteLine("It's not the beginning of the month. No changes made.");
        //    }
        //}
    }
}