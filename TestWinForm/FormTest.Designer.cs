namespace TestWinForm
{
    partial class FormTest
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
            panel1 = new Panel();
            checkedListBox1 = new CheckedListBox();
            textBox1 = new TextBox();
            progressBar1 = new ProgressBar();
            button1 = new Button();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.AppWorkspace;
            panel1.Controls.Add(checkedListBox1);
            panel1.Location = new Point(169, 80);
            panel1.Name = "panel1";
            panel1.Size = new Size(583, 132);
            panel1.TabIndex = 0;
            // 
            // checkedListBox1
            // 
            checkedListBox1.BackColor = Color.Black;
            checkedListBox1.ForeColor = Color.White;
            checkedListBox1.FormattingEnabled = true;
            checkedListBox1.Location = new Point(85, 16);
            checkedListBox1.Name = "checkedListBox1";
            checkedListBox1.Size = new Size(161, 94);
            checkedListBox1.TabIndex = 0;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 12);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 1;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(127, 12);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(100, 23);
            progressBar1.TabIndex = 2;
            // 
            // button1
            // 
            button1.Location = new Point(412, 36);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 4;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // FormTest
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 241);
            Controls.Add(button1);
            Controls.Add(panel1);
            Controls.Add(progressBar1);
            Controls.Add(textBox1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Name = "FormTest";
            Text = "Form1";
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private TextBox textBox1;
        private ProgressBar progressBar1;
        private CheckedListBox checkedListBox1;
        private Button button1;
    }
}
