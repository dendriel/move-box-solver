using Move_Box_Solver.Properties;
using Move_Box_Solver.Source.Game;
using Move_Box_Solver.Source.Solver;
using Newtonsoft.Json.Linq;
using System;
using System.Threading;
using System.Windows.Forms;

namespace Move_Box_Solver
{
	public partial class Form1 : Form
	{
		/// <summary>
		/// Default scenario.
		/// </summary>
		const int DEFAULT_SCENARIO = 0;

		/// <summary>
		/// Default delay between movements while in playback.
		/// </summary>
		const int DEFAULT_DELAY_BETWEEN_MOVES_IN_MS = 100;

		/// <summary>
		/// Global reference from this form.
		/// </summary>
		public static Form1 Ref { get; private set; }

		/// <summary>
		/// Scenario manager reference.
		/// </summary>
		GameManager man;

		/// <summary>
		/// Solution for the current state of the map.
		/// </summary>
		Directions[] currSolution = null;

		/// <summary>
		/// Solver algorithm.
		/// </summary>
		MBMazeSolver solver;

		/// <summary>
		/// The solver is searching for a solution?
		/// </summary>
		bool runningSolver = false;

		/// <summary>
		/// The solution for a maze is being executed?
		/// </summary>
		bool runningPlayback = false;

		/// <summary>
		/// Update graphics while solving the maaze?
		/// </summary>
		bool UpdateGraphicsWhileSolving { get; set; }

		/// <summary>
		/// Hold form texts.
		/// </summary>
		dynamic formTexts;

		public Form1()
		{
			InitializeComponent();
			Ref = this;
			formTexts = JObject.Parse(Resources.FormTexts);
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			UpdateGraphicsWhileSolving = EnableGraphicsCheckBox.Checked;

			man = new GameManager();
			SyncScenarioNumericUpDown();

			if (man.ScenarioAmount > 0) {
				LoadScenario(DEFAULT_SCENARIO);
			} else {
				MessageBox.Show("No scenarios could be loaded. Aborting the program...", "Warning");
				Application.Exit();
			}
		}

		/// <summary>
		/// Load a scenario.
		/// </summary>
		/// <param name="idx">The scenario index.</param>
		void LoadScenario(int idx)
		{
			man.LoadScenario(idx);

			ClearInterfaceValues();
			UpdateActionButtons(true);
			PlaybackButton.Enabled = false;

			UpdateFormTitle();
		}

		/// <summary>
		/// Update the form title with scenario name + information.
		/// </summary>
		public void UpdateFormTitle()
		{
			Text = man.ScenarioName + " [" + man.ScenarioInfo + "]";
		}

		/// <summary>
		/// Add a new picture box into the form.
		/// </summary>
		/// <param name="picBox">The picture box to be added.</param>
		public void SetupPictureBox(PictureBox picBox)
		{
			Controls.Add(picBox);
		}

		/// <summary>
		/// Reset the screen graphics.
		/// </summary>
		public static void ResetGraphics()
		{
			for (int ix = Ref.Controls.Count - 1; ix >= 0; ix--) {
				if (Ref.Controls[ix] is PictureBox) Ref.Controls[ix].Dispose();
			}
		}

		/// <summary>
		/// Execute the given list of steps for solving a maze.
		/// </summary>
		/// <param name="solution">The solution to be executed.</param>
		/// <param name="delayBetweenMovesInMs">Time to wait between movements in miliseconds.</param>
		void PlaybackSolution(Directions[] solution, int delayBetweenMovesInMs = DEFAULT_DELAY_BETWEEN_MOVES_IN_MS)
		{
			// Reload scenario so the user won't try to execute the solution in an invalid map state.
			man.ReloadScenario();

			foreach (Directions dir in solution) {
				man.MovePlayer(dir);
				Application.DoEvents();
				Thread.Sleep(delayBetweenMovesInMs);

				if (IsPlaybackCanceled()) break;
			}
		}

		/// <summary>
		/// Load a map state.
		/// </summary>
		/// <param name="state">The map state to be loaded.</param>
		public void LoadMapState(ScenarioData.MapElements[,] state)
		{
			man.LoadMapState(state);
		}

		private void LoadScenarioButton_Click(object sender, EventArgs e)
		{
			// The numericUpDown starts at 1 (but our internal indexing starts at 0).
			int scenarioIdx = (int)ScenarioNumericUpDown.Value - 1;
			LoadScenario(scenarioIdx);
		}

		/// <summary>
		/// Clear all dynamic contents from interface.
		/// </summary>
		private void ClearInterfaceValues()
		{
			OpenNodesToolStripStatusLabel.Text = "Open List: 0";
			ClosedNodesToolStripStatusLabel.Text = "Closed List: 0";
			ElapsedTimeToolStripStatusLabel.Text = "Time Consumed (ms): 0 miliseconds";
			BoxesLeftToolStripStatusLabel.Text = "Boxes Ready: 0/0";

			currSolution = null;
			SolutionTextBox.Text = "";

			PlaybackButton.Enabled = false;
			LoadScenarioButton.Enabled = true;
			SolveButton.Enabled = true;
			SolveButton.Text = "Solve";
		}

