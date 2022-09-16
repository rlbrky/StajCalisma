using System;
using System.Collections.Generic;

public class GenericPool<T>
{
    Func<T> factoryFunc;
    Action<T> turnOff;
    public List<T> myPool = new List<T>();
    public List<T> register = new List<T>();

    public GenericPool(Func<T> factoryFunc,Action<T> turnOff,int poolSize)
    {
        this.factoryFunc = factoryFunc;
        this.turnOff = turnOff;
        for(int i = 0; i < poolSize; i++)
        {
            var funcHolder = factoryFunc();
            turnOff(funcHolder);
            myPool.Add(funcHolder);
        }
    }

    public T Get()
    {
        if(myPool.Count > 0)
        {
            T temp = myPool[myPool.Count - 1];
            myPool.RemoveAt(myPool.Count - 1);
            return temp;
        }
        return default(T);
    }

    public void Return(T something)
    {
        turnOff(something);
        myPool.Add(something);
    }
}