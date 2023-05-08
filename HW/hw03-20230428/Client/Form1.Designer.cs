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
            this.tbClientText = new System.Windows.Forms.TextBox();
            this.btnGetRandomString = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbClientText
            // 
            this.tbClientText.Location = new System.Drawing.Point(12, 12);
            this.tbClientText.Multiline = true;
            this.tbClientText.Name = "tbClientText";
            this.tbClientText.PlaceholderText = "Client text";
            this.tbClientText.Size = new System.Drawing.Size(322, 110);
            this.tbClientText.TabIndex = 0;
            // 
            // btnGetRandomString
            // 
            this.btnGetRandomString.Location = new System.Drawing.Point(12, 141);
            this.btnGetRandomString.Name = "btnGetRandomString";
            this.btnGetRandomString.Size = new System.Drawing.Size(322, 23);
            this.btnGetRandomString.TabIndex = 1;
            this.btnGetRandomString.Text = "Get a random string";
            this.btnGetRandomString.UseVisualStyleBackColor = true;
            this.btnGetRandomString.Click += new System.EventHandler(this.btnGetRandomString_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 176);
            this.Controls.Add(this.btnGetRandomString);
            this.Controls.Add(this.tbClientText);
            this.Name = "Form1";
            this.Text = "Client";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox tbClientText;
        private Button btnGetRandomString;
    }
}