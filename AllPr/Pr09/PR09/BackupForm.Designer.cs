using System.Drawing;
using System;
using System.Windows.Forms;

namespace PR09
{
    partial class BackupForm
    {
        private System.ComponentModel.IContainer components = null;
        private Button btnBackup;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.btnBackup = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnBackup
            // 
            this.btnBackup.Location = new System.Drawing.Point(62, 110);
            this.btnBackup.Name = "btnBackup";
            this.btnBackup.Size = new System.Drawing.Size(150, 40);
            this.btnBackup.TabIndex = 0;
            this.btnBackup.Text = "Создать резервную копию";
            this.btnBackup.Click += new System.EventHandler(this.btnBackup_Click);
            // 
            // BackupForm
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.btnBackup);
            this.Name = "BackupForm";
            this.Text = "Резервное копирование базы данных";
            this.ResumeLayout(false);

        }

        #endregion
    }
}
