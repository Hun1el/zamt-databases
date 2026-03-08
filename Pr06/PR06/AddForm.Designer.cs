namespace PR06
{
    partial class FormAdd
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
            this.btnBack = new System.Windows.Forms.Button();
            this.txtSurname = new System.Windows.Forms.TextBox();
            this.txtFirstName = new System.Windows.Forms.TextBox();
            this.txtMiddleName = new System.Windows.Forms.TextBox();
            this.txtSpeciality = new System.Windows.Forms.TextBox();
            this.txtExperience = new System.Windows.Forms.TextBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.labelSurname = new System.Windows.Forms.Label();
            this.labelFirstName = new System.Windows.Forms.Label();
            this.labelMiddleName = new System.Windows.Forms.Label();
            this.labelSpeciality = new System.Windows.Forms.Label();
            this.labelExperience = new System.Windows.Forms.Label();
            this.labelPhone = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(12, 342);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(100, 30);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "Назад";
            this.btnBack.Click += new System.EventHandler(this.Backbutton);
            // 
            // txtSurname
            // 
            this.txtSurname.Location = new System.Drawing.Point(150, 20);
            this.txtSurname.MaxLength = 55;
            this.txtSurname.Name = "txtSurname";
            this.txtSurname.Size = new System.Drawing.Size(200, 26);
            this.txtSurname.TabIndex = 0;
            this.txtSurname.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Surname);
            // 
            // txtFirstName
            // 
            this.txtFirstName.Location = new System.Drawing.Point(150, 60);
            this.txtFirstName.MaxLength = 55;
            this.txtFirstName.Name = "txtFirstName";
            this.txtFirstName.Size = new System.Drawing.Size(200, 26);
            this.txtFirstName.TabIndex = 1;
            this.txtFirstName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Firstname);
            // 
            // txtMiddleName
            // 
            this.txtMiddleName.Location = new System.Drawing.Point(150, 100);
            this.txtMiddleName.MaxLength = 55;
            this.txtMiddleName.Name = "txtMiddleName";
            this.txtMiddleName.Size = new System.Drawing.Size(200, 26);
            this.txtMiddleName.TabIndex = 2;
            this.txtMiddleName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Middlename);
            // 
            // txtSpeciality
            // 
            this.txtSpeciality.Location = new System.Drawing.Point(150, 151);
            this.txtSpeciality.MaxLength = 60;
            this.txtSpeciality.Name = "txtSpeciality";
            this.txtSpeciality.Size = new System.Drawing.Size(200, 26);
            this.txtSpeciality.TabIndex = 3;
            this.txtSpeciality.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Speciality);
            // 
            // txtExperience
            // 
            this.txtExperience.Location = new System.Drawing.Point(150, 191);
            this.txtExperience.MaxLength = 2;
            this.txtExperience.Name = "txtExperience";
            this.txtExperience.Size = new System.Drawing.Size(200, 26);
            this.txtExperience.TabIndex = 4;
            this.txtExperience.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Experience);
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(150, 232);
            this.txtPhone.MaxLength = 11;
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(200, 26);
            this.txtPhone.TabIndex = 5;
            this.txtPhone.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Phone);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(150, 274);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 30);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Добавить";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.buttonAdd);
            // 
            // labelSurname
            // 
            this.labelSurname.Location = new System.Drawing.Point(50, 23);
            this.labelSurname.Name = "labelSurname";
            this.labelSurname.Size = new System.Drawing.Size(100, 23);
            this.labelSurname.TabIndex = 1;
            this.labelSurname.Text = "Фамилия:";
            // 
            // labelFirstName
            // 
            this.labelFirstName.Location = new System.Drawing.Point(50, 63);
            this.labelFirstName.Name = "labelFirstName";
            this.labelFirstName.Size = new System.Drawing.Size(100, 23);
            this.labelFirstName.TabIndex = 2;
            this.labelFirstName.Text = "Имя:";
            // 
            // labelMiddleName
            // 
            this.labelMiddleName.Location = new System.Drawing.Point(31, 100);
            this.labelMiddleName.Name = "labelMiddleName";
            this.labelMiddleName.Size = new System.Drawing.Size(113, 40);
            this.labelMiddleName.TabIndex = 3;
            this.labelMiddleName.Text = "Отчество\r\n(при наличии):";
            this.labelMiddleName.Click += new System.EventHandler(this.labelMiddleName_Click);
            // 
            // labelSpeciality
            // 
            this.labelSpeciality.Location = new System.Drawing.Point(27, 151);
            this.labelSpeciality.Name = "labelSpeciality";
            this.labelSpeciality.Size = new System.Drawing.Size(123, 26);
            this.labelSpeciality.TabIndex = 4;
            this.labelSpeciality.Text = "Специальность:";
            this.labelSpeciality.Click += new System.EventHandler(this.labelSpeciality_Click);
            // 
            // labelExperience
            // 
            this.labelExperience.Location = new System.Drawing.Point(44, 191);
            this.labelExperience.Name = "labelExperience";
            this.labelExperience.Size = new System.Drawing.Size(100, 23);
            this.labelExperience.TabIndex = 5;
            this.labelExperience.Text = "Опыт (лет):";
            // 
            // labelPhone
            // 
            this.labelPhone.Location = new System.Drawing.Point(44, 232);
            this.labelPhone.Name = "labelPhone";
            this.labelPhone.Size = new System.Drawing.Size(100, 23);
            this.labelPhone.TabIndex = 6;
            this.labelPhone.Text = "Телефон:";
            // 
            // FormAdd
            // 
            this.ClientSize = new System.Drawing.Size(402, 400);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.labelSurname);
            this.Controls.Add(this.labelFirstName);
            this.Controls.Add(this.labelMiddleName);
            this.Controls.Add(this.labelSpeciality);
            this.Controls.Add(this.labelExperience);
            this.Controls.Add(this.labelPhone);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.txtExperience);
            this.Controls.Add(this.txtSpeciality);
            this.Controls.Add(this.txtMiddleName);
            this.Controls.Add(this.txtFirstName);
            this.Controls.Add(this.txtSurname);
            this.Font = new System.Drawing.Font("Comic Sans MS", 10F);
            this.Name = "FormAdd";
            this.Text = "Добавление врача";
            this.Load += new System.EventHandler(this.FormAdd_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.TextBox txtSurname;
        private System.Windows.Forms.TextBox txtFirstName;
        private System.Windows.Forms.TextBox txtMiddleName;
        private System.Windows.Forms.TextBox txtSpeciality;
        private System.Windows.Forms.TextBox txtExperience;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label labelSurname;
        private System.Windows.Forms.Label labelFirstName;
        private System.Windows.Forms.Label labelMiddleName;
        private System.Windows.Forms.Label labelSpeciality;
        private System.Windows.Forms.Label labelExperience;
        private System.Windows.Forms.Label labelPhone;
    }
}
