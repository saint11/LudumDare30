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
        private OgmoProject OgProj;

        public int World = 1;

        public Mission()
            : base()
        {
            Instance = this;
            
            PauseGroup(World+1);
            Camera = Add(new CameraController());

            Add(new Ground() { Layer = 1100 });
            Add(new Sky() { Layer = -1000 });
            //Add(new Terrain("test"));

            OgProj = new OgmoProject(Bones.GAME_PATH + "Maps/Bones.oep", Bones.GAME_PATH + "Images/");
            OgProj.RegisterTag((int)Tags.Solid, "solid");
            OgProj.BaseTileDepth = 1000;

            OgProj.LoadLevel(Bones.GAME_PATH + "Maps/test.oel", this);
            
            Add(new Hud());
        }

        public override void Update()
        {
            if (Controls.Swap.Pressed)
            {
                SwapWorlds();
            }
        }

        private void SwapWorlds()
        {
            PauseGroupToggle(World);
            World++;
            if (World > 2) World = 1;
            Add(new Flash(Color.Cyan) { LifeSpan = 50, FinalAlpha = 0, Layer = Hud.LAYER + 1 });
            PauseGroupToggle(World);

            foreach (var e in GetEntities<StateEntity>()) e.Swap();

            var map = OgProj.Entities["tiles"].GetGraphic<Tilemap>();
            if (World == 1)
            {
                //map.SetTexture(Bones.Atlas["tiles"].GetAtlasTexture("caves"));
                map.SetTexture(Bones.GAME_PATH + "Gfx/tiles/caves.png");
            }
            else if (World==2)
            {
                map.SetTexture(Bones.GAME_PATH + "Gfx/tiles/cavesFuture.png");

            }

        }

        public Player GetCurrentPlayer()
        {
            Player player=null;
            var players = GetEntities<Player>();
            foreach (var p in players)
            {
                if (p.Group == World) player = p;
            }

            return player;
        }
    }
}
