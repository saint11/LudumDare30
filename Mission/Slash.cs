using Otter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bones
{
    public class Slash : Entity
    {
        private Spritemap<string> Sprite;
        private Vector2 Speed;

        public Slash(float x, float y, float angle,float sizeX,float sizeY)
            :base(x,y)
        {
            Sprite = AddGraphic(SpriteData.GetAnimation("slash"));
            Sprite.Angle = angle;
            Sprite.Play("attack");
            Sprite.ScaleX = sizeX * 0.5f;
            Sprite.ScaleY = sizeY * 0.5f;
            Tween(Sprite, new { ScaleX = sizeX, ScaleY = sizeY },12).Ease(Ease.BackOut);

            Speed = new Vector2((float)Math.Cos(angle * Util.DEG_TO_RAD), -(float)Math.Sin(angle * Util.DEG_TO_RAD));

            LifeSpan = 12;
        }

        public override void Update()
        {
            X += Speed.X*2;
            Y += Speed.Y*2;
        }
    }
}
