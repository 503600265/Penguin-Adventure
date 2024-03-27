using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Penguin_Adventure.Enemy
{
    public class Fox : Enemies
    {
        public Texture2D texture;
        public Vector2 position;
        public Rectangle collider;
        public Texture2D healthbar;
        public Rectangle healthRec;

        public int health = 40;
        public int damage = 2;

        private Rectangle[] animation;
        private int frame;

        float timer = 0;
        int animSpeed = 70;

        public override void Initialize(Texture2D text, Vector2 pos, Rectangle[] anim, int f, Rectangle collider, Texture2D healthbar, Rectangle healthRec)
        {
            texture = text;
            position = pos;
            animation = anim;
            frame = f;
            this.collider = collider;
            this.healthbar = healthbar;
            this.healthRec = healthRec;
        }

        public override Vector2 Position
        {
            get { return position; }
        }

        public override int Health
        {
            get { return health; }
            set { }
        }
        public override int Damage
        {
            get { return damage; }
        }

        public override void Update(GameTime gameTime)
        {
            if (timer > animSpeed)
            {
                if (frame < 13)
                {
                    frame++;
                }
                else
                    frame = 0;
            }
            else
            {
                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, animation[frame], Color.White);
        }

        public override void attack()
        {
            throw new NotImplementedException();
        }

        public override void die()
        {
            throw new NotImplementedException();
        }
    }
}
