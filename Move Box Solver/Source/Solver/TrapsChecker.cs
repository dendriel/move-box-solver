using Move_Box_Solver.Source.Game;
using System.Drawing;

namespace Move_Box_Solver.Source.Solver
{
	/// <summary>
	/// Checks for move box traps in a grid map.
	/// </summary>
	public static class TrapsChecker
	{       /// <summary>
			/// Checks if the box is in a wall. (no targets and no way to the box from there).
			/// 
			/// Wall trap:
			///  __  __  __  __  __
			/// |__||__||__||__||__|
			/// |__|     BOX    |__| (the wall may have any lenght)
			/// 
			/// </summary>
			/// <param name="boxPos">The box to be checked.</param>
			/// <param name="state">The map state to be checked.</param>
			/// <returns>true if the box in is an wall trap; false otherwise.</returns>
		public static bool IsBoxInWallTrap(Point boxPos, ScenarioData.MapElements[,] state)
		{
			Point upPos = GameInteraction.FindNextPosition(boxPos, Directions.UP);
			Point rightPos = GameInteraction.FindNextPosition(boxPos, Directions.RIGHT);
			Point downPos = GameInteraction.FindNextPosition(boxPos, Directions.DOWN);
			Point leftPos = GameInteraction.FindNextPosition(boxPos, Directions.LEFT);

			// Check wall up.
			if (IsBoxInWallTrapCheck(upPos, leftPos, rightPos, new Point(-1, 0), new Point(1, 0), state)) {
				return true;

				// Check wall down.
			} else if (IsBoxInWallTrapCheck(downPos, leftPos, rightPos, new Point(-1, 0), new Point(1, 0), state)) {
				return true;

				// Check wall left.
			} else if (IsBoxInWallTrapCheck(leftPos, upPos, downPos, new Point(0, -1), new Point(0, 1), state)) {
				return true;

				// Check wall right.
			} else if (IsBoxInWallTrapCheck(rightPos, upPos, downPos, new Point(0, -1), new Point(0, 1), state)) {
				return true;

			} else {
				return false;
			}
		}

		/// <summary>
		/// Checks if the box is in a wall trap.
		/// </summary>
		/// <param name="wallPos">Wall position (up, down, left, right)</param>
		/// <param name="sideA">Side A. (up, down, left, right)</param>
		/// <param name="sideB">Side B. (up, down, left, right)</param>
		/// <param name="offsetA">A point that moves towards Side A.</param>
		/// <param name="offsetB">A point that moves towars Side B.</param>
		/// <param name="state">Current maze state.</param>
		/// <returns>true if the box is in an wall trap; false otherwise.</returns>
		static bool IsBoxInWallTrapCheck(Point wallPos, Point sideA, Point sideB, Point offsetA, Point offsetB, ScenarioData.MapElements[,] state)
		{
			if (GameInteraction.IsElementAWall(wallPos, state) &&
				IsBoxInWallTrapCheckSide(sideA, wallPos, offsetA, state) &&
				IsBoxInWallTrapCheckSide(sideB, wallPos, offsetB, state)) {
				return true;
			} else {
				return false;
			}
		}

