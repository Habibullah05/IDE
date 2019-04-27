using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using ExzamenVS.Models;
using FastColoredTextBoxNS;
using AutocompleteMenuNS;
using Microsoft.CSharp;
using System.Xml;

namespace ExzamenVS.Services
{
    public class ProjectService : IProjectService
    {

        public void ProjectSerialization(Project project)
        {
            string xmlFile = project.Path  +"\\"+ project.Name + ".mysln";
            XmlTextWriter xmlWriter = new XmlTextWriter(xmlFile, null);

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("project");
            xmlWriter.WriteAttributeString("name", project.Name);

            foreach (var item in project.csfile)
            {
            xmlWriter.WriteStartElement("outputfile");
            xmlWriter.WriteAttributeString("path", item.Name);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteStartElement("csfile");
            xmlWriter.WriteAttributeString("path", item.Path);
            xmlWriter.WriteEndElement();
            }

            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
        }
       

        public void CreatingProjectFolder(Project project)
        {
            string pp = project.Path + "\\" + project.Name;
            Directory.CreateDirectory(pp);

            
            using (Stream fs = new FileStream(pp + "\\" + project.csfile[0].Name, FileMode.Create, FileAccess.ReadWrite))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(project.csfile[0].Text);
                }

            }



        }

        public string GetPahtFolder()
        {

            using (FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog())
            {
                folderBrowserDialog1.SelectedPath = @"C:\\";

                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    return folderBrowserDialog1.SelectedPath;

                }
                return null;
            }

        }

        public TabPage AddPage(CS cS)
        {
            TabPage tabPage = new TabPage(cS.Name);
            FastColoredTextBox textBox = new FastColoredTextBox();
            textBox.Dock = DockStyle.Fill;

            textBox.Language = Language.CSharp;
            textBox.Text = cS.Text;
           
            tabPage.Controls.Add(textBox);

            return tabPage;


        }

        public void SaveFile(CS cS)
        {
            using (Stream fs = new FileStream(cS.Path, FileMode.Open, FileAccess.ReadWrite))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(cS.Text);
                }

            }
        }

        public void Run(CS cS)
        {

            SaveFile(cS);
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters cp = new CompilerParameters();

            cp.GenerateExecutable = true;

            string path = (cS.ToString().TrimEnd(cS.Name.ToCharArray())) + "mymy.exe";
            cp.OutputAssembly = path;

            cp.GenerateInMemory = false;
            
            var result = provider.CompileAssemblyFromSource(cp, cS.Text);
            if (result.Errors.HasErrors)
            {
                MessageBox.Show("Errors:(" + result.Errors);
            }
            else
            {
                Process.Start(path);
               
            }
        }
        public CompilerErrorCollection Build(CS cS)
        {

            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            CompilerParameters cp = new CompilerParameters();
            string path = (cS.ToString().TrimEnd(cS.Name.ToCharArray())) + "mymy.exe";
            cp.GenerateExecutable = true;
            cp.OutputAssembly = path;
            cp.GenerateInMemory = false;
            cp.WarningLevel = 3;
            var result = codeProvider.CompileAssemblyFromSource(cp, cS.Text);

            if (result.Errors.HasErrors)
            {
                return result.Errors;
                                           
            }
            return null;

        }

        public CS CreateCS()
        {
            CS cS = new CS();
            Stream fs;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.Filter = "cs files (*.cs)|*.cs";
                saveFileDialog1.FilterIndex = 1;
                saveFileDialog1.RestoreDirectory = true;
                saveFileDialog1.AddExtension = true;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((fs = saveFileDialog1.OpenFile()) != null)
                    {
                        cS.Path = saveFileDialog1.FileName;
                        cS.Name = Path.GetFileName(saveFileDialog1.FileName);
                        cS.Text = "using System;\n\nclass "+cS.Name.Substring(0,cS.Name.Length-3)+"{\n\n \n}";
                        MessageBox.Show(Path.GetFileName(saveFileDialog1.FileName));


                        fs.Close();
                    }
                }


            }

            return cS;
        }

        public Project OpenProject(string path)
        {
             Project project = new Project();

            project.Path = Path.GetDirectoryName(path);
            string filename;
            using (XmlReader reader = XmlReader.Create(path))
            {
                int name = 0;
                int pathh = 0;
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        filename = reader.Name.ToString();
                        switch (filename)
                        {
                            case "project":
                                project.Name= reader.GetAttribute("name");
                                break;
                            case "outputfile":
                                CS cS = new CS() { Name = reader.GetAttribute("path") } ;
                                project.csfile.Add(cS) ;
                                
                                break;
                            case "csfile":
                                project.csfile[pathh].Path=reader.GetAttribute("path");
                                ++pathh;
                                break;
                        }
                    }
                }
            }

            foreach (CS item in project.csfile)
            {
                using (Stream fs = new FileStream(item.Path, FileMode.Open, FileAccess.ReadWrite))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        item.Text = sr.ReadToEnd();
                    }

                }
            }


                return project;

        }

        public CS OpenFile(string path)
        {
            CS cS = new CS();
            using (Stream fs = new FileStream(path, FileMode.Open, FileAccess.ReadWrite))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    cS.Name = Path.GetFileName(path);
                    cS.Path = path;
                    cS.Text = sr.ReadToEnd();
                    return cS;
                }

            }
        }

        public Project RemoveFile(Project project, CS cS)
        {
            project.csfile.Remove(cS);
            File.Delete(cS.Path);
            return project;
        }
    }
}
