using UnityEditor.IMGUI.Controls;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Unity.Entities;

namespace UnityEditor.ECS
{
    public class WorldListView : TreeView
    {
        private ReadOnlyCollection<World> worlds;

        private Dictionary<int, World> worldsById;

        readonly IWorldSelectionWindow window;

        public WorldListView(TreeViewState state, IWorldSelectionWindow window) : base(state)
        {
            this.window = window;
            Reload();
        }

        public void SetWorlds(ReadOnlyCollection<World> newWorlds)
        {
            worlds = newWorlds;
            worldsById = new Dictionary<int, World>(worlds.Count);
            Reload();
            SelectionChanged(GetSelection());
        }

        protected override TreeViewItem BuildRoot()
        {
            var currentID = 0;
            var root  = new TreeViewItem { id = currentID++, depth = -1, displayName = "Root" };
            if (worlds == null || worlds.Count == 0)
            {
                root.AddChild(new TreeViewItem { id = currentID++, displayName = "No Worlds Loaded"});
            }
            else
            {
                foreach (var world in worlds)
                {
                    worldsById.Add(currentID, world);
                    var item = new TreeViewItem { id = currentID++, displayName = world.Name };
                    root.AddChild(item);
                }
                SetupDepthsFromParentsAndChildren(root);
            }
            return root;
        }

        protected override void SelectionChanged(IList<int> selectedIds)
        {
            if (selectedIds.Count > 0 && worldsById.ContainsKey(selectedIds[0]))
            {
                window.SetWorldSelection(worldsById[selectedIds[0]]);
            }
            else
            {
                window.SetWorldSelection(null);
            }
        }

        protected override bool CanMultiSelect(TreeViewItem item)
        {
            return false;
        }

    }
}
