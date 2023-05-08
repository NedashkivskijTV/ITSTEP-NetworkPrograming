namespace Client
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
            this.tbClientStatistics = new System.Windows.Forms.TextBox();
            this.btnSendCientsText = new System.Windows.Forms.Button();
            this.tbClientText = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbClientStatistics
            // 
            this.tbClientStatistics.Location = new System.Drawing.Point(12, 12);
            this.tbClientStatistics.Multiline = true;
            this.tbClientStatistics.Name = "tbClientStatistics";
            this.tbClientStatistics.PlaceholderText = "Clients statistics";
            this.tbClientStatistics.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbClientStatistics.Size = new System.Drawing.Size(290, 332);
            this.tbClientStatistics.TabIndex = 0;
            // 
            // btnSendCientsText
            // 
            this.btnSendCientsText.Location = new System.Drawing.Point(12, 379);
            this.btnSendCientsText.Name = "btnSendCientsText";
            this.btnSendCientsText.Size = new System.Drawing.Size(290, 23);
            this.btnSendCientsText.TabIndex = 1;
            this.btnSendCientsText.Text = "Send Clients Text";
            this.btnSendCientsText.UseVisualStyleBackColor = true;
            this.btnSendCientsText.Click += new System.EventHandler(this.btnSendCientsText_Click);
            // 
            // tbClientText
            // 
            this.tbClientText.Location = new System.Drawing.Point(12, 350);
            this.tbClientText.Name = "tbClientText";
            this.tbClientText.PlaceholderText = "Enter client text";
            this.tbClientText.Size = new System.Drawing.Size(290, 23);
            this.tbClientText.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(313, 410);
            this.Controls.Add(this.tbClientText);
            this.Controls.Add(this.btnSendCientsText);
            this.Controls.Add(this.tbClientStatistics);
            this.Name = "Form1";
            this.Text = "Client TCP";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox tbClientStatistics;
        private Button btnSendCientsText;
        private TextBox tbClientText;
    }
}