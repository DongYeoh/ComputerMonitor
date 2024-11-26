namespace ComputerMonitorApp.MonitorControls
{
    partial class RAMMonitorControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.nameLabel1 = new ComputerMonitorApp.UI.NameLabel();
            this.valueLabel1 = new ComputerMonitorApp.UI.ValueLabel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 43F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 43F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.nameLabel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.valueLabel1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(192, 29);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // nameLabel1
            // 
            this.nameLabel1.AutoSize = true;
            this.nameLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nameLabel1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.nameLabel1.Location = new System.Drawing.Point(3, 0);
            this.nameLabel1.Name = "nameLabel1";
            this.nameLabel1.Size = new System.Drawing.Size(37, 29);
            this.nameLabel1.TabIndex = 0;
            this.nameLabel1.Text = "内存";
            this.nameLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // valueLabel1
            // 
            this.valueLabel1.AutoSize = true;
            this.valueLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.valueLabel1.Location = new System.Drawing.Point(46, 0);
            this.valueLabel1.Name = "valueLabel1";
            this.valueLabel1.Size = new System.Drawing.Size(37, 29);
            this.valueLabel1.TabIndex = 1;
            this.valueLabel1.Text = "-";
            this.valueLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // RAMMonitorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "RAMMonitorControl";
            this.Size = new System.Drawing.Size(192, 29);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private UI.NameLabel nameLabel1;
        private UI.ValueLabel valueLabel1;
    }
}
