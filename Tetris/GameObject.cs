using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public abstract class GameObject
    {
        public virtual Vector2 Position { get; set; }
        public Color Color { get; set; }
        public virtual float Rotation { get; set; }
        public Vector2 Scale { get; set; }
        public Vector2 Origin { get; set; }
        public float LayerDepth { get; set; }
        public SpriteEffects Effects { get; set; }
        
        public GameObject(Vector2 position, Color color, float rotation, Vector2 scale, Vector2 origin, SpriteEffects effects = SpriteEffects.None, float layerDepth = 0)
        {
            Position = position;
            Color = color;
            Rotation = rotation;
            Scale = scale;
            Origin = origin;
            LayerDepth = layerDepth;
            Effects = effects;
        }
    }
}
