using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KirbyGame
{
    public class ButtonHandler : MonoBehaviour
    {
        public UIController _uiController;

        private float originalFixedTime;

        private void Start()
        {
            _uiController = GameObject.Find("Canvas").GetComponent<UIController>();
            originalFixedTime = Time.fixedUnscaledDeltaTime;
        }

        public void PauseButton()
        {
            PauseToggle(true);
        }

        public void ResumeButton()
        {
            PauseToggle(false);
        }

        public void ReturnToMenu()
        {
            StartTime();
            SceneManager.LoadScene("Title_screen");
        }

        public void StartGame()
        {
            SceneManager.LoadScene("First_level");
        }

        public void GameOver()
        {
            SceneManager.LoadScene("Game_over_screen");
        }

        public void CreditsScreen()
        {
            SceneManager.LoadScene("Credits");
        }

        public void ExitButton()
        {
            Application.Quit();
        }

        private void PauseToggle(bool is_running)
        {
            Debug.Log("fixed delta time " + Time.fixedDeltaTime);
            if (is_running)
            {
                Time.timeScale = 0f;
                Time.fixedDeltaTime = 0f;
            }
            else
            {
                Time.timeScale = 1f;
                Time.fixedDeltaTime = originalFixedTime;
            }

            Debug.Log("time Scale " + Time.timeScale);

            _uiController.PauseScreenToggle();
        }

        private void StartTime()
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = originalFixedTime;
        }
    }
}