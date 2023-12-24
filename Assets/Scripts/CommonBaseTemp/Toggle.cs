//使用utf-8
using UnityEngine.Events;
using UnityEngine.UI;

namespace CommonBase
{
    public class Toggle : Button, IToggle
    {
        public UnityEvent unselectEvent;
        public UnityEvent selectEvent;

        public string toggleTag;
        private string id;

        public bool isToggled;

        public bool IsToggle { get { return isToggled; } set { isToggled = value; } }
        public string ID { get => id; set { id = value; } }

        public string ToggleTag => toggleTag;

        public void AddSelectedAction(UnityAction<IToggle> action)
        {
            selectEvent.AddListener(() => action(this));
        }


        public void AddUnselectedAction(UnityAction<IToggle> action)
        {
            unselectEvent.AddListener(() => action(this));
        }

        public void OnToggleSelect()
        {
            selectEvent?.Invoke();
        }

        public void OnToggleUnselect()
        {
            unselectEvent?.Invoke();
        }
    }
}