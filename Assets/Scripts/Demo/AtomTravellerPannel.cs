using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Scripts.Demo;
using UnityEngine.UI;
using System;

#pragma warning disable IDE0051
#pragma warning disable IDE0090

namespace Scripts.Demo
{
    public class AtomTravellerPannel : MonoBehaviour
    {
        private BaseUIUnit _unit;
        private TextMesh _textPlace;
        private bool _selectedActivated = false;


        private KeyCode _eKeyCode = new KeyCode();
        private bool[] _arrKeyboardDownStatus;
        private bool[] _arrKeyboardTackled;


        public void Click()
        {
            _selectedActivated = !_selectedActivated;
            if(_selectedActivated)
                gameObject.GetComponent<Image>().color = Color.gray;
            else
                gameObject.GetComponent<Image>().color = Color.white;

            //TODO: Need to acknowledge global selector
        }

        private void Awake()
        {
            _arrKeyboardDownStatus = new bool[System.Enum.GetValues(_eKeyCode.GetType()).Length];
            _arrKeyboardTackled    = new bool[System.Enum.GetValues(_eKeyCode.GetType()).Length];
            for(int i=0;i<_arrKeyboardDownStatus.Length;i++)
            {
                _arrKeyboardDownStatus[i] = false;
                _arrKeyboardTackled[i]    = false;
            }    
        }

        private void Start()
        {
            if (gameObject.GetComponent<BaseUIUnit>() != null)
                _unit = gameObject.GetComponent<BaseUIUnit>();
            else
                throw new UnityException("Need mount an UIUnit");
            _unit.MountClickTask(clickTodo: Click);
        }

        private void Update()
        {
            RefreshKeyboardStatus();
        }

        private void RefreshKeyboardStatus()
        {
            //string reportStr = "In the frame:" + Time.time.ToString() + "\n";
            //bool reportRequired = false;
            for (int i=0;i<_arrKeyboardDownStatus.Length;i++)
            {
                _arrKeyboardDownStatus[i] = Input.GetKey((KeyCode)i);
                
                if (_arrKeyboardDownStatus[i])
                {
                    if (_arrKeyboardTackled[i] == false)
                    {
                        _arrKeyboardTackled[i] = true;
                        //reportRequired = true;
                        //reportStr += "\n KeyPressed: " + (KeyCode)i + "; Value of i: " + i.ToString();
                        _arrKeyboardTackled[i] = true;
                    }
                }
                else
                {
                    _arrKeyboardTackled[i] = false;
                }
            }
            //if (reportRequired)
            //    print(reportStr);

            //Test result: no conflict keyboard's input can be detected in a single frame properly -- 20220507
            //中文在瞎讲: 出于贪婪, 我更希望其记录除了哪个键被"按下并持续按下", 这个逻辑之后可以用于更加精准的多键监控
        }

        private string KeyboardListener()
        {
            RefreshKeyboardStatus();
            throw new NotImplementedException();
            string ret = "";
            return ret;
        }
    }

}