using CommonBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OfficeWar
{
    public class WelcomePanel : BaseUI
    {
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
    }
}
