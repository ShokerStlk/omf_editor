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

namespace OMF_Editor
{
    public partial class Form1 : Form
    {
        OMFEditor editor = new OMFEditor();

        List<AnimationsContainer> OMFFiles = new List<AnimationsContainer>();

        public Form1()
        {
            InitializeComponent();
            InitButtons();
        }

        private void InitButtons()
        {
            openFileDialog1.Filter = "OMF file|*.omf";
            openFileDialog1.FileName = "";
        }

        private void OpenFile(string filename)
        {
            if(editor.OpenOMF(filename, OMFFiles))
            {
                MessageBox.Show("Done!");
                listBox1.DisplayMember = "MotionName";
                listBox1.DataSource = OMFFiles[0].Anims;
            }
    
        }

        private void SaveOMF(AnimationsContainer omf_file)
        {
            string path = Directory.GetCurrentDirectory()+"\\test.omf";

            using (BinaryWriter writer = new BinaryWriter(File.Create(path)))
            {
                editor.WriteOMF(writer, omf_file);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            try
            {
                OMFFiles.Clear();
                OpenFile(openFileDialog1.FileName);
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.ToString());
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(OMFFiles.Count != 0)
                SaveOMF(OMFFiles[0]);
        }
    }
}