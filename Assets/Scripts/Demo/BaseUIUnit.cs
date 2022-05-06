using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

#pragma warning disable IDE0051
#pragma warning disable IDE0079
#pragma warning disable IDE0090
#pragma warning disable IDE0003
#pragma warning disable IDE0044
//#pragma warning disable IDE0052

namespace Scripts.Demo
{
    /// <summary>
    /// Demo built in 20220506
    /// </summary>
    public class BaseUIUnit : MonoBehaviour
    {
        public class HoverETrigger : EventTrigger
        {
            private bool _hoverStatus;
            public bool HoverStatus { get => _hoverStatus; }

            public override void OnPointerEnter(PointerEventData eventData)
            {
                base.OnPointerEnter(eventData);
                _hoverStatus = true;
            }

            public override void OnPointerExit(PointerEventData eventData)
            {
                base.OnPointerExit(eventData);
                _hoverStatus = false;
            }
        }

        public class SelectETrigger : EventTrigger
        {
            private bool _selectedStatus;
            public bool SelectedStatus { get => _selectedStatus; }

            public delegate void clickDelegate();
            public clickDelegate clickTodo;

            public override void OnPointerClick(PointerEventData eventData)
            {
                base.OnPointerDown(eventData);
                if(clickTodo != null)
                    clickTodo.Invoke();
            }
            
        }

        private enum EVisualOption
        {
            /// <summary>
            /// Default visual option
            /// </summary>
            ScalingEye = 0,
        }

        [SerializeField]
        private EVisualOption eVisualOption = EVisualOption.ScalingEye;
        [SerializeField]
        private Vector2 vo_ScalingEye_target; // Using methods like these to save target data -- was ridiculous.
        [SerializeField, Range(0f, 1f)]
        private float vo_ScalingEye_lerpVar;

        private HoverETrigger _hoverEventTrigger;
        private SelectETrigger _selectEventTrigger;
        private Image _image;

        [SerializeField]
        private bool _hovered;
        public bool Hovered { get => _hovered; }
        private bool _selected;
        public bool Selected { get => _selected; }

        public void MountClickTask(SelectETrigger.clickDelegate clickTodo)
        {
            _selectEventTrigger.clickTodo = new SelectETrigger.clickDelegate(clickTodo);
        }

        private void Awake()
        {
            this._selectEventTrigger = gameObject.AddComponent<SelectETrigger>();

            this._hoverEventTrigger = gameObject.AddComponent<HoverETrigger>();
            if (gameObject.GetComponent<Image>())
                _image = gameObject.GetComponent<Image>();
            else
                throw new UnityException("Need to append an UI.Image Component to the GameObject named <" + gameObject.name + ">"); // Relax, just testing this.

            //data setting
            //vo_ScalingEye_target = new Vector2(1.2f, 1.2f);
            //vo_ScalingEye_lerpVar = 0.1f;
        }

        private void Start()
        {

        }

        void Update()
        {
            _hovered = this._hoverEventTrigger.HoverStatus;
            
            switch(eVisualOption)
            {
                case EVisualOption.ScalingEye:
                    if (_hovered)
                        _image.transform.localScale = Vector2.Lerp(_image.transform.localScale, vo_ScalingEye_target, vo_ScalingEye_lerpVar);
                    else
                        _image.transform.localScale = Vector2.Lerp(_image.transform.localScale, new Vector2(1f,1f), vo_ScalingEye_lerpVar);
                    break;
            }
        }
    }
}
