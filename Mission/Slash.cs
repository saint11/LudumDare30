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
        private List<Entity> HaveHit;
        private Vector2 Speed;
        private float Angle;

        public Slash(float x, float y, float angle,float sizeX,float sizeY)
            :base(x,y)
        {
            Angle = angle;
            Sprite = AddGraphic(SpriteData.GetAnimation("slash"));
            Sprite.Angle = angle;
            Sprite.Play("attack");
            Sprite.ScaleX = sizeX;// *0.5f;
            Sprite.ScaleY = sizeY;// *0.5f;
            //Tween(Sprite, new { ScaleX = sizeX, ScaleY = sizeY },12).Ease(Ease.BackOut);

            Speed = new Vector2((float)Math.Cos(angle * Util.DEG_TO_RAD), -(float)Math.Sin(angle * Util.DEG_TO_RAD));
            AddCollider(new BoxCollider(
                (int)(Math.Abs(Sprite.ScaledWidth*1f)),
                (int)(Math.Abs(Sprite.ScaledHeight*1f)), (int)Tags.FX));
            Collider.CenterOrigin();
            Collider.X += Speed.X * 8;
            Collider.Y += Speed.Y * 8;
            X += Speed.X*4;
            Y += Speed.Y*4;
            LifeSpan = 12;

            HaveHit = new List<Entity>();
        }

        public override void Update()
        {
            X += Speed.X*2;
            Y += Speed.Y*2;

            Layer = -(int)Y - (int)(Math.Abs(Sprite.ScaledHeight));

            List<Entity> col = CollideEntities(X,Y,(int)Tags.Actor);
            foreach(var c in col)
            {
                if (c is Enemy && !HaveHit.Contains(c))
                {
                    Enemy e = c as Enemy;
                    e.Damage(1, Angle);
                    HaveHit.Add(e);
                }
            }
        }

        public override void Render()
        {
            //Collider.Render();
        }
    }
}
