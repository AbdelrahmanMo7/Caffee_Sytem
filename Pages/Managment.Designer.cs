namespace Cafffe_Sytem.Pages
{
    partial class Managment
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.Reports_Tab_controls = new System.Windows.Forms.TabControl();
            this.Users = new System.Windows.Forms.TabPage();
            this.searchtxt = new System.Windows.Forms.TextBox();
            this.selectedUserNametxt = new System.Windows.Forms.TextBox();
            this.Searchbtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Users_dataGridView = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Namee = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Password = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Position = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.system = new System.Windows.Forms.TabPage();
            this.Savebtn = new System.Windows.Forms.Button();
            this.instatxt = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.apointmenttxt = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.facebooktxt = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.Nametxt = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.addresstxt = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.phonetxt = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel13 = new System.Windows.Forms.Panel();
            this.panel14 = new System.Windows.Forms.Panel();
            this.panel12 = new System.Windows.Forms.Panel();
            this.panel15 = new System.Windows.Forms.Panel();
            this.editInfobtn = new System.Windows.Forms.Button();
            this.addbtn = new System.Windows.Forms.Button();
            this.Updatebtn = new System.Windows.Forms.Button();
            this.deletebtn = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogOut_pictureBox)).BeginInit();
            this.Reports_Tab_controls.SuspendLayout();
            this.Users.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Users_dataGridView)).BeginInit();
            this.system.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel13.SuspendLayout();
            this.panel12.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Size = new System.Drawing.Size(1155, 749);
            // 
            // panel2
            // 
            this.panel2.AutoSize = false;
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.Size = new System.Drawing.Size(1726, 353);
            // 
            // panel3
            // 
            this.panel3.AutoSize = false;
            this.panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel3.Controls.Add(this.deletebtn);
            this.panel3.Controls.Add(this.Updatebtn);
            this.panel3.Controls.Add(this.addbtn);
            this.panel3.Size = new System.Drawing.Size(1155, 150);
            // 
            // Page_Name
            // 
            this.Page_Name.Size = new System.Drawing.Size(163, 39);
            this.Page_Name.Text = "Managment";
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            this.panel4.Controls.Add(this.Reports_Tab_controls);
            this.panel4.Size = new System.Drawing.Size(1156, 611);
            // 
            // pictureBox2
            // 
            this.LogOut_pictureBox.Anchor = System.Windows.Forms.AnchorStyles.Top;
            // 
            // Reports_Tab_controls
            // 
            this.Reports_Tab_controls.Controls.Add(this.Users);
            this.Reports_Tab_controls.Controls.Add(this.system);
            this.Reports_Tab_controls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Reports_Tab_controls.Font = new System.Drawing.Font("Microsoft Tai Le", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Reports_Tab_controls.Location = new System.Drawing.Point(0, 0);
            this.Reports_Tab_controls.Name = "Reports_Tab_controls";
            this.Reports_Tab_controls.SelectedIndex = 0;
            this.Reports_Tab_controls.Size = new System.Drawing.Size(1156, 611);
            this.Reports_Tab_controls.TabIndex = 1;
            // 
            // Users
            // 
            this.Users.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(239)))), ((int)(((byte)(233)))));
            this.Users.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Users.Controls.Add(this.searchtxt);
            this.Users.Controls.Add(this.selectedUserNametxt);
            this.Users.Controls.Add(this.Searchbtn);
            this.Users.Controls.Add(this.label3);
            this.Users.Controls.Add(this.label2);
            this.Users.Controls.Add(this.Users_dataGridView);
            this.Users.Location = new System.Drawing.Point(4, 35);
            this.Users.Name = "Users";
            this.Users.Padding = new System.Windows.Forms.Padding(3);
            this.Users.Size = new System.Drawing.Size(1148, 572);
            this.Users.TabIndex = 0;
            this.Users.Text = "Users";
            // 
            // searchtxt
            // 
            this.searchtxt.Location = new System.Drawing.Point(626, 23);
            this.searchtxt.Name = "searchtxt";
            this.searchtxt.Size = new System.Drawing.Size(200, 34);
            this.searchtxt.TabIndex = 26;
            this.searchtxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // selectedUserNametxt
            // 
            this.selectedUserNametxt.Location = new System.Drawing.Point(216, 23);
            this.selectedUserNametxt.Name = "selectedUserNametxt";
            this.selectedUserNametxt.ReadOnly = true;
            this.selectedUserNametxt.Size = new System.Drawing.Size(200, 34);
            this.selectedUserNametxt.TabIndex = 25;
            this.selectedUserNametxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Searchbtn
            // 
            this.Searchbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Searchbtn.Location = new System.Drawing.Point(967, 20);
            this.Searchbtn.Name = "Searchbtn";
            this.Searchbtn.Size = new System.Drawing.Size(100, 40);
            this.Searchbtn.TabIndex = 7;
            this.Searchbtn.Text = "Search";
            this.Searchbtn.UseVisualStyleBackColor = true;
            this.Searchbtn.Click += new System.EventHandler(this.Searchbtn_Click);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(548, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 26);
            this.label3.TabIndex = 5;
            this.label3.Text = "Search";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(87, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 26);
            this.label2.TabIndex = 3;
            this.label2.Text = "User Name";
            // 
            // Users_dataGridView
            // 
            this.Users_dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Users_dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.Users_dataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.Users_dataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(239)))), ((int)(((byte)(233)))));
            this.Users_dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Tai Le", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(133)))), ((int)(((byte)(127)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Users_dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.Users_dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Users_dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Namee,
            this.UserName,
            this.Password,
            this.Position});
            this.Users_dataGridView.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(206)))), ((int)(((byte)(176)))));
            this.Users_dataGridView.Location = new System.Drawing.Point(79, 85);
            this.Users_dataGridView.Name = "Users_dataGridView";
            this.Users_dataGridView.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.Users_dataGridView.Size = new System.Drawing.Size(1000, 321);
            this.Users_dataGridView.TabIndex = 1;
            this.Users_dataGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Reports_conteinar_dataGridView1_CellContentClick);
            // 
            // ID
            // 
            this.ID.HeaderText = "User ID";
            this.ID.Name = "ID";
            // 
            // Namee
            // 
            this.Namee.HeaderText = "Name";
            this.Namee.Name = "Namee";
            // 
            // UserName
            // 
            this.UserName.HeaderText = "User Name";
            this.UserName.Name = "UserName";
            // 
            // Password
            // 
            this.Password.HeaderText = "Password";
            this.Password.Name = "Password";
            // 
            // Position
            // 
            this.Position.HeaderText = "Position";
            this.Position.Name = "Position";
            // 
            // system
            // 
            this.system.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(239)))), ((int)(((byte)(233)))));
            this.system.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.system.Controls.Add(this.Savebtn);
            this.system.Controls.Add(this.instatxt);
            this.system.Controls.Add(this.label15);
            this.system.Controls.Add(this.apointmenttxt);
            this.system.Controls.Add(this.label12);
            this.system.Controls.Add(this.facebooktxt);
            this.system.Controls.Add(this.label11);
            this.system.Controls.Add(this.Nametxt);
            this.system.Controls.Add(this.label10);
            this.system.Controls.Add(this.addresstxt);
            this.system.Controls.Add(this.label9);
            this.system.Controls.Add(this.phonetxt);
            this.system.Controls.Add(this.label7);
            this.system.Controls.Add(this.panel6);
            this.system.Controls.Add(this.editInfobtn);
            this.system.Location = new System.Drawing.Point(4, 35);
            this.system.Name = "system";
            this.system.Padding = new System.Windows.Forms.Padding(3);
            this.system.Size = new System.Drawing.Size(1148, 572);
            this.system.TabIndex = 1;
            this.system.Text = "System Information";
            // 
            // Savebtn
            // 
            this.Savebtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(96)))), ((int)(((byte)(176)))));
            this.Savebtn.Location = new System.Drawing.Point(1009, 18);
            this.Savebtn.Name = "Savebtn";
            this.Savebtn.Size = new System.Drawing.Size(100, 40);
            this.Savebtn.TabIndex = 36;
            this.Savebtn.Text = "Save";
            this.Savebtn.UseVisualStyleBackColor = false;
            this.Savebtn.Visible = false;
            this.Savebtn.Click += new System.EventHandler(this.Savebtn_Click);
            // 
            // instatxt
            // 
            this.instatxt.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.instatxt.Location = new System.Drawing.Point(801, 376);
            this.instatxt.Name = "instatxt";
            this.instatxt.ReadOnly = true;
            this.instatxt.Size = new System.Drawing.Size(200, 34);
            this.instatxt.TabIndex = 35;
            this.instatxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(564, 379);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(122, 26);
            this.label15.TabIndex = 34;
            this.label15.Text = "Instagram  :";
            // 
            // apointmenttxt
            // 
            this.apointmenttxt.Location = new System.Drawing.Point(801, 138);
            this.apointmenttxt.Name = "apointmenttxt";
            this.apointmenttxt.ReadOnly = true;
            this.apointmenttxt.Size = new System.Drawing.Size(200, 34);
            this.apointmenttxt.TabIndex = 28;
            this.apointmenttxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(564, 141);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(139, 26);
            this.label12.TabIndex = 27;
            this.label12.Text = "Apointment  :";
            // 
            // facebooktxt
            // 
            this.facebooktxt.Location = new System.Drawing.Point(801, 252);
            this.facebooktxt.Name = "facebooktxt";
            this.facebooktxt.ReadOnly = true;
            this.facebooktxt.Size = new System.Drawing.Size(200, 34);
            this.facebooktxt.TabIndex = 26;
            this.facebooktxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(564, 255);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(111, 26);
            this.label11.TabIndex = 25;
            this.label11.Text = "Facebook :";
            // 
            // Nametxt
            // 
            this.Nametxt.Location = new System.Drawing.Point(290, 133);
            this.Nametxt.Name = "Nametxt";
            this.Nametxt.ReadOnly = true;
            this.Nametxt.Size = new System.Drawing.Size(200, 34);
            this.Nametxt.TabIndex = 24;
            this.Nametxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(53, 136);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 26);
            this.label10.TabIndex = 23;
            this.label10.Text = "Name  :";
            // 
            // addresstxt
            // 
            this.addresstxt.Location = new System.Drawing.Point(290, 381);
            this.addresstxt.Name = "addresstxt";
            this.addresstxt.ReadOnly = true;
            this.addresstxt.Size = new System.Drawing.Size(200, 34);
            this.addresstxt.TabIndex = 22;
            this.addresstxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(47, 384);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(103, 26);
            this.label9.TabIndex = 21;
            this.label9.Text = "Address  :";
            // 
            // phonetxt
            // 
            this.phonetxt.Location = new System.Drawing.Point(290, 255);
            this.phonetxt.Name = "phonetxt";
            this.phonetxt.ReadOnly = true;
            this.phonetxt.Size = new System.Drawing.Size(200, 34);
            this.phonetxt.TabIndex = 18;
            this.phonetxt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(47, 258);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 26);
            this.label7.TabIndex = 17;
            this.label7.Text = "Phone :";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel6.Controls.Add(this.panel13);
            this.panel6.Controls.Add(this.panel12);
            this.panel6.Location = new System.Drawing.Point(545, 101);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(2, 350);
            this.panel6.TabIndex = 15;
            // 
            // panel13
            // 
            this.panel13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel13.Controls.Add(this.panel14);
            this.panel13.Location = new System.Drawing.Point(22, 0);
            this.panel13.Name = "panel13";
            this.panel13.Size = new System.Drawing.Size(2, 350);
            this.panel13.TabIndex = 17;
            // 
            // panel14
            // 
            this.panel14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel14.Location = new System.Drawing.Point(0, 0);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(2, 350);
            this.panel14.TabIndex = 16;
            // 
            // panel12
            // 
            this.panel12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel12.Controls.Add(this.panel15);
            this.panel12.Location = new System.Drawing.Point(0, 0);
            this.panel12.Name = "panel12";
            this.panel12.Size = new System.Drawing.Size(2, 350);
            this.panel12.TabIndex = 16;
            // 
            // panel15
            // 
            this.panel15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel15.Location = new System.Drawing.Point(0, 0);
            this.panel15.Name = "panel15";
            this.panel15.Size = new System.Drawing.Size(2, 350);
            this.panel15.TabIndex = 17;
            // 
            // editInfobtn
            // 
            this.editInfobtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(96)))), ((int)(((byte)(176)))));
            this.editInfobtn.Location = new System.Drawing.Point(1009, 18);
            this.editInfobtn.Name = "editInfobtn";
            this.editInfobtn.Size = new System.Drawing.Size(100, 40);
            this.editInfobtn.TabIndex = 12;
            this.editInfobtn.Text = "Edit";
            this.editInfobtn.UseVisualStyleBackColor = false;
            this.editInfobtn.Click += new System.EventHandler(this.editInfobtn_Click);
            // 
            // addbtn
            // 
            this.addbtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addbtn.BackColor = System.Drawing.Color.Green;
            this.addbtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.addbtn.Font = new System.Drawing.Font("Microsoft Tai Le", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addbtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.addbtn.Location = new System.Drawing.Point(551, 32);
            this.addbtn.Name = "addbtn";
            this.addbtn.Size = new System.Drawing.Size(120, 40);
            this.addbtn.TabIndex = 8;
            this.addbtn.Text = "Add";
            this.addbtn.UseVisualStyleBackColor = false;
            this.addbtn.Click += new System.EventHandler(this.addbtn_Click);
            // 
            // Updatebtn
            // 
            this.Updatebtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Updatebtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(11)))), ((int)(((byte)(96)))), ((int)(((byte)(176)))));
            this.Updatebtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Updatebtn.Font = new System.Drawing.Font("Microsoft Tai Le", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Updatebtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Updatebtn.Location = new System.Drawing.Point(712, 32);
            this.Updatebtn.Name = "Updatebtn";
            this.Updatebtn.Size = new System.Drawing.Size(120, 40);
            this.Updatebtn.TabIndex = 9;
            this.Updatebtn.Text = "Update";
            this.Updatebtn.UseVisualStyleBackColor = false;
            this.Updatebtn.Click += new System.EventHandler(this.Updatebtn_Click);
            // 
            // deletebtn
            // 
            this.deletebtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deletebtn.BackColor = System.Drawing.Color.Brown;
            this.deletebtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.deletebtn.Font = new System.Drawing.Font("Microsoft Tai Le", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.deletebtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.deletebtn.Location = new System.Drawing.Point(875, 32);
            this.deletebtn.Name = "deletebtn";
            this.deletebtn.Size = new System.Drawing.Size(120, 40);
            this.deletebtn.TabIndex = 10;
            this.deletebtn.Text = "Delete";
            this.deletebtn.UseVisualStyleBackColor = false;
            this.deletebtn.Visible = false;
            this.deletebtn.Click += new System.EventHandler(this.deletebtn_Click);
            // 
            // Managment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1370, 749);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "Managment";
            this.Text = "Managment";
            this.Load += new System.EventHandler(this.Managment_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LogOut_pictureBox)).EndInit();
            this.Reports_Tab_controls.ResumeLayout(false);
            this.Users.ResumeLayout(false);
            this.Users.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Users_dataGridView)).EndInit();
            this.system.ResumeLayout(false);
            this.system.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel13.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl Reports_Tab_controls;
        private System.Windows.Forms.TabPage Users;
        private System.Windows.Forms.Button Searchbtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView Users_dataGridView;
        private System.Windows.Forms.TabPage system;
        private System.Windows.Forms.TextBox instatxt;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox apointmenttxt;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox facebooktxt;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox Nametxt;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox addresstxt;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox phonetxt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Button editInfobtn;
        private System.Windows.Forms.Button deletebtn;
        private System.Windows.Forms.Button Updatebtn;
        private System.Windows.Forms.Button addbtn;
        private System.Windows.Forms.TextBox searchtxt;
        private System.Windows.Forms.TextBox selectedUserNametxt;
        private System.Windows.Forms.Button Savebtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Namee;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Password;
        private System.Windows.Forms.DataGridViewTextBoxColumn Position;
    }
}