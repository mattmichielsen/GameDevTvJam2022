using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float jumpScale = 1;
    [SerializeField] float moveSpeed = 1;
    [SerializeField] float ghostSpeed = 2;
    [SerializeField] float ghostMass = 300;
    [SerializeField] TextMeshProUGUI scoreUI;

    private Rigidbody _body;
    private GameObject _bodyParts;
    private GameObject _ghost;
    private bool _jumping;
    private int _score = 0;


    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody>();
        _bodyParts = transform.Find("BodyParts").gameObject;
        _ghost = transform.Find("Ghost").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space) && !_jumping && tag != "Ghost")
        {
            _body.AddRelativeForce(Vector3.up * jumpScale);
            _jumping = true;
        }

        var direction = new Vector3(horizontal, 0, vertical);
        direction.Normalize();
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);

        if (direction != Vector3.zero)
        {
            transform.forward = direction;
        }

        if (scoreUI != null)
        {
            scoreUI.text = _score.ToString();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            _jumping = false;
            if (gameObject.tag == "Ghost")
            {
                _body.constraints = RigidbodyConstraints.FreezePositionY;
                _body.useGravity = false;
            }
        }
        else if (gameObject.tag == "Ghost" && other.gameObject.tag == "Murderer")
        {
            _score += Kill(other.gameObject);
        }
        else if (other.gameObject.tag == "Knife" || other.gameObject.tag == "Murderer")
        {
            Die();
        }
    }

    private int Kill(GameObject enemy)
    {
        var chaser = enemy.GetComponent<Chaser>();
        if (chaser != null)
        {
            return chaser.Die();
        }

        return 0;
    }

    private void Die()
    {
        _body.mass = ghostMass;
        moveSpeed = ghostSpeed;
        _bodyParts.SetActive(false);
        _ghost.SetActive(true);
        tag = "Ghost";
    }
}
