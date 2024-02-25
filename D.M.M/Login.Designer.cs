namespace Cafffe_Sytem.D.M.M
{
    partial class Login
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.exist_labl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.username_txt = new System.Windows.Forms.TextBox();
            this.passward_txt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.show_passward_checkBox = new System.Windows.Forms.CheckBox();
            this.login_btn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(133)))), ((int)(((byte)(127)))));
            this.panel1.Controls.Add(this.exist_labl);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(-2, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(427, 194);
            this.panel1.TabIndex = 0;
            // 
            // exist_labl
            // 
            this.exist_labl.AutoSize = true;
            this.exist_labl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.exist_labl.Location = new System.Drawing.Point(387, 9);
            this.exist_labl.Name = "exist_labl";
            this.exist_labl.Size = new System.Drawing.Size(21, 20);
            this.exist_labl.TabIndex = 2;
            this.exist_labl.Text = "X";
            this.exist_labl.Click += new System.EventHandler(this.exist_labl_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Monotype Corsiva", 20F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(141, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 33);
            this.label1.TabIndex = 1;
            this.label1.Text = "Cafe System";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(75, 233);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "UserName";
            // 
            // username_txt
            // 
            this.username_txt.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.username_txt.Location = new System.Drawing.Point(72, 269);
            this.username_txt.Name = "username_txt";
            this.username_txt.Size = new System.Drawing.Size(288, 32);
            this.username_txt.TabIndex = 3;
            // 
            // passward_txt
            // 
            this.passward_txt.Font = new System.Drawing.Font("Microsoft Tai Le", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.passward_txt.Location = new System.Drawing.Point(72, 365);
            this.passward_txt.Name = "passward_txt";
            this.passward_txt.Size = new System.Drawing.Size(288, 32);
            this.passward_txt.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(75, 328);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 24);
            this.label4.TabIndex = 4;
            this.label4.Text = "Passward";
            // 
            // show_passward_checkBox
            // 
            this.show_passward_checkBox.AutoSize = true;
            this.show_passward_checkBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.show_passward_checkBox.Location = new System.Drawing.Point(77, 410);
            this.show_passward_checkBox.Name = "show_passward_checkBox";
            this.show_passward_checkBox.Size = new System.Drawing.Size(99, 17);
            this.show_passward_checkBox.TabIndex = 6;
            this.show_passward_checkBox.Text = "ShowPassward";
            this.show_passward_checkBox.UseVisualStyleBackColor = true;
            this.show_passward_checkBox.CheckedChanged += new System.EventHandler(this.show_passward_checkBox_CheckedChanged);
            // 
            // login_btn
            // 
            this.login_btn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(133)))), ((int)(((byte)(127)))));
            this.login_btn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.login_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.login_btn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.login_btn.Location = new System.Drawing.Point(72, 481);
            this.login_btn.Name = "login_btn";
            this.login_btn.Size = new System.Drawing.Size(288, 35);
            this.login_btn.TabIndex = 7;
            this.login_btn.Text = "LOGIN";
            this.login_btn.UseVisualStyleBackColor = false;
            this.login_btn.Click += new System.EventHandler(this.login_btn_Click);
            this.login_btn.MouseLeave += new System.EventHandler(this.login_btn_MouseLeave);
            this.login_btn.MouseMove += new System.Windows.Forms.MouseEventHandler(this.login_btn_MouseMove);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pictureBox1.Image = global::Cafffe_Sytem.Properties.Resources.cafe1;
            this.pictureBox1.Location = new System.Drawing.Point(153, 24);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(124, 97);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(423, 550);
            this.Controls.Add(this.login_btn);
            this.Controls.Add(this.show_passward_checkBox);
            this.Controls.Add(this.passward_txt);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.username_txt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximumSize = new System.Drawing.Size(423, 550);
            this.MinimumSize = new System.Drawing.Size(423, 550);
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.Load += new System.EventHandler(this.Login_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label exist_labl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox username_txt;
        private System.Windows.Forms.TextBox passward_txt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox show_passward_checkBox;
        private System.Windows.Forms.Button login_btn;
    }
}