		/// <summary>
		/// Check one side of the box in wall trap.
		/// </summary>
		/// <param name="sidePos">Side to test. (leftPos, downPos, upPos, rightPos).</param>
		/// <param name="wallPos">Wall position. (leftPos, downPos, upPos, rightPos).</param>
		/// <param name="inc">Increment determines the next position to test.</param>
		/// <param name="state">Current map state.</param>
		/// <returns>true if the side is part of a wall trap; false otherwise.</returns>
		static bool IsBoxInWallTrapCheckSide(Point sidePos, Point wallPos, Point inc, ScenarioData.MapElements[,] state)
		{
			Point currSidePos = sidePos;
			Point currWallSidePos = new Point(wallPos.X + inc.X, wallPos.Y + inc.Y);
			while (true) {

				// Checks if the wall keeps going to the sides.
				if (GameInteraction.IsElementAWall(currWallSidePos, state) == false) {
					return false;

					// If there is a target close to the box. The box may be in the right place.
				} else if (GameInteraction.IsElementATarget(currSidePos, state)) {
					return false;

					// check if it is the last part of the wall.
				} else if (GameInteraction.IsElementAWall(currSidePos, state)) {
					break;
				}

				currSidePos = new Point(currSidePos.X + inc.X, currSidePos.Y + inc.Y);
				currWallSidePos = new Point(currWallSidePos.X + inc.X, currWallSidePos.Y + inc.Y);
			}

			return true;
		}
		/// <summary>
		/// Checks if the box in the square trap.
		///      __
		///  BOX|__|  (may be a square of boxes)
		///  |__|__|
		/// </summary>
		/// <param name="boxPos">Box position.</param>
		/// <param name="state">Maze state.</param>
		/// <returns></returns>
		public static bool IsBoxInSquareTrap(Point boxPos, ScenarioData.MapElements[,] state)
		{
			Point upPos = GameInteraction.FindNextPosition(boxPos, Directions.UP);
			Point rightPos = GameInteraction.FindNextPosition(boxPos, Directions.RIGHT);
			Point downPos = GameInteraction.FindNextPosition(boxPos, Directions.DOWN);
			Point leftPos = GameInteraction.FindNextPosition(boxPos, Directions.LEFT);

			if (IsBoxInSquareTrapCheck(rightPos, downPos, new Point(downPos.X + 1, downPos.Y), state)) {
				return true;

			} else if (IsBoxInSquareTrapCheck(leftPos, downPos, new Point(downPos.X - 1, downPos.Y), state)) {
				return true;

			} else if (IsBoxInSquareTrapCheck(rightPos, upPos, new Point(upPos.X + 1, upPos.Y), state)) {
				return true;

			} else if (IsBoxInSquareTrapCheck(leftPos, upPos, new Point(upPos.X - 1, upPos.Y), state)) {
				return true;

			} else {
				return false;
			}
		}

		/// <summary>
		/// Check if the given sides aren't moveable.
		/// </summary>
		/// <param name="sideA">Side A (direct side: up, down, left, right).</param>
		/// <param name="sideB">Side B (direct side: up, down, left, right).</param>
		/// <param name="sideC">Side C (side with offset. Diagonally position).</param>
		/// <param name="state">Current maze state.</param>
		/// <returns>true if the box is in a square, false otherwise.</returns>
		static bool IsBoxInSquareTrapCheck(Point sideA, Point sideB, Point sideC, ScenarioData.MapElements[,] state)
		{
			return (GameInteraction.IsPositionNotMoveable(sideA, state) &&
					GameInteraction.IsPositionNotMoveable(sideB, state) &&
					GameInteraction.IsPositionNotMoveable(sideC, state));
		}

		/// <summary>
		/// Check if the box is in a corner trap.
		/// Corner trap may be:
		///      __     __
		///  __ |__|   |__|__
		/// |__|BOX    BOX|__| (and up side down).
		/// </summary>
		/// <param name="boxPos">The box position.</param>
		/// <param name="state">Current maze state.</param>
		/// <returns>true if the box is trapped in a corner; false otherwise.</returns>
		public static bool IsBoxInCornerTrap(Point boxPos, ScenarioData.MapElements[,] state)
		{
			Point upPos = GameInteraction.FindNextPosition(boxPos, Directions.UP);
			Point rightPos = GameInteraction.FindNextPosition(boxPos, Directions.RIGHT);
			Point downPos = GameInteraction.FindNextPosition(boxPos, Directions.DOWN);
			Point leftPos = GameInteraction.FindNextPosition(boxPos, Directions.LEFT);

			if (IsBoxInCornerTrapCheck(upPos, leftPos, state)) {
				return true;

			} else if (IsBoxInCornerTrapCheck(upPos, rightPos, state)) {
				return true;

			} else if (IsBoxInCornerTrapCheck(downPos, leftPos, state)) {
				return true;

			} else if (IsBoxInCornerTrapCheck(downPos, rightPos, state)) {
				return true;

			} else {
				return false;
			}
		}

		/// <summary>
		/// Check if the box is in a specific square trap.
		/// </summary>
		/// <param name="sideA">Side A (direct side: up, down, left, right).</param>
		/// <param name="sideB">Side B (direct side: up, down, left, right).</param>
		/// <param name="state">Current maze state.</param>
		/// <returns>true if the box is in an square trap; false otherwise.</returns>
		static bool IsBoxInCornerTrapCheck(Point sideA, Point sideB, ScenarioData.MapElements[,] state)
		{
			return (GameInteraction.IsElementAWall(sideA, state) && GameInteraction.IsElementAWall(sideB, state));
		}

