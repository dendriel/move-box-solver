using System;
using System.Windows.Forms;

namespace Move_Box_Solver
{
	/// <summary>
	/// Holds playback related interface features.
	/// </summary>
	public partial class Form1 : Form
	{
		private void PlaybackButton_Click(object sender, EventArgs e)
		{
			if (PlaybackButton.Text == "Playback") {
				StartPlayback();

			} else {
				StopPlayback();
			}
		}

		/// <summary>
		/// Execute the solution.
		/// </summary>
		private void StartPlayback()
		{
			SetActionButtonsBeforePlayback();

			runningPlayback = true;
			PlaybackSolution(currSolution, (int)DelayBetweenMovesNumericUpDown.Value);
			runningPlayback = false;

			SetActionButtonsAfterPlayback();
		}

		/// <summary>
		/// Cancel the solution playback.
		/// </summary>
		private void StopPlayback()
		{
			runningPlayback = false;
		}

		/// <summary>
		/// Setup interface buttons when the playback is started.
		/// </summary>
		private void SetActionButtonsBeforePlayback()
		{
			LoadScenarioButton.Enabled = false;
			ReloadScenarioButton.Enabled = false;
			SolveButton.Enabled = false;

			PlaybackButton.Text = "Cancel";
		}

		/// <summary>
		/// Setup interface buttons when the playback is finished.
		/// </summary>
		private void SetActionButtonsAfterPlayback()
		{
			LoadScenarioButton.Enabled = true;
			ReloadScenarioButton.Enabled = true;
			SolveButton.Enabled = true;
			PlaybackButton.Enabled = false;

			PlaybackButton.Text = "Playback";
		}

		/// <summary>
		/// Checks if the solution playback was canceled.
		/// </summary>
		private bool IsPlaybackCanceled()
		{
			return !runningPlayback;
		}
	}
}
