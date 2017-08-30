using System.Collections.Generic;
using System.Drawing;

namespace Move_Box_Solver.Source.Game
{
	/// <summary>
	/// Handles game interactions.
	/// </summary>
	class GameManager
	{
		/// <summary>
		/// Current loaded scenario name.
		/// </summary>
		public string ScenarioName {
			get { return (scenMan.LoadedScenario != null) ? scenMan.LoadedScenario.Name : ""; }
		}

		/// <summary>
		/// Current loaded scenario information.
		/// </summary>
		public string ScenarioInfo {
			get { return (scenMan.LoadedScenario != null) ? scenMan.LoadedScenario.Info : ""; }
		}

		/// <summary>
		/// Return the amount of available scenarios.
		/// </summary>
		public int ScenarioAmount { get { return scenMan.ScenarioAmount; } }

		/// <summary>
		/// Reference to a scenario manager.
		/// </summary>
		ScenarioManager scenMan;

		/// <summary>
		/// Create a new instance of GameManager.
		/// </summary>
		public GameManager()
		{
			scenMan = new ScenarioManager();
		}

		/// <summary>
		/// Load a map state.
		/// </summary>
		/// <param name="state">The map state to be loaded.</param>
		public void LoadMapState(ScenarioData.MapElements[,] state)
		{
			scenMan.LoadedScenario.LoadElements(state);
		}

		/// <summary>
		/// Move the player towards the given direction.
		/// </summary>
		/// <param name="dir">Direction to move.</param>
		/// <returns>true if moved; false if the path is blocked.</returns>
		public bool MovePlayer(Directions dir)
		{
			if (GameInteraction.MovePlayer(dir, scenMan.LoadedScenario.Map)) {
				UpdatePlayerPosition(dir);
				UpdateBoxesPositions();

				return true;

			} else {
				return false;
			}			
		}

		/// <summary>
		/// Update player sprite position in the scenario.
		/// </summary>
		/// <param name="dir">Direction in which the player moved.</param>
		private void UpdatePlayerPosition(Directions dir)
		{
			scenMan.LoadedScenario.Player.Move(dir);
		}

		/// <summary>
		/// Update all boxes sprite positions..
		/// </summary>
		private void UpdateBoxesPositions()
		{
			List<Point> allBoxesPos = new List<Point>();
			allBoxesPos.AddRange(GameInteraction.FindElemsPosition(ScenarioData.MapElements.BOX, scenMan.LoadedScenario.Map));
			allBoxesPos.AddRange(GameInteraction.FindElemsPosition(ScenarioData.MapElements.BOX_OVER_TARGET, scenMan.LoadedScenario.Map));

			for (int i = 0; i < allBoxesPos.Count; i++) {
				// Set box position.
				Box currBox = scenMan.LoadedScenario.BoxList[i];
				currBox.Position = allBoxesPos[i];
				// Update box state.
				bool boxIsReady = (scenMan.LoadedScenario.Map[currBox.Position.X, currBox.Position.Y] == ScenarioData.MapElements.BOX_OVER_TARGET);
				currBox.IsReady = boxIsReady;
			}
		}

		/// <summary>
		/// Check the victory condition (all boxes ready).
		/// </summary>
		/// <returns>true if the game as won; false otherwise.</returns>
		public bool CheckVictory()
		{
			return GameInteraction.CheckVictory(scenMan.LoadedScenario.Map);
		}

		/// <summary>
		/// Create a copy of the current map state.
		/// </summary>
		/// <returns>The current map state.</returns>
		public ScenarioData.MapElements[,] CopyMap()
		{
			return scenMan.LoadedScenario.Map.Clone() as ScenarioData.MapElements[,];
		}

		/// <summary>
		/// Load scenario by its index.
		/// </summary>
		/// <param name="id">Scenario index.</param>
		public void LoadScenario(int idx)
		{
			scenMan.LoadScenario(idx);
		}

		/// <summary>
		/// Reload the current scenario.
		/// </summary>
		public void ReloadScenario()
		{
			scenMan.ReloadScenario();
		}

		/// Load scenarios data from file.
		/// </summary>
		/// <param name="scenariosToLoad">List of scenarios filenames to be loaded.</param>
		/// <returns>The amount of succefully loaded scenarios.</returns>
		public int LoadScenarioData(string[] scenariosToLoad)
		{
			return scenMan.LoadScenarioData(scenariosToLoad);
		}
	}
}
