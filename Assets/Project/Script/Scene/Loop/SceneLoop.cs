using System;
using System.Collections.Generic;
using VContainer;
using VContainer.Unity;

namespace Mgfirefox.CrisisTd
{
    public class SceneLoop : IStartable, IFixedTickable, ITickable, ILateTickable, IDisposable
    {
        private readonly ISet<ISceneStartedListener> startedListeners =
            new HashSet<ISceneStartedListener>();
        private readonly ISet<ISceneFixedTickedListener> fixedTickedListeners =
            new HashSet<ISceneFixedTickedListener>();
        private readonly ISet<ISceneTickedListener> tickedListeners =
            new HashSet<ISceneTickedListener>();
        private readonly ISet<ISceneLateTickedListener> lateTickedListeners =
            new HashSet<ISceneLateTickedListener>();
        private readonly ISet<ISceneFinishedListener> finishedListeners =
            new HashSet<ISceneFinishedListener>();

        private readonly ISet<ISceneListener> addedListeners = new HashSet<ISceneListener>();
        private readonly ISet<ISceneListener> removedListeners = new HashSet<ISceneListener>();

        [Inject]
        public SceneLoop()
        {
        }

        public void Start()
        {
            foreach (ISceneListener listener in addedListeners)
            {
                AddListener(listener);
            }
            addedListeners.Clear();
        }

        public void FixedTick()
        {
            foreach (ISceneFixedTickedListener fixedTickedListener in fixedTickedListeners)
            {
                fixedTickedListener.OnSceneFixedTicked();
            }
        }

        public void Tick()
        {
            foreach (ISceneTickedListener tickedListener in tickedListeners)
            {
                tickedListener.OnSceneTicked();
            }

            Start();

            foreach (ISceneListener listener in removedListeners)
            {
                RemoveListener(listener);
            }
            removedListeners.Clear();
        }

        public void LateTick()
        {
            foreach (ISceneLateTickedListener lateTickedListener in lateTickedListeners)
            {
                lateTickedListener.OnSceneLateTicked();
            }
        }

        public void Dispose()
        {
            foreach (ISceneFinishedListener finishedListener in finishedListeners)
            {
                finishedListener.OnSceneFinished();
            }

            startedListeners.Clear();
            fixedTickedListeners.Clear();
            tickedListeners.Clear();
            lateTickedListeners.Clear();
            finishedListeners.Clear();

            addedListeners.Clear();
            removedListeners.Clear();
        }

        public void AddObject(ISceneObject @object)
        {
            if (@object == null)
            {
                return;
            }
            if (@object is ISceneListener listener)
            {
                addedListeners.Add(listener);

                if (listener is ISceneStartedListener startedListener)
                {
                    startedListener.OnSceneStarted();
                }
            }
        }

        public void RemoveObject(ISceneObject @object)
        {
            if (@object is null)
            {
                return;
            }
            if (@object is ISceneListener listener)
            {
                removedListeners.Add(listener);

                if (listener is ISceneFinishedListener finishedListener)
                {
                    finishedListener.OnSceneFinished();
                }
            }
        }

        private void AddListener(ISceneListener listener)
        {
            if (listener is ISceneStartedListener startedListener)
            {
                startedListeners.Add(startedListener);
            }
            if (listener is ISceneFixedTickedListener fixedTickedListener)
            {
                fixedTickedListeners.Add(fixedTickedListener);
            }
            if (listener is ISceneTickedListener tickedListener)
            {
                tickedListeners.Add(tickedListener);
            }
            if (listener is ISceneLateTickedListener lateTickedListener)
            {
                lateTickedListeners.Add(lateTickedListener);
            }
            if (listener is ISceneFinishedListener finishedListener)
            {
                finishedListeners.Add(finishedListener);
            }
        }

        private void RemoveListener(ISceneListener listener)
        {
            if (listener is ISceneStartedListener startedListener)
            {
                startedListeners.Remove(startedListener);
            }
            if (listener is ISceneFixedTickedListener fixedTickedListener)
            {
                fixedTickedListeners.Remove(fixedTickedListener);
            }
            if (listener is ISceneTickedListener tickedListener)
            {
                tickedListeners.Remove(tickedListener);
            }
            if (listener is ISceneLateTickedListener lateTickedListener)
            {
                lateTickedListeners.Remove(lateTickedListener);
            }
            if (listener is ISceneFinishedListener finishedListener)
            {
                finishedListeners.Remove(finishedListener);
            }
        }
    }
}
