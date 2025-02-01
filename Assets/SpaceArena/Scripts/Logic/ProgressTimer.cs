using Michsky.MUIP;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YG;
using static Assets.Scripts.Logic.ProgressTimer;

namespace Assets.Scripts.Logic
{
    public class ProgressTimer: MonoBehaviour
    {
        public ProgressBar Progress;
        public Image ProgressBarImage;
        [Range(0f, 300f)]
        public float MaxActiveTime = 30f;
        [Range(0f, 300f)]
        public float CoolDown = 60f;
        public Color ReadyColor;
        public Color ActiveColor;
        public Color CoolDownColor;        
        public Button StartTimerButton;
        public string rewardID = "";

        private ProgressTimerState _timerState = ProgressTimerState.Stopped;
        private float _currentCoolDown = 0f;

        public UnityEvent onActivate;
        public UnityEvent onDeactivate;

        
        private void Start()
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
            YG2.RewardedAdvShow(rewardID, () => {
                _timerState = ProgressTimerState.Active;
                _currentCoolDown = MaxActiveTime;
                StartTimerButton.gameObject.SetActive(false);
                ProgressBarImage.color = ActiveColor;
                onActivate?.Invoke();
            });
            
        }

        private void StartTimerCooldown()
        {
            _timerState = ProgressTimerState.CoolDown;
            _currentCoolDown = CoolDown;
            ProgressBarImage.color = CoolDownColor;
            onDeactivate?.Invoke();
        }

        private void StopTimer()
        {
            _timerState = ProgressTimerState.Stopped;
            _currentCoolDown = 0;
            Progress.SetValue(100);
            Progress.SetText("");
            ProgressBarImage.color = ReadyColor;
            StartTimerButton.gameObject.SetActive(true);
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
