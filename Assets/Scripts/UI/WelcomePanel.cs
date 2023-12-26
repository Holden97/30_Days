using CommonBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

namespace OfficeWar
{
    public class WelcomePanel : BaseUI
    {
        [Header("背景")]
        public GameObject BG;
        [Header("Start")]
        public GameObject start;
        [Header("DevelopmentList")]
        public GameObject developmentList;

        public void StartGame()
        {
            UIManager.Instance.ShowPanel<PreparePanel>(data: GameManager.Instance.weaponsData);
        }

        public void ExitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }


        /// <summary>
        /// 开发人员名单
        /// </summary>
        public void DevelopmentList()
        {
            start.SetActive(false);

            BG.transform.DOScale(new Vector3(2, 2, 1), 1f);
            BG.transform.DOLocalMove(new Vector3(-360, -540, 0), 1f);

            Invoke("active", 1f);
        }
        void active()
        {
            developmentList.SetActive(true);
        }
        public void CloseIt()
        {
            start.SetActive(true);
            developmentList.SetActive(false);

            BG.transform.DOScale(new Vector3(1, 1, 1), 1f);
            BG.transform.DOLocalMove(new Vector3(0, 0, 0), 1f);
        }
    }
}
