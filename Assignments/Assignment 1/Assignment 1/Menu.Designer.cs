namespace Assignment_1
{
    partial class frmMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMenu));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtBio = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lblBio = new System.Windows.Forms.Label();
            this.lblAge = new System.Windows.Forms.Label();
            this.txtAge = new System.Windows.Forms.TextBox();
            this.txtGender = new System.Windows.Forms.TextBox();
            this.lblGender = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Assignment_1.Properties.Resources.blank_profile;
            this.pictureBox1.Location = new System.Drawing.Point(15, 15);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(128, 128);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // txtBio
            // 
            this.txtBio.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBio.Location = new System.Drawing.Point(155, 155);
            this.txtBio.Margin = new System.Windows.Forms.Padding(6);
            this.txtBio.Multiline = true;
            this.txtBio.Name = "txtBio";
            this.txtBio.ReadOnly = true;
            this.txtBio.Size = new System.Drawing.Size(470, 268);
            this.txtBio.TabIndex = 3;
            this.txtBio.Text = resources.GetString("txtBio.Text");
            this.txtBio.TextChanged += new System.EventHandler(this.txtBio_TextChanged);
            // 
            // txtUsername
            // 
            this.txtUsername.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsername.Location = new System.Drawing.Point(155, 15);
            this.txtUsername.Margin = new System.Windows.Forms.Padding(6);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.ReadOnly = true;
            this.txtUsername.Size = new System.Drawing.Size(470, 68);
            this.txtUsername.TabIndex = 5;
            this.txtUsername.Text = "Username";
            this.txtUsername.TextChanged += new System.EventHandler(this.txtUsername_TextChanged);
            // 
            // lblBio
            // 
            this.lblBio.AutoSize = true;
            this.lblBio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBio.Location = new System.Drawing.Point(67, 155);
            this.lblBio.Margin = new System.Windows.Forms.Padding(6);
            this.lblBio.Name = "lblBio";
            this.lblBio.Size = new System.Drawing.Size(76, 37);
            this.lblBio.TabIndex = 4;
            this.lblBio.Text = "Bio:";
            // 
            // lblAge
            // 
            this.lblAge.AutoSize = true;
            this.lblAge.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAge.Location = new System.Drawing.Point(155, 106);
            this.lblAge.Margin = new System.Windows.Forms.Padding(6);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(87, 37);
            this.lblAge.TabIndex = 6;
            this.lblAge.Text = "Age:";
            // 
            // txtAge
            // 
            this.txtAge.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAge.Location = new System.Drawing.Point(254, 99);
            this.txtAge.Margin = new System.Windows.Forms.Padding(6);
            this.txtAge.Name = "txtAge";
            this.txtAge.ReadOnly = true;
            this.txtAge.Size = new System.Drawing.Size(52, 44);
            this.txtAge.TabIndex = 7;
            this.txtAge.Text = "18";
            // 
            // txtGender
            // 
            this.txtGender.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtGender.Location = new System.Drawing.Point(469, 99);
            this.txtGender.Margin = new System.Windows.Forms.Padding(6);
            this.txtGender.Name = "txtGender";
            this.txtGender.ReadOnly = true;
            this.txtGender.Size = new System.Drawing.Size(156, 44);
            this.txtGender.TabIndex = 8;
            this.txtGender.Text = "Male";
            // 
            // lblGender
            // 
            this.lblGender.AutoSize = true;
            this.lblGender.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGender.Location = new System.Drawing.Point(318, 106);
            this.lblGender.Margin = new System.Windows.Forms.Padding(6);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(139, 37);
            this.lblGender.TabIndex = 9;
            this.lblGender.Text = "Gender:";
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(175, 496);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(431, 227);
            this.btnLogout.TabIndex = 10;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(634, 15);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(131, 68);
            this.btnEdit.TabIndex = 11;
            this.btnEdit.Text = "Edit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // frmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(998, 804);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.lblGender);
            this.Controls.Add(this.txtGender);
            this.Controls.Add(this.txtAge);
            this.Controls.Add(this.lblAge);
            this.Controls.Add(this.txtUsername);
            this.Controls.Add(this.lblBio);
            this.Controls.Add(this.txtBio);
            this.Controls.Add(this.pictureBox1);
            this.Name = "frmMenu";
            this.Text = "Menu";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtBio;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lblBio;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.TextBox txtAge;
        private System.Windows.Forms.TextBox txtGender;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnEdit;
    }
}