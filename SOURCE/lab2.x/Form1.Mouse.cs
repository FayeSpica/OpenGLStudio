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

namespace lab2.x
{//鼠标交互
    public partial class Form1
    {
        /******************************************************************
        *   鼠标响应事件
        *   
        *******************************************************************/
        //拖拽操作
        private void openGLControl_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.All;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        private void openGLControl_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var item = (string[])e.Data.GetData(DataFormats.FileDrop);
                UpdateTextureImage(item[0]);
            }
        }
        private void UpdateTextureImage(string filename)
        {
            if (selectedSceneElement != null && isPolygon(selectedSceneElement))
            {
                Polygon polygon = getPolygon(selectedSceneElement);
                //polygon.Material = new Material();
                polygon.Material.Texture.Create(sceneControl1.Scene.OpenGL, filename);
                statusStripStatusLabel1.Text = "Tips:拖入纹理前请选中要添加纹理的对象";
            }
            else if (selectedSceneElement != null && isQuadric(selectedSceneElement))
            {
                Quadric quadric = getQuadric(selectedSceneElement);
                quadric.Material.Texture.Create(sceneControl1.Scene.OpenGL, filename);
                statusStripStatusLabel1.Text = "Tips:拖入纹理前请选中要添加纹理的对象";
            }
            else
            {
                statusStripStatusLabel1.Text = "Warming:当前未选中物体,请左键点击后再进行操作。";
                statusStripStatusLabel1.ForeColor = Color.Red;
            }
            //var gl = this.sceneControl1.OpenGL;
            //if (this.texture != null)
            //{
            //    this.texture.Destroy(gl);
            //    this.texture.Create(gl, filename);
            //}
        }

        //滚轮
        void sceneControl1_MouseWheel(object sender, MouseEventArgs e)
        {
            objectArcBallEffect.ArcBall.Scale -= e.Delta * 0.001f;
            axisArcBallEffect.ArcBall.Scale -= e.Delta * 0.001f;
        }
        const float near = 0.01f;
        const float far = 10000;
        private bool mouseDownFlag = false;

        //单击选中 完成度100% + 鼠标控制相机视角完成度90%
        private void sceneControl1_MouseDown(object sender, MouseEventArgs e)
        {
            this.mouseDownFlag = true;
            objectArcBallEffect.ArcBall.SetBounds(this.sceneControl1.Width, this.sceneControl1.Height);
            objectArcBallEffect.ArcBall.MouseDown(e.X, e.Y);
            axisArcBallEffect.ArcBall.SetBounds(this.sceneControl1.Width, this.sceneControl1.Height);
            axisArcBallEffect.ArcBall.MouseDown(e.X, e.Y);

            var itemsHit = sceneControl1.Scene.DoHitTest(e.X, e.Y);

            if (itemsHit.Count() > 0)
            {
                SelectedSceneElement = itemsHit.First();
                //Console.WriteLine(SelectedSceneElement.Name);
                OnSelectedSceneElementChanged();
            }
        }

        private void sceneControl1_MouseUp(object sender, MouseEventArgs e)
        {
            this.mouseDownFlag = false;
            objectArcBallEffect.ArcBall.MouseUp(e.X, e.Y);
            axisArcBallEffect.ArcBall.MouseUp(e.X, e.Y);
        }

        private void sceneControl1_MouseMove(object sender, MouseEventArgs e)
        {
            objectArcBallEffect.ArcBall.MouseMove(e.X, e.Y);
            axisArcBallEffect.ArcBall.MouseMove(e.X, e.Y);
        }
    }
}
