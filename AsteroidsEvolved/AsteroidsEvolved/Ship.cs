using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsteroidsEvolved
{
	class Ship: WorldModel
	{
		public Ship(Model model):
			base(model)
		{ }



		public override void update(GameTime gameTime)
		{
			rotation.Y += (float)gameTime.ElapsedGameTime.TotalMilliseconds * MathHelper.ToRadians(0.1f);
			worldMatrix = modelScale * getRotationMatrix() * Matrix.CreateTranslation(position);
		}
	}
}
