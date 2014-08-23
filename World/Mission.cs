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
        private CameraController Camera;

        public Mission()
            : base()
        {
            Instance = this;

            Add(new Ground());
            Add(new Sky() { Layer = -100 });
            //Add(new Terrain("test"));
            Player = Add(new Player(80, 80));

            OgmoProject og = new OgmoProject(Bones.GAME_PATH + "Maps/Bones.oep", Bones.GAME_PATH + "Images/");
            og.RegisterTag((int)Tags.Solid, "solid");

            og.LoadLevel(Bones.GAME_PATH + "Maps/test.oel", this);

            Camera = Add(new CameraController());
            Camera.SetTarget(Player);
        }
    }
}
