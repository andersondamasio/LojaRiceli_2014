using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Text;

namespace Loja.CustomWebControls
{
    public class DynamicTreeView : TreeView
    {
        public DynamicTreeView()
        {

        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!Page.IsPostBack)
            {
                this.Nodes.Clear();
                AddPagesToTreeView(null, System.Web.HttpContext.Current.Server.MapPath(""));
            }
        }
        private bool HasAspxIn(string path)
        {
            return System.IO.Directory.GetFiles(path, "*.aspx", System.IO.SearchOption.AllDirectories).Count() > 0;
        }
        private string SplitWordsByUpperCase(string s)
        {
            StringBuilder sb = new StringBuilder();
            bool first = true;
            
            foreach (Char c in s.ToCharArray())
            {
                if (first)
                {
                    sb.Append(c.ToString().ToUpper());

                }
                else
                {
                    if (Char.IsUpper(c))
                    {
                        sb.Append(" ");
                    }
                    sb.Append(c);
                }
                first = false;
            }
            return sb.ToString();
        }
        private int AddPagesToTreeView(TreeNode node, string path)
        {
            string[] files = System.IO.Directory.GetFiles(path, "*.aspx", System.IO.SearchOption.TopDirectoryOnly);
            string[] directories = System.IO.Directory.GetDirectories(path);
            foreach (string directory in directories)
            {
                if (HasAspxIn(directory))
                {
                    TreeNode tn = new TreeNode();
                    tn.Value = directory;
                    tn.Text = SplitWordsByUpperCase(directory.Substring(directory.LastIndexOf(@"\") + 1));

                    if (node == null)
                    {
                        this.Nodes.Add(tn);
                    }
                    else
                    {
                        node.ChildNodes.Add(tn);
                    }
                    AddPagesToTreeView(tn, directory);
                }
            }
            foreach (string file in files)
            {
                TreeNode tn = new TreeNode();
                tn.Value = file;
                tn.Text = SplitWordsByUpperCase(file.Substring(file.LastIndexOf(@"\") + 1).Replace(".aspx", ""));
                tn.NavigateUrl = this.ResolveClientUrl(file.Replace(System.Web.HttpContext.Current.Server.MapPath(""), ""));
                if (node == null)
                {
                    this.Nodes.Add(tn);
                }
                else
                {
                    node.ChildNodes.Add(tn);
                }
            }
            return files.Count();
        }
    }
}