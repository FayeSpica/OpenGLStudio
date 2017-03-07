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
        /******************************************************************
        *   初始化
        *   
        *******************************************************************/
        //初始化场景、相机
        private void ini(object sender, EventArgs e)
        {
            //sceneControl1.Scene = new Scene();
            //sceneControl1.Scene.RenderBoundingVolumes = !sceneControl1.Scene.RenderBoundingVolumes;

            //  Add the root element to the tree.
            //AddElementToTree(sceneControl1.Scene.SceneContainer, treeView1.Nodes);
            //texture.Bind(sceneControl1.Scene.OpenGL);

            //sceneControl1.Scene.OpenGL.Enable(OpenGL.GL_TEXTURE_2D);
            //sceneControl1.Scene.OpenGL.Enable(OpenGL.GL_LIGHTING);
            //sceneControl1.Scene.OpenGL.Enable(OpenGL.GL_LIGHT0);
            //sceneControl1.Scene.OpenGL.Enable(OpenGL.GL_LIGHT1);
            //sceneControl1.Scene.OpenGL.Enable(OpenGL.GL_DEPTH_TEST);

            //camera = new CameraS(); ;
            //setCameraPosi(polar);
            //sceneControl1.Scene.OpenGL.LookAt(camera.Position.X, camera.Position.Y, camera.Position.Z,            /*设定摄像机位置，*/                                                                      /*这就是鼠标调整摄像机位置的结果*/
            //                       0.0, 0.0, 0.0,/*这三个参数是摄像机观察的点*/
            //                       0.0, 1.0, 0.0);
            //camera.bind(sceneControl1.Scene.OpenGL);
            var scene = this.sceneControl1.Scene;
            scene.SceneContainer.Children.Clear();
            scene.RenderBoundingVolumes = false;
            // 设置视角
            var camera = GetCamera();
            camera.Near = near;
            camera.Far = far;
            camera.Position = new Vertex(10f, 10f, 10f);//相机位置
            camera.Target = new Vertex(0f, 0, 0); //相机朝向
            camera.UpVector = new Vertex(0.000f, 0.000f, 1.000f);//相机法向

            InitElements(scene);
            //axisArcBallEffect.ArcBall.SetTranslate(
            //    12.490292456095853f, -1.5011389593859834f, 11.489356270615454f);
            //axisArcBallEffect.ArcBall.Scale = 0.001f;
            AddElementToTree(sceneControl1.Scene.SceneContainer, treeView1.Nodes);
        }
        private void InitElements(Scene scene)
        {
            // This implements free rotation(with translation and rotation).
            var camera = GetCamera();
            objectArcBallEffect = new ArcBallEffect(
                camera.Position.X, camera.Position.Y, camera.Position.Z,
                camera.Target.X, camera.Target.Y, camera.Target.Z,
                camera.UpVector.X, camera.UpVector.Y, camera.UpVector.Z);
            //objectRoot.AddEffect(objectArcBallEffect);
            axisArcBallEffect = new ArcBallEffect(camera.Position.X,
                camera.Position.Y, camera.Position.Z,
                camera.Target.X, camera.Target.Y, camera.Target.Z,
                camera.UpVector.X, camera.UpVector.Y, camera.UpVector.Z);
            //axisRoot.AddEffect(axisArcBallEffect);

            InitLight(sceneControl1.Scene.SceneContainer);
            var grid = new Grid() { Name = "网格" };
            grid.AddEffect(objectArcBallEffect);
            sceneControl1.Scene.SceneContainer.AddChild(grid);
            Axies axies = new Axies() { Name = "坐标系" };
            axies.AddEffect(objectArcBallEffect);
            sceneControl1.Scene.SceneContainer.AddChild(axies);
        }
        private void InitAxis(SceneElement parent)
        {
            var folder = new SharpGL.SceneGraph.Primitives.Folder() { Name = "Axis" };
            parent.AddChild(folder);

            // X轴
            Material red = new Material();
            red.Emission = Color.Red;
            red.Diffuse = Color.Red;

            Cylinder x1 = new Cylinder() { Name = "X1" };
            x1.BaseRadius = 0.05;
            x1.TopRadius = 0.05;
            x1.Height = 1.5;
            x1.Transformation.RotateY = 90f;
            x1.Material = red;
            folder.AddChild(x1);

            Cylinder x2 = new Cylinder() { Name = "X2" };
            x2.BaseRadius = 0.1;
            x2.TopRadius = 0;
            x2.Height = 0.2;
            x2.Transformation.TranslateX = 1.5f;
            x2.Transformation.RotateY = 90f;
            x2.Material = red;
            folder.AddChild(x2);

            // Y轴
            Material green = new Material();
            green.Emission = Color.Green;
            green.Diffuse = Color.Green;

            Cylinder y1 = new Cylinder() { Name = "Y1" };
            y1.BaseRadius = 0.05;
            y1.TopRadius = 0.05;
            y1.Height = 1.5;
            y1.Transformation.RotateX = -90f;
            y1.Material = green;
            folder.AddChild(y1);

            Cylinder y2 = new Cylinder() { Name = "Y2" };
            y2.BaseRadius = 0.1;
            y2.TopRadius = 0;
            y2.Height = 0.2;
            y2.Transformation.TranslateY = 1.5f;
            y2.Transformation.RotateX = -90f;
            y2.Material = green;
            folder.AddChild(y2);

            // Z轴
            Material blue = new Material();
            blue.Emission = Color.Blue;
            blue.Diffuse = Color.Blue;

            Cylinder z1 = new Cylinder() { Name = "Z1" };
            z1.BaseRadius = 0.05;
            z1.TopRadius = 0.05;
            z1.Height = 1.5;
            z1.Material = blue;
            folder.AddChild(z1);

            Cylinder z2 = new Cylinder() { Name = "Z2" };
            z2.BaseRadius = 0.1;
            z2.TopRadius = 0;
            z2.Height = 0.2;
            z2.Transformation.TranslateZ = 1.5f;
            z2.Material = blue;
            folder.AddChild(z2);
        }
        private void InitLight(SceneElement parent)
        {
            Light light1 = new Light()
            {
                Name = "Light 1",
                On = true,
                Position = new Vertex(-9, -9, 11),
                GLCode = OpenGL.GL_LIGHT0
            };
            Light light2 = new Light()
            {
                Name = "Light 2",
                On = true,
                Position = new Vertex(9, -9, 11),
                GLCode = OpenGL.GL_LIGHT1
            };
            Light light3 = new Light()
            {
                Name = "Light 3",
                On = true,
                Position = new Vertex(0, 15, 15),
                GLCode = OpenGL.GL_LIGHT2
            };
            var folder = new SharpGL.SceneGraph.Primitives.Folder() { Name = "光照" };

            parent.AddChild(folder);
            folder.AddChild(light1);
            folder.AddChild(light2);
            folder.AddChild(light3);
        }
        private void iniCameraEffect()
        {
            var camera = GetCamera();
            objectArcBallEffect = new ArcBallEffect(
                camera.Position.X, camera.Position.Y, camera.Position.Z,
                camera.Target.X, camera.Target.Y, camera.Target.Z,
                camera.UpVector.X, camera.UpVector.Y, camera.UpVector.Z);
            axisArcBallEffect = new ArcBallEffect(camera.Position.X,
                camera.Position.Y, camera.Position.Z,
                camera.Target.X, camera.Target.Y, camera.Target.Z,
                camera.UpVector.X, camera.UpVector.Y, camera.UpVector.Z);
            foreach (var e in sceneControl1.Scene.SceneContainer.Traverse())
            {
                if (!isPolygon(e))
                    e.AddEffect(axisArcBallEffect);
            }
        }
    }
}
