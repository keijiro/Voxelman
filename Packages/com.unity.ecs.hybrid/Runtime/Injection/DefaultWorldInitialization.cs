using System;
using System.Linq;
using System.Collections.Generic;

namespace Unity.Entities
{
    static class DefaultWorldInitialization
    {
        const string k_DefaultWorldName = "Default World";
        static World s_CreatedWorld;

        static void DomainUnloadShutdown()
        {
            if (World.Active == s_CreatedWorld)
            {
                World.Active.Dispose ();
                World.Active = null;

                ScriptBehaviourUpdateOrder.UpdatePlayerLoop();
            }
            else
            {
                Debug.LogError("World has already been destroyed");
            }
        }

        static void GetBehaviourManagerAndLogException(World world, Type type)
        {
            try
            {
                world.GetOrCreateManager(type);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        public static void Initialize()
        {
            var world = new World(k_DefaultWorldName);
            World.Active = world;
            s_CreatedWorld = world;

            // Register hybrid injection hooks
            InjectionHookSupport.RegisterHook(new GameObjectArrayInjectionHook());
            InjectionHookSupport.RegisterHook(new TransformAccessArrayInjectionHook());
            InjectionHookSupport.RegisterHook(new ComponentArrayInjectionHook());

            PlayerLoopManager.RegisterDomainUnload (DomainUnloadShutdown, 10000);

            foreach (var ass in AppDomain.CurrentDomain.GetAssemblies())
            {
                var allTypes = ass.GetTypes();

                // Create all ComponentSystem
                var systemTypes = allTypes.Where(t => t.IsSubclassOf(typeof(ComponentSystemBase)) && !t.IsAbstract && !t.ContainsGenericParameters && t.GetCustomAttributes(typeof(DisableAutoCreationAttribute), true).Length == 0);
                foreach (var type in systemTypes)
                {
                    GetBehaviourManagerAndLogException(world, type);
                }
            }

            ScriptBehaviourUpdateOrder.UpdatePlayerLoop(world);
        }
    }
}
