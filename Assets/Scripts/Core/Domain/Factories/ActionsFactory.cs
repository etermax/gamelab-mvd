using System;
using System.Collections.Generic;

namespace Core.Domain.Factories
{
    public static partial class ActionsFactory
    {
        static readonly Dictionary<Type, object> instances = new Dictionary<Type, object>();

        public static T GetOrInstanciate<T>(Func<object> instanciator)
        {
            object singletonObj = Get<T>();
            object nullValue = default(T);

            if (singletonObj != nullValue)
                return (T) singletonObj;
            return Instanciate<T>(instanciator);
        }

        static ActionsFactory()
        {
            
        }

        private static T Get<T>()
        {
            var type = typeof(T);
            object singletonObj;

            if (instances.TryGetValue(type, out singletonObj))
                return (T) singletonObj;

            return default(T);
        }

        public static void Set<T>(object singletonObj)
        {
            instances[typeof(T)] = singletonObj;
        }

        private static T Instanciate<T>(Func<object> instanciator)
        {
            var singletonObj = instanciator();
            instances[typeof(T)] = singletonObj;
            return (T) singletonObj;
        }
    }
}