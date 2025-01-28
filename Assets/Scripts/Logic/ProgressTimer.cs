using Michsky.MUIP;
using UnityEngine;

namespace Assets.Scripts.Logic
{
    public class ProgressTimer: MonoBehaviour
    {
        public ProgressBar Progress;
        [Range(0f, 300f)]
        public float MaxActiveTime = 30f;
        [Range(0f, 300f)]
        public float CoolDown = 60f;

        private ProgressTimerState _timerState = ProgressTimerState.Stopped;
        public float _currentCoolDown = 0f;

        
        private void Awake()
        {
            StopTimer();
        }

        private void Update()
        {
            if (IsTimerTicking())
            {
                TickTimer();
            }
        }

        public void OnTimerClick()
        {
            if (!IsStopped()) return;

            StartTimer();
        }

        private void TickTimer()
        {
            _currentCoolDown -= Time.deltaTime;
            UpdateTimerStatus();

            if (IsActive())
            {
                Progress.SetValue(_currentCoolDown * 100 / MaxActiveTime);
                Progress.SetText(Mathf.FloorToInt(_currentCoolDown + 1).ToString());
            } else if(IsCoolDown())
            {
                Progress.SetValue(100 - _currentCoolDown * 100 / CoolDown);
                Progress.SetText(Mathf.FloorToInt(_currentCoolDown + 1).ToString());
            }
        }

        private void UpdateTimerStatus()
        {
            if (_currentCoolDown < 0f)
            {
                _currentCoolDown = 0f;

                if (IsActive())
                {
                    StartTimerCooldown();
                }
                else if (IsCoolDown())
                {
                    StopTimer();
                }
            }
        }

        private bool IsCoolDown()
        {
            return _timerState == ProgressTimerState.CoolDown;
        }

        private bool IsActive()
        {
            return _timerState == ProgressTimerState.Active;
        }

        private bool IsTimerTicking()
        {
            return (_timerState == ProgressTimerState.Active 
                        || _timerState == ProgressTimerState.CoolDown) 
                    && _currentCoolDown > 0;
        }

        private void StartTimer()
        {
            _timerState = ProgressTimerState.Active;
            _currentCoolDown = MaxActiveTime;
            Debug.Log(_timerState);
        }

        private void StartTimerCooldown()
        {
            _timerState = ProgressTimerState.CoolDown;
            _currentCoolDown = CoolDown;
            Debug.Log(_timerState);
        }

        private void StopTimer()
        {
            _timerState = ProgressTimerState.Stopped;
            _currentCoolDown = 0;
            Progress.SetValue(100);
            Progress.SetText(MaxActiveTime.ToString());
            Debug.Log(_timerState);
        }

        private bool IsStopped()
        {
            return _timerState == ProgressTimerState.Stopped;
        }
    }

    public enum ProgressTimerState
    {
        Stopped,
        CoolDown,
        Active
    }
}
