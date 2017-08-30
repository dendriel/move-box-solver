using Move_Box_Solver.Properties;
using System.Drawing;

namespace Move_Box_Solver.Source.Game
{
	class Box : DynamicElement
	{
		/// <summary>
		/// Default box image.
		/// </summary>
		Bitmap defaultBoxImage = Resources.box;

		/// <summary>
		/// Ready box image.
		/// </summary>
		Bitmap readyBoxImage = Resources.ready_box;

		/// <summary>
		/// The box is over a target (is ready?)
		/// </summary>
		bool _isReady = false;
		public bool IsReady {
			get { return _isReady; }
			set {
				_isReady = value;
				Sprite = (value) ? readyBoxImage : defaultBoxImage;
			}
		}

		/// <summary>
		/// Create a new box.
		/// </summary>
		/// <param name="startingPosition">Box's starting position.</param>
		public Box(Point startingPosition) : base(Resources.box, startingPosition) {}
	}
}