		/// <summary>
		/// Check if the box is in a snake trap.
		///   Snake (S) case:
		///       __
		///		 |__|
		///	 BOX BOX
		///	|__|
		/// </summary>
		/// <param name="boxPos">The box position.</param>
		/// <param name="state">Current maze state.</param>
		/// <returns></returns>
		public static bool IsBoxInSnakeTrap(Point boxPos, ScenarioData.MapElements[,] state)
		{
			Point upPos = GameInteraction.FindNextPosition(boxPos, Directions.UP);
			Point rightPos = GameInteraction.FindNextPosition(boxPos, Directions.RIGHT);
			Point downPos = GameInteraction.FindNextPosition(boxPos, Directions.DOWN);
			Point leftPos = GameInteraction.FindNextPosition(boxPos, Directions.LEFT);

			if (IsBoxInSnakeTrapCheck(upPos, rightPos, new Point(upPos.X - 1, upPos.Y), state)) {
				return true;

			} else if (IsBoxInSnakeTrapCheck(rightPos, downPos, new Point(upPos.X + 1, upPos.Y), state)) {
				return true;

			} else if (IsBoxInSnakeTrapCheck(downPos, leftPos, new Point(downPos.X + 1, downPos.Y), state)) {
				return true;

			} else if (IsBoxInSnakeTrapCheck(leftPos, upPos, new Point(downPos.X - 1, downPos.Y), state)) {
				return true;

			// Check reversed.
			} else if (IsBoxInSnakeTrapCheck(upPos, leftPos, new Point(upPos.X + 1, upPos.Y), state)) {
				return true;

			} else if (IsBoxInSnakeTrapCheck(rightPos, upPos, new Point(downPos.X + 1, downPos.Y), state)) {
				return true;

			} else if (IsBoxInSnakeTrapCheck(downPos, rightPos, new Point(downPos.X - 1, downPos.Y), state)) {
				return true;

			} else if (IsBoxInSnakeTrapCheck(leftPos, downPos, new Point(upPos.X - 1, upPos.Y), state)) {
				return true;

			} else {
				return false;
			}
		}

		/// <summary>
		/// Check if the box is in a snake trap.
		/// </summary>
		/// <param name="notMoveablePos">A close position that may be a box or a wall.</param>
		/// <param name="wallPosA">A pos that must be a wall.</param>
		/// <param name="wallPosB">A pos that must be a wall.</param>
		/// <param name="state">Current maze state.</param>
		/// <returns>true if the box is in a snake trap; false otherwise.</returns>
		static bool IsBoxInSnakeTrapCheck(Point notMoveablePos, Point wallPosA, Point wallPosB, ScenarioData.MapElements[,] state)
		{
			return (GameInteraction.IsPositionNotMoveable(notMoveablePos, state) &&
					GameInteraction.IsElementAWall(wallPosA, state) &&
					GameInteraction.IsElementAWall(wallPosB, state));
		}

		/// <summary>
		/// Check if the box is in a hall.
		/// A hall may be:
		///   __     __         __
		///  |__|   |__|BOX BOX|__|
		///   BOX 
		///   BOX
		///  |__|  (and up side down).
		/// 
		/// </summary>
		/// <param name="boxPos">The box position.</param>
		/// <param name="state">Current maze state.</param>
		/// <returns>true if the box is in a hall; false otherwise.</returns>
		public static bool IsBoxInAHall(Point boxPos, ScenarioData.MapElements[,] state)
		{
			Point upPos = GameInteraction.FindNextPosition(boxPos, Directions.UP);
			Point rightPos = GameInteraction.FindNextPosition(boxPos, Directions.RIGHT);
			Point downPos = GameInteraction.FindNextPosition(boxPos, Directions.DOWN);
			Point leftPos = GameInteraction.FindNextPosition(boxPos, Directions.LEFT);

			if (IsBoxInAHallCheck(upPos, downPos, new Point(downPos.X, downPos.Y + 1), state)) {
				return true;

			} else if (IsBoxInAHallCheck(leftPos, rightPos, new Point(rightPos.X + 1, rightPos.Y), state)) {
				return true;

			} else if (IsBoxInAHallCheck(downPos, upPos, new Point(upPos.X, upPos.Y - 1), state)) {
				return true;

			} else if (IsBoxInAHallCheck(rightPos, leftPos, new Point(leftPos.X - 1, leftPos.Y), state)) {
				return true;

			} else {
				return false;
			}
		}

