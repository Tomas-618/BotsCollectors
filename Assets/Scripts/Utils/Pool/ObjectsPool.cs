using System;
using System.Collections.Generic;
using System.Linq;

namespace Pool
{
    public class ObjectsPool<T> where T : class
    {
        private readonly List<T> _storedEntities;

        private List<T> _allEntities;

        public ObjectsPool(Func<T> created, in int count)
        {
            if (count < 0)
                throw new ArgumentOutOfRangeException(count.ToString());

            _storedEntities = CreateEntities(created ?? throw new ArgumentNullException(nameof(created)), count);
            _allEntities = _storedEntities
                .ToList();
        }

        public event Action<T> PutIn;

        public event Action<T> PutOut;

        public event Action<T> Removed;

        public IReadOnlyCollection<T> AllEntities => _allEntities;

        public IReadOnlyCollection<T> StoredEntities => _storedEntities;

        public IReadOnlyCollection<T> NonstoredEntities => _allEntities
            .Except(_storedEntities)
            .ToList();

        public T GetEntityFrom()
        {
            if (_storedEntities.Count == 0)
                return null;

            T entity = _storedEntities
                .First();

            _storedEntities.Remove(entity);
            PutOut?.Invoke(entity);

            return entity;
        }

        public void PutInEntity(T entity)
        {
            if (_allEntities.Contains(entity) == false)
                throw new ArgumentException(nameof(entity));

            _storedEntities.Add(entity ?? throw new ArgumentNullException(nameof(entity)));
            PutIn?.Invoke(entity);
        }

        public void PutInAllUnstoredEntities()
        {
            if (NonstoredEntities.Count == 0)
                return;

            foreach (T entity in NonstoredEntities)
                PutInEntity(entity);
        }

        public void RemoveStoredEntity()
        {
            T entity = _storedEntities
                .First();

            _storedEntities.Remove(entity);
            _allEntities.Remove(entity);
            Removed?.Invoke(entity);
        }

        public void RemoveEntity()
        {
            T entity = _allEntities
                .First();

            if (_storedEntities.Contains(entity))
                _storedEntities.Remove(entity);

            _allEntities.Remove(entity);
            Removed?.Invoke(entity);
        }

        public void ClearStoredEntities()
        {
            if (_storedEntities.Count == 0)
                return;

            foreach (T entity in _storedEntities)
                Removed?.Invoke(entity);

            _allEntities = NonstoredEntities
                .ToList();

            _storedEntities.Clear();
        }

        public void ClearAllEntities()
        {
            if (_allEntities.Count == 0)
                return;

            foreach (T entity in _allEntities)
                Removed?.Invoke(entity);

            _storedEntities.Clear();
            _allEntities.Clear();
        }

        private List<T> CreateEntities(Func<T> created, in int count)
        {
            List<T> entities = new List<T>();

            for (int i = 0; i < count; i++)
                entities.Add(created.Invoke());

            return entities;
        }
    }
}
