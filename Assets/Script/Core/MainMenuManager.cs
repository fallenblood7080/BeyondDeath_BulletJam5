using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BulletJam.Core
{
    public class MainMenuManager : MonoBehaviour
    {
        public void PlayGame()
        {
            GameManager.Instance.Play(0);
        }

        public void Quit()
        {
            Application.Quit();
        }

        public void MainMenu()
        {
            GameManager.Instance.MainMenu();
        }

        public void Retry(int index)
        {
            GameManager.Instance.Play(index);
        }
    }
}