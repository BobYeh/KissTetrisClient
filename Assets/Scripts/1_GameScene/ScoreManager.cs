﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace KissTetris.GameScene
{
    //How tiles be destroyed , base score depends on it
    public enum DestroyType
    {
        Underwear,
        Kiss
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

            if (combo > 2)
            {
                ComboViewManager.Instance.ShowCombo(combo - 2);
            }
        }

        public int GetBaseScore(DestroyType type, int faceNumber)
        {
            switch (type)
            {
                case DestroyType.Underwear:
                    return GameSettings.baseScoreForItems;
                case DestroyType.Kiss:
                    return GameSettings.baseScoreForEachFaces * faceNumber;
                default:
                    return 0;
            }
        }

        public int AddScore(DestroyType type, int faceNumber)
        {
            currentScore += (int)(GetBaseScore(type, faceNumber) * Mathf.Pow(2, combo - 1));
            return currentScore;
        }

        public void UpdateScoreText()
        {
            scoreText.text = currentScore.ToString();
        }

        public int CurrentScore
        {
            get
            {
                return currentScore;
            }

        }
    }
}
