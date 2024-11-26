using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComputerMonitorApp
{
    public partial class ChartControl : UserControl
    {

        /// <summary>
        /// Y轴最大值
        /// </summary>
        [Browsable(true)]
        [DefaultValue(100.0)]
        public double YMaximum { get; set; } = 100.0f;
        /// <summary>
        /// 缓存大小
        /// </summary>
        [Browsable(true)]
        [DefaultValue(30)]
        public int BufferLimit { get; set; } = 30;

        private Queue<double> buffer = new Queue<double>();
        public ChartControl()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
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
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.buffer.Clear();
        }
        /// <summary>
        /// Y轴显示区域占比，占用总高度的比例
        /// </summary>
        [Browsable(true)]
        [DefaultValue(0.4)]
        public double YAreaRatio { get; set; } = 0.4;
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (this.buffer.Count <= 1)
            {
                return;
            }
            var g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            var xStep = this.Width * 1.0 / (this.BufferLimit - 1);
            var totalHeight = this.Height * YAreaRatio;
            var offset = (this.Height - totalHeight) * 0.5;
            var yStep = totalHeight * 1.0 / this.YMaximum;
            var points = buffer.Select((value, index) =>
            {
                return new PointF((int)(index * xStep), (int)(totalHeight - value * yStep + offset));
            }).ToArray();
            using (var pen = new Pen(this.ForeColor, 2.0f))
            {
                g.DrawLines(pen, points);
            }
        }
    }
}
