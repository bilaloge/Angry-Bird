using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Bird : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float _force = 500;
    [SerializeField] float _maxDragDistace = 3;
    Vector2 _startPosition;
    Rigidbody2D _body;
    SpriteRenderer _renderer;
    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _body = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
       _startPosition = _body.position;
        _body.isKinematic = true;
    }
    void OnMouseDown()
    {
        _renderer.color = Color.red;
    }
    void OnMouseUp()
    {
        Vector2 currentPosition = _body.position;
        Vector2 direction= _startPosition - currentPosition;
        direction.Normalize();
        _body.isKinematic = false;
        _body.AddForce(direction* _force);

        _renderer.color = Color.white;
    }
    void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 desiredPosition = mousePosition;

        float distence = Vector2.Distance(desiredPosition, _startPosition);
        if (distence > _maxDragDistace)
        {
            Vector2 direction = desiredPosition - _startPosition;
            direction.Normalize();
            desiredPosition = _startPosition + (direction * _maxDragDistace);
        }

        if (desiredPosition.x>_startPosition.x)
            desiredPosition.x = _startPosition.x;

        _body.position = desiredPosition;



        //transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(ResetAfterDelay());
    }
     IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(3);
        _body.position = _startPosition;
        _body.isKinematic = true;
        _body.velocity = Vector2.zero;
    }
}
