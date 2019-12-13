using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MichaelLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Tetris.Screens
{
    public class MenuScreen : Screen
    {
        Sprite TetrisTitle;

        AdvancedShadowButton playButton;
        AdvancedShadowButton settingsButton;
        AdvancedShadowButton exitButton;
        AdvancedShadowButton multiplayerButton;
        public MenuScreen(ContentManager content, GraphicsDeviceManager graphics)
            : base(content, graphics)
        {
            var center = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);

            var titleTexture = content.Load<Texture2D>("MenuAssets/title");
            TetrisTitle = new Sprite(titleTexture, new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, titleTexture.Height / 2), Color.White, Vector2.One);

            var playButtonTexture = content.Load<Texture2D>("MenuAssets/singlePlayerButton");
            var playButtonScale = Vector2.One * 2;
            playButton = new AdvancedShadowButton(playButtonTexture, new Vector2(center.X, center.Y - playButtonTexture.Height * playButtonScale.Y), Color.White, playButtonScale, null, Color.LightBlue)
            {
                DelayBeforeRunningAction = TimeSpan.FromMilliseconds(30),
                ClickedAction = () => ScreenManager.ChangeScreen(ScreenStates.SingePlayerGame)
            };

            var multiplayerTexture = content.Load<Texture2D>("MenuAssets/multiplayerButton");
            var multiplayerButtonScale = Vector2.One * 2;
            multiplayerButton = new AdvancedShadowButton(multiplayerTexture, new Vector2(center.X, center.Y + multiplayerTexture.Height / 2 * multiplayerButtonScale.Y - 30), Color.White, multiplayerButtonScale, null, Color.LightBlue)
            {
                DelayBeforeRunningAction = TimeSpan.FromMilliseconds(30),
                ClickedAction = () => ScreenManager.ChangeScreen(ScreenStates.MultiplayerGame)
            };
        

            var settingsButtonTexture = content.Load<Texture2D>("MenuAssets/optionsButtonNew");
            var settingsButtonScale = Vector2.One * 2;
            settingsButton = new AdvancedShadowButton(settingsButtonTexture, new Vector2(center.X, center.Y + settingsButtonTexture.Height * 2 * settingsButtonScale.Y - 60), Color.White, settingsButtonScale, null, Color.LightBlue)
            {
                DelayBeforeRunningAction = TimeSpan.FromMilliseconds(30),
                ClickedAction = () => ScreenManager.ChangeScreen(ScreenStates.Settings)
            };

            var exitButtonTexture = content.Load<Texture2D>("MenuAssets/quitButton");
            var exitButtonScale = Vector2.One * 2;
            exitButton = new AdvancedShadowButton(exitButtonTexture, new Vector2(center.X, center.Y + exitButtonTexture.Height * 4 * exitButtonScale.Y - 140), Color.White, exitButtonScale, null, Color.LightBlue)
            {
                DelayBeforeRunningAction = TimeSpan.FromMilliseconds(30),
                ClickedAction = Game1.GameExitDelegate
            };

            AddToDrawList(TetrisTitle);
        }

        public override void Update(GameTime gameTime)
        {
            playButton.Update(gameTime, InputManager.MouseState);
            settingsButton.Update(gameTime, InputManager.MouseState);
            exitButton.Update(gameTime, InputManager.MouseState);
            multiplayerButton.Update(gameTime, InputManager.MouseState);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            playButton.Draw(spriteBatch);
            settingsButton.Draw(spriteBatch);
            exitButton.Draw(spriteBatch);
            multiplayerButton.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}
