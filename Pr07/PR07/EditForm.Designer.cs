using System.Windows.Forms;

namespace PR07
{
    partial class EditForm
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

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvTickets = new System.Windows.Forms.DataGridView();
            this.txtMovieTitle = new System.Windows.Forms.TextBox();
            this.txtHallNumber = new System.Windows.Forms.TextBox();
            this.txtRowNumber = new System.Windows.Forms.TextBox();
            this.txtSeatNumber = new System.Windows.Forms.TextBox();
            this.dtpShowTime = new System.Windows.Forms.DateTimePicker();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.butEdit = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblMovieTitle = new System.Windows.Forms.Label();
            this.lblHallNumber = new System.Windows.Forms.Label();
            this.lblRowNumber = new System.Windows.Forms.Label();
            this.lblSeatNumber = new System.Windows.Forms.Label();
            this.lblShowTime = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTickets)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvTickets
            // 
            this.dgvTickets.AllowUserToAddRows = false;
            this.dgvTickets.AllowUserToDeleteRows = false;
            this.dgvTickets.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTickets.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgvTickets.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvTickets.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvTickets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTickets.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvTickets.Location = new System.Drawing.Point(0, 0);
            this.dgvTickets.Name = "dgvTickets";
            this.dgvTickets.ReadOnly = true;
            this.dgvTickets.Size = new System.Drawing.Size(1264, 277);
            this.dgvTickets.TabIndex = 0;
            this.dgvTickets.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTickets_CellContentClick);
            // 
            // txtMovieTitle
            // 
            this.txtMovieTitle.Location = new System.Drawing.Point(210, 287);
            this.txtMovieTitle.MaxLength = 100;
            this.txtMovieTitle.Name = "txtMovieTitle";
            this.txtMovieTitle.Size = new System.Drawing.Size(200, 31);
            this.txtMovieTitle.TabIndex = 1;
            this.txtMovieTitle.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.MovietitleKeyPress);
            // 
            // txtHallNumber
            // 
            this.txtHallNumber.Location = new System.Drawing.Point(210, 344);
            this.txtHallNumber.MaxLength = 2;
            this.txtHallNumber.Name = "txtHallNumber";
            this.txtHallNumber.Size = new System.Drawing.Size(200, 31);
            this.txtHallNumber.TabIndex = 2;
            // 
            // txtRowNumber
            // 
            this.txtRowNumber.Location = new System.Drawing.Point(210, 317);
            this.txtRowNumber.MaxLength = 1;
            this.txtRowNumber.Name = "txtRowNumber";
            this.txtRowNumber.Size = new System.Drawing.Size(200, 31);
            this.txtRowNumber.TabIndex = 3;
            // 
            // txtSeatNumber
            // 
            this.txtSeatNumber.Location = new System.Drawing.Point(210, 374);
            this.txtSeatNumber.MaxLength = 4;
            this.txtSeatNumber.Name = "txtSeatNumber";
            this.txtSeatNumber.Size = new System.Drawing.Size(200, 31);
            this.txtSeatNumber.TabIndex = 4;
            this.txtSeatNumber.TextChanged += new System.EventHandler(this.txtSeatNumber_TextChanged);
            // 
            // dtpShowTime
            // 
            this.dtpShowTime.Location = new System.Drawing.Point(210, 401);
            this.dtpShowTime.Name = "dtpShowTime";
            this.dtpShowTime.Size = new System.Drawing.Size(200, 31);
            this.dtpShowTime.TabIndex = 5;
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(210, 429);
            this.txtPrice.MaxLength = 10;
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(200, 31);
            this.txtPrice.TabIndex = 6;
            // 
            // butEdit
            // 
            this.butEdit.Location = new System.Drawing.Point(15, 504);
            this.butEdit.Name = "butEdit";
            this.butEdit.Size = new System.Drawing.Size(264, 40);
            this.butEdit.TabIndex = 7;
            this.butEdit.Text = "Редактировать";
            this.butEdit.UseVisualStyleBackColor = true;
            this.butEdit.Click += new System.EventHandler(this.EditClick);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.Color.Crimson;
            this.btnBack.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnBack.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnBack.ForeColor = System.Drawing.Color.White;
            this.btnBack.Location = new System.Drawing.Point(0, 557);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(1264, 104);
            this.btnBack.TabIndex = 8;
            this.btnBack.Text = "Назад";
            this.btnBack.UseVisualStyleBackColor = false;
            this.btnBack.Click += new System.EventHandler(this.Backbutton);
            // 
            // lblMovieTitle
            // 
            this.lblMovieTitle.AutoSize = true;
            this.lblMovieTitle.Location = new System.Drawing.Point(11, 287);
            this.lblMovieTitle.Name = "lblMovieTitle";
            this.lblMovieTitle.Size = new System.Drawing.Size(193, 23);
            this.lblMovieTitle.TabIndex = 9;
            this.lblMovieTitle.Text = "Название фильма:";
            // 
            // lblHallNumber
            // 
            this.lblHallNumber.AutoSize = true;
            this.lblHallNumber.Location = new System.Drawing.Point(11, 317);
            this.lblHallNumber.Name = "lblHallNumber";
            this.lblHallNumber.Size = new System.Drawing.Size(132, 23);
            this.lblHallNumber.TabIndex = 10;
            this.lblHallNumber.Text = "Номер зала:";
            // 
            // lblRowNumber
            // 
            this.lblRowNumber.AutoSize = true;
            this.lblRowNumber.Location = new System.Drawing.Point(11, 347);
            this.lblRowNumber.Name = "lblRowNumber";
            this.lblRowNumber.Size = new System.Drawing.Size(134, 23);
            this.lblRowNumber.TabIndex = 11;
            this.lblRowNumber.Text = "Номер ряда:";
            // 
            // lblSeatNumber
            // 
            this.lblSeatNumber.AutoSize = true;
            this.lblSeatNumber.Location = new System.Drawing.Point(11, 377);
            this.lblSeatNumber.Name = "lblSeatNumber";
            this.lblSeatNumber.Size = new System.Drawing.Size(142, 23);
            this.lblSeatNumber.TabIndex = 12;
            this.lblSeatNumber.Text = "Номер места:";
            // 
            // lblShowTime
            // 
            this.lblShowTime.AutoSize = true;
            this.lblShowTime.Location = new System.Drawing.Point(11, 407);
            this.lblShowTime.Name = "lblShowTime";
            this.lblShowTime.Size = new System.Drawing.Size(151, 23);
            this.lblShowTime.TabIndex = 13;
            this.lblShowTime.Text = "Время сеанса:";
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(11, 437);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(67, 23);
            this.lblPrice.TabIndex = 14;
            this.lblPrice.Text = "Цена:";
            // 
            // EditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 661);
            this.Controls.Add(this.dgvTickets);
            this.Controls.Add(this.txtMovieTitle);
            this.Controls.Add(this.lblMovieTitle);
            this.Controls.Add(this.txtHallNumber);
            this.Controls.Add(this.lblHallNumber);
            this.Controls.Add(this.txtRowNumber);
            this.Controls.Add(this.lblRowNumber);
            this.Controls.Add(this.txtSeatNumber);
            this.Controls.Add(this.lblSeatNumber);
            this.Controls.Add(this.dtpShowTime);
            this.Controls.Add(this.lblShowTime);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.butEdit);
            this.Controls.Add(this.btnBack);
            this.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.MinimumSize = new System.Drawing.Size(1200, 700);
            this.Name = "EditForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Редактирование Билетов";
            this.Load += new System.EventHandler(this.EditForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTickets)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.DataGridView dgvTickets;
        private System.Windows.Forms.TextBox txtMovieTitle;
        private System.Windows.Forms.TextBox txtHallNumber;
        private System.Windows.Forms.TextBox txtRowNumber;
        private System.Windows.Forms.TextBox txtSeatNumber;
        private System.Windows.Forms.DateTimePicker dtpShowTime;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.Button butEdit;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label lblMovieTitle;
        private System.Windows.Forms.Label lblHallNumber;
        private System.Windows.Forms.Label lblRowNumber;
        private System.Windows.Forms.Label lblSeatNumber;
        private System.Windows.Forms.Label lblShowTime;
        private System.Windows.Forms.Label lblPrice;
    }
}
