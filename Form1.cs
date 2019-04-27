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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Globalization;

namespace Fuelles
{
    public partial class Form1 : Form
	{
		double dblPaperWidth;
        double dblFoldWidth;
        float gradbeta;
        int nFolds;
		int nPage;
		int nPages;
		Configuration m_Config;
		FuellesConfigElement m_CurrentElement;
		bool m_bCalculationError;
        GenerateGCode dlg;
        float dvC,angD ,xC ,yC;


        public Form1()
		{
			m_bCalculationError = true;

            InitializeComponent();

			cboShape.Items.Add(FuellesConfigElement.EBellowsShape.MediaCubierta);
			cboShape.Items.Add(FuellesConfigElement.EBellowsShape.CajaCerrada);
            cboShape.Items.Add(FuellesConfigElement.EBellowsShape.Abierta);

            cboInversions.Items.Add(1);
			cboInversions.Items.Add(2);
			cboInversions.Items.Add(3);
			cboInversions.Items.Add(4);

			m_Config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

			FuelleConfig cfg = (FuelleConfig)m_Config.Sections["FuelleConfig"];
			if (cfg == null)
			{
				cfg = new FuelleConfig();
				m_Config.Sections.Add("FuelleConfig", cfg);
				cfg = (FuelleConfig)m_Config.Sections["FuelleConfig"];
				cfg.SectionInformation.ForceSave = true;
			}

			foreach (FuellesConfigElement b in cfg.Bellows)
			{
				cboConfig.Items.Add(b);
			}
			int nItem = cboConfig.FindStringExact( ((FuelleConfig)m_Config.Sections["FuelleConfig"]).SelectedItem );
            if (nItem >= 0)
                cboConfig.SelectedIndex = nItem;
            else
            {
                if ( cboConfig.Items.Count > 0 )
                    cboConfig.SelectedIndex = 0;
            }

			printDocument1.PrinterSettings = printDialog1.PrinterSettings;
            pliegues.SetItemChecked(0, true);
            pliegues.SetItemChecked(1, true);
            UpdateCalculations();
		}

		public bool UpdateCalculations()
		{
			bool bRet = false;
			try
			{
				double dblLength = double.Parse(txtLength.Text);
				int nMountFolds = int.Parse(txtMountFolds.Text);
				double dblExtensionAngle = 120.0;
				double dblWidth = double.Parse(txtWidth.Text);
                double dblHeight = double.Parse(txtHeight.Text);

                dblFoldWidth = double.Parse(txtFoldWidth.Text);

                // Compute the number of folds 
                // = Length / (FoldWidth*sin(120))
                double dblFolds = dblLength / (dblFoldWidth * Math.Sin(dblExtensionAngle / 2 / 180 * Math.PI));
				nFolds = (int)(dblFolds + 1.0);

				// Then add mount folds
				nFolds += nMountFolds; //va bien si nMountFolds es par

                // Round up to even number.
                if ((nFolds & 1) == 1)
                    nFolds++;

                // Use width and height to calculate fold dimensions
                double dblTopWidth = dblWidth + 2 * dblFoldWidth;
				double dblSideHeight = dblHeight + dblFoldWidth;

				dblPaperWidth = dblTopWidth + 2 * dblSideHeight;

				lblPageWidth.Text = dblPaperWidth.ToString() + " mm";
				lblPageHeight.Text = ((double)nFolds * dblFoldWidth).ToString() + "mm";


				bRet = true;
				m_bCalculationError = false;
			}
			catch (Exception )
			{
				bRet = false;
				m_bCalculationError = true;
			}

			panel1.Invalidate();
			return bRet;
		}
		private void panel1_Paint(object sender, PaintEventArgs e)
		{
			DrawBellows(e.Graphics);
		}

