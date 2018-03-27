using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.GameScene
{
    //How tiles be destroyed , base score depends on it
    public enum DestroyType
    {
        Underwear,
        ThreeFace,
        TwoFace,
    }

    public class ScoreManager : Singleton<ScoreManager>
    {
        [SerializeField]
        Text scoreText;

        int currentScore = 0;
        int baseScore = 0;
        int combo = 0;

        public void Initialize()
        {
            currentScore = 0;
            UpdateScoreText();
        }

        public void InitializeCombo()
        {
            combo = 0;
        }

        public void AddCombo()
        {
            combo += 1;
        }

        public int GetBaseScore(DestroyType type)
        {
            switch (type)
            {
                case DestroyType.Underwear:
                    return  1;
                case DestroyType.TwoFace:
                    return  5;
                case DestroyType.ThreeFace:
                    return 10;
                default:
                    return 0;
            }
        }

        public void AddScore(DestroyType type)
        {
            currentScore += (int)(GetBaseScore(type) * Mathf.Pow(2, combo - 1));
        }

        public void UpdateScoreText()
        {
            scoreText.text = currentScore.ToString();
        }
    }
}
