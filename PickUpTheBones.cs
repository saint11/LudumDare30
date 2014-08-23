using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Otter;
using LuaInterface;
using SFML.Window;

namespace Bones
{
    public enum Tags { Actor, Solid, FX, Player };

    class Bones
    {
        public static String GAME_PATH;
        public static Game Game;

        public static Surface MainLayer;
        public static Surface HudLayer;
        public static Dictionary<string,Atlas> Atlas;

        public static Dictionary<string, Shader> Shader;

        //GlobalVars
        public static int TILEWIDTH = 32;

        static void Main(string[] args)
        {
            GAME_PATH = "Assets/";
#if DEBUG
            GAME_PATH = "../../Assets/";
#endif
            Game = new Game("Game", 428, 240);
            Game.SetWindow(428*2, 240*2);
            //Game.CameraZoom = 2;
            //Game.SetWindow((int)VideoMode.DesktopMode.Width, (int)VideoMode.DesktopMode.Height,true);

            MainLayer = Game.Surface;
            Game.Surface.Smooth = false;
            
            HudLayer = new Surface(Game.Width, Game.Height, Color.None);
            HudLayer.Scroll = 0;

            Controls.Init();
            Atlas = new Dictionary<string, Otter.Atlas>();

            Atlas["interface"] = new Atlas( GAME_PATH + "Images/interface.xml");
            Atlas["units"] = new Atlas(GAME_PATH + "Images/units.xml");
            Atlas["tiles"] = new Atlas(GAME_PATH + "Images/tiles.xml");

            //Shaders
            Shader = new Dictionary<string, Shader>();
            Shader["solid"] = new Shader(GAME_PATH + "Shaders/solid.frag");

            SpriteData.Init();

            Game.FirstScene = new Mission();
            Game.OnUpdate += Controls.Update;

            Game.Start();
        
        }

    }

    public enum Team
    {
        Player,
        Enemy
    }
}
