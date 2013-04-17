using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsteroidsEvolved
{
	class Ship : WorldObject
	{
		public Ship(Model model) :
			base(model)
		{
			rotation.X = MathHelper.ToRadians(90.0f);
			rotation.Y = MathHelper.ToRadians(180.0f);
		}



		public override void update(GameTime gameTime)
		{
			//rotation.Y += (float)gameTime.ElapsedGameTime.TotalMilliseconds * MathHelper.ToRadians(0.1f);
			base.update(gameTime);
		}


		public void moveForward()
		{
			//translate(0, 0.1f);
		}
	}
}
