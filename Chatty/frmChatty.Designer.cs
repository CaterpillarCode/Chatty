namespace Chatty
{
    partial class frmChatty
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
            this.gbClientMessages = new System.Windows.Forms.GroupBox();
            this.rtbClientMessages = new System.Windows.Forms.RichTextBox();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.HomePage = new System.Windows.Forms.TabPage();
            this.lbUsername = new System.Windows.Forms.Label();
            this.clbActiveClients = new System.Windows.Forms.CheckedListBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.TbMsg = new System.Windows.Forms.TextBox();
            this.SettingsPage = new System.Windows.Forms.TabPage();
            this.gbClientMessages.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.HomePage.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbClientMessages
            // 
            this.gbClientMessages.Controls.Add(this.rtbClientMessages);
            this.gbClientMessages.Location = new System.Drawing.Point(8, 6);
            this.gbClientMessages.Name = "gbClientMessages";
            this.gbClientMessages.Size = new System.Drawing.Size(660, 356);
            this.gbClientMessages.TabIndex = 4;
            this.gbClientMessages.TabStop = false;
            this.gbClientMessages.Text = "Chatty Console";
            // 
            // rtbClientMessages
            // 
            this.rtbClientMessages.Font = new System.Drawing.Font("Rubik", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbClientMessages.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.rtbClientMessages.Location = new System.Drawing.Point(6, 19);
            this.rtbClientMessages.Name = "rtbClientMessages";
            this.rtbClientMessages.ReadOnly = true;
            this.rtbClientMessages.Size = new System.Drawing.Size(648, 331);
            this.rtbClientMessages.TabIndex = 3;
            this.rtbClientMessages.Text = "";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.HomePage);
            this.tabControl.Controls.Add(this.SettingsPage);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(804, 451);
            this.tabControl.TabIndex = 2;
            // 
            // HomePage
            // 
            this.HomePage.BackColor = System.Drawing.SystemColors.Window;
            this.HomePage.Controls.Add(this.lbUsername);
            this.HomePage.Controls.Add(this.clbActiveClients);
            this.HomePage.Controls.Add(this.btnConnect);
            this.HomePage.Controls.Add(this.tbUsername);
            this.HomePage.Controls.Add(this.gbClientMessages);
            this.HomePage.Controls.Add(this.btnSend);
            this.HomePage.Controls.Add(this.TbMsg);
            this.HomePage.Location = new System.Drawing.Point(4, 22);
            this.HomePage.Name = "HomePage";
            this.HomePage.Padding = new System.Windows.Forms.Padding(3);
            this.HomePage.Size = new System.Drawing.Size(796, 425);
            this.HomePage.TabIndex = 0;
            this.HomePage.Text = "Chatty";
            // 
            // lbUsername
            // 
            this.lbUsername.AutoSize = true;
            this.lbUsername.Location = new System.Drawing.Point(701, 10);
            this.lbUsername.Name = "lbUsername";
            this.lbUsername.Size = new System.Drawing.Size(58, 13);
            this.lbUsername.TabIndex = 8;
            this.lbUsername.Text = "Username:";
            // 
            // clbActiveClients
            // 
            this.clbActiveClients.FormattingEnabled = true;
            this.clbActiveClients.Location = new System.Drawing.Point(671, 85);
            this.clbActiveClients.Name = "clbActiveClients";
            this.clbActiveClients.Size = new System.Drawing.Size(119, 334);
            this.clbActiveClients.TabIndex = 7;
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(671, 52);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(119, 23);
            this.btnConnect.TabIndex = 6;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.BtnConnect_Click);
            // 
            // tbUsername
            // 
            this.tbUsername.Location = new System.Drawing.Point(671, 26);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(119, 20);
            this.tbUsername.TabIndex = 5;
            this.tbUsername.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TbUsername_KeyDown);
            // 
            // btnSend
            // 
            this.btnSend.Enabled = false;
            this.btnSend.Location = new System.Drawing.Point(14, 394);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(648, 23);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.BtnSend_Click);
            // 
            // TbMsg
            // 
            this.TbMsg.Location = new System.Drawing.Point(14, 368);
            this.TbMsg.Name = "TbMsg";
            this.TbMsg.Size = new System.Drawing.Size(648, 20);
            this.TbMsg.TabIndex = 0;
            this.TbMsg.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TbMsg_KeyDown);
            // 
            // SettingsPage
            // 
            this.SettingsPage.Location = new System.Drawing.Point(4, 22);
            this.SettingsPage.Name = "SettingsPage";
            this.SettingsPage.Padding = new System.Windows.Forms.Padding(3);
            this.SettingsPage.Size = new System.Drawing.Size(796, 425);
            this.SettingsPage.TabIndex = 1;
            this.SettingsPage.Text = "Settings";
            this.SettingsPage.UseVisualStyleBackColor = true;
            // 
            // frmChatty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 451);
            this.Controls.Add(this.tabControl);
            this.Name = "frmChatty";
            this.Text = "Chatty";
            this.gbClientMessages.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.HomePage.ResumeLayout(false);
            this.HomePage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbClientMessages;
        private System.Windows.Forms.RichTextBox rtbClientMessages;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage HomePage;
        private System.Windows.Forms.Label lbUsername;
        private System.Windows.Forms.CheckedListBox clbActiveClients;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox TbMsg;
        private System.Windows.Forms.TabPage SettingsPage;
    }
}

