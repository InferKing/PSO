namespace WindowsFormsApp5
{
    partial class target
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
            this.label1 = new System.Windows.Forms.Label();
            this.targetX = new System.Windows.Forms.TextBox();
            this.targetY = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.targetButton1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(47, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Координаты цели";
            // 
            // targetX
            // 
            this.targetX.Location = new System.Drawing.Point(236, 34);
            this.targetX.Name = "targetX";
            this.targetX.Size = new System.Drawing.Size(100, 22);
            this.targetX.TabIndex = 1;
            // 
            // targetY
            // 
            this.targetY.Location = new System.Drawing.Point(236, 62);
            this.targetY.Name = "targetY";
            this.targetY.Size = new System.Drawing.Size(100, 22);
            this.targetY.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(215, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "X";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(214, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Y";
            // 
            // targetButton1
            // 
            this.targetButton1.Location = new System.Drawing.Point(140, 128);
            this.targetButton1.Name = "targetButton1";
            this.targetButton1.Size = new System.Drawing.Size(131, 35);
            this.targetButton1.TabIndex = 5;
            this.targetButton1.Text = "Подтвердить";
            this.targetButton1.UseVisualStyleBackColor = true;
            this.targetButton1.Click += new System.EventHandler(this.targetButton1_Click);
            // 
            // target
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 187);
            this.Controls.Add(this.targetButton1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.targetY);
            this.Controls.Add(this.targetX);
            this.Controls.Add(this.label1);
            this.Name = "target";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox targetX;
        private System.Windows.Forms.TextBox targetY;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button targetButton1;
    }
}