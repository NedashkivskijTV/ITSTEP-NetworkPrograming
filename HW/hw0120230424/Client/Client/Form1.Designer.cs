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
            this.tbServerMessage = new System.Windows.Forms.TextBox();
            this.tbClientMessage = new System.Windows.Forms.TextBox();
            this.btnSendMessage = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbServerMessage
            // 
            this.tbServerMessage.Location = new System.Drawing.Point(12, 12);
            this.tbServerMessage.Multiline = true;
            this.tbServerMessage.Name = "tbServerMessage";
            this.tbServerMessage.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbServerMessage.Size = new System.Drawing.Size(553, 221);
            this.tbServerMessage.TabIndex = 0;
            // 
            // tbClientMessage
            // 
            this.tbClientMessage.Location = new System.Drawing.Point(12, 239);
            this.tbClientMessage.Name = "tbClientMessage";
            this.tbClientMessage.PlaceholderText = "Enter message";
            this.tbClientMessage.Size = new System.Drawing.Size(553, 23);
            this.tbClientMessage.TabIndex = 1;
            this.tbClientMessage.TextChanged += new System.EventHandler(this.tbClientMessage_TextChanged);
            // 
            // btnSendMessage
            // 
            this.btnSendMessage.Location = new System.Drawing.Point(12, 291);
            this.btnSendMessage.Name = "btnSendMessage";
            this.btnSendMessage.Size = new System.Drawing.Size(553, 23);
            this.btnSendMessage.TabIndex = 2;
            this.btnSendMessage.Text = "Send message to the Server";
            this.btnSendMessage.UseVisualStyleBackColor = true;
            this.btnSendMessage.Click += new System.EventHandler(this.btnSendMessage_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 326);
            this.Controls.Add(this.btnSendMessage);
            this.Controls.Add(this.tbClientMessage);
            this.Controls.Add(this.tbServerMessage);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox tbServerMessage;
        private TextBox tbClientMessage;
        private Button btnSendMessage;
    }
}