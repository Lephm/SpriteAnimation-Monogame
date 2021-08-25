using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpriteAnimation
{
    public class AnimationController
    {
        public bool isCurrentlyInAnimation = false;
        private Texture2D _currentSprite;

        public Texture2D CurrentSprite
        {
            get
            {
                return _currentSprite;
            }
        }

        private Animation _currentAnimation;

        public Animation CurrentAnimation
        {
            get
            {
                return _currentAnimation;
            }
        }

        public Animation CreateAnimation(Texture2D[] sprites, float speed = 1, bool loop = true)
        {
            return new Animation(this, sprites, speed, loop);
        }

        //Should be call every update to handle animation transition logic
        //This is where the logic for how user can fill out there logic for transitioning between animation
        public virtual void UpdateAnimation(GameTime gameTime)
        {
            if (_currentAnimation != null)
            {
                _currentAnimation.Update(gameTime);
            }
        }

        //It is okay to call this multiple time in an update
        //Call this where you want to setNewAnimation
        public void SetNewAnimation(Animation newAnimation, bool skip = false)
        {
            if (!skip && isCurrentlyInAnimation)
            {
                return;
            }
            //Unsubcribe to old animation event
            if (_currentAnimation != null)
            {
                _currentAnimation.onStartEvent -= this.OnStartAnimation;
                _currentAnimation.onEndEvent -= this.OnEndAnimation;
            }

            _currentAnimation = newAnimation.Play();
            //Subcribe to new animation event
            _currentAnimation.onStartEvent += this.OnStartAnimation;
            _currentAnimation.onEndEvent += this.OnEndAnimation;
            _currentSprite = _currentAnimation.CurrentSprite;
        }

        public virtual void OnStartAnimation()
        {
            isCurrentlyInAnimation = true;
        }

        public virtual void OnEndAnimation()
        {
            isCurrentlyInAnimation = false;
        }


    }
}