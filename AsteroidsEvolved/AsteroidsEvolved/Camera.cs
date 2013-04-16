using Microsoft.Xna.Framework;

namespace AsteroidsEvolved
{
	class Camera
	{
		private Vector3 m_eye = new Vector3(0, 0, -3);
		private Vector3 m_up = Vector3.Up;
		private Matrix m_mView;
		private Matrix m_mProjection;

		public Camera(GraphicsDeviceManager graphics)
		{
			m_mView = Matrix.CreateLookAt(m_eye, Vector3.Zero, m_up);
			m_mProjection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(90.0f), graphics.GraphicsDevice.Viewport.AspectRatio, 0.01f, 100.0f);
		}

		public Matrix getView()
		{
			return m_mView;
		}

		public Matrix getProjection()
		{
			return m_mProjection;
		}
	}
}
