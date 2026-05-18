namespace kut_oto.Kaynaklar
{
    partial class KaynakEkleForm
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            kaynakAdtxt = new TextBox();
            kaynakYazartxt = new TextBox();
            kaynakYayincitxt = new TextBox();
            numericUpDown1 = new NumericUpDown();
            dateTimePicker1 = new DateTimePicker();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(338, 30);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(488, 367);
            dataGridView1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(45, 38);
            label1.Name = "label1";
            label1.Size = new Size(28, 15);
            label1.TabIndex = 1;
            label1.Text = "Ad :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(33, 73);
            label2.Name = "label2";
            label2.Size = new Size(40, 15);
            label2.TabIndex = 2;
            label2.Text = "Yazar :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(23, 110);
            label3.Name = "label3";
            label3.Size = new Size(50, 15);
            label3.TabIndex = 3;
            label3.Text = "Yayıncı :";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(0, 153);
            label4.Name = "label4";
            label4.RightToLeft = RightToLeft.No;
            label4.Size = new Size(73, 15);
            label4.TabIndex = 4;
            label4.Text = "Sayfa Sayısı :";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(33, 187);
            label5.Name = "label5";
            label5.Size = new Size(39, 15);
            label5.TabIndex = 5;
            label5.Text = "Tarih :";
            // 
            // kaynakAdtxt
            // 
            kaynakAdtxt.Location = new Point(84, 30);
            kaynakAdtxt.Name = "kaynakAdtxt";
            kaynakAdtxt.Size = new Size(120, 23);
            kaynakAdtxt.TabIndex = 6;
            // 
            // kaynakYazartxt
            // 
            kaynakYazartxt.Location = new Point(84, 65);
            kaynakYazartxt.Name = "kaynakYazartxt";
            kaynakYazartxt.Size = new Size(120, 23);
            kaynakYazartxt.TabIndex = 7;
            // 
            // kaynakYayincitxt
            // 
            kaynakYayincitxt.Location = new Point(84, 107);
            kaynakYayincitxt.Name = "kaynakYayincitxt";
            kaynakYayincitxt.Size = new Size(120, 23);
            kaynakYayincitxt.TabIndex = 8;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(84, 145);
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(120, 23);
            numericUpDown1.TabIndex = 9;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(84, 181);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(193, 23);
            dateTimePicker1.TabIndex = 10;
            // 
            // button1
            // 
            button1.BackColor = Color.Sienna;
            button1.Location = new Point(45, 227);
            button1.Name = "button1";
            button1.Size = new Size(173, 53);
            button1.TabIndex = 11;
            button1.Text = "Kaydet";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // KaynakEkleForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(939, 450);
            Controls.Add(button1);
            Controls.Add(dateTimePicker1);
            Controls.Add(numericUpDown1);
            Controls.Add(kaynakYayincitxt);
            Controls.Add(kaynakYazartxt);
            Controls.Add(kaynakAdtxt);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(dataGridView1);
            ForeColor = SystemColors.ControlText;
            Name = "KaynakEkleForm";
            Text = "KaynakEkleForm";
            Load += KaynakEkleForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private TextBox kaynakAdtxt;
        private TextBox kaynakYazartxt;
        private TextBox kaynakYayincitxt;
        private NumericUpDown numericUpDown1;
        private DateTimePicker dateTimePicker1;
        private Button button1;
    }
}