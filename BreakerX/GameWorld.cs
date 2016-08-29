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
    class GameWorld
    {
        public AssetLoader mAssetLoader;
        public List<Entity> mDestoryList;

        private List<Entity> mEntityList;
        private List<Entity> mBlockList;
        private List<UIElement> mUIList;
        private Texture2D mBackground;
		private UIElement mGameOverImage;

		public int mScore;
		public int mLives;
        public int mMultiplier;

		public bool bIsGameOver = false;

        public Ball mMainBall;
        public Paddle mMainPaddle;
        public UIElement X;

        public GameWorld(AssetLoader pAssetLoader)
        {
            mAssetLoader = pAssetLoader;
            mBackground = mAssetLoader.GetSprite("Background");

            mEntityList = new List<Entity>();
            mBlockList = new List<Entity>();
            mDestoryList = new List<Entity>();
            mUIList = new List<UIElement>();
			mGameOverImage = new UIElement(new Vector2(360, 1000), mAssetLoader.GetSprite("GameOver"), null);
			mUIList.Add(mGameOverImage);
            mMainBall = new Ball(this);
            mMainPaddle = new Paddle(this);
            mEntityList.Add(mMainPaddle);
            mBlockList.Add(mMainPaddle);
            mEntityList.Add(mMainBall);

            mScore = 0;
            mLives = 3;
            mMultiplier = 1;

            Animator xAnim = new Animator(mAssetLoader.GetSprite("XAnimation"), new Vector2(640, 1280), 160, 164, 10);
            X = new UIElement(new Vector2(500, 500), null, xAnim);
            mUIList.Add(X);

            CreateMap();
        }

        public void CreateMap()
        {
            Color[] rainbow = { Color.Red, Color.Orange, Color.Yellow, Color.Green, Color.Blue, Color.Purple };
            int colorCount = 0;
            for (int x = 100; x < 700; x += 100)
            {
                for (int i = 0; i < 1440; i += 160)
                {
                    if (i == 160 || i == 1120)
                    {
                        MultiBlock newBlock = new MultiBlock(this, new Vector2(i, x), rainbow[colorCount]);
                        mBlockList.Add(newBlock);
                        mEntityList.Add(newBlock);
                    }
                    else
                    {
                        Block newBlock = new Block(this, new Vector2(i, x), rainbow[colorCount]);
                        mBlockList.Add(newBlock);
                        mEntityList.Add(newBlock);
                    }
                }
                colorCount++;
            }
        }

        public void UpdateGameWorld(GameTime gameTime)
        {
			if(bIsGameOver)
			{
				//Check if the player has pressed the new game button.
				TouchCollection mTouchCollection = TouchPanel.GetState();
				if(mTouchCollection.Count > 0)
				{
					//Reset the game if the button was pressed
					if(mTouchCollection[0].Position.X >= 437 && mTouchCollection[0].Position.X <= 984 && mTouchCollection[0].Position.Y >= 1145 && mTouchCollection[0].Position.Y <= 1223)
					{
						ResetGame();
					}
				}
			}

            foreach(UIElement u in mUIList)
            {
                u.Update(gameTime);
            }

            foreach(Entity e in mEntityList)
            {
                e.Update(gameTime);
            }

            mMainBall.CheckCollisions(mBlockList);

            foreach(Entity e in mDestoryList)
            {
                mBlockList.Remove(e);
                mEntityList.Remove(e);
            }

            mDestoryList.Clear();
        }

        public void DrawWorld(SpriteBatch pSpriteBatch)
        {
            pSpriteBatch.Draw(mBackground, Vector2.Zero, Color.White);

            foreach(Entity e in mEntityList)
            {
                e.Draw(pSpriteBatch);
            }

            foreach (UIElement u in mUIList)
            {
                u.Draw(pSpriteBatch);
            }

            pSpriteBatch.DrawString(mAssetLoader.GetFont(), "Score: " + mScore, new Vector2(100, 0), Color.White);
            pSpriteBatch.DrawString(mAssetLoader.GetFont(), "Lives: " + mLives, new Vector2(1000, 0), Color.White);

            if(!X.mHidden)
                pSpriteBatch.DrawString(mAssetLoader.mMultiFont, mMultiplier.ToString(), new Vector2(820, 1280), Color.White);
        }

		public void ResetGame()
		{
			bIsGameOver = false;
			mScore = 0;
			mLives = 3;
			mMultiplier = 1;
			mMainPaddle.isLaunchMode = true;
			mMainPaddle.ResetPaddle();
			mGameOverImage.mHidden = true;
			mMainPaddle.bAcceptInput = true;
			mBlockList.Clear();
			mEntityList.Clear();
			mEntityList.Add(mMainBall);
			mEntityList.Add(mMainPaddle);
			mBlockList.Add(mMainPaddle);
			CreateMap();
		}

        public void IncreaseScore(int pAmt)
        {
            mScore += pAmt * mMultiplier;
        }

        public void ResetMultiplier()
        {
            mMultiplier = 1;
        }

        public void PlayMultiplierAnim()
        {
            X.mHidden = false;
        }

		public bool CheckGameOver()
		{
			if(mLives == 0)
			{
				mGameOverImage.mHidden = false;
				bIsGameOver = true;
				return true;
			}

			return false;
		}
    }
}