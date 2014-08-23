using Otter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bones
{
    public class Player : StateEntity
    {
        public static Player Instance;


        BasicMovement Movement = new BasicMovement(200, 200, 10);
        Spritemap<string> Sprite;

        public Player(int x, int y)
            : base(new Otter.Vector2(x, y))
        {
            Sprite = AddGraphic(SpriteData.GetAnimation("soldier"));

            AddCollider(new BoxCollider(16, 16, (int)Tags.Actor));
            Collider.OriginX = 8;
            Collider.OriginY = 16;
            
            AddComponent(Movement);
            Movement.Collider = Collider;
            Movement.Axis = Controls.Axis;
            Movement.AddCollision((int)Tags.Solid);
            SetState(Normal);
        }

        private IEnumerator Normal()
        {
            while (true)
            {
                
                if (Math.Abs(Controls.Axis.X) > 0 || Math.Abs(Controls.Axis.Y) > 0)
                {
                    Sprite.Play("run");
                    Sprite.ScaleX = Controls.Axis.X < 0 ? -1 : 1;
                }
                else
                {
                    Sprite.Play("idle");
                }

                yield return 0;
            }
        }

        public override void Render()
        {
            Collider.Render();
        }
    }
}
