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
    class Animator
    {
        private Rectangle mDrawRectangle;
        private Texture2D mSpriteSheet;
        private Vector2 mPosition;
        private int mNumFrames;
        private double mTimeCounter;
        private double mFrameRate;
        private int mFrameCounter;

        public Animator(Texture2D pSpriteSheet, Vector2 pPosition, int pFrameWidth, int pFrameHeight, int pNumFrames)
        {
            mSpriteSheet = pSpriteSheet;
            mPosition = pPosition;
            mDrawRectangle = new Rectangle(0, 0, pFrameWidth, pFrameHeight);
            mNumFrames = pNumFrames;
            mTimeCounter = 0;
            mFrameCounter = 0;
            mFrameRate = 0.05f;
        }

        public bool Update(GameTime gameTime)
        {
            mTimeCounter += gameTime.ElapsedGameTime.TotalSeconds;
            
            if(mTimeCounter > mFrameRate && mFrameCounter < mNumFrames)
            {
                mTimeCounter = 0;
                mDrawRectangle.X += mDrawRectangle.Width;
                mFrameCounter++;
            }

            if (mFrameCounter >= mNumFrames)
            {
                return true;
            }

            return false;
        }

        public void Draw(SpriteBatch pSpriteBatch)
        {
            pSpriteBatch.Draw(mSpriteSheet, mPosition, mDrawRectangle, Color.White, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
        }

        public void Reset()
        {
            mFrameCounter = 0;
            mDrawRectangle.X = 0;
        }
    }
}