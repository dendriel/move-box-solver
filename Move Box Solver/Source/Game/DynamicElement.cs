using System.Drawing;

namespace Move_Box_Solver.Source.Game
{
	/// <summary>
	/// Dynamic (moveable) elements in the scenario.
	/// </summary>
	class DynamicElement : ScenarioElement
	{
		/// <summary>
		/// Movement counter. Amount of movements in the scenario.
		/// </summary>
		public int MoveCount { get; private set; } = 0;

		/// <summary>
		/// Create a new dynamic element.
		/// </summary>
		/// <param name="image">Element appearance (image).</param>
		/// <param name="position">Element starting position.</param>
		/// <param name="blockeable">This element blocks the path?</param>
		public DynamicElement(Bitmap image, Point position, bool blockeable = true) : base(image, position)
		{
			Moveable = true;
			Blockeable = blockeable;
		}

		/// <summary>
		/// Move this element towards the given direction.
		/// </summary>
		/// <param name="dir">The direction to move.</param>
		public void Move(Directions dir)
		{
			if (dir == Directions.UP) {
				Position = new Point(Position.X, Position.Y - 1);

			} else if (dir == Directions.RIGHT) {
				Position = new Point(Position.X + 1, Position.Y);

			} else if (dir == Directions.DOWN) {
				Position = new Point(Position.X, Position.Y + 1);

				// Directions.LEFT
			} else {
				Position = new Point(Position.X - 1, Position.Y);
			}

			MoveCount++;
		}

		/// <summary>
		/// Bring the sprite to the front (so it won't be hidden by other sprites).
		/// 
		//  *Updating Position property from ScenarioElement triggers BringToFront().
		/// </summary>
		public void BringSpriteToFront()
		{
			Position = Position;
		}
	}
}
