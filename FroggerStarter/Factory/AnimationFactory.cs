using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FroggerStarter.Enums;
using FroggerStarter.View.Dying_Animation;
using FroggerStarter.View.Sprites;

namespace FroggerStarter.Factory
{
    class AnimationFactory
    {

        public static List<BaseSprite> CreateAnimationSprites(AnimationType animationType)
        {
            switch (animationType)
            {
                case AnimationType.Death:
                    var deathAnimation = new List<BaseSprite>();
                    deathAnimation.Add(new FrameOne());
                    deathAnimation.Add(new FrameTwo());
                    deathAnimation.Add(new FrameThree());
                    deathAnimation.Add(new FrameFour());
                    return deathAnimation;

                default:
                    throw new ArgumentOutOfRangeException(nameof(animationType), animationType, null);
            }
        }
    }
}
