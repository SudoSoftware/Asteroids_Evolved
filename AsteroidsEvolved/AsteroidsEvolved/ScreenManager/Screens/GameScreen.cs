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

namespace AsteroidsEvolved
{
    class GameScreen : Screen
    {
        Scene scene;

        int score;
        int lives;
        public static Texture2D life_texture;

        public GameScreen(ScreenManager manager, Screen exit_screen)
            : base(manager, exit_screen)
        {
            // Set initial score and life count.
            score = 0;
            lives = 3;

            scene = new Scene(new Camera(manager.RM.Graphics), manager.RM.Background);
            addShip();
            addAsteroids();
			ThreadPool.getInstance().enqueueWorkItem(new CollisionDetector(scene));

			ThreadPool.getInstance().startWork();
        }


        public override void HandleInput(GameTime time, UserInput input)
        {
            // Add control code here.
            base.HandleInput(time, input);
        }



        public void addShip()
        {
            Ship ship = new Ship(scene, manager.RM.Content.Load<Model>(GameParameters.Ship.MODEL));
            scene.addShip(ship);
			ThreadPool.getInstance().enqueueWorkItem(new ShipUpdater(scene));
        }



        public void addAsteroids()
        {
            Asteroid asteroid = new Asteroid(scene, manager.RM.Content.Load<Model>(GameParameters.Asteroid.MODEL), new Vector3(200, 200, 0));
            scene.addAsteroid(asteroid);
			ThreadPool.getInstance().enqueueWorkItem(new AsteroidsUpdater(scene));
        }



        public void addRocket()
        {
            Rocket rocket = new Rocket(scene, manager.RM.Content.Load<Model>(GameParameters.Rocket.MODEL), new Vector3(200, 200, 0), new Vector2(0, 0), new Vector2(1, -1));
            scene.addRocket(rocket);
			ThreadPool.getInstance().enqueueWorkItem(new RocketUpdater(scene));
        }



        public override void Draw()
        {
            scene.draw();
            
            // Draw scores and lives.
            manager.RM.SpriteB.Begin();
            manager.RM.SpriteB.DrawString(
                (SpriteFont)manager.RM.FontHash["IntroFont"],
                score.ToString(),
                GameParameters.World.score_position,
                Color.AntiqueWhite
                );

            Vector2 lifepos = GameParameters.World.life_position;

            for (int i = 0; i < lives - 1; ++i)
            {
                manager.RM.SpriteB.Draw(
                    life_texture,
                    lifepos,
                    Color.AntiqueWhite
                    );

                lifepos += GameParameters.World.life_increment;
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
