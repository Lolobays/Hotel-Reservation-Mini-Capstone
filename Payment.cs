using Microsoft.Reporting.Map.WebForms.BingMaps;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Trabyahe;
using System.Drawing.Imaging;

namespace TRABYAHE
{
    public partial class Payment : Form
    {
        public Payment()
        {
            InitializeComponent();
        }

        private void Payment_Load(object sender, EventArgs e)
        {
            lblTotal.Text = Placeholder.TotalAmount;
            btnBook.Enabled = false;
            btnReceipt.Enabled = false;

            // Disable textboxes initially
            txtName.Enabled = false;
            txtCardNumber.Enabled = false;
            txtExpirationDate.Enabled = false;
            txtCVV.Enabled = false;

            string expirationDate = txtExpirationDate.Text;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserReservation userReservation = new UserReservation();
            userReservation.ShowDialog();
        }

        public static string consString = "Data Source=DESKTOP-3SPCRJ0\\SQLEXPRESS;Initial Catalog=Trabyahe;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        SqlConnection conn = new SqlConnection(consString);

        // Function Over Here

        private void lblReceipt_Click(object sender, EventArgs e)
        {
            PrintDocument printDocument = new PrintDocument();

            printDocument.PrintPage += (s, ev) =>
            {

                string currentDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); // Customize as needed

                // Get Check-Out date and time
                string checkOutDateTime = Placeholder.CheckOut.ToString("yyyy-MM-dd HH:mm:ss");

                // Define fonts for the receipt
                Font titleFont = new Font("Arial", 16, FontStyle.Bold); // Larger title font
                Font headerFont = new Font("Arial", 10, FontStyle.Bold); // Bold headers
                Font regularFont = new Font("Arial", 10); // Regular text font

                // Define margins
                float leftMargin = ev.MarginBounds.Left;
                float rightMargin = ev.MarginBounds.Right;
                float topMargin = ev.MarginBounds.Top;
                float pageWidth = ev.PageBounds.Width;


                // Draw the logo
                Image logo = null;
                try
                {
                    logo = Image.FromFile(@"C:\Users\SECDR\Desktop\NEED_FOR_REVISION_SYSTEM\LATEST ROYAL SOLACE");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading logo image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (logo != null)
                {
                    int logoWidth = 200;
                    int logoHeight = 100;
                    float centerX = (pageWidth - logoWidth) / 2;
                    ev.Graphics.DrawImage(logo, centerX, topMargin, logoWidth, logoHeight);
                    topMargin += logoHeight + 20;
                }

                // Draw the title at the center of the page
                string title = "ROYAL SOLACE RECEIPT";
                SizeF titleSize = ev.Graphics.MeasureString(title, titleFont);
                float titleX = (pageWidth - titleSize.Width) / 2;
                ev.Graphics.DrawString(title, titleFont, Brushes.Black, titleX, topMargin);
                topMargin += titleSize.Height + 10;

                // Add a separator
                ev.Graphics.DrawString(new string('-', 50), regularFont, Brushes.Black, leftMargin, topMargin);
                topMargin += 20;
                ev.Graphics.DrawString("Paid By:", headerFont, Brushes.Black, leftMargin, topMargin);
                topMargin += 20;
                ev.Graphics.DrawString($"Name: {Placeholder.FullName}", regularFont, Brushes.Black, leftMargin, topMargin);
                topMargin += 15;
                ev.Graphics.DrawString($"Contact: {Placeholder.ContactNumber}", regularFont, Brushes.Black, leftMargin, topMargin);
                topMargin += 15;
                ev.Graphics.DrawString(new string('-', 50), regularFont, Brushes.Black, leftMargin, topMargin);
                topMargin += 20;
                // Room details
                ev.Graphics.DrawString("Room Details:", headerFont, Brushes.Black, leftMargin, topMargin);
                topMargin += 20;
                ev.Graphics.DrawString($"Room Name: {Placeholder.RoomName}", regularFont, Brushes.Black, leftMargin, topMargin);
                topMargin += 15;
                ev.Graphics.DrawString($"Room Type: {Placeholder.RoomType}", regularFont, Brushes.Black, leftMargin, topMargin);
                topMargin += 15;
                ev.Graphics.DrawString($"Room Number: {Placeholder.RoomNumber}", regularFont, Brushes.Black, leftMargin, topMargin);
                topMargin += 15;
                ev.Graphics.DrawString($"Room Price: {Placeholder.RoomPrice}", regularFont, Brushes.Black, leftMargin, topMargin);
                topMargin += 15;
                ev.Graphics.DrawString(new string('-', 50), regularFont, Brushes.Black, leftMargin, topMargin);
                topMargin += 20;
                // Reservation details
                ev.Graphics.DrawString("Reservation Details:", headerFont, Brushes.Black, leftMargin, topMargin);
                topMargin += 20;
                ev.Graphics.DrawString($"Check In: {currentDateTime}", regularFont, Brushes.Black, leftMargin, topMargin);
                topMargin += 15;
                ev.Graphics.DrawString($"Check Out: {checkOutDateTime}", regularFont, Brushes.Black, leftMargin, topMargin);
                topMargin += 15;  // Adjust position for next item
                ev.Graphics.DrawString(new string('-', 50), regularFont, Brushes.Black, leftMargin, topMargin);
                topMargin += 20;
                // Payment details
                ev.Graphics.DrawString("Payment Details:", headerFont, Brushes.Black, leftMargin, topMargin);
                topMargin += 20;
                ev.Graphics.DrawString($"Paid Using: {Placeholder.PaymentMethod}", regularFont, Brushes.Black, leftMargin, topMargin);
                topMargin += 15;
                ev.Graphics.DrawString($"Total Amount: {Placeholder.TotalAmount}", regularFont, Brushes.Black, leftMargin, topMargin);
                topMargin += 15;
                ev.Graphics.DrawString(new string('-', 50), regularFont, Brushes.Black, leftMargin, topMargin);
                topMargin += 20;
                // Footer
                ev.Graphics.DrawString("Thank you for booking with us!", headerFont, Brushes.Black, leftMargin, topMargin);
                topMargin += 20;
                ev.Graphics.DrawString(new string('-', 50), regularFont, Brushes.Black, leftMargin, topMargin);

                string todayDate = DateTime.Now.ToString("yyyy-MM-dd"); // Format: 2024-12-10
                ev.Graphics.DrawString($"Printed in: {DateTime.Now.ToString("yyyy-MM-dd")}", regularFont, Brushes.Black, leftMargin, topMargin);

            };

            // Show the print preview dialog
            PrintPreviewDialog previewDialog = new PrintPreviewDialog();
            previewDialog.Document = printDocument;
            previewDialog.ShowDialog();
        }



