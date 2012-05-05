using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Injector
{
    public partial class Form1 : Form
    {
        int _fileCount = 0;
        int _changeCount = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtFolderPath.Text.Trim() == "")
                MessageBox.Show("Give Path!!!");
            else if (txtInjectorData.Text.Trim() == "")
                MessageBox.Show("Give Data!!!");
            else if (txtBeforeTag.Text.Trim() == "")
                MessageBox.Show("Give Tag!!!");
            else
            {
                button1.Enabled = false;
                Convert(txtFolderPath.Text.Trim(), txtBeforeTag.Text.Trim(), txtInjectorData.Text);
                MessageBox.Show("Total File : " + _fileCount + " Total Change : " + _changeCount);
                button1.Enabled = true;
            }
        }


        private void Convert(string path, string tag, string data)
        {

            if (Directory.Exists(path))
            {
                string[] files = Directory.GetFiles(path);
                
                foreach (string file in files)
                {
                    _fileCount++;
                    string content = File.ReadAllText(file);
                    int k = content.ToLower().IndexOf(tag.ToLower());
                    if (k != -1)
                    {
                        content = content.Substring(0, k) + data + content.Substring(k, content.Length - k);
                        File.WriteAllText(file, content);
                        _changeCount++;
                    }
                    else
                    {
                        textBox1.Text += file + "\n";
                    }
                }

                string[] folders = Directory.GetDirectories(path);
                foreach (string folder in folders)
                {
                    Convert(folder, tag, data);
                }
            }
        }
    }
}
