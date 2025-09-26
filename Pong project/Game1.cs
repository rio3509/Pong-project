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
        private int _player1Height;
        private int _player2Score;
        private int _player2X;
        private int _player2Y;
        private int _player2Height;
        private int _ballX;
        private int _ballY;
        private int _ballHeight;
        private int _ballWidth;
        private int _paddleSpeed = 10;
        private int _ballXSpeed = 4;
        private int _ballYSpeed = 4;
        private string _player1StringScore = "Score: 0";
        private string _player2StringScore = "Score: 0";
        private string _winString = "";
        private bool _player1Shrink = false;
        private bool _player2Shrink = false;
        private bool _gameRunning = false;
        private Rectangle _player1Collision = new Rectangle();
        private Rectangle _player2Collision = new Rectangle();
        private Rectangle _ballCollision = new Rectangle();
        private Vector2 _player1ScoreVector = new Vector2();
        private Vector2 _player2ScoreVector = new Vector2();
        private Vector2 _winStringVector = new Vector2();
        private Color _gameColor = Color.White;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            _winStringVector.X = _graphics.PreferredBackBufferWidth/2 - 100;
            _winStringVector.Y = _graphics.PreferredBackBufferHeight/2 + 40;
            _player1X = 10;
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
            _player1Height = _player1.Height;
            _player2 = Content.Load<Texture2D>("paddle");
            _player2Height = _player2.Height;
            _ball = Content.Load<Texture2D>("ball");
            _ballHeight = _ball.Height;
            _ballWidth = _ball.Width;
            _font = Content.Load<SpriteFont>("scoreFont");

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                _gameRunning = true;
            }


            if (_gameRunning)
            {
                Rectangle _player1Collision = new Rectangle(_player1X, _player1Y, _player1.Width, _player1Height);
                Rectangle _player2Collision = new Rectangle(_player2X, _player2Y, _player2.Width, _player2Height);
                Rectangle _ballCollision = new Rectangle(_ballX, _ballY, _ball.Width, _ball.Height);

                //check player win condition (10pts)
                if (_player1Score == 10)
                {
                    //player 1 win - stop ball + display message
                    _ballXSpeed = 0;
                    _ballYSpeed = 0;
                    _winString = "Player 1 has won";
                }
                if (_player2Score == 10)
                {
                    //player 2 win - stop ball + display message
                    _ballXSpeed = 0;
                    _ballYSpeed = 0;
                    _winString = "Player 2 has won";
                }


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
                    if (_player1Y < _graphics.PreferredBackBufferHeight - _player1.Height)
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
                    if (_player2Y < _graphics.PreferredBackBufferHeight - _player2.Height)
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
                if (_ballX <= 0)
                {
                    _player2Score += 1;
                    _player2StringScore = "Score: " + _player2Score.ToString();
                    _ballX = _graphics.PreferredBackBufferWidth / 2 - 20;
                    _ballY = _graphics.PreferredBackBufferHeight / 2 - 20;
                }
                if (_ballX >= _graphics.PreferredBackBufferWidth - _ball.Width)
                {
                    _player1Score += 1;
                    _player1StringScore = "Score: " + _player1Score.ToString();
                    _ballX = _graphics.PreferredBackBufferWidth / 2 - 20;
                    _ballY = _graphics.PreferredBackBufferHeight / 2 - 20;
                }


                //the first time a player is 5 points ahead of another, their paddle halves in size

                if (_player1Shrink == false)
                {
                    if (_player2Score - _player1Score <= -5)
                    {
                        _player1Height = _player1Height / 2;
                        _player1Shrink = true;
                    }
                }

                if (_player2Shrink == false)
                {
                    if (_player1Score - _player2Score <= -5)
                    {
                        _player2Height = _player2Height / 2;
                        _player2Shrink = true;
                    }
                }

                //if the "y" key is pressed, then the ball becomes twice as fast
                if (Keyboard.GetState().IsKeyDown(Keys.Y))
                {
                    _ballXSpeed = _ballXSpeed * 2;
                    _ballYSpeed = _ballYSpeed * 2;
                }
                //if the "n" key is pressed, then the ball becomes half as fast
                if (Keyboard.GetState().IsKeyDown(Keys.N))
                {
                    _ballXSpeed = _ballXSpeed / 2;
                    _ballYSpeed = _ballYSpeed / 2;
                }
                //if the "h" key is pressed, then the ball becomes smaller
                if (Keyboard.GetState().IsKeyDown(Keys.H))
                {
                    _ballWidth = _ball.Width / 2;
                    _ballHeight = _ball.Height / 2;
                }
                //if the "r" key is pressed, then everything becomes red
                if (Keyboard.GetState().IsKeyDown(Keys.R))
                {
                    _gameColor = Color.Red;
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            _spriteBatch.Draw(_player1, new Rectangle(_player1X, _player1Y, _player1.Width, _player1Height), _gameColor);
            _spriteBatch.Draw(_player2, new Rectangle(_player2X, _player2Y, _player2.Width, _player2Height), _gameColor);
            _spriteBatch.Draw(_ball, new Rectangle(_ballX, _ballY, _ball.Width, _ball.Height), _gameColor);
            _spriteBatch.DrawString(_font, _player1StringScore, _player1ScoreVector, _gameColor);
            _spriteBatch.DrawString(_font, _player2StringScore, _player2ScoreVector, _gameColor);
            _spriteBatch.DrawString(_font, _winString, _winStringVector, _gameColor);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
