using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ExzamenVS.Models;
using FastColoredTextBoxNS;
using Microsoft.CSharp;
using AutocompleteMenuNS;

namespace ExzamenVS.Views
{
    public partial class FormVS : Form, IFormVSView
    {
        public event EventHandler<EventArgs> NewProjectEvent;
        public event EventHandler<CSEventArgs> AddPageEvent;
        public event EventHandler<CSEventArgs> RunEvent;
        public event EventHandler<EventArgs> AddCSEvent;
        public event EventHandler<OpenFileEventArgs> OpenProjectEvent;
        public event EventHandler<SerealizationEventArgs> SerealizationEvent;
        public event EventHandler<OpenFileEventArgs> OpenFileEvent;
        public event EventHandler<CSEventArgs> SaveFileEvent;
        public event EventHandler<CSEventArgs> BuildEvent;
        public event EventHandler<RemoveFileEventArgs> RemoveFileEvent;

        TreeNode CNode;

        private Project project = new Project();

        public FormVS()
        {
            InitializeComponent();
        }


        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            NewProjectEvent?.Invoke(this, new EventArgs());
        }

        bool IView.ShowDialog()
        {
            return this.ShowDialog() == DialogResult.OK;
        }

        public void SetProject(Project project)
        {
            this.project = project;

            SerealizationEvent?.Invoke(this,new SerealizationEventArgs() { project=this.project});
            toolStripButtonBuild.Enabled = true;
            toolStripButtonRun.Enabled = true;
            tabControlCode.Enabled = true;
            treeView1.Nodes.Clear();
            tabControlCode.TabPages.Clear();
            var Node = treeView1.Nodes.Add(this.project.Name);
            Node.ContextMenuStrip = contextMenuStripProject;
            Node.SelectedImageKey = "cs_ico.png";
            Node.ImageKey = "cs_ico.png";
            CNode = Node;

            foreach (var item in this.project.csfile)
            {
                AddFileis(CNode, item);
                AddPageEvent?.Invoke(this, new CSEventArgs() { cS = item as CS });

            }

        }

        private void AddFileis(TreeNode node, CS cS)
        {

            TreeNode prodNode = new TreeNode(cS.Name)
            {
                ImageKey = "cs_ico.png",
                SelectedImageKey = "cs_ico.png",
                
            };
            prodNode.ContextMenuStrip = contextMenuStripEditCS;
            node.Nodes.Add(prodNode);

        }

        public void AddPage(TabPage tabPage)
        {
            string[] snippets = { "if(^)\n{\n}",
                "if(^)\n{\n}\nelse\n{\n}",
                "for(^;;)\n{\n}",
                "while(^)\n{\n}",
                "do${\n^}while();",
                "switch(^)\n{\n\tcase : break;\n}" };
            var items = new List<AutocompleteMenuNS.AutocompleteItem>();

            foreach (var item in snippets)
                items.Add(new AutocompleteMenuNS.SnippetAutocompleteItem(item) { ImageIndex = 1 });

            //set as autocomplete source
            autocompleteMenu1.SetAutocompleteItems(items);



            tabPage.ContextMenuStrip = contextMenuStripEdit;
            foreach (FastColoredTextBox item in tabPage.Controls)
            {
                if (item is FastColoredTextBox)
                {
                    item.TextChanged += Item_TextChanged;
                   // item.AutoCompleteBracketsList = autocompleteMenu1.ToString().ToCharArray();
                }

            }
            tabControlCode.TabPages.Add(tabPage);
            tabControlCode.SelectedTab = tabPage;

        }

