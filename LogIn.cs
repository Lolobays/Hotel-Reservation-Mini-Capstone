using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using Trabyahe;

namespace TRABYAHE
{
    public partial class LogIn : Form
    {
        private string EmailAddress;
        private string Password;

        public class AdminAccount
        {
            public string userAdminAccount = "admin";
            public string passwordAdminAccount = "admin";

            public AdminAccount() { }

            public AdminAccount(string userAdminAccount, string passwordAdminAccount)
            {
                this.userAdminAccount = userAdminAccount;
                this.passwordAdminAccount = passwordAdminAccount;
            }
        }
        public class UserAccount
        {
            public string userAccount = "user";
            public string passwordAccount = "user";

            public UserAccount() { }

            public UserAccount(string userAccount, string passwordAccount)
            {
                this.userAccount = userAccount;
                this.passwordAccount = passwordAccount;
            }
        }
        public LogIn()
        {
            InitializeComponent();
        }

        public static string consString = "Data Source=DESKTOP-3SPCRJ0\\SQLEXPRESS;Initial Catalog=Trabyahe;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        SqlConnection conn = new SqlConnection(consString);
        private void btnLogIn_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();

                // Get user inputs
                EmailAddress = txtEmailAddress.Text.Trim();
                Password = txtPassword.Text.Trim();

                if (string.IsNullOrEmpty(EmailAddress) || string.IsNullOrEmpty(Password))
                {
                    MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // admin login 
                AdminAccount adminAccount = new AdminAccount();
                AdminDashboard adminDashboard = new AdminDashboard();

                if (EmailAddress == adminAccount.userAdminAccount && Password == adminAccount.passwordAdminAccount)
                {
                    MessageBox.Show("Welcome Admin!", "Admin Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    adminDashboard.ShowDialog();
                    
                }

                // Check if the account exists
                string accountID = GetAccountIDByEmailAddress(EmailAddress);
                if (string.IsNullOrEmpty(accountID))
                {
                    MessageBox.Show("Invalid Email Address or Password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Check if the account is activated
                if (!IsActivated(accountID))
                {
                    MessageBox.Show("Your account is not activated yet. Please contact support.", "Account Inactive", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Validate user credentials
                if (IsValidLogin(EmailAddress, Password))
                {
                    string username = GetUserNameByEmailAddress(EmailAddress);
                    CurrentUser.AccountID = accountID;
                    CurrentUser.EmailAddress = EmailAddress;

                    MessageBox.Show($"Welcome, {username}.", "Welcome to Trabyahe", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Hide();
                    UserDashboard userDashboard = new UserDashboard();
                    userDashboard.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Invalid Email Address or Password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
        }




        public bool IsActivated(string accountID)
        {
            bool IsActvated = false;
            try
            {
                string query = "SELECT IsActivated FROM Accounts WHERE Account_ID = @Account_ID";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Account_ID", accountID);
                object result = cmd.ExecuteScalar();

                if (result != null && (bool)result)
                {
                    IsActvated = true; 
                }
                else
                {
                    IsActvated =  false; 
                }
            }
            catch
            {

            }

            return IsActvated;
        }

        public bool IsValidLogin(string emailAddress, string password)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM Accounts WHERE EmailAddress = @EmailAddress AND Password = @Password AND IsActivated = 1";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EmailAddress", emailAddress);
                cmd.Parameters.AddWithValue("@Password", password);

                int count = (int)cmd.ExecuteScalar();

                return count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return false;
            }
        }

        //fetch username
        public string GetUserNameByEmailAddress(string emailAddress)
        {
            try
            {
                string query = "SELECT Username FROM Accounts WHERE EmailAddress = @EmailAddress";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EmailAddress", emailAddress);

                var result = cmd.ExecuteScalar();

                return result?.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return null;
            }
        }
        //fetch account id
        public string GetAccountIDByEmailAddress(string emailAddress)
        {
            try
            {
                string query = "SELECT Account_ID FROM Accounts WHERE EmailAddress = @EmailAddress";

                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@EmailAddress", emailAddress);

                var result = cmd.ExecuteScalar();

                return result?.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                return null;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            this.Hide();
            SignUp signUp = new SignUp();
            signUp.ShowDialog();

        }

        private void showPassword_CheckedChanged_1(object sender, EventArgs e)
        {
            if (showPassword.Checked)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }

        private void IsAccountActivated()
        {
            string query = "";
        }

        private void LogIn_Load(object sender, EventArgs e)
        {

        }
    }
}
