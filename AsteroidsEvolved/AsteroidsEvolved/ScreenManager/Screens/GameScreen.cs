using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using AsteroidsEvolved.World;
using AsteroidsEvolved.World.WorldObjects;
using AsteroidsEvolved.Threading;
using AsteroidsEvolved.Threading.WorkItems;
using AsteroidsEvolved.GameInput;

namespace AsteroidsEvolved
{
    class GameScreen : Screen
    {
        Scene scene;
        public static Texture2D life_texture;

		GameParameters.Mode prevval_1;
		GameParameters.Mode prevval_2;
		bool attract;

        public GameScreen(ScreenManager manager, Screen exit_screen, bool attract)
            : base(manager, exit_screen)
        {
			prevval_1 = GameParameters.Player1.PlayerMode;
			prevval_2 = GameParameters.Player2.PlayerMode;

			this.attract = attract;
			if (attract)
			{
				GameParameters.Player1.PlayerMode = GameParameters.Mode.AI;
				GameParameters.Player2.PlayerMode = GameParameters.Mode.NA;
				manager.input = GameParameters.Player1.userInput;
			}

            GameParameters.Player1.reset_vars();
			GameParameters.Player2.reset_vars();

            scene = new Scene(new Camera(manager.RM.Graphics), manager.RM.Background);
			ThreadPool.getInstance().enqueueWorkItem(new CollisionDetector(scene));
			ThreadPool.getInstance().enqueueWorkItem(new RocketUpdater(scene));
			addShips();
            addAsteroids();

			ThreadPool.getInstance().startWork();
        }


		public override void HandleInput(GameTime time, UserInput input)
        {
			GameParameters.Player2.update();
            base.HandleInput(time, input);
        }



		public void addShips()
		{
			addShip(GameParameters.Player1, new Vector3(-50, 0, 0));
			addShip(GameParameters.Player2, new Vector3(50, 0, 0));
			ThreadPool.getInstance().enqueueWorkItem(new ShipUpdater(scene));
		}



        public void addShip(Player player, Vector3 location)
        {
			System.Diagnostics.Debug.WriteLine(player.PlayerMode);
			if (player.PlayerMode == GameParameters.Mode.NA)
				return;
			
            Ship ship = new Ship(scene, manager.RM.Content.Load<Model>(GameParameters.Ship.MODEL), location, player);
            scene.addShip(ship);
        }



        public void addAsteroids()
        {
			MyRandom rand = new MyRandom();

			for (int i = 0 ; i < 4 ; ++i)
				scene.addAsteroid(
					new Asteroid(
						scene,
						manager.RM.Content.Load<Model>(GameParameters.Asteroid.MODEL),
						new Vector3((int)rand.nextRange(-960, 960) + 100, (int)rand.nextRange(-512, 512) + 100, 0),
						rand.nextCircleVector()
						)
					);

			ThreadPool.getInstance().enqueueWorkItem(new AsteroidsUpdater(scene));
        }



        public override void Draw()
        {
            scene.draw();
            
            // Draw scores and lives.
            manager.RM.SpriteB.Begin();
			if (GameParameters.Player1.PlayerMode != GameParameters.Mode.NA)
            {
                manager.RM.SpriteB.DrawString(
                    (SpriteFont)manager.RM.FontHash["IntroFont"],
                    GameParameters.Player1.score.ToString(),
                    GameParameters.Player1.score_position,
                    Color.AntiqueWhite
                    );

                Vector2 lifepos = GameParameters.Player1.life_position;

                for (int i = 0; i < GameParameters.Player1.lives - 1; ++i)
                {
                    manager.RM.SpriteB.Draw(
                        life_texture,
                        lifepos,
                        Color.AntiqueWhite
                        );

                    lifepos += GameParameters.Player1.life_increment;
                }
            }

			if (GameParameters.Player2.PlayerMode != GameParameters.Mode.NA)
            {
                manager.RM.SpriteB.DrawString(
                    (SpriteFont)manager.RM.FontHash["IntroFont"],
                    GameParameters.Player2.score.ToString(),
                    GameParameters.Player2.score_position,
                    Color.AntiqueWhite
                    );

                Vector2 lifepos = GameParameters.Player2.life_position;

                for (int i = 0; i < GameParameters.Player2.lives - 1; ++i)
                {
                    manager.RM.SpriteB.Draw(
                        life_texture,
                        lifepos,
                        Color.AntiqueWhite
                        );

                    lifepos += GameParameters.Player2.life_increment;
                }
            }
            manager.RM.SpriteB.End();
        }



        public override void ExitScreen()
        {
            ThreadPool.getInstance().terminate();

			if (attract)
			{
				GameParameters.Player1.PlayerMode = prevval_1;
				GameParameters.Player2.PlayerMode = prevval_2;
				manager.input = GameParameters.Player1.userInput;
			}

            base.ExitScreen();
        }
    }
}
