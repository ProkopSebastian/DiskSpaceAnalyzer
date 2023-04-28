using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.ListViewItem;

namespace Rura
{

    public partial class Form2 : Form
    {
        public string? paths;
        public DirectoryInfo[]? dirs2;
        public List<string> pathsy = new List<string>();
        public List<string> drives = new List<string>();
        public List<ProgressBar> progressbars = new List<ProgressBar>();
        public int buttonNumber = -1;
        public Form2()
        {
            InitializeComponent();
            var dyski = DriveInfo.GetDrives();
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            foreach (var d in dyski)
            {
                drives.Add(d.Name);
                // Progress Bar
                ProgressBar pb = new ProgressBar();
                this.progressbars.Add(pb);
                ListViewItem item2 = new ListViewItem(d.Name); // to daje nazwe w pierwszej kolumnie
                item2.SubItems.Add(string.Format("{0:N2} GB", d.TotalSize / 1073741824.0));
                item2.SubItems.Add(string.Format("{0:N2} GB", d.TotalFreeSpace / 1073741824.0));
                item2.SubItems.Add(""); // tak chyba trzeba, żeby tam cokolwiek było
                item2.SubItems.Add(string.Format("{0:N2}%", ((d.TotalSize - d.AvailableFreeSpace) * 100.0) / d.TotalSize));

                //pb.Name = "w";
                //item2.SubItems.Add(pb.Name);
                listView1.Items.Add(item2);

                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                //Rectangle r = item2.SubItems[2].Bounds;
                Rectangle r = item2.Bounds;
                Rectangle r0 = item2.SubItems[0].Bounds;
                Rectangle r1 = item2.SubItems[1].Bounds;
                Rectangle r2 = item2.SubItems[2].Bounds;
                Rectangle r3 = item2.SubItems[3].Bounds;
                Rectangle r4 = item2.SubItems[4].Bounds;
                int a = (int)(d.TotalSize / 1073741824.0);
                int b = (int)((d.TotalSize - d.TotalFreeSpace) / 1073741824.0);
                pb.Maximum = a;
                pb.Minimum = 0;
                pb.Value = b;
                //pb.SetBounds(r4.X - r4.Width / 2, r4.Y, r4.Width + 10, r4.Height);
                pb.SetBounds(r3.X, r3.Y, r3.Width, r3.Height);
                listView1.Controls.Add(pb);

                //listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                //listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            }
            created = true;
            // pozostałe ustawienia przy okazji inicjalizacji
            //textBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
            //textBox1.AutoCompleteSource = ...

        }

        public void button1_Click(object sender, EventArgs e) // Button 1 to select '...' wewnatrz formsa2 
        {
            radioButton3.Checked = true;
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();
                //dirs2 = new DirectoryInfo[1];
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    //string[] dirs = Directory.GetDirectories(fbd.SelectedPath);
                    textBox1.Text = fbd.SelectedPath;
                    //DriveInfo[] allDrives = DriveInfo.GetDrives();
                    //foreach (DriveInfo d in allDrives)
                    //{
                    //    //d.VolumeLabel
                    //    listView1.Items.Add((d.AvailableFreeSpace / 1000).ToString());

                    //}
                    //paths = new string[1];
                    ////dirs2[0] = fbd.InitialDirectory
                    //paths[0] = textBox1.Text;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            validate();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            radioButton2.Checked = true;
            // wybrane trzeba dodawac do listy wybranych 
            pathsy.Clear();
            for (int i = 0; i < listView1.SelectedIndices.Count; i++)
            {
                pathsy.Add(drives[listView1.SelectedIndices[i]]);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e) // Czyli 'OK' button 
        {
            if (validate() == 0)
            {
                //paths = new string[1];
                paths = textBox1.Text;
                //Form1.currentDir = paths[0];
                //dirs = new DirectoryInfo[1];
                //dirs[0] = textBox1.Text;
                // tutaj wywołać publiczną metodę z Form1
                if (radioButton1.Checked) { buttonNumber = 1; }
                if (radioButton2.Checked) { buttonNumber = 2; }
                if (radioButton3.Checked) { buttonNumber = 3; }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        //private void listView1_Resize(object sender, EventArgs e)
        //{
        //    //listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

        //    //listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        //}

        private void listView1_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {

        }

        //private void listView1_SizeChanged(object sender, EventArgs e)
        //{
        //    //listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

        //    //listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        //}
        int validate()
        {
            if (radioButton3.Checked)
            {
                if (!string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    DirectoryInfo dirinfo = new DirectoryInfo(textBox1.Text);

                    if (dirinfo.Exists)
                    {
                        textBox1.ForeColor = Color.Black;
                        return 0;
                    }
                    textBox1.ForeColor = Color.Red;
                    return 1;
                }
            }
            if (radioButton2.Checked)
            {
                if (listView1.SelectedItems.Count > 0) { return 0; }
            }
            if (radioButton1.Checked) { return 0; }

            return 1;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            pathsy.Clear();
            if (listView1.SelectedItems.Count == 0) return;
            for (int i = 0; i < listView1.SelectedIndices.Count; i++)
            {
                pathsy.Add(drives[listView1.SelectedIndices[i]]);
            }
        }

        private void Form2_Shown(object sender, EventArgs e)
        {
            foreach (DriveInfo d in DriveInfo.GetDrives())
            {
                drives.Add(d.Name);
            }
        }

        private void listView1_Resize(object sender, EventArgs e)
        {
            //// przestawiac na biezaco progress bara
            //Rectangle r = listView1.Items[0].SubItems[3].Bounds;
            //foreach (var v in progressbars)
            //{
            //    v.Bounds = r;
            //}
        }

        private void listView1_SizeChanged(object sender, EventArgs e)
        {
            //Rectangle r = listView1.Items[0].SubItems[3].Bounds;
            //foreach (var v in progressbars)
            //{
            //    v.Bounds = r;
            //}
        }
        bool created = false;
        private void listView1_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
        {
            //if (!created) { return; }
            //if (listView1.Items == null) { return; }
            //if (listView1.Items[0] != null && listView1.Items[0].SubItems[3] != null)
            //{
            //    Rectangle r = listView1.Items[0].SubItems[3].Bounds;
            //    foreach (var v in progressbars)
            //    {
            //        v.Bounds = r;
            //    }
            //}
        }

        private void listView1_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            if (!created) { return; }
            int i = 0;
            foreach (var v in progressbars)
            {
                Rectangle r = listView1.Items[i++].SubItems[3].Bounds;
                v.Bounds = r;
            }
        }

        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            radioButton3.Checked = true;
        }
    }
}
