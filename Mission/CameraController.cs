using Otter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bones
{
    public class CameraController :Entity
    {
        private Entity Target;
        private Vector2 ShakeAmmount;
        private float ShakeDuration;

        
        public CameraController()
            : base()
        {
            
        }

        public void SetTarget(Entity target)
        {
            this.Target = target;
        }

        public override void Update()
        {
            Vector2 final = new Vector2();
            if (Target != null && Target.Group==Mission.Instance.World)
            {
                final.X = Target.X - Game.Width / 2;
                final.Y = Target.Y - Game.Height / 2;
            }
            else
            {
                SetTarget(Mission.Instance.GetCurrentPlayer());
            }

            Scene.CameraX = Calc.LerpSnap(Scene.CameraX, final.X, 0.5f) + (Rand.Float(ShakeAmmount.X) * ShakeDuration);
            Scene.CameraY = Calc.LerpSnap(Scene.CameraY, final.Y, 0.5f) + (Rand.Float(ShakeAmmount.Y) * ShakeDuration);
            if (ShakeDuration > 0) ShakeDuration--;

            if (Scene.CameraX < 0) Scene.CameraX = 0;
            if (Scene.CameraX + Game.Width > Scene.Width) Scene.CameraX = Scene.Width - Game.Width;
            if (Scene.CameraY < 0) Scene.CameraY = 0;
            if (Scene.CameraY + Game.Height > Scene.Height) Scene.CameraY = Scene.Height - Game.Height;   
        }

        internal bool OnCamera(Entity other)
        {
            return (new Rectangle(0, 0, (int)Game.Width, (int)Game.Height)).Contains((int)(other.X - Scene.CameraX), (int)(other.Y - Scene.CameraY));
        }

        public void Shake(float ammount, int duration)
        {
            ShakeAmmount = new Vector2(ammount*2 / duration);
            ShakeDuration = duration;
        }
    }
}
