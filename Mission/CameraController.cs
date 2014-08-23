using Otter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bones
{
    public class CameraController :StateEntity
    {
        private Entity target;

        public void InitScripts(LuaScript script)
        {
            if (script == null) return;
            script.AddCommand<string>("CameraFollow", this, Follow);
        }

        public CameraController()
            : base(Vector2.Zero)
        {
            
        }

        public void SetTarget(Entity target)
        {
            this.target = target;
        }

        public void Follow(string target)
        {
            if (target.ToLower() == "player")
                this.target = Player.Instance;
        }

        public override void Update()
        {
            if (target != null)
            {
                Scene.CameraX = target.X - Game.Width / 2;
                if (Scene.CameraX < 0) Scene.CameraX = 0;
                if (Scene.CameraX + Game.Width > Scene.Width) Scene.CameraX = Scene.Width - Game.Width;
                
                Scene.CameraY = target.Y - Game.Height / 2;
                if (Scene.CameraY < 0) Scene.CameraY = 0;
                if (Scene.CameraY + Game.Height > Scene.Height) Scene.CameraY = Scene.Height - Game.Height;
                
            }
        }

        internal bool OnCamera(Entity other)
        {
            return (new Rectangle(0, 0, (int)Game.Width, (int)Game.Height)).Contains((int)(other.X - Scene.CameraX), (int)(other.Y - Scene.CameraY));
        }
    }
}
