using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KirbyGame
{
    public class UIController : MonoBehaviour
    {
        public TextMeshProUGUI keyNumber;
        public GameObject[] hearts = new GameObject[3];

        private GameObject pauseScreen;

        private void Start()
        {
            pauseScreen = GameObject.Find("Pause Screen");
            pauseScreen.SetActive(false);
        }

        public void PauseScreenToggle()
        {
            if (pauseScreen.activeInHierarchy)
            {
                pauseScreen.SetActive(false);
            }
            else
            {
                pauseScreen.SetActive(true);
            }
        }

        public void GameOver()
        {
            SceneManager.LoadScene("Game_over_screen");
        }
        

        public void UpdateKeys(int K)
        {
            keyNumber.text = K.ToString();
        }

        public void UpdateHearts(int H)
        {
            hearts[H].SetActive(false);
        }
    }
}