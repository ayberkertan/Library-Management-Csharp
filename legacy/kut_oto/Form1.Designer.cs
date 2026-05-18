namespace kut_oto
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
            adGiristxt = new TextBox();
            sifreGiristxt = new TextBox();
            label1 = new Label();
            label2 = new Label();
            personelGirisbtn = new Button();
            SuspendLayout();
            // 
            // adGiristxt
            // 
            adGiristxt.Location = new Point(53, 97);
            adGiristxt.Name = "adGiristxt";
            adGiristxt.Size = new Size(110, 23);
            adGiristxt.TabIndex = 0;
            // 
            // sifreGiristxt
            // 
            sifreGiristxt.Location = new Point(53, 126);
            sifreGiristxt.Name = "sifreGiristxt";
            sifreGiristxt.Size = new Size(110, 23);
            sifreGiristxt.TabIndex = 1;
            sifreGiristxt.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(2, 105);
            label1.Name = "label1";
            label1.Size = new Size(25, 15);
            label1.TabIndex = 2;
            label1.Text = "Ad:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(2, 134);
            label2.Name = "label2";
            label2.Size = new Size(36, 15);
            label2.TabIndex = 3;
            label2.Text = "Şifre :";
            // 
            // personelGirisbtn
            // 
            personelGirisbtn.Location = new Point(12, 183);
            personelGirisbtn.Name = "personelGirisbtn";
            personelGirisbtn.Size = new Size(151, 23);
            personelGirisbtn.TabIndex = 4;
            personelGirisbtn.Text = "Giriş";
            personelGirisbtn.UseVisualStyleBackColor = true;
            personelGirisbtn.Click += personelGirisbtn_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightSeaGreen;
            BackgroundImage = kut_oto.Properties.Resources.admin_panel;
            BackgroundImageLayout = ImageLayout.Zoom;
            ClientSize = new Size(477, 366);
            Controls.Add(personelGirisbtn);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(sifreGiristxt);
            Controls.Add(adGiristxt);
            DoubleBuffered = true;
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox adGiristxt;
        private TextBox sifreGiristxt;
        private Label label1;
        private Label label2;
        private Button personelGirisbtn;
    }
}
