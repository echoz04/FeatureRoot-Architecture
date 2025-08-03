using System;
using System.Collections.Generic;
using Sources.Core.Features.Logic;
using UnityEngine;
using VContainer.Unity;

namespace Sources.Core.Features.Root
{
    public abstract class BaseRoot : IDisposable, IStartable
    {
        public IReadOnlyList<ILogic> Logics => _logics;

        private readonly List<ILogic> _logics = new();

        public virtual void Start()
        {
            Debug.Log("Start");

#if UNITY_EDITOR
            RootRegistry.Register(this);
#endif
            InitializeLogic();
        }

        public abstract void InitializeLogic();

        public void AddLogic(params ILogic[] logics)
        {
            Debug.Log("Add Logic");

            foreach (var logic in logics)
            {
                if (logic == null || _logics.Contains(logic) == true)
                    continue;

                _logics.Add(logic);
            }
        }

        public void RemoveLogic(params ILogic[] logics)
        {
            Debug.Log("Remove Logic");

            foreach (ILogic logic in logics)
                _logics.Remove(logic);
        }

        public virtual void Dispose()
        {
#if UNITY_EDITOR
            RootRegistry.Unregister(this);
#endif
        }
    }
}
