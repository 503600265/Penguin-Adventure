using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Penguin_Adventure.Enemy
{
    public abstract class Enemies
    {
        public abstract Vector2 Position { get; }
        public abstract int Health { get; set; }
        public abstract int Damage { get; }

        public abstract void Initialize(Texture2D text, Vector2 pos, Rectangle[] anim, int f, Rectangle collider, Texture2D healthbar, Rectangle healthRec);
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
        public abstract void attack();
        public abstract void die();

    }
}
