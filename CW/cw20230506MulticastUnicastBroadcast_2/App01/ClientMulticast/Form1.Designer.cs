namespace ClientMulticast
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
            this.tbClientMulticast = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbClientMulticast
            // 
            this.tbClientMulticast.Location = new System.Drawing.Point(12, 12);
            this.tbClientMulticast.Multiline = true;
            this.tbClientMulticast.Name = "tbClientMulticast";
            this.tbClientMulticast.ReadOnly = true;
            this.tbClientMulticast.Size = new System.Drawing.Size(273, 426);
            this.tbClientMulticast.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 450);
            this.Controls.Add(this.tbClientMulticast);
            this.Name = "Form1";
            this.Text = "ClientMulticast";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox tbClientMulticast;
    }
}