namespace kut_oto
{
    partial class KullaniciSilFormu
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
            dataGridView1 = new DataGridView();
            Sitbtn = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 99);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(776, 328);
            dataGridView1.TabIndex = 0;
            // 
            // Sitbtn
            // 
            Sitbtn.BackColor = Color.Sienna;
            Sitbtn.Location = new Point(12, 12);
            Sitbtn.Name = "Sitbtn";
            Sitbtn.Size = new Size(111, 68);
            Sitbtn.TabIndex = 1;
            Sitbtn.Text = "Sil";
            Sitbtn.UseVisualStyleBackColor = false;
            Sitbtn.Click += Silbtn_Click;
            // 
            // KullaniciSilFormu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(800, 450);
            Controls.Add(Sitbtn);
            Controls.Add(dataGridView1);
            Name = "KullaniciSilFormu";
            Text = "KullaniciSilFormu";
            Load += KullaniciSilFormu_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridView1;
        private Button Sitbtn;
    }
}