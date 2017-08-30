using CSGameUtils;
using Move_Box_Solver.Source.Game;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace Move_Box_Solver.Source.Solver
{
	/// <summary>
	/// Find a solution for a move box maze using A* algorithm.
	/// </summary>
	class MBMazeSolver : AStarSolver
	{
		/// <summary>
		/// Background worker reference to report progress and chack for pending cancelation.
		/// </summary>
		BackgroundWorker worker;

		/// <summary>
		/// Create a maze solver.
		/// </summary>
		/// <param name="startingNode">Starting node.</param>
		/// <param name="_IsSleepBetweenLoopsEnabled">Func to check if solver should sleep (100 ms) between loops. Return true to sleep between loops.</param>
		/// <param name="worker">BackgroundWorker to report progress and check for pending cancelation.</param>
		public MBMazeSolver(IAStarNode startingNode, Func<bool> IsSleepBetweenLoopsEnabled, BackgroundWorker _worker = null) :
			base(startingNode, IsSleepBetweenLoopsEnabled)
		{
			worker = _worker;
		}

		/// <summary>
		/// Find a solution for a maze.
		/// </summary>
		/// <returns>A list of directions for reaching the solution.</returns>
		public Directions[] FindDirections()
		{
			IAStarNode[] nodeSolution = FindSolution();

			List<Directions> directionSolution = new List<Directions>();

			foreach (MBMazeNode node in nodeSolution) {
				directionSolution.Add(node.MovedDir);
			}

			return directionSolution.ToArray();
		}

		/// <summary>
		/// Report progress by means of worker.
		/// </summary>
		protected override void ReportProgress()
		{
			if (worker != null) {
				worker.ReportProgress(0);
			}
		}

		/// <summary>
		/// Check if there is a cancelation pending from the worker.
		/// </summary>
		/// <returns>true if the solver must be interrupted; false otherwise.</returns>
		protected override bool IsCancelationPending()
		{
			if (worker != null) {
				return worker.CancellationPending;

			} else {
				return false;
			}
		}

		/// <summary>
		/// Check if the current node state is a solution state.
		/// </summary>
		/// <returns>true if found a solution; false otherwise.</returns>
		protected override bool HasFoundASolution()
		{
			ScenarioData.MapElements[,] currtate = (CurrNode as MBMazeNode).MazeState;
			return GameInteraction.CheckVictory(currtate);
		}		

		/// <summary>
		/// Find the valid (to be moved onto) neighbors from the current node.
		/// It won't return nodes that have the same cost and maze state from a node already in the list. (identical
		/// states may occur since we are not using "movement cost" heuristics).
		/// </summary>
		/// <returns>A list of valid neighbors from the current node.</returns>
		protected override IAStarNode[] FindNeighbors()
		{
			List<IAStarNode> neighbors = new List<IAStarNode>();
			
			IAStarNode upNode = FindAValidNeighbor(Directions.UP);
			if (upNode != null) {
				neighbors.Add(upNode);
			}

			IAStarNode rightNode = FindAValidNeighbor(Directions.RIGHT);
			if (rightNode != null) {
				neighbors.Add(rightNode);
			}

			IAStarNode downNode = FindAValidNeighbor(Directions.DOWN);
			if (downNode != null) {
				neighbors.Add(downNode);
			}

			IAStarNode leftNode = FindAValidNeighbor(Directions.LEFT);
			if (leftNode != null) {
				neighbors.Add(leftNode);
			}

			return neighbors.ToArray();
		}

		/// <summary>
		/// Find the next node for the given direction.
		/// </summary>
		/// <param name="node">Target node.</param>
		/// <param name="direction">Target direction.</param>
		/// <returns>The node if it is valid and isn't in any of the lists; false otherwise.</returns>
		private IAStarNode FindAValidNeighbor(Directions direction)
		{

			IAStarNode nextNode = CreateNode(direction);
			if ((nextNode != null) && IsNodeNotInList(nextNode, openList) && IsNodeNotInList(nextNode, closedList)) {
				return nextNode;
			} else {
				return null;
			}
		}

		/// <summary>
		/// Create a new node if the next position is moveable (if the player may move in there).
		/// </summary>
		/// <returns>A node for the new position if it is valid; null otherwise.</returns>
		private IAStarNode CreateNode(Directions dir)
		{
			IAStarNode prevNode = CurrNode;
			Point playerPos = GameInteraction.FindElemPosition(ScenarioData.MapElements.PLAYER, (prevNode as MBMazeNode).MazeState);

			ScenarioData.MapElements[,] newMazeState = CopyMazeStateFromNode(prevNode);
						
			if (GameInteraction.MovePlayer(dir, newMazeState)) {
				return new MBMazeNode(prevNode, dir, newMazeState);
			} else {
				return null;
			}						
		}

		/// <summary>
		/// Copy the maze state from the given node.
		/// </summary>
		/// <param name="node">The node which the maze will be copied.</param>
		/// <returns>a copy of the maze state from the given node.</returns>
		static ScenarioData.MapElements[,] CopyMazeStateFromNode(IAStarNode node)
		{
			return (node as MBMazeNode).MazeState.Clone() as ScenarioData.MapElements[,];
		}

		/// <summary>
		/// Check if the node is inside the list. Use for(;;) due to performance issues.
		/// 
		/// Use for(;;) due to performance issues. It may be at least 2x times "cheaper" than
		/// foreach loops (https://stackoverflow.com/questions/365615/in-net-which-loop-runs-faster-for-or-foreach).
		/// </summary>
		/// <param name="node">Target node.</param>
		/// <param name="list">List ot check.</param>
		/// <returns>true if the target node is in the list; false otherwise.</returns>
		static bool IsNodeInList(IAStarNode node, List<IAStarNode> list)
		{
			for (int i = 0; i < list.Count; i++) {
				if (list[i].Equals(node)) {
					return true;
				}
			}

			return false;
		}

		/// <summary>
		/// Check if the node is not inside the list.
		/// </summary>
		/// <param name="node">Target node.</param>
		/// <param name="list">List ot check.</param>
		/// <returns>true if the target node is NOT in the list; false otherwise.</returns>
		static bool IsNodeNotInList(IAStarNode node, List<IAStarNode> list)
		{
			return !IsNodeInList(node, list);
		}

		/// <summary>
		/// Debug. Print the solution (backwards, as-is).
		/// </summary>
		/// <param name="lastNode"></param>
		static void PrintSolution(IAStarNode lastNode)
		{
			int i = 0;

			IAStarNode prevNode = lastNode;
			while (prevNode != null) {
				Console.WriteLine("i: " + i + " dir: " + (prevNode as MBMazeNode).MovedDir);
				i++;
				prevNode = prevNode.PrevNode;
			}
		}
	}
}
