using System;
using System.Collections.Generic;

namespace J.ObserverPattern
{
    abstract class Subject : IObserverRegister
    {
        readonly List<Observer> observers;

        public Subject()
        {
            observers = new List<Observer>();
        }

        void IObserverRegister.Register(Observer observer)
        {
            if (observers.Contains(observer))
            {
                Console.WriteLine("Observer has registered: " + observer.ToString());
                return;
            }
            observers.Add(observer);
        }

        void IObserverRegister.Remove(Observer observer)
        {
            if (!observers.Remove(observer))
            {
                Console.WriteLine("Observer not exit: " + observer.ToString());
            }
        }

        public virtual void Update()
        {
            foreach (var item in observers)
                item.Update();
        }
    }
}