        private void btnBook_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Do you wish to book now?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    conn.Open();
                    string query = "UPDATE Rooms SET Room_Availability = @value WHERE Room_ID = @RoomID";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@RoomID", Placeholder.RoomID);
                    cmd.Parameters.AddWithValue("@value", 0);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("BOOK UPDATE " + ex.Message + "btnBook_Click");
                }
                finally
                {
                    conn.Close();
                }


                //Storing Values in Database:

                string bookingID = GenerateBookingId();

                try
                {
                    //Booking Table
                    conn.Open();
                    string query = @"INSERT INTO Bookings(Booking_ID, Account_ID, FullName, EmailAddress, ContactNumber, Gender, Address)
                                 Values(@Booking_ID, @Account_ID, @FullName, @EmailAddress, @ContactNumber, @Gender, @Address)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Booking_ID", bookingID);
                    cmd.Parameters.AddWithValue("@Account_ID", CurrentUser.AccountID);
                    cmd.Parameters.AddWithValue("@FullName", Placeholder.FullName);
                    cmd.Parameters.AddWithValue("@EmailAddress", Placeholder.EmailAddress);
                    cmd.Parameters.AddWithValue("@ContactNumber", Placeholder.ContactNumber);
                    cmd.Parameters.AddWithValue("@Gender", Placeholder.Gender);
                    cmd.Parameters.AddWithValue("@Address", Placeholder.Address);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("BookingID Success");
                    Placeholder.Booking_ID = bookingID;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "Generate Booking ID, Booking");
                }
                finally
                {
                    conn.Close();
                }

                //Transaction

                string transactionID = GenerateTransactionID();
                Placeholder.TransactionID = transactionID;

                try
                {
                    conn.Open();
                    string query = "INSERT INTO TransactionList(Transaction_ID, Room_ID, Booking_ID, Account_ID, CheckIn, CheckOut, TotalAmount)" +
                        "Values(@Transaction_ID, @Room_ID, @Booking_ID, @Account_ID, @CheckIn, @CheckOut, @TotalAmount)";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Transaction_ID", transactionID);
                    cmd.Parameters.AddWithValue("@Room_ID", Placeholder.RoomID);
                    cmd.Parameters.AddWithValue("@Booking_ID", Placeholder.Booking_ID);
                    cmd.Parameters.AddWithValue("@Account_ID", CurrentUser.AccountID);
                    cmd.Parameters.AddWithValue("@CheckIn", Placeholder.CheckIn);
                    cmd.Parameters.AddWithValue("@CheckOut", Placeholder.CheckOut);
                    cmd.Parameters.AddWithValue("@TotalAmount", Placeholder.TotalAmount);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Transaction Success" + " " + "Tranasaction ID: " + Placeholder.TransactionID);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "Generate Transaction ID, Transaction");
                }
                finally
                {
                    conn.Close();
                }
            }else if (result == DialogResult.No)
            {
                MessageBox.Show("Your booking has been successfully canceled.", "Booking Canceled", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                UserDashboard userDashboard = new UserDashboard();
                userDashboard.ShowDialog();
            }

            btnReceipt.Enabled = true;


            string qrData = $"Transaction ID: {Placeholder.TransactionID}\n" +
            $"Full Name: {Placeholder.FullName}\n" +
            $"Email Address: {Placeholder.EmailAddress}\n" +
            $"Contact Number: {Placeholder.ContactNumber}\n\n" +
            $"Room Details:\n" +
            $"Room ID: {Placeholder.RoomID}\n" +
            $"Room Name: {Placeholder.RoomName}\n" +
            $"Room Type: {Placeholder.RoomType}\n" +
            $"Room Number: {Placeholder.RoomNumber}\n" +
            $"Room Price: {Placeholder.RoomPrice}\n" +
            $"Reservation Details:\n\n" +
            $"Booking ID: {Placeholder.Booking_ID}\n" +
            $"Check-In: {Placeholder.CheckIn:yyyy-MM-dd hh:mm tt}\n" +
            $"Check-Out: {Placeholder.CheckOut:yyyy-MM-dd hh:mm tt}\n\n" +
            $"Total Amount: {Placeholder.TotalAmount}\n" +
            $"Payment Method: {Placeholder.PaymentMethod}";

            try
            {
                // Generate QR code
                using (var qrGenerator = new QRCoder.QRCodeGenerator())
                {
                    var qrCodeData = qrGenerator.CreateQrCode(qrData, QRCoder.QRCodeGenerator.ECCLevel.Q);
                    using (var qrCode = new QRCoder.QRCode(qrCodeData))
                    {
                        Bitmap qrCodeImage = qrCode.GetGraphic(20);

                        using (MemoryStream ms = new MemoryStream())
                        {
                            qrcode.Image = qrCodeImage;
                            qrCodeImage.Save(ms, ImageFormat.Png);
                            byte[] qrCodeBytes = ms.ToArray();

                            // Save to database
                            SaveQRCodeToDatabase(qrCodeBytes, Placeholder.TransactionID);
                        }

                        
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating QR code: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //Method Below Here

        private void SaveQRCodeToDatabase(byte[] qrCodeBytes, string transactionID)
        {
            string query = "UPDATE TransactionList SET QRIMAGE = @QRCodeImage WHERE Transaction_ID = @TransactionID";


            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@QRCodeImage", qrCodeBytes);
            cmd.Parameters.AddWithValue("@TransactionID", transactionID);
            conn.Open();
            cmd.ExecuteNonQuery();
                
            
        }


        //Generate Transaction ID
        private string GenerateTransactionID()
        {
            DateTime now = DateTime.Now;
            string dateTimePart = now.ToString("yyyyMMdd_HHmmss");
            string transactionId = dateTimePart;

            return transactionId;
        }

        //Generate Booking ID
        private string GenerateBookingId()
        {
            string lastId = "B0"; // Default value if no records exist
            string newId;

            try
            {
                // Query to fetch the last inserted ID
                string query = "SELECT ISNULL(MAX(Booking_ID), 'B0') FROM Bookings";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();
                object result = cmd.ExecuteScalar();

                if (result != null && !string.IsNullOrEmpty(result.ToString()))
                {
                    lastId = result.ToString();
                }

                // Debugging: Print the retrieved last ID
                Console.WriteLine($"Last ID Retrieved: {lastId}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching last ID: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }

            // Remove the 'B' prefix and parse the numeric part of the ID
            string numericPartString = lastId.Substring(1); // Get the numeric part (e.g., '6' from 'B6')
            int numericPart = int.Parse(numericPartString); // Convert '6' -> 6

            numericPart++; // Increment the numeric part

            // Format the new ID with the 'B' prefix
            newId = "B" + numericPart;

            // Debugging: Print the new ID
            Console.WriteLine($"Generated New ID: {newId}");

            return newId;
        }


        private void UpdatePaymentMethod()
        {
            if (chkBoxVisa.Checked)
            {
                Placeholder.PaymentMethod = "Visa";
            }
            else if (chkBoxMaster.Checked)
            {
                Placeholder.PaymentMethod = "Mastercard";
            }
            else if (chkBoxPaypal.Checked)
            {
                Placeholder.PaymentMethod = "PNB";
            }
            else
            {
                Placeholder.PaymentMethod = "None"; // Default value if no checkbox is selected
            }
        }
            private void chkBoxVisa_CheckedChanged(object sender, EventArgs e)
            {
            // Uncheck other checkboxes when one is checked
            if (chkBoxVisa.Checked)
            {
                chkBoxMaster.Checked = false;
                chkBoxPaypal.Checked = false;

                // Enable the textboxes when Visa is selected
                txtCardNumber.Enabled = true;
                txtExpirationDate.Enabled = true;
                txtCVV.Enabled = true;
                txtName.Enabled = true;
            }
            else
            {
                // Disable the textboxes when Visa is unchecked
                txtCardNumber.Enabled = false;
                txtExpirationDate.Enabled = false;
                txtCVV.Enabled = false;
                txtName.Enabled = false;
            }

            UpdatePaymentMethod();
        }

        private void chkBoxMaster_CheckedChanged_1(object sender, EventArgs e)
        {
            // Uncheck other checkboxes when one is checked
            if (chkBoxMaster.Checked)
            {
                chkBoxVisa.Checked = false;
                chkBoxPaypal.Checked = false;

                // Enable the textboxes when MasterCard is selected
                txtCardNumber.Enabled = true;
                txtExpirationDate.Enabled = true;
                txtCVV.Enabled = true;
                txtName.Enabled = true;

            }
            else
            {
                // Disable the textboxes when MasterCard is unchecked
                txtCardNumber.Enabled = false;
                txtExpirationDate.Enabled = false;
                txtCVV.Enabled = false;
                txtName.Enabled = false;

            }

            UpdatePaymentMethod();

        }

        private void chkBoxPaypal_CheckedChanged(object sender, EventArgs e)
        {
            // Uncheck other checkboxes when one is checked
            if (chkBoxPaypal.Checked)
            {
                chkBoxVisa.Checked = false;
                chkBoxMaster.Checked = false;

                // Enable the textboxes when PayPal is selected
                txtCardNumber.Enabled = true;
                txtExpirationDate.Enabled = true;
                txtCVV.Enabled = true;
                txtName.Enabled = true;
                txtName.Enabled = true;


            }
            else
            {
                // Disable the textboxes when PayPal is unchecked
                txtCardNumber.Enabled = false;
                txtExpirationDate.Enabled = false;
                txtCVV.Enabled = false;
                txtName.Enabled = false;

            }

            UpdatePaymentMethod();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            // Ensure a payment method checkbox is selected
            if (!chkBoxVisa.Checked && !chkBoxMaster.Checked && !chkBoxPaypal.Checked)
            {
                MessageBox.Show("Please select a payment method before entering payment details.", "Payment Method Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }

            // Get values from textboxes
            string cardNumber = txtCardNumber.Text;
            string expirationDate = txtExpirationDate.Text;
            string cvv = txtCVV.Text;

            // Validate Card Number
            if (!ValidateCardNumber(cardNumber))
            {
                MessageBox.Show("Card number must be in the format: **** **** **** **** and contain 16 digits.");
                return;
            }

            // Validate expiration date
            if (!ValidateExpirationDate(expirationDate))
            {
                MessageBox.Show("The year must not be less than the current year.", "Invalid Expiration Date", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Validate CVV
            if (!ValidateCVV(cvv))
            {
                MessageBox.Show("CVV must be 3 or 4 digits.");
                return;
            }

            MessageBox.Show("Payment details are valid!");

            btnBook.Enabled = true;
        }

        private bool ValidateCardNumber(string cardNumber)
        {
            string pattern = @"^\d{4} \d{4} \d{4} \d{4}$";
            return Regex.IsMatch(cardNumber, pattern);
        }

        private bool ValidateExpirationDate(string expirationDate)
        {
            string pattern = @"^(0[1-9]|1[0-2])\/\d{2}$";

            if (!Regex.IsMatch(expirationDate, pattern))
            {
                return false;
            }

            string yearString = expirationDate.Substring(3, 2);
            int expirationYear = int.Parse(yearString);
            int currentYear = DateTime.Now.Year % 100; 

            if (expirationYear < currentYear)
            {
                return false;
            }

            return true;
        }

        private bool ValidateCVV(string cvv)
        {
            // Check if CVV is 3 or 4 digits
            string pattern = @"^\d{3,4}$";
            return Regex.IsMatch(cvv, pattern);
        }

        // Placeholder

        private void DateExp_Enter(object sender, EventArgs e)
        {
            if (txtExpirationDate.Text == "mm/yy")
            {
                txtExpirationDate.Text = "";
                txtExpirationDate.ForeColor = Color.Black;

            }
        }

        private void DateExp_Exit(object sender, EventArgs e)
        {
            if (txtExpirationDate.Text == "")
            {
                txtExpirationDate.Text = "mm/yy";
                txtExpirationDate.ForeColor = Color.DarkGray;

            }
        }

        private void CardNumber_Enter(object sender, EventArgs e)
        {
            if (txtCardNumber.Text == "0000 0000 0000 0000")
            {
                txtCardNumber.Text = "";
                txtCardNumber.ForeColor = Color.Black;

            }
        }

        private void CardNumber_Exit(object sender, EventArgs e)
        {
            if (txtCardNumber.Text == "")
            {
                txtCardNumber.Text = "0000 0000 0000 0000";
                txtCardNumber.ForeColor = Color.DarkGray;
            }
        }

        private void CVC_Enter(object sender, EventArgs e)
        {
            if (txtCVV.Text == "CVC")
            {
                txtCVV.Text = "";
                txtCVV.ForeColor = Color.Black;
            }
        }

        private void CVC_Exit(object sender, EventArgs e)
        {
            if (txtCVV.Text == "")
            {
                txtCVV.Text = "CVC";
                txtCVV.ForeColor = Color.DarkGray;
            }
        }







    }   
}