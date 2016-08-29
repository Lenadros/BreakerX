using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace BreakerX
{
    class Paddle : Entity
    {
        private TouchCollection mTouchCollection;

        public float mSpeed;
        public bool isLaunchMode;
        public bool bMovePaddle;
		public bool bAcceptInput;
        public float mNewYPos;

		public const int EXTRA_BOUNDS = 100;

        public Paddle(GameWorld pGameWorld) : base(pGameWorld.mAssetLoader.GetSprite("Paddle"), new Vector2(720, 2400), pGameWorld)
        {
            mSpeed = 0.6f;
            mCollisionBox = new Rectangle((int)mPosition.X, (int)mPosition.Y, 300, 50);
            isLaunchMode = true;
            bMovePaddle = false;
			bAcceptInput = true;
        }

        public override void Update(GameTime gameTime)
        {
            mTouchCollection = TouchPanel.GetState();

            if (isLaunchMode)
            {
                mGameWorld.mMainBall.UpdateBallPosition(new Vector2(mPosition.X + 130, mPosition.Y - 40));
            }

            if (bAcceptInput && mTouchCollection.Count > 0)
            {
                if (isLaunchMode && mTouchCollection[0].Position.X >= mCollisionBox.X - EXTRA_BOUNDS && mTouchCollection[0].Position.X <= mCollisionBox.X + mCollisionBox.Width + EXTRA_BOUNDS &&
                mTouchCollection[0].Position.Y >= mCollisionBox.Y - EXTRA_BOUNDS && mTouchCollection[0].Position.Y <= mCollisionBox.Y + mCollisionBox.Height + EXTRA_BOUNDS)
                {
                    mGameWorld.mMainBall.LaunchBall(new Vector2(mSpeed, -mSpeed));
                    isLaunchMode = false;
                }
                else if (mTouchCollection[0].Position.X > 720)
                {
                    mPosition.X += mSpeed * (float)gameTime.ElapsedGameTime.Milliseconds;
                    if (mPosition.X >= 1140)
                        mPosition.X = 1140;
                }
                else if(mTouchCollection[0].Position.X < 720)
                {
                    mPosition.X -= mSpeed * (float)gameTime.ElapsedGameTime.Milliseconds;
                    if (mPosition.X <= 0)
                        mPosition.X = 0;
                }
            }

            mCollisionBox.X = (int)mPosition.X;
            mCollisionBox.Y = (int)mPosition.Y;

            if(bMovePaddle)
            {
                mPosition.Y = MathHelper.Lerp(mPosition.Y, mNewYPos, 0.1f);
                if (mPosition.Y == mNewYPos)
                    bMovePaddle = false;
            }

            base.Update(gameTime);
        }

        public void MovePaddleUp()
        {
            mNewYPos = mPosition.Y - 150;
			bMovePaddle = true;
        }

        public void ResetPaddle()
        {
            mNewYPos = 2400;
            bMovePaddle = true;
        }
    }
}