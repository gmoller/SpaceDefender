using CollisionDetectionLibrary.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefender
{
    public interface IDrawableGameComponent
    {
        bool IsAlive { get; set; }
        Vector2 CenterPosition { get; set; }
        Circle BoundingCircle { get; }
        void LoadContent(ContentManager content);
        void Update(GameTime gameTime, InputState inputState);
        void Draw(SpriteBatch spriteBatch);
    }
}