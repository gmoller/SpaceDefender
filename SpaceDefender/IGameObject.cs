﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceDefender
{
    internal interface IGameObject
    {
        //void Update(GameTime gameTime);
        void Draw(SpriteBatch spriteBatch);
    }
}