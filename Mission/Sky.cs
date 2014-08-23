using Otter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bones
{
    public class Sky: StateEntity
    {
        Image Bg;

        public Sky()
            : base(Vector2.Zero)
        {
            Bg = AddGraphic(new Image(Bones.GAME_PATH + "Images/bg/sky0.png"));
            Bg.Repeat = Repeat.XY;
            Bg.Alpha = 0.15f;
            Bg.Scroll = 0.5f;
        }

        public override void Update()
        {
            X-=0.1f;
        }
    }
}
