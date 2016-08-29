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
    class MultiBlock : Block
    {
        public MultiBlock(GameWorld pGameWorld, Vector2 pPosition, Color pColor) : base(pGameWorld, pPosition, pColor)
        {
            mSprite = mGameWorld.mAssetLoader.GetSprite("MultiBlock");
        }

        public override void Destroy()
        {
			if(mGameWorld.mMultiplier < 10)
			{
				mGameWorld.mMainPaddle.MovePaddleUp();
				mGameWorld.mMultiplier++;
				mGameWorld.PlayMultiplierAnim();
			}

            base.Destroy();
        }
    }
}