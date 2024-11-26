#if UNITY_5_3_OR_NEWER
using System.Collections.Generic;
using UnityEngine;

namespace WindowFocuser.Unity
{
    public class WindowFocuser : MonoBehaviour
    {
        [SerializeField] private float _focusInterval = 30f;
        [Tooltip("The focusing process continues until a window is found from the top of the list.")]
        [SerializeField] private List<string> _targetProcessNames = new List<string>() { "notepad" };
        
        private float _timer = 0f;

        private void Update()
        {
            _timer += Time.unscaledDeltaTime;
            
            // Reset the timer if any key / mouse button is pressed
            if (Input.anyKey || Input.GetMouseButton(0) || Input.GetMouseButton(1) || Input.GetMouseButton(2))
            {
                _timer = 0f;
            }
            
            if (_timer > _focusInterval)
            {
                _timer = 0f;
                foreach (string targetProcessName in _targetProcessNames)
                {
                    if (WindowUtility.FocusWindow(targetProcessName)) break;
                }
            }
        }
    }
}
#endif