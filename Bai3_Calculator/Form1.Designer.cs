namespace WinFormsApp1
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
            So_A = new Label();
            A = new TextBox();
            Cong = new Button();
            Nhan = new Button();
            Tru = new Button();
            Chia = new Button();
            So_B = new Label();
            B = new TextBox();
            Ket_qua = new Label();
            KQ = new TextBox();
            Close = new Button();
            SuspendLayout();
            // 
            // So_A
            // 
            So_A.AutoSize = true;
            So_A.Location = new Point(41, 29);
            So_A.Name = "So_A";
            So_A.Size = new Size(42, 20);
            So_A.TabIndex = 0;
            So_A.Text = "So_A";
            So_A.Click += label1_Click;
            // 
            // A
            // 
            A.Location = new Point(121, 26);
            A.Name = "A";
            A.Size = new Size(125, 27);
            A.TabIndex = 1;
            A.TextChanged += textBox1_TextChanged;
            // 
            // Cong
            // 
            Cong.Location = new Point(290, 26);
            Cong.Name = "Cong";
            Cong.Size = new Size(94, 29);
            Cong.TabIndex = 2;
            Cong.Text = "Cong";
            Cong.UseVisualStyleBackColor = true;
            Cong.Click += Cong_Click;
            // 
            // Nhan
            // 
            Nhan.Location = new Point(290, 71);
            Nhan.Name = "Nhan";
            Nhan.Size = new Size(94, 29);
            Nhan.TabIndex = 3;
            Nhan.Text = "Nhan";
            Nhan.UseVisualStyleBackColor = true;
            Nhan.Click += Nhan_Click;
            // 
            // Tru
            // 
            Tru.Location = new Point(433, 25);
            Tru.Name = "Tru";
            Tru.Size = new Size(94, 29);
            Tru.TabIndex = 4;
            Tru.Text = "Tru";
            Tru.UseVisualStyleBackColor = true;
            Tru.Click += Tru_Click;
            // 
            // Chia
            // 
            Chia.Location = new Point(433, 71);
            Chia.Name = "Chia";
            Chia.Size = new Size(94, 29);
            Chia.TabIndex = 5;
            Chia.Text = "Chia";
            Chia.UseVisualStyleBackColor = true;
            Chia.Click += Chia_Click;
            // 
            // So_B
            // 
            So_B.AutoSize = true;
            So_B.Location = new Point(41, 75);
            So_B.Name = "So_B";
            So_B.Size = new Size(39, 20);
            So_B.TabIndex = 6;
            So_B.Text = "So B";
            So_B.Click += label2_Click;
            // 
            // B
            // 
            B.Location = new Point(121, 71);
            B.Name = "B";
            B.Size = new Size(125, 27);
            B.TabIndex = 7;
            // 
            // Ket_qua
            // 
            Ket_qua.AutoSize = true;
            Ket_qua.Location = new Point(41, 121);
            Ket_qua.Name = "Ket_qua";
            Ket_qua.Size = new Size(62, 20);
            Ket_qua.TabIndex = 8;
            Ket_qua.Text = "Ket_qua";
            // 
            // KQ
            // 
            KQ.Location = new Point(121, 118);
            KQ.Name = "KQ";
            KQ.Size = new Size(125, 27);
            KQ.TabIndex = 9;
            // 
            // Close
            // 
            Close.Location = new Point(290, 118);
            Close.Name = "Close";
            Close.Size = new Size(94, 29);
            Close.TabIndex = 10;
            Close.Text = "Close";
            Close.UseVisualStyleBackColor = true;
            Close.Click += Close_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(551, 179);
            Controls.Add(Close);
            Controls.Add(KQ);
            Controls.Add(Ket_qua);
            Controls.Add(B);
            Controls.Add(So_B);
            Controls.Add(Chia);
            Controls.Add(Tru);
            Controls.Add(Nhan);
            Controls.Add(Cong);
            Controls.Add(A);
            Controls.Add(So_A);
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label So_A;
        private TextBox A;
        private Button Cong;
        private Button Nhan;
        private Button Tru;
        private Button Chia;
        private Label So_B;
        private TextBox B;
        private Label Ket_qua;
        private TextBox KQ;
        private Button Close;
    }
}
