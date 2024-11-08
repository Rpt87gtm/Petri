using Mirror;
using System;
using System.Collections.Generic;
using UnityEngine;

public class NetworkQueuePool<T> where T : NetworkBehaviour
{
    private T _prefab;
    private bool _autoExpand;
    private Transform _container;
    private Queue<T> _pool;

    public NetworkQueuePool(T prefab, bool autoExpand = true, int count = 0, Transform container = null)
    {
        _prefab = prefab ?? throw new ArgumentNullException(nameof(prefab));
        _autoExpand = autoExpand;
        _container = container;
        _pool = new Queue<T>(count);
        CreatePool(count);
    }

    private void CreatePool(int count)
    {
        for (int i = 0; i < count; i++)
        {
            T createdObject = CreateGameObject();
            _pool.Enqueue(createdObject);
        }
    }

    private T CreateGameObject(bool isActiveByDefault = false)
    {
        var createdObject = UnityEngine.Object.Instantiate(_prefab, _container);
        createdObject.gameObject.SetActive(isActiveByDefault);
        NetworkServer.Spawn(createdObject.gameObject);
        return createdObject;
    }

    public T GetFreeElement(bool isActive = true)
    {
        if (_pool.Count > 0)
        {
            T element = _pool.Dequeue();
            element.gameObject.SetActive(isActive);
            return element;
        }

        if (_autoExpand)
        {
            T newElement = CreateGameObject(isActive);
            return newElement;
        }

        throw new PoolEmptyException($"No free elements in pool of type {typeof(T)}");
    }

    public void ReturnToPool(T element)
    {
        element.gameObject.SetActive(false);
        _pool.Enqueue(element);
    }

    public int CountFreeElements()
    {
        return _pool.Count;
    }
}


