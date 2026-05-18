namespace kut_oto
{
    partial class KullaniciGüncelleForm
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
            button1 = new Button();
            radioK = new RadioButton();
            radioE = new RadioButton();
            label7 = new Label();
            kullaniciCezatxt = new TextBox();
            kullaniciMailtxt = new TextBox();
            kullaniciTeltxt = new TextBox();
            kullaniciTctxt = new TextBox();
            kullaniciSoyadtxt = new TextBox();
            kullaniciAdtxt = new TextBox();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            dataGridView1 = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // button1
            // 
            button1.BackColor = Color.Sienna;
            button1.Location = new Point(26, 241);
            button1.Name = "button1";
            button1.Size = new Size(237, 39);
            button1.TabIndex = 31;
            button1.Text = "Kaydet";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // radioK
            // 
            radioK.AutoSize = true;
            radioK.Location = new Point(206, 55);
            radioK.Name = "radioK";
            radioK.Size = new Size(32, 19);
            radioK.TabIndex = 30;
            radioK.TabStop = true;
            radioK.Text = "K";
            radioK.UseVisualStyleBackColor = true;
            // 
            // radioE
            // 
            radioE.AutoSize = true;
            radioE.Location = new Point(72, 54);
            radioE.Name = "radioE";
            radioE.Size = new Size(31, 19);
            radioE.TabIndex = 29;
            radioE.TabStop = true;
            radioE.Text = "E";
            radioE.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(7, 51);
            label7.Name = "label7";
            label7.Size = new Size(55, 15);
            label7.TabIndex = 28;
            label7.Text = "Cinsiyet :";
            // 
            // kullaniciCezatxt
            // 
            kullaniciCezatxt.Location = new Point(68, 199);
            kullaniciCezatxt.Name = "kullaniciCezatxt";
            kullaniciCezatxt.Size = new Size(193, 23);
            kullaniciCezatxt.TabIndex = 27;
            // 
            // kullaniciMailtxt
            // 
            kullaniciMailtxt.Location = new Point(68, 170);
            kullaniciMailtxt.Name = "kullaniciMailtxt";
            kullaniciMailtxt.Size = new Size(193, 23);
            kullaniciMailtxt.TabIndex = 26;
            // 
            // kullaniciTeltxt
            // 
            kullaniciTeltxt.Location = new Point(68, 141);
            kullaniciTeltxt.Name = "kullaniciTeltxt";
            kullaniciTeltxt.Size = new Size(193, 23);
            kullaniciTeltxt.TabIndex = 25;
            // 
            // kullaniciTctxt
            // 
            kullaniciTctxt.Location = new Point(68, 112);
            kullaniciTctxt.Name = "kullaniciTctxt";
            kullaniciTctxt.Size = new Size(193, 23);
            kullaniciTctxt.TabIndex = 24;
            // 
            // kullaniciSoyadtxt
            // 
            kullaniciSoyadtxt.Location = new Point(68, 80);
            kullaniciSoyadtxt.Name = "kullaniciSoyadtxt";
            kullaniciSoyadtxt.Size = new Size(193, 23);
            kullaniciSoyadtxt.TabIndex = 23;
            // 
            // kullaniciAdtxt
            // 
            kullaniciAdtxt.Location = new Point(68, 19);
            kullaniciAdtxt.Name = "kullaniciAdtxt";
            kullaniciAdtxt.Size = new Size(193, 23);
            kullaniciAdtxt.TabIndex = 22;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(24, 207);
            label6.Name = "label6";
            label6.Size = new Size(38, 15);
            label6.TabIndex = 21;
            label6.Text = "Ceza :";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(26, 178);
            label5.Name = "label5";
            label5.Size = new Size(36, 15);
            label5.TabIndex = 20;
            label5.Text = "Mail :";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(34, 149);
            label4.Name = "label4";
            label4.Size = new Size(28, 15);
            label4.TabIndex = 19;
            label4.Text = "Tel :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(37, 120);
            label3.Name = "label3";
            label3.Size = new Size(25, 15);
            label3.TabIndex = 18;
            label3.Text = "Tc :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(17, 88);
            label2.Name = "label2";
            label2.Size = new Size(45, 15);
            label2.TabIndex = 17;
            label2.Text = "Soyad :";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(34, 22);
            label1.Name = "label1";
            label1.Size = new Size(28, 15);
            label1.TabIndex = 16;
            label1.Text = "Ad :";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(342, 22);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(583, 416);
            dataGridView1.TabIndex = 32;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // KullaniciGüncelleForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(937, 450);
            Controls.Add(dataGridView1);
            Controls.Add(button1);
            Controls.Add(radioK);
            Controls.Add(radioE);
            Controls.Add(label7);
            Controls.Add(kullaniciCezatxt);
            Controls.Add(kullaniciMailtxt);
            Controls.Add(kullaniciTeltxt);
            Controls.Add(kullaniciTctxt);
            Controls.Add(kullaniciSoyadtxt);
            Controls.Add(kullaniciAdtxt);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            ForeColor = SystemColors.ControlText;
            Name = "KullaniciGüncelleForm";
            Text = "KullaniciGüncelleForm";
            Load += KullaniciGüncelleForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private RadioButton radioK;
        private RadioButton radioE;
        private Label label7;
        private TextBox kullaniciCezatxt;
        private TextBox kullaniciMailtxt;
        private TextBox kullaniciTeltxt;
        private TextBox kullaniciTctxt;
        private TextBox kullaniciSoyadtxt;
        private TextBox kullaniciAdtxt;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private DataGridView dataGridView1;
    }
}