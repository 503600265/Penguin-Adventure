using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Penguin_Adventure.Controls
{
    public class Button : Component
    {
        private MouseState currentMouse;
        private MouseState previousMouse;
        private SpriteFont font;
        private bool isHovering;
        private Texture2D texture;

        public EventHandler Click;
        public bool clicked { get; private set; }
        public Color penColor { get; set; }
        public Vector2 position { get; set; }

        public Rectangle rectangle
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            }
        }

        public string text { get; set; }

        public Button(Texture2D tex, SpriteFont f)
        {
            texture = tex;

            font = f;

            penColor = Color.Black;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var colour = Color.White;

            if (isHovering)
                colour = Color.Gray;

            spriteBatch.Draw(texture, rectangle, colour);

            if (!string.IsNullOrEmpty(text))
            {
                var x = (rectangle.X + (rectangle.Width / 2)) - (font.MeasureString(text).X / 2);
                var y = (rectangle.Y + (rectangle.Height / 2)) - (font.MeasureString(text).Y / 2);

                spriteBatch.DrawString(font, text, new Vector2(x, y), penColor);
            }
        }

        public override void Update(GameTime gameTime)
        {
            previousMouse = currentMouse;
            currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(currentMouse.X, currentMouse.Y, 1, 1);

            isHovering = false;

            if (mouseRectangle.Intersects(rectangle))
            {
                isHovering = true;

                if (currentMouse.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }
    }
}
