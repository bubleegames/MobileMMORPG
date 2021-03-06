﻿using Assets.Code.AssetHandling.Sprites.Animations;
using Assets.Code.Game.Entities;
using MapHandler;

namespace Assets.Code.AssetHandling
{
    public class AttackAnimation : AnimationBase
    {

        public static readonly int HIT_FRAME = 8;

        public AttackAnimation(SpriteSheet sheet) : base(sheet) {
            this.AnimationTimeInSeconds = 0.06f;
        }

        public override AnimationResult Loop(Direction dir)
        {
            float offset = 0;

            CurrentFrame++;

            var spriteFrame = 2;
            var sriteDir = dir;

            // preparing (going back)
            if (CurrentFrame < 5)
            {
                offset = 0.005f * CurrentFrame;
                spriteFrame = 2;
            }
            // iddle
            else if (CurrentFrame < 6)
            { 
                offset = 0.02f;
                spriteFrame = 2;
            }
            // going forward
            else if (CurrentFrame < 9) // 6 - 7 - 8
            {
                if(CurrentFrame==6)
                    offset = -0.00f;
                else if(CurrentFrame == 7)
                    offset = -0.02f;
                 else
                    offset = -0.04f;
                spriteFrame = 8 - CurrentFrame;
            }
            else if(CurrentFrame < 15)
            {
                spriteFrame = 0;
                offset = -0.02f;
                if (CurrentFrame == 14)
                    offset = -0.01f;
            } else
            {
                Reset();
                IsOver = true;
                spriteFrame = 0;
            }

            var offsetX = 0f;
            var offsetY = 0f;

            if (dir == Direction.LEFT)
                offsetX = offset;
            else if (dir == Direction.RIGHT)
                offsetX = -offset;
            else if (dir == Direction.SOUTH)
                offsetY = -offset;
            else if (dir == Direction.NORTH)
                offsetY = offset;

            return new AnimationResult()
            {
                Sprite = this.SpriteSheet.GetSheet(sriteDir)[spriteFrame],
                OffsetX = offsetX,
                OffsetY = offsetY
            };
        }
    }
}
