namespace Cafffe_Sytem.Pages.SacondryPages
{
    partial class AddCategoryPage
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
            this.label2 = new System.Windows.Forms.Label();
            this.OfferComBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.AddBtn = new System.Windows.Forms.Button();
            this.CategoryTxtBox = new System.Windows.Forms.TextBox();
            this.pagetitle_labl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(67, 339);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 25);
            this.label2.TabIndex = 28;
            this.label2.Text = "Offer";
            // 
            // OfferComBox
            // 
            this.OfferComBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OfferComBox.FormattingEnabled = true;
            this.OfferComBox.Location = new System.Drawing.Point(197, 331);
            this.OfferComBox.Name = "OfferComBox";
            this.OfferComBox.Size = new System.Drawing.Size(250, 33);
            this.OfferComBox.TabIndex = 27;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(67, 249);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 25);
            this.label1.TabIndex = 26;
            this.label1.Text = "category";
            // 
            // AddBtn
            // 
            this.AddBtn.BackColor = System.Drawing.Color.Green;
            this.AddBtn.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.AddBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AddBtn.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.AddBtn.Location = new System.Drawing.Point(213, 486);
            this.AddBtn.Name = "AddBtn";
            this.AddBtn.Size = new System.Drawing.Size(120, 40);
            this.AddBtn.TabIndex = 24;
            this.AddBtn.Text = "Add";
            this.AddBtn.UseVisualStyleBackColor = false;
            this.AddBtn.Click += new System.EventHandler(this.AddBtn_Click);
            // 
            // CategoryTxtBox
            // 
            this.CategoryTxtBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CategoryTxtBox.Location = new System.Drawing.Point(197, 246);
            this.CategoryTxtBox.Name = "CategoryTxtBox";
            this.CategoryTxtBox.Size = new System.Drawing.Size(250, 31);
            this.CategoryTxtBox.TabIndex = 29;
            // 
            // pagetitle_labl
            // 
            this.pagetitle_labl.AutoSize = true;
            this.pagetitle_labl.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pagetitle_labl.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pagetitle_labl.Location = new System.Drawing.Point(183, 81);
            this.pagetitle_labl.Name = "pagetitle_labl";
            this.pagetitle_labl.Size = new System.Drawing.Size(171, 29);
            this.pagetitle_labl.TabIndex = 30;
            this.pagetitle_labl.Text = "Add Category";
            // 
            // AddCategoryPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(133)))), ((int)(((byte)(127)))));
            this.ClientSize = new System.Drawing.Size(534, 662);
            this.Controls.Add(this.pagetitle_labl);
            this.Controls.Add(this.CategoryTxtBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.OfferComBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AddBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(550, 701);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(550, 701);
            this.Name = "AddCategoryPage";
            this.Text = "Add Category";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox OfferComBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button AddBtn;
        private System.Windows.Forms.TextBox CategoryTxtBox;
        private System.Windows.Forms.Label pagetitle_labl;
    }
}