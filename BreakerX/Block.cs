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
    class Block : Entity
    {
        private Rectangle mDrawRect;
        private int mHealth;
        private Color mBlockColor;
        private Animator mAnimatior;
        private bool bPlayAnimation;

        public Block(GameWorld pGameWorld, Vector2 pPosition, Color pColor) : base(pGameWorld.mAssetLoader.GetSprite("Block"), pPosition, pGameWorld)
        {
            mDrawRect = new Rectangle(0, 0, 160, 50);
            mCollisionBox = new Rectangle((int)mPosition.X, (int)mPosition.Y, 160, 50);
            mAnimatior = new Animator(pGameWorld.mAssetLoader.GetSprite("BlockAnimation"), mPosition, 160, 50, 5);
            mHealth = 100;
            mBlockColor = pColor;
            bPlayAnimation = false;
        }

        public override void Update(GameTime gameTime)
        {
            if (bPlayAnimation)
            {
                if (mAnimatior.Update(gameTime))
                {
                    Destroy();
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch pSpriteBatch)
        {
            if (bPlayAnimation)
            {
                mAnimatior.Draw(pSpriteBatch);
            }
            else
            {
                pSpriteBatch.Draw(mSprite, mPosition, mDrawRect, mBlockColor, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
            }
        }

        public virtual void Destroy()
        {
            mGameWorld.mDestoryList.Add(this);
        }

        public void ApplyDamage(int pDamage)
        {
            mHealth -= pDamage;
            mDrawRect.X += 160;
            if (mHealth <= 0)
            {
                bPlayAnimation = true;
                mGameWorld.IncreaseScore(100);
            }
            else
            {
                mGameWorld.IncreaseScore(10);
            }
        }
    }
}