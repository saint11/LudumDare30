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

        public int World = 0;

        public Mission()
            : base()
        {
            Instance = this;

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
                World++;
                if (World > 1) World = 0;
                Add(new Flash(Color.Cyan) { LifeSpan = 50, FinalAlpha = 0, Layer = Hud.LAYER + 1 });
                var map = OgProj.Entities["tiles"].GetGraphic<Tilemap>();
                if(World==0)
                {
                    //map.SetTexture(Bones.Atlas["tiles"].GetAtlasTexture("caves"));
                    map.SetTexture(Bones.GAME_PATH+"Gfx/tiles/caves.png");
                }
                else
                {
                    map.SetTexture(Bones.GAME_PATH + "Gfx/tiles/cavesFuture.png");

                }
                
            }
        }
    }
}
