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
        ThreadPool threading = ThreadPool.getInstance();

        int score;
        int lives;
        public static Texture2D life_texture;

        public GameScreen(ScreenManager manager, Screen exit_screen)
            : base(manager, exit_screen)
        {
            GameParameters.threading = threading;
            threading.startWork(); //comment out to switch back to regular XNA cycle

            // Set initial score and life count.
            score = 0;
            lives = 3;

            scene = new Scene(new Camera(manager.RM.Graphics), manager.RM.Background);
            addShip();
            addAsteroids();
        }



        public void addShip()
        {
            List<WorldObject> objs = new List<WorldObject>();
            Ship ship = new Ship(scene, manager.RM.Content.Load<Model>(GameParameters.Ship.MODEL));

            scene.setShip(ship);
            objs.Add(ship);
            threading.enqueueWorkItem(new WorldObjectUpdater(ref objs));
        }



        public void addAsteroids()
        {
            List<WorldObject> objs = new List<WorldObject>();
            Asteroid asteroid = new Asteroid(scene, manager.RM.Content.Load<Model>(GameParameters.Asteroid.MODEL), new Vector3(200, 200, 0));

            scene.addAsteroid(asteroid);
            objs.Add(asteroid);
            threading.enqueueWorkItem(new WorldObjectUpdater(ref objs));
        }



        public void addRocket()
        {
            List<WorldObject> objs = new List<WorldObject>();
            Rocket rocket = new Rocket(scene, manager.RM.Content.Load<Model>(GameParameters.Rocket.MODEL), new Vector3(200, 200, 0), new Vector2(0, 0), new Vector2(1, -1));

            scene.addRocket(rocket);
            objs.Add(rocket);
            threading.enqueueWorkItem(new WorldObjectUpdater(ref objs));


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
            threading.terminate();
            base.ExitScreen();
        }
    }
}
