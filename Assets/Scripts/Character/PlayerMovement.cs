using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour, IMovable
{
    [Header("Character Speed")]
    [SerializeField] private float moveSpeed = 5f;

    [Header("Tilemap")]
    [SerializeField] private Tilemap walkableTilemap;

    private PlayerInput    _input;
    private PlayerAnimator _playerAnimator;
    private Rigidbody2D    _rb;

    private void Awake()
    {
        _input          = GetComponent<PlayerInput>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _rb             = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Vector3Int cell = walkableTilemap.WorldToCell(transform.position);
        transform.position = walkableTilemap.GetCellCenterWorld(cell);
    }

    private void Update()
    {
        Vector2 dir    = _input.MoveInput;
        bool    moving = dir != Vector2.zero;

        _playerAnimator.UpdateAnimation(dir, moving);
    }

    private void FixedUpdate()
    {
        Vector2 dir = _input.MoveInput;
        _rb.velocity = dir * moveSpeed;
    }

    public void Move(Vector2 direction)
    {
        _rb.velocity = direction * moveSpeed;
    }

    public bool CanMoveTo(Vector2 targetPosition)
    {
        Vector3Int cell = walkableTilemap.WorldToCell(targetPosition);
        return walkableTilemap.HasTile(cell);
    }
}