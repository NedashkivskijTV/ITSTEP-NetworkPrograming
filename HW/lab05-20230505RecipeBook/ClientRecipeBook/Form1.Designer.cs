namespace ClientRecipeBook
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
            this.label1 = new System.Windows.Forms.Label();
            this.dgvRecipesList = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.btnGetRecipes = new System.Windows.Forms.Button();
            this.tbIngredients = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecipesList)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(235, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Enter the ingredients separated by commas";
            // 
            // dgvRecipesList
            // 
            this.dgvRecipesList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRecipesList.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders;
            this.dgvRecipesList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecipesList.Location = new System.Drawing.Point(12, 110);
            this.dgvRecipesList.Name = "dgvRecipesList";
            this.dgvRecipesList.RowTemplate.Height = 25;
            this.dgvRecipesList.Size = new System.Drawing.Size(442, 258);
            this.dgvRecipesList.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "List of recipes";
            // 
            // btnGetRecipes
            // 
            this.btnGetRecipes.Location = new System.Drawing.Point(222, 39);
            this.btnGetRecipes.Name = "btnGetRecipes";
            this.btnGetRecipes.Size = new System.Drawing.Size(232, 23);
            this.btnGetRecipes.TabIndex = 4;
            this.btnGetRecipes.Text = "Get Recipes";
            this.btnGetRecipes.UseVisualStyleBackColor = true;
            this.btnGetRecipes.Click += new System.EventHandler(this.btnGetRecipes_Click);
            // 
            // tbIngredients
            // 
            this.tbIngredients.Location = new System.Drawing.Point(12, 39);
            this.tbIngredients.Name = "tbIngredients";
            this.tbIngredients.Size = new System.Drawing.Size(204, 23);
            this.tbIngredients.TabIndex = 5;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 380);
            this.Controls.Add(this.tbIngredients);
            this.Controls.Add(this.btnGetRecipes);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvRecipesList);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Client";
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecipesList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Label label1;
        private DataGridView dgvRecipesList;
        private Label label2;
        private Button btnGetRecipes;
        private TextBox tbIngredients;
    }
}