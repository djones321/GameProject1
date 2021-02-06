using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject1
{
    public class Lil_Dragon1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SpriteFont spriteFont;
        private CharacterSprite dragon;
        private EnemySprite[] mecheval;
        private System.Random rand;
        private int alive;



        //private EnemySprite enemy1;



        public Lil_Dragon1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            dragon = new CharacterSprite();
            rand = new System.Random();
            mecheval = new EnemySprite[]
            {
                new EnemySprite(new Vector2((float)rand.NextDouble() * GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * GraphicsDevice.Viewport.Height)),
                new EnemySprite(new Vector2((float)rand.NextDouble() * GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * GraphicsDevice.Viewport.Height)),
                new EnemySprite(new Vector2((float)rand.NextDouble() * GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * GraphicsDevice.Viewport.Height)),
                new EnemySprite(new Vector2((float)rand.NextDouble() * GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * GraphicsDevice.Viewport.Height)),
                new EnemySprite(new Vector2((float)rand.NextDouble() * GraphicsDevice.Viewport.Width, (float)rand.NextDouble() * GraphicsDevice.Viewport.Height)),

            };
            alive = mecheval.Length;
            //enemy1 = new EnemySprite(new Vector2(300, 300));
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            dragon.LoadContent(Content);
            foreach (EnemySprite enemy in mecheval) enemy.LoadContent(Content);
            spriteFont = Content.Load<SpriteFont>("bangers");
            //enemy1.LoadContent(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            dragon.Update(gameTime);
            dragon.Color = Color.White;
            foreach (EnemySprite enemy in mecheval)
            {
                if (enemy.Bounds.CollidesWith(dragon.Bounds))
                {
                    dragon.Color = Color.Red;
                    continue;
                }
                enemy.Update(dragon.position);

            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            
            foreach (EnemySprite enemy in mecheval) enemy.Draw(gameTime, _spriteBatch);
            dragon.Draw(gameTime, _spriteBatch);
            //_spriteBatch.DrawString(spriteFont, $"Angle?: {dragon.rotation}", new Vector2(2, 2), Color.Gold);
            //enemy1.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
