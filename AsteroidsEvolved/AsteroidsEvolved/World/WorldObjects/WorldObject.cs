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
			manifests.Add(new Manifestation(new Vector3(-GameParameters.WORLD_BOUNDS.Width, -GameParameters.WORLD_BOUNDS.Height, 0), this)); //todo: remove /3
			manifests.Add(new Manifestation(new Vector3(manifests[1].position.X, 0, 0), this));
			manifests.Add(new Manifestation(new Vector3(0, manifests[1].position.Y, 0), this));
		}



		public virtual void update(GameTime gameTime)
		{
			foreach (Manifestation manifest in manifests)
			{
				if (outsideWorldBounds(manifest.get2DBounds()))
					manifest.visible = false;
				else
					manifest.visible = true;

				manifest.update();
			}

		}



		public bool outsideWorldBounds(Rectangle rect)
		{
			return rect.Left < GameParameters.WORLD_BOUNDS.Left
				|| rect.Right > GameParameters.WORLD_BOUNDS.Right
				|| rect.Top < GameParameters.WORLD_BOUNDS.Top
				|| rect.Bottom > GameParameters.WORLD_BOUNDS.Bottom;
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
				if (!manifest.visible)
					continue;

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
			public bool visible = true;

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
				return new Rectangle((int)position.X - 1, (int)position.Y - 1, 2, 2);
			}
		}
	}
}
