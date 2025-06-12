using System.Collections.Generic;

public class Observer<T>
{
    private List<IObservable<T>> observables = new List<IObservable<T>>();

    public void AddObservable(IObservable<T> observer)
    {
        observables.Add(observer);
    }
    public void RemoveObservable(IObservable<T> observer)
    {
        var index = observables.IndexOf(observer);
        if (index > -1)
        {
            observables.RemoveAt(index);
        }
    }
    public void Notify(T value)
    {
        for (int i = 0; i < observables.Count; i++)
        {
            observables[i].OnNotify(value);
        }
    }
}