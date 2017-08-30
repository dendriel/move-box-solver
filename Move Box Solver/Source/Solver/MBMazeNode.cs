using CSGameUtils;
using Move_Box_Solver.Source.Game;
using System;
using System.Drawing;

namespace Move_Box_Solver.Source.Solver
{
	/// <summary>
	/// Holds maze nodes data.
	/// </summary>
	class MBMazeNode : IAStarNode, IComparable<IAStarNode>
	{
		/// <summary>
		/// Heuristic values.
		/// </summary>
		enum HeuristicValus
		{
			CRITICAL_WEIGHT = 10000000,
			HIGH_AVOID_WEIGHT = 2000,
			LOW_AVOID_WEIGHT = 1000,
			BOX_CLOSE_TO_TARGET = 10,
			BOX_NOT_MOVED_BETWEEN_STATES = 20,
			BOX_DISTANCE_TARGET_STEP = 40,
			NODE_PATH_COST = 0,
		};

		/// <summary>
		/// Previous node.
		/// </summary>
		public IAStarNode PrevNode { get; private set; }

		/// <summary>
		/// Total cost to get in this node.
		/// </summary>
		public int TotalCost { get { return nodeStateWeight + nodePathWeight; } }

		/// <summary>
		/// Heuristic related to node state.
		/// </summary>
		int nodeStateWeight;

		/// <summary>
		/// Heuristic related to node path.
		/// </summary>
		int nodePathWeight;
		
		/// <summary>
		/// Direction moved from previous state to get in this state.
		/// </summary>
		public Directions MovedDir { get; private set; }

		/// <summary>
		/// Current maze state.
		/// </summary>
		public ScenarioData.MapElements[,] MazeState { get; private set; }

		/// <summary>
		/// Create a new maze node.
		/// </summary>
		/// <param name="prevNode">Previous node.</param>
		/// <param name="moveDir">Movement from previous node to get in this node.</param>
		/// <param name="mazeState">Current maze state.</param>
		public MBMazeNode(IAStarNode prevNode, Directions moveDir, ScenarioData.MapElements[,] mazeState)
		{
			PrevNode = prevNode;
			MovedDir = moveDir;
			MazeState = mazeState;

			nodeStateWeight = CalculateStateHeuristic();
			nodePathWeight = CalculatePathHeuristic();
		}

		public int CalculatePathHeuristic()
		{
			return ((PrevNode != null) ? (PrevNode as MBMazeNode).nodePathWeight : 0) + (int)HeuristicValus.NODE_PATH_COST;
		}
		
		public int CalculateStateHeuristic()
		{
			Point[] boxes = GameInteraction.FindElemsPosition(ScenarioData.MapElements.BOX, MazeState);
			Point[] targets = GameInteraction.FindElemsPosition(ScenarioData.MapElements.TARGET, MazeState);

			int totalCost = 0;

			for (int i = 0; i < boxes.Length; i++) {
				Point currBox = boxes[i];

				if (IsBoxInATrap(currBox, MazeState)) {
					totalCost += (int)HeuristicValus.CRITICAL_WEIGHT;
					
				} else if (TrapsChecker.IsBoxInAHall(currBox, MazeState)) {
					totalCost += (int)HeuristicValus.HIGH_AVOID_WEIGHT;
				
				} else if (IsBoxCloseToATarget(currBox, MazeState)) {
					totalCost += (int)HeuristicValus.BOX_CLOSE_TO_TARGET;

					// The box is somewhere else.
				} else {
					int closestDist = HowFarFromClosestTarget(currBox, targets);
					totalCost += (int)HeuristicValus.BOX_DISTANCE_TARGET_STEP * closestDist;
				}
			}

			// Check if there is a way to move a box into the targets.
			for (int i = 0; i < targets.Length; i++) {
				Point currTarget = targets[i];

				// Skip if the target is ready.
				if (GameInteraction.IsElementABox(currTarget, MazeState)) continue;

				if (IsThereAWayToTarget(currTarget, MazeState) != true) {
					totalCost += (int)HeuristicValus.HIGH_AVOID_WEIGHT;
				}
			}

			/* These heuristic doesn't have a positive effect.
			 * - Player closer to boxes as preferable state;
			 * - States with not moved boxes from previous states receives a high weight.
			 */

			return totalCost;
		}

		/// <summary>
		/// Check if for the current state the box is inside a trap (not moveable state).
		/// </summary>
		/// <param name="boxPos">The box position.</param>
		/// <param name="state">Current maze state.</param>
		/// <returns>true if the box is in a trap; false otherwise.</returns>
		bool IsBoxInATrap(Point boxPos, ScenarioData.MapElements[,] state)
		{
			if (TrapsChecker.IsBoxInWallTrap(boxPos, state)) {
				return true;

			} else if (TrapsChecker.IsBoxInSquareTrap(boxPos, state)) {
				return true;

			} else if (TrapsChecker.IsBoxInCornerTrap(boxPos, state)) {
				return true;

			} else if (TrapsChecker.IsBoxInSnakeTrap(boxPos, state)) {
				return true;

			} else if (TrapsChecker.IsBoxInHalfSquareTrap(boxPos, state)) {
				return true;

			} else if (TrapsChecker.IsBoxInRocketTrap(boxPos, state)) {
				return true;

			} else {
				return false;
			}
		}

