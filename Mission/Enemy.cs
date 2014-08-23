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
            Layer = -(int)Y + ExtraLayer;
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
            if (Hp <= 0) SetState(Die);

            Sprite.Scale = 1.3f;
            Tween(Sprite, new { ScaleX = 1, ScaleY = 1 }, 30).Ease(Ease.BounceOut);
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
        private System.Collections.IEnumerator Die()
        {
            ExtraLayer = 100;
            RemoveColliders(Collider);
            Sprite.Play("die");
            yield return 50;
            Tween(Sprite, new { Alpha = 0 }, 200).OnComplete(() => { RemoveSelf(); });
            yield return 0;
        }

    }
}
