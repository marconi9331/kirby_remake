using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace KirbyGame
{
    public class PlayerController : MonoBehaviour
    {
        private Vector2 acceleInput;
        public Animator playerAnimator;
        private Transform playerTrans;
        private Rigidbody2D playerRB;
        private Collider2D playerCollider2D;
        private SpriteRenderer playerSprite;
        private int health;
        private int keysInInventory;
        public float speedMultiplier;
        private float originalDrag;
        private const float invincibiltyTimer = 1.5f;
        private float lastHit = 1f;
        private DoorController DoorController;
        private UIController _uiController;

        private void Start()
        {
            health = GameConstants.PLAYER_HEALTH;
            keysInInventory = 0;
            Screen.orientation = ScreenOrientation.Portrait;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            playerAnimator = GetComponent<Animator>();
            playerTrans = GetComponent<Transform>();
            playerRB = GetComponent<Rigidbody2D>();
            originalDrag = playerRB.drag;
            playerCollider2D = GetComponent<Collider2D>();
            playerSprite = GetComponent<SpriteRenderer>();
            _uiController = GameObject.Find("Canvas").GetComponent<UIController>();
            _uiController.UpdateKeys(keysInInventory);
        }

        private void Update()
        {
            playerAnimator.speed = Mathf.Clamp(playerRB.velocity.sqrMagnitude / 4, 0, 2);
            acceleInput = new Vector2(Input.acceleration.x * speedMultiplier, Input.acceleration.y * speedMultiplier);
            InvencibilityFrames();
            if (health <= 0)
            {
                GameOver();
            }
        }

        private void FixedUpdate()
        {
            playerRB.AddForce((acceleInput * Time.fixedDeltaTime), ForceMode2D.Impulse);
            playerRB.AddForce(
                (new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * Time.fixedDeltaTime) *
                speedMultiplier / 2, ForceMode2D.Impulse);
//          rb.velocity = Vector2.ClampMagnitude(rb.velocity, GameConstants.PLAYER_SPEED_MAX);
            playerTrans.rotation = Quaternion.LookRotation(Vector3.back, playerRB.velocity);
        }


        private void OnTriggerEnter2D(Collider2D other)
        {
            switch (other.gameObject.tag)
            {
                case GameConstants.DOOR_TAG:
                    if (keysInInventory > 0)
                    {
                        DoorOpener(other.gameObject);
                        keysInInventory--;
                    }

                    _uiController.UpdateKeys(keysInInventory);

                    break;
                case GameConstants.COLLECTIBLE_TAG:
                    DeactivateObject(other.gameObject);
                    keysInInventory++;
                    _uiController.UpdateKeys(keysInInventory);
                    break;
                case GameConstants.HAZARD_TAG:
                    if (!InvencibilityFrames())
                    {
                        HitDetector();
                        UpdateHealth();
                    }

                    break;
                case GameConstants.ENEMY_BULLET_TAG:
                    if (!InvencibilityFrames())
                    {
                        HitDetector();
                        UpdateHealth();
                    }

                    break;
                case GameConstants.ENEMY_TAG:
                    if (!InvencibilityFrames() && other.gameObject.name != "Squid" && other.gameObject.name != "Squid (1)")
                    {
                        HitDetector();
                        UpdateHealth();
                    }

                    break;

                case GameConstants.FINISH_TAG:
                    SceneManager.LoadScene("Credits");
                    break;

                case GameConstants.SAND_TAG:
//                    if (other.gameObject.CompareTag(GameConstants.SAND_TAG) && other.bounds.Intersects(playerCollider2D.bounds))
//                    {
                    playerRB.drag = 20F;
//                    }
                    break;
            }
        }

//        private void OnTriggerStay2D(Collider2D other)
//        {
//            if (other.gameObject.CompareTag(GameConstants.SAND_TAG) && other.bounds.Intersects(playerCollider2D.bounds))
//            {
//                playerRB.drag = 20F;
//                Debug.Log("Stuck");
//            }
//        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.CompareTag(GameConstants.GROUND_TAG) && !other.bounds.Intersects(playerCollider2D.bounds))
            {
                Debug.Log("saiu");
                GameOver();
            }

            if (other.gameObject.CompareTag(GameConstants.SAND_TAG) && !other.bounds.Intersects(playerCollider2D.bounds))
            {
                playerRB.drag = originalDrag;
                Debug.Log("free");
            }
        }

        private void GameOver()
        {
            SceneManager.LoadScene("Game_over_screen");
        }

        private bool InvencibilityFrames()
        {
            if (lastHit < invincibiltyTimer)
            {
                lastHit += Time.deltaTime;
                playerSprite.color = new Color(1, 1, 1, 0.5f);
                return true;
            }
            else
            {
                playerSprite.color = new Color(1, 1, 1, 1);
                return false;
            }
        }

        private void UpdateHealth()
        {
            health--;
            _uiController.UpdateHearts(health);
        }


        private void HitDetector()
        {
            lastHit = 0;
        }

        private void DeactivateObject(GameObject go)
        {
            go.SetActive(false);
        }

        private void DoorOpener(GameObject Door)
        {
            if (keysInInventory >= 1)
            {
                Vector3 DoorPos = Door.transform.position;
                DeactivateObject(Door);
                RaycastHit2D doorToLeft = Physics2D.Raycast(DoorPos, Vector3.left, 2f, 1 << 14);
                RaycastHit2D doorToRight = Physics2D.Raycast(DoorPos, Vector3.right, 2f, 1 << 14);

                if (doorToLeft != false)
                {
                    DoorOpener(doorToLeft.collider.gameObject);
                }

                if (doorToRight != false)
                {
                    DoorOpener(doorToRight.collider.gameObject);
                }
            }
        }
    }
}