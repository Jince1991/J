namespace J.ObserverPattern
{
    interface IObserverRegister
    {
        void Register(Observer observer);
        void Remove(Observer observer);
    }
}