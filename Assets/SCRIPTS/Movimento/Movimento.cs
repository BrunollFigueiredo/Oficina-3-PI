using UnityEngine;

public class Movimento : MonoBehaviour
{
    public VariableJoystick joystick;

    void Update()
    {
        if (joystick == null)
        {
            BasicSpawner.TouchMoveInput = Vector2.zero;
            return;
        }

        Vector2 direction = new Vector2(joystick.Horizontal, joystick.Vertical);

        if (direction.sqrMagnitude > 1f)
            direction.Normalize();

        BasicSpawner.TouchMoveInput = direction;
    }

    public void Pulo()
    {
        BasicSpawner.JumpPressed = true;
    }
}