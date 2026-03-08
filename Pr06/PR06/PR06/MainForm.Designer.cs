namespace PR06
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
#region
        private void InitializeComponent()
        {
            this.btnAddDoctor = new System.Windows.Forms.Button();
            this.btnViewDoctors = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.labelTitle = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAddDoctor
            // 
            this.btnAddDoctor.Location = new System.Drawing.Point(127, 379);
            this.btnAddDoctor.Name = "btnAddDoctor";
            this.btnAddDoctor.Size = new System.Drawing.Size(200, 40);
            this.btnAddDoctor.TabIndex = 0;
            this.btnAddDoctor.Text = "Добавить врача";
            this.btnAddDoctor.Click += new System.EventHandler(this.Addbutton);
            // 
            // btnViewDoctors
            // 
            this.btnViewDoctors.Location = new System.Drawing.Point(127, 435);
            this.btnViewDoctors.Name = "btnViewDoctors";
            this.btnViewDoctors.Size = new System.Drawing.Size(200, 40);
            this.btnViewDoctors.TabIndex = 1;
            this.btnViewDoctors.Text = "Просмотр врачей";
            this.btnViewDoctors.Click += new System.EventHandler(this.Viewbutton);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(127, 530);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(200, 40);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Выход";
            this.btnExit.Click += new System.EventHandler(this.Exitbutton);
            // 
            // labelTitle
            // 
            this.labelTitle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelTitle.AutoSize = true;
            this.labelTitle.Location = new System.Drawing.Point(172, 43);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(104, 19);
            this.labelTitle.TabIndex = 0;
            this.labelTitle.Text = "Поликлиника";
            // 
            // pictureBox
            // 
            this.pictureBox.Image = global::PR06.Properties.Resources.pngegg;
            this.pictureBox.Location = new System.Drawing.Point(79, 99);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(285, 274);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox.TabIndex = 3;
            this.pictureBox.TabStop = false;
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(474, 603);
            this.Controls.Add(this.labelTitle);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnViewDoctors);
            this.Controls.Add(this.btnAddDoctor);
            this.Font = new System.Drawing.Font("Comic Sans MS", 10F);
            this.Name = "MainForm";
            this.Text = "Главная форма";
            this.Load += new System.EventHandler(this.MainForm_Load_1);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
#endregion

        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Button btnAddDoctor;
        private System.Windows.Forms.Button btnViewDoctors;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.PictureBox pictureBox;
    }
}
