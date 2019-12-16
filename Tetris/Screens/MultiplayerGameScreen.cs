using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Net.Sockets;

namespace Tetris.Screens
{
    internal class MultiplayerGameScreen : Screen
    {
        private TcpClient client;

        public MultiplayerGameScreen(ContentManager content, GraphicsDeviceManager graphics, TcpClient client)
            : base(content, graphics)
        {
            this.client = client;
        }
    }
}