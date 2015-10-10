using GameLibrary;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace SpaceDefender.GameComponents
{
    public class Explosion : AnimatedSpriteNew
    {
        public Explosion(Vector2 centerPosition)
            //: base("Explosions", centerPosition, 8, 16, 1, 20)
            : base("Explosions", centerPosition, "Explosion3", 20)
        {
        }

        public override void LoadContent(ContentManager content)
        {
            //Sprite = new Sprite(new TextureAtlas(content.Load<Texture2D>(AssetName), Rows, Columns) { Frame = Row * Columns })
            //{
            //    Scale = new Vector2(1.5f, 1.5f) * new Vector2(GameRoot.ScreenSize.X / 1280.0f, GameRoot.ScreenSize.Y / 720.0f),
            //    OriginNormalized = new Vector2(0.5f, 0.5f)
            //};

            base.LoadContent(content);
            Sprite.Scale = new Vector2(1.5f, 1.5f) * new Vector2(GameRoot.ScreenSize.X / 1280.0f, GameRoot.ScreenSize.Y / 720.0f);
        }
    }
}