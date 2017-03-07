using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SharpGL;
using SharpGL.SceneGraph;
using SharpGL.SceneGraph.Cameras;
using SharpGL.SceneGraph.Primitives;
using SharpGL.Serialization;
using SharpGL.SceneGraph.Core;
using SharpGL.Enumerations;
using SharpGL.SceneGraph.Assets;
using SharpGL.SceneGraph.Effects;
using SharpGL.SceneGraph.Quadrics;
using SharpGL.SceneGraph.Lighting;
using System.Threading;

namespace lab2.x
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.sceneControl1.MouseWheel += sceneControl1_MouseWheel;
            iniCameraEffect();

            toolbarIni();

            //列表默认展开
            treeView1.ExpandAll();
            //多线程更新当前选中的物体
            new Thread(() =>
            {
                while (true)
                {
                    try {
                        if (selectedSceneElement != null)
                        {
                            toolStripSelectedElementStatus.ForeColor = Color.Black;
                            toolStripSelectedElementStatus.Text = "当前选中物体:" + selectedSceneElement;
                        }
                        else
                        {
                            toolStripSelectedElementStatus.Text = "未选中";
                            toolStripSelectedElementStatus.ForeColor = Color.IndianRed;
                        }
                    }
                    catch { }
                    Thread.Sleep(1000);
                }
            })
            { IsBackground = true }.Start();
        }
        //窗体大小变化时重绘OpenGL
        private void Form1_Resize(object sender, EventArgs e)
        {
            this.objectArcBallEffect.ArcBall.SetBounds(this.sceneControl1.Width, this.sceneControl1.Height);
            var gl = this.sceneControl1.OpenGL;
            var axis = gl.UnProject(50, 50, 0.1);
            axisArcBallEffect.ArcBall.SetTranslate(axis[0], axis[1], axis[2]);
            axisArcBallEffect.ArcBall.Scale = 0.001f;
        }
        private void 关于ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            关于 我 = new 关于();
            我.Show();
        }      
        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        /******************************************************************
        *   工具栏控制
        *   原本为鼠标响应设计的，仿照Unity5的界面最终没有用到，响应设计太复杂
        *******************************************************************/
        void toolbarIni()
        {
            toolStripButton1.Checked = true;
        }
        void toolBarChanger(int i)
        {
            switch (i)
            {
                case 1:
                    toolStripButton1.Checked = true;
                    toolStripButton2.Checked = false;
                    toolStripButton3.Checked = false;
                    toolStripButton4.Checked = false;
                    break;
                case 2:
                    toolStripButton1.Checked = false;
                    toolStripButton2.Checked = true;
                    toolStripButton3.Checked = false;
                    toolStripButton4.Checked = false;
                    break;
                case 3:
                    toolStripButton1.Checked = false;
                    toolStripButton2.Checked = false;
                    toolStripButton3.Checked = true;
                    toolStripButton4.Checked = false;
                    break;
                case 4:
                    toolStripButton1.Checked = false;
                    toolStripButton2.Checked = false;
                    toolStripButton3.Checked = false;
                    toolStripButton4.Checked = true;
                    break;
            }
        }
        private void OnToolBarChange1(object sender, EventArgs e)
        {
            toolBarChanger(1);
        }
        private void OnToolBarChange2(object sender, EventArgs e)
        {
            toolBarChanger(2);
        }
        private void OnToolBarChange3(object sender, EventArgs e)
        {
            toolBarChanger(3);
        }
        private void OnToolBarChange4(object sender, EventArgs e)
        {
            toolBarChanger(4);
        }
        private void OnToolBarSelectedChanged(object sender, EventArgs e)
        {
            Image im;
            if (toolStripButton1.Checked == true)
            {
                im = (Image)Properties.Resources.ResourceManager.GetObject("selected");
            }
            else
            {
                im = (Image)Properties.Resources.ResourceManager.GetObject("select");
            }
            toolStripButton1.Image = im;
        }
        private void OnToolBarTransformedChanged(object sender, EventArgs e)
        {
            Image im;
            if (toolStripButton2.Checked == true)
            {
                im = (Image)Properties.Resources.ResourceManager.GetObject("transformed");
            }
            else
            {
                im = (Image)Properties.Resources.ResourceManager.GetObject("transform");
            }
            toolStripButton2.Image = im;
        }
        private void OnToolBarRotatedChanged(object sender, EventArgs e)
        {
            Image im;
            if (toolStripButton3.Checked == true)
            {
                im = (Image)Properties.Resources.ResourceManager.GetObject("rotated");
            }
            else
            {
                im = (Image)Properties.Resources.ResourceManager.GetObject("rotate");
            }
            toolStripButton3.Image = im;
        }
        private void OnToolBarScaledChanged(object sender, EventArgs e)
        {
            Image im;
            if (toolStripButton4.Checked == true)
            {
                im = (Image)Properties.Resources.ResourceManager.GetObject("scale");
            }
            else
            {
                im = (Image)Properties.Resources.ResourceManager.GetObject("scaled");
            }
            toolStripButton4.Image = im;
        }
        /******************************************************************
        *   Draw回调函数
        *   
        *******************************************************************/
        private void sceneControl1_OpenGLDraw(object sender, RenderEventArgs args)
        {
            ////更新光照信息 光照存在bug，暂未修复
            //sceneControl1.Scene.OpenGL.Enable(OpenGL.GL_LIGHT0);
            //sceneControl1.Scene.OpenGL.Enable(OpenGL.GL_LIGHT1);
            //sceneControl1.Scene.OpenGL.Enable(OpenGL.GL_LIGHT2);
            //sceneControl1.Scene.OpenGL.Enable(OpenGL.GL_LIGHT3);
            //sceneControl1.Scene.OpenGL.Enable(OpenGL.GL_LIGHT4);
            //sceneControl1.Scene.OpenGL.Enable(OpenGL.GL_LIGHT5);
            //sceneControl1.Scene.OpenGL.Enable(OpenGL.GL_LIGHT6);
            //sceneControl1.Scene.OpenGL.Enable(OpenGL.GL_LIGHT7);
        }
    }
}