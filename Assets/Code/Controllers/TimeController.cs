using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Controllers
{
    public sealed class TimeController: IOnController, IOnUpdate
    {
        private float _endTime;
        private bool _isTimerOn;
        private bool _timerIsDone;
        //TODO: Посмотреть у Романа
        //TODO: https://github.com/RomanAstra/Patterns
        public void OnUpdate(float deltaTime)
        {
            if (_isTimerOn)
                    if (_endTime <= Time.time)
                    {
                        _isTimerOn = false;
                        _timerIsDone = true;
                    }
        }

        public void AddTimer(float secondsToTheEnd)
        {
            _isTimerOn = false;
            SetEndTime(secondsToTheEnd);
        }
        
        private void SetEndTime(float secondsToTheEnd)
        {
            _endTime = secondsToTheEnd+Time.time;
        }

        public void TurnOnTimer()
        {
            _isTimerOn = true;
            _timerIsDone = false;
        }

        public bool TimerIsDone()
        {
            return _timerIsDone;
        }
    }
}