		private void DrawBellows( Graphics g )
		{
			double dblFoldWidth;
			double dblHeight;
            byte que = 0;
            if (pliegues.CheckedItems.Count > 0)
            {
                if (pliegues.CheckedItems.Contains("Positivo") == true ) que = 2;
                if (pliegues.CheckedItems.Contains("Negativo") == true) que += 1;
            }
            if (!double.TryParse(txtFoldWidth.Text, out dblFoldWidth))
                return;

            if (!double.TryParse(txtHeight.Text, out dblHeight))
                return;

            double dblPaperHeight = (double)nFolds * dblFoldWidth;

            Pen oSolidPen;
			Pen oDottedPen;
			if (!m_bCalculationError)
			{
				oSolidPen = new Pen(Color.Black, 0.25f);
				oDottedPen = new Pen(Color.Red, 0.25f);
			}
			else
			{
				oSolidPen = new Pen(Color.Green, 0.25f);
				oDottedPen = new Pen(Color.Cyan, 0.25f);
			}

			oDottedPen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;

			double a1 = 72.57 / 180.0 * Math.PI;
			double a2 = 27.57 / 180.0 * Math.PI;

			double x1 = dblHeight + dblFoldWidth;
			double x2 = dblPaperWidth - x1;

			bool bAlternate = chkAlternateFolds.Checked;

			g.DrawRectangle(oSolidPen, 0, 0, (float)dblPaperWidth, (float)dblPaperHeight);// marco
			g.DrawLine(oSolidPen, 0, 0, (float)dblPaperWidth, (float)dblPaperHeight);//linea de seguimiento
			for (int i = 0; i < nFolds; i += 2)
			{
				double dxa1 = dblFoldWidth / Math.Tan(a1);
				double dxa2 = dblFoldWidth / Math.Tan(a2);

				double y = (double)(i + 1) * dblFoldWidth;
                if ((que & 2) == 2)
                {
                    g.DrawLine(oSolidPen, 0, (float)y, (float)dblPaperWidth, (float)y);
                    g.DrawLine(oSolidPen, 0, (float)(y - dblFoldWidth), 0, (float)(float)(y + dblFoldWidth));
                    g.DrawLine(oSolidPen, (float)dblPaperWidth, (float)(y - dblFoldWidth), (float)dblPaperWidth, (float)(float)(y + dblFoldWidth));
                }

                double dblAlt = 1.0;
				if (bAlternate)
					if ((i & 2) == 0)
						dblAlt = 1.0;
					else
						dblAlt = -1.0;

                if ((que & 2) == 2)
                {
                    g.DrawLine(oSolidPen, (float)(x1 - dblAlt * dxa2), (float)(y + dblFoldWidth), (float)(x1 - dblAlt * dxa1), (float)(y + dblFoldWidth));
                    g.DrawLine(oSolidPen, (float)(x2 + dblAlt * dxa1), (float)(y + dblFoldWidth), (float)(x2 + dblAlt * dxa2), (float)(y + dblFoldWidth));
                }
                if ((que & 1) == 1)
                {
                    //g.DrawLine(oDottedPen, 0, (float)(y + dblFoldWidth), (float)(x1 - dblAlt * dxa2), (float)(y + dblFoldWidth));
                    g.DrawLine(oDottedPen, (float)(x1 - dblAlt * dxa1), (float)(y + dblFoldWidth), (float)(x2 + dblAlt * dxa1), (float)(y + dblFoldWidth));
                    //g.DrawLine(oDottedPen, (float)(x2 + dblAlt * dxa1), (float)(y + dblFoldWidth), (float)(dblPaperWidth), (float)(y + dblFoldWidth));
                }

				if ( bAlternate )
					if ( (i&2)== 0 )
						dblAlt = -1.0;
					else
						dblAlt = 1.0;

                if ((que & 2) == 2)
                {
                    g.DrawLine(oSolidPen, (float)(x1 - dblAlt * dxa1), (float)(y - dblFoldWidth), (float)(x1), (float)(y));
                    g.DrawLine(oSolidPen, (float)(x2 + dblAlt * dxa1), (float)(y - dblFoldWidth), (float)(x2), (float)(y));
                }
                if ((que & 1) == 1) //exterior bajo
                {
                    g.DrawLine(oDottedPen, (float)(x1 - dblAlt * dxa2), (float)(y - dblFoldWidth), (float)(x1), (float)(y));
                    g.DrawLine(oDottedPen, (float)(x2 + dblAlt * dxa2), (float)(y - dblFoldWidth), (float)(x2), (float)(y));
                }

				if (bAlternate) 
					if ( (i & 2) == 0)
						dblAlt = 1.0;
					else
						dblAlt = -1.0;

                if ((que & 2) == 2)
                {

                    g.DrawLine(oSolidPen, (float)(x1 - dblAlt * dxa1), (float)(y + dblFoldWidth), (float)(x1), (float)(y));
                    g.DrawLine(oSolidPen, (float)(x2 + dblAlt * dxa1), (float)(y + dblFoldWidth), (float)(x2), (float)(y));
                }

                if ((que & 1) == 1) //exterior alto
                {
                    g.DrawLine(oDottedPen, (float)(x1 - dblAlt * dxa2), (float)(y + dblFoldWidth), (float)(x1), (float)(y));
                    g.DrawLine(oDottedPen, (float)(x2 + dblAlt * dxa2), (float)(y + dblFoldWidth), (float)(x2), (float)(y));
                }
			}

		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			UpdateCalculations();
			printPreviewDialog1.Document = printDocument1;
			nPage = 0;
			if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
			{
			}
		}

