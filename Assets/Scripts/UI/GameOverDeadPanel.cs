using CommonBase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OfficeWar
{
    public class GameOverDeadPanel : BaseUI
    {
        public void OnClickBackToMainMenu()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
