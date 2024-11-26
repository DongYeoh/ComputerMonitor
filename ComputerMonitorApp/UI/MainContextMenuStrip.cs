using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ComputerMonitorApp.UI
{

    internal class MainContextMenuStrip : ContextMenuStrip
    {
        public MainContextMenuStrip()
        {
            Renderer = new CustomRenderer();
            Closing += MainContextMenuStrip_Closing;
        }

        private void MainContextMenuStrip_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            //点击菜单项时，不隐藏
            if (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked)
            {
                e.Cancel = true;
            }
        }

        public ToolStripMenuItem AddCheckMenuItem(string text)
        {
            var item = new ToolStripMenuItem(text) { CheckOnClick = true };
            Items.Add(item);
            return item;
        }
        public ToolStripMenuItem AddMenuItem(string text)
        {
            var item = new ToolStripMenuItem(text);
            Items.Add(item);
            return item;
        }

        class CustomColorTable : ProfessionalColorTable
        {
            // 设置菜单背景色
            public override Color MenuStripGradientBegin => Color.Black;
            public override Color MenuStripGradientEnd => Color.Black;

            public override Color ImageMarginGradientBegin => Color.Black;
            public override Color ImageMarginGradientEnd => Color.Black;

            public override Color ImageMarginGradientMiddle => Color.Black;

            // 设置菜单项背景色
            public override Color MenuItemSelected => Color.DarkGray;
            public override Color MenuItemSelectedGradientBegin => Color.Gray;
            public override Color MenuItemPressedGradientMiddle => Color.Gray;
            public override Color MenuItemSelectedGradientEnd => Color.Gray;

            // 设置菜单项被点击时的背景色
            public override Color MenuItemPressedGradientBegin => Color.DimGray;
            public override Color MenuItemPressedGradientEnd => Color.DimGray;

            // 设置菜单边框颜色
            public override Color MenuBorder => Color.Black;

            public override Color MenuItemBorder => Color.Black;

        }
        class CustomRenderer : ToolStripProfessionalRenderer
        {
            public CustomRenderer() : base(new CustomColorTable()) { }

            protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
            {
                base.OnRenderMenuItemBackground(e);

                // 绘制选中项背景
                if (e.Item.Selected)
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.DarkGray), e.Item.ContentRectangle);
                }
                else
                {
                    e.Graphics.FillRectangle(new SolidBrush(Color.Black), e.Item.ContentRectangle);
                }
            }
            protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
            {
                // 绘制菜单背景
                e.Graphics.FillRectangle(new SolidBrush(Color.Black), e.AffectedBounds);
            }

            protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
            {
                // 设置文本颜色
                e.TextColor = Color.White;
                base.OnRenderItemText(e);
            }
        }
    }
}
