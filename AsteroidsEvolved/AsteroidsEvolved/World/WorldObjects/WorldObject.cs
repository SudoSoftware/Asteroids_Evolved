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


		public WorldObject(Model model)
		{
			this.model = model;
			Debug.Assert(model.Meshes.Count == 1);
			
			BoundingBox boundingBox = createBoundingBox();
			Vector3 scale = boundingBox.Max - boundingBox.Min;
			scale.X = GameParameters.MODEL_SIZE / scale.X;
			scale.Y = GameParameters.MODEL_SIZE / scale.Y;
			scale.Z = GameParameters.MODEL_SIZE / scale.Z;

			System.Diagnostics.Debug.WriteLine(boundingBox);

			modelScale = Matrix.CreateScale(scale);
			boundingBox.Min *= scale;
			boundingBox.Max *= scale;

			System.Diagnostics.Debug.WriteLine(boundingBox);
			System.Diagnostics.Debug.WriteLine(boundingBox.Max - boundingBox.Min);

			manifests.Add(new Manifestation(new Vector3(), boundingBox, this));
			for (int j = 0; j < 3; j++)
				manifests.Add(new Manifestation(new Vector3(float.MinValue, float.MinValue, 0), boundingBox, this));
		}



		public virtual void update(GameTime gameTime)
		{
			for (int j = 0; j < manifests.Count; j++)
			{
				BoundingBox boundingBox = manifests[j].getBoundingBox();
				if (outsideWorldBounds(boundingBox))
					manifests[j].visible = false;
				else
					manifests[j].visible = true;

				//find the main ship currently entirely on the screen
				if (insideWorldBounds(boundingBox))
				{
					float offsetX = (manifests[j].position.X >= 0) ? manifests[j].position.X - GameParameters.WORLD_BOUNDS.Width : manifests[j].position.X + GameParameters.WORLD_BOUNDS.Width;
					float offsetY = (manifests[j].position.Y >= 0) ? manifests[j].position.Y - GameParameters.WORLD_BOUNDS.Height : manifests[j].position.Y + GameParameters.WORLD_BOUNDS.Height;

					manifests[(j + 1) % manifests.Count].position.X = manifests[j].position.X;
					manifests[(j + 1) % manifests.Count].position.Y = offsetY;

					manifests[(j + 2) % manifests.Count].position.X = offsetX;
					manifests[(j + 2) % manifests.Count].position.Y = offsetY;

					manifests[(j + 3) % manifests.Count].position.X = offsetX;
					manifests[(j + 3) % manifests.Count].position.Y = manifests[j].position.Y;
				}

				manifests[j].update();
			}
		}



		public bool insideWorldBounds(BoundingBox box)
		{
			return box.Min.X > GameParameters.WORLD_BOUNDS.Left && box.Max.X < GameParameters.WORLD_BOUNDS.Right
				&& box.Min.Y > GameParameters.WORLD_BOUNDS.Top && box.Max.Y < GameParameters.WORLD_BOUNDS.Bottom;
		}


		
		public bool outsideWorldBounds(BoundingBox box)
		{
			return box.Min.X > GameParameters.WORLD_BOUNDS.Right || box.Max.X < GameParameters.WORLD_BOUNDS.Left
				|| box.Min.Y > GameParameters.WORLD_BOUNDS.Bottom || box.Max.Y < GameParameters.WORLD_BOUNDS.Top;
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
					effect.World = manifest.getWorldMatrix();
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
			public bool visible = true;

			private WorldObject parent;
			private Matrix worldMatrix;
			private BoundingBox boundingBox;


			public Manifestation(Vector3 position, BoundingBox boundingBox, WorldObject parent)
			{
				this.position = position;
				this.parent = parent;
				this.boundingBox = boundingBox;
			}



			public void update()
			{
				worldMatrix = parent.modelScale * parent.getRotationMatrix() * Matrix.CreateTranslation(position);
			}



			public Matrix getWorldMatrix()
			{
				return worldMatrix;
			}



			public BoundingBox getBoundingBox()
			{
				BoundingBox bb = boundingBox;
				bb.Min += position;
				bb.Max += position;
				return bb;
			}
		}
	}
}