		/// <summary>
		/// Check if the box is close to a target.
		///      o
		///   o BOX o
		///      o       (o = target)
		/// </summary>
		/// <param name="boxPos">The box position.</param>
		/// <param name="state">Current maze state.</param>
		/// <returns></returns>
		bool IsBoxCloseToATarget(Point boxPos, ScenarioData.MapElements[,] state)
		{
			Point upPos = GameInteraction.FindNextPosition(boxPos, Directions.UP);
			Point rightPos = GameInteraction.FindNextPosition(boxPos, Directions.RIGHT);
			Point downPos = GameInteraction.FindNextPosition(boxPos, Directions.DOWN);
			Point leftPos = GameInteraction.FindNextPosition(boxPos, Directions.LEFT);

				return (GameInteraction.IsElementATarget(upPos, state) ||
						GameInteraction.IsElementATarget(rightPos, state) ||
						GameInteraction.IsElementATarget(downPos, state) ||
						GameInteraction.IsElementATarget(leftPos, state));
		}

		/// <summary>
		/// Check how far a position is from other positions.
		/// </summary>
		/// <param name="pos">The position to check.</param>
		/// <param name="targets">The target positions.</param>
		/// <returns>The distance between pos and the closest position in targets.</returns>
		int HowFarFromClosestTarget(Point pos, Point[] targets)
		{
			int closestDist = int.MaxValue;

			// Check how far the pos is from the targets.
			for (int i = 0; i < targets.Length; i++) {

				Point currTarget = targets[i];
				int dist = Math.Abs(pos.X - currTarget.X) + Math.Abs(pos.Y - currTarget.Y);
				if (dist < closestDist) {
					closestDist = dist;
				}
			}

			return closestDist;
		}

		/// <summary>
		/// Check how many boxes dont moved in this state (comparable to previous state).
		/// </summary>
		/// <param name="boxes">Boxes to be checked.</param>
		/// <param name="prevState">Previous state to be checked.</param>
		/// <returns></returns>
		int HowManyBoxesWerentMoved(Point[] boxes, ScenarioData.MapElements[,] prevState)
		{
			int count = 0;
			foreach (Point currBox in boxes) {
				if (GameInteraction.IsElementABox(currBox, prevState)) {
					count++;
				}
			}

			return count;
		}

		/// <summary>
		/// Check if there is enough space to move a box into the target.
		/// </summary>
		/// <param name="target">Target position.</param>
		/// <param name="state">Current maze state.</param>
		/// <returns>true if there is enough space to move a box into the target; false otherwise.</returns>
		bool IsThereAWayToTarget(Point target, ScenarioData.MapElements[,] state)
		{
			Point upPos = GameInteraction.FindNextPosition(target, Directions.UP);
			Point rightPos = GameInteraction.FindNextPosition(target, Directions.RIGHT);
			Point downPos = GameInteraction.FindNextPosition(target, Directions.DOWN);
			Point leftPos = GameInteraction.FindNextPosition(target, Directions.LEFT);
			
			if (IsThereAWayToTargetCheck(upPos, new Point(0, -1), state)) {
				return true;

			} else if (IsThereAWayToTargetCheck(downPos, new Point(0, 1), state)) {
				return true;

			} else if (IsThereAWayToTargetCheck(leftPos, new Point(-1, 0), state)) {
				return true;

			} else if (IsThereAWayToTargetCheck(rightPos, new Point(1, 0), state)) {
				return true;

			} else {
				return false;
			}
		}

		/// <summary>
		/// Check if the given direction is free to move a box.
		/// </summary>
		/// <param name="dir">Close to target direction.</param>
		/// <param name="dirOffset">Direction offset. (will be added to dir to find the next position).</param>
		/// <param name="state">Current maze state.</param>
		/// <returns>true if the next 2 tiles are free; false otherwise.</returns>
		bool IsThereAWayToTargetCheck(Point dir, Point dirOffset, ScenarioData.MapElements[,] state)
		{
			Point nextDir = new Point(dir.X + dirOffset.X, dir.Y + dirOffset.Y);

			return GameInteraction.IsPositionMoveable(dir, state) &&
				   GameInteraction.IsPositionMoveable(nextDir, state);
		}

		public override bool Equals(object obj)
		{
			IAStarNode node = obj as IAStarNode;
			if (node == null) {
				return false;
			}

			/* Doesn't check the previous node. We just want to know if this node state
			 * was already found disregarding previous states.
			 */

			if (node.TotalCost != TotalCost) {
				return false;
			}

			// Compare arrays.
			for (int row = 0; row < MazeState.GetLength(1); row++) {
				for (int col = 0; col < MazeState.GetLength(0); col++) {
					if (MazeState[col, row] != (node as MBMazeNode).MazeState[col, row])
						return false;
				}
			}

			return true;
		}

		public int CompareTo(IAStarNode other)
		{
			if (TotalCost < other.TotalCost) {
				return -1;

			} else if (TotalCost > other.TotalCost) {
				return 1;

			} else {
				return 0;
			}
		}

		public override string ToString()
		{
			return "Node cost: " + TotalCost;
		}
	}
}
