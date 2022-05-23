using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float jumpScale = 1;
    [SerializeField] float moveSpeed = 1;
    [SerializeField] float ghostSpeed = 2;
    [SerializeField] float ghostMass = 300;

    private Rigidbody _body;
    private GameObject _bodyParts;
    private GameObject _ghost;
    private bool _jumping;

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
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            _jumping = false;
        }
        else if (other.gameObject.tag == "Knife" || other.gameObject.tag == "Murderer")
        {
            Die();
        }
    }

    private void Die()
    {
        _body.constraints = RigidbodyConstraints.FreezePositionY;
        _body.useGravity = false;
        _body.mass = ghostMass;
        moveSpeed = ghostSpeed;
        _bodyParts.SetActive(false);
        _ghost.SetActive(true);
        tag = "Ghost";
    }
}
