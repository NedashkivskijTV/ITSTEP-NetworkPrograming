namespace ClientScreenCapture
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
            this.btnSendScreen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSendScreen
            // 
            this.btnSendScreen.Location = new System.Drawing.Point(12, 12);
            this.btnSendScreen.Name = "btnSendScreen";
            this.btnSendScreen.Size = new System.Drawing.Size(203, 23);
            this.btnSendScreen.TabIndex = 0;
            this.btnSendScreen.Text = "Send Screen";
            this.btnSendScreen.UseVisualStyleBackColor = true;
            this.btnSendScreen.Click += new System.EventHandler(this.btnSendScreen_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(227, 52);
            this.Controls.Add(this.btnSendScreen);
            this.Name = "Form1";
            this.Text = "Client";
            this.ResumeLayout(false);

        }

        #endregion

        private Button btnSendScreen;
    }
}