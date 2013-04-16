using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsteroidsEvolved
{
	abstract class WorldModel
	{
		protected Vector3 position = new Vector3(0, 0, 0);
		protected Vector3 rotation = new Vector3(0, 0, 0);

		protected Model model;

		protected Matrix worldMatrix;
		protected Matrix modelScale;


		public WorldModel(Model model):
			this(model, 0, 0, 0)
		{}


		public WorldModel(Model model, float x, float y, float z)
		{
			this.model = model;
			position = new Vector3(x, y, z);
			modelScale = Matrix.CreateScale(1.0f / model.Meshes[0].BoundingSphere.Radius);
		}



		public abstract void update(GameTime gameTime);



		public void setModel(Model newModel)
		{
			model = newModel;
		}



		public Model getModel()
		{
			return model;
		}



		public Matrix getRotationMatrix()
		{
			return Matrix.CreateRotationX(rotation.X) * Matrix.CreateRotationY(rotation.Y) * Matrix.CreateRotationZ(rotation.Z);
		}



		public Matrix getWorldMatrix()
		{
			return worldMatrix;
		}
	}
}
