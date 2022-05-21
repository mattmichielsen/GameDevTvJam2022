using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser : MonoBehaviour
{
    [SerializeField] float chaseSpeed = 1;
    [SerializeField] float maxRotateRadians = 0.5f;
    [SerializeField] float maxRotateMagnitude = 1;

    private GameObject _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += Vector3.ClampMagnitude(_player.transform.position - gameObject.transform.position, 1) * chaseSpeed * Time.deltaTime;
        gameObject.transform.LookAt(_player.transform, Vector3.up);
    }
}
