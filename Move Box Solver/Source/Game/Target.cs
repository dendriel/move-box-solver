using Move_Box_Solver.Properties;
using System.Drawing;

namespace Move_Box_Solver.Source.Game
{
	/// <summary>
	/// Target handler.
	/// </summary>
	class Target : StaticElement
	{
		/// <summary>
		/// Create a new target.
		/// </summary>
		/// <param name="position">Target position.</param>
		public Target(Point position) : base(Resources.target, position, false)
		{ }
	}
}
