using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ExzamenVS.Models;

namespace ExzamenVS.Views
{
    public partial class NewProjectForm : Form, INewProjectView
    {
        public event EventHandler<EventArgs> OpenFolderEvent;
        public event EventHandler<AddProjectEventArgs> AddProjectEvent;

        private Project project { get; set; }

        public NewProjectForm()
        {
            InitializeComponent();
        }


        bool IView.ShowDialog()
        {
            return this.ShowDialog() == DialogResult.OK;

        }

        private void buttonFolder_Click(object sender, EventArgs e)
        {
            OpenFolderEvent?.Invoke(this,new EventArgs());
            
        }
        public void SetPathFolde(string path)
        {
            textBoxFolder.Text = path;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            Project Project = new Project() {
                Name=textBoxName.Text,
                Path=textBoxFolder.Text,
                

            };
            CS cS = new CS() {Name= "Program.cs" };
            cS.Path = Project.Path + "\\" + Project.Name+ "\\" + cS.Name;
            cS.Text = "using System;\n\nclass Program{\n\n  static void Main(){\n Console.WriteLine(\"Hello, world\");\n Console.Read();\n}\n}";
            
            Project.csfile.Add(cS) ;
            AddProjectEvent?.Invoke(this,new AddProjectEventArgs(){ project = Project});


        }


        private void textBoxNewProject_TextChanged(object sender, EventArgs e)
        {
            if (textBoxName.Text != "" && textBoxFolder.Text != "")
            {
                buttonOk.Enabled = true;
            }

        }
    }
}
