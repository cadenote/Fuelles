// Fuelles - bellows fold pattern printer, based on US Patent No 6,054,194,
// Mathematically optimized family of ultra low distortion bellow fold patterns, Nathan R. Kane.
// Copyright (C) 2008, Frank Tkalcevic, www.franksworkshop.com

//It is a fork of bellows.zip in http://www.franksworkshop.com.au/CNC/Bellows/Bellows.htm

// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// any later version.

// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.

// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

namespace Fuelles
{
    partial class GenerateGCode
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
            this.txtOutputFile = new System.Windows.Forms.TextBox();
            this.lblOutputFile = new System.Windows.Forms.Label();
            this.txtCutterOffset = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.Orienta = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.CentroX = new System.Windows.Forms.TextBox();
            this.CentroY = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.reverso = new System.Windows.Forms.CheckedListBox();
            this.gobierno = new System.Windows.Forms.CheckBox();
            this.Resorte = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtOutputFile
            // 
            this.txtOutputFile.Location = new System.Drawing.Point(111, 16);
            this.txtOutputFile.Margin = new System.Windows.Forms.Padding(4);
            this.txtOutputFile.Name = "txtOutputFile";
            this.txtOutputFile.Size = new System.Drawing.Size(208, 22);
            this.txtOutputFile.TabIndex = 0;
            this.txtOutputFile.Text = "E:\\temp\\Fuelles";
            // 
            // lblOutputFile
            // 
            this.lblOutputFile.AutoSize = true;
            this.lblOutputFile.Location = new System.Drawing.Point(17, 20);
            this.lblOutputFile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblOutputFile.Name = "lblOutputFile";
            this.lblOutputFile.Size = new System.Drawing.Size(77, 17);
            this.lblOutputFile.TabIndex = 1;
            this.lblOutputFile.Text = "Output File";
            // 
            // txtCutterOffset
            // 
            this.txtCutterOffset.Location = new System.Drawing.Point(163, 49);
            this.txtCutterOffset.Margin = new System.Windows.Forms.Padding(4);
            this.txtCutterOffset.Name = "txtCutterOffset";
            this.txtCutterOffset.Size = new System.Drawing.Size(156, 22);
            this.txtCutterOffset.TabIndex = 2;
            this.txtCutterOffset.Text = "0.8";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 53);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Cutter Offset (mm)";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Fuelles.Properties.Resources.Cutter1;
            this.pictureBox1.Location = new System.Drawing.Point(328, 15);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(319, 298);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(21, 284);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 28);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(131, 284);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // Orienta
            // 
            this.Orienta.AutoSize = true;
            this.Orienta.Location = new System.Drawing.Point(18, 133);
            this.Orienta.Name = "Orienta";
            this.Orienta.Size = new System.Drawing.Size(138, 17);
            this.Orienta.TabIndex = 7;
            this.Orienta.Text = "Orientacion GCode º";
            this.Orienta.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.DecimalPlaces = 1;
            this.numericUpDown1.Location = new System.Drawing.Point(162, 131);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(100, 22);
            this.numericUpDown1.TabIndex = 8;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 173);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(220, 17);
            this.label2.TabIndex = 9;
            this.label2.Text = "X              Centro Volteo             Y";
            // 
            // CentroX
            // 
            this.CentroX.Location = new System.Drawing.Point(21, 193);
            this.CentroX.Name = "CentroX";
            this.CentroX.Size = new System.Drawing.Size(100, 22);
            this.CentroX.TabIndex = 10;
            this.CentroX.Text = "0.0";
            this.CentroX.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CentroY
            // 
            this.CentroY.Location = new System.Drawing.Point(196, 193);
            this.CentroY.Name = "CentroY";
            this.CentroY.Size = new System.Drawing.Size(100, 22);
            this.CentroY.TabIndex = 11;
            this.CentroY.Text = "0.0";
            this.CentroY.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 237);
            this.label3.Name = "label3";
            this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label3.Size = new System.Drawing.Size(61, 17);
            this.label3.TabIndex = 12;
            this.label3.Text = "Reverso";
            // 
            // reverso
            // 
            this.reverso.CheckOnClick = true;
            this.reverso.FormattingEnabled = true;
            this.reverso.Items.AddRange(new object[] {
            "EspejoX",
            "EspejoY"});
            this.reverso.Location = new System.Drawing.Point(94, 226);
            this.reverso.MinimumSize = new System.Drawing.Size(4, 50);
            this.reverso.Name = "reverso";
            this.reverso.Size = new System.Drawing.Size(120, 38);
            this.reverso.TabIndex = 13;
            this.reverso.SelectedIndexChanged += new System.EventHandler(this.CheckedListBox1_SelectedIndexChanged);
            // 
            // gobierno
            // 
            this.gobierno.AutoSize = true;
            this.gobierno.BackColor = System.Drawing.SystemColors.Control;
            this.gobierno.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.gobierno.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.gobierno.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gobierno.Location = new System.Drawing.Point(20, 93);
            this.gobierno.Name = "gobierno";
            this.gobierno.Size = new System.Drawing.Size(115, 22);
            this.gobierno.TabIndex = 14;
            this.gobierno.Text = "Gobierno A";
            this.gobierno.UseVisualStyleBackColor = false;
            // 
            // Resorte
            // 
            this.Resorte.Appearance = System.Windows.Forms.Appearance.Button;
            this.Resorte.AutoSize = true;
            this.Resorte.Location = new System.Drawing.Point(174, 93);
            this.Resorte.Name = "Resorte";
            this.Resorte.Size = new System.Drawing.Size(59, 27);
            this.Resorte.TabIndex = 15;
            this.Resorte.Text = "Muelle";
            this.Resorte.UseVisualStyleBackColor = true;
            this.Resorte.CheckedChanged += new System.EventHandler(this.CheckBox1_CheckedChanged);
            // 
            // GenerateGCode
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(695, 348);
            this.Controls.Add(this.Resorte);
            this.Controls.Add(this.gobierno);
            this.Controls.Add(this.reverso);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CentroY);
            this.Controls.Add(this.CentroX);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.Orienta);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtCutterOffset);
            this.Controls.Add(this.lblOutputFile);
            this.Controls.Add(this.txtOutputFile);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "GenerateGCode";
            this.Text = "Genera GCode";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox txtOutputFile;
        private System.Windows.Forms.Label lblOutputFile;
        public System.Windows.Forms.TextBox txtCutterOffset;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label Orienta;
        public System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox CentroX;
        public System.Windows.Forms.TextBox CentroY;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.CheckedListBox reverso;
        public System.Windows.Forms.CheckBox gobierno;
        public System.Windows.Forms.CheckBox Resorte;
    }
}