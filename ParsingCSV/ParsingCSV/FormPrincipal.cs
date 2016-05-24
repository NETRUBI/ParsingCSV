using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Microsoft.VisualBasic.FileIO;

namespace ParsingCSV
{
    public partial class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string[] header;
                string[] body;
                DataTable dt = new DataTable("Lancamentos");
                this.dataGridView1.DataSource = dt;
                try
                {
                    TextFieldParser parser = new TextFieldParser(openFileDialog.FileName) { Delimiters = new string[] { ";" } };
                    while (!parser.EndOfData)
                    {
                        if (parser.LineNumber == 1)
                        {
                            header = parser.ReadFields();
                            for (int i = 0; i < header.Length; i++)
                            {
                                DataColumn dc = new DataColumn(header[i]);
                                dt.Columns.Add(dc);
                            }
                        }
                        else
                        {
                            body = parser.ReadFields();
                            DataRow dr = dt.NewRow();
                            for (int i = 0; i < body.Length; i++)
                            {
                                dr[i] = body[i];
                            }
                            dt.Rows.Add(dr);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Convert.ToString(ex));
                }
            }
        }
    }
}
