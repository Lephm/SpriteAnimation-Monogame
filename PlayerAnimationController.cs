using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpriteAnimation
{

    public class PlayerAnimationController : AnimationController
    {
        public Animation idleAnimation;
        public Animation attackAnimation;

        public Animation walkAnimation;
        public Animation deadAnimation;

        public bool isDead = false;

        public PlayerAnimationController(Texture2D[] idleSprites, Texture2D[] attackSprites, Texture2D[] walkSprites, Texture2D[] deadSprites)
        {
            idleAnimation = CreateAnimation(idleSprites, 0.25f);
            attackAnimation = CreateAnimation(attackSprites, 0.5f, false);
            walkAnimation = CreateAnimation(walkSprites);
            deadAnimation = CreateAnimation(deadSprites, 1, false);

        }

        public override void UpdateAnimation(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.D))
            {
                isDead = true;
            }
            if (!isDead)
            {
                if (keyboardState.IsKeyDown(Keys.Right))
                {
                    SetNewAnimation(walkAnimation, true);
                }
                else if (keyboardState.IsKeyUp(Keys.Right))
                {
                    SetNewAnimation(idleAnimation);
                }
                if (keyboardState.IsKeyDown(Keys.Space))
                {
                    SetNewAnimation(attackAnimation, true);
                }

            }

            else
            {
                SetNewAnimation(deadAnimation, true);
            }

            base.UpdateAnimation(gameTime);
        }

        public override void OnEndAnimation()
        {
            base.OnEndAnimation();
            if (CurrentAnimation == attackAnimation)
            {
                SetNewAnimation(idleAnimation);
            }
        }
    }
}