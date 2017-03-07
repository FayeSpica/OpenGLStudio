using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab2.x
{
    public partial class MyPropertyGrid : Form
    {
        public MyPropertyGrid(string name,object obj = null)
        {
            InitializeComponent();
            DisplayObject(obj);
            this.Text = name;
        }
        public void DisplayObject(object obj)
        {
            if (!this.IsDisposed)
            {
                this.propertyGrid1.SelectedObject = obj;
            }
        }
    }
}
