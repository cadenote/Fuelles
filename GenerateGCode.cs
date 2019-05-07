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

namespace Fuelles
{
    public partial class GenerateGCode : Form
    {
        Form1 formulario;
        System.Windows.Forms.Panel mipanel;
        public GenerateGCode(Form1 formu)
        {
            formulario = formu;
            mipanel = new System.Windows.Forms.Panel();
            InitializeComponent();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("¿Cerrar Programa?", "Cancelada la Generacion", MessageBoxButtons.YesNo);
            if (dialog == DialogResult.Yes)
            {
                Application.Exit();
            }
            else if (dialog == DialogResult.No)
            {
                // vuelve al Programa
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            //string anves="";
            //string reves="";
            string textReaderText;
            System.IO.Stream myStream;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.InitialDirectory = txtOutputFile.Text;
            saveFileDialog1.Filter = "Ficheros LinuxCnC (*.ngc)|*.ngc|Ficheros grbl (*.nc)|*.nc|All files (*.*)|*.*";
            saveFileDialog1.FilterIndex = 3;
            //saveFileDialog1.RestoreDirectory = true;
            //textReaderText=formulario.GenBellows(pinta.Graphics);
            //textReaderText = formulario.RayaFuelles();
            if (formulario.pliegues.CheckedItems.Count > 0)
            {
                //if (formulario.pliegues.CheckedItems.Contains("Picos") == true) anves = formulario.GenFuelles(2);
                //if (formulario.pliegues.CheckedItems.Contains("Valles") == true) reves = formulario.GenFuelles(1);
                //textReaderText = anves + reves;
                textReaderText = formulario.GenFuelles();
                byte[] array = Encoding.ASCII.GetBytes(textReaderText);
                int longi = array.Length;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((myStream = saveFileDialog1.OpenFile()) != null)
                    {
                        myStream.Write(array, 0, longi);
                        myStream.Close();
                    }
                }
            }
            this.Close();
        }

        private void CheckedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Resorte.Checked == true)
                {
                this.gobierno.Checked = false;
                this.gobierno.Enabled = false;
                }
            else this.gobierno.Enabled = true;
        }

    }
}
