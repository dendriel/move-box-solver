
using System.Drawing;

namespace Move_Box_Solver.Source.Game
{
	/// <summary>
	/// Static elements in the scenario.
	/// </summary>
	class StaticElement : ScenarioElement
	{
		/// <summary>
		/// Static elements types.
		/// </summary>
		public enum StaticElementType
		{
			FLOOR,
			WALL,
			TARGET
		}

		/// <summary>
		/// Create a new static element.
		/// </summary>
		/// <param name="image">Element appearance (image).</param>
		/// <param name="position">Element starting position.</param>
		/// <param name="blockeable">The element may block the path?</param>
		public StaticElement(Bitmap image, Point position, bool blockeable) : base(image, position)
		{
			Moveable = false;
			Blockeable = blockeable;
		}
	}
}
