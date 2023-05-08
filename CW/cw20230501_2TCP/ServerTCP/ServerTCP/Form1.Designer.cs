namespace ServerTCP
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
            this.tbServerTCPStatistic = new System.Windows.Forms.TextBox();
            this.btnStartTCPServer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbServerTCPStatistic
            // 
            this.tbServerTCPStatistic.Location = new System.Drawing.Point(12, 12);
            this.tbServerTCPStatistic.Multiline = true;
            this.tbServerTCPStatistic.Name = "tbServerTCPStatistic";
            this.tbServerTCPStatistic.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbServerTCPStatistic.Size = new System.Drawing.Size(305, 378);
            this.tbServerTCPStatistic.TabIndex = 0;
            // 
            // btnStartTCPServer
            // 
            this.btnStartTCPServer.Location = new System.Drawing.Point(113, 415);
            this.btnStartTCPServer.Name = "btnStartTCPServer";
            this.btnStartTCPServer.Size = new System.Drawing.Size(100, 23);
            this.btnStartTCPServer.TabIndex = 1;
            this.btnStartTCPServer.Text = "Start TCP Server";
            this.btnStartTCPServer.UseVisualStyleBackColor = true;
            this.btnStartTCPServer.Click += new System.EventHandler(this.btnStartTCPServer_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(327, 450);
            this.Controls.Add(this.btnStartTCPServer);
            this.Controls.Add(this.tbServerTCPStatistic);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox tbServerTCPStatistic;
        private Button btnStartTCPServer;
    }
}