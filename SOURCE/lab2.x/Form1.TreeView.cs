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
        //加载场景元素到Tree
        private void AddElementToTree(SceneElement sceneElement, TreeNodeCollection nodes)
        {
            foreach (var element in sceneElement.Children)
                AddElementToTreeSub(element, nodes);

            treeView1.ExpandAll();
        }
        private void AddElementToTreeSub(SceneElement sceneElement, TreeNodeCollection nodes)
        {
            TreeNode newNode = new TreeNode()
            {
                Text = sceneElement.Name,
                Tag = sceneElement
            };
            nodes.Add(newNode);

            //  递归添加
            foreach (var element in sceneElement.Children)
                AddElementToTreeSub(element, newNode.Nodes);
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //if (SelectedSceneElement != null)
            //{
            //    SelectedSceneElement.RemoveEffect(objectArcBallEffect);
            //}
            SelectedSceneElement = e.Node.Tag as SceneElement;
            //SelectedSceneElement.AddEffect(objectArcBallEffect);
        }

        //重绘树
        private void treeView1_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            treeView1.Nodes.Clear();
            AddElementToTree(sceneControl1.Scene.SceneContainer, treeView1.Nodes);
        }
        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            treeView1.Nodes.Clear();
            AddElementToTree(sceneControl1.Scene.SceneContainer, treeView1.Nodes);
        }
        private void OnSelectedSceneElementChanged()
        {
            propertyGrid1.SelectedObject = SelectedSceneElement;
        }
        private SceneElement selectedSceneElement = null;
        public SceneElement SelectedSceneElement
        {
            get { return selectedSceneElement; }
            set
            {
                selectedSceneElement = value;
                OnSelectedSceneElementChanged();
            }
        }
        private void treeView1_Click(object sender, EventArgs e)
        {
            statusStripStatusLabel1.Text = "就绪";
            statusStripStatusLabel1.ForeColor = Color.Black;
        }
    }
}
