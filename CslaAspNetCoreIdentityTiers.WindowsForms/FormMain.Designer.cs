namespace CslaAspNetCoreIdentityTiers.WindowsForms
{
    partial class FormMain
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
            this.btnTestExiss = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnTestExiss
            // 
            this.btnTestExiss.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnTestExiss.Location = new System.Drawing.Point(14, 14);
            this.btnTestExiss.Name = "btnTestExiss";
            this.btnTestExiss.Size = new System.Drawing.Size(438, 48);
            this.btnTestExiss.TabIndex = 0;
            this.btnTestExiss.Text = "Test exists normalized user name async";
            this.btnTestExiss.UseVisualStyleBackColor = true;
            this.btnTestExiss.Click += new System.EventHandler(this.TestExistsNormalizedUserNameAsync);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 281);
            this.Controls.Add(this.btnTestExiss);
            this.Name = "FormMain";
            this.Text = "Test Form";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnTestExiss;
    }
}

