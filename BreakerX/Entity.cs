using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace BreakerX
{
    class Entity
    {
        public Texture2D mSprite;
        public GameWorld mGameWorld;
        public Rectangle mCollisionBox;
        public Vector2 mPosition;
        public Vector2 mCenter = Vector2.Zero;

        public Entity(Texture2D pSprite, Vector2 pPosition, GameWorld pGameWorld)
        {
            if (pPosition == null)
                pPosition = new Vector2(0, 0);

            mSprite = pSprite;
            mPosition = pPosition;
            mGameWorld = pGameWorld;
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch pSpriteBatch)
        {
            pSpriteBatch.Draw(mSprite, mPosition, Color.White);
        }

        public virtual Vector2 GetCenter()
        {
            mCenter.X = (int)mPosition.X + mCollisionBox.Width / 2;
            mCenter.Y = (int)mPosition.Y + mCollisionBox.Height / 2;
            return mCenter;
        }
    }
}