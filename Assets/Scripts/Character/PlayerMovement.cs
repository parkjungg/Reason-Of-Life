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

    private void Awake()
    {
        _input          = GetComponent<PlayerInput>();
        _playerAnimator = GetComponent<PlayerAnimator>();
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

        if (!moving) return;

        Vector2 nextPos = (Vector2)transform.position + dir * moveSpeed * Time.deltaTime;

        if (CanMoveTo(nextPos))
            transform.position = nextPos;
    }

    public void Move(Vector2 direction)
    {
        transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
    }

    public bool CanMoveTo(Vector2 targetPosition)
    {
        Vector3Int cell = walkableTilemap.WorldToCell(targetPosition);
        return walkableTilemap.HasTile(cell);
    }
}