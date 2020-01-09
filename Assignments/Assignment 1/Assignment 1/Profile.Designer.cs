namespace Assignment_1
{
    partial class frmProfile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProfile));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblAgeLabel = new System.Windows.Forms.Label();
            this.lblGenderLabel = new System.Windows.Forms.Label();
            this.lblBioLabel = new System.Windows.Forms.Label();
            this.lblEmailLabel = new System.Windows.Forms.Label();
            this.lblFirstnameLabel = new System.Windows.Forms.Label();
            this.lblLastnameLabel = new System.Windows.Forms.Label();
            this.lblAge = new System.Windows.Forms.Label();
            this.lblGender = new System.Windows.Forms.Label();
            this.txtBio = new System.Windows.Forms.TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblFirstname = new System.Windows.Forms.Label();
            this.lblLastname = new System.Windows.Forms.Label();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Assignment_1.Properties.Resources.blank_profile;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // lblUsername
            // 
            resources.ApplyResources(this.lblUsername, "lblUsername");
            this.lblUsername.Name = "lblUsername";
            // 
            // lblAgeLabel
            // 
            resources.ApplyResources(this.lblAgeLabel, "lblAgeLabel");
            this.lblAgeLabel.Name = "lblAgeLabel";
            // 
            // lblGenderLabel
            // 
            resources.ApplyResources(this.lblGenderLabel, "lblGenderLabel");
            this.lblGenderLabel.Name = "lblGenderLabel";
            // 
            // lblBioLabel
            // 
            resources.ApplyResources(this.lblBioLabel, "lblBioLabel");
            this.lblBioLabel.Name = "lblBioLabel";
            // 
            // lblEmailLabel
            // 
            resources.ApplyResources(this.lblEmailLabel, "lblEmailLabel");
            this.lblEmailLabel.Name = "lblEmailLabel";
            // 
            // lblFirstnameLabel
            // 
            resources.ApplyResources(this.lblFirstnameLabel, "lblFirstnameLabel");
            this.lblFirstnameLabel.Name = "lblFirstnameLabel";
            // 
            // lblLastnameLabel
            // 
            resources.ApplyResources(this.lblLastnameLabel, "lblLastnameLabel");
            this.lblLastnameLabel.Name = "lblLastnameLabel";
            // 
            // lblAge
            // 
            resources.ApplyResources(this.lblAge, "lblAge");
            this.lblAge.Name = "lblAge";
            // 
            // lblGender
            // 
            resources.ApplyResources(this.lblGender, "lblGender");
            this.lblGender.Name = "lblGender";
            // 
            // txtBio
            // 
            resources.ApplyResources(this.txtBio, "txtBio");
            this.txtBio.Name = "txtBio";
            // 
            // lblEmail
            // 
            resources.ApplyResources(this.lblEmail, "lblEmail");
            this.lblEmail.Name = "lblEmail";
            // 
            // lblFirstname
            // 
            resources.ApplyResources(this.lblFirstname, "lblFirstname");
            this.lblFirstname.Name = "lblFirstname";
            // 
            // lblLastname
            // 
            resources.ApplyResources(this.lblLastname, "lblLastname");
            this.lblLastname.Name = "lblLastname";
            // 
            // btnEdit
            // 
            resources.ApplyResources(this.btnEdit, "btnEdit");
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnLogout
            // 
            resources.ApplyResources(this.btnLogout, "btnLogout");
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // frmProfile
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.lblLastname);
            this.Controls.Add(this.lblFirstname);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtBio);
            this.Controls.Add(this.lblGender);
            this.Controls.Add(this.lblAge);
            this.Controls.Add(this.lblLastnameLabel);
            this.Controls.Add(this.lblFirstnameLabel);
            this.Controls.Add(this.lblEmailLabel);
            this.Controls.Add(this.lblBioLabel);
            this.Controls.Add(this.lblGenderLabel);
            this.Controls.Add(this.lblAgeLabel);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.pictureBox1);
            this.Name = "frmProfile";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Label lblAgeLabel;
        private System.Windows.Forms.Label lblGenderLabel;
        private System.Windows.Forms.Label lblBioLabel;
        private System.Windows.Forms.Label lblEmailLabel;
        private System.Windows.Forms.Label lblFirstnameLabel;
        private System.Windows.Forms.Label lblLastnameLabel;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.TextBox txtBio;
        private System.Windows.Forms.Label lblEmail;
        private System.Windows.Forms.Label lblFirstname;
        private System.Windows.Forms.Label lblLastname;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnLogout;
    }
}