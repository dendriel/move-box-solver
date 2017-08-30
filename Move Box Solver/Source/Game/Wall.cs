using Move_Box_Solver.Properties;
using System.Drawing;

namespace Move_Box_Solver.Source.Game
{
	/// <summary>
	/// Wall handler.
	/// </summary>
	class Wall : StaticElement
	{
		/// <summary>
		/// Create a new wall.
		/// </summary>
		/// <param name="position">Wall position.</param>
		public Wall(Point position) : base(Resources.wall, position, false)
		{ }
	}
}
