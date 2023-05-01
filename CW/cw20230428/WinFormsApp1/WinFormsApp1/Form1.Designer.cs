namespace WinFormsApp1
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btnIpClient = new System.Windows.Forms.Button();
            this.btnOpenFormAsync = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(477, 204);
            this.textBox1.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(21, 281);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(468, 23);
            this.textBox2.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(512, 40);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Start UDP";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(512, 281);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Send UDP";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnIpClient
            // 
            this.btnIpClient.Location = new System.Drawing.Point(516, 144);
            this.btnIpClient.Name = "btnIpClient";
            this.btnIpClient.Size = new System.Drawing.Size(75, 23);
            this.btnIpClient.TabIndex = 4;
            this.btnIpClient.Text = "IPClient";
            this.btnIpClient.UseVisualStyleBackColor = true;
            this.btnIpClient.Click += new System.EventHandler(this.btnIpClient_Click);
            // 
            // btnOpenFormAsync
            // 
            this.btnOpenFormAsync.Location = new System.Drawing.Point(231, 341);
            this.btnOpenFormAsync.Name = "btnOpenFormAsync";
            this.btnOpenFormAsync.Size = new System.Drawing.Size(132, 23);
            this.btnOpenFormAsync.TabIndex = 5;
            this.btnOpenFormAsync.Text = "Open FormAsync";
            this.btnOpenFormAsync.UseVisualStyleBackColor = true;
            this.btnOpenFormAsync.Click += new System.EventHandler(this.btnOpenFormAsync_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 376);
            this.Controls.Add(this.btnOpenFormAsync);
            this.Controls.Add(this.btnIpClient);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox textBox1;
        private TextBox textBox2;
        private Button button1;
        private Button button2;
        private Button btnIpClient;
        private Button btnOpenFormAsync;
    }
}