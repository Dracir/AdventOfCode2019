using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T>
{
    private Stack<T> _pool = new Stack<T>();
    private Func<T> _instantiator;

    public ObjectPool(int initialSize = 10, Func<T> instantiator = null)
    {
        _instantiator = instantiator;
        for (int i = 0; i < initialSize; i++)
            CreateObjectAndAddToPool();
    }

    private void CreateObjectAndAddToPool()
    {
        if (_instantiator == null)
            _pool.Push(default(T));
        else
            _pool.Push(_instantiator());
    }

    public T Pop()
    {
        if (_pool.Count == 0)
            CreateObjectAndAddToPool();
        return _pool.Pop();
    }
}

