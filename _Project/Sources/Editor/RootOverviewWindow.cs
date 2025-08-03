#if UNITY_EDITOR

using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;
using Sources.Core.Features.Root;
using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities.Editor;

namespace Sources.Editor
{
    public class RootOverviewWindow : OdinEditorWindow
    {
        [MenuItem("Tools/Root Overview")]
        private static void OpenWindow()
        {
            var window = GetWindow<RootOverviewWindow>();
            window.titleContent = new GUIContent("Root Overview");
            window.minSize = new Vector2(500, 400);
            window.Show();
        }

        private List<RootDisplay> _roots = new();

        protected override void OnEnable()
        {
            base.OnEnable();
            EditorApplication.update += OnEditorUpdate;
            Refresh();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            EditorApplication.update -= OnEditorUpdate;
        }

        private void OnEditorUpdate()
        {
            Refresh();
        }

        protected override void OnGUI()
        {
            GUILayout.Space(5);

            int totalRoots = _roots.Count;
            int totalLogics = _roots.Sum(r => r.Logics.Count);

            SirenixEditorGUI.BeginBox();
            GUILayout.Label($"Roots Count: {totalRoots}", SirenixGUIStyles.Label);
            GUILayout.Label($"Logics Count: {totalLogics}", SirenixGUIStyles.Label);
            SirenixEditorGUI.EndBox();

            GUILayout.Space(10);

            foreach (var root in _roots)
            {
                root.IsExpanded = SirenixEditorGUI.Foldout(root.IsExpanded, $"🔹 {root.RootType}", SirenixGUIStyles.Foldout);

                if (SirenixEditorGUI.BeginFadeGroup(root.GetHashCode()))
                {
                    SirenixEditorGUI.BeginIndentedVertical(SirenixGUIStyles.BoxContainer);

                    GUILayout.Label($"Logics ({root.Logics.Count})", SirenixGUIStyles.BoldLabel);
                    GUILayout.Space(4);

                    foreach (var logic in root.Logics)
                    {
                        SirenixEditorGUI.BeginBox();
                        GUILayout.Label($"⚙ {logic}", SirenixGUIStyles.Label);
                        SirenixEditorGUI.EndBox();
                    }

                    SirenixEditorGUI.EndIndentedVertical();
                }

                SirenixEditorGUI.EndFadeGroup();
                GUILayout.Space(6);
            }
        }

        private void Refresh()
        {
            var currentRoots = RootRegistry.Roots;
            var existingDict = _roots.ToDictionary(r => r.RootType);
            var newRoots = new List<RootDisplay>();

            foreach (var root in currentRoots)
            {
                if (root is BaseRoot baseRoot)
                {
                    var typeName = baseRoot.GetType().Name;

                    if (existingDict.TryGetValue(typeName, out var existing))
                    {
                        existing.UpdateLogics(baseRoot);
                        newRoots.Add(existing);
                    }
                    else
                    {
                        newRoots.Add(new RootDisplay(baseRoot));
                    }
                }
            }

            _roots = newRoots;
            Repaint();
        }

        private class RootDisplay
        {
            public string RootType;
            public List<string> Logics = new();
            public bool IsExpanded = true;

            public RootDisplay(BaseRoot baseRoot)
            {
                RootType = baseRoot.GetType().Name;
                UpdateLogics(baseRoot);
            }

            public void UpdateLogics(BaseRoot baseRoot)
            {
                Logics.Clear();
                foreach (var logic in baseRoot.Logics)
                {
                    Logics.Add(logic.GetType().Name);
                }
            }
        }
    }
}

#endif
