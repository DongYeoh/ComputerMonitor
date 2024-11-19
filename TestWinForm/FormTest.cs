using System.Windows.Forms;

namespace TestWinForm
{
    
    public partial class FormTest : Form
    {
        private ContextMenuStrip contextMenu;
        public FormTest()
        {
            InitializeComponent();
            // ��ʼ�� ContextMenuStrip
            contextMenu = new ContextMenuStrip
            {
                Renderer = new CustomRenderer() // ʹ���Զ�����Ⱦ��
            };
            // ʹ���Զ���� CheckableToolStripMenuItem
            ToolStripMenuItem option1 = new ToolStripMenuItem("ѡ�� 1") { CheckOnClick = true };
            ToolStripMenuItem option2 = new ToolStripMenuItem("ѡ�� 2") { CheckOnClick = true };
            ToolStripMenuItem option3 = new ToolStripMenuItem("ѡ�� 3") { CheckOnClick = true };

            contextMenu.ShowCheckMargin = false;
            // ��Ӳ˵���
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
        // ���ò˵�����ɫ
        public override Color MenuStripGradientBegin => Color.Black;
        public override Color MenuStripGradientEnd => Color.Black;

        public override Color ImageMarginGradientBegin => Color.Black;
        public override Color ImageMarginGradientEnd => Color.Black;

        public override Color ImageMarginGradientMiddle => Color.Black;

        // ���ò˵����ɫ
        public override Color MenuItemSelected => Color.DarkGray;
        public override Color MenuItemSelectedGradientBegin => Color.Gray;
        public override Color MenuItemPressedGradientMiddle => Color.Gray;
        public override Color MenuItemSelectedGradientEnd => Color.Gray;

        // ���ò˵�����ʱ�ı���ɫ
        public override Color MenuItemPressedGradientBegin => Color.DimGray;
        public override Color MenuItemPressedGradientEnd => Color.DimGray;

        // ���ò˵��߿���ɫ
        public override Color MenuBorder => Color.Black;

        public override Color MenuItemBorder => Color.Black;

    }
    public class CustomRenderer : ToolStripProfessionalRenderer
    {
        public CustomRenderer() : base(new CustomColorTable()) { }

        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            base.OnRenderMenuItemBackground(e);

            // ����ѡ�����
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
            // ���Ʋ˵�����
            e.Graphics.FillRectangle(new SolidBrush(Color.Black), e.AffectedBounds);
        }
        
        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            // �����ı���ɫ
            e.TextColor = Color.White;
            base.OnRenderItemText(e);
        }
    }
}
