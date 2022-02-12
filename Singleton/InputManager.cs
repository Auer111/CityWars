using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class InputManager : MonoBehaviour
    {
        public static UnityEvent m_panStart;
        public static UnityEvent m_panEnd;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0)){
                m_panStart.Invoke();
            }
        }

    }
}
