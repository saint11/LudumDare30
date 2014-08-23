using Otter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bones
{

    public class Controls
    {
        static public Controls Instance;

        public static Button Attack = new Button();
        public static Button SpecialAttack = new Button();
        public static Button Back = new Button();
        public static Button Action = new Button();
        public static Button Inventory = new Button();
        public static Button ChangeStance = new Button();
        public static Button Defend = new Button();

        public static Button RefreshScripts = new Button();
        public static Button RefreshProcedural = new Button();
        public static Button OpenEditor = new Button();

        public static Axis Axis { get; private set; }

        internal static void Init()
        {
            Axis = new Axis(Key.Up, Key.Right, Key.Down, Key.Left);

            Axis.AddKey(Key.Left, Direction.Left);
            Axis.AddKey(Key.A, Direction.Left);
            Axis.AddAxis(JoyAxis.X, JoyAxis.Y, 1);

            Attack.AddKey(Key.Z);
            Attack.AddAxisButton(AxisButton.ZMinus,1);

            SpecialAttack.AddKey(Key.LControl);
            SpecialAttack.AddButton(5,1);

            Back.AddKey(Key.Escape);
            Back.AddButton(6, 1);

            Action.AddKey(Key.Space);
            Action.AddButton(0, 1);

            Inventory.AddKey(new Key[] { Key.Tab, Key.I });
            Inventory.AddButton(7, 1);

            ChangeStance.AddKey(Key.Up);
            ChangeStance.AddButton(3, 1);

            Defend.AddKey(Key.Down);
            Defend.AddAxisButton(AxisButton.ZPlus, 1);

            //Debug keys
            RefreshScripts.AddKey(Key.F9);
            RefreshProcedural.AddKey(Key.F5);
            OpenEditor.AddKey(Key.F8);
        }

        internal static void Update()
        {
            Attack.UpdateFirst();
            SpecialAttack.UpdateFirst();
            Back.UpdateFirst();
            Action.UpdateFirst();
            Inventory.UpdateFirst();
            ChangeStance.UpdateFirst();
            Defend.UpdateFirst();

            RefreshScripts.UpdateFirst();
            RefreshProcedural.UpdateFirst();
            OpenEditor.UpdateFirst();

            Axis.UpdateFirst();
        }
    }
}
