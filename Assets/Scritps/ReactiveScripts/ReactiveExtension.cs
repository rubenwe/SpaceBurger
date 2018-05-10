using System;

namespace Scritps.ReactiveScripts
{
    public static class ReactiveExtension
    {
        public static void Subscribe<T>(this ReactiveProperty<T> reactiveProperty, Action action)
        {
            reactiveProperty.Observers.Add(new Observer(action));
        }

        public static void UnubscribeAll<T>(this ReactiveProperty<T> reactiveProperty)
        {
            reactiveProperty.Observers.Clear();
        }
    }
}