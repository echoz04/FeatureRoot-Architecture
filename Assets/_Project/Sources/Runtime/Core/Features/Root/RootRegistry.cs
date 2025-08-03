#if UNITY_EDITOR

using System.Collections.Generic;

namespace Sources.Core.Features.Root
{
    public static class RootRegistry
    {
        public static IReadOnlyList<BaseRoot> Roots => _roots;

        private static readonly List<BaseRoot> _roots = new();

        public static void Register(BaseRoot root) => _roots.Add(root);
        public static void Unregister(BaseRoot root) => _roots.Remove(root);
    }
}

#endif