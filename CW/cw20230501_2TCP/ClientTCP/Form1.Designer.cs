namespace ClientTCP
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
            this.tbClientsTText = new System.Windows.Forms.TextBox();
            this.btnSendTcpClient = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbClientsTText
            // 
            this.tbClientsTText.Location = new System.Drawing.Point(12, 12);
            this.tbClientsTText.Name = "tbClientsTText";
            this.tbClientsTText.PlaceholderText = "Enter text";
            this.tbClientsTText.Size = new System.Drawing.Size(254, 23);
            this.tbClientsTText.TabIndex = 0;
            // 
            // btnSendTcpClient
            // 
            this.btnSendTcpClient.Location = new System.Drawing.Point(12, 51);
            this.btnSendTcpClient.Name = "btnSendTcpClient";
            this.btnSendTcpClient.Size = new System.Drawing.Size(254, 23);
            this.btnSendTcpClient.TabIndex = 1;
            this.btnSendTcpClient.Text = "Send TCP Client";
            this.btnSendTcpClient.UseVisualStyleBackColor = true;
            this.btnSendTcpClient.Click += new System.EventHandler(this.btnSendTcpClient_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 95);
            this.Controls.Add(this.btnSendTcpClient);
            this.Controls.Add(this.tbClientsTText);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox tbClientsTText;
        private Button btnSendTcpClient;
    }
}