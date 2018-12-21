using UnityEngine;

namespace KirbyGame
{
    public class SquidController : MonoBehaviour
    {
        private GameObject squidGo;
        private Animator squidAnim;
        private float ChargeTimer;
        private float AtkResetTimer;
        public GameObject[] Whirlpools;
        private Animator[] WhirlpoolsAN;

        private void Start()
        {
            squidGo = GetComponent<GameObject>();
            squidAnim = GetComponent<Animator>();
            WhirlpoolsAN = new Animator[Whirlpools.Length];

            for (int i = 0; i < Whirlpools.Length; i++)
            {
                WhirlpoolsAN[i] = Whirlpools[i].GetComponent<Animator>();
                ToggleWhirlpools(false);
            }
        }

        private void Update()
        {
            if (CoolDown())
            {
                squidAnim.ResetTrigger("Is charging");
                squidAnim.ResetTrigger("Charged");
                squidAnim.SetTrigger("Returned");
                ToggleWhirlpools(false);
            }
            else
            {
                CoolDown();
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag(GameConstants.PLAYER_TAG))
            {
                AtkResetTimer = 0f;
                if (Charged())
                {
                    squidAnim.ResetTrigger("Is charging");
                    squidAnim.SetTrigger("Charged");
                    ToggleWhirlpools(true);
                }
                else
                {
                    Charged();
                }
            }
        }

//        private void OnTriggerExit2D(Collider2D other)
//        {
//            if (other.CompareTag(GameConstants.PLAYER_TAG))
//            {
//                CoolDown();
//            }
//        }

        private void ToggleWhirlpools(bool activation)
        {
            foreach (var poolGo in Whirlpools)
            {
                poolGo.SetActive(activation);
            }
        }


//        IEnumerator Charge()
//        {
//            if (ChargeTimer < GameConstants.SQUID_CHARGE_TIME)
//            {
//                ChargeTimer += Time.deltaTime;
//            }
//            ToggleWhirlpools(true);
//            yield return null;
//        }

        private bool Charged()
        {
            if (ChargeTimer <= GameConstants.SQUID_CHARGE_TIME)
            {
                ChargeTimer += Time.deltaTime;
                squidAnim.ResetTrigger("Returned");
                squidAnim.SetTrigger("Is charging");
                return false;
            }

            return true;
        }

        private bool CoolDown()
        {
            if (AtkResetTimer <= GameConstants.SQUID_RESET_TIMER)
            {
                AtkResetTimer += Time.deltaTime;
                return false;
            }

            ChargeTimer = 0;
            return true;
        }
    }
}