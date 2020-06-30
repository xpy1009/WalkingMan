using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour {

    public GameObject avatar;
    public float walkSpeed;
    public float runSpeed;

    private Animator animator;
    private Rigidbody2D rb;
    private Collider2D col;
    private Transform trans;

    public LayerMask mask;
    public float boxHeight;
    [Range (0, 10)] public float jumpSpeed;
    private Vector2 boxSize;
    private bool jumpRequest;
    private bool isGround = true;

    public Renderer spirteRenderer;
    [Range (0, 2)] public float blinkDuration;
    private bool isInvincible = false;
    private float timeSpentInvincible = 0.0f;

    private bool isMagicArea = false;

    // Start is called before the first frame update
    void Start () {
        animator = avatar.GetComponent<Animator> ();
        rb = this.GetComponent<Rigidbody2D> ();
        col = this.GetComponent<Collider2D> ();
        trans = this.transform;

        boxSize = new Vector2 (0.2f, boxHeight);
    }

    // Update is called once per frame
    void Update () {
        Movement ();

        if (isInvincible) {
            timeSpentInvincible += Time.deltaTime;
            if (timeSpentInvincible < blinkDuration) {
                float remainder = timeSpentInvincible % 0.3f;
                spirteRenderer.enabled = remainder > 0.15f;
            } else {
                spirteRenderer.enabled = true;
                isInvincible = false;
            }
        }

        if (trans.position.y < -10.0f) {
            SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
        }
    }

    void Movement () {
        float h = Input.GetAxis ("Horizontal");
        float v = Input.GetAxis ("Vertical");

        float speed = 0.0f;

        if (Input.GetKey (KeyCode.LeftShift)) {
            animator.SetBool ("Run", true);
            animator.SetBool ("Walk", false);
            speed = runSpeed;
        } else {
            animator.SetBool ("Run", false);
            speed = walkSpeed;
        }

        float hRaw = Input.GetAxisRaw ("Horizontal");
        if (hRaw != 0.0f) {
            avatar.transform.localScale = new Vector3 (-hRaw, 1, -1);
            animator.SetBool ("Walk", true);
            animator.SetBool ("Idle", false);
        } else {
            animator.SetBool ("Walk", false);
            animator.SetBool ("Idle", true);
        }

        float vy = Mathf.Min (10.0f, rb.velocity.y);
        rb.velocity = new Vector2 (h * speed * Time.fixedDeltaTime, vy);

        float vRaw = Input.GetAxisRaw ("Vertical");
        if (vRaw > 0.0f && isGround && !isMagicArea) {
            jumpRequest = true;
        }
    }
    private void FixedUpdate () {
        if (jumpRequest) {
            rb.AddForce (Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            jumpRequest = false;
            isGround = false;
        } else {
            Vector2 boxCenter = (Vector2) trans.position;

            if (Physics2D.OverlapBox (boxCenter, boxSize, 0, mask) != null) {
                isGround = true;
            } else {
                isGround = false;
            }
        }
    }

    private void OnTriggerEnter2D (Collider2D other) {
        if (other.gameObject.tag == "Money") {
            Debug.Log ("Money");
            Destroy (other.gameObject);
            int money = PlayerPrefs.GetInt ("Money", 0);
            PlayerPrefs.SetInt ("Money", money + 1);
            Debug.Log (PlayerPrefs.GetInt ("Money", 0));
        } else if (other.gameObject.tag == "Spike") {
            Debug.Log ("Spike");
            isInvincible = true;
            timeSpentInvincible = 0.0f;
        } else if (other.gameObject.tag == "NextLevelFlag") {
            Debug.Log ("NextLevelFlag");
            Scene scene = SceneManager.GetActiveScene ();
            const int totalNumberOfLevels = 2;
            int levelID = int.Parse (scene.name.Substring (5));
            int nextLevelId = (levelID % totalNumberOfLevels + 1);
            string nextSceneName = "Level" + nextLevelId.ToString ();
            SceneManager.LoadScene (nextSceneName);
        } else if (other.gameObject.tag == "MagicArea") {
            Debug.Log ("MagicArea");
            isMagicArea = true;
        }
    }

    private void OnTriggerExit2D (Collider2D other) {
        if (other.gameObject.tag == "MagicArea") {
            Debug.Log ("MagicArea");
            isMagicArea = false;
        }
    }
    private void OnDrawGizmos () {
        if (isGround) {
            Gizmos.color = Color.red;
        } else {
            Gizmos.color = Color.green;
        }
        Vector2 boxCenter = (Vector2) transform.position;
        Gizmos.DrawWireCube (boxCenter, boxSize);
    }

}