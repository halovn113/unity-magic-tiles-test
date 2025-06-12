public interface IObservable<T>
{
    void OnNotify(T value);
}