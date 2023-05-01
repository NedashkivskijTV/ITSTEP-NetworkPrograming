namespace ServerProducts
{
    partial class FormShowProducts
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvShowProducts = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvShowProducts)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvShowProducts
            // 
            this.dgvShowProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvShowProducts.Location = new System.Drawing.Point(12, 12);
            this.dgvShowProducts.Name = "dgvShowProducts";
            this.dgvShowProducts.RowTemplate.Height = 25;
            this.dgvShowProducts.Size = new System.Drawing.Size(402, 325);
            this.dgvShowProducts.TabIndex = 0;
            // 
            // FormShowProducts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(426, 349);
            this.Controls.Add(this.dgvShowProducts);
            this.Name = "FormShowProducts";
            this.Text = "FormShowProducts";
            ((System.ComponentModel.ISupportInitialize)(this.dgvShowProducts)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DataGridView dgvShowProducts;
    }
}