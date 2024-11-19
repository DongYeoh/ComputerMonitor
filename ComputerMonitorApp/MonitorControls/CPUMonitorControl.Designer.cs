namespace ComputerMonitorApp.MonitorControls;

partial class CPUMonitorControl
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
        label1 = new Label();
        labelLoad = new Label();
        chartControl = new ChartControl();
        tableLayoutPanel1 = new TableLayoutPanel();
        tableLayoutPanel1.SuspendLayout();
        SuspendLayout();
        // 
        // label1
        // 
        label1.Anchor = AnchorStyles.None;
        label1.AutoSize = true;
        label1.Font = new Font("Microsoft YaHei UI", 9F, FontStyle.Bold);
        label1.ForeColor = Color.Yellow;
        label1.Location = new Point(8, 9);
        label1.Name = "label1";
        label1.Size = new Size(33, 17);
        label1.TabIndex = 0;
        label1.Text = "CPU";
        // 
        // labelLoad
        // 
        labelLoad.Anchor = AnchorStyles.None;
        labelLoad.Location = new Point(53, 9);
        labelLoad.Name = "labelLoad";
        labelLoad.Size = new Size(44, 17);
        labelLoad.TabIndex = 2;
        // 
        // chartControl
        // 
        chartControl.BackColor = Color.DarkGray;
        chartControl.BufferLimit = 60;
        chartControl.Dock = DockStyle.Fill;
        chartControl.Location = new Point(100, 0);
        chartControl.Margin = new Padding(0);
        chartControl.Name = "chartControl";
        chartControl.Size = new Size(125, 36);
        chartControl.TabIndex = 3;
        chartControl.YMaximum = 100D;
        // 
        // tableLayoutPanel1
        // 
        tableLayoutPanel1.ColumnCount = 3;
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 50F));
        tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.Controls.Add(chartControl, 2, 0);
        tableLayoutPanel1.Controls.Add(labelLoad, 1, 0);
        tableLayoutPanel1.Controls.Add(label1, 0, 0);
        tableLayoutPanel1.Dock = DockStyle.Fill;
        tableLayoutPanel1.Location = new Point(0, 0);
        tableLayoutPanel1.Margin = new Padding(0);
        tableLayoutPanel1.Name = "tableLayoutPanel1";
        tableLayoutPanel1.RowCount = 1;
        tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableLayoutPanel1.Size = new Size(225, 36);
        tableLayoutPanel1.TabIndex = 4;
        // 
        // CPUMonitorControl
        // 
        AutoScaleDimensions = new SizeF(7F, 17F);
        AutoScaleMode = AutoScaleMode.Font;
        Controls.Add(tableLayoutPanel1);
        Margin = new Padding(0);
        Name = "CPUMonitorControl";
        Size = new Size(225, 36);
        tableLayoutPanel1.ResumeLayout(false);
        tableLayoutPanel1.PerformLayout();
        ResumeLayout(false);
    }

    #endregion

    private Label label1;
    private Label labelLoad;
    private ChartControl chartControl;
    private TableLayoutPanel tableLayoutPanel1;
}