        public String GenBellows(Graphics g)
        {      
            String solido = "";
            String punteado ="\n(MSG,Oprime CONTINUA)\nM0\n";
            String Preambulo = "G01 F100\n";
            String Corolario = "G00 Z5 M2\n";
            if (dlg.gobierno.Checked) Corolario = "G00 Z5 A0 M2\n";
            int continua = 0;
            int discontinua = 1;
            double dblFoldWidth;
            double dblHeight;
            gradbeta = 0.0f;

            if (!double.TryParse(txtFoldWidth.Text, out dblFoldWidth))
                return(Preambulo);

            if (!double.TryParse(txtHeight.Text, out dblHeight))
                return(Preambulo);

            double dblPaperHeight = (double)nFolds * dblFoldWidth;

            Pen oSolidPen;
            Pen oDottedPen;
            if (!m_bCalculationError)
            {
                oSolidPen = new Pen(Color.Black, 0.25f);
                oDottedPen = new Pen(Color.Red, 0.25f);
            }
            else
            {
                oSolidPen = new Pen(Color.Green, 0.25f);
                oDottedPen = new Pen(Color.Cyan, 0.25f);
            }

            oDottedPen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDot;

            double a1 = 72.57 / 180.0 * Math.PI;
            double a2 = 27.57 / 180.0 * Math.PI;

            double x1 = dblHeight + dblFoldWidth;
            double x2 = dblPaperWidth - x1;

            bool bAlternate = chkAlternateFolds.Checked;

            g.DrawRectangle(oSolidPen, 0, 0, (float)dblPaperWidth, (float)dblPaperHeight);// marco
            g.DrawLine(oSolidPen, 0, 0, (float)dblPaperWidth, (float)dblPaperHeight);//linea de seguimiento
            solido += Preambulo;
            punteado += Preambulo;
            dvC = float.Parse(dlg.txtCutterOffset.Text, CultureInfo.GetCultureInfo("en-GB"));
            angD = float.Parse(dlg.numericUpDown1.Text);
            xC = float.Parse(dlg.CentroX.Text, CultureInfo.GetCultureInfo("en-GB"));
            yC = float.Parse(dlg.CentroY.Text, CultureInfo.GetCultureInfo("en-GB"));

            for (int i = 0; i < nFolds; i += 2)
            {
                double dxa1 = dblFoldWidth / Math.Tan(a1);
                double dxa2 = dblFoldWidth / Math.Tan(a2);

                double y = (double)(i + 1) * dblFoldWidth;
                g.DrawLine(oSolidPen, 0, (float)y, (float)dblPaperWidth, (float)y);
                solido += A_Linea_CNC (continua, 0, (float)y, (float)dblPaperWidth, (float)y);

                g.DrawLine(oSolidPen, 0, (float)(y - dblFoldWidth), 0, (float)(float)(y + dblFoldWidth));
                solido += A_Linea_CNC (continua, 0, (float)(y - dblFoldWidth), 0, (float)(float)(y + dblFoldWidth));

                g.DrawLine(oSolidPen, (float)dblPaperWidth, (float)(y - dblFoldWidth), (float)dblPaperWidth, (float)(float)(y + dblFoldWidth));
                solido += A_Linea_CNC (continua,(float)dblPaperWidth, (float)(y - dblFoldWidth), (float)dblPaperWidth, (float)(float)(y + dblFoldWidth));

                double dblAlt = 1.0;
                if (bAlternate)
                    if ((i & 2) == 0)
                        dblAlt = 1.0;
                    else
                        dblAlt = -1.0;

                g.DrawLine(oDottedPen, 0, (float)(y + dblFoldWidth), (float)(x1 - dblAlt * dxa2), (float)(y + dblFoldWidth));
                punteado += A_Linea_CNC (discontinua, 0, (float)(y + dblFoldWidth), (float)(x1 - dblAlt * dxa2), (float)(y + dblFoldWidth));

                g.DrawLine(oSolidPen, (float)(x1 - dblAlt * dxa2), (float)(y + dblFoldWidth), (float)(x1 - dblAlt * dxa1), (float)(y + dblFoldWidth));
                solido += A_Linea_CNC(continua, (float)(x1 - dblAlt * dxa2), (float)(y + dblFoldWidth), (float)(x1 - dblAlt * dxa1), (float)(y + dblFoldWidth));

                g.DrawLine(oDottedPen, (float)(x1 - dblAlt * dxa1), (float)(y + dblFoldWidth), (float)(x2 + dblAlt * dxa1), (float)(y + dblFoldWidth));
                punteado += A_Linea_CNC(discontinua, (float)(x1 - dblAlt * dxa1), (float)(y + dblFoldWidth), (float)(x2 + dblAlt * dxa1), (float)(y + dblFoldWidth));

                g.DrawLine(oSolidPen, (float)(x2 + dblAlt * dxa1), (float)(y + dblFoldWidth), (float)(x2 + dblAlt * dxa2), (float)(y + dblFoldWidth));
                solido += A_Linea_CNC(continua,(float)(x2 + dblAlt * dxa1), (float)(y + dblFoldWidth), (float)(x2 + dblAlt * dxa2), (float)(y + dblFoldWidth));

                g.DrawLine(oDottedPen, (float)(x2 + dblAlt * dxa1), (float)(y + dblFoldWidth), (float)(dblPaperWidth), (float)(y + dblFoldWidth));
                punteado += A_Linea_CNC(discontinua, (float)(x2 + dblAlt * dxa1), (float)(y + dblFoldWidth), (float)(dblPaperWidth), (float)(y + dblFoldWidth));


                if (bAlternate)
                    if ((i & 2) == 0)
                        dblAlt = -1.0;
                    else
                        dblAlt = 1.0;

                g.DrawLine(oDottedPen, (float)(x1 - dblAlt * dxa2), (float)(y - dblFoldWidth), (float)(x1), (float)(y));
                punteado += A_Linea_CNC(discontinua, (float)(x1 - dblAlt * dxa2), (float)(y - dblFoldWidth), (float)(x1), (float)(y));

                g.DrawLine(oSolidPen, (float)(x1 - dblAlt * dxa1), (float)(y - dblFoldWidth), (float)(x1), (float)(y));
                solido += A_Linea_CNC(continua,(float)(x1 - dblAlt * dxa1), (float)(y - dblFoldWidth), (float)(x1), (float)(y));

                g.DrawLine(oDottedPen, (float)(x2 + dblAlt * dxa2), (float)(y - dblFoldWidth), (float)(x2), (float)(y));
                punteado += A_Linea_CNC(discontinua,(float)(x2 + dblAlt * dxa2), (float)(y - dblFoldWidth), (float)(x2), (float)(y));

                g.DrawLine(oSolidPen, (float)(x2 + dblAlt * dxa1), (float)(y - dblFoldWidth), (float)(x2), (float)(y));
                solido += A_Linea_CNC(continua,(float)(x2 + dblAlt * dxa1), (float)(y - dblFoldWidth), (float)(x2), (float)(y));

                if (bAlternate)
                    if ((i & 2) == 0)
                        dblAlt = 1.0;
                    else
                        dblAlt = -1.0;

                g.DrawLine(oDottedPen, (float)(x1 - dblAlt * dxa2), (float)(y + dblFoldWidth), (float)(x1), (float)(y));
                punteado += A_Linea_CNC(discontinua,(float)(x1 - dblAlt * dxa2), (float)(y + dblFoldWidth), (float)(x1), (float)(y));

                g.DrawLine(oSolidPen, (float)(x1 - dblAlt * dxa1), (float)(y + dblFoldWidth), (float)(x1), (float)(y));
                solido += A_Linea_CNC(continua, (float)(x1 - dblAlt * dxa1), (float)(y + dblFoldWidth), (float)(x1), (float)(y));

                g.DrawLine(oDottedPen, (float)(x2 + dblAlt * dxa2), (float)(y + dblFoldWidth), (float)(x2), (float)(y));
                punteado += A_Linea_CNC(discontinua,(float)(x2 + dblAlt * dxa2), (float)(y + dblFoldWidth), (float)(x2), (float)(y));

                g.DrawLine(oSolidPen, (float)(x2 + dblAlt * dxa1), (float)(y + dblFoldWidth), (float)(x2), (float)(y));
                solido += A_Linea_CNC(continua,(float)(x2 + dblAlt * dxa1), (float)(y + dblFoldWidth), (float)(x2), (float)(y));

            }
            punteado += Corolario;
            return (solido+punteado);
        }
        private (float,float)  Rotn(float x, float y, float angulo)
        {
            double Deg2Rad = Math.PI / 180.0;
            double radio = Math.Sqrt(x * x + y * y);
            double theta = Math.Atan2(y, x);
            double newx = radio * Math.Cos(theta + angulo * Deg2Rad);
            double newy = radio * Math.Sin(theta + angulo * Deg2Rad);
            return ((float) newx, (float) newy);
        }
        private (float, float) DespP (float distxy, float gradangulo)
        {
            double Deg2Rad = Math.PI / 180.0;
            double newx = distxy * Math.Cos(gradangulo * Deg2Rad);
            double newy = distxy * Math.Sin(gradangulo * Deg2Rad);
            return ((float)newx, (float)newy);
        }

