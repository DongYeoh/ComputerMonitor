namespace ComputerMonitorApp;

partial class HeadBar
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
        buttonClose = new Button();
        mainContextMenuStrip = new MainContextMenuStrip();
        SuspendLayout();
        // 
        // buttonClose
        // 
        buttonClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        buttonClose.FlatAppearance.BorderSize = 0;
        buttonClose.FlatAppearance.MouseOverBackColor = Color.Red;
        buttonClose.FlatStyle = FlatStyle.Flat;
        buttonClose.Font = new Font("Microsoft YaHei UI", 7F);
        buttonClose.Location = new Point(227, -1);
        buttonClose.Margin = new Padding(0);
        buttonClose.Name = "buttonClose";
        buttonClose.Size = new Size(26, 24);
        buttonClose.TabIndex = 0;
        buttonClose.TabStop = false;
        buttonClose.Text = "X";
        buttonClose.UseVisualStyleBackColor = true;
        buttonClose.Click += buttonClose_Click;
        // 
        // mainContextMenuStrip
        // 
        mainContextMenuStrip.Name = "mainContextMenuStrip";
        mainContextMenuStrip.Size = new Size(61, 4);
        mainContextMenuStrip.Opening += mainContextMenuStrip_Opening;
        // 
        // HeadBar
        // 
        AutoScaleDimensions = new SizeF(7F, 17F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(255, 128, 128);
        BorderStyle = BorderStyle.FixedSingle;
        ContextMenuStrip = mainContextMenuStrip;
        Controls.Add(buttonClose);
        ForeColor = Color.White;
        Margin = new Padding(0);
        Name = "HeadBar";
        Size = new Size(253, 23);
        ResumeLayout(false);
    }

    #endregion

    private Button buttonClose;
    private MainContextMenuStrip mainContextMenuStrip;
}
