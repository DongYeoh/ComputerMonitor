namespace ComputerMonitorApp.MonitorControls
{
    partial class NetworkMonitorControl
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
            this.valueLabelDowanload = new ComputerMonitorApp.UI.ValueLabel();
            this.valueLabelUpload = new ComputerMonitorApp.UI.ValueLabel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 43F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.nameLabel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.valueLabelDowanload, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.valueLabelUpload, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(217, 38);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // nameLabel1
            // 
            this.nameLabel1.AutoSize = true;
            this.nameLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nameLabel1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.nameLabel1.Location = new System.Drawing.Point(3, 0);
            this.nameLabel1.Name = "nameLabel1";
            this.tableLayoutPanel1.SetRowSpan(this.nameLabel1, 2);
            this.nameLabel1.Size = new System.Drawing.Size(37, 38);
            this.nameLabel1.TabIndex = 0;
            this.nameLabel1.Text = "网络";
            this.nameLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // valueLabelDowanload
            // 
            this.valueLabelDowanload.AutoSize = true;
            this.valueLabelDowanload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.valueLabelDowanload.Location = new System.Drawing.Point(46, 0);
            this.valueLabelDowanload.Name = "valueLabelDowanload";
            this.valueLabelDowanload.Size = new System.Drawing.Size(168, 19);
            this.valueLabelDowanload.TabIndex = 1;
            this.valueLabelDowanload.Text = "-";
            this.valueLabelDowanload.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // valueLabelUpload
            // 
            this.valueLabelUpload.AutoSize = true;
            this.valueLabelUpload.Dock = System.Windows.Forms.DockStyle.Fill;
            this.valueLabelUpload.Location = new System.Drawing.Point(46, 19);
            this.valueLabelUpload.Name = "valueLabelUpload";
            this.valueLabelUpload.Size = new System.Drawing.Size(168, 19);
            this.valueLabelUpload.TabIndex = 2;
            this.valueLabelUpload.Text = "-";
            this.valueLabelUpload.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // NetworkMonitorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "NetworkMonitorControl";
            this.Size = new System.Drawing.Size(217, 38);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private UI.NameLabel nameLabel1;
        private UI.ValueLabel valueLabelDowanload;
        private UI.ValueLabel valueLabelUpload;
    }
}