		/// <summary>
		/// Check if the box is in a hall.
		/// </summary>
		/// <param name="wallA">Wall closest to the box.</param>
		/// <param name="notMoveablePos">A close position that may be a box or a wall.</param>
		/// <param name="wallB">Wall closest to the moveable position.</param>
		/// <param name="state">Current maze state.</param>
		/// <returns>true if the box is in a hall; false otherwise.</returns>
		static bool IsBoxInAHallCheck(Point wallA, Point notMoveablePos, Point wallB, ScenarioData.MapElements[,] state)
		{
			return (GameInteraction.IsElementAWall(wallA, state) &&
					GameInteraction.IsElementABox(notMoveablePos, state) &&
					GameInteraction.IsElementAWall(wallB, state));
		}

		/// <summary>
		/// Check if the box is in a half square trap.
		/// Half square trap:
		///  __  __
		/// |__||__| __      BOX BOX
		///  BOX    |__| or  BOX     BOX
		///     BOX |__|         BOX BOX
		/// 
		/// </summary>
		/// <param name="boxPos">The box position.</param>
		/// <param name="state">Current maze state.</param>
		/// <returns></returns>
		public static bool IsBoxInHalfSquareTrap(Point boxPos, ScenarioData.MapElements[,] state)
		{
			Point upPos = GameInteraction.FindNextPosition(boxPos, Directions.UP);
			Point rightPos = GameInteraction.FindNextPosition(boxPos, Directions.RIGHT);
			Point downPos = GameInteraction.FindNextPosition(boxPos, Directions.DOWN);
			Point leftPos = GameInteraction.FindNextPosition(boxPos, Directions.LEFT);

			if (IsBoxInHalfSquareTrapCheck(leftPos, rightPos, upPos, new Point(0, -1), state)) {
				return true;

			} else if (IsBoxInHalfSquareTrapCheck(rightPos, leftPos, downPos, new Point(0, 1), state)) {
				return true;

			} else if (IsBoxInHalfSquareTrapCheck(upPos, downPos, rightPos, new Point(1, 0), state)) {
				return true;

			} else if (IsBoxInHalfSquareTrapCheck(downPos, upPos, leftPos, new Point(-1, 0), state)) {
				return true;

				// Check reversed.
			} else if (IsBoxInHalfSquareTrapCheck(rightPos, leftPos, upPos, new Point(0, -1), state)) {
				return true;

			} else if (IsBoxInHalfSquareTrapCheck(leftPos, rightPos, downPos, new Point(0, 1), state)) {
				return true;

			} else if (IsBoxInHalfSquareTrapCheck(downPos, upPos, rightPos, new Point(1, 0), state)) {
				return true;

			} else if (IsBoxInHalfSquareTrapCheck(upPos, downPos, leftPos, new Point(-1, 0), state)) {
				return true;

			} else {
				return false;
			}
		}

		/// <summary>
		/// Check if the given positions are a half square trap.
		///  __    __
		/// |__|  |__|  __
		///  BOX front |__|    -> front = up = offset (0, -1)
		/// sideA BOX |sideB|  -> front = down = offset (0, 1)
		///					   -> front = right = offset (1, 0)
		///					   -> front = left = offset (-1, 0)
		/// </summary>
		/// <param name="sideA">One side position of the box.</param>
		/// <param name="sideB">Inverse (to side A) side position.</param>
		/// <param name="front">Front position of the box relative to the trap.</param>
		/// <param name="offset">Indicates the orientation of the trap to be checked.</param>
		/// <param name="state">Current maze state.</param>
		/// <returns>true if the box is in a trap; false otherwise.</returns>
		static bool IsBoxInHalfSquareTrapCheck(Point sideA, Point sideB, Point front, Point offset, ScenarioData.MapElements[,] state)
		{
			Point doubleOffset = new Point(offset.X * 2, offset.Y * 2);

			return (GameInteraction.IsElementABox(new Point(sideA.X + offset.X, sideA.Y + offset.Y), state) &&
				GameInteraction.IsPositionNotMoveable(new Point(sideA.X + doubleOffset.X, sideA.Y + doubleOffset.Y), state) &&
				GameInteraction.IsPositionNotMoveable(sideB, state) &&
				GameInteraction.IsElementNotATarget(front, state) &&
				GameInteraction.IsPositionNotMoveable(new Point(front.X + offset.X, front.Y + offset.Y), state) &&
				GameInteraction.IsPositionNotMoveable(new Point(sideB.X + offset.X, sideB.Y + offset.Y), state));
		}

