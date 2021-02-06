using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using GameProject1.Collisions;

namespace GameProject1
{
    public class CharacterSprite
    {
        private GamePadState gamePadState;

        private KeyboardState keyboardState;

        private Texture2D texture;

        public Vector2 position = new Vector2(200, 200);

        private bool flipped;

        private MouseState mouseState;

        public double rotation;

        /*private BoundingCircle bounds = new BoundingCircle(new Vector2(200, 200), 16);

        public BoundingCircle Bounds => bounds; //equivalent to { get; set; }*/

        private BoundingRectangle bounds = new BoundingRectangle(new Vector2(200, 200 - 16), 32, 32);

        public BoundingRectangle Bounds => bounds;

        public Color Color { get; set; } = Color.White;

        /// <summary>
        /// Loads the sprite texture using the provided ContentManager
        /// </summary>
        /// <param name="content">The ContentManager to load with</param>
        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Lil_Dragon_Head1");
        }

        /// <summary>
        /// Updates the sprite's position based on user input
        /// </summary>
        /// <param name="gameTime">The GameTime</param>
        public void Update(GameTime gameTime)
        {
            gamePadState = GamePad.GetState(0);
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            rotation = Math.Atan2((double)mouseState.Y - position.Y, (double)mouseState.X - position.X);

            if (rotation > 1.59 || rotation < -1.59) flipped = true;
            else flipped = false;
            // Apply the gamepad movement with inverted Y axis
            position += gamePadState.ThumbSticks.Left * new Vector2(1, -1);
            if (gamePadState.ThumbSticks.Left.X < 0) flipped = true;
            if (gamePadState.ThumbSticks.Left.X > 0) flipped = false;

            // Apply keyboard movement
            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W)) position += new Vector2(0, -1);
            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S)) position += new Vector2(0, 1);
            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                position += new Vector2(-1, 0);
                //flipped = true;
            }
            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                position += new Vector2(1, 0);
                //flipped = false;
            }

            //update sprite's collision bounds
            //bounds.Center = position;
            bounds.X = position.X;
            bounds.Y = position.Y;
        }

        /// <summary>
        /// Draws the sprite using the supplied SpriteBatch
        /// </summary>
        /// <param name="gameTime">The game time</param>
        /// <param name="spriteBatch">The spritebatch to render with</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //SpriteEffects spriteEffects = (flipped) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            SpriteEffects spriteEffects = (flipped) ? SpriteEffects.FlipVertically : SpriteEffects.None;
            spriteBatch.Draw(texture, new Vector2(position.X + 32, position.Y + 32), null, Color, (float)rotation, new Vector2(24, 32), 1f, spriteEffects, 0);
        }
    }
}
