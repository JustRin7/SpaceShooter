using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//работа со сценой
using UnityEngine.UI;

namespace SpaceShooter
{
    public class UISetPause : MonoBehaviour
    {
        [SerializeField] private GameObject PausedPanel;
        [SerializeField] private GameObject JoyStickPanel;
        [SerializeField] private GameObject StatisticsPanel;
        public static string MainMenuSceneNickname = "main_menu";//имя сцены главного меню

        [SerializeField] private Text ScoreText;
        [SerializeField] private Text KillsText;
        [SerializeField] private Text TimeText;

        private bool isPaused = false;

        private int time;








        




        private void Start()
        {
            Time.timeScale = 1f;
        }





        public void Update()
        {
            time = (int)LevelController.Instance.LevelTime;

            ScoreText.text = "Score: " + Player.Instance.Score.ToString();
            KillsText.text = "Kills: " + Player.Instance.NumKills.ToString();
            TimeText.text = "Time: " + time.ToString();

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }

        }






        public void Resume()
        {
            if (MovementController.controlModeMobile == true)
            {
                JoyStickPanel.SetActive(true);
            }            
            PausedPanel.SetActive(false);
            StatisticsPanel.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }

        public void Pause()
        {
            if (MovementController.controlModeMobile == true)
            {
                JoyStickPanel.SetActive(false);
            }                
            PausedPanel.SetActive(true);
            StatisticsPanel.SetActive(false);
            Time.timeScale = 0f;
            isPaused = true;
        }


        public void ResumeButtom()
        {
            if (MovementController.controlModeMobile == true)
            {
                JoyStickPanel.SetActive(true);
            }                
            PausedPanel.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }

        public void ExitButtom()
        {
            Application.Quit();
        }


        public void StatisticsButton()
        {
            StatisticsPanel.SetActive(true);
            PausedPanel.SetActive(false);
            JoyStickPanel.SetActive(false);
        }


        public void BackToMenuButton()
        {
            StatisticsPanel.SetActive(false);
            PausedPanel.SetActive(true);
            JoyStickPanel.SetActive(false);
        }


        public void MainMenuButton()
        {
            SceneManager.LoadScene(MainMenuSceneNickname);
        }

        
    }
}






