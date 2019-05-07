// Fuelles is a forked work of Bellows Program obtained from www.franksworkshop.com Copyright (C) 2008, Frank Tkalcevic.
// Bellows - bellows fold pattern printer, based on US Patent No 6,054,194,
// Mathematically optimized family of ultra low distortion bellow fold patterns, Nathan R. Kane.

//Fuelles Copyright (C) 2019 , Jose Jimenez <cadenote@hotmail.com>

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
	partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtWidth = new System.Windows.Forms.TextBox();
            this.txtHeight = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtLength = new System.Windows.Forms.TextBox();
            this.chkAlternateFolds = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtFoldWidth = new System.Windows.Forms.TextBox();
            this.txtMountFolds = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnPrintSetup = new System.Windows.Forms.Button();
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.btnPageSetup = new System.Windows.Forms.Button();
            this.cboConfig = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnNewConfig = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.actualiza = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.listinv = new System.Windows.Forms.ListBox();
            this.pared = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.pliegues = new System.Windows.Forms.CheckedListBox();
            this.btnGCode = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblPageWidth = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblPageHeight = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(40, 199);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Inversiones";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 25);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(114, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Angulo de Pared";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(369, 20);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Ancho Interior";
            this.label3.Click += new System.EventHandler(this.Label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(385, 57);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Alto Interior";
            // 
            // txtWidth
            // 
            this.txtWidth.Location = new System.Drawing.Point(473, 17);
            this.txtWidth.Margin = new System.Windows.Forms.Padding(4);
            this.txtWidth.Name = "txtWidth";
            this.txtWidth.Size = new System.Drawing.Size(132, 22);
            this.txtWidth.TabIndex = 4;
            this.txtWidth.Leave += new System.EventHandler(this.txtWidth_Leave);
            // 
            // txtHeight
            // 
            this.txtHeight.Location = new System.Drawing.Point(473, 49);
            this.txtHeight.Margin = new System.Windows.Forms.Padding(4);
            this.txtHeight.Name = "txtHeight";
            this.txtHeight.Size = new System.Drawing.Size(132, 22);
            this.txtHeight.TabIndex = 5;
            this.txtHeight.Leave += new System.EventHandler(this.txtHeight_Leave);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(337, 85);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(128, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "Longitud Protegida";
            // 
            // txtLength
            // 
            this.txtLength.Location = new System.Drawing.Point(473, 81);
            this.txtLength.Margin = new System.Windows.Forms.Padding(4);
            this.txtLength.Name = "txtLength";
            this.txtLength.Size = new System.Drawing.Size(132, 22);
            this.txtLength.TabIndex = 6;
            this.txtLength.Leave += new System.EventHandler(this.txtLength_Leave);
            // 
            // chkAlternateFolds
            // 
            this.chkAlternateFolds.AutoSize = true;
            this.chkAlternateFolds.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.chkAlternateFolds.Location = new System.Drawing.Point(20, 116);
            this.chkAlternateFolds.Margin = new System.Windows.Forms.Padding(4);
            this.chkAlternateFolds.Name = "chkAlternateFolds";
            this.chkAlternateFolds.Size = new System.Drawing.Size(137, 21);
            this.chkAlternateFolds.TabIndex = 3;
            this.chkAlternateFolds.Text = "Alternar pliegues";
            this.chkAlternateFolds.UseVisualStyleBackColor = true;
            this.chkAlternateFolds.CheckedChanged += new System.EventHandler(this.chkAlternateFolds_CheckedChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(351, 117);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 17);
            this.label6.TabIndex = 12;
            this.label6.Text = "Ancho de Pliegue";
            // 
            // txtFoldWidth
            // 
            this.txtFoldWidth.Location = new System.Drawing.Point(473, 113);
            this.txtFoldWidth.Margin = new System.Windows.Forms.Padding(4);
            this.txtFoldWidth.Name = "txtFoldWidth";
            this.txtFoldWidth.Size = new System.Drawing.Size(132, 22);
            this.txtFoldWidth.TabIndex = 7;
            this.txtFoldWidth.Leave += new System.EventHandler(this.txtFoldWidth_Leave);
            // 
            // txtMountFolds
            // 
            this.txtMountFolds.Location = new System.Drawing.Point(196, 68);
            this.txtMountFolds.Margin = new System.Windows.Forms.Padding(4);
            this.txtMountFolds.Name = "txtMountFolds";
            this.txtMountFolds.Size = new System.Drawing.Size(72, 22);
            this.txtMountFolds.TabIndex = 2;
            this.txtMountFolds.TextChanged += new System.EventHandler(this.TxtMountFolds_TextChanged);
            this.txtMountFolds.Leave += new System.EventHandler(this.txtMountFolds_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(17, 71);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(136, 17);
            this.label7.TabIndex = 14;
            this.label7.Text = "Pliegues de Montaje";
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(128, 148);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(125, 28);
            this.btnPrint.TabIndex = 9;
            this.btnPrint.Text = "Print Preview";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnPrintSetup
            // 
            this.btnPrintSetup.Location = new System.Drawing.Point(261, 148);
            this.btnPrintSetup.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrintSetup.Name = "btnPrintSetup";
            this.btnPrintSetup.Size = new System.Drawing.Size(132, 28);
            this.btnPrintSetup.TabIndex = 10;
            this.btnPrintSetup.Text = "Print...";
            this.btnPrintSetup.UseVisualStyleBackColor = true;
            this.btnPrintSetup.Click += new System.EventHandler(this.btnPrintSetup_Click);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // printDocument1
            // 
            this.printDocument1.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.printDocument1_BeginPrint);
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // btnPageSetup
            // 
            this.btnPageSetup.Location = new System.Drawing.Point(20, 148);
            this.btnPageSetup.Margin = new System.Windows.Forms.Padding(4);
            this.btnPageSetup.Name = "btnPageSetup";
            this.btnPageSetup.Size = new System.Drawing.Size(100, 28);
            this.btnPageSetup.TabIndex = 8;
            this.btnPageSetup.Text = "Page Setup";
            this.btnPageSetup.UseVisualStyleBackColor = true;
            this.btnPageSetup.Click += new System.EventHandler(this.btnPageSetup_Click);
            // 
            // cboConfig
            // 
            this.cboConfig.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboConfig.FormattingEnabled = true;
            this.cboConfig.Location = new System.Drawing.Point(99, 17);
            this.cboConfig.Margin = new System.Windows.Forms.Padding(4);
            this.cboConfig.Name = "cboConfig";
            this.cboConfig.Size = new System.Drawing.Size(160, 24);
            this.cboConfig.TabIndex = 0;
            this.cboConfig.SelectedIndexChanged += new System.EventHandler(this.cboConfig_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 21);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 17);
            this.label8.TabIndex = 19;
            this.label8.Text = "Config";
            // 
            // btnNewConfig
            // 
            this.btnNewConfig.Location = new System.Drawing.Point(284, 15);
            this.btnNewConfig.Margin = new System.Windows.Forms.Padding(4);
            this.btnNewConfig.Name = "btnNewConfig";
            this.btnNewConfig.Size = new System.Drawing.Size(100, 28);
            this.btnNewConfig.TabIndex = 1;
            this.btnNewConfig.Text = "New";
            this.btnNewConfig.UseVisualStyleBackColor = true;
            this.btnNewConfig.Click += new System.EventHandler(this.btnNewConfig_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.actualiza);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.listinv);
            this.groupBox1.Controls.Add(this.pared);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.pliegues);
            this.groupBox1.Controls.Add(this.btnGCode);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btnPageSetup);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.btnPrintSetup);
            this.groupBox1.Controls.Add(this.lblPageWidth);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblPageHeight);
            this.groupBox1.Controls.Add(this.btnPrint);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtMountFolds);
            this.groupBox1.Controls.Add(this.txtWidth);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtHeight);
            this.groupBox1.Controls.Add(this.txtFoldWidth);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtLength);
            this.groupBox1.Controls.Add(this.chkAlternateFolds);
            this.groupBox1.Location = new System.Drawing.Point(16, 50);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(899, 535);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            // 
            // actualiza
            // 
            this.actualiza.Location = new System.Drawing.Point(168, 279);
            this.actualiza.Name = "actualiza";
            this.actualiza.Size = new System.Drawing.Size(95, 28);
            this.actualiza.TabIndex = 28;
            this.actualiza.Text = "Actualiza";
            this.actualiza.UseVisualStyleBackColor = true;
            this.actualiza.Click += new System.EventHandler(this.Actualiza_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(163, 251);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 22);
            this.textBox1.TabIndex = 27;
            // 
            // listinv
            // 
            this.listinv.FormattingEnabled = true;
            this.listinv.ItemHeight = 16;
            this.listinv.Location = new System.Drawing.Point(37, 223);
            this.listinv.Name = "listinv";
            this.listinv.Size = new System.Drawing.Size(120, 84);
            this.listinv.TabIndex = 26;
            this.listinv.SelectedIndexChanged += new System.EventHandler(this.Listinv_SelectedIndexChanged);
            // 
            // pared
            // 
            this.pared.AutoSize = true;
            this.pared.Location = new System.Drawing.Point(180, 25);
            this.pared.Name = "pared";
            this.pared.Size = new System.Drawing.Size(54, 17);
            this.pared.TabIndex = 24;
            this.pared.Text = "label12";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(708, 116);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(138, 17);
            this.label11.TabIndex = 22;
            this.label11.Text = "Lineas de Pliegue";
            this.label11.UseMnemonic = false;
            // 
            // pliegues
            // 
            this.pliegues.CheckOnClick = true;
            this.pliegues.FormattingEnabled = true;
            this.pliegues.Items.AddRange(new object[] {
            "Picos",
            "Valles"});
            this.pliegues.Location = new System.Drawing.Point(733, 138);
            this.pliegues.MinimumSize = new System.Drawing.Size(4, 50);
            this.pliegues.Name = "pliegues";
            this.pliegues.Size = new System.Drawing.Size(98, 38);
            this.pliegues.TabIndex = 21;
            this.pliegues.SelectedIndexChanged += new System.EventHandler(this.Pliegues_SelectedIndexChanged);
            // 
            // btnGCode
            // 
            this.btnGCode.Location = new System.Drawing.Point(403, 148);
            this.btnGCode.Margin = new System.Windows.Forms.Padding(4);
            this.btnGCode.Name = "btnGCode";
            this.btnGCode.Size = new System.Drawing.Size(132, 28);
            this.btnGCode.TabIndex = 20;
            this.btnGCode.Text = "Genera G Code";
            this.btnGCode.UseVisualStyleBackColor = true;
            this.btnGCode.Click += new System.EventHandler(this.btnGCode_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Location = new System.Drawing.Point(534, 185);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(356, 343);
            this.panel1.TabIndex = 19;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // lblPageWidth
            // 
            this.lblPageWidth.AutoSize = true;
            this.lblPageWidth.Location = new System.Drawing.Point(745, 21);
            this.lblPageWidth.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPageWidth.Name = "lblPageWidth";
            this.lblPageWidth.Size = new System.Drawing.Size(81, 17);
            this.lblPageWidth.TabIndex = 5;
            this.lblPageWidth.Text = "Page Width";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(653, 21);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(96, 17);
            this.label10.TabIndex = 5;
            this.label10.Text = "Ancho Pagina";
            // 
            // lblPageHeight
            // 
            this.lblPageHeight.AutoSize = true;
            this.lblPageHeight.Location = new System.Drawing.Point(745, 53);
            this.lblPageHeight.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblPageHeight.Name = "lblPageHeight";
            this.lblPageHeight.Size = new System.Drawing.Size(86, 17);
            this.lblPageHeight.TabIndex = 6;
            this.lblPageHeight.Text = "Page Height";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(653, 53);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 17);
            this.label9.TabIndex = 6;
            this.label9.Text = "Alto Pagina";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(931, 601);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnNewConfig);
            this.Controls.Add(this.cboConfig);
            this.Controls.Add(this.label8);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Forma del Fuelle";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtWidth;
		private System.Windows.Forms.TextBox txtHeight;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtLength;
		private System.Windows.Forms.CheckBox chkAlternateFolds;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtFoldWidth;
		private System.Windows.Forms.TextBox txtMountFolds;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button btnPrint;
		private System.Windows.Forms.Button btnPrintSetup;
		private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
		private System.Windows.Forms.PrintDialog printDialog1;
		private System.Drawing.Printing.PrintDocument printDocument1;
		private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
		private System.Windows.Forms.Button btnPageSetup;
		private System.Windows.Forms.ComboBox cboConfig;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Button btnNewConfig;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label lblPageWidth;
		private System.Windows.Forms.Label lblPageHeight;
        private System.Windows.Forms.Button btnGCode;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.CheckedListBox pliegues;
        private System.Windows.Forms.Label pared;
        private System.Windows.Forms.ListBox listinv;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button actualiza;
    }
}

