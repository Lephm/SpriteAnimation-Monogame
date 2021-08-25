using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpriteAnimation
{

    public delegate void AnimationEvent();
    public class Animation
    {
        private bool _isloop;
        private float _speed;

        private Texture2D[] _sprites;

        private AnimationController _currentAnimationController;

        private int _currentSpriteIndex = 0;

        private float _countdownTillNextSprite = 1f;

        private Texture2D _currentSprite;

        public Texture2D CurrentSprite
        {
            get
            {
                return _currentSprite;
            }
        }

        public event AnimationEvent onStartEvent;
        public event AnimationEvent onEndEvent;
        public Animation(AnimationController animController, Texture2D[] sprites, float speed = 1, bool loop = true)
        {
            _sprites = sprites;
            _speed = speed;
            _isloop = loop;
            _currentAnimationController = animController;

        }

        public Animation Play()
        {
            //Reset animation if the previous animation is not this
            //This is if statement right here is called when the Animation starts to play for the first time
            if (_currentAnimationController.CurrentAnimation != this)
            {
                HandleStartAnimation();
                System.Diagnostics.Debug.WriteLine("Start new animation");
            }

            return this;

        }

        public void Update(GameTime gameTime)
        {
            float deltatime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _countdownTillNextSprite -= 1 * _speed;
            if (_countdownTillNextSprite <= 0)
            {
                PlayNextSprite();
            }
            _currentSprite = _sprites[_currentSpriteIndex];
        }
        private void ResetAnimation()
        {
            _currentSpriteIndex = 0;
            _currentSprite = _sprites[_currentSpriteIndex];
            _countdownTillNextSprite = 1;
        }
        private void PlayNextSprite()
        {
            _countdownTillNextSprite = 1f;
            _currentSpriteIndex++;
            //loop animation or not especially when the animation end
            if (_currentSpriteIndex >= _sprites.Length)
            {
                HandleEndAnimation();
            }
        }
        private void HandleEndAnimation()
        {
            if (_isloop)
            {
                _currentSpriteIndex = 0;
            }
            else
            {
                _currentSpriteIndex = _sprites.Length - 1;
            }

            if (onEndEvent != null)
            {
                onEndEvent();
            }
        }

        private void HandleStartAnimation()
        {
            ResetAnimation();

            if (onStartEvent != null)
            {
                onStartEvent();
            }
        }


    }
}