namespace TextPad
{
    partial class FindAndReplace
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
            this.Find_Btn = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Replace_Btn = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.Next_Btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Find_Btn
            // 
            this.Find_Btn.Location = new System.Drawing.Point(442, 15);
            this.Find_Btn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Find_Btn.Name = "Find_Btn";
            this.Find_Btn.Size = new System.Drawing.Size(112, 34);
            this.Find_Btn.TabIndex = 0;
            this.Find_Btn.Text = "查找";
            this.Find_Btn.UseVisualStyleBackColor = true;
            this.Find_Btn.Click += new System.EventHandler(this.Find_Btn_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(18, 18);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(398, 28);
            this.textBox1.TabIndex = 1;

            // 
            // Replace_Btn
            // 
            this.Replace_Btn.Location = new System.Drawing.Point(442, 102);
            this.Replace_Btn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Replace_Btn.Name = "Replace_Btn";
            this.Replace_Btn.Size = new System.Drawing.Size(112, 34);
            this.Replace_Btn.TabIndex = 2;
            this.Replace_Btn.Text = "替换";
            this.Replace_Btn.UseVisualStyleBackColor = true;
            this.Replace_Btn.Click += new System.EventHandler(this.Replace_Btn_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(18, 105);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(397, 28);
            this.textBox2.TabIndex = 3;
            // 
            // Next_Btn
            // 
            this.Next_Btn.Location = new System.Drawing.Point(442, 58);
            this.Next_Btn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Next_Btn.Name = "Next_Btn";
            this.Next_Btn.Size = new System.Drawing.Size(112, 34);
            this.Next_Btn.TabIndex = 4;
            this.Next_Btn.Text = "下一个";
            this.Next_Btn.UseVisualStyleBackColor = true;
            this.Next_Btn.Click += new System.EventHandler(this.Next_Btn_Click);
            // 
            // FindAndReplace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 154);
            this.Controls.Add(this.Next_Btn);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.Replace_Btn);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Find_Btn);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "FindAndReplace";
            this.Text = "Form2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Find_Btn;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button Replace_Btn;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button Next_Btn;
    }
}