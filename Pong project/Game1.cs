using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Pong_project
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _player1;
        private Texture2D _player2;
        private Texture2D _ball;
        private SpriteFont _font;
        private int _player1X;
        private int _player1Y;
        private int _player1Score;
        private int _player2Score;
        private int _player2X;
        private int _player2Y;
        private int _ballX;
        private int _ballY;
        private int _paddleSpeed = 10;
        private int _ballXSpeed = 5;
        private int _ballYSpeed = 5;
        private string _player1StringScore = "0";
        private string _player2StringScore = "0";
        private Rectangle _player1Collision = new Rectangle();
        private Rectangle _player2Collision = new Rectangle();
        private Rectangle _ballCollision = new Rectangle();
        private Vector2 _player1ScoreVector = new Vector2();
        private Vector2 _player2ScoreVector = new Vector2();
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _player1X = 50;
            _player1Y = _graphics.PreferredBackBufferHeight / 2 - 70;
            _player1ScoreVector.X = 40;
            _player1ScoreVector.Y = 40;
            _player2X = 750;
            _player2Y = _graphics.PreferredBackBufferHeight / 2 - 70;
            _player2ScoreVector.X = 700;
            _player2ScoreVector.Y = 40;
            _ballX = _graphics.PreferredBackBufferWidth / 2 - 20;
            _ballY = _graphics.PreferredBackBufferHeight / 2 - 20;
            Content.RootDirectory = "Content";  
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _player1 = Content.Load<Texture2D>("paddle");
            _player2 = Content.Load<Texture2D>("paddle");
            _ball = Content.Load<Texture2D>("ball");
            _font = Content.Load<SpriteFont>("scoreFont");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            Rectangle _player1Collision = new Rectangle(_player1X, _player1Y, 40, 140);
            Rectangle _player2Collision = new Rectangle(_player2X, _player2Y, 40, 140);
            Rectangle _ballCollision = new Rectangle(_ballX, _ballY, 40, 40);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //player 1 movement
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                if (_player1Y > 0)
                {
                    //player1 up
                    _player1Y -= _paddleSpeed;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                if (_player1Y < _graphics.PreferredBackBufferHeight-_player1.Height)
                {
                    //player1 down
                    _player1Y += _paddleSpeed;
                }
            }

            //player 2 movement
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                if (_player2Y > 0)
                {
                    //player2 up
                    _player2Y -= _paddleSpeed;
                }
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                if (_player2Y < _graphics.PreferredBackBufferHeight-_player2.Height)
                {
                    //player2 down
                    _player2Y += _paddleSpeed;
                }
            }

            //ball movement

            _ballX += _ballXSpeed;
            _ballY += _ballYSpeed;

            if ((_ballY == 0 || _ballY == _graphics.PreferredBackBufferHeight - _ball.Height))
            {
                _ballYSpeed = -_ballYSpeed;
            }

            if (_ballCollision.Intersects(_player1Collision))
            {
                _ballXSpeed = -_ballXSpeed;
                _ballX += 10;
            }

            if (_ballCollision.Intersects(_player2Collision))
            {
                _ballXSpeed = -_ballXSpeed;
                _ballX -= 10;
            }

            //check score +1 condition
            if (_ballX < 0)
            {
                _player1Score += 1;
                _player1StringScore = _player1Score.ToString();
                _ballX = _graphics.PreferredBackBufferWidth / 2 - 20;
                _ballY = _graphics.PreferredBackBufferHeight / 2 - 20;
            }
            if (_ballX > _graphics.PreferredBackBufferWidth)
            {
                _player2Score += 1;
                _player2StringScore = _player2Score.ToString();
                _ballX = _graphics.PreferredBackBufferWidth / 2 - 20;
                _ballY = _graphics.PreferredBackBufferHeight / 2 - 20;
            }

            //check player win condition (10pts)
            if (_player2Score == 10)
            {
                //nothing here
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(_player1, new Rectangle(_player1X, _player1Y, _player1.Width, _player1.Height), Color.White);
            _spriteBatch.Draw(_player2, new Rectangle(_player2X, _player2Y, _player2.Width, _player2.Height), Color.White);
            _spriteBatch.Draw(_ball, new Rectangle(_ballX, _ballY, _ball.Width, _ball.Height), Color.White);
            _spriteBatch.DrawString(_font, _player1StringScore, _player1ScoreVector, Color.White);
            _spriteBatch.DrawString(_font, _player2StringScore, _player2ScoreVector, Color.White);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
