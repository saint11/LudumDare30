using Otter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

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

            AddCollider(new BoxCollider(16, 12, (int)Tags.Actor));
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
                    Movement.Speed.X *= 0.8f;
                    Movement.Speed.Y *= 0.8f;
                }

                if (Controls.Attack.Pressed) SetState(Attacking);
                yield return 0;
            }
        }

        private IEnumerator Attacking()
        {
            Movement.Freeze = true;
            Sprite.Play("attack");
            yield return 2;
            Scene.Add(new Slash(X, Y-16, Angle, 0.8f, -0.8f*Side));
            yield return 8;
            Movement.Freeze = false;
            yield return SetState(Normal);
        }

        public override void Update()
        {
            Layer = -(int)Y;
        }

        public override void Prerender()
        {
            float prev = Sprite.Alpha;
            Sprite.Shader = Bones.Shader["solid"];
            Sprite.Alpha = 0.75f * prev;
            Sprite.Shader.SetParameter("color", Color.Black);
            Sprite.Render(X, Y - 1);
            //Sprite.Render(X, Y + 1);
            Sprite.Render(X - 1, Y);
            Sprite.Render(X + 1, Y);

            Sprite.Shader = null;
            Sprite.Alpha = prev;
        }
        public override void Render()
        {
            //Collider.Render();
        }


        public static Player CreateFromXML(Scene scene, XmlAttributeCollection xml)
        {
            Player n = new Player(int.Parse(xml["x"].Value), int.Parse(xml["y"].Value));

            scene.Add(n);

            (scene as Mission).Camera.SetTarget(n);
            return n;
        }
    }
}