        private void Item_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tabControlCode.SelectedTab.Text[0] != '*')
                tabControlCode.SelectedTab.Text = "* " + tabControlCode.SelectedTab.Text;
        }

        private void toolStripButtonRun_Click(object sender, EventArgs e)
        {

            for (int i = 0; i < tabControlCode.TabCount; i++)
            {
                if (tabControlCode.TabPages[i].Focus())
                {
                    foreach (FastColoredTextBox item in tabControlCode.TabPages[i].Controls)
                    {
                        if (item is FastColoredTextBox)
                        {
                            this.project.csfile[i].Text = item.Text;

                        }
                    }

                    RunEvent?.Invoke(this, new CSEventArgs() { cS = this.project.csfile[i] });
                }

            }



        }

        private void creatPToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void runToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OutPut();
            toolStripButtonRun.PerformClick();
        }


        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {

            toolStripButtonAddFile.PerformClick();

        }

        public void AddCS(CS cS)
        {

            this.project.csfile.Add(cS);
            SerealizationEvent?.Invoke(this, new SerealizationEventArgs() { project = this.project });
            AddPageEvent?.Invoke(this, new CSEventArgs() { cS = this.project.csfile[this.project.csfile.Count - 1] });
            AddFileis(CNode, this.project.csfile[this.project.csfile.Count - 1]);
        }

        private void OutPut()
        {
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters cp = new CompilerParameters();

            cp.GenerateExecutable = true;

            string path = project.Path + "mymy.exe";
            cp.OutputAssembly = path;

            cp.GenerateInMemory = false;
            MessageBox.Show(tabControlCode.SelectedIndex.ToString());

            TabPage tab = new TabPage();
            foreach (TabPage page in tabControlCode.TabPages)
            {
                if (page.Focus())
                {
                    tab = page;
                }

            }
            MessageBox.Show(tab.Text);
            var result = provider.CompileAssemblyFromSource(cp, tab.Text);
            //richTextBox1.Text += result.Output;
            // richTextBox2.Text += result.Errors;
            // richTextBox1.Text = cp.;
        }

        private void creatPToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            toolStripButtonAddProject.PerformClick();
        }

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonOpenProject.PerformClick();
        }

        //tyt commend
        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            FastColoredTextBox textBox = tabControlCode.SelectedTab.Controls[0] as FastColoredTextBox;

            textBox.CommentSelected();
               
          

        }

        private void newFileToolStripMenuItem_Click(object sender, EventArgs e)
        {

            toolStripButtonAddFile.PerformClick();
        }

        private void tabControlCode_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                for (int i = 0; i < tabControlCode.TabCount; ++i)
                {
                    Rectangle r = tabControlCode.GetTabRect(i);
                    if (r.Contains(e.Location) /* && it is the header that was clicked*/)
                    {
                        tabControlCode.SelectedIndex = i;
                        contextMenuStripRemovePage.Show(tabControlCode, e.Location);
                        break;
                    }
                }
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlCode.TabPages.Remove(tabControlCode.SelectedTab);
        }

        private void toolStripButtonOpenProject_Click(object sender, EventArgs e)
        {
            string filePath;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = @"C:\Users\Директор\source\repos\ExzamenVS\Projects";
                openFileDialog.Filter = "All files (*.*)|*.*|mysln files (*.mysln)|*.mysln";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    OpenProjectEvent?.Invoke(this, new OpenFileEventArgs() { path = filePath });
                }
            }
        }

        private void toolStripButtonAddFile_Click(object sender, EventArgs e)
        {
            AddCSEvent?.Invoke(this, new EventArgs());
        }

        private void toolStripButtonOpenFile_Click(object sender, EventArgs e)
        {
            string filePath;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = @"C:\Users\Директор\source\repos\ExzamenVS\Projects";
                openFileDialog.Filter = "All files (*.*)|*.*|cs files (*.cs)|*.cs";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    OpenFileEvent?.Invoke(this, new OpenFileEventArgs() { path = filePath });
                }
            }

        }

        private void projectToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!projectToolStripTreeCollaps.Checked)
            {
                splitContainer2.Panel1Collapsed = false;

            }
            else
            {

                splitContainer2.Panel1Collapsed = true;

            }

        }



        private void errorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!projectToolStripBlockCollaps.Checked)
            {
                splitContainer1.Panel2Collapsed = false;

            }
            else
            {

                splitContainer1.Panel2Collapsed = true;

            }
          

        }

        private void seveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonSave.PerformClick();

        }

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            Save_Name();
            for (int i = 0; i < tabControlCode.TabCount; i++)
            {
                if (tabControlCode.TabPages[i].Focus())
                {
                    foreach (FastColoredTextBox item in tabControlCode.TabPages[i].Controls)
                    {
                        if (item is FastColoredTextBox)
                        {
                            
                            this.project.csfile[i].Text = item.Text;
                            SaveFileEvent?.Invoke(this, new CSEventArgs() { cS = this.project.csfile[i] });
                            break;
                        }
                    }

                }
            }
        }

        private void Save_Name()
        {
            if (tabControlCode.SelectedTab.Text[0] == '*')
                tabControlCode.SelectedTab.Text = tabControlCode.SelectedTab.Text.Substring(1);
        }

        private void toolStripButtonAllSave_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < tabControlCode.TabCount; i++)
            {

                foreach (FastColoredTextBox item in tabControlCode.TabPages[i].Controls)
                {
                    if (item is FastColoredTextBox)
                    {
                        this.project.csfile[i].Text = item.Text;
                        SaveFileEvent?.Invoke(this, new CSEventArgs() { cS = this.project.csfile[i] });

                    }
                }


            }
        }

        private void saveAllFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonAllSave.PerformClick();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < tabControlCode.TabCount; i++)
            {
                if (tabControlCode.TabPages[i].Focus())
                {
                    foreach (FastColoredTextBox item in tabControlCode.TabPages[i].Controls)
                    {
                        if (item is FastColoredTextBox)
                        {
                            this.project.csfile[i].Text = item.Text;

            BuildEvent?.Invoke(this, new CSEventArgs() { cS = project.csfile[i] });
                        }
                    }

                }

            }

        }

        private void closeProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControlCode.Enabled = false;
            toolStripButtonBuild.Enabled = false;
            toolStripButtonRun.Enabled = false;
            treeView1.Nodes.Clear();
            tabControlCode.TabPages.Clear();
        }

        private void exidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void excludToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < CNode.Nodes.Count; i++)
            {
                if (CNode.Nodes[i].IsSelected)
                {
                    var tmp = project;
                    tmp.csfile.Remove(tmp.csfile[i]);
                    SetProject(tmp);
                }

            }


        }



        private void addNewFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonAddFile.PerformClick();
        }

        private void runToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            toolStripButtonRun.PerformClick();
        }

        private void buildToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            toolStripButtonBuild.PerformClick();
        }

        private void closeProjectToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            closeProjectToolStripMenuItem.PerformClick();
        }

        private void removeFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < CNode.Nodes.Count; i++)
            {
                if (CNode.Nodes[i].IsSelected)
                {
                    RemoveFileEvent?.Invoke(this, new RemoveFileEventArgs() { project = this.project, cS = this.project.csfile[i] });
                    //File.Delete(tmp.csfile[i].Path);
                   // SetProject(tmp);
                }

            }
        }



        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            FastColoredTextBox textBox = tabControlCode.SelectedTab.Controls[0] as FastColoredTextBox;
            if (textBox.SelectionLength > 0)
            {
                textBox.Cut();
            }
            textBox.Focus();
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            FastColoredTextBox textBox = tabControlCode.SelectedTab.Controls[0] as FastColoredTextBox;
            if (textBox.SelectionLength > 0)
            {
                textBox.Copy();
            }
            textBox.Focus();
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            FastColoredTextBox textBox = tabControlCode.SelectedTab.Controls[0] as FastColoredTextBox;
            textBox.Paste();
            textBox.Focus();
        }

      

        public void SetErrors(CompilerErrorCollection errorCollection)
        {
            foreach (CompilerError error in errorCollection)
            {

                tabControlBuildRun.SelectedIndex = 0;
                listViewCompileInfo.Columns.Add("Code");
                listViewCompileInfo.Columns.Add("Message");
                listViewCompileInfo.Columns.Add("Line");
                listViewCompileInfo.Columns.Add("File");
                listViewCompileInfo.Items.Add(new ListViewItem(new string[] { error.ErrorNumber, error.ErrorText, Convert.ToString(error.Line), error.FileName }));

            }
        }

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            toolStripButtonCut.PerformClick();
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            toolStripButtonCopy.PerformClick();
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            toolStripButtonPaste.PerformClick();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonCut.PerformClick();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonCopy.PerformClick();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripButtonPaste.PerformClick();
        }

        private void lineDownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FastColoredTextBox textBox = tabControlCode.SelectedTab.Controls[0] as FastColoredTextBox;

            textBox.MoveSelectedLinesDown();
        }

        private void lineUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FastColoredTextBox textBox = tabControlCode.SelectedTab.Controls[0] as FastColoredTextBox;

            textBox.MoveSelectedLinesUp();
        }
    }
}

