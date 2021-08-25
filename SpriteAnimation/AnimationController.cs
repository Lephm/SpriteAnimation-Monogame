using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpriteAnimation
{
    public class AnimationController
    {
        private bool _isCurrentlyInAnimation = false;

        public bool IsCurrentlyInAnimation
        {
            get
            {
                return _isCurrentlyInAnimation;
            }
        }
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
        //This is where the user can fill out the logic for transitioning between animation
        public virtual void UpdateAnimation(GameTime gameTime)
        {
            if (_currentAnimation != null)
            {
                _currentSprite = _currentAnimation.CurrentSprite;
                _currentAnimation.Update(gameTime);
            }
        }

        //It is okay to call this multiple time in an update
        //Call this where you want to setNewAnimation
        public void SetNewAnimation(Animation newAnimation, bool skip = false)
        {
            if (!skip && _isCurrentlyInAnimation)
            {
                return;
            }

            Animation prevAnimation = _currentAnimation;
            //Unsubcribe to old animation event
            if (_currentAnimation != null)
            {
                _currentAnimation.onStartEvent -= this.OnStartAnimation;
                _currentAnimation.onEndEvent -= this.OnEndAnimation;
            }


            _currentAnimation = newAnimation.Play();
            if (prevAnimation != newAnimation)
            {
                OnChangeAnimation(prevAnimation, newAnimation);
            }
            //Subcribe to new animation event
            _currentAnimation.onStartEvent += this.OnStartAnimation;
            _currentAnimation.onEndEvent += this.OnEndAnimation;

        }


        //Get called everytime the animation is played
        public virtual void OnStartAnimation()
        {
            _isCurrentlyInAnimation = true;
        }

        public virtual void OnEndAnimation()
        {
            _isCurrentlyInAnimation = false;
        }

        public virtual void OnChangeAnimation(Animation prevAnimation, Animation newAnimation)
        {
            //This set the _currentSprite to the first array in the new animation
            _currentSprite = CurrentAnimation.CurrentSprite;
        }


    }
}