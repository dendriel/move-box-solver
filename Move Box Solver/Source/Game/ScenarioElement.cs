using System.Drawing;
using System.Windows.Forms;

namespace Move_Box_Solver.Source.Game
{
	/// <summary>
	/// Scenario element default properties.
	/// </summary>
	public class ScenarioElement
	{
		/// <summary>
		/// Element position inside scenario.
		/// </summary>
		Point _position;
		public Point Position {
			get { return _position; }
			set {
				_position = value;
				int xPos = (_position.X * ScenarioManager.SPRITE_WIDTH) + FormDefs.HOR_ALIGN_OFFSET;
				int yPos = (_position.Y * ScenarioManager.SPRITE_HEIGHT) + FormDefs.VER_ALIGN_OFFSET;
				sprite.Location = new Point(xPos, yPos);
				sprite.BringToFront();
			}
		}

		/// <summary>
		/// Element may be moved?
		/// </summary>
		public bool Moveable { get; protected set; }

		/// <summary>
		/// Element may block the path?
		/// </summary>
		public bool Blockeable { get; protected set; }

		/// <summary>
		/// Element sprite in the scenario.
		/// </summary>
		PictureBox sprite { get; set; }

		/// <summary>
		/// Element image (set only).
		/// </summary>
		public Bitmap Sprite { set { sprite.Image = value; } }

		/// <summary>
		/// Create a new scenario element.
		/// </summary>
		/// <param name="image">Element appearance (image).</param>
		/// <param name="position">Element starting position.</param>
		public ScenarioElement(Bitmap image, Point position)
		{
			sprite = new PictureBox() {
				Image = image,
				Width = ScenarioManager.SPRITE_WIDTH,
				Height = ScenarioManager.SPRITE_HEIGHT };

			Position = position;

			Form1.Ref.SetupPictureBox(sprite);
		}
	}
}
