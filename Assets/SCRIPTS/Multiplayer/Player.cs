using Fusion;
using UnityEngine;

public class Player : NetworkBehaviour
{
    private NetworkCharacterController _cc;

    [SerializeField] private float speed = 15f;
    [SerializeField] private float rotationSpeed = 700f;

    [Networked] private NetworkButtons PreviousButtons { get; set; }

    private void Awake()
    {
        _cc = GetComponent<NetworkCharacterController>();

        if (_cc == null)
        {
            Debug.LogError("NetworkCharacterController não encontrado no prefab do Player!");
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (_cc == null)
            return;

        if (GetInput(out NetworkInputData data))
        {
            Vector3 moveDirection = data.direction;

            if (moveDirection.sqrMagnitude > 1f)
                moveDirection.Normalize();

            _cc.Move(speed * moveDirection * Runner.DeltaTime);

            if (moveDirection.magnitude >= 0.1f)
            {
                Quaternion toRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(
                    transform.rotation,
                    toRotation,
                    rotationSpeed * Runner.DeltaTime
                );
            }

            if (data.buttons.WasPressed(PreviousButtons, InputButtons.Jump))
            {
                _cc.Jump();
            }

            PreviousButtons = data.buttons;
        }
    }
}