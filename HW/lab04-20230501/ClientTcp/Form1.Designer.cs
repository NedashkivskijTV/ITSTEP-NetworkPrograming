namespace ClientTcp
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
            this.tbClientsText = new System.Windows.Forms.TextBox();
            this.btnSendTCPText = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbClientsText
            // 
            this.tbClientsText.Location = new System.Drawing.Point(12, 12);
            this.tbClientsText.Name = "tbClientsText";
            this.tbClientsText.PlaceholderText = "Enter text";
            this.tbClientsText.Size = new System.Drawing.Size(267, 23);
            this.tbClientsText.TabIndex = 0;
            // 
            // btnSendTCPText
            // 
            this.btnSendTCPText.Location = new System.Drawing.Point(12, 53);
            this.btnSendTCPText.Name = "btnSendTCPText";
            this.btnSendTCPText.Size = new System.Drawing.Size(267, 23);
            this.btnSendTCPText.TabIndex = 1;
            this.btnSendTCPText.Text = "Send Text";
            this.btnSendTCPText.UseVisualStyleBackColor = true;
            this.btnSendTCPText.Click += new System.EventHandler(this.btnSendTCPText_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 89);
            this.Controls.Add(this.btnSendTCPText);
            this.Controls.Add(this.tbClientsText);
            this.Name = "Form1";
            this.Text = "ClientTCP";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox tbClientsText;
        private Button btnSendTCPText;
    }
}