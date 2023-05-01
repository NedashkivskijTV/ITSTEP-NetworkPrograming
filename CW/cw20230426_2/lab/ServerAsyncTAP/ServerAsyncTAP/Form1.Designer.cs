namespace ServerAsyncTAP
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
            this.btnStartServer = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnSendDataJson = new System.Windows.Forms.Button();
            this.btnCreateJsonFile = new System.Windows.Forms.Button();
            this.btnShowJsonFile = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // tbServerInf
            // 
            this.tbServerInf.Location = new System.Drawing.Point(12, 12);
            this.tbServerInf.Multiline = true;
            this.tbServerInf.Name = "tbServerInf";
            this.tbServerInf.Size = new System.Drawing.Size(334, 282);
            this.tbServerInf.TabIndex = 0;
            // 
            // btnStartServer
            // 
            this.btnStartServer.Location = new System.Drawing.Point(12, 322);
            this.btnStartServer.Name = "btnStartServer";
            this.btnStartServer.Size = new System.Drawing.Size(127, 23);
            this.btnStartServer.TabIndex = 1;
            this.btnStartServer.Text = "Start Server (Task)";
            this.btnStartServer.UseVisualStyleBackColor = true;
            this.btnStartServer.Click += new System.EventHandler(this.btnStartServer_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(352, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(354, 282);
            this.dataGridView1.TabIndex = 2;
            // 
            // btnSendDataJson
            // 
            this.btnSendDataJson.Location = new System.Drawing.Point(386, 322);
            this.btnSendDataJson.Name = "btnSendDataJson";
            this.btnSendDataJson.Size = new System.Drawing.Size(113, 23);
            this.btnSendDataJson.TabIndex = 3;
            this.btnSendDataJson.Text = "Send Products";
            this.btnSendDataJson.UseVisualStyleBackColor = true;
            this.btnSendDataJson.Click += new System.EventHandler(this.btnSendDataJson_Click);
            // 
            // btnCreateJsonFile
            // 
            this.btnCreateJsonFile.Location = new System.Drawing.Point(505, 322);
            this.btnCreateJsonFile.Name = "btnCreateJsonFile";
            this.btnCreateJsonFile.Size = new System.Drawing.Size(95, 23);
            this.btnCreateJsonFile.TabIndex = 4;
            this.btnCreateJsonFile.Text = "create json file";
            this.btnCreateJsonFile.UseVisualStyleBackColor = true;
            this.btnCreateJsonFile.Click += new System.EventHandler(this.btnCreateJsonFile_Click);
            // 
            // btnShowJsonFile
            // 
            this.btnShowJsonFile.Location = new System.Drawing.Point(606, 322);
            this.btnShowJsonFile.Name = "btnShowJsonFile";
            this.btnShowJsonFile.Size = new System.Drawing.Size(100, 23);
            this.btnShowJsonFile.TabIndex = 5;
            this.btnShowJsonFile.Text = "show json file";
            this.btnShowJsonFile.UseVisualStyleBackColor = true;
            this.btnShowJsonFile.Click += new System.EventHandler(this.btnShowJsonFile_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(718, 383);
            this.Controls.Add(this.btnShowJsonFile);
            this.Controls.Add(this.btnCreateJsonFile);
            this.Controls.Add(this.btnSendDataJson);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnStartServer);
            this.Controls.Add(this.tbServerInf);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox tbServerInf;
        private Button btnStartServer;
        private DataGridView dataGridView1;
        private Button btnSendDataJson;
        private Button btnCreateJsonFile;
        private Button btnShowJsonFile;
    }
}