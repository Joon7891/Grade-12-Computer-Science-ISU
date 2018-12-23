using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ISU_Medieval_Odyssey.Utility
{
    public class ObjectPool<T>
    {
        private readonly ConcurrentBag<T> objects;
        private readonly Func<T> objectGenerator;

        public ObjectPool(Func<T> objectGenerator = null)
        {
            if (objectGenerator == null)
            {
                objectGenerator = Activator.CreateInstance<T>;
            }

            objects = new ConcurrentBag<T>();
            this.objectGenerator = objectGenerator;
        }

        public void Warm(int size)
        {
            for (int i = 0; i < size; i++)
            {
                Add(objectGenerator());
            }
        }

        public T Get() => objects.TryTake(out T item) ? item : objectGenerator();
        public void Add(T item) => objects.Add(item);
    }
}
