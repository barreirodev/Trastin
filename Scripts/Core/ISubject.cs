using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public interface ISubject
    {
        void Subscribe(IObserver observer);
        void Unsubscribe(IObserver observer);
        void Notify();
    }
}