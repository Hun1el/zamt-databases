namespace PR14
{
    partial class AdminAddForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminAddForm));
            this.cmbPassenger = new System.Windows.Forms.ComboBox();
            this.cmbFlight = new System.Windows.Forms.ComboBox();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.txtSeatNumber = new System.Windows.Forms.TextBox();
            this.btnAddTicket = new System.Windows.Forms.Button();
            this.lblPassenger = new System.Windows.Forms.Label();
            this.lblFlight = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.lblSeatNumber = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbPassenger
            // 
            this.cmbPassenger.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.cmbPassenger.FormattingEnabled = true;
            this.cmbPassenger.Location = new System.Drawing.Point(150, 62);
            this.cmbPassenger.Name = "cmbPassenger";
            this.cmbPassenger.Size = new System.Drawing.Size(266, 32);
            this.cmbPassenger.TabIndex = 0;
            this.cmbPassenger.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbPassenger_KeyPress);
            // 
            // cmbFlight
            // 
            this.cmbFlight.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.cmbFlight.FormattingEnabled = true;
            this.cmbFlight.Location = new System.Drawing.Point(150, 156);
            this.cmbFlight.Name = "cmbFlight";
            this.cmbFlight.Size = new System.Drawing.Size(266, 32);
            this.cmbFlight.TabIndex = 1;
            this.cmbFlight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbFlight_KeyPress);
            // 
            // txtPrice
            // 
            this.txtPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtPrice.Location = new System.Drawing.Point(150, 234);
            this.txtPrice.MaxLength = 6;
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(266, 29);
            this.txtPrice.TabIndex = 2;
            this.txtPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrice_KeyPress);
            // 
            // txtSeatNumber
            // 
            this.txtSeatNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.txtSeatNumber.Location = new System.Drawing.Point(191, 320);
            this.txtSeatNumber.MaxLength = 4;
            this.txtSeatNumber.Name = "txtSeatNumber";
            this.txtSeatNumber.Size = new System.Drawing.Size(225, 29);
            this.txtSeatNumber.TabIndex = 3;
            this.txtSeatNumber.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSeatNumber_KeyPress);
            // 
            // btnAddTicket
            // 
            this.btnAddTicket.BackColor = System.Drawing.Color.Chocolate;
            this.btnAddTicket.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddTicket.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnAddTicket.Location = new System.Drawing.Point(117, 382);
            this.btnAddTicket.Name = "btnAddTicket";
            this.btnAddTicket.Size = new System.Drawing.Size(262, 56);
            this.btnAddTicket.TabIndex = 4;
            this.btnAddTicket.Text = "Добавить билет";
            this.btnAddTicket.UseVisualStyleBackColor = false;
            this.btnAddTicket.Click += new System.EventHandler(this.btnAddTicket_Click);
            // 
            // lblPassenger
            // 
            this.lblPassenger.AutoSize = true;
            this.lblPassenger.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.lblPassenger.Location = new System.Drawing.Point(11, 65);
            this.lblPassenger.Name = "lblPassenger";
            this.lblPassenger.Size = new System.Drawing.Size(133, 29);
            this.lblPassenger.TabIndex = 5;
            this.lblPassenger.Text = "Пассажир:";
            // 
            // lblFlight
            // 
            this.lblFlight.AutoSize = true;
            this.lblFlight.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.lblFlight.Location = new System.Drawing.Point(14, 156);
            this.lblFlight.Name = "lblFlight";
            this.lblFlight.Size = new System.Drawing.Size(76, 29);
            this.lblFlight.TabIndex = 6;
            this.lblFlight.Text = "Рейс:";
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.lblPrice.Location = new System.Drawing.Point(14, 233);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(78, 29);
            this.lblPrice.TabIndex = 7;
            this.lblPrice.Text = "Цена:";
            // 
            // lblSeatNumber
            // 
            this.lblSeatNumber.AutoSize = true;
            this.lblSeatNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F);
            this.lblSeatNumber.Location = new System.Drawing.Point(14, 320);
            this.lblSeatNumber.Name = "lblSeatNumber";
            this.lblSeatNumber.Size = new System.Drawing.Size(171, 29);
            this.lblSeatNumber.TabIndex = 8;
            this.lblSeatNumber.Text = "Номер места:";
            // 
            // AdminAddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 450);
            this.Controls.Add(this.cmbPassenger);
            this.Controls.Add(this.cmbFlight);
            this.Controls.Add(this.txtPrice);
            this.Controls.Add(this.txtSeatNumber);
            this.Controls.Add(this.btnAddTicket);
            this.Controls.Add(this.lblPassenger);
            this.Controls.Add(this.lblFlight);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.lblSeatNumber);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "AdminAddForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Добавление билета";
            this.ResumeLayout(false);
            this.PerformLayout();

        }


        #endregion

        private System.Windows.Forms.ComboBox cmbPassenger;
        private System.Windows.Forms.ComboBox cmbFlight;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.TextBox txtSeatNumber;
        private System.Windows.Forms.Button btnAddTicket;
        private System.Windows.Forms.Label lblPassenger;
        private System.Windows.Forms.Label lblFlight;
        private System.Windows.Forms.Label lblPrice;
        private System.Windows.Forms.Label lblSeatNumber;
    }
}