		/// <summary>
		/// Update all solving status according to current solver state.
		/// </summary>
		private void RefreshSolverInterfaceValues()
		{
			OpenNodesToolStripStatusLabel.Text = "Open List: " + solver.OpenListCount;
			ClosedNodesToolStripStatusLabel.Text = "Closed List: " + solver.ClosedListCount;
			ElapsedTimeToolStripStatusLabel.Text = "Time Consumed: " + solver.ElapsedTimeInMs + " miliseconds";

			MBMazeNode currNode = solver.CurrNode as MBMazeNode;
			int boxes = GameInteraction.FindElemsPosition(ScenarioData.MapElements.BOX, currNode.MazeState).Length;
			int boxes_over_target = GameInteraction.FindElemsPosition(ScenarioData.MapElements.BOX_OVER_TARGET, currNode.MazeState).Length;
			BoxesLeftToolStripStatusLabel.Text = "Boxes Ready: " + boxes_over_target + "/" + (boxes + boxes_over_target);
		}

		/// <summary>
		/// Disable all action buttons (useful when solving a maze).
		/// </summary>
		/// <param name="enabled">true - enable buttons; false - disable buttons.</param>
		private void UpdateActionButtons(bool enabled = false)
		{
			LoadScenarioButton.Enabled = enabled;
			ReloadScenarioButton.Enabled = enabled;
			PlaybackButton.Enabled = enabled;
		}

		/// <summary>
		/// Display the current solution in the Solution text box.
		/// </summary>
		private void DisplaySolution()
		{
			SolutionTextBox.Text = "";
			for (int i = 0; i < currSolution.Length; i++) {
				SolutionTextBox.AppendText("[" + (i + 1) + "] - " + currSolution[i]);

				if ((i + 1) < currSolution.Length) {
					SolutionTextBox.AppendText(Environment.NewLine);
				}
			}
		}

		/// <summary>
		/// Allows the user to play the game without using the solver.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			// Can't move while solver is running.
			if (runningSolver) {
				MessageBox.Show("Can't play while the solver is still running.");
				return;

			/* When the game is solved by the solver, we won't allow the player to move
			 * anymore.
			 */
			} else if (man.CheckVictory()) {
				return;
			}

			Directions dir = GameInteraction.KeyToDirection(e.KeyCode);
			if (dir != Directions.NONE) {
				man.MovePlayer(dir);
			}

			if (man.CheckVictory()) {
				MessageBox.Show("Solution found!", "You won!");
				LoadNextScenario();
			}
		}

		/// <summary>
		/// Load the next scenario (if not in the last scenario).
		/// </summary>
		private void LoadNextScenario()
		{
			if ((int)ScenarioNumericUpDown.Value >= man.ScenarioAmount) {
				return;
			}

			LoadScenario((int)ScenarioNumericUpDown.Value);
			ScenarioNumericUpDown.Value += 1;
		}

		/// <summary>
		/// Display the loaded scenario (if loaded multiple files, display the first in the list).
		/// </summary>
		private void DisplayLoadedScenario()
		{
			int prevMax = (int)ScenarioNumericUpDown.Maximum;
			SyncScenarioNumericUpDown();

			ScenarioNumericUpDown.Value = prevMax + 1;

			LoadScenario(prevMax);
		}

		/// <summary>
		/// Synchronize maximum value of scenario selector with the amount of loaded scenarios.
		/// </summary>
		private void SyncScenarioNumericUpDown()
		{
			ScenarioNumericUpDown.Maximum = man.ScenarioAmount;
		}

		private void EnableGraphicsCheckBox_CheckedChanged(object sender, EventArgs e)
		{
			UpdateGraphicsWhileSolving = EnableGraphicsCheckBox.Checked;
		}

		private void ReloadScenarioButton_Click(object sender, EventArgs e)
		{
			man.ReloadScenario();
			if (currSolution != null) {
				PlaybackButton.Enabled = true;
			}
		}

		private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DialogResult dialogResult = MessageBox.Show("Are you sure?", "Close Move Box Solver", MessageBoxButtons.YesNo);

			if (dialogResult == DialogResult.Yes) {
				StopPlayback();
				StopSolver();
				Application.Exit();
			}
		}

		private void LoadScenarioToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (runningSolver) {
				MessageBox.Show("Please, cancel the current solver execution in order to load a scenario.", "Can't load scenarios while solving a maze");
				return;
			}

			OpenFileDialog choofdlog = new OpenFileDialog() {
				Filter = "JSON Files (*.json)|*.json",
				Multiselect = true,
			};

			if (choofdlog.ShowDialog() != DialogResult.OK) {
				return;
			}

			string[] selectedFiles = choofdlog.FileNames;

			int loaded = man.LoadScenarioData(selectedFiles);
			if (loaded > 0) {
				DisplayLoadedScenario();
			}
		}

		/// <summary>
		/// Override OnFormClosing so we can cancel solution playback before exiting.
		/// (otherwise, the program will keep running in background).
		/// </summary>
		/// <param name="e">Unused.</param>
		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			StopPlayback();
			StopSolver();

			base.OnFormClosing(e);
		}

		/// <summary>
		/// Display an informational message (loaded from FormTexts metadata file).
		/// </summary>
		/// <param name="keyPrefix">Key prefix of the information.</param>
		private void DisplayInfoMessage(string keyPrefix)
		{
			string title = formTexts[keyPrefix + "_title"];
			string content = formTexts[keyPrefix + "_content"];

			MessageBox.Show(content, title);
		}
		
		private void HowToSolveScenariosToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DisplayInfoMessage("how_to_solve_scenarios");
		}

		private void HowToLoadScenariosToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DisplayInfoMessage("how_to_load_scenarios");
		}

		private void ReadInfosToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DisplayInfoMessage("how_to_read_info");
		}

		private void CreditsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DisplayInfoMessage("credits");
		}

		private void HeuristicsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DisplayInfoMessage("heuristics");
		}
	}
}