        private string A_Linea_CNC (int tipo, float xxx1, float yyy1, float xxx2, float yyy2)
        {
            String Linea = "";
            //Centra
            xxx1 -= xC; xxx2 -= xC;
            yyy1 -= yC; yyy2 -= yC;
            if (dlg.Resorte.Checked) gradbeta = 0.0f; 
            //Rotamos el dibujo
            (float xx1, float yy1) = Rotn(xxx1, yyy1,angD);
            (float xx2, float yy2) = Rotn(xxx2, yyy2,angD);
            //Lo espejamos en coordenadas maquina si es reverso
            if (tipo == 1)
                {
                if (dlg.reverso.CheckedItems.Contains("EspejoX") == true) { xx1 = -xx1; xx2 = -xx2; }
                if (dlg.reverso.CheckedItems.Contains("EspejoY") == true) { yy1 = -yy1; yy2 = -yy2; }
                }
            //En realidad tengo que desplazar los puntos 'desvio'  segun la linea
            double radalfa = Math.Atan2(yy2 - yy1, xx2 - xx1);
            float gradalfa = (float)(radalfa * (180.0 / Math.PI));
            Linea += "G00 Z5\n";
            //Si podemos orientar la cuchilla...
            if (dlg.gobierno.Checked)
                {
                Linea += ("A" + gradalfa.ToString("0.000", CultureInfo.GetCultureInfo("en-GB")) + '\n');
                gradbeta = gradalfa;
                }
            (float Dx, float Dy) = DespP(dvC, gradalfa);
            (float dx, float dy) = DespP(dvC, gradbeta);
            Linea += ("X" + (xx1 + dx).ToString("0.000", CultureInfo.GetCultureInfo("en-GB")));
            Linea += (" Y" + (yy1 + dy).ToString("0.000", CultureInfo.GetCultureInfo("en-GB"))+ '\n');
            Linea += "G01 Z0\n";
            if (!dlg.gobierno.Checked)
                {
                if (gradalfa > gradbeta) { Linea += ("G3 X");}
                else { Linea += ("G2 X");}
                Linea += (xx1 + Dx).ToString("0.000", CultureInfo.GetCultureInfo("en-GB"));
                Linea += (" Y" + (yy1 + Dy).ToString("0.000", CultureInfo.GetCultureInfo("en-GB")));
                Linea += (" I" + (-dx).ToString("0.000", CultureInfo.GetCultureInfo("en-GB")));
                Linea += (" J" + (-dy).ToString("0.000", CultureInfo.GetCultureInfo("en-GB")) + '\n');
                }
            Linea += ("G01 X" + (xx2 + Dx).ToString("0.000", CultureInfo.GetCultureInfo("en-GB")));
            Linea += (" Y" + (yy2 + Dy).ToString("0.000", CultureInfo.GetCultureInfo("en-GB")) + '\n');
            gradbeta = gradalfa;
            return Linea;
        }
        public String RayaFuelles()
        {
            String solido = "";
            String punteado = "\n(MSG,Oprime CONTINUA)\nM0\n";
            String Preambulo = "G01 F100\n";
            String Corolario = "G00 Z5 M2\n";
            if (dlg.gobierno.Checked) Corolario = "G00 Z5 A0 M2\n";
            int continua = 0;
            int discontinua = 1;
            double dblFoldWidth;
            double dblHeight;
            gradbeta = 0.0f;

            if (!double.TryParse(txtFoldWidth.Text, out dblFoldWidth))
                return ("Error en Ancho");

            if (!double.TryParse(txtHeight.Text, out dblHeight))
                return ("Error en Alto");

            double dblPaperHeight = (double)nFolds * dblFoldWidth;

            double a1 = 72.57 / 180.0 * Math.PI;
            double a2 = 27.57 / 180.0 * Math.PI;

            double x1 = dblHeight + dblFoldWidth;
            double x2 = dblPaperWidth - x1;

            bool bAlternate = chkAlternateFolds.Checked;

            solido += Preambulo;
            punteado += Preambulo;
            dvC = float.Parse(dlg.txtCutterOffset.Text, CultureInfo.GetCultureInfo("en-GB"));
            angD = float.Parse(dlg.numericUpDown1.Text);
            xC = float.Parse(dlg.CentroX.Text, CultureInfo.GetCultureInfo("en-GB"));
            yC = float.Parse(dlg.CentroY.Text, CultureInfo.GetCultureInfo("en-GB"));

            for (int i = 0; i < nFolds; i += 2)
            {
                double dxa1 = dblFoldWidth / Math.Tan(a1);
                double dxa2 = dblFoldWidth / Math.Tan(a2);

                double y = (double)(i + 1) * dblFoldWidth;
                solido += A_Linea_CNC(continua, 0, (float)y, (float)dblPaperWidth, (float)y);

                solido += A_Linea_CNC(continua, 0, (float)(y - dblFoldWidth), 0, (float)(float)(y + dblFoldWidth));

                solido += A_Linea_CNC(continua, (float)dblPaperWidth, (float)(y - dblFoldWidth), (float)dblPaperWidth, (float)(float)(y + dblFoldWidth));

                double dblAlt = 1.0;
                if (bAlternate)
                    if ((i & 2) == 0)
                        dblAlt = 1.0;
                    else
                        dblAlt = -1.0;

                punteado += A_Linea_CNC(discontinua, 0, (float)(y + dblFoldWidth), (float)(x1 - dblAlt * dxa2), (float)(y + dblFoldWidth));

                solido += A_Linea_CNC(continua, (float)(x1 - dblAlt * dxa2), (float)(y + dblFoldWidth), (float)(x1 - dblAlt * dxa1), (float)(y + dblFoldWidth));

                punteado += A_Linea_CNC(discontinua, (float)(x1 - dblAlt * dxa1), (float)(y + dblFoldWidth), (float)(x2 + dblAlt * dxa1), (float)(y + dblFoldWidth));

                solido += A_Linea_CNC(continua, (float)(x2 + dblAlt * dxa1), (float)(y + dblFoldWidth), (float)(x2 + dblAlt * dxa2), (float)(y + dblFoldWidth));

                punteado += A_Linea_CNC(discontinua, (float)(x2 + dblAlt * dxa1), (float)(y + dblFoldWidth), (float)(dblPaperWidth), (float)(y + dblFoldWidth));


                if (bAlternate)
                    if ((i & 2) == 0)
                        dblAlt = -1.0;
                    else
                        dblAlt = 1.0;

                punteado += A_Linea_CNC(discontinua, (float)(x1 - dblAlt * dxa2), (float)(y - dblFoldWidth), (float)(x1), (float)(y));

                solido += A_Linea_CNC(continua, (float)(x1 - dblAlt * dxa1), (float)(y - dblFoldWidth), (float)(x1), (float)(y));

                punteado += A_Linea_CNC(discontinua, (float)(x2 + dblAlt * dxa2), (float)(y - dblFoldWidth), (float)(x2), (float)(y));

                solido += A_Linea_CNC(continua, (float)(x2 + dblAlt * dxa1), (float)(y - dblFoldWidth), (float)(x2), (float)(y));

                if (bAlternate)
                    if ((i & 2) == 0)
                        dblAlt = 1.0;
                    else
                        dblAlt = -1.0;

                punteado += A_Linea_CNC(discontinua, (float)(x1 - dblAlt * dxa2), (float)(y + dblFoldWidth), (float)(x1), (float)(y));

                solido += A_Linea_CNC(continua, (float)(x1 - dblAlt * dxa1), (float)(y + dblFoldWidth), (float)(x1), (float)(y));

                punteado += A_Linea_CNC(discontinua, (float)(x2 + dblAlt * dxa2), (float)(y + dblFoldWidth), (float)(x2), (float)(y));

                solido += A_Linea_CNC(continua, (float)(x2 + dblAlt * dxa1), (float)(y + dblFoldWidth), (float)(x2), (float)(y));

            }
            punteado += Corolario;
            return (solido + punteado);
        }

