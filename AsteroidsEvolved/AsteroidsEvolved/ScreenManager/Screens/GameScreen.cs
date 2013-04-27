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

        public GameScreen(ScreenManager manager, Screen exit_screen)
            : base(manager, exit_screen)
        {
            GameParameters.Player1.reset_vars();
            GameParameters.Player2.reset_vars();

            scene = new Scene(new Camera(manager.RM.Graphics), manager.RM.Background);
			ThreadPool.getInstance().enqueueWorkItem(new CollisionDetector(scene));
            addShip();
            addAsteroids();

			ThreadPool.getInstance().startWork();
        }


        public override void HandleInput(GameTime time, UserInput input)
        {
            // Add control code here.
            base.HandleInput(time, input);
        }



        public void addShip()
        {
            Ship ship = new Ship(scene, manager.RM.Content.Load<Model>(GameParameters.Ship.MODEL), GameParameters.Player1); //todo: set up multiple ships
            scene.addShip(ship);
			ThreadPool.getInstance().enqueueWorkItem(new ShipUpdater(scene));
        }



        public void addAsteroids()
        {
            Asteroid asteroid = new Asteroid(scene, manager.RM.Content.Load<Model>(GameParameters.Asteroid.MODEL), new Vector3(200, 200, 0));
            scene.addAsteroid(asteroid);
			ThreadPool.getInstance().enqueueWorkItem(new AsteroidsUpdater(scene));
        }



        public override void Draw()
        {
            scene.draw();
            
            // Draw scores and lives.
            manager.RM.SpriteB.Begin();
            if (GameParameters.Player1.player_mode != GameParameters.Mode.NA)
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

            if (GameParameters.Player2.player_mode != GameParameters.Mode.NA)
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
            base.ExitScreen();
        }
    }
}
