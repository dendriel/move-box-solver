using Newtonsoft.Json.Linq;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Move_Box_Solver
{
	/// <summary>
	/// Loasd (from a file) and holds static scenario data.
	/// </summary>
	public class ScenarioData
	{
		/// <summary>
		/// Identifiers from elements in the map.
		/// </summary>
		public enum MapElements
		{
			EMPTY = 0,
			FLOOR = 1,
			TARGET = 2,
			WALL = 3,
			PLAYER = 4,
			BOX = 5,
			BOX_OVER_TARGET = 6,
			PLAYER_OVER_TARGET = 7
		}

		/// <summary>
		/// Scenario name.
		/// </summary>
		public string Name { get; private set; }

		/// <summary>
		/// Scenario information.
		/// </summary>
		public string Info { get; private set; }

		/// <summary>
		/// Scenario width.
		/// </summary>
		public int Width { get; private set; }

		/// <summary>
		/// Scenario height.
		/// </summary>
		public int Height { get; private set; }
		
		/// <summary>
		/// Starting matrix from the map.
		/// </summary>
		MapElements[,] mapMatrix;
		public MapElements[,] MapMatrix { get { return mapMatrix.Clone() as MapElements[,]; } }

		/// <summary>
		/// Map could be loaded.
		/// </summary>
		public bool Loaded { get; set; }

		/// <summary>
		/// Load scenario data.
		/// </summary>
		/// <param name="metadata">Scenario metadata file content (JSON).</param>
		public ScenarioData(string metadata)
		{			
			try {
				LoadSelf(File.ReadAllText(metadata));
				Loaded = true;
			} catch (Exception e) {
				Console.WriteLine(e);
				Loaded = false;
			}
		}

		/// <summary>
		/// Load map data.
		/// </summary>
		/// <param name="metadata">Scenario metadata file content (JSON).</param>
		void LoadSelf(string metadata)
		{
			dynamic data = JObject.Parse(metadata);

			// Parse elements.
			Name = (string) data.name;
			Info = (string)data.info;
			Width = (int)data.width;
						
			JArray mapArray = (JArray)data.map;
			Height = mapArray.Count / Width;

			mapMatrix = new MapElements[Width, Height];
			
			// Convert unidimensional array into 2d array. (also, parse the element from the current position).
			for (int row = 0; row < Height; row++) {
				for (int col = 0; col < Width; col++) {
					mapMatrix[col, row] = (MapElements)(int)mapArray[ (row * Width) + col];
				}
			}
		}
	}
}
