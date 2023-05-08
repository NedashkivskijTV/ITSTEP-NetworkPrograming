namespace ServerUDP
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
            this.tbServerInf = new System.Windows.Forms.TextBox();
            this.tbClientsString = new System.Windows.Forms.TextBox();
            this.btnStartUDP = new System.Windows.Forms.Button();
            this.btnSendUDP = new System.Windows.Forms.Button();
            this.btnRunFornAsync = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbServerInf
            // 
            this.tbServerInf.Location = new System.Drawing.Point(12, 12);
            this.tbServerInf.Multiline = true;
            this.tbServerInf.Name = "tbServerInf";
            this.tbServerInf.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbServerInf.Size = new System.Drawing.Size(441, 287);
            this.tbServerInf.TabIndex = 0;
            // 
            // tbClientsString
            // 
            this.tbClientsString.Location = new System.Drawing.Point(12, 358);
            this.tbClientsString.Name = "tbClientsString";
            this.tbClientsString.PlaceholderText = "Enter query";
            this.tbClientsString.Size = new System.Drawing.Size(441, 23);
            this.tbClientsString.TabIndex = 1;
            // 
            // btnStartUDP
            // 
            this.btnStartUDP.Location = new System.Drawing.Point(494, 12);
            this.btnStartUDP.Name = "btnStartUDP";
            this.btnStartUDP.Size = new System.Drawing.Size(75, 23);
            this.btnStartUDP.TabIndex = 2;
            this.btnStartUDP.Text = "Start UDP";
            this.btnStartUDP.UseVisualStyleBackColor = true;
            this.btnStartUDP.Click += new System.EventHandler(this.btnStartUDP_Click);
            // 
            // btnSendUDP
            // 
            this.btnSendUDP.Location = new System.Drawing.Point(494, 357);
            this.btnSendUDP.Name = "btnSendUDP";
            this.btnSendUDP.Size = new System.Drawing.Size(75, 23);
            this.btnSendUDP.TabIndex = 3;
            this.btnSendUDP.Text = "Send UDP";
            this.btnSendUDP.UseVisualStyleBackColor = true;
            this.btnSendUDP.Click += new System.EventHandler(this.btnSendUDP_Click);
            // 
            // btnRunFornAsync
            // 
            this.btnRunFornAsync.Location = new System.Drawing.Point(12, 407);
            this.btnRunFornAsync.Name = "btnRunFornAsync";
            this.btnRunFornAsync.Size = new System.Drawing.Size(112, 23);
            this.btnRunFornAsync.TabIndex = 4;
            this.btnRunFornAsync.Text = "Run Form Async";
            this.btnRunFornAsync.UseVisualStyleBackColor = true;
            this.btnRunFornAsync.Click += new System.EventHandler(this.btnRunFornAsync_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(581, 442);
            this.Controls.Add(this.btnRunFornAsync);
            this.Controls.Add(this.btnSendUDP);
            this.Controls.Add(this.btnStartUDP);
            this.Controls.Add(this.tbClientsString);
            this.Controls.Add(this.tbServerInf);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox tbServerInf;
        private TextBox tbClientsString;
        private Button btnStartUDP;
        private Button btnSendUDP;
        private Button btnRunFornAsync;
    }
}