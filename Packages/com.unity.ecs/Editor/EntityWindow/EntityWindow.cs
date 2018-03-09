using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Unity.Entities;
using UnityEngine;
using UnityEditor.IMGUI.Controls;

namespace UnityEditor.ECS
{
    public class EntityWindow : EditorWindow, IWorldSelectionWindow {

        const float kSystemListHeight = 100f;

        public void SetWorldSelection(World world)
        {
            CurrentWorldSelection = world;
        }

        public World CurrentWorldSelection
        {
            get { return currentWorldSelection; }
            set
            {
                currentWorldSelection = value;
                currentSystemSelection = null;
                currentComponentGroupSelection = null;
                InitSystemList();
                InitComponentGroupList();
                InitEntityList();
            }
        }
        World currentWorldSelection;

        public ComponentSystemBase CurrentSystemSelection {
            get { return currentSystemSelection; }
            set {
                currentSystemSelection = value;
                currentComponentGroupSelection = null;
                InitComponentGroupList();
                InitEntityList();
            }
        }
        ComponentSystemBase currentSystemSelection;

        public ComponentGroup CurrentComponentGroupSelection {
            get { return currentComponentGroupSelection; }
            set {
                currentComponentGroupSelection = value;
                InitEntityList();
            }
        }
        ComponentGroup currentComponentGroupSelection;

        void InitSystemList()
        {
            if (currentWorldSelection == null)
                return;
            var systemListState = SystemListView.GetStateForWorld(currentWorldSelection, ref systemListStates,
                ref systemListStateNames);
            systemListView = new SystemListView(systemListState, this);
            systemListView.SetManagers(systems);
        }

        void InitComponentGroupList()
        {
            if (currentSystemSelection == null)
                return;
            var groupListState = ComponentGroupListView.GetStateForSystem(currentSystemSelection, ref componentGroupListStates, ref componentGroupListStateNames);
            componentGroupListView = new ComponentGroupListView(groupListState, this, currentSystemSelection);
        }

        void InitEntityList()
        {
            if (currentComponentGroupSelection == null)
                return;
            entityListState = new TreeViewState();
            var headerState = EntityListView.GetOrBuildHeaderState(ref entityColumnHeaderStates, currentComponentGroupSelection, position.width - GUI.skin.verticalScrollbar.fixedWidth);
            var header = new MultiColumnHeader(headerState);
            entityListView = new EntityListView(entityListState, header, currentComponentGroupSelection);
        }

        [SerializeField] TreeViewState worldListState;
        WorldListView worldListView;

        SystemListView systemListView;

        [SerializeField]
        List<string> systemListStateNames;

        [SerializeField]
        List<TreeViewState> systemListStates;

        ComponentGroupListView componentGroupListView;

        [SerializeField]
        List<string> componentGroupListStateNames;

        [SerializeField]
        List<TreeViewState> componentGroupListStates;

        [SerializeField]
        TreeViewState entityListState;

        EntityListView entityListView;

        [SerializeField]
        List<MultiColumnHeaderState> entityColumnHeaderStates;

        [MenuItem ("Window/ECS/Entities", false, 2017)]
        static void Init ()
        {
            EditorWindow.GetWindow<EntityWindow>("Entities");
        }

        void OnEnable()
        {
            if (worldListState == null)
                worldListState = new TreeViewState();
            worldListView = new WorldListView(worldListState, this);
            SceneView.onSceneGUIDelegate += OnSceneGUI;
        }

        void OnDisable()
        {
            SceneView.onSceneGUIDelegate -= OnSceneGUI;
        }

        private ReadOnlyCollection<World> Worlds => worlds ?? (worlds = World.AllWorlds);
        private ReadOnlyCollection<World> worlds;

        ComponentSystemBase[] systems => (from s in CurrentWorldSelection.BehaviourManagers
            where s is ComponentSystemBase
            select s as ComponentSystemBase).ToArray();

        void WorldList(bool worldsAppeared)
        {
            if (worldsAppeared)
                worldListView.SetWorlds(Worlds);
            worldListView.OnGUI(GUIHelpers.GetExpandingRect());
        }

        void SystemList()
        {
            if (CurrentWorldSelection != null)
            {
                systemListView.OnGUI(GUIHelpers.GetExpandingRect());
            }
        }

        void ComponentGroupList()
        {
            if (CurrentSystemSelection != null)
            {
                componentGroupListView.OnGUI(GUIHelpers.GetExpandingRect());
            }
        }

        void EntityList()
        {
            if (currentComponentGroupSelection != null)
            {
                entityListView.PrepareData();
                entityListView.OnGUI(GUIHelpers.GetExpandingRect());
            }
        }

        private bool noWorlds = true;

        private float lastUpdate;
        void Update()
        {
            if (CurrentSystemSelection != null && EditorApplication.isPlaying && Time.time > lastUpdate + 0.1f)
            {
                Repaint();
            }
        }
        
        void OnGUI ()
        {
            var worldsAppeared = noWorlds && World.AllWorlds.Count > 0;
            noWorlds = World.AllWorlds.Count == 0;
            if (noWorlds)
            {
                GUIHelpers.ShowCenteredNotification(new Rect(Vector2.zero, position.size), "No ComponentSystems loaded. (Try pushing Play)");
                return;
            }

            GUILayout.BeginHorizontal(GUILayout.Height(kSystemListHeight));

            GUILayout.BeginVertical();
            WorldList(worldsAppeared);
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            SystemList();
            GUILayout.EndVertical();
            GUILayout.BeginVertical();
            ComponentGroupList();
            GUILayout.EndVertical();

            GUILayout.EndHorizontal();

            GUILayout.BeginVertical();
            EntityList();
            GUILayout.EndVertical();

            lastUpdate = EditorApplication.isPlaying ? 0f : Time.time;
        }

        void OnSceneGUI(SceneView sceneView)
        {
            entityListView?.DrawSelection();
        }
    }
}
