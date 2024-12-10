using Microsoft.Win32;
using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;

namespace TRABYAHE
{
    public partial class SignUp : Form
    {
        public SignUp()
        {
            InitializeComponent();
        }

        // Connection string to SQL Server database
        public static string consString = "Data Source=DESKTOP-3SPCRJ0\\SQLEXPRESS;Initial Catalog=Trabyahe;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        SqlConnection conn = new SqlConnection(consString);

        private string Username, EmailAddress, Password, Confirm_Password;
        private string imgLocation = "";
        private byte[] imageData = null;

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            this.Hide();
            LogIn logIn = new LogIn();
            logIn.ShowDialog();
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void showPassword_CheckedChanged_1(object sender, EventArgs e)
        {
            if (showPassword.Checked)
            {
                txtPassword.UseSystemPasswordChar = false;
                txtConfirmPass.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
                txtConfirmPass.UseSystemPasswordChar = true;
            }
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            EmailAddress = txtEmailAddress.Text;
            Username = txtUsername.Text;
            Password = txtPassword.Text;
            Confirm_Password = txtConfirmPass.Text;

            // Validate fields
            if (string.IsNullOrEmpty(EmailAddress) ||
                string.IsNullOrEmpty(Username) ||
                string.IsNullOrEmpty(Password) ||
                string.IsNullOrEmpty(Confirm_Password) ||
                profile.Image == null)
            {
                MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!EmailAddress.Contains("@gmail.com"))
            {
                MessageBox.Show("A valid email with '@gmail.com' is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!IsValidUserName(Username))
            {
                MessageBox.Show("Invalid username! It must be between 5 and 20 characters and can include letters, numbers, and symbols.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidLengthCharacters(Password) || !ValidLengthCharacters(Confirm_Password))
            {
                MessageBox.Show("Password has to be between 8-30 characters.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (Password != Confirm_Password)
            {
                MessageBox.Show("Passwords do not match.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check if the email already exists in the database
            try
            {
                conn.Open();

                string checkEmailQuery = "SELECT COUNT(*) FROM Accounts WHERE EmailAddress = @EmailAddress";
                SqlCommand checkCmd = new SqlCommand(checkEmailQuery, conn);
                checkCmd.Parameters.AddWithValue("@EmailAddress", EmailAddress);

                int emailCount = (int)checkCmd.ExecuteScalar();

                if (emailCount > 0)
                {
                    MessageBox.Show("This email address is already registered.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while checking the email: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            finally
            {
                conn.Close();
            }

            // If the email is not already registered, proceed with account creation
            string uniqueID = GenerateIncrementingId();

            try
            {
                conn.Open();

                string query = "INSERT INTO Accounts (Account_ID, Username, EmailAddress, Password, ValidID, IsActivated) VALUES (@Account_ID, @username, @EmailAddress, @Password, @ValidID, @IsActivated)";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Account_ID", uniqueID);
                cmd.Parameters.AddWithValue("@username", Username);
                cmd.Parameters.AddWithValue("@EmailAddress", EmailAddress);
                cmd.Parameters.AddWithValue("@Password", Password);
                cmd.Parameters.AddWithValue("@ValidID", imageData);
                cmd.Parameters.AddWithValue("@IsActivated", 0);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Account created successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Hide();
                LogIn log = new LogIn();
                log.ShowDialog();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }

        private void SignUp_Load(object sender, EventArgs e)
        {
            btnSignIn.Enabled = Placeholder.IsEnabledBtnSignIn;
        }

        private void btnTerms_Click(object sender, EventArgs e)
        {
            this.Hide();
            TermsAndConditions termsAndConditions = new TermsAndConditions();
            termsAndConditions.ShowDialog();
        }

        private void btnImport_Click(object sender, EventArgs e)
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
                    // Where it Resizes the imported Images
                    Image originalImage = Image.FromFile(imgLocation);

                    Image resizedImage = ResizeImage(originalImage, 150, 150); // Set desired width and height

                    profile.Image = resizedImage;

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

        // Validating username
        private bool IsValidUserName(string input)
        {
            string pattern = @"^[a-zA-Z0-9!@#$%^&*()_+\-=\[\]{};:'""\\|,.<>\/?]{5,20}$";
            return Regex.IsMatch(input, pattern);
        }

        // Validating password length
        private bool ValidLengthCharacters(string input)
        {
            string pattern = @"^.{8,30}$";
            return Regex.IsMatch(input, pattern);
        }

        // Generate User ID
        private string GenerateIncrementingId()
        {
            string lastId = "00000"; // Default value if no records exist
            string newId;

            try
            {
                // Query to fetch the last inserted ID
                string query = "SELECT ISNULL(MAX(Account_ID), '00000') FROM Accounts";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                object result = cmd.ExecuteScalar();

                if (result != null && !string.IsNullOrEmpty(result.ToString()))
                {
                    lastId = result.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching last ID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }

            // Increment the numeric part of the ID
            int numericPart = int.Parse(lastId); // Convert '00001' -> 1
            numericPart++;

            // Format the new ID with leading zeros
            newId = numericPart.ToString("D5"); // Formats as '00001', '00002', etc.

            return newId;
        }
    }
}
