using System.Windows.Forms;

namespace TestWinForm
{
    
    public partial class FormTest : Form
    {
        private ContextMenuStrip contextMenu;
        public FormTest()
        {
            InitializeComponent();
            // 初始化 ContextMenuStrip
            contextMenu = new ContextMenuStrip
            {
                Renderer = new CustomRenderer() // 使用自定义渲染器
            };
            // 使用自定义的 CheckableToolStripMenuItem
            ToolStripMenuItem option1 = new ToolStripMenuItem("选项 1") { CheckOnClick = true };
            ToolStripMenuItem option2 = new ToolStripMenuItem("选项 2") { CheckOnClick = true };
            ToolStripMenuItem option3 = new ToolStripMenuItem("选项 3") { CheckOnClick = true };

            contextMenu.ShowCheckMargin = false;
            // 添加菜单项
            contextMenu.Items.Add(option1);
            contextMenu.Items.Add(option2);
            contextMenu.Items.Add(option3);
            contextMenu.Closing += ContextMenu_Closing;
           
        }
        
        private void ContextMenu_Closing(object? sender, ToolStripDropDownClosingEventArgs e)
        {
            if(e.CloseReason == ToolStripDropDownCloseReason.ItemClicked)
            {
                e.Cancel = true;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        protected override void OnMouseEnter(EventArgs e)
        {

        }

        protected override void OnMouseLeave(EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.contextMenu.Show(this.PointToScreen(this.button1.Location));
        }

        private void contextMenuStrip1_Closing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            if(e.CloseReason == ToolStripDropDownCloseReason.ItemClicked)
            {
                e.Cancel = true;
            }
        }
    }
    public class CustomColorTable : ProfessionalColorTable
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
    public class CustomRenderer : ToolStripProfessionalRenderer
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
