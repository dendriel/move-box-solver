using Move_Box_Solver.Properties;
using System.Drawing;

namespace Move_Box_Solver.Source.Game
{
	/// <summary>
	/// Floor handler.
	/// </summary>
	class Floor : StaticElement
	{
		/// <summary>
		/// Create a new floor.
		/// </summary>
		/// <param name="position">Floor position.</param>
		public Floor(Point position) : base(Resources.floor, position, false)
		{}
	}
}
