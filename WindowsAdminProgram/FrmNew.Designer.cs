namespace WindowsAdminProgram
{
    partial class FrmNew
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
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtCountryProduced = new System.Windows.Forms.TextBox();
            this.dtpDateProduced = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(15, 167);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(99, 16);
            this.label10.TabIndex = 28;
            this.label10.Text = "Date Produced";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(15, 193);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(115, 16);
            this.label11.TabIndex = 29;
            this.label11.Text = "Country Produced";
            // 
            // txtCountryProduced
            // 
            this.txtCountryProduced.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCountryProduced.Location = new System.Drawing.Point(131, 190);
            this.txtCountryProduced.Name = "txtCountryProduced";
            this.txtCountryProduced.Size = new System.Drawing.Size(181, 22);
            this.txtCountryProduced.TabIndex = 30;
            // 
            // dtpDateProduced
            // 
            this.dtpDateProduced.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDateProduced.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDateProduced.Location = new System.Drawing.Point(131, 162);
            this.dtpDateProduced.Name = "dtpDateProduced";
            this.dtpDateProduced.Size = new System.Drawing.Size(109, 22);
            this.dtpDateProduced.TabIndex = 31;
            // 
            // FrmNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(330, 321);
            this.Controls.Add(this.dtpDateProduced);
            this.Controls.Add(this.txtCountryProduced);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Name = "FrmNew";
            this.Text = "New Item - Gibson Brand";
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.label11, 0);
            this.Controls.SetChildIndex(this.txtCountryProduced, 0);
            this.Controls.SetChildIndex(this.dtpDateProduced, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtCountryProduced;
        private System.Windows.Forms.DateTimePicker dtpDateProduced;
    }
}