        private void btnPrintSetup_Click(object sender, EventArgs e)
		{
			UpdateCalculations();
			printDialog1.Document = printDocument1;
			if (printDialog1.ShowDialog() == DialogResult.OK)
			{
				nPage = 0;
				printDocument1.Print();
			}
		}

		double dblPageWidth;
		double dblPageHeight;
		int nPartsVertical;
		int nPartsHorizontal;

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			if (nPage == 0)
			{
				// First page.  Compute the number of pages we will need.
				if (e.PageSettings.Landscape)
				{
					dblPageWidth = (double)e.PageSettings.PrintableArea.Height * 25.4 / 100.0;
					dblPageHeight = (double)e.PageSettings.PrintableArea.Width * 25.4 / 100.0;
				}
				else
				{
					dblPageWidth = (double)e.PageSettings.PrintableArea.Width * 25.4 / 100.0;
					dblPageHeight = (double)e.PageSettings.PrintableArea.Height * 25.4 / 100.0;
				}

				// 10mm overlap.
				dblPageWidth -= 10;
				dblPageHeight -= 10;

				double dblFoldWidth = double.Parse(txtFoldWidth.Text);
				double dblPaperHeight = (double)nFolds * dblFoldWidth;

				// Parts vertically.
				nPartsVertical = (int)(dblPaperHeight / dblPageHeight + 1.0);

				// Parts horizontal.
				nPartsHorizontal = (int)(dblPaperWidth / dblPageWidth + 1.0);

				nPages = nPartsVertical * nPartsHorizontal;
			}

