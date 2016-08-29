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
    class Ball : Entity
    {
        private Vector2 mVelocity;
        private int mDamage;

        public Ball(GameWorld pGameWorld) : base(pGameWorld.mAssetLoader.GetSprite("Ball"), new Vector2(200, 2370), pGameWorld)
        {
            mVelocity = Vector2.Zero;
            mDamage = 40;
        }

        public override void Update(GameTime gameTime)
        {
            //Move ball based on velocity
            mPosition.X += mVelocity.X * gameTime.ElapsedGameTime.Milliseconds;
            mPosition.Y += mVelocity.Y * gameTime.ElapsedGameTime.Milliseconds;

            //If ball hits boundry, reverse the coresponding velocity
            if(mPosition.X >= 1400 || mPosition.X <= 0)
            {
                mVelocity.X = -mVelocity.X;
            }
            else if(mPosition.Y <= 75)
            {
                mVelocity.Y = -mVelocity.Y;
            }
            else if(mPosition.Y >= 2520)
            {
				//Lose Condition
				mGameWorld.mLives--;

				if (!mGameWorld.CheckGameOver())
				{
					mGameWorld.mMainPaddle.isLaunchMode = true;
					mVelocity = Vector2.Zero;
					mGameWorld.mMultiplier = 1;
					mGameWorld.mMainPaddle.ResetPaddle();
				}
				else
				{
					mPosition.Y = -100;
					mVelocity = Vector2.Zero;
					mGameWorld.mMainPaddle.bAcceptInput = false;
				}
            }
            base.Update(gameTime);
        }

        public void UpdateBallPosition(Vector2 pPosition)
        {
            mPosition = pPosition;
        }

        public void LaunchBall(Vector2 pVelocity)
        {
            mVelocity = pVelocity;
        }

        public void CheckCollisions(List<Entity> pBlockList)
        {
            bool isCollided = false;

            foreach (Entity e in pBlockList)
            {
                if (Vector2.Distance(GetCenter(), e.GetCenter()) < 350 && !isCollided)
                {
                    Vector2 distance = new Vector2(GetCenter().X - e.GetCenter().X, GetCenter().Y - e.GetCenter().Y);
                    Vector2 clampVector = new Vector2(e.GetCenter().X + distance.X, e.GetCenter().Y + distance.Y);
                    clampVector = Vector2.Clamp(clampVector, new Vector2(e.mCollisionBox.X, e.mCollisionBox.Y), new Vector2(e.mCollisionBox.X + e.mCollisionBox.Width, e.mCollisionBox.Y + e.mCollisionBox.Height));
                    if (Vector2.Distance(clampVector, GetCenter()) < 20)
                    {
						if (clampVector.Y == e.mCollisionBox.Y || clampVector.Y == e.mCollisionBox.Y + e.mCollisionBox.Height)
						{
							mVelocity.Y = -mVelocity.Y;
                        }
                        if (clampVector.X == e.mCollisionBox.X || clampVector.X == e.mCollisionBox.X + e.mCollisionBox.Width)
                        {
							mVelocity.X = -mVelocity.X;
                        }

						if (e.GetType() != typeof(Paddle))
						{ 
							((Block)e).ApplyDamage(mDamage);
						}
                        isCollided = true;
                    }
                }
            }
        }

        public override Vector2 GetCenter()
        {
            mCenter.X = (int)mPosition.X + 20;
            mCenter.Y = (int)mPosition.Y + 20;
            return mCenter;
        }
    }
}