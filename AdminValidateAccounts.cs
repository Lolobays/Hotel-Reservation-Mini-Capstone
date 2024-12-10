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
    public partial class AdminValidateAccounts : Form
    {
        public AdminValidateAccounts()
        {
            InitializeComponent();
        }

        public static string consString = "Data Source=DESKTOP-3SPCRJ0\\SQLEXPRESS;Initial Catalog=Trabyahe;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        SqlConnection conn = new SqlConnection(consString);

        private void btnAddRoom_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminAddPlace adminAddPlace = new AdminAddPlace();
            adminAddPlace.ShowDialog(); 
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AdminValidateAccounts_Load(object sender, EventArgs e)
        {
            loadDataTable();
            dataViewer.ReadOnly = true;
            dataViewer.AllowUserToAddRows = true;
            dataViewer.AllowUserToDeleteRows = true;
            dataViewer.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataViewer.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            
        }



        private void dataViewer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataViewer.Rows[e.RowIndex];
                string userName = row.Cells["Username"].Value.ToString();
                string emailAdd = row.Cells["EmailAddress"].Value.ToString();
                string AccountID = row.Cells["Account_ID"].Value.ToString();

                CurrentUser.AccountID = AccountID;
                CurrentUser.EmailAddress = emailAdd;

                byte[] photoBytes = row.Cells["ValidID"].Value as byte[];
                MemoryStream ms = new MemoryStream(photoBytes);
                pictureBox2.Image = Image.FromStream(ms);


                lblUserName.Text = userName;
                lblEmailAddress.Text = emailAdd;
            }
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            string query = "UPDATE Accounts SET IsActivated = 1 WHERE Account_ID = @AccountID";
            conn.Open();
            try
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@AccountID", CurrentUser.AccountID);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex )
            {
                MessageBox.Show(ex.Message);
            }

            finally
            {
                conn.Close();
                MessageBox.Show(CurrentUser.EmailAddress + " is now Activated", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadDataTable();
            }

            
        }

        
        private void loadDataTable()
        {
            string query = "SELECT * FROM Accounts WHERE IsActivated = 0";

            try
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();

                dataAdapter.Fill(dt);
                dataViewer.DataSource = dt;
            }
            catch (Exception ex)
            {

            }

        }

        private void dashboard_Click(object sender, EventArgs e)
        {
            this.Hide();
            AdminDashboard adminDashboard = new AdminDashboard();
            adminDashboard.ShowDialog();
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
            AdminTransaction adminTransaction = new AdminTransaction();
            adminTransaction.ShowDialog();  
        }

        private void btnAddRoom_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            AdminAddPlace adminAddPlace = new AdminAddPlace();
            adminAddPlace.ShowDialog();
        }
    }
}
