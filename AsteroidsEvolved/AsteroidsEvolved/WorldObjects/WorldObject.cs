using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;

namespace AsteroidsEvolved
{
	class WorldObject
	{
		protected Model model;
		protected Vector3 rotation;
		protected Matrix modelScale;

		protected List<Manifestation> manifests = new List<Manifestation>();


		public WorldObject(Model model) :
			this(model, new Vector3())
		{ }



		public WorldObject(Model model, Vector3 position)
		{
			this.model = model;

			Debug.Assert(model.Meshes.Count == 1);
			modelScale = Matrix.CreateScale(1.0f / model.Meshes[0].BoundingSphere.Radius);
			
			manifests.Add(new Manifestation(new Vector3(), this));
			manifests.Add(new Manifestation(new Vector3(0, -0.5f, 0), this));
			manifests.Add(new Manifestation(new Vector3(-0.5f, 0, 0), this));
			manifests.Add(new Manifestation(new Vector3(-0.5f, -0.5f, 0), this));
		}



		public virtual void update(GameTime gameTime)
		{
			foreach (Manifestation manifest in manifests)
				manifest.update();
		}



		public void translate(float diffX, float diffY)
		{
			foreach (Manifestation manifest in manifests)
			{
				manifest.position.X += diffX;
				manifest.position.Y += diffY;
			}
		}


		public void draw(Camera camera)
		{
			foreach (Manifestation manifest in manifests)
			{
				foreach (BasicEffect effect in model.Meshes[0].Effects)
				{
					effect.EnableDefaultLighting();
					effect.World = manifest.worldMatrix;
					effect.View = camera.getView();
					effect.Projection = camera.getProjection();
				}

				model.Meshes[0].Draw();
			}
		}
		


		public void setModel(Model newModel)
		{
			model = newModel;
		}



		public Model getModel()
		{
			return model;
		}



		protected Matrix getRotationMatrix()
		{
			return Matrix.CreateRotationX(rotation.X) * Matrix.CreateRotationY(rotation.Y) * Matrix.CreateRotationZ(rotation.Z);
		}



		protected class Manifestation
		{
			public Vector3 position;
			public Matrix worldMatrix;
			public bool visible;

			private WorldObject parent;


			public Manifestation(Vector3 position, WorldObject parent)
			{
				this.position = position;
				this.parent = parent;
			}

			public void update()
			{
				worldMatrix = parent.modelScale * parent.getRotationMatrix() * Matrix.CreateTranslation(position);
			}


			public Rectangle get2DBounds()
			{
				float radius = parent.model.Meshes[0].BoundingSphere.Radius;
				return new Rectangle((int)(position.X - radius), (int)(position.Y - radius), (int)(radius * 2), (int)(radius * 2));
			}
		}
	}
}
