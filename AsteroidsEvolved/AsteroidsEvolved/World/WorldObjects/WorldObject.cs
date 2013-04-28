using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace AsteroidsEvolved.World.WorldObjects
{
	abstract class WorldObject
	{
        protected Scene scene;

		protected Model model;
		protected Matrix modelScale;
		private BoundingBox modelBounds;

		protected Vector3 rotation;
		protected List<Manifestation> manifests = new List<Manifestation>();


		public WorldObject(Scene scene, Model model, Vector3 initialLocation, float size):
			this(scene, model, initialLocation, new Vector3(size, size, size))
		{ }



		public WorldObject(Scene scene, Model model, Vector3 initialLocation, Vector3 desiredBounds)
		{
			this.scene = scene;
			this.model = model;
			modelBounds = createBoundingBox();

			Vector3 scale = modelBounds.Max - modelBounds.Min;
			scale.X = desiredBounds.X / scale.X;
			scale.Y = desiredBounds.Y / scale.Y;
			scale.Z = desiredBounds.Z / scale.Z;

			modelScale = Matrix.CreateScale(scale);
			modelBounds.Min *= scale;
			modelBounds.Max *= scale;

			System.Diagnostics.Debug.WriteLine("Constructing new " + this);
			//System.Diagnostics.Debug.WriteLine(desiredBounds + "	" + modelBounds);
			//System.Diagnostics.Debug.WriteLine(modelBounds.Max - modelBounds.Min);

			manifests.Add(new Manifestation(initialLocation, this));
			for (int j = 0; j < 3; j++)
				manifests.Add(new Manifestation(new Vector3(), this));
			rearrangeManifests(0);
		}



		public virtual void update(TimeSpan elapsedGameTime)
		{
			for (int j = 0; j < manifests.Count; j++)
			{
				//find the main ship currently entirely on the screen
				if (insideWorldBounds(manifests[j].getBoundingBox()))
					rearrangeManifests(j);

				manifests[j].update();
			}
		}



		public void draw(Camera camera)
		{
			foreach (Manifestation manifest in manifests)
			{
				if (!manifest.visible)
					continue;

				foreach (ModelMesh mesh in model.Meshes)
				{
					foreach (BasicEffect effect in mesh.Effects)
					{
						effect.EnableDefaultLighting();
						effect.World = manifest.getWorldMatrix();
						effect.View = camera.getView();
						effect.Projection = camera.getProjection();
					}

					mesh.Draw();
				}
			}
		}


		protected void rearrangeManifests(int masterManifest)
		{
			float offsetX = (manifests[masterManifest].position.X >= 0) ?
						manifests[masterManifest].position.X - GameParameters.World.BOUNDS.Width
						: manifests[masterManifest].position.X + GameParameters.World.BOUNDS.Width;

			float offsetY = (manifests[masterManifest].position.Y >= 0) ?
				manifests[masterManifest].position.Y - GameParameters.World.BOUNDS.Height
				: manifests[masterManifest].position.Y + GameParameters.World.BOUNDS.Height;

			manifests[(masterManifest + 1) % manifests.Count].position.X = manifests[masterManifest].position.X;
			manifests[(masterManifest + 1) % manifests.Count].position.Y = offsetY;

			manifests[(masterManifest + 2) % manifests.Count].position.X = offsetX;
			manifests[(masterManifest + 2) % manifests.Count].position.Y = offsetY;

			manifests[(masterManifest + 3) % manifests.Count].position.X = offsetX;
			manifests[(masterManifest + 3) % manifests.Count].position.Y = manifests[masterManifest].position.Y;
		}



		public bool intersects(WorldObject obj)
		{
			bool intersects = false;
			for (int j = 0; j < manifests.Count; j++)
				if (manifests[j].getBoundingBox().Intersects(obj.manifests[j].getBoundingBox()))
					intersects = true;
			return intersects;
		}



		public virtual void handleIntersection(WorldObject obj)
		{
			System.Diagnostics.Debug.WriteLine("WorldObject handling intersection between " + this + " and " + obj);
		}



		public void translate(float diffX, float diffY)
		{
			foreach (Manifestation manifest in manifests)
			{
				manifest.position.X += diffX;
				manifest.position.Y += diffY;
			}
		}



		public void rotate(float diffX, float diffY, float diffZ)
		{
			rotation.X += diffX;
			rotation.Y += diffY;
			rotation.Z += diffZ;
		}



		public bool insideWorldBounds(BoundingBox box)
		{
			return box.Min.X > GameParameters.World.BOUNDS.Left && box.Max.X < GameParameters.World.BOUNDS.Right
				&& box.Min.Y > GameParameters.World.BOUNDS.Top && box.Max.Y < GameParameters.World.BOUNDS.Bottom;
		}



		public bool outsideWorldBounds(BoundingBox box)
		{
			return box.Min.X > GameParameters.World.BOUNDS.Right || box.Max.X < GameParameters.World.BOUNDS.Left
				|| box.Min.Y > GameParameters.World.BOUNDS.Bottom || box.Max.Y < GameParameters.World.BOUNDS.Top;
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



		public Vector3 getPosOfFirstManifest()
		{
			return manifests[0].position;
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


			public Manifestation(Vector3 position, WorldObject parent)
			{
				this.position = position;
				this.parent = parent;
			}



			public void update()
			{
				worldMatrix = parent.modelScale * parent.getRotationMatrix() * Matrix.CreateTranslation(position);

				/*if (parent.outsideWorldBounds(getBoundingBox()))
					visible = false;
				else
					visible = true;*/
			}



			public Matrix getWorldMatrix()
			{
				return worldMatrix;
			}



			public BoundingBox getBoundingBox()
			{
				BoundingBox bb = parent.modelBounds;
				bb.Min += position;
				bb.Max += position;
				return bb;
			}
		}
	}
}
