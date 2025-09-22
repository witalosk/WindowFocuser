#if UNITY_5_3_OR_NEWER
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace WindowFocuser.Unity
{
    public enum SearchMethod
    {
        ByProcessName,
        ByWindowTitle,
    }

    /// <summary>
    /// Focuses on the window of the specified process at regular intervals.
    /// </summary>
    public class WindowFocuser : MonoBehaviour
    {
        public float FocusIntervalSec { get => _focusInterval; set => _focusInterval = value; }
        public List<string> TargetProcessNames { get => _targetNames; set => _targetNames = value; }

        [SerializeField] private float _focusInterval = 30f;
        [SerializeField] private SearchMethod _searchMethod = SearchMethod.ByProcessName;
        [FormerlySerializedAs("_targetProcessNames")]
        [Tooltip("The focusing process continues until a window is found from the top of the list.")]
        [SerializeField] private List<string> _targetNames = new List<string>() { "notepad" };

        private double _timer = 0d;

        private void Start()
        {
            Focus();
        }

        private void Update()
        {
            _timer += Time.unscaledDeltaTime;

            // Reset the timer if any key / mouse button is pressed
            if (Input.anyKey || Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2))
            {
                _timer = 0d;
            }

            if (_timer > _focusInterval)
            {
                _timer = 0d;
                Focus();
            }
        }

        private void Focus()
        {
            foreach (string targetProcessName in _targetNames)
            {
                if (_searchMethod == SearchMethod.ByWindowTitle)
                {
                    if (WindowUtility.FocusWindowByWindowName(targetProcessName)) break;
                }
                else
                {
                    if (WindowUtility.FocusWindowByProcessName(targetProcessName)) break;
                }
            }
        }
    }
}
#endif