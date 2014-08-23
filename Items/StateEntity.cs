using Otter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bones
{
    public class StateEntity:Entity
    {
        public delegate IEnumerator StateMethod();
        public StateMethod state { get; private set; }
        public StateMethod lastState { get; private set; }
        public string stateName { get; private set; }
        public string lastStateName { get; private set; }

        public List<Coroutine> coroutines;
        public List<Coroutine> coroutinesToAdd;
        public Coroutine StateCoroutine;

        public StateEntity(Vector2 position, int group)
            : base((int)position.X, (int)position.Y)
        {
            Group = group;
        }

        public override void Added()
        {
            Swap();
        }

        public int SetState(StateMethod stateMethod, bool reset = false)
        {
            if (reset || (stateMethod != state))
            {
                StopAllCoroutines();

                lastStateName = stateName;
                stateName = stateMethod.Method.Name;

                lastState = state;
                state = stateMethod;

                StartCoroutine(DoSetState());
            }
            return 0;
        }

        public virtual void SetState(string stateName)
        {
            StateMethod stateMethod = (StateMethod)Delegate.CreateDelegate(typeof(StateMethod), this, stateName, false);

            if ((stateMethod != state))
            {
                StopAllCoroutines();

                lastStateName = stateName;
                stateName = stateMethod.Method.Name;

                lastState = state;
                state = stateMethod;

                StartCoroutine(DoSetState());
            }
        }
        
        public void PauseState()
        {
            StateCoroutine.Active = false;
        }
        public void ResumeState()
        {
            StateCoroutine.Active = true;
        }


        public Coroutine StartCoroutine(IEnumerator coroutine)
        {
            if (coroutines == null)
                coroutines = new List<Coroutine>();
            if (coroutinesToAdd == null)
                coroutinesToAdd = new List<Coroutine>();

            Coroutine co = new Coroutine(coroutine);
            coroutines.Add(co);
            coroutinesToAdd.Add(co);

            return co;
        }

        public void StopAllCoroutines()
        {
            if (coroutines == null) return;
            foreach (Coroutine co in coroutines)
                co.Cancel();
            coroutines = null;
        }


        IEnumerator DoSetState()
        {
            yield return 0;
            StateCoroutine = StartCoroutine(state());
        }

        public void InvokeState(StateMethod stateMethod, int time)
        {
            StartCoroutine(DoInvokeState(stateMethod, time));
        }

        IEnumerator DoInvokeState(StateMethod stateMethod, int time)
        {
            yield return time;
            SetState(stateMethod);
        }

        public override void UpdateFirst()
        {
            base.UpdateFirst();

            if (coroutinesToAdd!=null)
            foreach(Coroutine co in coroutinesToAdd)
                AddComponent(co);

            coroutinesToAdd = null;
        }

        internal void Swap()
        {
            if (Mission.Instance.World == Group || Group==0)
            {
                Graphic.Alpha = 1f;
            }
            else
            {
                Graphic.Alpha = 0.1f;
            }
        }
    }
}
