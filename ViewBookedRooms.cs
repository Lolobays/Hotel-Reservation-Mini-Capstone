﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TRABYAHE
{
    public partial class ViewBookedRooms : Form
    {
        public ViewBookedRooms()
        {
            InitializeComponent();
        }
        public static string consString = "Data Source=DESKTOP-3SPCRJ0\\SQLEXPRESS;Initial Catalog=Trabyahe;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        SqlConnection conn = new SqlConnection(consString);

        private void ViewBookedRooms_Load(object sender, EventArgs e)
        {
            loadDataTable();

            dataViewer.ReadOnly = true;
            dataViewer.AllowUserToAddRows = true;
            dataViewer.AllowUserToDeleteRows = true;
            dataViewer.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataViewer.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataViewer.DefaultCellStyle.ForeColor = Color.Black;
        }

        private void loadDataTable()
        {
            try
            {
                string query = "SELECT * FROM Rooms WHERE Room_Availability = 0";

                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();

                dataAdapter.Fill(dt);

                dataViewer.DataSource = dt;
            }
            catch (Exception ex)
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        
    }
}
