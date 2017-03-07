using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.x
{
    public partial class Form1
    {
        private void openGL场景设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new MyPropertyGrid("场景设置", this.sceneControl1)).Show();
        }

        private void 列表设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new MyPropertyGrid("列表设置", this.treeView1)).Show();
        }

        private void 窗体设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new MyPropertyGrid("窗体设置", this)).Show();
        }

        private void 属性栏设置ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (new MyPropertyGrid("属性栏设置", this.propertyGrid1)).Show();
        }
    }
}
