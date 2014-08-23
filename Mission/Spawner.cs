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
    public class Spawner:StateEntity
    {
        private Spritemap<string> Sprite;
        public Spawner(int x, int y)
            :base(new Vector2(x,y))
        {
            Sprite = AddGraphic(SpriteData.GetAnimation("spawner"));
            Sprite.Play("idle");
            Sprite.X += Sprite.OriginX;
            Sprite.Y += Sprite.OriginY;

            Layer = -(int)Y;

            SetState(Wait);
        }

        private IEnumerator Spawn()
        {
            Scene.Add(new Enemy((int)X + 16, (int)Y + 16));
            Sprite.Scale = 1.3f;
            Tween(Sprite, new { ScaleX = 1, ScaleY = 1 }, 40).Ease(Ease.BounceOut);
            yield return 10;
            yield return SetState(Wait);
        }

        private IEnumerator Wait()
        {
            yield return 300;
            yield return SetState(Spawn);
        }

        public static Spawner CreateFromXML(Scene scene, XmlAttributeCollection xml)
        {
            Spawner n = new Spawner(int.Parse(xml["x"].Value), int.Parse(xml["y"].Value));
            scene.Add(n);

            return n;
        }
    }
}
