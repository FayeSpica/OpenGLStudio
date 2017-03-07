using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpGL;
using SharpGL.SceneGraph.Cameras;
using SharpGL.SceneGraph;
using System.Windows.Forms;

namespace lab2.x
{
    class WmGraph
    {
        class Sphere{//球体

        };
    }
    class CameraS : Camera
    {
        public CameraS()
        {
            m_posi = new Vertex(0, 0, 0);
            m_target = new Vertex(0, 0, -1f);
            m_up = new Vertex(0f, 1f, 0f);
        }
        public CameraS(Vertex Pos, Vertex Target, Vertex Up)
        {
            m_posi = Pos;
            m_target = Target;
            m_up = Up;
        }

        public Vertex GetPosi()
        {
            return m_posi;
        }

        public Vertex GetTarget()
        {
            return m_target;
        }

        public Vertex GetUp()
        {
            return m_up;
        }

        public void OnKeyBoard(KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    m_posi -= (m_target * StepScale);
                    break;
                case Keys.S:
                    m_posi += (m_target * StepScale);
                    break;
                case Keys.A:
                    Vertex Left = Cross(m_target, m_up);
                    Left.Normalize();
                    m_posi += Left * StepScale;
                    break;
                case Keys.D:
                    Vertex Right = Cross(m_target, m_up);
                    Right.Normalize();
                    m_posi -= Right * StepScale;
                    break;
            }
            this.Position = m_posi;//更新位置
            setCamera(gl);
        }

        public void setCamera(OpenGL gl)
        {
            gl.LookAt(m_posi.X, m_posi.Y, m_posi.Z, m_target.X, m_target.Y, m_target.Z, m_up.X, m_up.Y, m_up.Z);
        }

        public void bind(OpenGL gl)
        {
            this.gl = gl;
            setCamera(gl);
        }

        public override void TransformProjectionMatrix(OpenGL gl)
        {
            throw new NotImplementedException();
        }
        public Vertex m_posi;
        public Vertex m_target;
        public Vertex m_up;
        OpenGL gl;
        static float StepScale = 0.001f;

        Vertex Cross(Vertex a, Vertex b)
        {
            Vertex c = new Vertex();
            c.X = a.Y * b.Z - b.Y * a.Z;
            c.Y = a.Z * b.X - b.Z * a.X;
            c.Z = a.X * b.Y - b.X * a.Y;
            return c;
        }
    }
}
