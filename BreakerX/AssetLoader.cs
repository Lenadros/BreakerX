using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace BreakerX
{
    class AssetLoader
    {
        private ContentManager mContentManager;
        private static Dictionary<string, Texture2D> mSpriteList;

        public SpriteFont mFont;
        public SpriteFont mMultiFont;

        public AssetLoader(ContentManager pCM)
        {
            mContentManager = pCM;

            mSpriteList = new Dictionary<string, Texture2D>();

            LoadAssets();
        }

        public void LoadAssets()
        {
            mSpriteList.Add("Paddle", mContentManager.Load<Texture2D>("Paddle"));
            mSpriteList.Add("Ball", mContentManager.Load<Texture2D>("Ball"));
            mSpriteList.Add("Block", mContentManager.Load<Texture2D>("Block"));
            mSpriteList.Add("BlockAnimation", mContentManager.Load<Texture2D>("BlockAnimation"));
            mSpriteList.Add("Background", mContentManager.Load<Texture2D>("Background"));
            mSpriteList.Add("MultiBlock", mContentManager.Load<Texture2D>("MultiBlock"));
            mSpriteList.Add("XAnimation", mContentManager.Load<Texture2D>("XAnimation"));
			mSpriteList.Add("GameOver", mContentManager.Load<Texture2D>("GameOver"));

            mFont = mContentManager.Load<SpriteFont>("Courier New");
            mMultiFont = mContentManager.Load<SpriteFont>("Sans");
        }

        public Texture2D GetSprite(string pName)
        {
            return mSpriteList[pName];
        }

        public SpriteFont GetFont()
        {
            return mFont;
        }
    }
}