using System;
using UnityEditor.IMGUI.Controls;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;

namespace UnityEditor.ECS
{
    public class SystemListView : TreeView {

        Dictionary<string, List<ComponentSystemBase>> managersByNamespace;
        Dictionary<int, ComponentSystemBase> managersByID;

        readonly EntityWindow window;

        public static TreeViewState GetStateForWorld(World world, ref List<TreeViewState> states,
            ref List<string> stateNames)
        {
            if (world == null)
                return new TreeViewState();

            if (states == null)
            {
                states = new List<TreeViewState>();
                stateNames = new List<string>();
            }
            var currentWorldName = world.GetType().Name.ToString();

            TreeViewState stateForCurrentWorld = null;
            for (var i = 0; i < states.Count; ++i)
            {
                if (stateNames[i] == currentWorldName)
                {
                    stateForCurrentWorld = states[i];
                    break;
                }
            }
            if (stateForCurrentWorld == null)
            {
                stateForCurrentWorld = new TreeViewState();
                states.Add(stateForCurrentWorld);
                stateNames.Add(currentWorldName);
            }
            return stateForCurrentWorld;
        }

        public SystemListView(TreeViewState state, EntityWindow window) : base(state)
        {
            this.window = window;
            Reload();
        }

        public void SetManagers(ComponentSystemBase[] managers)
        {
            managersByNamespace = new Dictionary<string, List<ComponentSystemBase>>();
            managersByID = new Dictionary<int, ComponentSystemBase>();
            foreach (var manager in managers)
            {
                var ns = manager.GetType().Namespace ?? "global";
                if (!managersByNamespace.ContainsKey(ns))
                    managersByNamespace[ns] = new List<ComponentSystemBase>{manager};
                else
                    managersByNamespace[ns].Add(manager);
            }
            foreach (var managerSet in managersByNamespace.Values)
            {
                managerSet.Sort((x, y) => String.CompareOrdinal(x.GetType().Name, y.GetType().Name));
            }
            Reload();
            SelectionChanged(GetSelection());
        }

        protected override TreeViewItem BuildRoot()
        {
            var currentID = 0;
            var root  = new TreeViewItem { id = currentID++, depth = -1, displayName = "Root" };
            if (managersByNamespace == null || managersByNamespace.Count == 0)
            {
                root.AddChild(new TreeViewItem { id = currentID++, displayName = "No ComponentSystems Loaded"});
            }
            else
            {
                foreach (var ns in (from ns in managersByNamespace.Keys orderby ns select ns))
                {
                    var nsItem = new TreeViewItem { id = currentID++, displayName = ns };
                    root.AddChild(nsItem);
                    foreach (var manager in managersByNamespace[ns])
                    {
                        managersByID.Add(currentID, manager);
                        var managerItem = new TreeViewItem { id = currentID++, displayName = manager.GetType().Name.ToString() };
                        nsItem.AddChild(managerItem);
                    }
                }
                SetupDepthsFromParentsAndChildren(root);
            }
            return root;
        }

        protected override void SelectionChanged(IList<int> selectedIds)
        {
            if (selectedIds.Count > 0 && managersByID.ContainsKey(selectedIds[0]))
            {
                window.CurrentSystemSelection = managersByID[selectedIds[0]];
            }
            else
            {
                window.CurrentSystemSelection = null;
            }
        }

        protected override bool CanMultiSelect(TreeViewItem item)
        {
            return false;
        }

    }
}
