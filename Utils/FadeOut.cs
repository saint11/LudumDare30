using Otter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bones
{
    public class FadeOut : Entity
    {
        private Image image;
        private  float timeDelay;
        private  float timeOut;

        public bool Kill = true;
        public Action OnComplete;

        public FadeOut(float timeIn, float timeDelay, float timeOut, Color color)
        {
            Layer = -100;
            this.timeDelay = timeDelay;
            this.timeOut = timeOut;

            image = Image.CreateRectangle(Game.Instance.Width, Game.Instance.Height, color);
            AddGraphic(image);
            if (timeIn > 0)
            {
                image.Alpha = 0;
                Tween(image, new { Alpha = 1 }, timeIn).OnComplete(StartFadeDelay);
            }
            else
            {
                StartFadeDelay();
            }
        }

        private void StartFadeDelay()
        {
            Tween(image, new { }, timeDelay).OnComplete(StartFadeOut);
        }

        private void StartFadeOut()
        {
            Tween(image, new { Alpha = 0 }, timeOut).OnComplete(EndFunction);
        }

        private void EndFunction()
        {
            if (OnComplete != null) OnComplete();
            if (Kill) RemoveSelf();
        }
    }
}
