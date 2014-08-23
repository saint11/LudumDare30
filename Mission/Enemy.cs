using Otter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Bones
{
    public class Enemy:StateEntity
    {
        private Spritemap<string> Sprite;
        private int Hp = 3;
        private int ExtraLayer=0;
        private Entity Player;
        private BasicMovement Movement;
        private bool Outline=true;

        public Enemy(int x, int y)
            :base(Vector2.Zero)
        {
            SetPosition(x, y);

            Sprite = AddGraphic(SpriteData.GetAnimation("crab"));
            Sprite.Play("idle");

            AddCollider(new BoxCollider(16, 8, (int)Tags.Actor));
            Collider.OriginX = 8;
            Collider.OriginY = 22;

            Movement = AddComponent(new BasicMovement(300, 300, 10));
            Movement.AddCollision((int)Tags.Solid);
            Movement.Collider = Collider;
            SetState(Follow);
        }


        private System.Collections.IEnumerator Follow()
        {
            while(true)
            {
                if (Player!=null)
                {
                    Vector2 target = new Vector2(X - Player.X, Y - Player.Y);
                    target.Normalize(50);

                    Movement.TargetSpeed.X = -target.X;
                    Movement.TargetSpeed.Y = -target.Y;

                    Collider col = Collide(X,Y,(int)Tags.Player);
                    if (col != null)
                    {
                        Player p = col.Entity as Player;
                        p.Damage(1, this);
                    }
                }
                else
                {
                    Player = Scene.GetEntity<Player>();
                }

                if (Movement.Speed.Length>0.1f)
                {
                    Sprite.Play("run");
                }
                else
                {
                    Sprite.Play("idle");
                }

                yield return 0;
            }
        }
        public override void Update()
        {
            Layer = -(int)Y + ExtraLayer;
        }


        public override void Render()
        {
            //Collider.Render();
        }

        public static Enemy CreateFromXML(Scene scene, XmlAttributeCollection xml)
        {
            Enemy n = new Enemy(int.Parse(xml["x"].Value), int.Parse(xml["y"].Value));
            scene.Add(n);

            return n;
        }

        internal void Damage(int damage, float dir)
        {
            Movement.TargetSpeed.X = (float)(Math.Cos(dir * Util.DEG_TO_RAD) * 20);
            Movement.TargetSpeed.Y = -(float)(Math.Sin(dir * Util.DEG_TO_RAD) * 20);
            Movement.Speed.X = (float)(Math.Cos(dir * Util.DEG_TO_RAD) * 300);
            Movement.Speed.Y = -(float)(Math.Sin(dir * Util.DEG_TO_RAD) * 300);
            Hp -= damage;

            Sprite.Scale = 1.3f;
            Tween(Sprite, new { ScaleX = 1, ScaleY = 1 }, 30).Ease(Ease.BounceOut);
            
            if (Hp <= 0) SetState(Die);
            else SetState(TakeHit);
        }

        private System.Collections.IEnumerator TakeHit()
        {
            Sprite.Play("run");
            yield return 30;
            yield return SetState(Follow);
        }

        public override void Prerender()
        {
            if (Outline)
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
        }
        private System.Collections.IEnumerator Die()
        {
            Outline = false;
            Movement.TargetSpeed.X = 0;
            Movement.TargetSpeed.Y = 0;
            ExtraLayer = 100;
            RemoveColliders(Collider);
            Sprite.Play("die");
            yield return 50;
            Tween(Sprite, new { Alpha = 0 }, 200).OnComplete(() => { RemoveSelf(); });
            yield return 0;
        }

    }
}
