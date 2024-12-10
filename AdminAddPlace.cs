using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using Microsoft.Reporting.Map.WebForms.BingMaps;
using Microsoft.Win32;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Text.RegularExpressions;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;

namespace TRABYAHE
{
    public partial class AdminAddPlace : Form
    {
        private string RoomID, RoomName, RoomTypes, RoomNumber, RoomPrice, RoomDescription;
        private string imgLocation;
        private byte[] imageData;


        public AdminAddPlace()
        {

            InitializeComponent();

        }

        //SQL Connection
        public static string consString = "Data Source=DESKTOP-3SPCRJ0\\SQLEXPRESS;Initial Catalog=Trabyahe;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        SqlConnection conn = new SqlConnection(consString);

        public class DataHolders
        {
            public string roomId { get; set; }
            public string roomName { get; set; }
            public string roomType { get; set; }
            public string roomPrice { get; set; }
            public string roomDescription { get; set; }

            // Default constructor
            public DataHolders()
            {
                this.roomId = null;
                this.roomName = null;
                this.roomType = null;
                this.roomPrice = null;
                this.roomDescription = null;
            }

            // Parameterized constructor
            public DataHolders(string roomId, string roomName, string roomType, string roomPrice, string roomDescription)
            {
                this.roomId = roomId;
                this.roomName = roomName;
                this.roomType = roomType;
                this.roomPrice = roomPrice;
                this.roomDescription = roomDescription;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }
        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();

