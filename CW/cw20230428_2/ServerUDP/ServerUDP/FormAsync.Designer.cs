namespace ServerUDP
{
    partial class FormAsync
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
            this.tbServerInfoAsync = new System.Windows.Forms.TextBox();
            this.tbClientsStringAsync = new System.Windows.Forms.TextBox();
            this.btnStartServerAsync = new System.Windows.Forms.Button();
            this.btnSendUDPAsync = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbServerInfoAsync
            // 
            this.tbServerInfoAsync.Location = new System.Drawing.Point(12, 12);
            this.tbServerInfoAsync.Multiline = true;
            this.tbServerInfoAsync.Name = "tbServerInfoAsync";
            this.tbServerInfoAsync.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbServerInfoAsync.Size = new System.Drawing.Size(336, 285);
            this.tbServerInfoAsync.TabIndex = 0;
            // 
            // tbClientsStringAsync
            // 
            this.tbClientsStringAsync.Location = new System.Drawing.Point(12, 347);
            this.tbClientsStringAsync.Name = "tbClientsStringAsync";
            this.tbClientsStringAsync.PlaceholderText = "Enter yuor query";
            this.tbClientsStringAsync.Size = new System.Drawing.Size(336, 23);
            this.tbClientsStringAsync.TabIndex = 1;
            // 
            // btnStartServerAsync
            // 
            this.btnStartServerAsync.Location = new System.Drawing.Point(375, 12);
            this.btnStartServerAsync.Name = "btnStartServerAsync";
            this.btnStartServerAsync.Size = new System.Drawing.Size(124, 23);
            this.btnStartServerAsync.TabIndex = 2;
            this.btnStartServerAsync.Text = "Start UDP Async";
            this.btnStartServerAsync.UseVisualStyleBackColor = true;
            this.btnStartServerAsync.Click += new System.EventHandler(this.btnStartServerAsync_Click);
            // 
            // btnSendUDPAsync
            // 
            this.btnSendUDPAsync.Location = new System.Drawing.Point(375, 346);
            this.btnSendUDPAsync.Name = "btnSendUDPAsync";
            this.btnSendUDPAsync.Size = new System.Drawing.Size(124, 23);
            this.btnSendUDPAsync.TabIndex = 3;
            this.btnSendUDPAsync.Text = "Send UDP Async";
            this.btnSendUDPAsync.UseVisualStyleBackColor = true;
            this.btnSendUDPAsync.Click += new System.EventHandler(this.btnSendUDPAsync_Click);
            // 
            // FormAsync
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 450);
            this.Controls.Add(this.btnSendUDPAsync);
            this.Controls.Add(this.btnStartServerAsync);
            this.Controls.Add(this.tbClientsStringAsync);
            this.Controls.Add(this.tbServerInfoAsync);
            this.Name = "FormAsync";
            this.Text = "FormAsync";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox tbServerInfoAsync;
        private TextBox tbClientsStringAsync;
        private Button btnStartServerAsync;
        private Button btnSendUDPAsync;
    }
}