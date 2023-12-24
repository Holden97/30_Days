//使用utf-8
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace CommonBase
{
    public class ToggleGroup : MonoBehaviour, IToggleGroup
    {
        public List<IToggle> toggles;

        public Sprite toggleSelected;
        public Sprite toggleUnselected;
        public string groupTag;

        public void SelectToggle(IToggle toggle)
        {
            toggle.IsToggle = true;
            toggle.OnToggleSelect();
            foreach (IToggle t in toggles)
            {
                if (t != toggle)
                {
                    t.OnToggleUnselect();
                    t.IsToggle = false;
                }
            }
        }

        /// <summary>
        /// 初始化
        /// 初始化顺序:toggles数据确定=>为toggles绑定回调
        /// </summary>
        public void Initialize()
        {
            var oldToggles = toggles;
            toggles = SetToggles(groupTag);
            foreach (IToggle item in toggles)
            {
                if (oldToggles != null && oldToggles.Contains(item))
                {
                    continue;
                }
                Button b = (item as Button);
                if (b != null)
                {
                    b.onClick.AddListener(() =>
                    {
                        SelectToggle(item);
                    });
                }
            }
            SelectFirst();
        }

        public void SelectFirst()
        {
            if (toggles.Count > 0)
            {
                SelectToggle(toggles[0]);
            }
        }

        public virtual List<IToggle> SetToggles(string tag = null)
        {
            if (tag == null)
            {
                return GetComponentsInChildren<IToggle>().ToList();
            }
            else
            {
                return GetComponentsInChildren<IToggle>().ToList().FindAll(x => x.ToggleTag == tag);
            }
        }

        public List<IToggle> GetToggles(List<IToggle> original, string tag)
        {
            var result = new List<IToggle>();
            if (original != null)
            {
                return original.FindAll(x => x.ToggleTag == tag);
            }
            else
            {
                return null;
            }

        }
    }
}