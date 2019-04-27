namespace ExzamenVS.Views
{
    partial class NewProjectForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBoxNewProject = new System.Windows.Forms.GroupBox();
            this.checkBoxCreateSubdrectory = new System.Windows.Forms.CheckBox();
            this.buttonFolder = new System.Windows.Forms.Button();
            this.textBoxFolder = new System.Windows.Forms.TextBox();
            this.labelFolder = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelName = new System.Windows.Forms.Label();
            this.buttonCanel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.groupBoxNewProject.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxNewProject
            // 
            this.groupBoxNewProject.Controls.Add(this.checkBoxCreateSubdrectory);
            this.groupBoxNewProject.Controls.Add(this.buttonFolder);
            this.groupBoxNewProject.Controls.Add(this.textBoxFolder);
            this.groupBoxNewProject.Controls.Add(this.labelFolder);
            this.groupBoxNewProject.Controls.Add(this.textBoxName);
            this.groupBoxNewProject.Controls.Add(this.labelName);
            this.groupBoxNewProject.Location = new System.Drawing.Point(12, 12);
            this.groupBoxNewProject.Name = "groupBoxNewProject";
            this.groupBoxNewProject.Size = new System.Drawing.Size(235, 205);
            this.groupBoxNewProject.TabIndex = 0;
            this.groupBoxNewProject.TabStop = false;
            this.groupBoxNewProject.Text = "New Project";
            
            // 
            // checkBoxCreateSubdrectory
            // 
            this.checkBoxCreateSubdrectory.AutoSize = true;
            this.checkBoxCreateSubdrectory.Checked = true;
            this.checkBoxCreateSubdrectory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxCreateSubdrectory.Location = new System.Drawing.Point(6, 174);
            this.checkBoxCreateSubdrectory.Name = "checkBoxCreateSubdrectory";
            this.checkBoxCreateSubdrectory.Size = new System.Drawing.Size(115, 17);
            this.checkBoxCreateSubdrectory.TabIndex = 5;
            this.checkBoxCreateSubdrectory.Text = "Create subdrectory";
            this.checkBoxCreateSubdrectory.UseVisualStyleBackColor = true;
            // 
            // buttonFolder
            // 
            this.buttonFolder.Location = new System.Drawing.Point(6, 128);
            this.buttonFolder.Name = "buttonFolder";
            this.buttonFolder.Size = new System.Drawing.Size(225, 24);
            this.buttonFolder.TabIndex = 4;
            this.buttonFolder.Text = "Choose folder";
            this.buttonFolder.UseVisualStyleBackColor = true;
            this.buttonFolder.Click += new System.EventHandler(this.buttonFolder_Click);
            // 
            // textBoxFolder
            // 
            this.textBoxFolder.Enabled = false;
            this.textBoxFolder.Location = new System.Drawing.Point(6, 102);
            this.textBoxFolder.Name = "textBoxFolder";
            this.textBoxFolder.Size = new System.Drawing.Size(225, 20);
            this.textBoxFolder.TabIndex = 3;
            this.textBoxFolder.TextChanged += new System.EventHandler(this.textBoxNewProject_TextChanged);
            // 
            // labelFolder
            // 
            this.labelFolder.AutoSize = true;
            this.labelFolder.Location = new System.Drawing.Point(15, 82);
            this.labelFolder.Name = "labelFolder";
            this.labelFolder.Size = new System.Drawing.Size(39, 13);
            this.labelFolder.TabIndex = 2;
            this.labelFolder.Text = "Folder:";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(6, 41);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(225, 20);
            this.textBoxName.TabIndex = 1;
            this.textBoxName.TextChanged += new System.EventHandler(this.textBoxNewProject_TextChanged);
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(15, 25);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(38, 13);
            this.labelName.TabIndex = 0;
            this.labelName.Text = "Name:";
            // 
            // buttonCanel
            // 
            this.buttonCanel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCanel.Location = new System.Drawing.Point(12, 223);
            this.buttonCanel.Name = "buttonCanel";
            this.buttonCanel.Size = new System.Drawing.Size(113, 24);
            this.buttonCanel.TabIndex = 1;
            this.buttonCanel.Text = "Canel";
            this.buttonCanel.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Enabled = false;
            this.buttonOk.Location = new System.Drawing.Point(134, 223);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(113, 24);
            this.buttonOk.TabIndex = 2;
            this.buttonOk.Text = "Ok";
            this.buttonOk.UseVisualStyleBackColor = true;
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // NewProjectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(256, 254);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonCanel);
            this.Controls.Add(this.groupBoxNewProject);
            this.Name = "NewProjectForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "New Project";
            this.groupBoxNewProject.ResumeLayout(false);
            this.groupBoxNewProject.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxNewProject;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelFolder;
        private System.Windows.Forms.Button buttonFolder;
        private System.Windows.Forms.TextBox textBoxFolder;
        private System.Windows.Forms.CheckBox checkBoxCreateSubdrectory;
        private System.Windows.Forms.Button buttonCanel;
        private System.Windows.Forms.Button buttonOk;
    }
}