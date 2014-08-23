using LuaInterface;
using Otter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bones
{
    public class LuaScript
    {
        public Lua Script;
        public string Name { get; private set; }

        public Action<LuaScript> OnCallFunc;

        public LuaScript()
        {
            Script = new Lua();
            LoadDefaultStuff();
        }

        public LuaScript(string fileName)
        {
            Script = new Lua();
            
            LoadDefaultStuff();            
            LoadScript(fileName);
        }

        private void LoadDefaultStuff()
        {
            //Adding default commands
            AddCommand<string>("Log", this, Log);
            AddCommand("QuitGame", Bones.Game, Bones.Game.Close);
            Script["GameHeight"] = Game.Instance.Height;
            Script["GameWidth"] = Game.Instance.Width;
        }

        #region Default Commands

        public void Log(string s)
        {
            Debugger.Instance.Log("Lua> " + s);
        }

        #endregion

        #region Game Commands

        public void LoadScript(string fileName)
        {
            try
            {
                Script.DoFile(Bones.GAME_PATH + "Scripts/" + fileName + ".lua");
                Name = fileName;
            }
            catch (Exception e)
            {
                ErrorFlash();
                Debugger.Instance.Log("LUA ERROR>> " + e.Message);
            }
        }

        public void CallFunction(string functionName, object[] parameters = null)
        {
            if (OnCallFunc!=null) OnCallFunc(this);
            LuaFunction func = (LuaFunction)Script[functionName];
            if (func != null) try
            {
                if (parameters != null)
                    func.Call(parameters);
                else
                    func.Call();
            }
            catch (LuaException e)
            {
                ErrorFlash();
                Debugger.Instance.Log("LUA ERROR [" + functionName + "]> " + e.Message);
            }
            else Console.WriteLine("[lua script (" + Name + ")] Function not found: " + functionName);
        }

        
        #endregion

        #region Adding Commands and Functions


        public void AddCommand(string name, object owner, Action command)
        {
            Script.RegisterFunction(name, owner, command.Method);
        }
        public void AddCommand<T>(string name, object owner, Action<T> command)
        {
            Script.RegisterFunction(name, owner, command.Method);
        }
        public void AddCommand<T1, T2>(string name, object owner, Action<T1, T2> command)
        {
            Script.RegisterFunction(name, owner, command.Method);
        }
        public void AddCommand<T1, T2, T3>(string name, object owner, Action<T1, T2, T3> command)
        {
            Script.RegisterFunction(name, owner, command.Method);
        }
        public void AddCommand<T1, T2, T3, T4>(string name, object owner, Action<T1, T2, T3, T4> command)
        {
            Script.RegisterFunction(name, owner, command.Method);
        }
        public void AddCommand<T1, T2, T3, T4, T5>(string name, object owner, Action<T1, T2, T3, T4, T5> command)
        {
            Script.RegisterFunction(name, owner, command.Method);
        }
        public void AddCommand<T1, T2, T3, T4, T5, T6>(string name, object owner, Action<T1, T2, T3, T4, T5, T6> command)
        {
            Script.RegisterFunction(name, owner, command.Method);
        }
        public void AddCommand<T1, T2, T3, T4, T5, T6, T7>(string name, object owner, Action<T1, T2, T3, T4, T5, T6, T7> command)
        {
            Script.RegisterFunction(name, owner, command.Method);
        }


        internal void AddFunction<T>(string name, object owner, Func<T> func)
        {
            Script.RegisterFunction(name, owner, func.Method);
        }
        internal void AddFunction<T1, T2>(string name, object owner, Func<T1, T2> func)
        {
            Script.RegisterFunction(name, owner, func.Method);
        }
        internal void AddFunction<T1, T2, T3>(string name, object owner, Func<T1, T2, T3> func)
        {
            Script.RegisterFunction(name, owner, func.Method);
        }


        #endregion


        internal float GetFloat(string varName)
        {
            return (float)Script.GetNumber(varName);
        }

        internal int GetInt(string varName)
        {
            return (int)Script.GetNumber(varName);
        }

        private void ErrorFlash()
        {
            if (Scene.Instance != null)
            {
                Scene.Instance.Add(new Flash(Color.Red) { LifeSpan = 45, Alpha = 0.5f, Surface = Scene.Instance.Surface });
            }
        }
    }
}
