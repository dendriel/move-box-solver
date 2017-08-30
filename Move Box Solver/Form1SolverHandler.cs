using Move_Box_Solver.Source.Game;
using Move_Box_Solver.Source.Solver;
using System;
using System.Windows.Forms;

namespace Move_Box_Solver
{
	/// <summary>
	/// Holds solver related interface features.
	/// </summary>
	public partial class Form1 : Form
	{
		private void SolveButton_Click(object sender, EventArgs e)
		{
			if (SolveButton.Text == "Solve") {
				StartSolver();

			} else {
				StopSolver();
			}
		}

		/// <summary>
		/// Tun the solver to find a solution for the current scenario.
		/// </summary>
		private void StartSolver()
		{
			SetupActionButtonsBeforeSolving();

			currSolution = null;

			// Reload scenario so the algorithm will solve a map from its initial state.
			man.ReloadScenario();
			
			runningSolver = true;
			SolverBackgroundWorker.RunWorkerAsync(man.CopyMap());

			SolveButton.Text = "Cancel";
			SolveButton.Enabled = true;
		}

		/// <summary>
		/// Stops the current solving process.
		/// </summary>
		private void StopSolver()
		{
			SolverBackgroundWorker.CancelAsync();
			ClearInterfaceValues();

			SolveButton.Text = "Solve";
			SolveButton.Enabled = true;
		}

		/// <summary>
		/// Setup interface buttons when the solver is started.
		/// </summary>
		private void SetupActionButtonsBeforeSolving()
		{
			LoadScenarioButton.Enabled = false;
			ReloadScenarioButton.Enabled = false;
			PlaybackButton.Enabled = false;
			SolveButton.Enabled = false;

			SolutionTextBox.Text = "";
		}

		/// <summary>
		/// Setup interface buttons when the solver has finished.
		/// </summary>
		private void SetupActionButtonsAfterSolving()
		{
			LoadScenarioButton.Enabled = true;
			ReloadScenarioButton.Enabled = true;
			PlaybackButton.Enabled = true;

			SolveButton.Text = "Solve";
		}

		private void SolverBackgroundWorker_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
		{
			MBMazeNode startingNode = new MBMazeNode(null, Directions.NONE, e.Argument as ScenarioData.MapElements[,]);
			solver = new MBMazeSolver(startingNode, () => { return UpdateGraphicsWhileSolving; }, SolverBackgroundWorker);

			currSolution = solver.FindDirections();
		}

		private void SolverBackgroundWorker_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
		{
			RefreshSolverInterfaceValues();

			if (UpdateGraphicsWhileSolving) {
				MBMazeNode currNode = solver.CurrNode as MBMazeNode;
				Ref.LoadMapState(currNode.MazeState);
				Application.DoEvents();
			}
		}

		private void SolverBackgroundWorker_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
		{
			// Solver may be interrupted and we won't have a solution.
			if (currSolution == null) return;

			DisplaySolution();

			RefreshSolverInterfaceValues();

			SetupActionButtonsAfterSolving();

			runningSolver = false;
		}
	}
}
