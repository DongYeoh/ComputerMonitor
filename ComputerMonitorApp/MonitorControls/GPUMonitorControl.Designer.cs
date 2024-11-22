using System.Windows.Forms;
using System.Drawing;
namespace ComputerMonitorApp.MonitorControls
{

    partial class GPUMonitorControl
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
            this.labelLoad = new ComputerMonitorApp.UI.ValueLabel();
            this.labelOther = new ComputerMonitorApp.UI.ValueLabel();
            this.chartControlLoad = new ComputerMonitorApp.ChartControl();
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
            this.tableLayoutPanel1.Controls.Add(this.labelLoad, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.labelOther, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.chartControlLoad, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(209, 48);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // nameLabel1
            // 
            this.nameLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nameLabel1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
            this.nameLabel1.ForeColor = System.Drawing.Color.Yellow;
            this.nameLabel1.Location = new System.Drawing.Point(3, 0);
            this.nameLabel1.Name = "nameLabel1";
            this.tableLayoutPanel1.SetRowSpan(this.nameLabel1, 2);
            this.nameLabel1.Size = new System.Drawing.Size(37, 48);
            this.nameLabel1.TabIndex = 0;
            this.nameLabel1.Text = "GPU";
            this.nameLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelLoad
            // 
            this.labelLoad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelLoad.Location = new System.Drawing.Point(46, 0);
            this.labelLoad.Name = "labelLoad";
            this.labelLoad.Size = new System.Drawing.Size(37, 24);
            this.labelLoad.TabIndex = 1;
            this.labelLoad.Text = "-";
            this.labelLoad.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelOther
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.labelOther, 2);
            this.labelOther.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelOther.Location = new System.Drawing.Point(46, 24);
            this.labelOther.Name = "labelOther";
            this.labelOther.Size = new System.Drawing.Size(160, 24);
            this.labelOther.TabIndex = 2;
            this.labelOther.Text = "-";
            this.labelOther.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // chartControlLoad
            // 
            this.chartControlLoad.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartControlLoad.Location = new System.Drawing.Point(89, 1);
            this.chartControlLoad.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
            this.chartControlLoad.Name = "chartControlLoad";
            this.chartControlLoad.Size = new System.Drawing.Size(117, 22);
            this.chartControlLoad.TabIndex = 4;
            // 
            // GPUMonitorControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "GPUMonitorControl";
            this.Size = new System.Drawing.Size(209, 48);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private UI.NameLabel nameLabel1;
        private UI.ValueLabel labelLoad;
        private UI.ValueLabel labelOther;
        private ChartControl chartControlLoad;
    }
}