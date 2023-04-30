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
        string OpenFILES;
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
        private string ContentFILES(string filePATHmap)
        {
            string FileTXT = richTextBox1.Text;
            if (Path.GetExtension(filePATHmap) == ".wt")
            {
                FileTXT = string.Join(Environment.NewLine, FileTXT,
                   $"{richTextBox1.SelectionColor.R}, {richTextBox1.SelectionColor.G}, {richTextBox1.SelectionColor.B}",
                    $"{richTextBox1.BackColor.R}, {richTextBox1.BackColor.G}, {richTextBox1.BackColor.B}",
                    $"{richTextBox1.Font.Name}, {richTextBox1.Font.Size}"
                    );
            }
            return FileTXT;
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string FileTXT = richTextBox1.Text;
            string FileName = saveFileDialog1.FileName;
            if (Path.GetExtension(FileName) == ".wt")
            {
                FileTXT = string.Join(Environment.NewLine, FileTXT,
                   $"{richTextBox1.SelectionColor.R}, {richTextBox1.SelectionColor.G}, {richTextBox1.SelectionColor.B}",
                    $"{richTextBox1.BackColor.R}, {richTextBox1.BackColor.G}, {richTextBox1.BackColor.B}",
                    $"{richTextBox1.Font.Name}, {richTextBox1.Font.Size}"
                    );
            }
            Console.WriteLine(FileTXT);
            File.WriteAllText(FileName, FileTXT);
            MessageBox.Show("SAVE FILE!");
        }
        //public Font StringToFont(string fontString)
        //{
        //}

        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string FileName = openFileDialog1.FileName;
            this.OpenFILES = FileName;
            string FileText = File.ReadAllText(FileName);
            if (Path.GetExtension(FileName) == ".wt")
            {
                string[] parametras = FileText.Split(Environment.NewLine.ToCharArray());
                richTextBox1.Text = parametras[0];
                string fontparameters = parametras[6];
                string[] font = fontparameters.Split(',');
                string fontcolparameters = parametras[2];
                string[] colorf = fontcolparameters.Split(',');
                string fontbackparameters = parametras[4];
                string[] colorback = fontbackparameters.Split(',');
                //Console.WriteLine(fontparameters);
                Color BACKCOLOR = Color.FromArgb(int.Parse(colorback[0]), int.Parse(colorback[1]), int.Parse(colorback[2])); ;
                richTextBox1.BackColor = BACKCOLOR;
                //System.ComponentModel.TypeConverter converter =
                //System.ComponentModel.TypeDescriptor.GetConverter(typeof(Font));

                //Font font1 = (Font)converter.ConvertFromString("Arial, 72pt");
                Font testFont = new Font(font[0], int.Parse(font[1]));
                richTextBox1.Font = testFont;
                Color fontCOLOR = Color.FromArgb(int.Parse(colorf[0]), int.Parse(colorf[1]), int.Parse(colorf[2]));
                richTextBox1.SelectionColor = fontCOLOR;
                Console.WriteLine(colorf[0]);
                Console.WriteLine(fontCOLOR);
            }
            else
            {
                richTextBox1.Text = FileText;
            }
            MessageBox.Show("OPEN FILE!");
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
            Console.WriteLine(col.Color);
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
        void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (File.Exists(this.OpenFILES) == true)
            {
                string content = this.ContentFILES(this.OpenFILES);
                // Создайте экземпляр StreamWriter для записи в файл.
                using (StreamWriter writer = new StreamWriter(this.OpenFILES))
                {
                    // Запишите содержимое в файл.
                    writer.Write(content);
                }
            }
            else
            {
                this.saveAsToolStripMenuItem_Click(sender, e);
            }

        }
    }
}
