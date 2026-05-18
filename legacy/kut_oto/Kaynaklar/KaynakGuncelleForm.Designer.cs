namespace kut_oto.Kaynaklar
{
    partial class KaynakGüncelleForm
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
            button1 = new Button();
            dateTimePicker1 = new DateTimePicker();
            numericUpDown1 = new NumericUpDown();
            kaynakYayincitxt = new TextBox();
            kaynakYazartxt = new TextBox();
            kaynakAdtxt = new TextBox();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(316, 25);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(608, 363);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellClick += dataGridView1_CellClick;
            // 
            // button1
            // 
            button1.BackColor = Color.Sienna;
            button1.Location = new Point(51, 222);
            button1.Name = "button1";
            button1.Size = new Size(173, 53);
            button1.TabIndex = 22;
            button1.Text = "Güncelle";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(73, 176);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(193, 23);
            dateTimePicker1.TabIndex = 21;
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(90, 140);
            numericUpDown1.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(120, 23);
            numericUpDown1.TabIndex = 20;
            // 
            // kaynakYayincitxt
            // 
            kaynakYayincitxt.Location = new Point(90, 102);
            kaynakYayincitxt.Name = "kaynakYayincitxt";
            kaynakYayincitxt.Size = new Size(120, 23);
            kaynakYayincitxt.TabIndex = 19;
            // 
            // kaynakYazartxt
            // 
            kaynakYazartxt.Location = new Point(90, 60);
            kaynakYazartxt.Name = "kaynakYazartxt";
            kaynakYazartxt.Size = new Size(120, 23);
            kaynakYazartxt.TabIndex = 18;
            // 
            // kaynakAdtxt
            // 
            kaynakAdtxt.Location = new Point(90, 25);
            kaynakAdtxt.Name = "kaynakAdtxt";
            kaynakAdtxt.Size = new Size(120, 23);
            kaynakAdtxt.TabIndex = 17;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(28, 182);
            label5.Name = "label5";
            label5.Size = new Size(39, 15);
            label5.TabIndex = 16;
            label5.Text = "Tarih :";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 148);
            label4.Name = "label4";
            label4.RightToLeft = RightToLeft.No;
            label4.Size = new Size(73, 15);
            label4.TabIndex = 15;
            label4.Text = "Sayfa Sayısı :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(29, 105);
            label3.Name = "label3";
            label3.Size = new Size(50, 15);
            label3.TabIndex = 14;
            label3.Text = "Yayıncı :";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(39, 68);
            label2.Name = "label2";
            label2.Size = new Size(40, 15);
            label2.TabIndex = 13;
            label2.Text = "Yazar :";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(51, 33);
            label1.Name = "label1";
            label1.Size = new Size(28, 15);
            label1.TabIndex = 12;
            label1.Text = "Ad :";
            // 
            // KaynakGüncelleForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveCaption;
            ClientSize = new Size(936, 450);
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
            Name = "KaynakGüncelleForm";
            Text = "KaynakGüncelleForm";
            Load += KaynakGüncelleForm_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button button1;
        private DateTimePicker dateTimePicker1;
        private NumericUpDown numericUpDown1;
        private TextBox kaynakYayincitxt;
        private TextBox kaynakYazartxt;
        private TextBox kaynakAdtxt;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
    }
}