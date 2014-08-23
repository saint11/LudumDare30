using Otter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bones
{
    public class Hud:Entity
    {
        List<Spritemap<string>> HpDisplay;
        Player Player;

        public Hud()
        {
            Layer = -100000000;
        }

        public override void Update()
        {
            if (Player==null)
            {
                Player = Scene.GetEntity<Player>();

                HpDisplay = new List<Spritemap<string>>();
                for (int i = 0; i < Player.MaxHp; i++)
                {
                    Spritemap<string> s = SpriteData.GetAnimation("heart");
                    s.X = 32 + i * 16;
                    s.Y = 32;
                    AddGraphicGUI(s);
                    HpDisplay.Add(s);
                }

            }
            else
            {
                UpdateValues();
            }
        }

        private void UpdateValues()
        {
            for (int i = 0; i < Player.MaxHp; i++)
            {
                if (i >= Player.Hp) HpDisplay[i].Play("empty");
                else HpDisplay[i].Play("full");
            }
        }
    }
}
