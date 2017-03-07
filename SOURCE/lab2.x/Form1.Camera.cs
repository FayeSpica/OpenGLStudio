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
        *   相机定义
        *   
        *******************************************************************/
        private ArcBallEffect objectArcBallEffect;
        private ArcBallEffect axisArcBallEffect;
        Camera
        camera;

        private LookAtCamera GetCamera()
        {
            return this.sceneControl1.Scene.CurrentCamera as LookAtCamera;
        }
    }
}
