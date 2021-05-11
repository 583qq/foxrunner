using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    [SerializeField] LayerMask groundLayer;
    private int groundLayerNum;

    private Animator _animator;
    [Header("References")]
    [SerializeField] private Text _pointsText;
    [SerializeField] private HealthBar _healthBar;

    private bool isGrounded;

    [Header("Settings")]
    [SerializeField] private float _speed;
    [SerializeField] private Vector3 _startOffset;
    [SerializeField] private int healthMaximum;
    
    [HideInInspector] public int maxHealth => healthMaximum;
    public int health {get; private set;}
    public int points {get; private set;}


    [HideInInspector] public float maxDistance;   // Road & player. Refactor
    [SerializeField] private float distanceToPoint = 10;
    private int distancePoint = 0;


    // Horizontal Movement
    private Vector3 _movement = Vector3.zero;
    private float _horizontalEndpoint;
    private bool _isMoving = false;
    private float _eps = 0.01f;
    
    // Horizontal constraints
    private float _constraintLeft = 2 * Vector3.left.x;
    private float _constraintRight = 2 * Vector3.right.x;


    [Header("Jumping")]
    [SerializeField] private int _maxJumps = 2;
    [SerializeField] private float _jumpHeight = 2f;
    private int _jumpsCount = 0;
    private bool _isJumping;
    private Vector3 _jump = Vector3.zero;

    void Awake()
    {
        health = healthMaximum;

        groundLayerNum = GameUtilities.GetLayerNumber(groundLayer);
        _animator = GetComponent<Animator>();
    }

    void Start()
    {
        transform.position = Vector3.zero + _startOffset;
        
        _animator.Play("Run");
    }

    void Update()
    {
        // Reset if we are too far
        if(transform.position.z > maxDistance)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            distancePoint = 0;
        }

        HandleInput();

        // Move Forward
        transform.position += Vector3.forward * _speed * Time.deltaTime;

        GetDistancePoints();
    }

    public void FixedUpdate()
    {
        Jump();
        MoveHorizontal();
    }

    public void LateUpdate()
    {
        if(!isGrounded)
            _animator.SetTrigger("Jump");

        if(_movement.x >= Vector3.right.x)
            _animator.SetTrigger("RunRight");
        
        if(_movement.x <= Vector3.left.x)
            _animator.SetTrigger("RunLeft");
    }

    private void HandleInput()
    {
        // Move Left
        if(Input.GetKeyDown(KeyCode.A))
            if(!_isMoving && InConstraintsDelta(_movement + Vector3.left))
            {
                // We are not moving & our movement will be inside constraints.
                _movement += Vector3.left;
                Debug.Log($"Movement: {_movement}");
            }
        
        // Move Right
        if(Input.GetKeyDown(KeyCode.D))
            if(!_isMoving && InConstraintsDelta(_movement + Vector3.right))
            {
                // We are not moving & our movement will be inside constraints.
                _movement += Vector3.right;
                Debug.Log($"Movement: {_movement}");
            }

        // Jump
        if(Input.GetKey(KeyCode.Space))
        {
            if(!_isJumping || _jumpsCount < _maxJumps)
            {
                _jump += Vector3.up * _jumpHeight;
                _jumpsCount++;
                _isJumping = true;
            }
        }
    }

    private void MoveHorizontal()
    {
        if(!_isMoving && _movement != Vector3.zero)
        {
            // Movement start
            _isMoving = true;
            _horizontalEndpoint = transform.position.x + _movement.x;
        }

        // Create new end point vector. X will be the same.
        Vector3 endpoint = transform.position;
        endpoint.x = _horizontalEndpoint;

        Vector3 dest = Vector3.MoveTowards(transform.position, endpoint, _speed * Time.deltaTime);

        if(!InConstraints(dest, _movement) || !_isMoving)
        {
            // We are not moving or our movement will be out of constraints.
            _isMoving = false;
            _movement = Vector3.zero;
            return;
        }


        if(Vector3.Distance(dest, endpoint) > _eps)
            transform.position = dest;
        else
        {
            // Movement finished
            _isMoving = false;
            // Correct position
            transform.position = new Vector3(_horizontalEndpoint, transform.position.y, transform.position.z);
            // Reset movement vector
            _movement = Vector3.zero;
        }
        
    }

    private void Jump()
    {
        isGrounded = Physics.CheckSphere(transform.position, 0.001f, groundLayer.value, QueryTriggerInteraction.Ignore);

        float height = Mathf.Lerp(transform.position.y, _jump.y, _speed * Time.deltaTime);

        transform.position = new Vector3(transform.position.x, height, transform.position.z);

        // Check if we are grounded
        if(isGrounded)
        {
            // If so reset
            _jump = Vector3.zero;
            _jumpsCount = 0;
            _isJumping = false;
        }
    }

    private bool InConstraintsDelta(Vector3 delta)
    {
        return InConstraints(transform.position, delta);
    }

    private bool InConstraints(Vector3 dest, Vector3 delta)
    {
        if(delta.x > 0)
            if(dest.x > _constraintRight + _eps)
                return false;
        
        if(delta.x < 0)
            if(dest.x < _constraintLeft - _eps)
                return false;
        
        return true;
    }

    private void GetDistancePoints()
    {
        int currentPoint = (int) (transform.position.z / distanceToPoint);

        if(currentPoint > distancePoint)
        {
            distancePoint = currentPoint;
            AddPoints(1);
        }
    }

    public void AddPoints(int points) 
    {
        this.points += points;
        _pointsText.text = this.points.ToString();
    }

    public void TakeDamage(int value)
    {
        if(health - value < 0)
        {
            health = 0;
            value = health;
        }
        else
            health -= value;

        _healthBar.RemoveIcon(value);
    }

    public void Heal(int value)
    {
        Debug.Log($"Trying to heal: {value}");

        if((health + value) > healthMaximum)
        {
            health = healthMaximum;
            value = healthMaximum - health;
        }
        else
            health += value;
        
        _healthBar.AddIcon(value);
    }
}
