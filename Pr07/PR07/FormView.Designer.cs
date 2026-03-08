using System.Windows.Forms;

namespace PR07
{
    partial class FormView
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ticketGrid = new System.Windows.Forms.DataGridView();
            this.backButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ticketGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // ticketGrid
            // 
            this.ticketGrid.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.ticketGrid.AllowUserToAddRows = false;
            this.ticketGrid.AllowUserToDeleteRows = false;
            this.ticketGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.ticketGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.ticketGrid.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False; // Вот это отвечает за будет ли  переносится (сейчас нет)
            this.ticketGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.ticketGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ticketGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ticketGrid.Location = new System.Drawing.Point(0, 0);
            this.ticketGrid.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.ticketGrid.Name = "ticketGrid";
            this.ticketGrid.ReadOnly = true;
            this.ticketGrid.Size = new System.Drawing.Size(1264, 661);
            this.ticketGrid.TabIndex = 0;
            this.ticketGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ticketGrid_CellContentClick);
            // 
            // backButton
            // 
            this.backButton.BackColor = System.Drawing.Color.Crimson;
            this.backButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.backButton.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.backButton.ForeColor = System.Drawing.Color.White;
            this.backButton.Location = new System.Drawing.Point(0, 557);
            this.backButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.backButton.Name = "backButton";
            this.backButton.Size = new System.Drawing.Size(1264, 104);
            this.backButton.TabIndex = 1;
            this.backButton.Text = "Назад";
            this.backButton.UseVisualStyleBackColor = false;
            this.backButton.Click += new System.EventHandler(this.Backbutton);
            // 
            // FormView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1264, 661);
            this.Controls.Add(this.backButton);
            this.Controls.Add(this.ticketGrid);
            this.Font = new System.Drawing.Font("Verdana", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(1200, 700);
            this.Name = "FormView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Просмотр билетов";
            this.Load += new System.EventHandler(this.FormView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ticketGrid)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView ticketGrid;
        private System.Windows.Forms.Button backButton;
    }
}