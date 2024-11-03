using System;

public interface ITimer
{
    event Action TimerFinished;
    void StartTimer(float duration);
}