namespace InstinetTicketer
{
    partial class Allocate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Allocate));
            this.btnSaveAllo = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.lblRemaining = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblAllocated = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblAllocate = new System.Windows.Forms.Label();
            this.DGVAllocator = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.btnColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.tbcQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbcCommission = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tbcClientAcct = new System.Windows.Forms.DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DGVAllocator)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSaveAllo
            // 
            this.btnSaveAllo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveAllo.BackColor = System.Drawing.Color.LimeGreen;
            this.btnSaveAllo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSaveAllo.Location = new System.Drawing.Point(574, 302);
            this.btnSaveAllo.Name = "btnSaveAllo";
            this.btnSaveAllo.Size = new System.Drawing.Size(124, 46);
            this.btnSaveAllo.TabIndex = 21;
            this.btnSaveAllo.Text = "Save";
            this.btnSaveAllo.UseVisualStyleBackColor = false;
            this.btnSaveAllo.Click += new System.EventHandler(this.btnSaveAllo_Click);
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(37, 317);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 17);
            this.label5.TabIndex = 20;
            this.label5.Text = "Total:";
            // 
            // lblRemaining
            // 
            this.lblRemaining.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblRemaining.AutoSize = true;
            this.lblRemaining.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRemaining.Location = new System.Drawing.Point(372, 317);
            this.lblRemaining.Name = "lblRemaining";
            this.lblRemaining.Size = new System.Drawing.Size(84, 17);
            this.lblRemaining.TabIndex = 19;
            this.lblRemaining.Text = "Remaining";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(511, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 37);
            this.label4.TabIndex = 18;
            this.label4.Text = "Side: ";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(511, 148);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 37);
            this.label3.TabIndex = 17;
            this.label3.Text = "Price: ";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(580, 148);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 24);
            this.label2.TabIndex = 16;
            this.label2.Text = "label2";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(580, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 25);
            this.label1.TabIndex = 15;
            this.label1.Text = "label1";
            // 
            // lblAllocated
            // 
            this.lblAllocated.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblAllocated.AutoSize = true;
            this.lblAllocated.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAllocated.Location = new System.Drawing.Point(195, 317);
            this.lblAllocated.Name = "lblAllocated";
            this.lblAllocated.Size = new System.Drawing.Size(75, 17);
            this.lblAllocated.TabIndex = 14;
            this.lblAllocated.Text = "Allocated";
            // 
            // lblTotal
            // 
            this.lblTotal.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(93, 317);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(45, 17);
            this.lblTotal.TabIndex = 13;
            this.lblTotal.Text = "Total";
            // 
            // lblAllocate
            // 
            this.lblAllocate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAllocate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAllocate.Location = new System.Drawing.Point(552, 12);
            this.lblAllocate.Name = "lblAllocate";
            this.lblAllocate.Size = new System.Drawing.Size(146, 87);
            this.lblAllocate.TabIndex = 12;
            this.lblAllocate.Text = "  Notes";
            // 
            // DGVAllocator
            // 
            this.DGVAllocator.AllowDrop = true;
            this.DGVAllocator.AllowUserToAddRows = false;
            this.DGVAllocator.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DGVAllocator.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVAllocator.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.btnColumn,
            this.tbcQty,
            this.tbcCommission,
            this.tbcClientAcct});
            this.DGVAllocator.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.DGVAllocator.Location = new System.Drawing.Point(12, 12);
            this.DGVAllocator.Name = "DGVAllocator";
            this.DGVAllocator.RowTemplate.Height = 24;
            this.DGVAllocator.Size = new System.Drawing.Size(474, 251);
            this.DGVAllocator.TabIndex = 11;
            this.DGVAllocator.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVAllocator_CellContentClick);
            this.DGVAllocator.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVAllocator_CellValueChanged);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(36, 365);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(582, 24);
            this.label6.TabIndex = 22;
            // 
            // btnColumn
            // 
            this.btnColumn.HeaderText = "";
            this.btnColumn.Image = ((System.Drawing.Image)(resources.GetObject("btnColumn.Image")));
            this.btnColumn.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.btnColumn.Name = "btnColumn";
            this.btnColumn.Width = 35;
            // 
            // tbcQty
            // 
            this.tbcQty.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.tbcQty.FillWeight = 235.1852F;
            this.tbcQty.HeaderText = "Quantity";
            this.tbcQty.Name = "tbcQty";
            this.tbcQty.Width = 80;
            // 
            // tbcCommission
            // 
            this.tbcCommission.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.tbcCommission.FillWeight = 55.55556F;
            this.tbcCommission.HeaderText = "Comm";
            this.tbcCommission.Name = "tbcCommission";
            this.tbcCommission.Width = 50;
            // 
            // tbcClientAcct
            // 
            this.tbcClientAcct.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.tbcClientAcct.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
            this.tbcClientAcct.FillWeight = 9.259262F;
            this.tbcClientAcct.HeaderText = "Account";
            this.tbcClientAcct.Name = "tbcClientAcct";
            // 
            // Allocate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(733, 414);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnSaveAllo);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblRemaining);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblAllocated);
            this.Controls.Add(this.lblTotal);
            this.Controls.Add(this.lblAllocate);
            this.Controls.Add(this.DGVAllocator);
            this.Name = "Allocate";
            this.Text = "Allocate";
            ((System.ComponentModel.ISupportInitialize)(this.DGVAllocator)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSaveAllo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblRemaining;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblAllocated;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblAllocate;
        public System.Windows.Forms.DataGridView DGVAllocator;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridViewImageColumn btnColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn tbcQty;
        private System.Windows.Forms.DataGridViewTextBoxColumn tbcCommission;
        private System.Windows.Forms.DataGridViewComboBoxColumn tbcClientAcct;
    }
}