			int nPageX = nPage / nPartsVertical;
			int nPageY = nPage % nPartsVertical;

			e.Graphics.PageUnit = GraphicsUnit.Millimeter;
			e.Graphics.TranslateTransform(-(float)nPageX * (float)dblPageWidth, -(float)nPageY * (float)dblPageHeight);

			DrawBellows(e.Graphics);
			if (nPage+1 < nPages )
			{
				nPage++;
				e.HasMorePages = true;
			}
		}

		private void btnPageSetup_Click(object sender, EventArgs e)
		{
			pageSetupDialog1.Document = printDocument1;
			pageSetupDialog1.ShowDialog();
		}

		private void printDocument1_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			nPage = 0;
		}

		private void StoreItems()
		{
			if (m_CurrentElement != null)
			{
				m_CurrentElement.Inversions = (int)cboInversions.SelectedItem;
				m_CurrentElement.BellowsShape = (FuellesConfigElement.EBellowsShape)cboShape.SelectedItem;
				m_CurrentElement.FoldWidth = Double.Parse(txtFoldWidth.Text);
				m_CurrentElement.Height = Double.Parse(txtHeight.Text);
				m_CurrentElement.Width = Double.Parse(txtWidth.Text);
				m_CurrentElement.Length = Double.Parse(txtLength.Text);
				m_CurrentElement.MountFolds = Int32.Parse(txtMountFolds.Text);
				m_CurrentElement.AlternateFolds = chkAlternateFolds.Checked;
			}
		}

		private void cboConfig_SelectedIndexChanged(object sender, EventArgs e)
		{
			// Store old values
			StoreItems();
			// Load new values
			m_CurrentElement = (FuellesConfigElement)(((ComboBox)sender).SelectedItem);

			cboInversions.SelectedItem = m_CurrentElement.Inversions;
			cboShape.SelectedItem = m_CurrentElement.BellowsShape;
			txtFoldWidth.Text = m_CurrentElement.FoldWidth.ToString();
			txtHeight.Text = m_CurrentElement.Height.ToString();
			txtWidth.Text = m_CurrentElement.Width.ToString();
			txtLength.Text = m_CurrentElement.Length.ToString();
			txtMountFolds.Text = m_CurrentElement.MountFolds.ToString();
			chkAlternateFolds.Checked = m_CurrentElement.AlternateFolds;
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			StoreItems();
			((FuelleConfig)m_Config.Sections["FuelleConfig"]).SelectedItem = cboConfig.SelectedItem.ToString();
			m_Config.Save(ConfigurationSaveMode.Full);
		}

		private void btnNewConfig_Click(object sender, EventArgs ev)
		{
			NewName frm = new NewName();
			if ( frm.ShowDialog(this) == DialogResult.OK )
			{
				FuellesCollection col = ((FuelleConfig)m_Config.Sections["FuelleConfig"]).Bellows;
				if (col[frm.NameText] != null)
				{
					MessageBox.Show(this,"Configuration '" + frm.NameText + "' already exists");
				}
				else
				{
					FuellesConfigElement e = new FuellesConfigElement();
					e.Name = frm.NameText;
					col.Add(e);
					cboConfig.Items.Add( e );
					cboConfig.SelectedItem = e;
				}
			}
		}

		private void cboShape_SelectedIndexChanged(object sender, EventArgs e)
		{
			UpdateCalculations();
		}

		private void cboInversions_SelectedIndexChanged(object sender, EventArgs e)
		{
			UpdateCalculations();
		}

		private void chkAlternateFolds_CheckedChanged(object sender, EventArgs e)
		{
			UpdateCalculations();
		}

		private void txtMountFolds_Leave(object sender, EventArgs e)
		{
			UpdateCalculations();
		}

		private void txtWidth_Leave(object sender, EventArgs e)
		{
			UpdateCalculations();
		}

		private void txtHeight_Leave(object sender, EventArgs e)
		{
			UpdateCalculations();
		}

		private void txtLength_Leave(object sender, EventArgs e)
		{
			UpdateCalculations();
		}

        private void Pliegues_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateCalculations();
        }

        private void txtFoldWidth_Leave(object sender, EventArgs e)
		{
			UpdateCalculations();
		}

        private void btnGCode_Click(object sender, EventArgs e)
        {
           dlg = new GenerateGCode(this);
            dlg.CentroX.Text = (this.dblPaperWidth/2).ToString();
            dlg.CentroY.Text = (this.nFolds* this.dblFoldWidth / 2).ToString();
            if ( dlg.ShowDialog(this) == DialogResult.OK )
            {
            }
        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }
    }
}