using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;
using System;

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
			BoundingBox boundingBox = createBoundingBox();

			Vector3 scale = boundingBox.Max - boundingBox.Min;
			scale.X = 1 / scale.X;
			scale.Y = 1 / scale.Y;
			scale.Z = 1 / scale.Z;

			modelScale = Matrix.CreateScale(scale);
			boundingBox.Min *= scale;
			boundingBox.Max *= scale;
			
			manifests.Add(new Manifestation(new Vector3(), boundingBox, this));
			manifests.Add(new Manifestation(new Vector3(-GameParameters.WORLD_BOUNDS.Width, -GameParameters.WORLD_BOUNDS.Height, 0), boundingBox, this));
			manifests.Add(new Manifestation(new Vector3(manifests[1].position.X, 0, 0), boundingBox, this));
			manifests.Add(new Manifestation(new Vector3(0, manifests[1].position.Y, 0), boundingBox, this));
		}



		public virtual void update(GameTime gameTime)
		{
			int count = 0;
			for (int j = 0; j < manifests.Count; j++)
			{
				//if (outsideWorldBounds(manifest.get2DBounds()))
				//	manifest.visible = false;
				//else
				//	manifest.visible = true;

				//find the main ship currently entirely on the screen
				Rectangle bounds = manifests[j].get2DBounds();
				
				if (GameParameters.WORLD_BOUNDS.Contains(bounds))
				{
					//manifests[(j + 1) % manifests.Count].position.X = bounds.Left;
					manifests[(j + 1) % manifests.Count].position.Y = bounds.Top;

					if (bounds.Top >= 0)
					{
						manifests[(j + 2) % manifests.Count].position.Y = bounds.Top - GameParameters.WORLD_BOUNDS.Height;
						manifests[(j + 3) % manifests.Count].position.Y = bounds.Top - GameParameters.WORLD_BOUNDS.Height;
					}
					else
					{
						manifests[(j + 2) % manifests.Count].position.Y = bounds.Top + GameParameters.WORLD_BOUNDS.Height;
						manifests[(j + 3) % manifests.Count].position.Y = bounds.Top + GameParameters.WORLD_BOUNDS.Height;
					}

					if (bounds.Left >= 0)
					{
						manifests[(j + 2) % manifests.Count].position.X = bounds.Left - GameParameters.WORLD_BOUNDS.Width;
						manifests[(j + 3) % manifests.Count].position.X = bounds.Left - GameParameters.WORLD_BOUNDS.Width;
					}
					else
					{
						manifests[(j + 2) % manifests.Count].position.X = bounds.Left + GameParameters.WORLD_BOUNDS.Width;
						manifests[(j + 3) % manifests.Count].position.X = bounds.Left + GameParameters.WORLD_BOUNDS.Width;
					}


					//System.Diagnostics.Debug.WriteLine(bounds);
					count++;
				}
				

				manifests[j].update();
			}
			System.Diagnostics.Debug.WriteLine(count);
		}



		public bool outsideWorldBounds(Rectangle rect)
		{
			//System.Diagnostics.Debug.WriteLine(rect);
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



		//http://www.toymaker.info/Games/XNA/html/xna_bounding_box.html
		private BoundingBox createBoundingBox()
		{
			// Create variables to hold min and max xyz values for the model. Initialise them to extremes
			Vector3 modelMax = new Vector3(float.MinValue, float.MinValue, float.MinValue);
			Vector3 modelMin = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);

			foreach (ModelMesh mesh in model.Meshes)
			{
				//Create variables to hold min and max xyz values for the mesh. Initialise them to extremes
				Vector3 meshMax = new Vector3(float.MinValue, float.MinValue, float.MinValue);
				Vector3 meshMin = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);

				// There may be multiple parts in a mesh (different materials etc.) so loop through each
				foreach (ModelMeshPart part in mesh.MeshParts)
				{
					// The stride is how big, in bytes, one vertex is in the vertex buffer
					// We have to use this as we do not know the make up of the vertex
					int stride = part.VertexBuffer.VertexDeclaration.VertexStride;

					byte[] vertexData = new byte[stride * part.NumVertices];
					part.VertexBuffer.GetData(part.VertexOffset * stride, vertexData, 0, part.NumVertices, 1); // fixed 13/4/11

					// Find minimum and maximum xyz values for this mesh part
					// We know the position will always be the first 3 float values of the vertex data
					Vector3 vertPosition = new Vector3();
					for (int ndx = 0; ndx < vertexData.Length; ndx += stride)
					{
						vertPosition.X = BitConverter.ToSingle(vertexData, ndx);
						vertPosition.Y = BitConverter.ToSingle(vertexData, ndx + sizeof(float));
						vertPosition.Z = BitConverter.ToSingle(vertexData, ndx + sizeof(float) * 2);

						// update our running values from this vertex
						meshMin = Vector3.Min(meshMin, vertPosition);
						meshMax = Vector3.Max(meshMax, vertPosition);
					}
				}

				// Expand model extents by the ones from this mesh
				modelMin = Vector3.Min(modelMin, meshMin);
				modelMax = Vector3.Max(modelMax, meshMax);
			}

			// Create and return the model bounding box
			return new BoundingBox(modelMin, modelMax);
		}



		protected class Manifestation
		{
			public Vector3 position;
			public Matrix worldMatrix;
			public BoundingBox bounds;
			public bool visible = true;

			private WorldObject parent;


			public Manifestation(Vector3 position, BoundingBox boundingBox, WorldObject parent)
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
