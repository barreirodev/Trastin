using System;
using UnityEngine;
using Box;

[CreateAssetMenu]
public class EventBus : ScriptableObject
{
    public event Action<BoxMediator> Event;
    public void NotifyEvent(BoxMediator box) => Event?.Invoke(box);

}
