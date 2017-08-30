using Move_Box_Solver.Properties;
using System.Drawing;

namespace Move_Box_Solver.Source.Game
{
	class Player : DynamicElement
	{
		/// <summary>
		/// Create a new player.
		/// </summary>
		/// <param name="startingPosition">Player's starting position.</param>
		public Player(Point startingPosition) : base(Resources.player, startingPosition)
		{}
	}
}
