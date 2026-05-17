using UnityEngine;


public interface IMovable
{
    void Move(Vector2 direction);
    bool CanMoveTo(Vector2 targetPosition);
}