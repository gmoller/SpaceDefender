using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameLibrary
{
    public class TextureAtlas
    {
        private readonly Texture2D _texture;
        private readonly Rectangle[] _frameRectangles;

        public Texture2D Texture { get { return _texture; } }
        public int Frame { get; set; }

        public int SingleTextureWidth
        {
            get
            {
                return _frameRectangles[Frame].Width;
            }
        }

        public int SingleTextureHeight
        {
            get
            {
                return _frameRectangles[Frame].Height;
            }
        }

        public Rectangle FrameRectangle
        {
            get
            {
                return _frameRectangles[Frame];
            }
        }

        public TextureAtlas(Texture2D texture, int rows = 1, int columns = 1)
        {
            _texture = texture;
            int singleTextureWidth = texture.Width / columns;
            int singleTextureHeight = texture.Height / rows;
            Frame = 0;

            _frameRectangles = new Rectangle[rows * columns];
            int frame = 0;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    int x = singleTextureWidth * j;
                    int y = singleTextureHeight * i;
                    _frameRectangles[frame] = new Rectangle(x, y, singleTextureWidth, singleTextureHeight);
                    frame++;
                }
            }
        }

        public TextureAtlas(Texture2D texture, params Rectangle[] frameRectangles)
        {
            _texture = texture;
            _frameRectangles = frameRectangles;
            Frame = 0;
        }
    }
}