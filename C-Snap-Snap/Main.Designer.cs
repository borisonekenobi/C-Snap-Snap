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
            this.export = new System.Windows.Forms.Button();
            this.language = new System.Windows.Forms.ComboBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.blockCloser = new System.Windows.Forms.Label();
            this.files = new System.Windows.Forms.TabControl();
            this.blocks = new System.Windows.Forms.TabControl();
            this.variables = new System.Windows.Forms.TabPage();
            this.ifStatements = new System.Windows.Forms.TabPage();
            this.loops = new System.Windows.Forms.TabPage();
            this.math = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.blocks.SuspendLayout();
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
            this.splitContainer1.Panel1.Controls.Add(this.export);
            this.splitContainer1.Panel1.Controls.Add(this.language);
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
            this.to.AutoSize = true;
            this.to.Location = new System.Drawing.Point(645, 15);
            this.to.Name = "to";
            this.to.Size = new System.Drawing.Size(16, 13);
            this.to.TabIndex = 2;
            this.to.Text = "to";
            // 
            // export
            // 
            this.export.Enabled = false;
            this.export.Location = new System.Drawing.Point(564, 10);
            this.export.Name = "export";
            this.export.Size = new System.Drawing.Size(75, 23);
            this.export.TabIndex = 1;
            this.export.Text = "Export";
            this.export.UseVisualStyleBackColor = true;
            // 
            // language
            // 
            this.language.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.language.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.language.DropDownWidth = 120;
            this.language.FormattingEnabled = true;
            this.language.Items.AddRange(new object[] {
            "Choose Language",
            "C++"});
            this.language.Location = new System.Drawing.Point(667, 12);
            this.language.Name = "language";
            this.language.Size = new System.Drawing.Size(120, 21);
            this.language.TabIndex = 0;
            this.language.SelectedIndexChanged += new System.EventHandler(this.language_SelectedIndexChanged);
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
            this.splitContainer2.Panel1.Controls.Add(this.blocks);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer2.Panel2.Controls.Add(this.blockCloser);
            this.splitContainer2.Panel2.Controls.Add(this.files);
            this.splitContainer2.Panel2.DoubleClick += new System.EventHandler(this.AddFile);
            this.splitContainer2.Size = new System.Drawing.Size(800, 401);
            this.splitContainer2.SplitterDistance = 169;
            this.splitContainer2.TabIndex = 0;
            this.splitContainer2.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer2_SplitterMoved);
            // 
            // blockCloser
            // 
            this.blockCloser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.blockCloser.AutoSize = true;
            this.blockCloser.Location = new System.Drawing.Point(0, 388);
            this.blockCloser.Name = "blockCloser";
            this.blockCloser.Size = new System.Drawing.Size(13, 13);
            this.blockCloser.TabIndex = 0;
            this.blockCloser.Text = "<";
            this.blockCloser.Click += new System.EventHandler(this.blockCloser_Click);
            // 
            // files
            // 
            this.files.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.files.Location = new System.Drawing.Point(0, 0);
            this.files.Margin = new System.Windows.Forms.Padding(0);
            this.files.Name = "files";
            this.files.SelectedIndex = 0;
            this.files.Size = new System.Drawing.Size(627, 401);
            this.files.TabIndex = 1;
            this.files.Visible = false;
            // 
            // blocks
            // 
            this.blocks.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.blocks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.blocks.Controls.Add(this.variables);
            this.blocks.Controls.Add(this.ifStatements);
            this.blocks.Controls.Add(this.loops);
            this.blocks.Controls.Add(this.math);
            this.blocks.Location = new System.Drawing.Point(0, 0);
            this.blocks.Multiline = true;
            this.blocks.Name = "blocks";
            this.blocks.SelectedIndex = 0;
            this.blocks.Size = new System.Drawing.Size(170, 401);
            this.blocks.TabIndex = 0;
            // 
            // variables
            // 
            this.variables.Location = new System.Drawing.Point(23, 4);
            this.variables.Name = "variables";
            this.variables.Size = new System.Drawing.Size(143, 393);
            this.variables.TabIndex = 0;
            this.variables.Text = "Variables";
            this.variables.UseVisualStyleBackColor = true;
            // 
            // ifStatements
            // 
            this.ifStatements.Location = new System.Drawing.Point(23, 4);
            this.ifStatements.Name = "ifStatements";
            this.ifStatements.Size = new System.Drawing.Size(143, 393);
            this.ifStatements.TabIndex = 1;
            this.ifStatements.Text = "If Statements";
            this.ifStatements.UseVisualStyleBackColor = true;
            // 
            // loops
            // 
            this.loops.Location = new System.Drawing.Point(23, 4);
            this.loops.Name = "loops";
            this.loops.Size = new System.Drawing.Size(143, 393);
            this.loops.TabIndex = 2;
            this.loops.Text = "Loops";
            this.loops.UseVisualStyleBackColor = true;
            // 
            // math
            // 
            this.math.Location = new System.Drawing.Point(23, 4);
            this.math.Name = "math";
            this.math.Size = new System.Drawing.Size(143, 393);
            this.math.TabIndex = 3;
            this.math.Text = "Math";
            this.math.UseVisualStyleBackColor = true;
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
            this.blocks.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Label blockCloser;
        private System.Windows.Forms.ComboBox language;
        private System.Windows.Forms.TabControl files;
        private System.Windows.Forms.Label to;
        private System.Windows.Forms.Button export;
        private System.Windows.Forms.TabControl blocks;
        private System.Windows.Forms.TabPage variables;
        private System.Windows.Forms.TabPage ifStatements;
        private System.Windows.Forms.TabPage loops;
        private System.Windows.Forms.TabPage math;
    }
}

