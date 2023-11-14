using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace SpaceShooter
{
    public class LevelSequenceController : SingletonBase<LevelSequenceController>
    {
        public static string MainMenuSceneNickname = "main_menu";//��� ����� �������� ����

        public Episode CurrentEpisode { get; private set; }

        public int CurrentLevel { get; private set; }//������ � ����� ������� ���� ������� �������


        public bool LastLevelResult { get; private set; }

        public PlayerStatistics LevelStatistics { get; private set; }

        public static SpaceShip PlayerShip { get; set; }



        public void StartEpisode(Episode e)
        {
            CurrentEpisode = e;
            CurrentLevel = 0;

            ///���������� ����� ����� ������� �������

            LevelStatistics = new PlayerStatistics();
            LevelStatistics.Reset();

            SceneManager.LoadScene(e.Levels[CurrentLevel]);
        }



        /// <summary>
        /// �������������� ������� ������
        /// </summary>
        public void RestartLevel()
        {
            LevelStatistics.Reset();
        }


        public void Reset()
        {
            SceneManager.LoadScene(CurrentLevel);
        }


        public void FinishCurrentLevel(bool succes)
        {
            LastLevelResult = succes;

            CalculateLevelStatistic();


            if (LevelStatistics.time < 5)
            {
                LevelStatistics.scorescoreMultiplier = 4;
            }

            if (LevelStatistics.time >= 5 && LevelStatistics.time < 7)
            {
                LevelStatistics.scorescoreMultiplier = 2;
            }

            if (LevelStatistics.time > 10)
            {
                LevelStatistics.scorescoreMultiplier = 1;
            }

            ResultPanelController.Instance.ShowResults(LevelStatistics, succes);
        }


        /// <summary>
        /// ���������� ������
        /// </summary>
        public void AdvanceLevel()
        {
            LevelStatistics.Reset();

            CurrentLevel++;

            if(CurrentEpisode.Levels.Length <= CurrentLevel)
            {
                SceneManager.LoadScene(MainMenuSceneNickname);
            }
            else
            {
                SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
            }
        }


        private void CalculateLevelStatistic()
        {
            LevelStatistics.score = Player.Instance.Score;
            LevelStatistics.numKills = Player.Instance.NumKills;
            LevelStatistics.time = (int)LevelController.Instance.LevelTime;

        }


    }
}
