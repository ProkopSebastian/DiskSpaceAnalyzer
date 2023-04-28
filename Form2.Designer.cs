namespace Rura
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            listView1 = new ListView();
            NameColumn = new ColumnHeader();
            Total = new ColumnHeader();
            Free = new ColumnHeader();
            usedTotal = new ColumnHeader();
            usedTotalBar = new ColumnHeader();
            textBox1 = new TextBox();
            button1 = new Button();
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            button2 = new Button();
            button3 = new Button();
            radioButton3 = new RadioButton();
            SuspendLayout();
            // 
            // listView1
            // 
            listView1.Activation = ItemActivation.OneClick;
            listView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listView1.Columns.AddRange(new ColumnHeader[] { NameColumn, Total, Free, usedTotal, usedTotalBar });
            listView1.HoverSelection = true;
            listView1.Location = new Point(14, 67);
            listView1.Margin = new Padding(3, 4, 3, 4);
            listView1.Name = "listView1";
            listView1.Size = new Size(601, 183);
            listView1.TabIndex = 2;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            listView1.ColumnWidthChanged += listView1_ColumnWidthChanged;
            listView1.ColumnWidthChanging += listView1_ColumnWidthChanging;
            listView1.DrawSubItem += listView1_DrawSubItem;
            listView1.SelectedIndexChanged += listView1_SelectedIndexChanged;
            listView1.SizeChanged += listView1_SizeChanged;
            listView1.Resize += listView1_Resize;
            // 
            // NameColumn
            // 
            NameColumn.Text = "Name";
            // 
            // Total
            // 
            Total.DisplayIndex = 2;
            Total.Text = "Total";
            // 
            // Free
            // 
            Free.DisplayIndex = 1;
            Free.Text = "Free";
            // 
            // usedTotal
            // 
            usedTotal.Text = "Used/Total";
            // 
            // usedTotalBar
            // 
            usedTotalBar.Tag = "ProgressBar";
            usedTotalBar.Text = "Used/Total";
            // 
            // textBox1
            // 
            textBox1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBox1.AutoCompleteSource = AutoCompleteSource.FileSystem;
            textBox1.Location = new Point(14, 288);
            textBox1.Margin = new Padding(3, 4, 3, 4);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(511, 27);
            textBox1.TabIndex = 4;
            textBox1.MouseClick += textBox1_MouseClick;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            button1.Location = new Point(531, 284);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(82, 31);
            button1.TabIndex = 5;
            button1.Text = "...";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(14, 0);
            radioButton1.Margin = new Padding(3, 4, 3, 4);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(137, 24);
            radioButton1.TabIndex = 6;
            radioButton1.TabStop = true;
            radioButton1.Text = "&All Local Drivers";
            radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(14, 33);
            radioButton2.Margin = new Padding(3, 4, 3, 4);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(145, 24);
            radioButton2.TabIndex = 7;
            radioButton2.TabStop = true;
            radioButton2.Text = "&Individual Drivers";
            radioButton2.UseVisualStyleBackColor = true;
            radioButton2.CheckedChanged += radioButton2_CheckedChanged;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button2.Location = new Point(439, 323);
            button2.Margin = new Padding(3, 4, 3, 4);
            button2.Name = "button2";
            button2.Size = new Size(86, 31);
            button2.TabIndex = 8;
            button2.Text = "OK";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button3.Location = new Point(531, 323);
            button3.Margin = new Padding(3, 4, 3, 4);
            button3.Name = "button3";
            button3.Size = new Size(86, 31);
            button3.TabIndex = 9;
            button3.Text = "Cancel";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // radioButton3
            // 
            radioButton3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(12, 257);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(84, 24);
            radioButton3.TabIndex = 10;
            radioButton3.TabStop = true;
            radioButton3.Text = "A &folder";
            radioButton3.UseVisualStyleBackColor = true;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(629, 367);
            Controls.Add(radioButton3);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(radioButton2);
            Controls.Add(radioButton1);
            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(listView1);
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(500, 300);
            Name = "Form2";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Form2";
            Shown += Form2_Shown;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ListView listView1;
        private ColumnHeader NameColumn;
        private ColumnHeader Total;
        private ColumnHeader Free;
        private ColumnHeader usedTotal;
        private TextBox textBox1;
        private Button button1;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private Button button2;
        private Button button3;
        private ColumnHeader usedTotalBar;
        private RadioButton radioButton3;
    }
}