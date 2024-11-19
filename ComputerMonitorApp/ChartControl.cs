using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputerMonitorApp;
public partial class ChartControl : UserControl
{
    /// <summary>
    /// Y轴最大值
    /// </summary>
    public double YMaximum { get; set; }
    /// <summary>
    /// 缓存大小
    /// </summary>
    public int BufferLimit { get; set; }

    private Queue<double> buffer = new Queue<double>();
    public ChartControl()
    {
        InitializeComponent();
        this.DoubleBuffered = true;
        this.YMaximum = 100.0;
        this.BufferLimit = 60;
    }
    

    public void AddValue(double value)
    {
        buffer.Enqueue(value);
        if (buffer.Count > this.BufferLimit)
        {
            buffer.Dequeue();
        }
        this.Invalidate();
    }
    /// <summary>
    /// Y轴显示区域占比，占用总高度的比例
    /// </summary>
    private const double YAreaRatio = 0.5;
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        if (this.buffer.Count <= 1)
        {
            return;
        }
        var g = e.Graphics;
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        var lastPoint = new Point(0, 0);
        var xStep = this.Width * 1.0 / (this.BufferLimit - 1);
        var totalHeight = this.Height * YAreaRatio;
        var offset = (this.Height - totalHeight) * 0.5;
        var yStep = totalHeight * 1.0 / this.YMaximum;
        var points = buffer.Select((value, index) => {
            return new PointF((int)(index * xStep), (int)(totalHeight - value * yStep+ offset));
        }).ToArray();
        using (var pen = new Pen(Color.Red,2.0f))
        {
            g.DrawLines(pen, points);
        }
    }
}
