using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace Move_Box_Solver.Source.Game
{
	/// <summary>
	/// Handles scenarios data.
	/// </summary>
	class ScenarioManager
	{
		/// <summary>
		/// Scenarios data directory name.
		/// </summary>
		public const string SCENARIOS_DIR = "scenarios";

		/// <summary>
		/// Sprites witdth.
		/// </summary>
		public const int SPRITE_WIDTH = 64;

		/// <summary>
		/// Sprites height.
		/// </summary>
		public const int SPRITE_HEIGHT = 64;
		
		/// <summary>
		/// Return the amount of available scenarios.
		/// </summary>
		public int ScenarioAmount { get { return scenariosData.Count; } }

		/// <summary>
		/// List of all loaded scenarios from scenarios directory.
		/// </summary>
		List<ScenarioData> scenariosData;

		/// <summary>
		/// Currently loaded scenario.
		/// </summary>
		public Scenario LoadedScenario { get; private set; }

		/// <summary>
		/// Create a new ScenarioManager.
		/// </summary>
		public ScenarioManager()
		{
			scenariosData = new List<ScenarioData>();

			LoadScenarioData();
		}

		/// <summary>
		/// Load scenarios data from file.
		/// </summary>
		/// <param name="scenariosToLoad">List of scenarios filenames to be loaded.</param>
		/// <returns>The amount of succefully loaded scenarios.</returns>
		public int LoadScenarioData(string[] scenariosToLoad=null)
		{
			string[] scenFileArray = scenariosToLoad ?? Directory.GetFiles(@SCENARIOS_DIR);

			int loaded = 0;
			foreach(string scenFile in scenFileArray) {

				ScenarioData newScenData = new ScenarioData(@scenFile);
				
				if (newScenData.Loaded) {
					scenariosData.Add(newScenData);
					loaded++;
				} else {
					MessageBox.Show("Could not load scenario " + @scenFile + Environment.NewLine + 
						"Scenario not found or not properly formatted.", "Scenario can't be loaded.");
				}
			}

			return loaded;
		}

		/// <summary>
		/// Load scenario by its index.
		/// </summary>
		/// <param name="id">Scenario index.</param>
		public void LoadScenario(int idx)
		{
			Debug.Assert(idx < scenariosData.Count, "Invalid scenario index: " + idx);

			LoadedScenario = new Scenario(scenariosData[idx]);
		}

		/// <summary>
		/// Reload the current scenario.
		/// </summary>
		public void ReloadScenario()
		{
			LoadedScenario.Reload();
		}
	}
}
