using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class BallsController : MonoBehaviour
    {
        public static BallsController instance;

        [SerializeField]
        private GameObject menuBall, purchaseButton;

        [SerializeField]
        private Sprite[] balls;

        [SerializeField]
        private int currentBall;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        private void Start()
        {
            PrepareBackgrounds();
            if (GameController.instance.IsPaidUser()) {
                purchaseButton.SetActive(false);
            }
        }
        private void PrepareBackgrounds()
        {
            if (!GameController.instance.IsBasketBallUnlocked())
            {
                balls[(int)Balls.Basket] = null;
            }
            if (!GameController.instance.IsBeachBallUnlocked())
            {
                balls[(int)Balls.Beach] = null;
            }
            if (!GameController.instance.IsSoccerBallUnlocked())
            {
                balls[(int)Balls.Soccer] = null;
            }
        }

        public int CycleBalls()
        {
            var possibilities = balls.Where(b => b != null);
            if (possibilities.Count() > 0)
            {
                currentBall++;
                if (currentBall < balls.Length)
                {
                    if (balls[currentBall] != null)
                    {
                        SelectBall(currentBall);
                    }
                    else
                    {
                        currentBall = CycleBalls();
                    }
                }
                else
                {
                    currentBall = 0;
                    SelectBall(currentBall);
                }
            }
            return currentBall;
        }

        public void SelectBall(int selectedBall)
        {
            if (currentBall != selectedBall)
            {
                currentBall = selectedBall;
            }

            if (selectedBall < balls.Length && balls[selectedBall] != null)
            {
                menuBall.GetComponent<SpriteRenderer>().sprite = balls[selectedBall];

            }
        }
    }
}
