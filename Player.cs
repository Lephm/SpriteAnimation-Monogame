using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
namespace SpriteAnimation
{

    public class Player
    {
        public PlayerAnimationController playerAnimationController;
        public void LoadContent(Microsoft.Xna.Framework.Content.ContentManager content)
        {
            //Load Attack Animation
            List<Texture2D> attackSprites = new List<Texture2D>();
            for (int i = 0; i <= 5; i++)
            {
                attackSprites.Add(content.Load<Texture2D>("Attack/HeroKnight_Attack1_" + i.ToString()));
            }
            //Load Dead Animation
            List<Texture2D> deadSprites = new List<Texture2D>();
            for (int i = 1; i <= 9; i++)
            {
                deadSprites.Add(content.Load<Texture2D>("Dead/HeroKnight_Death_" + i.ToString()));
            }
            //Load Idle Animation
            List<Texture2D> idleSprites = new List<Texture2D>();
            for (int i = 0; i <= 7; i++)
            {
                idleSprites.Add(content.Load<Texture2D>("Idle/HeroKnight_Idle_" + i.ToString()));
            }

            //Load Walk Animation
            List<Texture2D> walkSprites = new List<Texture2D>();
            for (int i = 0; i <= 9; i++)
            {
                walkSprites.Add(content.Load<Texture2D>("Walk/HeroKnight_Run_" + i.ToString()));
            }

            //Initialize playerAnimation Controller
            playerAnimationController = new PlayerAnimationController(idleSprites.ToArray(), attackSprites.ToArray(), walkSprites.ToArray(), deadSprites.ToArray());
        }

        public void Update(GameTime gameTime)
        {
            playerAnimationController.UpdateAnimation(gameTime);
        }
    }
}