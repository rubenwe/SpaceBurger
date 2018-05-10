using System;

namespace Scritps.ReactiveScripts
{
    public class Observer
    {
        public Action Action;

        public Observer(Action action)
        {
            Action = action;
        }
    }
}