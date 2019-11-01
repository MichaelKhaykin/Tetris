using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Screen
    {
        public ContentManager Content { get; private set; }
        public GraphicsDeviceManager Graphics { get; private set; }
        private List<IGameObject> ObjectsToUpdate;
        private List<IGameObject> ObjectsToDraw;
        public Screen(ContentManager content, GraphicsDeviceManager graphics)
        {
            Content = content;
            Graphics = graphics;

            ObjectsToUpdate = new List<IGameObject>();
            ObjectsToDraw = new List<IGameObject>();
        }

        public void AddToUpdateList(IGameObject a) => ObjectsToUpdate.Add(a);
        public void AddToDrawList(IGameObject a) => ObjectsToDraw.Add(a);
        
        public void AddToBoth(IGameObject a)
        {
            ObjectsToUpdate.Add(a);
            ObjectsToDraw.Add(a);
        }
        public void AddRangeToUpdateList(List<IGameObject> gameObjects) => ObjectsToUpdate.AddRange(gameObjects);
        public void AddRangeToDrawList(List<IGameObject> gameObjects) => ObjectsToDraw.AddRange(gameObjects);
  
        public void AddRangeToBoth(List<IGameObject> gameObjects)
        {
            ObjectsToUpdate.AddRange(gameObjects);
            ObjectsToDraw.AddRange(gameObjects);
        }
        public virtual void Update(GameTime gameTime)
        {
            foreach(var gameObject in ObjectsToUpdate)
            {
                gameObject.Update(gameTime);
            }
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            foreach(var gameObject in ObjectsToDraw)
            {
                gameObject.Draw(spriteBatch);
            }
        }
    }
}
