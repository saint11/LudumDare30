using Otter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bones
{
    public class Beam:Entity
    {
        private Spritemap<string> Sprite;
        private List<Entity> HaveHit;
        private float Angle;
        public Beam(float x, float y, float angle, Entity owner)
        {
            Angle = angle;
            SetPosition(x, y);

            Sprite = AddGraphic(SpriteData.GetAnimation("beam"));
            Sprite.ScaleX = 2;
            Sprite.Play("attack");
            Sprite.Angle = angle;
            
            var vec = new Vector2(Sprite.ScaledWidth,0);
            vec =Util.Rotate(vec,angle);
            AddCollider(new LineCollider(0, 0, vec.X, vec.Y));
            Layer = owner.Layer - 100;

            LifeSpan = 30;
            HaveHit = new List<Entity>();
        }

        public override void Update()
        {
            Sprite.Alpha = 2 - (Timer / LifeSpan) * 2;

            List<Entity> col = CollideEntities(X, Y, (int)Tags.Actor);
            foreach (var c in col)
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
