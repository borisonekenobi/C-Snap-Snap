namespace C_Snap_Snap
{
    partial class Main
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.to = new System.Windows.Forms.Label();
            this.Export = new System.Windows.Forms.Button();
            this.Language = new System.Windows.Forms.ComboBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.Blocks = new System.Windows.Forms.TabControl();
            this.Variables = new System.Windows.Forms.TabPage();
            this.IfStatements = new System.Windows.Forms.TabPage();
            this.Loops = new System.Windows.Forms.TabPage();
            this.Math = new System.Windows.Forms.TabPage();
            this.BlockCloser = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.Blocks.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.to);
            this.splitContainer1.Panel1.Controls.Add(this.Export);
            this.splitContainer1.Panel1.Controls.Add(this.Language);
            this.splitContainer1.Panel1MinSize = 45;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(800, 450);
            this.splitContainer1.SplitterDistance = 45;
            this.splitContainer1.TabIndex = 0;
            // 
            // to
            // 
            this.to.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.to.AutoSize = true;
            this.to.Location = new System.Drawing.Point(645, 15);
            this.to.MaximumSize = new System.Drawing.Size(16, 13);
            this.to.MinimumSize = new System.Drawing.Size(16, 13);
            this.to.Name = "to";
            this.to.Size = new System.Drawing.Size(16, 13);
            this.to.TabIndex = 2;
            this.to.Text = "to";
            // 
            // export
            // 
            this.Export.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Export.Enabled = false;
            this.Export.Location = new System.Drawing.Point(564, 10);
            this.Export.MaximumSize = new System.Drawing.Size(75, 23);
            this.Export.MinimumSize = new System.Drawing.Size(75, 23);
            this.Export.Name = "export";
            this.Export.Size = new System.Drawing.Size(75, 23);
            this.Export.TabIndex = 1;
            this.Export.Text = "Export";
            this.Export.UseVisualStyleBackColor = true;
            // 
            // language
            // 
            this.Language.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Language.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Language.DropDownWidth = 120;
            this.Language.FormattingEnabled = true;
            this.Language.Items.AddRange(new object[] {
            "Choose Language",
            "C++"});
            this.Language.Location = new System.Drawing.Point(667, 12);
            this.Language.Name = "language";
            this.Language.Size = new System.Drawing.Size(120, 21);
            this.Language.TabIndex = 0;
            this.Language.SelectedIndexChanged += new System.EventHandler(this.Language_SelectedIndexChanged);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.Blocks);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer2.Panel2.Controls.Add(this.BlockCloser);
            this.splitContainer2.Panel2.DoubleClick += new System.EventHandler(this.AddFile);
            this.splitContainer2.Size = new System.Drawing.Size(800, 401);
            this.splitContainer2.SplitterDistance = 169;
            this.splitContainer2.TabIndex = 0;
            this.splitContainer2.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.SplitContainer2_SplitterMoved);
            // 
            // Blocks
            // 
            this.Blocks.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.Blocks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Blocks.Controls.Add(this.Variables);
            this.Blocks.Controls.Add(this.IfStatements);
            this.Blocks.Controls.Add(this.Loops);
            this.Blocks.Controls.Add(this.Math);
            this.Blocks.Location = new System.Drawing.Point(0, 0);
            this.Blocks.Multiline = true;
            this.Blocks.Name = "Blocks";
            this.Blocks.SelectedIndex = 0;
            this.Blocks.Size = new System.Drawing.Size(170, 401);
            this.Blocks.TabIndex = 0;
            // 
            // variables
            // 
            this.Variables.Location = new System.Drawing.Point(23, 4);
            this.Variables.Name = "variables";
            this.Variables.Size = new System.Drawing.Size(143, 393);
            this.Variables.TabIndex = 0;
            this.Variables.Text = "Variables";
            this.Variables.UseVisualStyleBackColor = true;
            // 
            // ifStatements
            // 
            this.IfStatements.Location = new System.Drawing.Point(23, 4);
            this.IfStatements.Name = "ifStatements";
            this.IfStatements.Size = new System.Drawing.Size(143, 393);
            this.IfStatements.TabIndex = 1;
            this.IfStatements.Text = "If Statements";
            this.IfStatements.UseVisualStyleBackColor = true;
            // 
            // loops
            // 
            this.Loops.Location = new System.Drawing.Point(23, 4);
            this.Loops.Name = "loops";
            this.Loops.Size = new System.Drawing.Size(143, 393);
            this.Loops.TabIndex = 2;
            this.Loops.Text = "Loops";
            this.Loops.UseVisualStyleBackColor = true;
            // 
            // math
            // 
            this.Math.Location = new System.Drawing.Point(23, 4);
            this.Math.Name = "math";
            this.Math.Size = new System.Drawing.Size(143, 393);
            this.Math.TabIndex = 3;
            this.Math.Text = "Math";
            this.Math.UseVisualStyleBackColor = true;
            // 
            // blockCloser
            // 
            this.BlockCloser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.BlockCloser.AutoSize = true;
            this.BlockCloser.Location = new System.Drawing.Point(0, 388);
            this.BlockCloser.Name = "blockCloser";
            this.BlockCloser.Size = new System.Drawing.Size(13, 13);
            this.BlockCloser.TabIndex = 0;
            this.BlockCloser.Text = "<";
            this.BlockCloser.Click += new System.EventHandler(this.BlockCloser_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Main";
            this.Text = "C-Snap-Snap";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.Blocks.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label BlockCloser;
        private System.Windows.Forms.ComboBox Language;
        private System.Windows.Forms.Label to;
        private System.Windows.Forms.Button Export;
        private System.Windows.Forms.TabControl Blocks;
        private System.Windows.Forms.TabPage Variables;
        private System.Windows.Forms.TabPage IfStatements;
        private System.Windows.Forms.TabPage Loops;
        private System.Windows.Forms.TabPage Math;
    }
}

