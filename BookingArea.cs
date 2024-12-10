using System;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TRABYAHE
{
    public partial class BookingArea : Form
    {
        public BookingArea()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            BookingArea bookingArea = new BookingArea();
            bookingArea.Show();
            this.Hide();
        }

        private void BookingArea_Load(object sender, EventArgs e)
        {
            btnProceed.Enabled = false;
        }

        // Function to confirm and save user data
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string fullName = txtFullName.Text.Trim();
            string emailAddress = txtEmailAddress.Text.Trim();
            string contactNumber = txtContact.Text.Trim();
            string address = txtAddress.Text.Trim();
            string gender = cbGender.SelectedItem?.ToString();

            // Validate the input fields
            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(emailAddress) ||
                string.IsNullOrWhiteSpace(contactNumber) || string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(gender))
            {
                MessageBox.Show("Please fill in all required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!emailAddress.Contains("@gmail.com"))
            {
                MessageBox.Show("Please enter a valid email address.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!IsValidContactNumber(contactNumber))
            {
                MessageBox.Show($"{contactNumber} is not a valid Philippine cellphone number.", "Validation Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Confirm details with the user
            DialogResult result = MessageBox.Show(
                $"Name: {fullName}\nEmail Address: {emailAddress}\nContact Number: {contactNumber}\nAddress: {address}\nGender: {gender}\n\nIs this information correct?",
                "Confirm Details",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                // Save the entered information into the Placeholder class
                Placeholder.FullName = fullName;
                Placeholder.EmailAddress = emailAddress;
                Placeholder.ContactNumber = contactNumber;
                Placeholder.Address = address;
                Placeholder.Gender = gender;

                MessageBox.Show("Information successfully saved.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Allow the user to update the details
                MessageBox.Show("Please update your details.", "Edit Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

            btnProceed.Enabled = true;

        }

        private void btnProceed_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text) ||
                string.IsNullOrWhiteSpace(txtEmailAddress.Text) ||
                string.IsNullOrWhiteSpace(txtContact.Text) ||
                cbGender.SelectedIndex == -1 ||
                string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("Please fill in all the required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                this.Hide();
                Payment payment = new Payment();
                payment.ShowDialog();
            }
        }

        // Method to validate contact number (Philippine format)
        private bool IsValidContactNumber(string input)
        {
            string pattern = "^(?:\\+639\\d{9}|09\\d{9})$";
            return Regex.IsMatch(input, pattern);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtFullName.Clear();
            txtEmailAddress.Clear();
            txtContact.Clear();
            txtAddress.Clear();
            cbGender.SelectedIndex = -1;
        }
    }
}
