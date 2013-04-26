using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AsteroidsEvolved.World.WorldObjects
{
    class Particle
    {
        public Particle(int name, Vector2 position, Vector2 direction, float speed, TimeSpan lifetime)
        {
            this.Name = name;
            this.Position = position;
            this.Direction = direction;
            this.Speed = speed;
            this.Lifetime = lifetime;

            this.Rotation = 0;
            this.Alive = TimeSpan.Zero;
        }

        public int Name;
        public Vector2 Position;
        public Vector2 Direction;
        public float Rotation;
        public float Speed;
        public TimeSpan Lifetime;
        public TimeSpan Alive;
        public Texture2D Texture;
    }
}
