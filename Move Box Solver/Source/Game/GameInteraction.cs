using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Move_Box_Solver.Source.Game
{
	/// <summary>
	/// Holds common game interactions that update the map state.
	/// </summary>
	static class GameInteraction
	{
		/// <summary>
		/// Check the victory condition (all boxes ready).
		/// </summary>
		/// <returns>true if the game as won; false otherwise.</returns>
		public static bool CheckVictory(ScenarioData.MapElements[,] mapState)
		{
			// Check if there is any box that is not ready.
			for (int row = 0; row < mapState.GetLength(1); row++) {
				for (int col = 0; col < mapState.GetLength(0); col++) {
					if (mapState[col, row] == ScenarioData.MapElements.BOX) {
						return false;
					}
				}
			}

			return true;
		}

		/// <summary>
		/// Move the player towards the given direction.
		/// </summary>
		/// <param name="dir">Direction to move.</param>
		/// <param name="mapState">Current map state in which the player will be moved.</param>
		/// <returns>true if moved; false if the path is blocked.</returns>
		public static bool MovePlayer(Directions dir, ScenarioData.MapElements[,] mapState)
		{
			Point currPos = FindPlayerPosition(mapState);

			// Find next player pos.
			Point nextPos = FindNextPosition(currPos, dir);
			if (IsPositionMoveable(nextPos, mapState) != true) {

				ScenarioData.MapElements nextPosElem = mapState[nextPos.X, nextPos.Y];
				// Check if there is a box in the next position.
				if ((nextPosElem != ScenarioData.MapElements.BOX) && (nextPosElem != ScenarioData.MapElements.BOX_OVER_TARGET)) {
					// Next position is empty or has a wall.
					return false;
				}

				// Try to move the box to the next tile.
				if (MoveBox(nextPos, dir, mapState) != true) {
					return false;
				}
			}

			// Player can move.
			// Keep target floor if player is over a target.
			if (mapState[currPos.X, currPos.Y] == ScenarioData.MapElements.PLAYER_OVER_TARGET) {
				mapState[currPos.X, currPos.Y] = ScenarioData.MapElements.TARGET;
			} else {
				mapState[currPos.X, currPos.Y] = ScenarioData.MapElements.FLOOR;
			}

			// Update player.
			if (mapState[nextPos.X, nextPos.Y] == ScenarioData.MapElements.TARGET) {
				mapState[nextPos.X, nextPos.Y] = ScenarioData.MapElements.PLAYER_OVER_TARGET;
			} else {
				mapState[nextPos.X, nextPos.Y] = ScenarioData.MapElements.PLAYER;
			}

			return true;
		}

		/// <summary>
		/// Find the player position (PLAYER or PLAYER_OVER_TARGET).
		/// </summary>
		/// <param name="mapState">Current map state.</param>
		/// <returns>The player position.</returns>
		public static Point FindPlayerPosition(ScenarioData.MapElements[,] mapState)
		{
			Point currPos = FindElemPosition(ScenarioData.MapElements.PLAYER, mapState);
			// If could not found a PLAYER, the player is over a target.
			if (currPos.X == -1) {
				currPos = FindElemPosition(ScenarioData.MapElements.PLAYER_OVER_TARGET, mapState);
			}

			return currPos;
		}

		/// <summary>
		/// Try to move a box towards the given direction.
		/// </summary>
		/// <param name="boxPos">The inital box position.</param>
		/// <param name="dir">The direction to move.</param>
		/// <param name="mapState">Current map state.</param>
		/// <returns>true if the box could be moved; false otherwise.</returns>
		public static bool MoveBox(Point boxPos, Directions dir, ScenarioData.MapElements[,] mapState)
		{
			Point nextBoxPos = FindNextPosition(boxPos, dir);

			if (IsPositionMoveable(nextBoxPos, mapState) != true) {
				return false;
			}

			// Update previous box position.
			if (mapState[boxPos.X, boxPos.Y] == ScenarioData.MapElements.BOX_OVER_TARGET) {
				mapState[boxPos.X, boxPos.Y] = ScenarioData.MapElements.TARGET;
			} else {
				mapState[boxPos.X, boxPos.Y] = ScenarioData.MapElements.FLOOR;
			}

			// Set the box as ready if the new position is a target.
			if (mapState[nextBoxPos.X, nextBoxPos.Y] == ScenarioData.MapElements.TARGET) {
				mapState[nextBoxPos.X, nextBoxPos.Y] = ScenarioData.MapElements.BOX_OVER_TARGET;
			} else {
				mapState[nextBoxPos.X, nextBoxPos.Y] = ScenarioData.MapElements.BOX;
			}

			return true;
		}

		/// <summary>
		/// Find the next position after a movement.
		/// </summary>
		/// <param name="currPos">Current position.</param>
		/// <param name="dir">Movement direction.</param>
		/// <returns>New position.</returns>
		public static Point FindNextPosition(Point currPos, Directions dir)
		{
			if (dir == Directions.UP) {
				return new Point(currPos.X, currPos.Y - 1);

			} else if (dir == Directions.RIGHT) {
				return new Point(currPos.X + 1, currPos.Y);

			} else if (dir == Directions.DOWN) {
				return new Point(currPos.X, currPos.Y + 1);

				// LEFT.
			} else {
				return new Point(currPos.X - 1, currPos.Y);
			}
		}

		/// <summary>
		/// Check if the position is moveable.
		/// </summary>
		/// <param name="pos">The position to be checked.</param>
		/// <param name="mapState">Current map state.</param>
		/// <returns>true if the position is moveable; false otherwise.</returns>
		public static bool IsPositionMoveable(Point pos, ScenarioData.MapElements[,] mapState)
		{
			ScenarioData.MapElements elemInPos = mapState[pos.X, pos.Y];

			// Position is free to move onto?
			if ((elemInPos == ScenarioData.MapElements.FLOOR) ||
				(elemInPos == ScenarioData.MapElements.TARGET) ||
				(elemInPos == ScenarioData.MapElements.PLAYER) ||
				(elemInPos == ScenarioData.MapElements.PLAYER_OVER_TARGET)) {
				return true;

			} else {
				return false;
			}
		}

		/// <summary>
		/// Check if the position is NOT moveable.
		/// </summary>
		/// <param name="pos">The position to be checked.</param>
		/// <param name="mapState">Current map state.</param>
		/// <returns>true if the position is NOT moveable; false otherwise.</returns>
		public static bool IsPositionNotMoveable(Point pos, ScenarioData.MapElements[,] mapState)
		{
			return !IsPositionMoveable(pos, mapState);
		}

		/// <summary>
		/// Return the position of the first element of "type" found.
		/// </summary>
		/// <param name="type">Element's type</param>
		/// <param name="mapState">Map state to be searched.</param>
		/// <returns>The element's position if found; otherwise, returns Point(-1, -1).</returns>
		public static Point FindElemPosition(ScenarioData.MapElements type, ScenarioData.MapElements[,] mapState)
		{
			for (int row = 0; row < mapState.GetLength(1); row++) {
				for (int col = 0; col < mapState.GetLength(0); col++) {
					if (mapState[col, row] == type) {
						return new Point(col, row);
					}
				}
			}

			return new Point(-1, -1);
		}

		/// <summary>
		/// Find all elements positions of "type".
		/// </summary>
		/// <param name="type">Type of elements to be looked for.</param>
		/// <param name="mapState">Map state to be searched.</param>
		/// <returns>An array of all elements found.</returns>
		public static Point[] FindElemsPosition(ScenarioData.MapElements type, ScenarioData.MapElements[,] mapState)
		{
			List<Point> elemsFound = new List<Point>();

			// Convert unidimensional array into 2d array. (also, parse the element from the current position).
			for (int row = 0; row < mapState.GetLength(1); row++) {
				for (int col = 0; col < mapState.GetLength(0); col++) {
					if (mapState[col, row] == type) {
						elemsFound.Add(new Point(col, row));
					}
				}
			}

			return elemsFound.ToArray();
		}

		/// <summary>
		/// Check if the given element position is a wall.
		/// </summary>
		/// <param name="pos">The position to be checked.</param>
		/// <param name="mapState">The current map state.</param>
		/// <returns>true if the position is a wall; false otherwise.</returns>
		public static bool IsElementAWall(Point pos, ScenarioData.MapElements[,] mapState)
		{
			return (mapState[pos.X, pos.Y] == ScenarioData.MapElements.WALL);
		}

		/// <summary>
		/// Check if the given element position is a box (or a box over a target).
		/// </summary>
		/// <param name="pos">The position to be checked.</param>
		/// <param name="mapState">The current map state.</param>
		/// <returns>true if the position is a box; false otherwise.</returns>
		public static bool IsElementABox(Point pos, ScenarioData.MapElements[,] mapState)
		{
			return (mapState[pos.X, pos.Y] == ScenarioData.MapElements.BOX ||
					mapState[pos.X, pos.Y] == ScenarioData.MapElements.BOX_OVER_TARGET);
		}

		/// <summary>
		/// Check if the given element position is a wall.
		/// </summary>
		/// <param name="x">Column position to be checked.</param>
		/// <param name="y">Row position to be checked.</param>
		/// <param name="mapState">The current map state.</param>
		/// <returns>true if the position is a wall; false otherwise.</returns>
		public static bool IsElementAWall(int x, int y, ScenarioData.MapElements[,] mapState)
		{
			return IsElementAWall(new Point(x, y), mapState);
		}

		/// <summary>
		/// Check if the given element position is a target.
		/// </summary>
		/// <param name="pos">The position to be checked.</param>
		/// <param name="mapState">The current map state.</param>
		/// <returns>true if the position is a target; false otherwise.</returns>
		public static bool IsElementATarget(Point pos, ScenarioData.MapElements[,] mapState)
		{
			return (mapState[pos.X, pos.Y] == ScenarioData.MapElements.TARGET);
		}

		/// <summary>
		/// Check if the given element position is not a target.
		/// </summary>
		/// <param name="pos">The position to be checked.</param>
		/// <param name="mapState">The current map state.</param>
		/// <returns>true if the position is NOT a target; false otherwise.</returns>
		public static bool IsElementNotATarget(Point pos, ScenarioData.MapElements[,] mapState)
		{
			return !IsElementATarget(pos, mapState);
		}
		
		/// <summary>
		/// Debug. Prints the current map state.
		/// </summary>
		/// <param name="state">The map state to be printed.</param>
		public static void PrintMapState(ScenarioData.MapElements[,] state)
		{
			for (int row = 0; row < state.GetLength(1); row++) {
				for (int col = 0; col < state.GetLength(0); col++) {
					Console.Write(((int)state[col, row] + ", "));
				}
				Console.WriteLine();
			}
			Console.WriteLine();
		}

		/// <summary>
		/// Translate a key input into a game direction.
		/// </summary>
		/// <param name="key">The key to be translated.</param>
		/// <returns>A direction if the key is know; Direction.NONE if the key is not handled.</returns>
		public static Directions KeyToDirection(Keys key)
		{
			if (key == Keys.Up) {
				return Directions.UP;

			} else if (key == Keys.Right) {
				return Directions.RIGHT;

			} else if (key == Keys.Down) {
				return Directions.DOWN;

			} else if (key == Keys.Left) {
				return Directions.LEFT;

			} else {
				return Directions.NONE;
			}
		}
	}
}
