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
{
    public partial class Form1
    {
        private void 显示轮廓ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sceneControl1.Scene.RenderBoundingVolumes = !sceneControl1.Scene.RenderBoundingVolumes;
            显示轮廓ToolStripMenuItem.Checked = !显示轮廓ToolStripMenuItem.Checked;
        }
        private void 点模型ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            点模型ToolStripMenuItem1.Checked = true;
            线框模型ToolStripMenuItem1.Checked = false;
            面模型ToolStripMenuItem1.Checked = false;
            sceneControl1.OpenGL.PolygonMode(FaceMode.FrontAndBack, PolygonMode.Points);
        }
        private void 线框模型ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            点模型ToolStripMenuItem1.Checked = false;
            线框模型ToolStripMenuItem1.Checked = true;
            面模型ToolStripMenuItem1.Checked = false;
            sceneControl1.OpenGL.PolygonMode(FaceMode.FrontAndBack, PolygonMode.Lines);
        }
        private void 面模型ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            点模型ToolStripMenuItem1.Checked = false;
            线框模型ToolStripMenuItem1.Checked = false;
            面模型ToolStripMenuItem1.Checked = true;
            sceneControl1.OpenGL.PolygonMode(FaceMode.FrontAndBack, PolygonMode.Filled);
        }
        private void 显示边界ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sceneControl1.Scene.RenderBoundingVolumes = !sceneControl1.Scene.RenderBoundingVolumes;
            显示边界ToolStripMenuItem.Checked = !显示边界ToolStripMenuItem.Checked;
        }
        /******************************************************************
        *   treeView的右键菜单响应 完成度90%
        *   
        *******************************************************************/
        private void 多面体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sceneControl1.Scene.OpenGL.NewQuadric();
            Polygon 多面体 = new Polygon() { Name = "多面体" };
            多面体.Material = new Material();
            多面体.AddEffect(objectArcBallEffect);
            sceneControl1.Scene.SceneContainer.AddChild(多面体);
            treeView1.Nodes.Clear();
            AddElementToTree(sceneControl1.Scene.SceneContainer, treeView1.Nodes);
        }
        private void 立方体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sceneControl1.Scene.OpenGL.NewQuadric();
            Cube 立方 = new Cube() {Name ="立方体" };
            立方.Material = new Material();
            立方.AddEffect(objectArcBallEffect);
            sceneControl1.Scene.SceneContainer.AddChild(立方);
            treeView1.Nodes.Clear();
            AddElementToTree(sceneControl1.Scene.SceneContainer, treeView1.Nodes);
        }
        private void 球体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //SharpGL.SceneGraph.Quadrics.Sphere 球 = new SharpGL.SceneGraph.Quadrics.Sphere();
            SharpGL.SceneGraph.Quadrics.Sphere 球 = new SharpGL.SceneGraph.Quadrics.Sphere() { Name="球"};
            球.Material = new Material();
            球.TextureCoords = true;//允许纹理贴图
            球.AddEffect(objectArcBallEffect);
            sceneControl1.Scene.SceneContainer.AddChild(球);
            treeView1.Nodes.Clear();
            AddElementToTree(sceneControl1.Scene.SceneContainer, treeView1.Nodes);
        }
        private void 柱体ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SharpGL.SceneGraph.Quadrics.Cylinder 柱体 = new SharpGL.SceneGraph.Quadrics.Cylinder() { Name ="柱体"};
            柱体.Material = new Material();
            柱体.TextureCoords = true;//允许纹理贴图
            柱体.AddEffect(objectArcBallEffect);
            sceneControl1.Scene.SceneContainer.AddChild(柱体);
            treeView1.Nodes.Clear();
            AddElementToTree(sceneControl1.Scene.SceneContainer, treeView1.Nodes);
        }
        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (selectedSceneElement != null)
            {
                //sceneControl1.Scene.SceneContainer.RemoveChild(selectedSceneElement);
                selectedSceneElement.Parent.RemoveChild(selectedSceneElement);
                treeView1.Nodes.Clear();
                AddElementToTree(sceneControl1.Scene.SceneContainer, treeView1.Nodes);
                selectedSceneElement = null;
            }
        }
        private void 坐标系ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Axies axies = new Axies() { Name = "坐标系" };
            axies.AddEffect(axisArcBallEffect);
            sceneControl1.Scene.SceneContainer.AddChild(axies);
            treeView1.Nodes.Clear();
            AddElementToTree(sceneControl1.Scene.SceneContainer, treeView1.Nodes);
        }
        private void 网格ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Grid grid = new Grid() { Name = "网格" };
            grid.AddEffect(axisArcBallEffect);
            sceneControl1.Scene.SceneContainer.AddChild(grid);
            treeView1.Nodes.Clear();
            AddElementToTree(sceneControl1.Scene.SceneContainer, treeView1.Nodes);
        }
        private void 光照ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            sceneControl1.Scene.SceneContainer.AddChild(new
                        SharpGL.SceneGraph.Lighting.Light()
            { Name="光照"});
            treeView1.Nodes.Clear();
            AddElementToTree(sceneControl1.Scene.SceneContainer, treeView1.Nodes);
        }
        private void 聚光灯ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sceneControl1.Scene.SceneContainer.AddChild(new
                        SharpGL.SceneGraph.Lighting.Light());
            treeView1.Nodes.Clear();
            AddElementToTree(sceneControl1.Scene.SceneContainer, treeView1.Nodes);
        }
        private void 清空ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //sceneControl1.Scene = new Scene();
            sceneControl1.Scene = new Scene();
            sceneControl1.Scene.OpenGL.PolygonMode(FaceMode.FrontAndBack, PolygonMode.Filled);
            sceneControl1.Scene.RenderBoundingVolumes = false;
            treeView1.Nodes.Clear();
            AddElementToTree(sceneControl1.Scene.SceneContainer, treeView1.Nodes);
        }
        /******************************************************************
        *   文理定义、加载
        *   
        *******************************************************************/
        Texture texture = new Texture();
        private void 纹理ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            点模型ToolStripMenuItem1.Checked = false;
            线框模型ToolStripMenuItem1.Checked = false;
            面模型ToolStripMenuItem1.Checked = false;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Title = "选择图片文件";
            openFileDialog.Filter = "图片|*.bmp;*jpg;*jpeg;*gif";//SerializationEngine.Instance.Filter;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //  Destroy the existing texture.
                //texture.Destroy(sceneControl1.Scene.OpenGL);
                //  Create a new texture.
                //texture.Create(sceneControl1.Scene.OpenGL, openFileDialog.FileName);

                //  Redraw.
                //openGLControl1.Invalidate();
                // openGLControl1.OpenGL.PolygonMode(FaceMode.FrontAndBack, PolygonMode.Filled);
                //sceneControl1.OpenGL.PolygonMode(FaceMode.FrontAndBack, PolygonMode.Filled);
                // openGLControl1.OpenGL.Disable(OpenGL.GL_LIGHTING);
                if (selectedSceneElement != null && isPolygon(selectedSceneElement))
                {
                    Polygon polygon = getPolygon(selectedSceneElement);
                    //polygon.Material = new Material();
                    polygon.Material.Texture.Create(sceneControl1.Scene.OpenGL, openFileDialog.FileName);
                    //polygon.Material.Texture.Name = "12312";//openFileDialog.FileName.Substring(openFileDialog.FileName.LastIndexOf('\\')); ;
                }
                else if (selectedSceneElement != null && isQuadric(selectedSceneElement))
                {
                    Quadric quadric = getQuadric(selectedSceneElement);
                    quadric.Material.Texture.Create(sceneControl1.Scene.OpenGL, openFileDialog.FileName);
                }
                else
                {
                    statusStripStatusLabel1.Text = "Warming:当前未选中物体,请左键点击后再进行操作。";
                    statusStripStatusLabel1.ForeColor = Color.Red;
                }

            }
        }
        bool isPolygon(SceneElement selectedSceneElement)
        {
            Console.WriteLine(selectedSceneElement.GetType().ToString());
            if (selectedSceneElement.GetType().ToString() == "SharpGL.SceneGraph.Primitives.Polygon" || selectedSceneElement.GetType().ToString() == "SharpGL.SceneGraph.Primitives.Cube" )
                return true;
            return false;
        }
        bool isQuadric(SceneElement selectedSceneElement)
        {
            Console.WriteLine(selectedSceneElement.GetType().ToString());
            if (selectedSceneElement.GetType().ToString() == "SharpGL.SceneGraph.Quadrics.Cylinder" || selectedSceneElement.GetType().ToString() == "SharpGL.SceneGraph.Quadrics.Sphere")
                return true;
            return false;
        }
        Polygon getPolygon(SceneElement selectedSceneElement)
        {
                return selectedSceneElement as Polygon;
        }
        Quadric getQuadric(SceneElement selectedSceneElement)
        {
            return selectedSceneElement as Quadric;
        }
        /******************************************************************
        *   模型加载 完成度90%
        *   
        *******************************************************************/
        private void importPolygonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //打开模型文件对话框
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Multiselect = true;
            openDialog.Title = "选择obj文件";
            openDialog.Filter = "模型|*.obj";//SerializationEngine.Instance.Filter;
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                foreach (var str in openDialog.FileNames)
                {
                    ObjReader objReader = new ObjReader(str);
                    List<Polygon> polyList = new List<Polygon>(objReader.obj);
                    for (int i = 0; i < polyList.Count; i++)
                    {

                        //  Get the bounds of the polygon.
                        BoundingVolume boundingVolume = polyList[i].BoundingVolume;
                        float[] extent = new float[3];
                        polyList[i].BoundingVolume.GetBoundDimensions(out extent[0], out extent[1], out extent[2]);
                        //  Get the max extent.
                        float maxExtent = extent.Max();

                        //  Scale so that we are at most 10 units in size.
                        float scaleFactor = maxExtent > 10 ? 10.0f / maxExtent : 1;
                        polyList[i].Transformation.ScaleX = scaleFactor;
                        polyList[i].Transformation.ScaleY = scaleFactor;
                        polyList[i].Transformation.ScaleZ = scaleFactor;

                        polyList[i].AddEffect(objectArcBallEffect);
                        polyList[i].Material = new Material();
                        sceneControl1.Scene.SceneContainer.AddChild(polyList[i]);
                    }

                    treeView1.Nodes.Clear();
                    AddElementToTree(sceneControl1.Scene.SceneContainer, treeView1.Nodes);
                }
            }
        }
    }
}
