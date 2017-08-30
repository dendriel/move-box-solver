using System;
using System.Collections.Generic;
using System.Drawing;

namespace Move_Box_Solver.Source.Game
{
	/// <summary>
	/// An instance of a scenario. Handles dynamic scenario data.
	/// </summary>
	class Scenario
	{
		/// <summary>
		/// Static scenario data.
		/// </summary>
		ScenarioData data;

		/// <summary>
		/// Scenario name.
		/// </summary>
		public string Name { get { return data.Name; } }

		/// <summary>
		/// Scenario information.
		/// </summary>
		public string Info { get { return data.Info; } }

		/// <summary>
		/// List of all in-game floors.
		/// </summary>
		public List<StaticElement> FloorList { get; private set; }
		
		/// <summary>
		/// List of all in-game boxes.
		/// </summary>
		public List<Box> BoxList { get; private set; }

		/// <summary>
		/// Player reference.
		/// </summary>
		public DynamicElement Player { get; private set; }

		/// <summary>
		/// Map matrix.
		/// </summary>
		public ScenarioData.MapElements[,] Map;

		/// <summary>
		/// Load a new scenario.
		/// </summary>
		/// <param name="_data">The scenario static data</param>
		public Scenario(ScenarioData _data)
		{
			data = _data;

			Reload();
		}

		/// <summary>
		/// Reload scenario.
		/// </summary>
		public void Reload()
		{
			LoadElements(data.MapMatrix);
		}

		/// <summary>
		/// Load scenario elements.
		/// </summary>
		public void LoadElements(ScenarioData.MapElements[,] mapState)
		{
			FloorList = new List<StaticElement>();
			BoxList = new List<Box>();

			Map = mapState;

			// Clear past graphics on the screen.
			Form1.ResetGraphics();

			// Parse the element from the current position.
			for (int row = 0; row < mapState.GetLength(1); row++) {
				for (int col = 0; col < mapState.GetLength(0); col++) {					
					ParseMapElement(new Point(col, row), Map[col, row]);
				}
			}
		}

		/// <summary>
		/// Parse a map element.
		/// </summary>
		/// <param name="position">Element map position.</param>
		/// <param name="id">Element type.</param>
		void ParseMapElement(Point position, ScenarioData.MapElements type)
		{
			switch (type) {
				case ScenarioData.MapElements.BOX: {
						FloorList.Add(new Floor(position));
						Box newBox = new Box(position);
						newBox.BringSpriteToFront();
						BoxList.Add(newBox);
					}
					break;

				case ScenarioData.MapElements.BOX_OVER_TARGET: {
						FloorList.Add(new Target(position));
						Box newBox = new Box(position);
						newBox.BringSpriteToFront();
						newBox.IsReady = true;
						BoxList.Add(newBox);
					}
					break;

				case ScenarioData.MapElements.PLAYER: {
						FloorList.Add(new Floor(position));
						Player = new Player(position);
						Player.BringSpriteToFront();
					}
					break;

				case ScenarioData.MapElements.PLAYER_OVER_TARGET: {
						FloorList.Add(new Target(position));
						Player = new Player(position);
						Player.BringSpriteToFront();
					}
					break;
				case ScenarioData.MapElements.FLOOR:
					FloorList.Add(new Floor(position));
					break;
				case ScenarioData.MapElements.WALL:
					new Wall(position);
					break;
				case ScenarioData.MapElements.TARGET:
					new Target(position);
					break;
				case ScenarioData.MapElements.EMPTY:
					// skip.
					break;
				default:
					throw new Exception("Received unhandled map element type: " + type);
			}
		}
	}
}
