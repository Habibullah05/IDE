using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExzamenVS.Models;

namespace ExzamenVS.Services
{
    interface IProjectService
    {
        string GetPahtFolder();
        void CreatingProjectFolder(Project project );
        System.Windows.Forms.TabPage AddPage(CS cS);
        void Run(CS cS);
        CS CreateCS();
        Project OpenProject(string path);
        CS OpenFile(string path);
        void ProjectSerialization(Project project);
        void SaveFile(CS cS);
        CompilerErrorCollection  Build(CS cS);
        Project RemoveFile(Project project, CS cS);
       
    }
}
