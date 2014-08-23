using Otter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bones
{
    public class Mission : Scene
    {
        public new static Mission Instance;
        public Player Player;
        public CameraController Camera;

        public Mission()
            : base()
        {
            Instance = this;

            Camera = Add(new CameraController());

            Add(new Ground() { Layer = 1100 });
            Add(new Sky() { Layer = -1000 });
            //Add(new Terrain("test"));

            OgmoProject og = new OgmoProject(Bones.GAME_PATH + "Maps/Bones.oep", Bones.GAME_PATH + "Images/");
            og.RegisterTag((int)Tags.Solid, "solid");
            og.BaseTileDepth = 1000;

            og.LoadLevel(Bones.GAME_PATH + "Maps/test.oel", this);

            Add(new Hud());
        }
    }
}
