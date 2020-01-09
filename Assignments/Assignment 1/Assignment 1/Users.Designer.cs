namespace Assignment_1
{
    partial class frmUsers
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
            this.lsbUsers = new System.Windows.Forms.ListBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnProfile = new System.Windows.Forms.Button();
            this.cbxSort = new System.Windows.Forms.ComboBox();
            this.lblSortLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lsbUsers
            // 
            this.lsbUsers.FormattingEnabled = true;
            this.lsbUsers.ItemHeight = 25;
            this.lsbUsers.Location = new System.Drawing.Point(15, 80);
            this.lsbUsers.Margin = new System.Windows.Forms.Padding(6);
            this.lsbUsers.Name = "lsbUsers";
            this.lsbUsers.Size = new System.Drawing.Size(282, 304);
            this.lsbUsers.TabIndex = 0;
            // 
            // btnBack
            // 
            this.btnBack.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnBack.Location = new System.Drawing.Point(631, 15);
            this.btnBack.Margin = new System.Windows.Forms.Padding(6);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(128, 68);
            this.btnBack.TabIndex = 29;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnProfile
            // 
            this.btnProfile.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnProfile.Location = new System.Drawing.Point(631, 95);
            this.btnProfile.Margin = new System.Windows.Forms.Padding(6);
            this.btnProfile.Name = "btnProfile";
            this.btnProfile.Size = new System.Drawing.Size(128, 68);
            this.btnProfile.TabIndex = 30;
            this.btnProfile.Text = "Profile";
            this.btnProfile.UseVisualStyleBackColor = true;
            this.btnProfile.Click += new System.EventHandler(this.btnProfile_Click);
            // 
            // cbxSort
            // 
            this.cbxSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSort.FormattingEnabled = true;
            this.cbxSort.Location = new System.Drawing.Point(631, 212);
            this.cbxSort.Margin = new System.Windows.Forms.Padding(6);
            this.cbxSort.Name = "cbxSort";
            this.cbxSort.Size = new System.Drawing.Size(128, 33);
            this.cbxSort.TabIndex = 31;
            // 
            // lblSortLabel
            // 
            this.lblSortLabel.AutoSize = true;
            this.lblSortLabel.Location = new System.Drawing.Point(655, 175);
            this.lblSortLabel.Margin = new System.Windows.Forms.Padding(6);
            this.lblSortLabel.Name = "lblSortLabel";
            this.lblSortLabel.Size = new System.Drawing.Size(82, 25);
            this.lblSortLabel.TabIndex = 32;
            this.lblSortLabel.Text = "Sort By";
            // 
            // frmUsers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 729);
            this.Controls.Add(this.lblSortLabel);
            this.Controls.Add(this.cbxSort);
            this.Controls.Add(this.btnProfile);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.lsbUsers);
            this.MinimumSize = new System.Drawing.Size(800, 800);
            this.Name = "frmUsers";
            this.Text = "Users";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lsbUsers;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnProfile;
        private System.Windows.Forms.ComboBox cbxSort;
        private System.Windows.Forms.Label lblSortLabel;
    }
}