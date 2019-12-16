using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris.Screens
{
    public class WaitingForMultiplayerGameScreen : Screen
    {
        TcpClient client;

        Sprite loadingicon;

        public WaitingForMultiplayerGameScreen(ContentManager content, GraphicsDeviceManager graphics)
            : base(content, graphics)
        {
            var center = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);

            var loadingiconTexture = Content.Load<Texture2D>("loadingicon");
            loadingicon = new Sprite(loadingiconTexture, center, Color.White, Vector2.One);

            Int32 port = 10000;

            var serverIP = "192.168.1.93";

            //lobby room connection
            client = new TcpClient(serverIP, port);

            var data = new Byte[256];

            Int32 bytes = client.GetStream().Read(data, 0, data.Length);
            var responseData = Encoding.ASCII.GetString(data, 0, bytes);
            var newPortToConnectTo = int.Parse(responseData);

            client.Close();

            //now we are connected into the game but are still waiting on second player
            client = new TcpClient(serverIP, newPortToConnectTo);

            AddToDrawList(loadingicon);
        }

        public override void Update(GameTime gameTime)
        {
            loadingicon.Rotation += 0.5f;


            //this means the server has given us the go to start a game
            if(client.GetStream().DataAvailable)
            {
                byte[] bytes = new byte[10];
                var length = client.GetStream().Read(bytes, 0, bytes.Length);
                var msg = Encoding.ASCII.GetString(bytes, 0, length);
                if (msg == "1")
                {
                    ScreenManager.AddScreen(ScreenStates.MultiplayerGame, new MultiplayerGameScreen(Content, Graphics, client));
                    ScreenManager.ChangeScreen(ScreenStates.MultiplayerGame);
                }
            }

            base.Update(gameTime);
        }
    }
}