            LogIn log = new LogIn();
            log.ShowDialog();
        }
        private void dashboard_Click(object sender, EventArgs e)
        {
            this.Hide();

            AdminDashboard adminDashboard = new AdminDashboard();
            adminDashboard.ShowDialog();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AdminAddPlace_Load_1(object sender, EventArgs e)
        {
            loadDataTable();
            dataViewerPlaces.ReadOnly = true;
            dataViewerPlaces.AllowUserToAddRows = true;
            dataViewerPlaces.AllowUserToDeleteRows = true;
            dataViewerPlaces.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataViewerPlaces.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataViewerPlaces.Columns["Room_Description"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

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

            // Add all items to the dropdown
            foreach (string item in items)
            {
                txtRoomType.Items.Add(item);
            }

        }


        //Functionality Over Here

        private void btnAddPlaces_Click(object sender, EventArgs e)
        {
            RoomID = txtRoomID.Text.Trim();
            RoomName = txtRoomName.Text.Trim();
            RoomTypes = txtRoomType.Text.Trim();
            RoomNumber = txtRoomNumber.Text.Trim();
            RoomPrice = txtRoomPrice.Text.Trim();
            RoomDescription = txtRoomDescription.Text.Trim();

            if (string.IsNullOrEmpty(RoomID) ||
                string.IsNullOrEmpty(RoomName) ||
                string.IsNullOrEmpty(RoomTypes) ||
                string.IsNullOrEmpty(RoomNumber) ||
                string.IsNullOrEmpty(RoomPrice) ||
                string.IsNullOrEmpty(RoomDescription) ||
                PictureBoxPlaces.Image == null)
            {
                MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!forIdNumberOnly(RoomID))
            {
                MessageBox.Show("Invalid Input! Enter a valid Room ID/Only Accepts number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (ValidLengthCharacters(RoomName))
            {
                MessageBox.Show("Room Name must be characters only.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;

            }

            if (!numbersOnly(RoomNumber))
            {
                MessageBox.Show("Room number is invalid. Please enter a number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            //adding to database
            try
            {
                conn.Open();

                // Check for duplicates (Room ID or Room Name)
                string checkQuery = @"
                            SELECT COUNT(*) 
                            FROM Rooms 
                            WHERE Room_ID = @Room_ID OR Room_Name = @Room_Name OR Room_Number = @Room_Number";

                SqlCommand checkCmd = new SqlCommand(checkQuery, conn);
                checkCmd.Parameters.AddWithValue("@Room_ID", RoomID);
                checkCmd.Parameters.AddWithValue("@Room_Name", RoomName);
                checkCmd.Parameters.AddWithValue("@Room_Number", RoomNumber);

                int count = (int)checkCmd.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("A room with the same ID, Name, or Room Number already exists.", "Duplicate Entry", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string query = @"INSERT INTO Rooms (Room_ID, Room_Name, Room_Type, Room_Number, Room_Price, Room_Description, Photo, Room_Availability)
                        VALUES (@Room_ID, @Room_Name, @Room_Type, @Room_Number, @Room_Price, @Room_Description, @Photo, @Room_Availability)";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Room_ID", RoomID);
                cmd.Parameters.AddWithValue("@Room_Name", RoomName);
                cmd.Parameters.AddWithValue("@Room_Type", RoomTypes);
                cmd.Parameters.AddWithValue("@Room_Number", RoomNumber);
                cmd.Parameters.AddWithValue("@Room_Price", RoomPrice);
                cmd.Parameters.AddWithValue("@Room_Description", RoomDescription);
                cmd.Parameters.AddWithValue("@Photo", imageData);
                cmd.Parameters.AddWithValue("@Room_Availability", 1);

                MessageBox.Show("Successfully added", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            loadDataTable();
            ClearInputs();
        }

        private void btnUpdatePlaces_Click(object sender, EventArgs e)
        {
            RoomID = txtRoomID.Text;
            RoomName = txtRoomName.Text;
            RoomTypes = txtRoomType.Text;
            RoomNumber = txtRoomNumber.Text;
            RoomPrice = txtRoomPrice.Text;
            RoomDescription = txtRoomDescription.Text;

            if (string.IsNullOrEmpty(RoomID) ||
                string.IsNullOrEmpty(RoomName) ||
                string.IsNullOrEmpty(RoomTypes) ||
                string.IsNullOrEmpty(RoomNumber) ||
                string.IsNullOrEmpty(RoomPrice) ||
                PictureBoxPlaces.Image == null)
            {
                MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            //Update Rooms
            try
            {
                conn.Open();

                string query = "UPDATE Rooms SET Room_Type = @Room_Type, Room_Name = @Room_Name, Room_Number = @Room_Number, Room_Price = @Room_Price, Room_Description = @Room_Description, Photo = @Photo WHERE Room_ID = @Room_ID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Room_ID", RoomID);
                cmd.Parameters.AddWithValue("@Room_Name", RoomName);
                cmd.Parameters.AddWithValue("@Room_Type", RoomTypes);
                cmd.Parameters.AddWithValue("@Room_Number", RoomNumber);
                cmd.Parameters.AddWithValue("@Room_Price", RoomPrice);
                cmd.Parameters.AddWithValue("@Room_Description", RoomDescription);
                cmd.Parameters.AddWithValue("@Photo", imageData);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Successfuly Updated");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            loadDataTable();
            ClearInputs();
        }

        private void btnDeletePlaces_Click(object sender, EventArgs e)
        {
            string packageIDToFind = txtRoomID.Text;

            if (string.IsNullOrEmpty(packageIDToFind))
            {
                MessageBox.Show("Room ID must be filled to delete a room.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                conn.Open();
                string query = "DELETE FROM Rooms WHERE Room_ID = @Room_ID";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Room_ID", packageIDToFind);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Package has been deleted.");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            loadDataTable();
            ClearInputs();
        }

        private void btnImportImages_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp",
                Title = "Select an Image"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                imgLocation = openFileDialog.FileName;

                try
                {
                    //Where it Resize the imported Images
                    Image originalImage = Image.FromFile(imgLocation);

                    Image resizedImage = ResizeImage(originalImage, 150, 150); // Set desired width and height

                    PictureBoxPlaces.Image = resizedImage;

                    // Convert resized image to byte array
                    using (MemoryStream ms = new MemoryStream())
                    {
                        resizedImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        imageData = ms.ToArray();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClearPlaces_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        // Methods Below Here

        //Check if only Contains a number
        private bool forIdNumberOnly(string input)
        {
            string pattern = @"^\d+$";
            return Regex.IsMatch(input, pattern);
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.Hide();
            LogIn logIn = new LogIn();
            logIn.ShowDialog();
        }

        private void btnTransactions_Click(object sender, EventArgs e)
        {
           this.Hide();
           AdminTransaction adminTransactions = new AdminTransaction();
           adminTransactions.ShowDialog();

        }

        private void dataViewerPlaces_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataViewerPlaces.Rows[e.RowIndex];

                txtRoomID.Text = row.Cells["Room_ID"].Value.ToString();
                txtRoomName.Text = row.Cells["Room_Name"].Value.ToString();
                txtRoomType.Text = row.Cells["Room_Type"].Value.ToString();
                txtRoomNumber.Text = row.Cells["Room_Number"].Value.ToString();
                txtRoomPrice.Text = row.Cells["Room_Price"].Value.ToString();
                txtRoomDescription.Text = row.Cells["Room_Description"].Value.ToString();

                byte[] photoBytes = row.Cells["Photo"].Value as byte[];
                MemoryStream ms = new MemoryStream(photoBytes);
                PictureBoxPlaces.Image = Image.FromStream(ms); 
            }
        }

        private void btnValidateAccount_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminValidateAccounts adminValidateAccounts = new AdminValidateAccounts();
            adminValidateAccounts.ShowDialog();
        }

        //Rather it only contains a letters/ no numbers included
        private bool ValidLengthCharacters(string input)
        {
            string pattern = @"^[a-zA-Z]$";
            return Regex.IsMatch(input, pattern);
        }

        //Acceots decimals
        private bool numbersOnly(string input)
        {
            string pattern = @"^\d+(\.\d+)?$";
            return Regex.IsMatch(input, pattern);
        }

        //Image Resize 
        private Image ResizeImage(Image image, int width, int height)
        {
            Bitmap resizedImage = new Bitmap(width, height);
            using (Graphics graphics = Graphics.FromImage(resizedImage))
            {
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.DrawImage(image, 0, 0, width, height);
            }
            return resizedImage;
        }

        private void ClearInputs()
        {
            txtRoomID.Clear();
            txtRoomType.Text = string.Empty;
            txtRoomName.Text = string.Empty;
            txtRoomNumber.Clear();
            txtRoomPrice.Clear();
            txtRoomDescription.Clear();
            PictureBoxPlaces.Image = null;
        }

        private void loadDataTable()
        {
            try
            {
                string query = "SELECT * FROM Rooms";

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
    }

}