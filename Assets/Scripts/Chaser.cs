using UnityEngine;

public class Chaser : MonoBehaviour
{
    [SerializeField] float chaseSpeed = 1;

    public bool IsAlive
    {
        get
        {
            return chaseSpeed > 0;
        }
    }

    private GameObject _player;
    private Rigidbody _body;
    private bool _started;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_started && (Mathf.Abs(Input.GetAxis("Horizontal")) > 0 || Mathf.Abs(Input.GetAxis("Vertical")) > 0))
        {
            _started = true;
        }

        if (_player != null && _started && chaseSpeed > 0)
        {
            var position = Vector3.ClampMagnitude(_player.transform.position - transform.position, 1) * chaseSpeed * Time.deltaTime;
            if (_player.tag == "Player")
            {
                transform.position += position;
                transform.LookAt(_player.transform, Vector3.up);
            }
            else if (_player.tag == "Ghost")
            {
                transform.position -= position;
                transform.forward = -position;
            }
        }
    }

    public int Die()
    {
        if (chaseSpeed > 0)
        {
            _body.freezeRotation = false;
            chaseSpeed = 0;
            return 1;
        }

        return 0;
    }
}
