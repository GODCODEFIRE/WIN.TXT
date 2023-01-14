using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            saveFileDialog1.Filter = "Text File(*.txt)|*.txt|wtx extension from WIN.TXT (*.wtx)|*.wtx";
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string FileName = saveFileDialog1.FileName;
            File.WriteAllText(FileName, richTextBox1.Text);
            MessageBox.Show("Save!");
        }

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string FileName = openFileDialog1.FileName;
            string FileText = File.ReadAllText(FileName);
            richTextBox1.Text = FileText;
            MessageBox.Show("Open!");
        }
    }
}
