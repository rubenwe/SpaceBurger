using System.Collections.Generic;
using Scritps.Player;

namespace Scritps.ReactiveScripts
{
    public class ReactiveProperty<T>
    {
        public ReactiveProperty(T value)
        {
            _value = value;
        }

        public readonly List<Observer> Observers = new List<Observer>();

        private T _value;

        public T Value
        {
            get { return _value; }
            set { SetValue(value); }
        }

        private void SetValue(T value)
        {
            _value = value;
            NotifyAllObservers();
        }

        private void NotifyAllObservers()
        {
            foreach (var observer in Observers)
            {
                observer.Action.Invoke();
            }
        }
    }
}