using CommonBase;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace OfficeWar
{
    /// <summary>
    /// 人物初始准备面板
    /// </summary>
    public class PreparePanel : BaseUI
    {
        public WeaponData initialWeapon;
        public List<WeaponData> candidateWeapons;

        public CommonList weaponList;
        public ToggleGroup weaponGroup;

        private void Awake()
        {
        }

        public override void UpdateView(object o)
        {
            base.UpdateView(o);
            var weapons = o as List<WeaponData>;
            weaponList.BindData(weapons);
            weaponGroup.Initialize();
        }

        public void StartGame()
        {

            SceneManager.LoadScene("Battle");
        }
    }
}