		/// <summary>
		/// Check if the box is in a rocket trap.
		/// Rocket trap:
		///   __
		///  |__|BOX __       BOX BOX __
		///  BOX __ |__|  or  BOX __ |__|
		///     |__|             |__|
		/// 
		/// </summary>
		/// <param name="boxPos">The box position.</param>
		/// <param name="state">Current maze state.</param>
		/// <returns>true if the box is in a trap; false otherwise.</returns>
		public static bool IsBoxInRocketTrap(Point boxPos, ScenarioData.MapElements[,] state)
		{
			Point upPos = GameInteraction.FindNextPosition(boxPos, Directions.UP);
			Point rightPos = GameInteraction.FindNextPosition(boxPos, Directions.RIGHT);
			Point downPos = GameInteraction.FindNextPosition(boxPos, Directions.DOWN);
			Point leftPos = GameInteraction.FindNextPosition(boxPos, Directions.LEFT);

			if (IsBoxInRocketTrapCheck(upPos, downPos, rightPos, new Point(1, 0), state)) {
				return true;

			} else if (IsBoxInRocketTrapCheck(rightPos, leftPos, downPos, new Point(0, 1), state)) {
				return true;

			} else if (IsBoxInRocketTrapCheck(downPos, upPos, leftPos, new Point(-1, 0), state)) {
				return true;

			} else if (IsBoxInRocketTrapCheck(leftPos, rightPos, upPos, new Point(0, -1), state)) {
				return true;

				// Check reversed.
			} else if (IsBoxInRocketTrapCheck(leftPos, rightPos, downPos, new Point(0, 1), state)) {
				return true;

			} else if (IsBoxInRocketTrapCheck(upPos, downPos, leftPos, new Point(-1, 0), state)) {
				return true;

			} else if (IsBoxInRocketTrapCheck(rightPos, leftPos, upPos, new Point(0, -1), state)) {
				return true;

			} else if (IsBoxInRocketTrapCheck(downPos, upPos, rightPos, new Point(1, 0), state)) {
				return true;

			} else {
				return false;
			}
		}

		/// <summary>
		/// Check if the given positions are a rocket trap.
		///   __
		///  |__|BOX __       |sideA| BOX  __
		///  BOX __ |__|  or   BOX  front |__|
		///     |__|          sideB    |__|   
		/// 
		/// </summary>
		/// <param name="sideA">One side position of the box.</param>
		/// <param name="sideB">Inverse (to side A) side position.</param>
		/// <param name="front">Front position of the box relative to the trap.</param>
		/// <param name="offset">Indicates the orientation of the trap to be checked.</param>
		/// <param name="state">Current maze state.</param>
		/// <returns>true if the box is in a trap; false otherwise.</returns>
		static bool IsBoxInRocketTrapCheck(Point sideA, Point sideB, Point front, Point offset, ScenarioData.MapElements[,] state)
		{
			return (GameInteraction.IsPositionNotMoveable(sideA, state) &&
				GameInteraction.IsElementNotATarget(front, state) &&
				GameInteraction.IsPositionNotMoveable(new Point(sideA.X + offset.X, sideA.Y + offset.Y), state) &&
				GameInteraction.IsElementAWall(new Point(front.X + offset.X, front.Y + offset.Y), state) &&
				GameInteraction.IsElementAWall(new Point(sideB.X + offset.X, sideB.Y + offset.Y), state));
		}

		/// <summary>
		/// Check if the elements are of the given type
		/// </summary>
		/// <param name="type">Expected element type.</param>
		/// <param name="state">Current maze state.</param>
		/// <param name="elements">Elements to be tested.</param>
		/// <returns></returns>
		static bool IsAllElementsOfType(ScenarioData.MapElements type, ScenarioData.MapElements[,] state, params Point[] elements)
		{
			for (int i = 0; i < elements.Length; i++) {
				Point currElem = elements[i];
				if (state[currElem.X, currElem.Y] != type) {
					return false;
				}
			}

			return true;
		}
	}
}
