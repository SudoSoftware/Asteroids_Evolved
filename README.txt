
This is our asteroids game.

Collision detection is implemented, but doesn't work correctly all the time. On impact, the rocket disappears and the asteroid shinks, to demonstrate the collision. This also adds to the scoring, which is unique to each player.

Code for tracking high scores is also there, but not used in the game.

World wrapping is done in a pretty cool manner, and you can see that parts of the model appear on the other side of the screen if you're on the edge.

Multiplayer is implemented (one person takes arrow keys, the other WASD)

To change keyboard controls, use the options submenu. Press Enter after selecting the desired key.

Task-based threading is done, and it updates different type of objects. Mutexes are used for synchonization.

All models are 3D and rotate. Finding free models was quite difficult, and we ended up having to use Blender to convert the file types of the ones that we found.

Attract mode is also implemented, though the AI is simple at the moment.