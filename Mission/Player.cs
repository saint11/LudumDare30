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
        private float Side;
        private float Angle = 0;

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
                    Side = Sprite.ScaleX = Controls.Axis.X < 0 ? -1 : 1;
                    Angle = (float)(Math.Atan2(-Controls.Axis.Y, Controls.Axis.X) * Util.RAD_TO_DEG);
                }
                else
                {
                    Sprite.Play("idle");
                }

                if (Controls.Attack.Pressed) SetState(Attacking);
                yield return 0;
            }
        }

        private IEnumerator Attacking()
        {
            Movement.Freeze = true;
            Sprite.Play("attack");
            Scene.Add(new Slash(X, Y-16, Angle, 0.8f, -0.8f*Side));
            yield return 8;
            Movement.Freeze = false;
            yield return SetState(Normal);
        }

        public override void Render()
        {
            //Collider.Render();
        }
    }
}
