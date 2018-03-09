using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Entities;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using UnityEngine.Experimental.LowLevel;

namespace UnityEditor.ECS
{
	public class PlayerLoopListView : TreeView
	{

		private HashSet<string> systemNames = new HashSet<string>();
		private Dictionary<int, PlayerLoopSystem> playerLoopSystemsByListID;
		private HashSet<int> systemSubtreeIDs;
		private static readonly string kPlayerlooplistviewItemId = "PlayerLoopListView Item ID";
		private PlayerLoopSystem playerLoop;

		public PlayerLoopListView(TreeViewState state) : base(state)
		{
			Reload();
		}

		public void UpdatePlayerLoop(PlayerLoopSystem playerLoop, HashSet<string> systemNames)
		{
			this.playerLoop = playerLoop;
			this.systemNames = systemNames;
			Reload();
		}

		protected override TreeViewItem BuildRoot()
		{
			var currentID = 0;
			TreeViewItem root;
			playerLoopSystemsByListID = new Dictionary<int, PlayerLoopSystem>();
			systemSubtreeIDs = new HashSet<int>();
			if (playerLoop.subSystemList == null ||
				playerLoop.subSystemList.Length == 0)
			{
				root = new TreeViewItem {id = currentID++, depth = -1, displayName = "Root"};
				root.AddChild(new TreeViewItem {id = currentID++, displayName = "No Player Loop Loaded"});
			}
			else
			{
				bool dummy;
				CreateItemsForLoopSystem(playerLoop, ref currentID, out root, out dummy);
			}
			SetupDepthsFromParentsAndChildren(root);
			return root;
		}

		private void CreateItemsForLoopSystem(PlayerLoopSystem system, ref int currentID, out TreeViewItem parent, out bool hasSystems)
		{
			parent = new TreeViewItem
			{
				id = currentID++,
				depth = -1,
				displayName = system.type == null ? "null" : system.type.Name
			};
			playerLoopSystemsByListID.Add(parent.id, system);

			hasSystems = system.type != null && systemNames.Contains(system.type.FullName);
			if (system.subSystemList != null)
			{
				foreach (var subSystem in system.subSystemList)
				{
					TreeViewItem child;
					bool childHasSystems;
					CreateItemsForLoopSystem(subSystem, ref currentID, out child, out childHasSystems);
					parent.AddChild(child);
					hasSystems |= childHasSystems;
				}
			}
			if (hasSystems)
			{
				systemSubtreeIDs.Add(parent.id);
			}
		}

		private void RecreatePlayerLoop()
		{
			var newPlayerLoop = BuildSystemFromList(rootItem);
		    ScriptBehaviourUpdateOrder.SetPlayerLoopAndNotify(newPlayerLoop);
			Reload();
		}

		private PlayerLoopSystem BuildSystemFromList(TreeViewItem parent)
		{
			var parentSystem = playerLoopSystemsByListID[parent.id];
			if (parent.hasChildren)
			{
				var childSystems = new PlayerLoopSystem[parent.children.Count];
				for (var i = 0; i < childSystems.Length; ++i)
				{
					childSystems[i] = BuildSystemFromList(parent.children[i]);
				}
				parentSystem.subSystemList = childSystems;
			}
			return parentSystem;
		}

		protected override bool CanStartDrag(CanStartDragArgs args)
		{
			return systemSubtreeIDs.Contains(args.draggedItem.id) && systemNames.Contains(playerLoopSystemsByListID[args.draggedItem.id].type.FullName);
		}

		protected override void SetupDragAndDrop(SetupDragAndDropArgs args)
		{
			var itemId = args.draggedItemIDs[0];
			var item = FindItem(itemId, rootItem);
			DragAndDrop.PrepareStartDrag();
			DragAndDrop.SetGenericData(kPlayerlooplistviewItemId, itemId);
			DragAndDrop.StartDrag(item.displayName);
		}

	    private TreeViewItem Find(Predicate<TreeViewItem> predicate, TreeViewItem searchFromThisItem)
	    {
	        if (predicate(searchFromThisItem))
	        {
	            return searchFromThisItem;
	        }
	        else if (searchFromThisItem.hasChildren)
	        {
	            foreach (var child in searchFromThisItem.children)
	            {
	                var found = Find(predicate, child);
	                if (found != null)
	                    return found;
	            }
	        }
	        return null;
	    }

	    private IEnumerable<Type> GetExpandedTypes()
	    {
	        var expandedIds = GetExpanded();
	        return from x in playerLoopSystemsByListID.Keys where expandedIds.Contains(x) select playerLoopSystemsByListID[x].type;
	    }

	    private void SetExpandedFromTypes(IEnumerable<Type> expandedTypes)
	    {
	        SetExpanded((from x in playerLoopSystemsByListID.Keys
	            where expandedTypes.Contains(playerLoopSystemsByListID[x].type)
	            select x).ToList());
	    }

	    private void SetSelectionFromType(Type type)
	    {
	        var newItem = Find(x => playerLoopSystemsByListID[x.id].type == type, rootItem);
	        SetSelection(new List<int>{newItem.id});
	    }

		protected override DragAndDropVisualMode HandleDragAndDrop(DragAndDropArgs args)
		{
			var dropData = DragAndDrop.GetGenericData(kPlayerlooplistviewItemId);
			if (dropData == null)
			{
				return DragAndDropVisualMode.None;
			}

			switch (args.dragAndDropPosition)
			{
				case DragAndDropPosition.OutsideItems:
				case DragAndDropPosition.UponItem:
					return DragAndDropVisualMode.None;
				case DragAndDropPosition.BetweenItems:
				    if (args.parentItem == rootItem)
				        return DragAndDropVisualMode.None;
					if (args.performDrop)
					{
						var itemId = (int)dropData;
						var item = FindItem(itemId, rootItem);
						if (item.parent == args.parentItem && item.parent.children.IndexOf(item) < args.insertAtIndex)
							--args.insertAtIndex;
						item.parent.children.Remove(item);
						item.parent = args.parentItem;
						args.parentItem.children.Insert(args.insertAtIndex, item);
					    var selectedType = playerLoopSystemsByListID[itemId].type;
					    var expandedTypes = GetExpandedTypes();

						RecreatePlayerLoop();

					    SetExpandedFromTypes(expandedTypes);
					    SetSelectionFromType(selectedType);
					}
					break;
				default:
					throw new ArgumentException("Unrecognized DragAndDropPosition");

			}

			return DragAndDropVisualMode.Move;
		}

		protected override bool CanMultiSelect(TreeViewItem item)
		{
			return false;
		}

		protected override bool CanBeParent(TreeViewItem item)
		{
			return false;
		}

		protected override void RowGUI(RowGUIArgs args)
		{
			GUI.enabled = systemSubtreeIDs.Contains(args.item.id);
			base.RowGUI(args);
			GUI.enabled = true;
		}
	}

}
