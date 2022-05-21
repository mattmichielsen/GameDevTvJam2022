using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float jumpScale = 1;
    [SerializeField] float moveSpeed = 1;

    private Rigidbody _body;
    private bool _jumping;

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Space) && !_jumping)
        {
            _body.AddRelativeForce(Vector3.up * jumpScale);
            _jumping = true;
        }

        gameObject.transform.position += Vector3.forward * vertical * moveSpeed * Time.deltaTime;
        gameObject.transform.position += Vector3.right * horizontal * moveSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            Debug.Log("Hit ground");
            _jumping = false;
        }
    }
}
