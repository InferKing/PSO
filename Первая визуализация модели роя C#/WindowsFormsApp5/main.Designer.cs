namespace WindowsFormsApp5
{
    partial class main
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.startButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.targetButton = new System.Windows.Forms.Button();
            this.toStartButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Window;
            this.pictureBox1.BackgroundImage = global::WindowsFormsApp5.Properties.Resources._1620265763_33_phonoteka_org_p_fon_karta_goroda_45;
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1274, 860);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // startButton
            // 
            this.startButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.startButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.startButton.Location = new System.Drawing.Point(1350, 41);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(197, 36);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "Начать движение";
            this.startButton.UseVisualStyleBackColor = false;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1302, 228);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(280, 644);
            this.textBox1.TabIndex = 2;
            // 
            // targetButton
            // 
            this.targetButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.targetButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.targetButton.Location = new System.Drawing.Point(1350, 96);
            this.targetButton.Name = "targetButton";
            this.targetButton.Size = new System.Drawing.Size(197, 36);
            this.targetButton.TabIndex = 3;
            this.targetButton.Text = "Отправить к цели";
            this.targetButton.UseVisualStyleBackColor = false;
            this.targetButton.Click += new System.EventHandler(this.targetButton_Click);
            // 
            // toStartButton
            // 
            this.toStartButton.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.toStartButton.Location = new System.Drawing.Point(1350, 147);
            this.toStartButton.Name = "toStartButton";
            this.toStartButton.Size = new System.Drawing.Size(197, 47);
            this.toStartButton.TabIndex = 4;
            this.toStartButton.Text = "Вернуть на исходные позиции";
            this.toStartButton.UseVisualStyleBackColor = false;
            this.toStartButton.Click += new System.EventHandler(this.toStartButton_Click);
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.ClientSize = new System.Drawing.Size(1594, 884);
            this.Controls.Add(this.toStartButton);
            this.Controls.Add(this.targetButton);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.pictureBox1);
            this.Name = "main";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button targetButton;
        private System.Windows.Forms.Button toStartButton;
    }
}

