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
using System.Drawing.Printing;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        Color PrintColor;
        public Form1(string FileName)
        {
            InitializeComponent();
            saveFileDialog1.Filter = "Text File(*.txt)|*.txt|wt extension from WIN.TXT (*.wt)|*.wt|WEB File(*.html)|*.html|Bat File(*.bat)|*.bat";
            if (FileName.Length > 0)
            {
                string FileText = File.ReadAllText(FileName);
                richTextBox1.Text = FileText;
            }
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

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.TextLength > 0)
            {
                richTextBox1.Copy();
            }
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.TextLength >= 0)
            {
                richTextBox1.Paste();
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.TextLength != 0)
            {
                richTextBox1.Cut();
            }
        }

        private void fontSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
            richTextBox1.Font = fontDialog1.Font;
        }

        private void backgroundSettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            colorDialog1.ShowDialog();
            richTextBox1.BackColor = colorDialog1.Color;
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length > 0)
            {
                richTextBox1.SelectAll();
            }
        }

        private void richTextBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                richTextBox1.ContextMenuStrip = contextMenuStrip1;
            }
        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length > 0)
            {
                richTextBox1.Copy();
            }
        }

        private void pasteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length > 0)
            {
                richTextBox1.Paste();
            }
        }

        private void cutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length > 0)
            {
                richTextBox1.Cut();
            }
        }

        private void selectAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text.Length > 0)
            {
                richTextBox1.SelectAll();
            }
        }

        private void fontColorsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog col = new ColorDialog();
            col.ShowDialog();
            richTextBox1.SelectionColor = col.Color;
        }
        void PrintPageHandler(object sender, PrintPageEventArgs e)
        {
            Brush brush = new SolidBrush(PrintColor);
            e.Graphics.DrawString(richTextBox1.Text, richTextBox1.Font, brush, 0, 0);
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog col = new ColorDialog();
            if (col.ShowDialog() == DialogResult.OK)
            {
                PrintColor = col.Color;

                ///richTextBox1.SelectionColor = col.Color;

                PrintDocument document = new PrintDocument();

                document.PrintPage += PrintPageHandler;
                PrintDialog printt = new PrintDialog();
                printt.Document = document;

                if (printt.ShowDialog() == DialogResult.OK)
                {
                    printt.Document.Print();
                }
            }
        }
    }
}
