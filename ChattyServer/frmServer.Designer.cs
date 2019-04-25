namespace ChattyServer
{
    partial class frmServer
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmServer));
            this.btnListen = new System.Windows.Forms.Button();
            this.SettingsPage = new System.Windows.Forms.TabPage();
            this.btnStopListening = new System.Windows.Forms.Button();
            this.gbConnectedClients = new System.Windows.Forms.GroupBox();
            this.clbConnectedClients = new System.Windows.Forms.CheckedListBox();
            this.StatusPage = new System.Windows.Forms.TabPage();
            this.gbServerMessages = new System.Windows.Forms.GroupBox();
            this.rtbServerMessages = new System.Windows.Forms.RichTextBox();
            this.cbSendToAll = new System.Windows.Forms.CheckBox();
            this.nudSpamAmount = new System.Windows.Forms.NumericUpDown();
            this.btnSpam = new System.Windows.Forms.Button();
            this.tbMsg = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.TsslReceivedMessages = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslServerStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.TsslConnectedClients = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.niMinimizedNotification = new System.Windows.Forms.NotifyIcon(this.components);
            this.SettingsPage.SuspendLayout();
            this.gbConnectedClients.SuspendLayout();
            this.StatusPage.SuspendLayout();
            this.gbServerMessages.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSpamAmount)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnListen
            // 
            this.btnListen.Location = new System.Drawing.Point(6, 6);
            this.btnListen.Name = "btnListen";
            this.btnListen.Size = new System.Drawing.Size(95, 23);
            this.btnListen.TabIndex = 0;
            this.btnListen.Text = "Start Server";
            this.btnListen.UseVisualStyleBackColor = true;
            this.btnListen.Click += new System.EventHandler(this.btnListen_Click);
            // 
            // SettingsPage
            // 
            this.SettingsPage.BackColor = System.Drawing.SystemColors.Window;
            this.SettingsPage.Controls.Add(this.btnStopListening);
            this.SettingsPage.Controls.Add(this.btnListen);
            this.SettingsPage.Location = new System.Drawing.Point(4, 23);
            this.SettingsPage.Name = "SettingsPage";
            this.SettingsPage.Padding = new System.Windows.Forms.Padding(3);
            this.SettingsPage.Size = new System.Drawing.Size(796, 534);
            this.SettingsPage.TabIndex = 1;
            this.SettingsPage.Text = "Settings";
            // 
            // btnStopListening
            // 
            this.btnStopListening.Location = new System.Drawing.Point(6, 35);
            this.btnStopListening.Name = "btnStopListening";
            this.btnStopListening.Size = new System.Drawing.Size(95, 23);
            this.btnStopListening.TabIndex = 1;
            this.btnStopListening.Text = "Stop Server";
            this.btnStopListening.UseVisualStyleBackColor = true;
            this.btnStopListening.Click += new System.EventHandler(this.btnStopListening_Click);
            // 
            // gbConnectedClients
            // 
            this.gbConnectedClients.Controls.Add(this.clbConnectedClients);
            this.gbConnectedClients.Location = new System.Drawing.Point(600, 6);
            this.gbConnectedClients.Name = "gbConnectedClients";
            this.gbConnectedClients.Size = new System.Drawing.Size(193, 447);
            this.gbConnectedClients.TabIndex = 14;
            this.gbConnectedClients.TabStop = false;
            this.gbConnectedClients.Text = "Connected Clients";
            // 
            // clbConnectedClients
            // 
            this.clbConnectedClients.CheckOnClick = true;
            this.clbConnectedClients.Font = new System.Drawing.Font("Rubik", 9.75F, System.Drawing.FontStyle.Bold);
            this.clbConnectedClients.ForeColor = System.Drawing.Color.DarkSeaGreen;
            this.clbConnectedClients.FormattingEnabled = true;
            this.clbConnectedClients.Location = new System.Drawing.Point(6, 19);
            this.clbConnectedClients.Name = "clbConnectedClients";
            this.clbConnectedClients.Size = new System.Drawing.Size(181, 418);
            this.clbConnectedClients.TabIndex = 4;
            // 
            // StatusPage
            // 
            this.StatusPage.BackColor = System.Drawing.SystemColors.Window;
            this.StatusPage.Controls.Add(this.gbConnectedClients);
            this.StatusPage.Controls.Add(this.gbServerMessages);
            this.StatusPage.Controls.Add(this.cbSendToAll);
            this.StatusPage.Controls.Add(this.nudSpamAmount);
            this.StatusPage.Controls.Add(this.btnSpam);
            this.StatusPage.Controls.Add(this.tbMsg);
            this.StatusPage.Controls.Add(this.btnSend);
            this.StatusPage.ForeColor = System.Drawing.SystemColors.Menu;
            this.StatusPage.Location = new System.Drawing.Point(4, 23);
            this.StatusPage.Name = "StatusPage";
            this.StatusPage.Padding = new System.Windows.Forms.Padding(3);
            this.StatusPage.Size = new System.Drawing.Size(796, 534);
            this.StatusPage.TabIndex = 0;
            this.StatusPage.Text = "Status";
            // 
            // gbServerMessages
            // 
            this.gbServerMessages.Controls.Add(this.rtbServerMessages);
            this.gbServerMessages.Location = new System.Drawing.Point(6, 6);
            this.gbServerMessages.Name = "gbServerMessages";
            this.gbServerMessages.Size = new System.Drawing.Size(588, 439);
            this.gbServerMessages.TabIndex = 13;
            this.gbServerMessages.TabStop = false;
            this.gbServerMessages.Text = "Server Console";
            // 
            // rtbServerMessages
            // 
            this.rtbServerMessages.Font = new System.Drawing.Font("Rubik", 9.75F, System.Drawing.FontStyle.Bold);
            this.rtbServerMessages.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.rtbServerMessages.Location = new System.Drawing.Point(6, 19);
            this.rtbServerMessages.Name = "rtbServerMessages";
            this.rtbServerMessages.ReadOnly = true;
            this.rtbServerMessages.Size = new System.Drawing.Size(576, 414);
            this.rtbServerMessages.TabIndex = 12;
            this.rtbServerMessages.Text = "";
            this.rtbServerMessages.TextChanged += new System.EventHandler(this.rtbServerMessages_TextChanged);
            // 
            // cbSendToAll
            // 
            this.cbSendToAll.AutoSize = true;
            this.cbSendToAll.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cbSendToAll.Location = new System.Drawing.Point(6, 460);
            this.cbSendToAll.Name = "cbSendToAll";
            this.cbSendToAll.Size = new System.Drawing.Size(83, 18);
            this.cbSendToAll.TabIndex = 3;
            this.cbSendToAll.Text = "Send To All";
            this.cbSendToAll.UseVisualStyleBackColor = true;
            // 
            // nudSpamAmount
            // 
            this.nudSpamAmount.Font = new System.Drawing.Font("KacstBook", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.nudSpamAmount.Location = new System.Drawing.Point(600, 459);
            this.nudSpamAmount.Name = "nudSpamAmount";
            this.nudSpamAmount.Size = new System.Drawing.Size(67, 24);
            this.nudSpamAmount.TabIndex = 5;
            // 
            // btnSpam
            // 
            this.btnSpam.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.btnSpam.Location = new System.Drawing.Point(673, 458);
            this.btnSpam.Name = "btnSpam";
            this.btnSpam.Size = new System.Drawing.Size(114, 23);
            this.btnSpam.TabIndex = 6;
            this.btnSpam.Text = "Spam";
            this.btnSpam.UseVisualStyleBackColor = true;
            this.btnSpam.Click += new System.EventHandler(this.btnSpam_Click);
            // 
            // tbMsg
            // 
            this.tbMsg.Location = new System.Drawing.Point(12, 487);
            this.tbMsg.Name = "tbMsg";
            this.tbMsg.Size = new System.Drawing.Size(775, 22);
            this.tbMsg.TabIndex = 0;
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.btnSend.Enabled = false;
            this.btnSend.Font = new System.Drawing.Font("Gadugi", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.ForeColor = System.Drawing.SystemColors.InfoText;
            this.btnSend.Location = new System.Drawing.Point(94, 458);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(500, 23);
            this.btnSend.TabIndex = 2;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // TsslReceivedMessages
            // 
            this.TsslReceivedMessages.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TsslReceivedMessages.Name = "TsslReceivedMessages";
            this.TsslReceivedMessages.Size = new System.Drawing.Size(160, 20);
            this.TsslReceivedMessages.Text = "Received Messages: 0";
            // 
            // tsslServerStatus
            // 
            this.tsslServerStatus.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsslServerStatus.Name = "tsslServerStatus";
            this.tsslServerStatus.Size = new System.Drawing.Size(79, 20);
            this.tsslServerStatus.Text = "State: Idle";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslServerStatus,
            this.TsslConnectedClients,
            this.TsslReceivedMessages});
            this.statusStrip1.Location = new System.Drawing.Point(0, 536);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(804, 25);
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // TsslConnectedClients
            // 
            this.TsslConnectedClients.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            this.TsslConnectedClients.Name = "TsslConnectedClients";
            this.TsslConnectedClients.Size = new System.Drawing.Size(151, 20);
            this.TsslConnectedClients.Text = "Connected Clients: 0";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.StatusPage);
            this.tabControl1.Controls.Add(this.SettingsPage);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Gadugi", 8.25F);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(804, 561);
            this.tabControl1.TabIndex = 6;
            // 
            // niMinimizedNotification
            // 
            this.niMinimizedNotification.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.niMinimizedNotification.BalloonTipText = "Chatty Server will be running in the background. :D";
            this.niMinimizedNotification.BalloonTipTitle = "FYI";
            this.niMinimizedNotification.Visible = true;
            // 
            // frmServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 561);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmServer";
            this.Text = "Server";
            this.SettingsPage.ResumeLayout(false);
            this.gbConnectedClients.ResumeLayout(false);
            this.StatusPage.ResumeLayout(false);
            this.StatusPage.PerformLayout();
            this.gbServerMessages.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudSpamAmount)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnListen;
        private System.Windows.Forms.TabPage SettingsPage;
        private System.Windows.Forms.Button btnStopListening;
        private System.Windows.Forms.GroupBox gbConnectedClients;
        private System.Windows.Forms.CheckedListBox clbConnectedClients;
        private System.Windows.Forms.TabPage StatusPage;
        private System.Windows.Forms.GroupBox gbServerMessages;
        private System.Windows.Forms.RichTextBox rtbServerMessages;
        private System.Windows.Forms.CheckBox cbSendToAll;
        private System.Windows.Forms.NumericUpDown nudSpamAmount;
        private System.Windows.Forms.Button btnSpam;
        private System.Windows.Forms.TextBox tbMsg;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.ToolStripStatusLabel TsslReceivedMessages;
        private System.Windows.Forms.ToolStripStatusLabel tsslServerStatus;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.NotifyIcon niMinimizedNotification;
        private System.Windows.Forms.ToolStripStatusLabel TsslConnectedClients;
    }
}

