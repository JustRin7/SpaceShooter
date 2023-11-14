using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class ResultPanelController : SingletonBase<ResultPanelController>
    {
        [SerializeField] private Text m_Kills;
        [SerializeField] private Text m_Score;
        [SerializeField] private Text m_Time;

        [SerializeField] private Text m_Result;

        [SerializeField] private Text m_ButtonNextText;

        private bool m_Success;//определяет поведение кнопки, зависит от успешности завершения лвл

        private void Start()
        {
            gameObject.SetActive(false);
        }

        public void ShowResults(PlayerStatistics levelResults, bool success)
        {
            gameObject.SetActive(true);//показываем окно с рез-тами

            m_Success = success;

            m_Result.text = success ? "Win" : "Lose";

            m_ButtonNextText.text = success ? "Next" : "Restart";

            m_Kills.text = "Kills : " + levelResults.numKills.ToString();
            m_Score.text = "Score : " + levelResults.score.ToString() + " X " + levelResults.scorescoreMultiplier.ToString();
            m_Time.text = "Time : " + levelResults.time.ToString();

            Time.timeScale = 0;
        }



        public void OnButtonNextAction()
        {
            gameObject.SetActive(false);

            Time.timeScale = 1;

            if(m_Success)//если уровень прошел успешно
            {
                LevelSequenceController.Instance.AdvanceLevel();
            }
            else
            {
                LevelSequenceController.Instance.Reset();
                LevelSequenceController.Instance.RestartLevel();
            }
        }


    }
}
