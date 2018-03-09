using System.Collections.Generic;
using NUnit.Framework;
using Unity.Entities;
using UnityEngine.ECS.Tests;
using UnityEditor.IMGUI.Controls;

namespace UnityEditor.ECS
{
    public class EntityListViewTests : ECSTestsFixture
    {

        static EntityListView CreateListViewForGroup(ComponentGroup group)
        {
            var state = new TreeViewState();
            var headerStates = new List<MultiColumnHeaderState>();
            var headerState = EntityListView.GetOrBuildHeaderState(ref headerStates, group, 100f);
            var header = new MultiColumnHeader(headerState);
            var listView = new EntityListView(state, header, group);
            return listView;
        }

        struct TestData
        {
            public float Value;
        }

        ComponentGroup CreateDataAndGroup()
        {
            m_Manager.CreateEntity(typeof(EcsTestData));
            return m_Manager.CreateComponentGroup(typeof(EcsTestData));
        }
        
        [Test]
        public void PrepareDataWorksWithComponentDataArray()
        {
            var componentGroup = CreateDataAndGroup();

            var listView = CreateListViewForGroup(componentGroup);
            
            listView.PrepareData();
            
            Assert.DoesNotThrow(listView.PrepareData);
        }
        
    }
}
