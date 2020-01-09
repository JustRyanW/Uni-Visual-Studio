namespace Assignment_1
{
    partial class frmHome
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
            this.brnProfile = new System.Windows.Forms.Button();
            this.btnUsers = new System.Windows.Forms.Button();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnQuit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // brnProfile
            // 
            this.brnProfile.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.brnProfile.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.brnProfile.Location = new System.Drawing.Point(76, 57);
            this.brnProfile.Margin = new System.Windows.Forms.Padding(6);
            this.brnProfile.Name = "brnProfile";
            this.brnProfile.Size = new System.Drawing.Size(200, 100);
            this.brnProfile.TabIndex = 0;
            this.brnProfile.Text = "Profile";
            this.brnProfile.UseVisualStyleBackColor = true;
            this.brnProfile.Click += new System.EventHandler(this.brnProfile_Click);
            // 
            // btnUsers
            // 
            this.btnUsers.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnUsers.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnUsers.Location = new System.Drawing.Point(288, 57);
            this.btnUsers.Margin = new System.Windows.Forms.Padding(6);
            this.btnUsers.Name = "btnUsers";
            this.btnUsers.Size = new System.Drawing.Size(200, 100);
            this.btnUsers.TabIndex = 1;
            this.btnUsers.Text = "Users";
            this.btnUsers.UseVisualStyleBackColor = true;
            this.btnUsers.Click += new System.EventHandler(this.btnUsers_Click);
            // 
            // btnLogout
            // 
            this.btnLogout.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnLogout.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.Location = new System.Drawing.Point(76, 169);
            this.btnLogout.Margin = new System.Windows.Forms.Padding(6);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(200, 100);
            this.btnLogout.TabIndex = 2;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnQuit
            // 
            this.btnQuit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnQuit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnQuit.Location = new System.Drawing.Point(288, 169);
            this.btnQuit.Margin = new System.Windows.Forms.Padding(6);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(200, 100);
            this.btnQuit.TabIndex = 3;
            this.btnQuit.Text = "Quit";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // frmHome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 329);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.btnUsers);
            this.Controls.Add(this.brnProfile);
            this.Name = "frmHome";
            this.Text = "Home";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button brnProfile;
        private System.Windows.Forms.Button btnUsers;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnQuit;
    }
}