namespace WindowsFormsApplication
{
    partial class AddForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.qBack = new System.Windows.Forms.Button();
            this.qAddBooks = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.qCountBook = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.qYearBook = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.qIzdatBook = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.qAuthorBook = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.qNameBook = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.qNumberBook = new System.Windows.Forms.TextBox();
            this.dataGridViewBooks = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBooks)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(847, 472);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.qBack);
            this.tabPage1.Controls.Add(this.qAddBooks);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.qCountBook);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.qYearBook);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.qIzdatBook);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.qAuthorBook);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.qNameBook);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.qNumberBook);
            this.tabPage1.Controls.Add(this.dataGridViewBooks);
            this.tabPage1.Location = new System.Drawing.Point(4, 33);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(839, 435);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Книги";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // qBack
            // 
            this.qBack.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.qBack.Location = new System.Drawing.Point(6, 380);
            this.qBack.Name = "qBack";
            this.qBack.Size = new System.Drawing.Size(147, 41);
            this.qBack.TabIndex = 16;
            this.qBack.Text = "Назад";
            this.qBack.UseVisualStyleBackColor = true;
            this.qBack.Click += new System.EventHandler(this.qBack_Click);
            // 
            // qAddBooks
            // 
            this.qAddBooks.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.qAddBooks.Location = new System.Drawing.Point(683, 380);
            this.qAddBooks.Name = "qAddBooks";
            this.qAddBooks.Size = new System.Drawing.Size(147, 41);
            this.qAddBooks.TabIndex = 15;
            this.qAddBooks.Text = "ДОБАВИТЬ";
            this.qAddBooks.UseVisualStyleBackColor = true;
            this.qAddBooks.Click += new System.EventHandler(this.qAddBooks_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(99, 330);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(159, 24);
            this.label6.TabIndex = 13;
            this.label6.Text = "количество книг";
            // 
            // qCountBook
            // 
            this.qCountBook.Location = new System.Drawing.Point(6, 327);
            this.qCountBook.MaxLength = 3;
            this.qCountBook.Name = "qCountBook";
            this.qCountBook.Size = new System.Drawing.Size(87, 29);
            this.qCountBook.TabIndex = 12;
            this.qCountBook.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.qCountBook_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(100, 295);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(122, 24);
            this.label5.TabIndex = 11;
            this.label5.Text = "год издания";
            // 
            // qYearBook
            // 
            this.qYearBook.Location = new System.Drawing.Point(6, 292);
            this.qYearBook.MaxLength = 4;
            this.qYearBook.Name = "qYearBook";
            this.qYearBook.Size = new System.Drawing.Size(87, 29);
            this.qYearBook.TabIndex = 10;
            this.qYearBook.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.qYearBook_KeyPress);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(679, 330);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 24);
            this.label4.TabIndex = 9;
            this.label4.Text = "издательство";
            // 
            // qIzdatBook
            // 
            this.qIzdatBook.Location = new System.Drawing.Point(291, 327);
            this.qIzdatBook.MaxLength = 50;
            this.qIzdatBook.Name = "qIzdatBook";
            this.qIzdatBook.Size = new System.Drawing.Size(382, 29);
            this.qIzdatBook.TabIndex = 8;
            this.qIzdatBook.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.qIzdatBook_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(679, 295);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 24);
            this.label3.TabIndex = 7;
            this.label3.Text = "автор книги";
            // 
            // qAuthorBook
            // 
            this.qAuthorBook.Location = new System.Drawing.Point(291, 292);
            this.qAuthorBook.MaxLength = 50;
            this.qAuthorBook.Name = "qAuthorBook";
            this.qAuthorBook.Size = new System.Drawing.Size(382, 29);
            this.qAuthorBook.TabIndex = 6;
            this.qAuthorBook.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.qAuthorBook_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(679, 260);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 24);
            this.label2.TabIndex = 5;
            this.label2.Text = "название книги";
            // 
            // qNameBook
            // 
            this.qNameBook.Location = new System.Drawing.Point(291, 257);
            this.qNameBook.MaxLength = 60;
            this.qNameBook.Name = "qNameBook";
            this.qNameBook.Size = new System.Drawing.Size(382, 29);
            this.qNameBook.TabIndex = 4;
            this.qNameBook.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.qNameBook_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(99, 260);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 24);
            this.label1.TabIndex = 3;
            this.label1.Text = "номер книги";
            // 
            // qNumberBook
            // 
            this.qNumberBook.Location = new System.Drawing.Point(6, 257);
            this.qNumberBook.MaxLength = 4;
            this.qNumberBook.Name = "qNumberBook";
            this.qNumberBook.Size = new System.Drawing.Size(87, 29);
            this.qNumberBook.TabIndex = 2;
            this.qNumberBook.TextChanged += new System.EventHandler(this.qNumberBook_TextChanged);
            this.qNumberBook.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.qNumberBook_KeyPress);
            // 
            // dataGridViewBooks
            // 
            this.dataGridViewBooks.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewBooks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewBooks.Location = new System.Drawing.Point(6, 6);
            this.dataGridViewBooks.Name = "dataGridViewBooks";
            this.dataGridViewBooks.Size = new System.Drawing.Size(827, 245);
            this.dataGridViewBooks.TabIndex = 1;
            // 
            // AddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 488);
            this.ControlBox = false;
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.Name = "AddForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "АСУ Библиотека | Добавление данных";
            this.Load += new System.EventHandler(this.AddForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewBooks)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button qAddBooks;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox qCountBook;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox qYearBook;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox qIzdatBook;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox qAuthorBook;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox qNameBook;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox qNumberBook;
        private System.Windows.Forms.DataGridView dataGridViewBooks;
        private System.Windows.Forms.Button qBack;
    }
}