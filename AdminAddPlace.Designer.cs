namespace TRABYAHE
{
    partial class AdminAddPlace
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminAddPlace));
            this.panel3 = new System.Windows.Forms.Panel();
            this.dataViewerPlaces = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.txtRoomDescription = new System.Windows.Forms.TextBox();
            this.txtRoomPrice = new System.Windows.Forms.TextBox();
            this.txtRoomNumber = new System.Windows.Forms.TextBox();
            this.txtRoomType = new System.Windows.Forms.ComboBox();
            this.txtRoomName = new System.Windows.Forms.TextBox();
            this.txtRoomID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.PictureBoxPlaces = new System.Windows.Forms.PictureBox();
            this.btnImportImages = new System.Windows.Forms.Button();
            this.btnClearPlaces = new System.Windows.Forms.Button();
            this.btnDeletePlaces = new System.Windows.Forms.Button();
            this.btnUpdatePlaces = new System.Windows.Forms.Button();
            this.btnAddPlaces = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.exit = new System.Windows.Forms.Button();
            this.dashboard = new System.Windows.Forms.Button();
            this.btnLogOut = new System.Windows.Forms.Button();
            this.btnAddRoom = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnValidateAccount = new System.Windows.Forms.Button();
            this.btnTransactions = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataViewerPlaces)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxPlaces)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.dataViewerPlaces);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Location = new System.Drawing.Point(210, 53);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(878, 243);
            this.panel3.TabIndex = 33;
            // 
            // dataViewerPlaces
            // 
            this.dataViewerPlaces.BackgroundColor = System.Drawing.Color.Lavender;
            this.dataViewerPlaces.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataViewerPlaces.GridColor = System.Drawing.Color.Purple;
            this.dataViewerPlaces.Location = new System.Drawing.Point(17, 51);
            this.dataViewerPlaces.Name = "dataViewerPlaces";
            this.dataViewerPlaces.RowHeadersWidth = 51;
            this.dataViewerPlaces.RowTemplate.Height = 24;
            this.dataViewerPlaces.Size = new System.Drawing.Size(842, 172);
            this.dataViewerPlaces.TabIndex = 1;
            this.dataViewerPlaces.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataViewerPlaces_CellContentClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 19);
            this.label2.TabIndex = 0;
            this.label2.Text = "All Rooms";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.White;
            this.panel4.Controls.Add(this.txtRoomDescription);
            this.panel4.Controls.Add(this.txtRoomPrice);
            this.panel4.Controls.Add(this.txtRoomNumber);
            this.panel4.Controls.Add(this.txtRoomType);
            this.panel4.Controls.Add(this.txtRoomName);
            this.panel4.Controls.Add(this.txtRoomID);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.PictureBoxPlaces);
            this.panel4.Controls.Add(this.btnImportImages);
            this.panel4.Controls.Add(this.btnClearPlaces);
            this.panel4.Controls.Add(this.btnDeletePlaces);
            this.panel4.Controls.Add(this.btnUpdatePlaces);
            this.panel4.Controls.Add(this.btnAddPlaces);
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Location = new System.Drawing.Point(210, 302);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(878, 386);
            this.panel4.TabIndex = 34;
            // 
            // txtRoomDescription
            // 
            this.txtRoomDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRoomDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.txtRoomDescription.Location = new System.Drawing.Point(27, 255);
            this.txtRoomDescription.Multiline = true;
            this.txtRoomDescription.Name = "txtRoomDescription";
            this.txtRoomDescription.Size = new System.Drawing.Size(402, 114);
            this.txtRoomDescription.TabIndex = 21;
            // 
            // txtRoomPrice
            // 
            this.txtRoomPrice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRoomPrice.Font = new System.Drawing.Font("Tahoma", 10.2F);
            this.txtRoomPrice.Location = new System.Drawing.Point(161, 194);
            this.txtRoomPrice.Name = "txtRoomPrice";
            this.txtRoomPrice.Size = new System.Drawing.Size(268, 24);
            this.txtRoomPrice.TabIndex = 20;
            // 
            // txtRoomNumber
            // 
            this.txtRoomNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRoomNumber.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRoomNumber.Location = new System.Drawing.Point(161, 153);
            this.txtRoomNumber.Name = "txtRoomNumber";
            this.txtRoomNumber.Size = new System.Drawing.Size(268, 24);
            this.txtRoomNumber.TabIndex = 19;
            // 
            // txtRoomType
            // 
            this.txtRoomType.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRoomType.FormattingEnabled = true;
            this.txtRoomType.Location = new System.Drawing.Point(161, 107);
            this.txtRoomType.Name = "txtRoomType";
            this.txtRoomType.Size = new System.Drawing.Size(268, 25);
            this.txtRoomType.TabIndex = 18;
            // 
            // txtRoomName
            // 
            this.txtRoomName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRoomName.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRoomName.Location = new System.Drawing.Point(161, 66);
            this.txtRoomName.Name = "txtRoomName";
            this.txtRoomName.Size = new System.Drawing.Size(268, 24);
            this.txtRoomName.TabIndex = 17;
            // 
            // txtRoomID
            // 
            this.txtRoomID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRoomID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.txtRoomID.Location = new System.Drawing.Point(161, 22);
            this.txtRoomID.Name = "txtRoomID";
            this.txtRoomID.Size = new System.Drawing.Size(268, 23);
            this.txtRoomID.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(25, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 17);
            this.label1.TabIndex = 15;
            this.label1.Text = "Room Name:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(23, 232);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(122, 17);
            this.label8.TabIndex = 14;
            this.label8.Text = "Room Description:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 10.2F);
            this.label7.Location = new System.Drawing.Point(25, 194);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 17);
            this.label7.TabIndex = 13;
            this.label7.Text = "Room Price:";
            // 
            // PictureBoxPlaces
            // 
            this.PictureBoxPlaces.BackColor = System.Drawing.Color.Lavender;
            this.PictureBoxPlaces.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PictureBoxPlaces.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PictureBoxPlaces.Location = new System.Drawing.Point(476, 34);
            this.PictureBoxPlaces.Name = "PictureBoxPlaces";
            this.PictureBoxPlaces.Size = new System.Drawing.Size(365, 166);
            this.PictureBoxPlaces.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.PictureBoxPlaces.TabIndex = 10;
            this.PictureBoxPlaces.TabStop = false;
            // 
            // btnImportImages
            // 
            this.btnImportImages.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.btnImportImages.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnImportImages.ForeColor = System.Drawing.Color.White;
            this.btnImportImages.Location = new System.Drawing.Point(606, 205);
            this.btnImportImages.Name = "btnImportImages";
            this.btnImportImages.Size = new System.Drawing.Size(107, 30);
            this.btnImportImages.TabIndex = 9;
            this.btnImportImages.Text = "Import";
            this.btnImportImages.UseVisualStyleBackColor = false;
            this.btnImportImages.Click += new System.EventHandler(this.btnImportImages_Click);
            // 
            // btnClearPlaces
            // 
            this.btnClearPlaces.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(55)))), ((int)(((byte)(126)))));
            this.btnClearPlaces.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearPlaces.ForeColor = System.Drawing.Color.White;
            this.btnClearPlaces.Location = new System.Drawing.Point(476, 311);
            this.btnClearPlaces.Name = "btnClearPlaces";
            this.btnClearPlaces.Size = new System.Drawing.Size(158, 41);
            this.btnClearPlaces.TabIndex = 8;
            this.btnClearPlaces.Text = "Clear";
            this.btnClearPlaces.UseVisualStyleBackColor = false;
            this.btnClearPlaces.Click += new System.EventHandler(this.btnClearPlaces_Click);
            // 
            // btnDeletePlaces
            // 
            this.btnDeletePlaces.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(55)))), ((int)(((byte)(126)))));
            this.btnDeletePlaces.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeletePlaces.ForeColor = System.Drawing.Color.White;
            this.btnDeletePlaces.Location = new System.Drawing.Point(683, 311);
            this.btnDeletePlaces.Name = "btnDeletePlaces";
            this.btnDeletePlaces.Size = new System.Drawing.Size(158, 41);
            this.btnDeletePlaces.TabIndex = 7;
            this.btnDeletePlaces.Text = "Delete";
            this.btnDeletePlaces.UseVisualStyleBackColor = false;
            this.btnDeletePlaces.Click += new System.EventHandler(this.btnDeletePlaces_Click);
            // 
            // btnUpdatePlaces
            // 
            this.btnUpdatePlaces.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(55)))), ((int)(((byte)(126)))));
            this.btnUpdatePlaces.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUpdatePlaces.ForeColor = System.Drawing.Color.White;
            this.btnUpdatePlaces.Location = new System.Drawing.Point(683, 255);
            this.btnUpdatePlaces.Name = "btnUpdatePlaces";
            this.btnUpdatePlaces.Size = new System.Drawing.Size(158, 41);
            this.btnUpdatePlaces.TabIndex = 6;
            this.btnUpdatePlaces.Text = "Update";
            this.btnUpdatePlaces.UseVisualStyleBackColor = false;
            this.btnUpdatePlaces.Click += new System.EventHandler(this.btnUpdatePlaces_Click);
            // 
            // btnAddPlaces
            // 
            this.btnAddPlaces.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(55)))), ((int)(((byte)(126)))));
            this.btnAddPlaces.Font = new System.Drawing.Font("Tahoma", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddPlaces.ForeColor = System.Drawing.Color.White;
            this.btnAddPlaces.Location = new System.Drawing.Point(476, 255);
            this.btnAddPlaces.Name = "btnAddPlaces";
            this.btnAddPlaces.Size = new System.Drawing.Size(158, 41);
            this.btnAddPlaces.TabIndex = 5;
            this.btnAddPlaces.Text = "Add";
            this.btnAddPlaces.UseVisualStyleBackColor = false;
            this.btnAddPlaces.Click += new System.EventHandler(this.btnAddPlaces_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(25, 153);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "Room Number:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(25, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 17);
            this.label4.TabIndex = 1;
            this.label4.Text = "Room Type:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(25, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "Room ID: ";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.panel2.Controls.Add(this.btnExit);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.exit);
            this.panel2.Location = new System.Drawing.Point(198, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(904, 47);
            this.panel2.TabIndex = 36;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Crimson;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(810, 11);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(82, 27);
            this.btnExit.TabIndex = 32;
            this.btnExit.Text = "X";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(8, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(403, 47);
            this.label6.TabIndex = 30;
            this.label6.Text = "Hotel Reservation System";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // exit
            // 
            this.exit.BackColor = System.Drawing.Color.Crimson;
            this.exit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exit.ForeColor = System.Drawing.Color.White;
            this.exit.Location = new System.Drawing.Point(1003, 11);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(82, 27);
            this.exit.TabIndex = 0;
            this.exit.Text = "X";
            this.exit.UseVisualStyleBackColor = false;
            // 
            // dashboard
            // 
            this.dashboard.FlatAppearance.BorderSize = 0;
            this.dashboard.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkMagenta;
            this.dashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dashboard.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dashboard.ForeColor = System.Drawing.Color.White;
            this.dashboard.Location = new System.Drawing.Point(0, 255);
            this.dashboard.Name = "dashboard";
            this.dashboard.Size = new System.Drawing.Size(200, 68);
            this.dashboard.TabIndex = 28;
            this.dashboard.Text = "Dashboard";
            this.dashboard.UseVisualStyleBackColor = true;
            this.dashboard.Click += new System.EventHandler(this.dashboard_Click);
            // 
            // btnLogOut
            // 
            this.btnLogOut.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.btnLogOut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogOut.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogOut.ForeColor = System.Drawing.Color.White;
            this.btnLogOut.Location = new System.Drawing.Point(0, 674);
            this.btnLogOut.Name = "btnLogOut";
            this.btnLogOut.Size = new System.Drawing.Size(200, 26);
            this.btnLogOut.TabIndex = 1;
            this.btnLogOut.Text = "LOG OUT";
            this.btnLogOut.UseVisualStyleBackColor = false;
            this.btnLogOut.Click += new System.EventHandler(this.btnLogOut_Click);
            // 
            // btnAddRoom
            // 
            this.btnAddRoom.FlatAppearance.BorderSize = 0;
            this.btnAddRoom.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkMagenta;
            this.btnAddRoom.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddRoom.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddRoom.ForeColor = System.Drawing.Color.White;
            this.btnAddRoom.Location = new System.Drawing.Point(0, 322);
            this.btnAddRoom.Name = "btnAddRoom";
            this.btnAddRoom.Size = new System.Drawing.Size(200, 68);
            this.btnAddRoom.TabIndex = 31;
            this.btnAddRoom.Text = "Add Room";
            this.btnAddRoom.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(55)))), ((int)(((byte)(126)))));
            this.panel1.Controls.Add(this.btnValidateAccount);
            this.panel1.Controls.Add(this.btnTransactions);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.btnAddRoom);
            this.panel1.Controls.Add(this.btnLogOut);
            this.panel1.Controls.Add(this.dashboard);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 700);
            this.panel1.TabIndex = 35;
            // 
            // btnValidateAccount
            // 
            this.btnValidateAccount.FlatAppearance.BorderSize = 0;
            this.btnValidateAccount.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkMagenta;
            this.btnValidateAccount.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnValidateAccount.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnValidateAccount.ForeColor = System.Drawing.Color.White;
            this.btnValidateAccount.Location = new System.Drawing.Point(0, 459);
            this.btnValidateAccount.Name = "btnValidateAccount";
            this.btnValidateAccount.Size = new System.Drawing.Size(200, 68);
            this.btnValidateAccount.TabIndex = 41;
            this.btnValidateAccount.Text = "Validate Accounts";
            this.btnValidateAccount.UseMnemonic = false;
            this.btnValidateAccount.UseVisualStyleBackColor = true;
            this.btnValidateAccount.Click += new System.EventHandler(this.btnValidateAccount_Click);
            // 
            // btnTransactions
            // 
            this.btnTransactions.FlatAppearance.BorderSize = 0;
            this.btnTransactions.FlatAppearance.MouseOverBackColor = System.Drawing.Color.DarkMagenta;
            this.btnTransactions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTransactions.Font = new System.Drawing.Font("Tahoma", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTransactions.ForeColor = System.Drawing.Color.White;
            this.btnTransactions.Location = new System.Drawing.Point(0, 391);
            this.btnTransactions.Name = "btnTransactions";
            this.btnTransactions.Size = new System.Drawing.Size(200, 68);
            this.btnTransactions.TabIndex = 34;
            this.btnTransactions.Text = "Transactions";
            this.btnTransactions.UseVisualStyleBackColor = true;
            this.btnTransactions.Click += new System.EventHandler(this.btnTransactions_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(-21, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(239, 240);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 32;
            this.pictureBox1.TabStop = false;
            // 
            // AdminAddPlace
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1100, 700);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AdminAddPlace";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AdminAddPlace";
            this.Load += new System.EventHandler(this.AdminAddPlace_Load_1);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataViewerPlaces)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBoxPlaces)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnAddPlaces;
        private System.Windows.Forms.Button btnUpdatePlaces;
        private System.Windows.Forms.Button btnDeletePlaces;
        private System.Windows.Forms.Button btnClearPlaces;
        private System.Windows.Forms.Button btnImportImages;
        private System.Windows.Forms.PictureBox PictureBoxPlaces;
        private System.Windows.Forms.DataGridView dataViewerPlaces;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button exit;
        private System.Windows.Forms.Button dashboard;
        private System.Windows.Forms.Button btnLogOut;
        private System.Windows.Forms.Button btnAddRoom;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtRoomDescription;
        private System.Windows.Forms.TextBox txtRoomPrice;
        private System.Windows.Forms.TextBox txtRoomNumber;
        private System.Windows.Forms.ComboBox txtRoomType;
        private System.Windows.Forms.TextBox txtRoomName;
        private System.Windows.Forms.TextBox txtRoomID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnTransactions;
        private System.Windows.Forms.Button btnValidateAccount;
    }
}