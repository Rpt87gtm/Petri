using UnityEngine;

public class TimerTest : MonoBehaviour
{
    private SpriteRenderer _sprite;

    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ITimer timer = MonoTimer.CreateTimer(gameObject);
            timer.StartTimer(2f);
            timer.TimerFinished += OnTimerFinished;
        }
    }

    private void OnTimerFinished()
    {
        if (_sprite != null)
        {
            _sprite.color = new Color(Random.value, Random.value, Random.value);
        }
        Debug.Log("MonoTimer finished!");
    }
}