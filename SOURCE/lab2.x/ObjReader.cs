using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using System.Collections;
using System.IO;
namespace lab2.x
{//读取obj文件
    class ObjReader
    {
        public List<Polygon> obj;
        public StreamReader objReader;      //文件读取实例
        public ArrayList al;                //用于暂存文件中数据
        public ObjReader()
        {
            obj = new List<Polygon>();
        }
        public ObjReader(string path)
        {
            obj = new List<Polygon>();
            objReader = new StreamReader(path);
            al = new ArrayList();
            string LineItem = "";
            while (objReader.Peek()!=-1)
            {
                LineItem = objReader.ReadLine();
                al.Add(LineItem);
            }

            string filename = path.Substring(path.LastIndexOf('\\') + 1);
            string[] tempfilename = filename.Split(new String[] {".obj"}, System.StringSplitOptions.RemoveEmptyEntries);
            Polygon tmp = new Polygon() { Name= tempfilename[0] };

            int VerticsC = 0;
            int UVerticsC = 0;
            int NVerticsC = 0;
            int FacesC = 0;

            foreach (string i in al)
            {
                if(i.IndexOf('v')==0&&i.IndexOf(' ') == 1)//v 顶点
                {
                    //字符串分割
                    string[] tempArray = i.Split(new char[] { 'v', ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
                    float x = float.Parse(tempArray[0]);
                    float y = float.Parse(tempArray[1]);
                    float z = float.Parse(tempArray[2]);
                    tmp.Vertices.Add(new Vertex(x,y,z));
                    VerticsC++;
                }
                //vt 贴图顶点
                else if (i.IndexOf('v') == 0 && i.IndexOf('t') == 1 && i.IndexOf(' ') == 2)
                {
                    string[] tempArray = i.Split(new char[] { 'v', 't',' ' }, System.StringSplitOptions.RemoveEmptyEntries);
                    float u = float.Parse(tempArray[0]);
                    float v = float.Parse(tempArray[1]);
                    tmp.UVs.Add(new UV(u, v));
                    UVerticsC++;
                }
                else if (i.IndexOf('v') == 0 && i.IndexOf('n') == 1 && i.IndexOf(' ') == 2)
                {
                    string[] tempArray = i.Split(new char[] { 'v', 'n', ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
                    float x = float.Parse(tempArray[0]);
                    float y = float.Parse(tempArray[1]);
                    float z = float.Parse(tempArray[2]);
                    tmp.Normals.Add(new Vertex(x, y, z));
                    NVerticsC++;
                }
                else if (i.IndexOf('f') == 0 && i.IndexOf(' ') == 1)
                {
                    string[] tempArray = i.Split(new char[] { 'f','/', ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
                    /*3种情况
                     * 1 f Vertex1/Texture1/Normal1 Vertex2/Texture2/Normal2 Vertex3/Texture3/Normal3
                     * 2 f Vertex1/Normal1 Vertex2/Normal2 Vertex3/Normal3
                     * 3 f Vertex1 Vertex2 Vertex3
                    */
                    int x=0, y=0, z=0;
                    int tx = 0, ty = 0, tz = 0;
                    int nx = 0, ny = 0, nz = 0;
                    if (tempArray.Count() == 9)//1
                    {
                        x = Convert.ToInt32(tempArray[0]);
                        y = Convert.ToInt32(tempArray[0 + 3]);
                        z = Convert.ToInt32(tempArray[0 + 3 * 2]);
                        tx = Convert.ToInt32(tempArray[1]);
                        ty = Convert.ToInt32(tempArray[1 + 3]);
                        tz = Convert.ToInt32(tempArray[1 + 3 * 2]);
                        nx = Convert.ToInt32(tempArray[2]);
                        ny = Convert.ToInt32(tempArray[2 + 3]);
                        nz = Convert.ToInt32(tempArray[2 + 3 * 2]);
                    }
                    else if (tempArray.Count() == 6)//2
                    {
                        x = Convert.ToInt32(tempArray[0]);
                        y = Convert.ToInt32(tempArray[0 + 2]);
                        z = Convert.ToInt32(tempArray[0 + 2 * 2]);
                        tx = Convert.ToInt32(tempArray[0]);
                        ty = Convert.ToInt32(tempArray[0 + 2]);
                        tz = Convert.ToInt32(tempArray[0 + 2 * 2]);
                    }
                    else if (tempArray.Count() == 3)//3
                    {
                        x = Convert.ToInt32(tempArray[0]);
                        y = Convert.ToInt32(tempArray[0 + 1]);
                        z = Convert.ToInt32(tempArray[0 + 1 * 2]);
                    }                   
                    Face item = new Face();
                    item.Indices.Add(new Index(x - 1, tx - 1, nx - 1)); //顶点索引,贴图索引, 法向量索引
                    item.Indices.Add(new Index(y - 1, ty - 1, ny - 1));
                    item.Indices.Add(new Index(z - 1, tz - 1, nz - 1));
                    tmp.Faces.Add(item);//面信息
                    
                    FacesC++;
                }
            }
            Console.WriteLine("顶点:面数="+VerticsC+":"+FacesC);
            obj.Add(tmp);
        }
    }
}
