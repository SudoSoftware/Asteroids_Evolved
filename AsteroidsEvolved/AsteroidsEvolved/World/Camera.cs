using Microsoft.Xna.Framework;

namespace AsteroidsEvolved
{
	class Camera
	{
		private Vector3 eyePos = new Vector3(0, 0, -1), upVector = Vector3.Up;
		private Matrix view, projection;


		public Camera(GraphicsDeviceManager graphics)
		{
			view = Matrix.CreateLookAt(eyePos, Vector3.Zero, upVector);

			//projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(90.0f), graphics.GraphicsDevice.Viewport.AspectRatio, 0.01f, 100.0f);
			projection = Matrix.CreateOrthographic(GameParameters.WORLD_BOUNDS.Width, GameParameters.WORLD_BOUNDS.Height, 0.01f, 100.0f);
		}



		public Matrix getView()
		{
			return view;
		}



		public Matrix getProjection()
		{
			return projection;
		}
	}
}
