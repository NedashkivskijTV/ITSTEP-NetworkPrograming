namespace ServerTcp
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
            this.tbServerTcpStatistics = new System.Windows.Forms.TextBox();
            this.btnStartServerTCP = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbServerTcpStatistics
            // 
            this.tbServerTcpStatistics.Location = new System.Drawing.Point(12, 12);
            this.tbServerTcpStatistics.Multiline = true;
            this.tbServerTcpStatistics.Name = "tbServerTcpStatistics";
            this.tbServerTcpStatistics.PlaceholderText = "ServerTCP statistics";
            this.tbServerTcpStatistics.Size = new System.Drawing.Size(281, 338);
            this.tbServerTcpStatistics.TabIndex = 0;
            // 
            // btnStartServerTCP
            // 
            this.btnStartServerTCP.Location = new System.Drawing.Point(12, 356);
            this.btnStartServerTCP.Name = "btnStartServerTCP";
            this.btnStartServerTCP.Size = new System.Drawing.Size(281, 23);
            this.btnStartServerTCP.TabIndex = 1;
            this.btnStartServerTCP.Text = "Start ServerTCP";
            this.btnStartServerTCP.UseVisualStyleBackColor = true;
            this.btnStartServerTCP.Click += new System.EventHandler(this.btnStartServerTCP_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 386);
            this.Controls.Add(this.btnStartServerTCP);
            this.Controls.Add(this.tbServerTcpStatistics);
            this.Name = "Form1";
            this.Text = "ServerTCP";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox tbServerTcpStatistics;
        private Button btnStartServerTCP;
    }
}