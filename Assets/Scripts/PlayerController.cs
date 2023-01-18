using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _rigidbody;
    [SerializeField] private float playerSpeed;
    [SerializeField] private List<GameObject> checkPoints;
    [SerializeField] private Vector3 checkPointLocation;
    [SerializeField] private float deadzone;
    [SerializeField] private float rotationSpeed;
    private float _jumpValue = 0f;
    [SerializeField] private TextMeshProUGUI checkpointText;
    private int _counter;
    [SerializeField] private TextMeshProUGUI tutorialText;
    
    void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        SetCpCountText();
        tutorialText.gameObject.SetActive(true);
        Invoke("RemoveTutorialText",4f);
    }
    

    private void FixedUpdate()
    {
        if (transform.position.y <= deadzone)
        {
            transform.position = checkPointLocation;
            Stop();
        }
        Move();
    }
    
    private void OnTriggerEnter(Collider locations)
    {
        if (locations.gameObject.CompareTag("Jumping"))
        {
            _rigidbody.AddForce(new Vector3(0,800,0));
        }
        if (locations.CompareTag("NextLevel"))
        {
            SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + 1));
        }

        if (locations.CompareTag("Checkpoint"))
        {
            checkPointLocation = transform.position;
            Destroy(locations.gameObject);
            _counter += 1;
            SetCpCountText();
        }
    }
    
    private void Stop()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }
    private void Move()
    {
        
        float moveVertical = Input.GetAxis("Vertical");
        
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up * (-rotationSpeed * Time.fixedDeltaTime));
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * (rotationSpeed * Time.fixedDeltaTime));
        }

        transform.Translate(new Vector3(0f, _jumpValue, moveVertical * playerSpeed * Time.fixedDeltaTime), Space.Self);
        
    }

    void SetCpCountText()
    {
        checkpointText.text = "Checkpoints:" + _counter.ToString();
    }

    void RemoveTutorialText()
    {
        tutorialText.gameObject.SetActive(false);
    }
    
}
