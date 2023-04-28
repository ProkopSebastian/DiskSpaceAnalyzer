using System.ComponentModel;
using System.IO;
using System.Xml.Linq;

namespace Rura
{
    public partial class Form1 : Form
    {
        public DirectoryInfo currentDir;
        string selectedPath;
        public Dictionary<string, int> fileTypes;
        bool finished = false;
        int p = 0;
        public Form1()
        {
            InitializeComponent();
            //get a list of the drives
            string[] drives = Environment.GetLogicalDrives();
            //this.selectedPath = drives[0];
            //fileTypes = new Dictionary<string, int>();

            foreach (string drive in drives)
            {
                DriveInfo di = new DriveInfo(drive);

                TreeNode node = new TreeNode(drive.Substring(0, 1));
                node.Tag = drive;

                if (di.IsReady == true)
                    node.Nodes.Add("...");

                treeView1.Nodes.Add(node);
            }
            // testowo

        }

        private void SelectButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.ShowDialog();
            }

        }

        private void selectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog(); // Shows Form2
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            DialogResult result = f2.ShowDialog(); // Shows Form2
            if (result == DialogResult.OK)
            {
                if (f2.buttonNumber == 3) // Folder
                {
                    this.selectedPath = f2.paths;
                    if (selectedPath == null) { return; }
                    treeView1.Nodes.Clear();
                    DirectoryInfo directoryInfo = new DirectoryInfo(this.selectedPath);
                    TreeNode node = new TreeNode(directoryInfo.Name);
                    node.Tag = selectedPath;
                    if (directoryInfo.GetDirectories().Count() > 0)
                        node.Nodes.Add(null, "...", 0, 0);
                    treeView1.Nodes.Add(node);
                    // musi byæ dummy node ¿eby by³a mo¿liwoœæ expand
                    node.Nodes.Add(new TreeNode());
                    // na expandzie foreach po DirectoryInfo.getDirectories...
                }
                if (f2.buttonNumber == 2) // Wybrane dyski 
                {
                    treeView1.Nodes.Clear(); // !!!
                    foreach (var v in f2.pathsy)
                    {
                        DirectoryInfo directoryInfo = new DirectoryInfo(v);
                        TreeNode node = new TreeNode(directoryInfo.Name);
                        node.Tag = v;
                        if (directoryInfo.GetDirectories().Count() > 0)
                            node.Nodes.Add(null, "...", 0, 0);
                        treeView1.Nodes.Add(node);
                        // musi byæ dummy node ¿eby by³a mo¿liwoœæ expand
                        node.Nodes.Add(new TreeNode());
                    }
                }
                if (f2.buttonNumber == 1)
                {
                    treeView1.Nodes.Clear();
                    //
                    string[] drives = Environment.GetLogicalDrives();
                    foreach (string drive in drives)
                    {
                        DriveInfo di = new DriveInfo(drive);

                        TreeNode node = new TreeNode(drive.Substring(0, 1));
                        node.Tag = drive;

                        if (di.IsReady == true)
                            node.Nodes.Add("...");

                        treeView1.Nodes.Add(node);
                    }
                    //


                }

            }
            treeView1.Update();

            // teraz zamiast update details bedzie to robic bacground worker
            //updateDetails();
            //backgroundWorker1.RunWorkerAsync();
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

            // 
            if (backgroundWorker1.IsBusy == true)
                backgroundWorker1.CancelAsync();
            while (backgroundWorker1.IsBusy)
                Application.DoEvents();
            //
            selectedPath = e.Node.Tag.ToString();
            int subdirs = 0;
            try
            {
                subdirs = Directory.GetDirectories(selectedPath).Count();
            }
            catch { }
            toolStripProgressBar1.Value = 0;
            if (subdirs == 0) { toolStripProgressBar1.Step = 100; }
            else { toolStripProgressBar1.Step = 100 / subdirs; }

            toolStripProgressBar1.Value = 0;
            toolStripProgressBar1.Step = 100 / 5;
            ButtonCancelAsync.Enabled = true;
            backgroundWorker1.RunWorkerAsync();

        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            // https://codehill.com/2013/06/list-drives-and-folders-in-a-treeview-using-c/
            if (e.Node.Nodes.Count > 0)
            {
                if (e.Node.Nodes[0].Text == "..." && e.Node.Nodes[0].Tag == null)
                {
                    e.Node.Nodes.Clear();

                    //get the list of sub direcotires
                    string[] dirs = Directory.GetDirectories(e.Node.Tag.ToString());
                    string[] files = Directory.GetFiles(e.Node.Tag.ToString());

                    foreach (string dir in dirs)
                    {
                        DirectoryInfo di = new DirectoryInfo(dir);
                        TreeNode node = new TreeNode(di.Name, 0, 1);

                        try
                        {
                            //keep the directory's full path in the tag for use later
                            node.Tag = dir;

                            //if the directory has sub directories add the place holder
                            if (di.GetDirectories().Count() > 0)
                                node.Nodes.Add(null, "...", 0, 0);
                            if (di.GetFiles().Count() > 0)
                                node.Nodes.Add(null, " ", 0, 0);
                        }
                        catch (UnauthorizedAccessException)
                        {
                            //display a locked folder icon
                            node.ImageIndex = 12;
                            node.SelectedImageIndex = 12;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "DirectoryLister",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        finally
                        {
                            e.Node.Nodes.Add(node);
                        }

                    }

                    if (files.Count() <= 3)
                    {
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(file);
                            TreeNode node = new TreeNode(fi.Name, 0, 1);
                            try
                            {
                                node.Tag = fi;
                            }
                            catch (UnauthorizedAccessException)
                            {
                                //display a locked folder icon
                                node.ImageIndex = 12;
                                node.SelectedImageIndex = 12;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "DirectoryLister",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            finally
                            {
                                e.Node.Nodes.Add(node);
                            }
                        }
                    }
                    else
                    {
                        TreeNode Filenode = new TreeNode("<Files>", 0, 1);
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(file);
                            TreeNode node = new TreeNode(fi.Name, 0, 1);
                            try
                            {
                                node.Tag = fi;
                            }
                            catch (UnauthorizedAccessException)
                            {
                                //display a locked folder icon
                                node.ImageIndex = 12;
                                node.SelectedImageIndex = 12;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "DirectoryLister",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            finally
                            {
                                Filenode.Nodes.Add(node);
                            }
                        }
                        Filenode.Tag =
                        e.Node.Nodes.Add(Filenode);
                    }

                }
            }
        }

        // https://stackoverflow.com/questions/24031083/how-to-return-multiple-result-from-backgroundworker-c-sharp
        class MyResult //Name the classes and properties accordingly
        {
            public string[] Strings { get; set; }
        }
        MyResult updateDetailsBackground(string selectedPath)
        {
            finished = false;
            string[] wynik = new string[6];
            //if (selectedPath == null) { return; }
            DirectoryInfo directoryInfo = new DirectoryInfo(selectedPath);
            if (directoryInfo.Exists)
            {
                wynik[0] = directoryInfo.FullName;
                int a = 0;
                long size = 0;
                int numItems = 0;
                int numFiles = 0;
                int numSubDirectories = 0;
                var options = new EnumerationOptions
                {
                    IgnoreInaccessible = true,
                    RecurseSubdirectories = true
                };
                size = directoryInfo.EnumerateFiles("*", options)
                         .Sum(file => file.Length);
                if (backgroundWorker1.CancellationPending == true) { return new MyResult(); }
                backgroundWorker1.ReportProgress(a++);
                //try
                //{
                //size = directoryInfo.EnumerateFiles("*", SearchOption.AllDirectories).Sum(file => file.Length);
                //size = directoryInfo.EnumerateFiles("*", new EnumerationOptions { IgnoreInaccessible = true}).Sum(file => file.Length);

                if (backgroundWorker1.CancellationPending == true) { return new MyResult(); }
                numItems = directoryInfo.EnumerateFileSystemInfos("*", options/*SearchOption.AllDirectories*/).Count();
                backgroundWorker1.ReportProgress(a++);
                if (backgroundWorker1.CancellationPending == true) { return new MyResult(); }
                numFiles = directoryInfo.EnumerateFiles("*", options/*SearchOption.AllDirectories*/).Count();
                backgroundWorker1.ReportProgress(a++);
                if (backgroundWorker1.CancellationPending == true) { return new MyResult(); }
                numSubDirectories = directoryInfo.EnumerateDirectories("*", options).Count();
                backgroundWorker1.ReportProgress(a++);
                if (backgroundWorker1.CancellationPending == true) { return new MyResult(); }
                backgroundWorker1.ReportProgress(a++);
                //}
                //catch (UnauthorizedAccessException)
                //{

                //}
                wynik[1] = BytesToString(size);
                wynik[2] = numItems.ToString();
                wynik[3] = numFiles.ToString();
                wynik[4] = numSubDirectories.ToString();
                wynik[5] = directoryInfo.LastWriteTime.ToString();
            }
            MyResult Result = new MyResult { Strings = wynik };
            finished = true;
            return Result;
        }
        // https://stackoverflow.com/questions/281640/how-do-i-get-a-human-readable-file-size-in-bytes-abbreviation-using-net
        static String BytesToString(long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + suf[place];
        }

        private void Form1_Shown(object sender, EventArgs e)
        {

        }

        public void PieChart()
        {
            //this.userControl11.
            //this.userControl11.GiveData(this.fileTypes);
            //userControl11.Invalidate();
            //
            Bitmap chartBitmap = new Bitmap(tabPage2.Width, tabPage2.Height);
            Graphics chartGraphics = Graphics.FromImage(chartBitmap);
            chartGraphics.Clear(Color.White);
            int total = fileTypes.Values.Sum();
            int startAngle = 0;
            int sweepAngle;
            float percentage;
            Random random = new Random();
            foreach (string fileType in fileTypes.Keys)
            {
                percentage = (float)fileTypes[fileType] / total;
                sweepAngle = (int)(percentage * 360);
                chartGraphics.FillPie(new SolidBrush(Color.FromArgb(random.Next(256), random.Next(256), random.Next(256))), new Rectangle(0, 0, chartBitmap.Width, chartBitmap.Height), startAngle, sweepAngle);
                startAngle += sweepAngle;
            }
            pictureBox.Image = chartBitmap;
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.Dock = DockStyle.Fill;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Asynchronicznie policz sobie tamte pliczki 
            if (fileTypes != null) { fileTypes.Clear(); } // -- zawsze trzeba to zrobiæ przed wywo³aniem Extentions
            int subdirs = 0;
            try
            {
                subdirs = Directory.GetDirectories(selectedPath).Count();
            }
            catch { }
            toolStripProgressBar1.Value = 0;
            if (subdirs == 0) { toolStripProgressBar1.Step = 100; }
            else { toolStripProgressBar1.Step = 100 / subdirs; }
            backgroundWorker2.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            MyResult res = updateDetailsBackground(selectedPath);
            e.Result = res;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            if (finished == false) { return; }
            string[] wynik = new string[6];
            MyResult res = (MyResult)e.Result;
            labelFullPath.Text = res.Strings[0];
            LabelSize.Text = res.Strings[1];
            LabelItems.Text = res.Strings[2];
            LabelFiles.Text = res.Strings[3];
            LabelSubdirs.Text = res.Strings[4];
            LabelLastChange.Text = res.Strings[5];
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar1.PerformStep();
        }


        public void Extentions(string path, int level)
        {
            if (level == 1)
            {
                backgroundWorker2.ReportProgress(p++);
            }
            if (backgroundWorker2.CancellationPending == true) { return; }
            try
            {
                foreach (string file in Directory.GetFiles(path))
                {
                    //backgroundWorker2.ReportProgress(p++);
                    string extension = Path.GetExtension(file).ToLower();
                    if (fileTypes.ContainsKey(extension))
                    {
                        fileTypes[extension]++;
                    }
                    else
                    {
                        fileTypes.Add(extension, 1);
                    }
                }
                //MessageBox.Show(Directory.GetDirectories(path).Count().ToString());
                foreach (var v in Directory.GetDirectories(path))
                {
                    //backgroundWorker2.ReportProgress(4);
                    //toolStripProgressBar1.PerformStep();
                    Extentions(v, level + 1);

                }
            }
            catch { }
            return;
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            fileTypes = new Dictionary<string, int>();
            Extentions(selectedPath, 0);
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            PieChart();
            //progressBar1.PerformStep();
            toolStripProgressBar1.PerformStep();
        }

        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar1.PerformStep();
        }

        private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
        {

        }

        private void ButtonCancelAsync_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy == true)
            {
                backgroundWorker1.CancelAsync();
                ButtonCancelAsync.Enabled = false;
                //toolStripProgressBar1.Value = 0;
            }
        }
    }
}

