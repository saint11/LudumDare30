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

        public Enemy()
            :base(Vector2.Zero)
        {
            Sprite = AddGraphic(SpriteData.GetAnimation("crab"));
            Sprite.Play("idle");

            AddCollider(new BoxCollider(16, 8, (int)Tags.Actor,(int)Tags.Solid));
            Collider.OriginX = 8;
            Collider.OriginY = 22;
        }
        public override void Update()
        {
            Layer = -(int)Y;
        }


        public override void Render()
        {
            //Collider.Render();
        }

        public static Enemy CreateFromXML(Scene scene, XmlAttributeCollection xml)
        {
            Enemy n = new Enemy();

            n.SetPosition(int.Parse(xml["x"].Value), int.Parse(xml["y"].Value));

            n.Layer = 1;
            scene.Add(n);

            return n;
        }

        internal void Damage(int damage)
        {
            Hp -= damage;
            if (Hp <= 0) RemoveSelf();

            Sprite.Scale = 1.3f;
            Tween(Sprite, new { ScaleX = 1, ScaleY = 1 }, 30).Ease(Ease.BounceOut);
        }
    }
}
