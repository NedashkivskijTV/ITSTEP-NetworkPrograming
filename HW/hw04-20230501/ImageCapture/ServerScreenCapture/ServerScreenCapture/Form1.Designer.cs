namespace ServerScreenCapture
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbServerStatistics = new System.Windows.Forms.TextBox();
            this.btnStartTcpServer = new System.Windows.Forms.Button();
            this.pbClientsScreen = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbClientsScreen)).BeginInit();
            this.SuspendLayout();
            // 
            // tbServerStatistics
            // 
            this.tbServerStatistics.Location = new System.Drawing.Point(12, 12);
            this.tbServerStatistics.Multiline = true;
            this.tbServerStatistics.Name = "tbServerStatistics";
            this.tbServerStatistics.PlaceholderText = "Server Statistics";
            this.tbServerStatistics.Size = new System.Drawing.Size(246, 318);
            this.tbServerStatistics.TabIndex = 0;
            // 
            // btnStartTcpServer
            // 
            this.btnStartTcpServer.Location = new System.Drawing.Point(12, 350);
            this.btnStartTcpServer.Name = "btnStartTcpServer";
            this.btnStartTcpServer.Size = new System.Drawing.Size(246, 23);
            this.btnStartTcpServer.TabIndex = 1;
            this.btnStartTcpServer.Text = "Start TCP Server";
            this.btnStartTcpServer.UseVisualStyleBackColor = true;
            this.btnStartTcpServer.Click += new System.EventHandler(this.btnStartTcpServer_Click);
            // 
            // pbClientsScreen
            // 
            this.pbClientsScreen.Location = new System.Drawing.Point(264, 12);
            this.pbClientsScreen.Name = "pbClientsScreen";
            this.pbClientsScreen.Size = new System.Drawing.Size(524, 318);
            this.pbClientsScreen.TabIndex = 2;
            this.pbClientsScreen.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 389);
            this.Controls.Add(this.pbClientsScreen);
            this.Controls.Add(this.btnStartTcpServer);
            this.Controls.Add(this.tbServerStatistics);
            this.Name = "Form1";
            this.Text = "Server";
            ((System.ComponentModel.ISupportInitialize)(this.pbClientsScreen)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox tbServerStatistics;
        private Button btnStartTcpServer;
        private PictureBox pbClientsScreen;
    }
}