using System;
using System.Collections.Generic;
using System.Xml;

namespace GameLibrary
{
    public class AnimationSequenceReader
    {
        public Dictionary<string, AnimationSequence> Read(string name)
        {
            var doc = new XmlDocument();
            string s = string.Format(@"Content\{0}.xml", name);

            doc.Load(s);

            var dict = new Dictionary<string, AnimationSequence>();

            if (doc.DocumentElement != null)
            {
                XmlNode node = doc.DocumentElement.SelectSingleNode("/SpriteSheet/AnimationSequences");
                if (node != null)
                {
                    foreach (XmlNode childNode in node.ChildNodes)
                    {
                        if (childNode.Attributes != null)
                        {
                            string id = childNode.Attributes["id"].InnerText;
                            string frames = childNode.Attributes["frames"].InnerText;

                            var animationSequence = new AnimationSequence(id, GetFrames(frames));
                            dict.Add(id, animationSequence);
                        }
                    }
                }
            }

            return dict;
        }

        private List<int> GetFrames(string frames)
        {
            var list = new List<int>();

            if (frames.Contains("-"))
            {
                string[] split = frames.Split('-');
                int firstFrame = Convert.ToInt32(split[0]);
                int lastFrame = Convert.ToInt32(split[1]);
                list.Add(firstFrame);

                for (int i = firstFrame + 1; i < lastFrame; i++)
                {
                    list.Add(i);
                }

                list.Add(lastFrame);
            }
            else
            {
                string[] split = frames.Split(',');
                foreach (string frame in split)
                {
                    list.Add(Convert.ToInt32(frame));
                }
            }

            return list;
        }
    }
}