namespace Move_Box_Solver
{
	partial class Form1
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
			if (disposing && (components != null)) {
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.ReloadScenarioButton = new System.Windows.Forms.Button();
			this.EnableGraphicsCheckBox = new System.Windows.Forms.CheckBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.DelayBetweenMovesNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.SolutionTextBox = new System.Windows.Forms.TextBox();
			this.PlaybackButton = new System.Windows.Forms.Button();
			this.SolveButton = new System.Windows.Forms.Button();
			this.LoadScenarioButton = new System.Windows.Forms.Button();
			this.ScenarioNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.OpenNodesToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.ClosedNodesToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.ElapsedTimeToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.BoxesLeftToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.SolverBackgroundWorker = new System.ComponentModel.BackgroundWorker();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.LoadScenarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.HowToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.HowToSolveScenariosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.HowToLoadScenariosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ReadInfosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.HeuristicsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.CreditsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.panel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DelayBetweenMovesNumericUpDown)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ScenarioNumericUpDown)).BeginInit();
			this.statusStrip1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
			this.panel1.Controls.Add(this.groupBox1);
			this.panel1.Location = new System.Drawing.Point(645, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(204, 675);
			this.panel1.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.ReloadScenarioButton);
			this.groupBox1.Controls.Add(this.EnableGraphicsCheckBox);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.DelayBetweenMovesNumericUpDown);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.SolutionTextBox);
			this.groupBox1.Controls.Add(this.PlaybackButton);
			this.groupBox1.Controls.Add(this.SolveButton);
			this.groupBox1.Controls.Add(this.LoadScenarioButton);
			this.groupBox1.Controls.Add(this.ScenarioNumericUpDown);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.groupBox1.Location = new System.Drawing.Point(7, 2);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(191, 660);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Simulation Controls";
			// 
			// ReloadScenarioButton
			// 
			this.ReloadScenarioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ReloadScenarioButton.Location = new System.Drawing.Point(98, 62);
			this.ReloadScenarioButton.Name = "ReloadScenarioButton";
			this.ReloadScenarioButton.Size = new System.Drawing.Size(81, 33);
			this.ReloadScenarioButton.TabIndex = 12;
			this.ReloadScenarioButton.Text = "Reload";
			this.ReloadScenarioButton.UseVisualStyleBackColor = true;
			this.ReloadScenarioButton.Click += new System.EventHandler(this.ReloadScenarioButton_Click);
			// 
			// EnableGraphicsCheckBox
			// 
			this.EnableGraphicsCheckBox.AutoSize = true;
			this.EnableGraphicsCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.EnableGraphicsCheckBox.Location = new System.Drawing.Point(13, 105);
			this.EnableGraphicsCheckBox.Name = "EnableGraphicsCheckBox";
			this.EnableGraphicsCheckBox.Size = new System.Drawing.Size(146, 24);
			this.EnableGraphicsCheckBox.TabIndex = 11;
			this.EnableGraphicsCheckBox.Text = "Enable Graphics";
			this.EnableGraphicsCheckBox.UseVisualStyleBackColor = true;
			this.EnableGraphicsCheckBox.Visible = false;
			this.EnableGraphicsCheckBox.CheckedChanged += new System.EventHandler(this.EnableGraphicsCheckBox_CheckedChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(118, 571);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(63, 20);
			this.label4.TabIndex = 10;
			this.label4.Text = "Mili Sec";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(9, 546);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(170, 20);
			this.label3.TabIndex = 9;
			this.label3.Text = "Delay Between Moves:";
			// 
			// DelayBetweenMovesNumericUpDown
			// 
			this.DelayBetweenMovesNumericUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.DelayBetweenMovesNumericUpDown.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.DelayBetweenMovesNumericUpDown.InterceptArrowKeys = false;
			this.DelayBetweenMovesNumericUpDown.Location = new System.Drawing.Point(13, 569);
			this.DelayBetweenMovesNumericUpDown.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
			this.DelayBetweenMovesNumericUpDown.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.DelayBetweenMovesNumericUpDown.Name = "DelayBetweenMovesNumericUpDown";
			this.DelayBetweenMovesNumericUpDown.Size = new System.Drawing.Size(99, 26);
			this.DelayBetweenMovesNumericUpDown.TabIndex = 8;
			this.DelayBetweenMovesNumericUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.DelayBetweenMovesNumericUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(7, 179);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(71, 20);
			this.label2.TabIndex = 7;
			this.label2.Text = "Solution:";
			// 
			// SolutionTextBox
			// 
			this.SolutionTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SolutionTextBox.Location = new System.Drawing.Point(11, 202);
			this.SolutionTextBox.Multiline = true;
			this.SolutionTextBox.Name = "SolutionTextBox";
			this.SolutionTextBox.ReadOnly = true;
			this.SolutionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.SolutionTextBox.Size = new System.Drawing.Size(173, 341);
			this.SolutionTextBox.TabIndex = 6;
			// 
			// PlaybackButton
			// 
			this.PlaybackButton.Enabled = false;
			this.PlaybackButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.PlaybackButton.Location = new System.Drawing.Point(11, 602);
			this.PlaybackButton.Name = "PlaybackButton";
			this.PlaybackButton.Size = new System.Drawing.Size(173, 52);
			this.PlaybackButton.TabIndex = 5;
			this.PlaybackButton.Text = "Playback";
			this.PlaybackButton.UseVisualStyleBackColor = true;
			this.PlaybackButton.Click += new System.EventHandler(this.PlaybackButton_Click);
			// 
			// SolveButton
			// 
			this.SolveButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.SolveButton.Location = new System.Drawing.Point(13, 135);
			this.SolveButton.Name = "SolveButton";
			this.SolveButton.Size = new System.Drawing.Size(166, 34);
			this.SolveButton.TabIndex = 4;
			this.SolveButton.Text = "Solve";
			this.SolveButton.UseVisualStyleBackColor = true;
			this.SolveButton.Click += new System.EventHandler(this.SolveButton_Click);
			// 
			// LoadScenarioButton
			// 
			this.LoadScenarioButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.LoadScenarioButton.Location = new System.Drawing.Point(11, 62);
			this.LoadScenarioButton.Name = "LoadScenarioButton";
			this.LoadScenarioButton.Size = new System.Drawing.Size(81, 33);
			this.LoadScenarioButton.TabIndex = 3;
			this.LoadScenarioButton.Text = "Load";
			this.LoadScenarioButton.UseVisualStyleBackColor = true;
			this.LoadScenarioButton.Click += new System.EventHandler(this.LoadScenarioButton_Click);
			// 
			// ScenarioNumericUpDown
			// 
			this.ScenarioNumericUpDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ScenarioNumericUpDown.InterceptArrowKeys = false;
			this.ScenarioNumericUpDown.Location = new System.Drawing.Point(85, 30);
			this.ScenarioNumericUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.ScenarioNumericUpDown.Name = "ScenarioNumericUpDown";
			this.ScenarioNumericUpDown.Size = new System.Drawing.Size(99, 26);
			this.ScenarioNumericUpDown.TabIndex = 2;
			this.ScenarioNumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(7, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 20);
			this.label1.TabIndex = 1;
			this.label1.Text = "Scenario";
			// 
			// statusStrip1
			// 
			this.statusStrip1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenNodesToolStripStatusLabel,
            this.ClosedNodesToolStripStatusLabel,
            this.ElapsedTimeToolStripStatusLabel,
            this.BoxesLeftToolStripStatusLabel});
			this.statusStrip1.Location = new System.Drawing.Point(0, 665);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(848, 22);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.Stretch = false;
			this.statusStrip1.TabIndex = 1;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// OpenNodesToolStripStatusLabel
			// 
			this.OpenNodesToolStripStatusLabel.Name = "OpenNodesToolStripStatusLabel";
			this.OpenNodesToolStripStatusLabel.Size = new System.Drawing.Size(85, 17);
			this.OpenNodesToolStripStatusLabel.Text = "Open Nodes: 0";
			// 
			// ClosedNodesToolStripStatusLabel
			// 
			this.ClosedNodesToolStripStatusLabel.Name = "ClosedNodesToolStripStatusLabel";
			this.ClosedNodesToolStripStatusLabel.Size = new System.Drawing.Size(92, 17);
			this.ClosedNodesToolStripStatusLabel.Text = "Closed Nodes: 0";
			// 
			// ElapsedTimeToolStripStatusLabel
			// 
			this.ElapsedTimeToolStripStatusLabel.Name = "ElapsedTimeToolStripStatusLabel";
			this.ElapsedTimeToolStripStatusLabel.Size = new System.Drawing.Size(107, 17);
			this.ElapsedTimeToolStripStatusLabel.Text = "Time Consumed: 0";
			// 
			// BoxesLeftToolStripStatusLabel
			// 
			this.BoxesLeftToolStripStatusLabel.Name = "BoxesLeftToolStripStatusLabel";
			this.BoxesLeftToolStripStatusLabel.Size = new System.Drawing.Size(549, 17);
			this.BoxesLeftToolStripStatusLabel.Spring = true;
			this.BoxesLeftToolStripStatusLabel.Text = "Boxes Left: 0";
			this.BoxesLeftToolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// SolverBackgroundWorker
			// 
			this.SolverBackgroundWorker.WorkerReportsProgress = true;
			this.SolverBackgroundWorker.WorkerSupportsCancellation = true;
			this.SolverBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.SolverBackgroundWorker_DoWork);
			this.SolverBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.SolverBackgroundWorker_ProgressChanged);
			this.SolverBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.SolverBackgroundWorker_RunWorkerCompleted);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.aboutToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(848, 24);
			this.menuStrip1.TabIndex = 2;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LoadScenarioToolStripMenuItem,
            this.ExitToolStripMenuItem});
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
			this.toolStripMenuItem1.Text = "File";
			// 
			// LoadScenarioToolStripMenuItem
			// 
			this.LoadScenarioToolStripMenuItem.Name = "LoadScenarioToolStripMenuItem";
			this.LoadScenarioToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.LoadScenarioToolStripMenuItem.Text = "Load Scenario";
			this.LoadScenarioToolStripMenuItem.Click += new System.EventHandler(this.LoadScenarioToolStripMenuItem_Click);
			// 
			// ExitToolStripMenuItem
			// 
			this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
			this.ExitToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
			this.ExitToolStripMenuItem.Text = "Exit";
			this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HowToToolStripMenuItem,
            this.HeuristicsToolStripMenuItem,
            this.CreditsToolStripMenuItem});
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
			this.aboutToolStripMenuItem.Text = "About";
			// 
			// HowToToolStripMenuItem
			// 
			this.HowToToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HowToSolveScenariosToolStripMenuItem,
            this.HowToLoadScenariosToolStripMenuItem,
            this.ReadInfosToolStripMenuItem});
			this.HowToToolStripMenuItem.Name = "HowToToolStripMenuItem";
			this.HowToToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
			this.HowToToolStripMenuItem.Text = "How To";
			// 
			// HowToSolveScenariosToolStripMenuItem
			// 
			this.HowToSolveScenariosToolStripMenuItem.Name = "HowToSolveScenariosToolStripMenuItem";
			this.HowToSolveScenariosToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
			this.HowToSolveScenariosToolStripMenuItem.Text = "Solve Scenarios";
			this.HowToSolveScenariosToolStripMenuItem.Click += new System.EventHandler(this.HowToSolveScenariosToolStripMenuItem_Click);
			// 
			// HowToLoadScenariosToolStripMenuItem
			// 
			this.HowToLoadScenariosToolStripMenuItem.Name = "HowToLoadScenariosToolStripMenuItem";
			this.HowToLoadScenariosToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
			this.HowToLoadScenariosToolStripMenuItem.Text = "Load Scenarios";
			this.HowToLoadScenariosToolStripMenuItem.Click += new System.EventHandler(this.HowToLoadScenariosToolStripMenuItem_Click);
			// 
			// ReadInfosToolStripMenuItem
			// 
			this.ReadInfosToolStripMenuItem.Name = "ReadInfosToolStripMenuItem";
			this.ReadInfosToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
			this.ReadInfosToolStripMenuItem.Text = "Read Infos";
			this.ReadInfosToolStripMenuItem.Click += new System.EventHandler(this.ReadInfosToolStripMenuItem_Click);
			// 
			// HeuristicsToolStripMenuItem
			// 
			this.HeuristicsToolStripMenuItem.Name = "HeuristicsToolStripMenuItem";
			this.HeuristicsToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
			this.HeuristicsToolStripMenuItem.Text = "Heuristics";
			this.HeuristicsToolStripMenuItem.Click += new System.EventHandler(this.HeuristicsToolStripMenuItem_Click);
			// 
			// CreditsToolStripMenuItem
			// 
			this.CreditsToolStripMenuItem.Name = "CreditsToolStripMenuItem";
			this.CreditsToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
			this.CreditsToolStripMenuItem.Text = "Credits";
			this.CreditsToolStripMenuItem.Click += new System.EventHandler(this.CreditsToolStripMenuItem_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
			this.ClientSize = new System.Drawing.Size(848, 687);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.menuStrip1);
			this.Controls.Add(this.panel1);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.KeyPreview = true;
			this.MainMenuStrip = this.menuStrip1;
			this.MaximizeBox = false;
			this.Name = "Form1";
			this.Text = "Move Box Solver";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
			this.panel1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.DelayBetweenMovesNumericUpDown)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ScenarioNumericUpDown)).EndInit();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.NumericUpDown ScenarioNumericUpDown;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button LoadScenarioButton;
		private System.Windows.Forms.Button PlaybackButton;
		private System.Windows.Forms.Button SolveButton;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox SolutionTextBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown DelayBetweenMovesNumericUpDown;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel OpenNodesToolStripStatusLabel;
		private System.Windows.Forms.ToolStripStatusLabel ClosedNodesToolStripStatusLabel;
		private System.ComponentModel.BackgroundWorker SolverBackgroundWorker;
		private System.Windows.Forms.ToolStripStatusLabel ElapsedTimeToolStripStatusLabel;
		private System.Windows.Forms.CheckBox EnableGraphicsCheckBox;
		private System.Windows.Forms.Button ReloadScenarioButton;
		private System.Windows.Forms.ToolStripStatusLabel BoxesLeftToolStripStatusLabel;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem LoadScenarioToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem HowToToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem HeuristicsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem CreditsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem HowToSolveScenariosToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem HowToLoadScenariosToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ReadInfosToolStripMenuItem;
	}
}

