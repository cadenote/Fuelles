// Fuelles is a forked work of Bellows Program obtained from www.franksworkshop.com Copyright (C) 2008, Frank Tkalcevic.
// Bellows - bellows fold pattern printer, based on US Patent No 6,054,194,
// Mathematically optimized family of ultra low distortion bellow fold patterns, Nathan R. Kane.
// Also the project includes Cohen–Sutherland clipping algorithm extracted from wikipedia

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
        double dblPaperHeight;
        double dblFoldWidth;
        float[] gradbetas = new float[3] { 0,0,0 };
        int nFolds;
        int nPage;
        int nPages;
        Configuration m_Config;
        FuellesConfigElement m_CurrentElement;
        bool m_bCalculationError;
        GenerateGCode dlg;
        float dvC, angD, xC, yC;


        public Form1()
        {
            m_bCalculationError = true;

            InitializeComponent();
            listinv.Items.AddRange(new object[] { "72.57", "27.57", "0" });
            //listinv.Items.AddRange(new object[] { "60", "30","0" });
            m_Config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            FuelleConfig cfg = (FuelleConfig)m_Config.Sections["ParamdeFuelle"];
            if (cfg == null)
            {
                cfg = new FuelleConfig();
                m_Config.Sections.Add("ParamdeFuelle", cfg);
                cfg = (FuelleConfig)m_Config.Sections["ParamdeFuelle"];
                cfg.SectionInformation.ForceSave = true;
            }

            foreach (FuellesConfigElement b in cfg.Fuelless)
            {
                cboConfig.Items.Add(b);
            }
            int nItem = cboConfig.FindStringExact(((FuelleConfig)m_Config.Sections["ParamdeFuelle"]).SelectedItem);
            if (nItem >= 0)
                cboConfig.SelectedIndex = nItem;
            else
            {
                if (cboConfig.Items.Count > 0)
                    cboConfig.SelectedIndex = 0;
            }

            printDocument1.PrinterSettings = printDialog1.PrinterSettings;
            pliegues.SetItemChecked(0, true); //Ver picos
            pliegues.SetItemChecked(1, true); //Ver valles
            actualizapared();
            UpdateCalculations();
        }

        public bool UpdateCalculations()
        {
            double angradian, dx,cabe;
            bool bRet = false;
            try
            {
                double dblLength = double.Parse(txtLength.Text);//longitud a cubrir
                int nMountFolds = int.Parse(txtMountFolds.Text);//pliegue de montaje
                double dblExtensionAngle = 120.0;
                double dblWidth = double.Parse(txtWidth.Text);//ancho interior requrido
                double dblHeight = double.Parse(txtHeight.Text);//alto interior

                dblFoldWidth = double.Parse(txtFoldWidth.Text);//anchura pliegue

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
                double dblTopWidth = dblWidth + 2 * dblFoldWidth;//ancho exterior necesario si pliegues hacia dentro
                double dblSideHeight = dblHeight + dblFoldWidth;//alto exterior necesario si pliegues hacia dentro

                dblPaperWidth = dblTopWidth + 2 * dblSideHeight;//ancho de papel

                lblPageWidth.Text = dblPaperWidth.ToString() + " mm";
                lblPageHeight.Text = ((double)nFolds * dblFoldWidth).ToString() + "mm";
                if(chkAlternateFolds.Checked) // comprueba si pliegues interiores caben
                {
                    for(int i=0;i< listinv.Items.Count-1;i++)
                    {
                        angradian = Math.PI / 180.0 * float.Parse(listinv.Items[i].ToString(), CultureInfo.GetCultureInfo("en-GB"));
                        dx = dblFoldWidth / Math.Tan(angradian);
                        cabe= 1/((dblTopWidth/2 >  dx) ? 1:0);
                    }
                }

                bRet = true;
                m_bCalculationError = false;
            }
            catch (Exception)
            {
                bRet = false;
                m_bCalculationError = true;
            }

            panel1.Invalidate();
            return bRet;
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            PintaFuelles(e.Graphics);
        }
        private void PintaFuelles(Graphics g)
        {
            double dblFoldWidth;
            double dblHeight;
            double dblWidth;
            double angradian,angradian0;
            double y,dx,d0x,dvx;
            double p1x, p1y, p2x, p2y,v1x,v2x;
            int impar,tam,a,prueba;
            byte que = 0;
            byte trazo;
            bool toca;

            if (pliegues.CheckedItems.Count > 0)
            {
                if (pliegues.CheckedItems.Contains("Picos") == true) que = 2;
                if (pliegues.CheckedItems.Contains("Valles") == true) que += 1;
            }
            if (!double.TryParse(txtFoldWidth.Text, out dblFoldWidth))
                return;

            if (!double.TryParse(txtHeight.Text, out dblHeight))
                return;
            if (!double.TryParse(txtWidth.Text, out dblWidth))
                return;

            double dblTopWidth = dblWidth + 2 * dblFoldWidth;//ancho exterior necesario si pliegues hacia dentro
            double dblSideHeight = dblHeight + dblFoldWidth;//alto exterior necesario si pliegues hacia dentro
            dblPaperHeight = (double)nFolds * dblFoldWidth;
            double nuevorig = dblTopWidth + dblSideHeight;
            bool bAlternate = chkAlternateFolds.Checked;// alternar

            Pen oSolidPen;
            Pen oDottedPen;
            Pen lapiz;
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

            g.DrawRectangle(oSolidPen, 0, 0, (float)dblPaperWidth, (float)dblPaperHeight);// marco
            g.DrawLine(oSolidPen, 0, 0, (float)dblPaperWidth, (float)dblPaperHeight);//linea de seguimiento

            //quebradas verticales duplico el bucle para no interrupir trazados
            for (int ix=0;ix < listinv.Items.Count-1 ; ix++) // izquierdo por inversiones pero el cero final no
            {
                impar = ix % 2;
                lapiz = (impar == 0) ?  oSolidPen : oDottedPen;
                trazo = (impar==0) ? (byte) 2 : (byte) 1;
                angradian = Math.PI/180.0*float.Parse(listinv.Items[ix].ToString(), CultureInfo.GetCultureInfo("en-GB"));
                for (int plieg=0; plieg< nFolds; plieg++)
                {
                    impar = plieg % 2;
                    y = (double)plieg * dblFoldWidth;
                    dx = dblFoldWidth / Math.Tan(angradian);
                    toca = ((plieg+1) %4 ==0 | (plieg % 4) == 0);
                    if (bAlternate & toca) dx = -dx;
                    (p1x,p1y,p2x,p2y)=Encaja( dblSideHeight + (impar - 1) * dx, y, dblSideHeight - dx * impar, y + dblFoldWidth);
                    if ((que & trazo) != 0 ) g.DrawLine(lapiz, (float)p1x, (float)p1y,(float)p2x,(float)p2y);
                }
            }
            
            for (int ix = 0; ix < listinv.Items.Count - 1; ix++) // derecho por inversiones pero el cero final no
            {
                impar = ix % 2;
                lapiz = (impar == 0) ? oSolidPen : oDottedPen;
                trazo = (impar == 0) ? (byte)2 : (byte)1;
                angradian = Math.PI / 180.0 * float.Parse(listinv.Items[ix].ToString(), CultureInfo.GetCultureInfo("en-GB"));
                for (int plieg = 0; plieg < nFolds; plieg++)
                {
                    impar = plieg % 2;
                    y = (double)plieg * dblFoldWidth;
                    dx = dblFoldWidth / Math.Tan(angradian);
                    toca = ((plieg + 1) % 4 == 0 | (plieg % 4) == 0);
                    if (bAlternate & toca) dx = -dx;
                    (p1x, p1y, p2x, p2y) = Encaja(nuevorig - (impar - 1) * dx, y, nuevorig + dx * impar, y + dblFoldWidth);
                    if ((que & trazo) != 0) g.DrawLine(lapiz, (float)p1x, (float)p1y, (float)p2x, (float)p2y);
                }
            }
            // Horizontales
            angradian0 = Math.PI / 180.0 * float.Parse(listinv.Items[0].ToString(), CultureInfo.GetCultureInfo("en-GB"));//primera inversion
            d0x = dblFoldWidth / Math.Tan(angradian0);
            int externo;
            for (int plieg = 1; plieg < nFolds; plieg++)
            {
                toca = (plieg % 4 == 0 & bAlternate);
                externo = plieg % 2; //Los externos son los impares
                y = (double)plieg * dblFoldWidth;
                v1x = 0;
                if (externo == 0) //Izquierdos internos
                    {
                    tam = listinv.Items.Count - 1;
                    for (int ix = tam-1; ix >=0; ix--)
                        {
                        impar = ix  % 2;
                        lapiz = (impar == 1) ? oDottedPen : oSolidPen;
                        trazo = (impar == 1) ? (byte)1 : (byte)2;
                        prueba = ix - (a= toca ? 1 : 0) * (tam-1);
                        angradian = Math.PI / 180.0 * float.Parse(listinv.Items[Math.Abs(prueba)].ToString(), CultureInfo.GetCultureInfo("en-GB"));
                        dvx = dblFoldWidth / Math.Tan(angradian);
                        if (toca) dvx = -dvx;
                        v2x = dblSideHeight - dvx;
                        (p1x, p1y, p2x, p2y) = Encaja(v1x, y, v2x, y);
                        try {
                            if ((que & trazo) != 0) g.DrawLine(lapiz, (float)p1x, (float)p1y, (float)p2x, (float)p2y); }
                        catch { }
                        v1x = v2x;
                        }
                    }
                else //Izquierdos externos
                {
                    lapiz = (listinv.Items.Count % 2 == 1) ? oSolidPen : oDottedPen;
                    trazo = (listinv.Items.Count % 2 == 1) ? (byte)2 : (byte)1;
                    (p1x, p1y, p2x, p2y) = Encaja(0, y, dblSideHeight, y);
                    if ((que & trazo) != 0) g.DrawLine(lapiz, (float)p1x, (float)p1y, (float)p2x, (float)p2y);
                    v1x = dblSideHeight;
                    }
                //Central
                lapiz = (externo == 1) ? oSolidPen : oDottedPen;
                trazo = (externo == 1) ? (byte)2 : (byte)1;
                tam = listinv.Items.Count - 1;
                prueba = (a = toca ? 1 : 0) * (tam-1);
                angradian = Math.PI / 180.0 * float.Parse(listinv.Items[Math.Abs(prueba)].ToString(), CultureInfo.GetCultureInfo("en-GB"));
                dvx = dblFoldWidth / Math.Tan(angradian);
                if (toca) dvx = -dvx;
                v2x = nuevorig - dvx*(externo-1);
                (p1x, p1y, p2x, p2y) = Encaja(v1x, y, v2x, y);
                if ((que & trazo) != 0) g.DrawLine(lapiz, (float)p1x, (float)p1y, (float)p2x, (float)p2y);
                v1x = v2x;
                if (externo == 0)//Derechos Internos
                {
                    for (int ix = 1; ix < tam ; ix++) 
                    {
                        impar = ix % 2;
                        lapiz = (impar == 0) ? oDottedPen : oSolidPen;
                        trazo = (impar == 0) ? (byte)1 : (byte)2;
                        prueba = ix - (a = toca ? 1 : 0) * (tam-1);
                        angradian = Math.PI / 180.0 * float.Parse(listinv.Items[Math.Abs(prueba)].ToString(), CultureInfo.GetCultureInfo("en-GB"));
                        dvx = dblFoldWidth / Math.Tan(angradian);
                        if (toca) dvx = -dvx;
                        v2x = nuevorig + dvx;
                        (p1x, p1y, p2x, p2y) = Encaja(v1x, y, v2x, y);
                        try { if ((que & trazo) != 0) g.DrawLine(lapiz, (float)p1x, (float)p1y, (float)p2x, (float)p2y); }
                        catch { }
                        v1x = v2x; if (v1x > dblPaperWidth) v1x = dblPaperWidth;
                    }
                    lapiz = (listinv.Items.Count % 2 == 0) ? oSolidPen : oDottedPen;
                    trazo = (listinv.Items.Count % 2 == 0) ? (byte)2 : (byte)1;
                    v2x = dblPaperWidth; //ultima derecha
                    (p1x, p1y, p2x, p2y) = Encaja(v1x, y, v2x, y);
                    if ((que & trazo) != 0) g.DrawLine(lapiz, (float)p1x, (float)p1y, (float)p2x, (float)p2y);
                }
                else //Derechos Externos
                {
                    lapiz = (listinv.Items.Count % 2 == 1) ? oSolidPen : oDottedPen;
                    trazo = (listinv.Items.Count % 2 == 1) ? (byte)2 : (byte)1;
                    (p1x, p1y, p2x, p2y) = Encaja(nuevorig, y, dblPaperWidth, y);
                    if ((que & trazo) != 0) g.DrawLine(lapiz, (float)p1x, (float)p1y, (float)p2x, (float)p2y);
                }

            }
        } /*PintaFuelles*/
        public String GenFuelles(/*byte queparte*/ )
        {
            /*
            Este procedimiento solo es valido anverso (2) o reverso (1) pero no ambos(3)
            porque en el caso de ambos(3)las lineas picos y valles se generian de manera no consecutiva
            y por tanto las gragbetas anteriores serian erroneas.
            Esto se puede arreglar guardando esos datos por separado o llamando al procedimiento dos veces.
            */
            string espera = "\n(MSG,Oprime CONTINUA)\nM0\n";
            string Preambulo = "G01 F100\n";
            string Corolario = "G00 Z5 M2\n";
            string[] cnclin = new string [3];
            if (dlg.gobierno.Checked) Corolario = "G00 Z5 A0 M2\n";
            cnclin[2] = Preambulo;
            int trazo;
            double dblFoldWidth;
            double dblHeight;
            double dblWidth;
            double angradian;
            double y, dx, dvx;
            double p1x, p1y, p2x, p2y, v1x, v2x;
            int impar, tam, a, prueba;
            byte que = 0;// queparte;
            bool toca;
            if (pliegues.CheckedItems.Count > 0)
            {
                if (pliegues.CheckedItems.Contains("Picos") == true) que = 2;
                if (pliegues.CheckedItems.Contains("Valles") == true) que += 1;
            }
            if (que ==3) cnclin[1] = espera;
            if (!double.TryParse(txtFoldWidth.Text, out dblFoldWidth))
                return("Error en ancho de pliegue");

            if (!double.TryParse(txtHeight.Text, out dblHeight))
                return("Error en Altura");
            if (!double.TryParse(txtWidth.Text, out dblWidth))
                return("Error en anchura");

            double dblTopWidth = dblWidth + 2 * dblFoldWidth;//ancho exterior necesario si pliegues hacia dentro
            double dblSideHeight = dblHeight + dblFoldWidth;//alto exterior necesario si pliegues hacia dentro
            dblPaperHeight = (double)nFolds * dblFoldWidth;
            double nuevorig = dblTopWidth + dblSideHeight;
            bool bAlternate = chkAlternateFolds.Checked;// alternar

            dvC = float.Parse(dlg.txtCutterOffset.Text, CultureInfo.GetCultureInfo("en-GB"));
            angD = float.Parse(dlg.numericUpDown1.Text);
            xC = float.Parse(dlg.CentroX.Text, CultureInfo.GetCultureInfo("en-GB"));
            yC = float.Parse(dlg.CentroY.Text, CultureInfo.GetCultureInfo("en-GB"));

            //quebradas verticales duplico el bucle para no interrupir trazados
            for (int ix = 0; ix < listinv.Items.Count - 1; ix++) // izquierdo por inversiones pero el cero final no
            {
                impar = ix % 2;
                trazo = (impar == 0) ? 2 : 1;
                angradian = Math.PI / 180.0 * float.Parse(listinv.Items[ix].ToString(), CultureInfo.GetCultureInfo("en-GB"));
                for (int plieg = 0; plieg < nFolds; plieg++)
                {
                    impar = plieg % 2;
                    y = plieg * dblFoldWidth;
                    dx = dblFoldWidth / Math.Tan(angradian);
                    toca = ((plieg + 1) % 4 == 0 | (plieg % 4) == 0);
                    if (bAlternate & toca) dx = -dx;
                    (p1x, p1y, p2x, p2y) = Encaja(dblSideHeight + (impar - 1) * dx, y, dblSideHeight - dx * impar, y + dblFoldWidth);
                    if ((que & trazo) != 0) cnclin[trazo] += A_Linea_CNC(trazo,(float)p1x, (float)p1y, (float)p2x, (float)p2y);
                }
            }

            for (int ix = 0; ix < listinv.Items.Count - 1; ix++) // derecho por inversiones pero el cero final no
            {
                impar = ix % 2;
                trazo = (impar == 0) ? (byte)2 : (byte)1;
                angradian = Math.PI / 180.0 * float.Parse(listinv.Items[ix].ToString(), CultureInfo.GetCultureInfo("en-GB"));
                for (int plieg = 0; plieg < nFolds; plieg++)
                {
                    impar = plieg % 2;
                    y = plieg * dblFoldWidth;
                    dx = dblFoldWidth / Math.Tan(angradian);
                    toca = ((plieg + 1) % 4 == 0 | (plieg % 4) == 0);
                    if (bAlternate & toca) dx = -dx;
                    (p1x, p1y, p2x, p2y) = Encaja(nuevorig - (impar - 1) * dx, y, nuevorig + dx * impar, y + dblFoldWidth);
                    if ((que & trazo) != 0) cnclin[trazo] += A_Linea_CNC(trazo, (float)p1x, (float)p1y, (float)p2x, (float)p2y);
                }
            }
            // Horizontales
            int externo;
            for (int plieg = 1; plieg < nFolds; plieg++)
            {
                toca = (plieg % 4 == 0 & bAlternate);
                externo = plieg % 2; //Los externos son los impares
                y = plieg * dblFoldWidth;
                v1x = 0;
                if (externo == 0) //Izquierdos internos
                {
                    tam = listinv.Items.Count - 1;
                    for (int ix = tam - 1; ix >= 0; ix--)
                    {
                        impar = ix % 2;
                        trazo = (impar == 1) ? 1 : 2;
                        prueba = ix - (a = toca ? 1 : 0) * (tam - 1);
                        angradian = Math.PI / 180.0 * float.Parse(listinv.Items[Math.Abs(prueba)].ToString(), CultureInfo.GetCultureInfo("en-GB"));
                        dvx = dblFoldWidth / Math.Tan(angradian);
                        if (toca) dvx = -dvx;
                        v2x = dblSideHeight - dvx;
                        (p1x, p1y, p2x, p2y) = Encaja(v1x, y, v2x, y);
                        try
                        {
                            if ((que & trazo) != 0) cnclin[trazo] += A_Linea_CNC(trazo, (float)p1x, (float)p1y, (float)p2x, (float)p2y);
                        }
                        catch { }
                        v1x = v2x;
                    }
                }
                else //Izquierdos externos
                {
                    trazo = (listinv.Items.Count % 2 == 1) ? (byte)2 : (byte)1;
                    (p1x, p1y, p2x, p2y) = Encaja(0, y, dblSideHeight, y);
                    if ((que & trazo) != 0) cnclin[trazo] += A_Linea_CNC(trazo, (float)p1x, (float)p1y, (float)p2x, (float)p2y);
                    v1x = dblSideHeight;
                }
                //Central
                trazo = (externo == 1) ? 2 : 1;
                tam = listinv.Items.Count - 1;
                prueba = (a = toca ? 1 : 0) * (tam - 1);
                angradian = Math.PI / 180.0 * float.Parse(listinv.Items[Math.Abs(prueba)].ToString(), CultureInfo.GetCultureInfo("en-GB"));
                dvx = dblFoldWidth / Math.Tan(angradian);
                if (toca) dvx = -dvx;
                v2x = nuevorig - dvx * (externo - 1);
                (p1x, p1y, p2x, p2y) = Encaja(v1x, y, v2x, y);
                if ((que & trazo) != 0) cnclin[trazo] += A_Linea_CNC(trazo, (float)p1x, (float)p1y, (float)p2x, (float)p2y);
                v1x = v2x;
                if (externo == 0)//Derechos Internos
                {
                    for (int ix = 1; ix < tam; ix++)
                    {
                        impar = ix % 2;
                        trazo = (impar == 0) ? 1 : 2;
                        prueba = ix - (a = toca ? 1 : 0) * (tam - 1);
                        angradian = Math.PI / 180.0 * float.Parse(listinv.Items[Math.Abs(prueba)].ToString(), CultureInfo.GetCultureInfo("en-GB"));
                        dvx = dblFoldWidth / Math.Tan(angradian);
                        if (toca) dvx = -dvx;
                        v2x = nuevorig + dvx;
                        (p1x, p1y, p2x, p2y) = Encaja(v1x, y, v2x, y);
                        try { if ((que & trazo) != 0) cnclin[trazo] += A_Linea_CNC(trazo, (float)p1x, (float)p1y, (float)p2x, (float)p2y); }
                        catch { }
                        v1x = v2x; if (v1x > dblPaperWidth) v1x = dblPaperWidth;
                    }
                    trazo = (listinv.Items.Count % 2 == 0) ? (byte)2 : (byte)1;
                    v2x = dblPaperWidth; //ultima derecha
                    (p1x, p1y, p2x, p2y) = Encaja(v1x, y, v2x, y);
                    if ((que & trazo) != 0) cnclin[trazo] += A_Linea_CNC(trazo, (float)p1x, (float)p1y, (float)p2x, (float)p2y);
                }
                else //Derechos Externos
                {
                    trazo = (listinv.Items.Count % 2 == 1) ? 2 : 1;
                    (p1x, p1y, p2x, p2y) = Encaja(nuevorig, y, dblPaperWidth, y);
                    if ((que & trazo) != 0) cnclin[trazo] += A_Linea_CNC(trazo, (float)p1x, (float)p1y, (float)p2x, (float)p2y);
                }
            }
            if (que != 2) cnclin[1] += Corolario;
            else cnclin[2] += Corolario;
            return (cnclin[2] + cnclin[1]);
        } /*GenFuelles*/

		private void btnPrint_Click(object sender, EventArgs e)
		{
			UpdateCalculations();
			printPreviewDialog1.Document = printDocument1;
			nPage = 0;
			if (printPreviewDialog1.ShowDialog() == DialogResult.OK)
			{
			}
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
            if (dlg.Resorte.Checked) gradbetas[tipo] = 0.0f; //Lo obligo al angulo del resorte
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
                gradbetas[tipo] = gradalfa;
                }
            (float Dx, float Dy) = DespP(dvC, gradalfa);
            (float dx, float dy) = DespP(dvC, gradbetas[tipo]);
            Linea += ("X" + (xx1 + dx).ToString("0.000", CultureInfo.GetCultureInfo("en-GB")));
            Linea += (" Y" + (yy1 + dy).ToString("0.000", CultureInfo.GetCultureInfo("en-GB"))+ '\n');
            Linea += "G01 Z0\n";
            if (!dlg.gobierno.Checked)
                {
                if (gradalfa < gradbetas[tipo]) { Linea += ("G3 X");}
                else { Linea += ("G2 X");}
                Linea += (xx1 + Dx).ToString("0.000", CultureInfo.GetCultureInfo("en-GB"));
                Linea += (" Y" + (yy1 + Dy).ToString("0.000", CultureInfo.GetCultureInfo("en-GB")));
                Linea += (" I" + (-dx).ToString("0.000", CultureInfo.GetCultureInfo("en-GB")));
                Linea += (" J" + (-dy).ToString("0.000", CultureInfo.GetCultureInfo("en-GB")) + '\n');
                }
            Linea += ("G01 X" + (xx2 + Dx).ToString("0.000", CultureInfo.GetCultureInfo("en-GB")));
            Linea += (" Y" + (yy2 + Dy).ToString("0.000", CultureInfo.GetCultureInfo("en-GB")) + '\n');
            gradbetas[tipo] = gradalfa;
            return Linea;
        }

        private string dimelistinv ()
        {
            int i;
            String cadena = "";
            for (i = 0; i < listinv.Items.Count-1; i++) cadena += listinv.Items[i] + ",";
            cadena += listinv.Items[i];
            return cadena;
        }
        private void cambialistinv ( string angulostring)
            {
            string valor;
            string[] tokens = angulostring.Split(',');
            int num = tokens.Length;
            int m = listinv.Items.Count;
            for (m = num - 1; m >= 0; m--)
                {
                valor = tokens[m];
                listinv.Items.Insert(0, valor);
                }
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

            if (!double.TryParse(txtFoldWidth.Text, out dblFoldWidth))
                return ("Error en Ancho");

            if (!double.TryParse(txtHeight.Text, out dblHeight))
                return ("Error en Alto");

            dblPaperHeight = (double)nFolds * dblFoldWidth;

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
				dblPaperHeight = (double)nFolds * dblFoldWidth;

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

            //DrawBellows(e.Graphics);
            PintaFuelles(e.Graphics);
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
                m_CurrentElement.Inversiones = dimelistinv();
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
            txtFoldWidth.Text = m_CurrentElement.FoldWidth.ToString();
			txtHeight.Text = m_CurrentElement.Height.ToString();
			txtWidth.Text = m_CurrentElement.Width.ToString();
			txtLength.Text = m_CurrentElement.Length.ToString();
			txtMountFolds.Text = m_CurrentElement.MountFolds.ToString();
			chkAlternateFolds.Checked = m_CurrentElement.AlternateFolds;
            cambialistinv(m_CurrentElement.Inversiones.ToString());
            Actualiza_Click(sender, e);

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			StoreItems();
			((FuelleConfig)m_Config.Sections["ParamdeFuelle"]).SelectedItem = cboConfig.SelectedItem.ToString();
			m_Config.Save(ConfigurationSaveMode.Full);
		}

		private void btnNewConfig_Click(object sender, EventArgs ev)
		{
			NewName frm = new NewName();
			if ( frm.ShowDialog(this) == DialogResult.OK )
			{
				FuellesCollection col = ((FuelleConfig)m_Config.Sections["ParamdeFuelle"]).Fuelless;
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
            btnGCode.Enabled = (pliegues.CheckedItems.Count == 0) ? false : true;
            UpdateCalculations();
        }

        private void TxtMountFolds_TextChanged(object sender, EventArgs e)
        {
            UpdateCalculations();
        }

        private void actualizapared()
        {
        double angulo = 180.0f;
        double parang;
        double valor;
        for (int i = 0; i < listinv.Items.Count; i++)
            {
                valor = Math.Pow(-1.0, (double)i);
                parang = float.Parse(listinv.Items[i].ToString(), CultureInfo.GetCultureInfo("en-GB"));
                angulo -= 2 * valor * parang;
            }

        pared.Text = angulo.ToString();

        }

        private void Listinv_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                textBox1.Text = listinv.SelectedItem.ToString();
            }
            catch {
                return;
                   }
            actualizapared();
       }


        private void Actualiza_Click(object sender, EventArgs e)
        {
            int ix;
            int indice = listinv.SelectedIndex;
            int quepasa = listinv.Items.Count;
            if (indice >= 0)
            {
                listinv.Items.RemoveAt(indice);
                listinv.Items.Insert(indice, textBox1.Text);
            }
            //Si la lista no termina en cero ponselo
            if (float.Parse(listinv.Items[quepasa-1].ToString(), CultureInfo.GetCultureInfo("en-GB")) != 0f)
                {
                listinv.Items.Insert(quepasa++, "0.0");
                
                }
            //Si en la lista hay un cero intermedio borrala
            for (ix=0; ix< quepasa; ix++)
                {
                if (float.Parse(listinv.Items[ix].ToString(), CultureInfo.GetCultureInfo("en-GB")) == 0f)
                    break;
                }
            while (--quepasa > ix)
                {
                listinv.Items.RemoveAt(quepasa);
                }
            //actualiza angulo pared()
            actualizapared();
            UpdateCalculations();
        }

        private void txtFoldWidth_Leave(object sender, EventArgs e)
		{
			UpdateCalculations();
		}

        private void btnGCode_Click(object sender, EventArgs e)
        {
           dlg = new GenerateGCode(this);
            dlg.CentroX.Text = (this.dblPaperWidth/2).ToString(CultureInfo.GetCultureInfo("en-GB")) ;
            dlg.CentroY.Text = (this.nFolds* this.dblFoldWidth / 2).ToString(CultureInfo.GetCultureInfo("en-GB"));
            if ( dlg.ShowDialog(this) == DialogResult.OK )
            {
            }
        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }
    }
}