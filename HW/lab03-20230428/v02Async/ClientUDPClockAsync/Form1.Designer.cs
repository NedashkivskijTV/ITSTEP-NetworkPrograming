namespace ClientUDPClockAsync
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
            this.lbNetworkTime = new System.Windows.Forms.Label();
            this.tbClientQuery = new System.Windows.Forms.TextBox();
            this.btnGetTime = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbNetworkTime
            // 
            this.lbNetworkTime.AutoSize = true;
            this.lbNetworkTime.Font = new System.Drawing.Font("Segoe UI", 39F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lbNetworkTime.Location = new System.Drawing.Point(50, 9);
            this.lbNetworkTime.Name = "lbNetworkTime";
            this.lbNetworkTime.Size = new System.Drawing.Size(184, 70);
            this.lbNetworkTime.TabIndex = 0;
            this.lbNetworkTime.Text = "--:--:--";
            // 
            // tbClientQuery
            // 
            this.tbClientQuery.Location = new System.Drawing.Point(12, 108);
            this.tbClientQuery.Multiline = true;
            this.tbClientQuery.Name = "tbClientQuery";
            this.tbClientQuery.Size = new System.Drawing.Size(258, 103);
            this.tbClientQuery.TabIndex = 1;
            // 
            // btnGetTime
            // 
            this.btnGetTime.Location = new System.Drawing.Point(104, 217);
            this.btnGetTime.Name = "btnGetTime";
            this.btnGetTime.Size = new System.Drawing.Size(75, 23);
            this.btnGetTime.TabIndex = 2;
            this.btnGetTime.Text = "Get Time";
            this.btnGetTime.UseVisualStyleBackColor = true;
            this.btnGetTime.Click += new System.EventHandler(this.btnGetTime_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(282, 254);
            this.Controls.Add(this.btnGetTime);
            this.Controls.Add(this.tbClientQuery);
            this.Controls.Add(this.lbNetworkTime);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lbNetworkTime;
        private TextBox tbClientQuery;
        private Button btnGetTime;
    }
}