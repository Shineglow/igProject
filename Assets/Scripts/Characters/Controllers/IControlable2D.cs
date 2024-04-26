using UnityEngine;

public interface IControlable2D
{
    void Move(Vector2 direction);
    void Jump();
    void Action();
}