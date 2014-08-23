using Otter;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Bones
{
    public class Coroutine : Component
    {
        public bool Finished { get; private set; }
        public Action OnComplete;
        public bool RemoveOnComplete = true;

        private Stack<IEnumerator> enumerators;
        private float waitTimer;
        public bool Active=true;

        static private readonly long DoTicks = (long)(TimeSpan.FromSeconds(1.0 / 60).Ticks * .3f);

        public Coroutine(IEnumerator functionCall)
            : base()
        {
            enumerators = new Stack<IEnumerator>();
            waitTimer = 0;
            
            enumerators.Push(functionCall);
        }

        public override void Update()
        {
            if (!Active) return;
            
            IEnumerator now = enumerators.Peek();

            if (waitTimer>0)
            {
                waitTimer--;
            }
            else if (now.MoveNext())
            {
                if (now.Current is int)
                {
                    waitTimer = (int)now.Current;
                }
                else if (now.Current is IEnumerator)
                    enumerators.Push(now.Current as IEnumerator);
            }
            else
            {
                enumerators.Pop();
                if (enumerators.Count == 0)
                {
                    Finished = true;
                    Active = false;
                    if (RemoveOnComplete)
                    {
                        if (OnComplete != null) OnComplete();
                        RemoveSelf();
                    }
                }
            }
        }

        public void Cancel()
        {
            Active = false;
            Finished = true;

            enumerators.Clear();
            waitTimer = 0;
            RemoveSelf();
        }

        public void Replace(IEnumerator functionCall)
        {
            Active = true;
            Finished = false;

            enumerators.Clear();
            waitTimer = 0;
            enumerators.Push(functionCall);
        }
        
        #region Static

        static public IEnumerator WaitOrPass(int frames, Func<bool> checker)
        {
            for (float i = 0; i < frames; i += 1)
            {
                if (checker())
                    break;
                yield return 0;
            }
        }

        static public IEnumerator DoNoFrameSkip(IEnumerator e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            while (e.MoveNext())
            {
                if (stopwatch.ElapsedTicks >= DoTicks)
                {
                    stopwatch.Stop();
                    yield return 0;
                    stopwatch.Restart();
                }
            }

            stopwatch.Stop();
        }

        static public IEnumerator DoNoFrameSkip(IEnumerator e, float framePercent)
        {
            long doTicks = (long)(TimeSpan.FromSeconds(1.0).Ticks * framePercent);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            while (e.MoveNext())
            {
                if (stopwatch.ElapsedTicks >= doTicks)
                {
                    stopwatch.Stop();
                    yield return 0;
                    stopwatch.Restart();
                }
            }

            stopwatch.Stop();
        }

        static public IEnumerator DoNoFrameSkipUnless(IEnumerator e, Func<int> waitChecker)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            while (e.MoveNext())
            {
                int wait = waitChecker();
                while (wait > 0)
                {
                    wait--;
                    yield return 0;
                }

                if (stopwatch.ElapsedTicks >= DoTicks)
                {
                    stopwatch.Stop();
                    yield return 0;
                    stopwatch.Restart();
                }
            }

            stopwatch.Stop();
        }

        #endregion
        
    }
}
