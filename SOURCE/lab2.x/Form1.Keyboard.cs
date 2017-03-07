using SharpGL.SceneGraph.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab2.x
{
    public partial class Form1
    {//键盘交互
        //键盘响应
        private void sceneControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (selectedSceneElement != null && isPolygon(selectedSceneElement))
            {
                //const float interval = 1;
                if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up)
                {
                    //this.objectArcBallEffect.ArcBall.GoUp(interval);
                    getPolygon(selectedSceneElement).Transformation.TranslateX++;
                }
                else if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down)
                {
                    //this.objectArcBallEffect.ArcBall.GoDown(interval);
                    getPolygon(selectedSceneElement).Transformation.TranslateX--;
                }
                else if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
                {
                    //this.objectArcBallEffect.ArcBall.GoLeft(interval);
                    getPolygon(selectedSceneElement).Transformation.TranslateY++;
                }
                else if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
                {
                    //this.objectArcBallEffect.ArcBall.GoRight(interval);
                    getPolygon(selectedSceneElement).Transformation.TranslateY--;
                }
                else if (e.KeyCode == Keys.OemMinus)
                {
                    //this.objectArcBallEffect.ArcBall.GoRight(interval);
                    getPolygon(selectedSceneElement).Transformation.TranslateZ--;
                }
                else if (e.KeyCode == Keys.Oemplus)
                {
                    //this.objectArcBallEffect.ArcBall.GoRight(interval);
                    getPolygon(selectedSceneElement).Transformation.TranslateZ++;
                }
            }
            else if (selectedSceneElement != null && isQuadric(selectedSceneElement))
            {
                //const float interval = 1;
                if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up)
                {
                    //this.objectArcBallEffect.ArcBall.GoUp(interval);
                    getQuadric(selectedSceneElement).Transformation.TranslateX++;
                }
                else if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down)
                {
                    //this.objectArcBallEffect.ArcBall.GoDown(interval);
                    getQuadric(selectedSceneElement).Transformation.TranslateX--;
                }
                else if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
                {
                    //this.objectArcBallEffect.ArcBall.GoLeft(interval);
                    getQuadric(selectedSceneElement).Transformation.TranslateY++;
                }
                else if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
                {
                    //this.objectArcBallEffect.ArcBall.GoRight(interval);
                    getQuadric(selectedSceneElement).Transformation.TranslateY--;
                }
                else if (e.KeyCode == Keys.OemMinus)
                {
                    //this.objectArcBallEffect.ArcBall.GoRight(interval);
                    getQuadric(selectedSceneElement).Transformation.TranslateZ--;
                }
                else if (e.KeyCode == Keys.Oemplus)
                {
                    //this.objectArcBallEffect.ArcBall.GoRight(interval);
                    getQuadric(selectedSceneElement).Transformation.TranslateZ++;
                }
            }
        }

    }
}
