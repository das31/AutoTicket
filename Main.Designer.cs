﻿namespace InstinetTicketer
{
    partial class Main
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
            this.btnInstinet_Uploader = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnInstinet_Uploader
            // 
            this.btnInstinet_Uploader.Location = new System.Drawing.Point(269, 272);
            this.btnInstinet_Uploader.Name = "btnInstinet_Uploader";
            this.btnInstinet_Uploader.Size = new System.Drawing.Size(219, 82);
            this.btnInstinet_Uploader.TabIndex = 0;
            this.btnInstinet_Uploader.Text = "Instinet Uploader";
            this.btnInstinet_Uploader.UseVisualStyleBackColor = true;
            this.btnInstinet_Uploader.Click += new System.EventHandler(this.btnInstinet_Uploader_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 542);
            this.Controls.Add(this.btnInstinet_Uploader);
            this.Name = "Main";
            this.Text = "Instinet Ticketer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnInstinet_Uploader;
    }
}

