using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class CountDownTimer : MonoBehaviour
    {
        public int secondsLeft = 60;
        private TMP_Text timerText;
        private bool takeAway;
        void Start()
        {

            timerText = GetComponent<TMP_Text>();
            timerText.text = "00:" + secondsLeft;
        }

        void Update()
        {
            if (takeAway == false && secondsLeft > 0)
            {
                StartCoroutine(TakeAway());
            }
        }

        public IEnumerator TakeAway()
        {
            takeAway = true;
            yield return new WaitForSeconds(1f);
            secondsLeft -= 1;
            if (secondsLeft < 10)
            {
                timerText.text = "00:0" + secondsLeft;
            }
            else
            {
                timerText.text = "00:" + secondsLeft;
            }

            takeAway = false;
        }
    }
}
