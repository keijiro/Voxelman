using UnityEditor.IMGUI.Controls;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;

namespace UnityEditor.ECS
{
    public class ComponentGroupListView : TreeView {
        
        Dictionary<int, ComponentGroup> componentGroupsById;

        ComponentSystemBase currentSystem;

        EntityWindow window;


        public static TreeViewState GetStateForSystem(ComponentSystemBase system, ref List<TreeViewState> states, ref List<string> stateNames)
        {
            if (system == null)
                return new TreeViewState();

            if (states == null)
            {
                states = new List<TreeViewState>();
                stateNames = new List<string>();
            }
            var currentSystemName = system.GetType().Name.ToString();

            TreeViewState stateForCurrentSystem = null;
            for (var i = 0; i < states.Count; ++i)
            {
                if (stateNames[i] == currentSystemName)
                {
                    stateForCurrentSystem = states[i];
                    break;
                }
            }
            if (stateForCurrentSystem == null)
            {
                stateForCurrentSystem = new TreeViewState();
                states.Add(stateForCurrentSystem);
                stateNames.Add(currentSystemName);
            }
            return stateForCurrentSystem;
        }

        public ComponentGroupListView(TreeViewState state, EntityWindow window, ComponentSystemBase system) : base(state)
        {
            this.window = window;
            currentSystem = system;
            Reload();
            SelectionChanged(GetSelection());
        }

        protected override TreeViewItem BuildRoot()
        {
            componentGroupsById = new Dictionary<int, ComponentGroup>();
            var currentID = 0;
            var root  = new TreeViewItem { id = currentID++, depth = -1, displayName = "Root" };
            if (currentSystem == null)
            {
                root.AddChild(new TreeViewItem { id = currentID++, displayName = "No Manager selected"});
            }
            else if (currentSystem.ComponentGroups.Length == 0)
            {
                root.AddChild(new TreeViewItem { id = currentID++, displayName = "No Component Groups in Manager"});
            }
            else
            {
                var groupIndex = 0;
                foreach (var group in currentSystem.ComponentGroups)
                {
                    componentGroupsById.Add(currentID, group);
                    var types = group.Types;
                    var groupName = string.Join(", ", (from x in types select x.Name).ToArray());

                    var groupItem = new TreeViewItem { id = currentID++, displayName = string.Format("({1}):", groupIndex, groupName) };
                    root.AddChild(groupItem);
                    ++groupIndex;
                }
                SetupDepthsFromParentsAndChildren(root);
            }
            return root;
        }

        override protected void RowGUI(RowGUIArgs args)
        {
            base.RowGUI(args);
            if (!componentGroupsById.ContainsKey(args.item.id))
                return;
            var countString = componentGroupsById[args.item.id].GetEntityArray().Length.ToString();
            DefaultGUI.LabelRightAligned(args.rowRect, countString, args.selected, args.focused);
        }

        override protected void SelectionChanged(IList<int> selectedIds)
        {
            if (selectedIds.Count > 0 && componentGroupsById.ContainsKey(selectedIds[0]))
            {
                window.CurrentComponentGroupSelection = componentGroupsById[selectedIds[0]];
            }
            else
            {
                window.CurrentComponentGroupSelection = null;
            }
        }

        override protected bool CanMultiSelect(TreeViewItem item)
        {
            return false;
        }

    }
}
