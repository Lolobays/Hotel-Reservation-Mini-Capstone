using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;
using TRABYAHE;

namespace Trabyahe
{
    public partial class UserDashboard : Form
    {
        public UserDashboard()
        {
            InitializeComponent();
        }
        public static string consString = "Data Source=DESKTOP-3SPCRJ0\\SQLEXPRESS;Initial Catalog=Trabyahe;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        SqlConnection conn = new SqlConnection(consString);

        private void btnBookRoom_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserReservation userReservation = new UserReservation();
            userReservation.ShowDialog();

        }
        private void btnLogOut_Click(object sender, EventArgs e)
        {
           this.Hide();
           LogIn logIn = new LogIn();
           logIn.ShowDialog();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnBookingHistory_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserBookingHistory history = new UserBookingHistory();
            history.ShowDialog();
        }

        //Functionality Over Here





        // Methods Below Here

    }
}