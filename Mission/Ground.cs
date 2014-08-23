using Otter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bones
{
    class Ground: StateEntity
    {
        Image Bg;

        public Ground()
            : base(Vector2.Zero)
        {
            Bg = AddGraphic(new Image(Bones.GAME_PATH + "Images/bg/ground.png"));
            Bg.Repeat = Repeat.XY;
        }
    }
}
