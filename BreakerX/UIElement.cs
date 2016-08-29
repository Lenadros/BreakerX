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
    class UIElement 
    {
        private Vector2 mPosition;
        private Texture2D mUISprite;
        private Animator mAnimator;

        public bool mHidden;

        public UIElement(Vector2 pPosition, Texture2D pSprite, Animator pAnimator)
        {
            mPosition = pPosition;
            mUISprite = pSprite;
            mAnimator = pAnimator;
            mHidden = true;
        }

        public void Update(GameTime gameTime)
        {
            if(mAnimator != null && !mHidden)
            {
                if (mAnimator.Update(gameTime))
                {
                    mAnimator.Reset();
                    mHidden = true;
                }
            }
        }

        public void Draw(SpriteBatch pSpriteBatch)
        {
            if(mAnimator != null && !mHidden)
            {
                mAnimator.Draw(pSpriteBatch);
            }
            else if(!mHidden)
            {
                pSpriteBatch.Draw(mUISprite, mPosition, Color.White);
            }
        }
    }
}