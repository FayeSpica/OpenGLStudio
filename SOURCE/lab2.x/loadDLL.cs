using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using System.IO;

namespace loadDLL
{
    /// <summary> 载入资源中的动态链接库(dll)文件
    /// </summary>
    static class LoadResourceDll
    {
        static Dictionary<string, Assembly> Dlls = new Dictionary<string, Assembly>();
        static Dictionary<string, object> Assemblies = new Dictionary<string, object>();

        static Assembly AssemblyResolve(object sender, ResolveEventArgs args)
        {
            //程序集
            Assembly ass;
            //获取加载失败的程序集的全名
            var assName = new AssemblyName(args.Name).FullName;
            //判断Dlls集合中是否有已加载的同名程序集
            if (Dlls.TryGetValue(assName, out ass) && ass != null)
            {
                Dlls[assName] = null;//如果有则置空并返回
                return ass;
            }
            else
            {
                throw new DllNotFoundException(assName);//否则抛出加载失败的异常
            }
        }

        /// <summary> 注册资源中的dll
        /// </summary>
        public static void RegistDLL()
        {
            if (!File.Exists(Environment.CurrentDirectory + "\\SharpGL.dll"))
            {
                byte[] Save = global::lab2.x.Properties.Resources.SharpGL;
                FileStream fsObj = new FileStream(Environment.CurrentDirectory + "\\SharpGL.dll", FileMode.CreateNew);
                fsObj.Write(Save, 0, Save.Length);
                fsObj.Close();
            }
            if (!File.Exists(Environment.CurrentDirectory + "\\SharpGL.SceneGraph.dll"))
            {
                byte[] Save = global::lab2.x.Properties.Resources.SharpGL2;
                FileStream fsObj = new FileStream(Environment.CurrentDirectory + "\\SharpGL.SceneGraph.dll", FileMode.CreateNew);
                fsObj.Write(Save, 0, Save.Length);
                fsObj.Close();
            }
            if (!File.Exists(Environment.CurrentDirectory + "\\SharpGL.Serialization.dll"))
            {
                byte[] Save = global::lab2.x.Properties.Resources.SharpGL1;
                FileStream fsObj = new FileStream(Environment.CurrentDirectory + "\\SharpGL.Serialization.dll", FileMode.CreateNew);
                fsObj.Write(Save, 0, Save.Length);
                fsObj.Close();
            }
            if (!File.Exists(Environment.CurrentDirectory + "\\SharpGL.WinForms.dll"))
            {
                byte[] Save = global::lab2.x.Properties.Resources.SharpGL3;
                FileStream fsObj = new FileStream(Environment.CurrentDirectory + "\\SharpGL.WinForms.dll", FileMode.CreateNew);
                fsObj.Write(Save, 0, Save.Length);
                fsObj.Close();
            }
        }


    }
}
