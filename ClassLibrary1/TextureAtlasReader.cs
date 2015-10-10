using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameLibrary
{
    public class TextureAtlasReader
    {
        public TextureAtlas Read(Texture2D texture)
        {
            var doc = new XmlDocument();
            string s = string.Format(@"Content\{0}.xml", texture.Name);

            doc.Load(s);

            var frameRectangles = new List<Rectangle>();

            if (doc.DocumentElement != null)
            {
                XmlNode node = doc.DocumentElement.SelectSingleNode("/SpriteSheet/Frames");
                if (node != null)
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        if (childNode.Attributes != null)
                        {
                            int x = Convert.ToInt32(childNode.Attributes["x"].InnerText);
                            int y = Convert.ToInt32(childNode.Attributes["y"].InnerText);
                            int width = Convert.ToInt32(childNode.Attributes["width"].InnerText);
                            int height = Convert.ToInt32(childNode.Attributes["height"].InnerText);

                            var rectangle = new Rectangle(x, y, width, height);
                            frameRectangles.Add(rectangle);
                        }
                    }
                }
            }

            var atlas = new TextureAtlas(texture, frameRectangles.ToArray());

            return atlas;
        }
    }
}