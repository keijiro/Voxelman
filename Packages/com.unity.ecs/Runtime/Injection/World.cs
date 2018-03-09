using System.Collections.Generic;
using System.Reflection;
using System;
using System.Collections.ObjectModel;

namespace Unity.Entities
{
	public class World : IDisposable
	{
		public IEnumerable<ScriptBehaviourManager> BehaviourManagers => new ReadOnlyCollection<ScriptBehaviourManager>(m_BehaviourManagers);

	    readonly List<ScriptBehaviourManager> 				m_BehaviourManagers = new List<ScriptBehaviourManager> ();
		//@TODO: What about multiple managers of the same type...
	    readonly Dictionary<Type, ScriptBehaviourManager> 	m_BehaviourManagerLookup = new Dictionary<Type, ScriptBehaviourManager> ();

	    int 										m_DefaultCapacity = 10;
	    bool 										m_AllowGetManager = true;

	    public string                               Name { get; }

	    public static World 				        Active { get; set; }

	    public static ReadOnlyCollection<World> AllWorlds => new ReadOnlyCollection<World>(allWorlds);
	    static readonly List<World> allWorlds = new List<World>();

	    int GetCapacityForType(Type type)
		{
			return m_DefaultCapacity;
		}

		public void SetDefaultCapacity(int value)
		{
			m_DefaultCapacity = value;
		}

        public World(string name)
        {
//			Debug.Log("Create World");
            Name = name;
            allWorlds.Add(this);
        }

		public void Dispose()
		{
		    if (allWorlds.Contains(this))
		        allWorlds.Remove(this);
//			Debug.Log("Dispose World");

			// Destruction should happen in reverse order to construction
			m_BehaviourManagers.Reverse();

			//@TODO: Crazy hackery to make EntityManager be destroyed last.
			foreach (var behaviourManager in m_BehaviourManagers)
			{
				if (behaviourManager is EntityManager)
				{
					m_BehaviourManagers.Remove(behaviourManager);
					m_BehaviourManagers.Add(behaviourManager);
					break;
				}
			}

			m_AllowGetManager = false;
			foreach (var behaviourManager in m_BehaviourManagers)
			{
				try
				{
					behaviourManager.DestroyInstance ();
				}
				catch (Exception e)
				{
					Debug.LogException(e);
				}
			}
			m_AllowGetManager = true;

			m_BehaviourManagers.Clear();
			m_BehaviourManagerLookup.Clear();
		}

	    ScriptBehaviourManager CreateManagerInternal (Type type, int capacity, object[] constructorArgumnents)
		{
#if ENABLE_UNITY_COLLECTIONS_CHECKS
			if (!m_AllowGetManager)
				throw new ArgumentException("During destruction of a system you are not allowed to create more systems.");

			if (constructorArgumnents != null && constructorArgumnents.Length != 0)
			{
				var constructors = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
				if (constructors.Length == 1 && constructors[0].IsPrivate)
					throw new MissingMethodException($"Constructing {type} failed because the constructor was private, it must be public.");
			}
#endif

		    m_AllowGetManager = true;
		    ScriptBehaviourManager manager;
		    try
		    {
		        manager = Activator.CreateInstance(type, constructorArgumnents) as ScriptBehaviourManager;

		    }
		    catch
		    {
		        m_AllowGetManager = false;
		        throw;
		    }

			m_BehaviourManagers.Add (manager);
		    AddTypeLookup(type, manager);

		    try
		    {
		        manager.CreateInstance (this, capacity);

		    }
		    catch
		    {
		        RemoveManagerInteral(manager);
		        throw;
		    }

			return manager;
		}

	    ScriptBehaviourManager GetExistingManagerInternal (Type type)
		{
#if ENABLE_UNITY_COLLECTIONS_CHECKS
			if (!m_AllowGetManager)
				throw new ArgumentException("During destruction of a system you are not allowed to get or create more systems.");
#endif

			ScriptBehaviourManager manager ;
			if (m_BehaviourManagerLookup.TryGetValue(type, out manager))
				return manager;

			return null;
		}

	    ScriptBehaviourManager GetOrCreateManagerInternal (Type type)
		{
			var manager = GetExistingManagerInternal(type);

			return manager ?? CreateManagerInternal(type, GetCapacityForType(type), null);
		}

	    void AddTypeLookup(Type type, ScriptBehaviourManager manager)
	    {
	        while (type != typeof(ScriptBehaviourManager))
	        {
	            if (!m_BehaviourManagerLookup.ContainsKey(type))
	                m_BehaviourManagerLookup.Add(type, manager);

	            type = type.BaseType;
	        }
	    }

	    void RemoveManagerInteral(ScriptBehaviourManager manager)
		{
			if (!m_BehaviourManagers.Remove(manager))
				throw new ArgumentException($"manager does not exist in the world");


			var type = manager.GetType();
			while (type != typeof(ScriptBehaviourManager))
			{
			    if (m_BehaviourManagerLookup[type] == manager)
			    {
			        m_BehaviourManagerLookup.Remove(type);

			        foreach (var otherManager in m_BehaviourManagers)
			        {
			            if (otherManager.GetType().IsSubclassOf(type))
			                AddTypeLookup(otherManager.GetType(), otherManager);
			        }
			    }

				type = type.BaseType;
			}
		}

		public ScriptBehaviourManager CreateManager(Type type, params object[] constructorArgumnents)
		{
			return CreateManagerInternal(type, GetCapacityForType(type), constructorArgumnents);
		}

		public T CreateManager<T>(params object[] constructorArgumnents) where T : ScriptBehaviourManager
		{
			return (T)CreateManagerInternal(typeof(T), GetCapacityForType(typeof(T)), constructorArgumnents);
		}

		public T GetOrCreateManager<T> () where T : ScriptBehaviourManager
		{
			return (T)GetOrCreateManagerInternal (typeof(T));
		}

		public ScriptBehaviourManager GetOrCreateManager(Type type)
		{
			return GetOrCreateManagerInternal (type);
		}

		public T GetExistingManager<T> () where T : ScriptBehaviourManager
		{
			return (T)GetExistingManagerInternal (typeof(T));
		}

		public ScriptBehaviourManager GetExistingManager(Type type)
		{
			return GetExistingManagerInternal (type);
		}

		public void DestroyManager(ScriptBehaviourManager manager)
		{
			RemoveManagerInteral(manager);
		    manager.DestroyInstance();
		}
	}
}
