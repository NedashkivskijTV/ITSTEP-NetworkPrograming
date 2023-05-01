namespace ClientProducts
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
            this.dgvProductsCollection = new System.Windows.Forms.DataGridView();
            this.btnGetProducts = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductsCollection)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvProductsCollection
            // 
            this.dgvProductsCollection.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductsCollection.Location = new System.Drawing.Point(12, 12);
            this.dgvProductsCollection.Name = "dgvProductsCollection";
            this.dgvProductsCollection.RowTemplate.Height = 25;
            this.dgvProductsCollection.Size = new System.Drawing.Size(388, 270);
            this.dgvProductsCollection.TabIndex = 0;
            // 
            // btnGetProducts
            // 
            this.btnGetProducts.Location = new System.Drawing.Point(12, 288);
            this.btnGetProducts.Name = "btnGetProducts";
            this.btnGetProducts.Size = new System.Drawing.Size(103, 23);
            this.btnGetProducts.TabIndex = 1;
            this.btnGetProducts.Text = "Get Products";
            this.btnGetProducts.UseVisualStyleBackColor = true;
            this.btnGetProducts.Click += new System.EventHandler(this.btnGetProducts_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(412, 317);
            this.Controls.Add(this.btnGetProducts);
            this.Controls.Add(this.dgvProductsCollection);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductsCollection)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DataGridView dgvProductsCollection;
        private Button btnGetProducts;
    }
}