using UnityEngine;

public class PlayerFacing : MonoBehaviour
{
    private GameObject _player;
    private Movement _mov;

    public float YOnMovementHorizontal = -0.4f;
    public float XOnMovementHorizontal = 0.7f;
    public float YOnMovementUp = -0.1f;
    public float YOnMovementDown = -1f;


    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _mov = _player.GetComponent<Movement>();
    }

    private void Update()
    {
        MoveFacingBlock();
        Ineract();
    }

    private void MoveFacingBlock()
    {

        var dir = _mov.PlayerFacing;

        if (dir == Direction.South)
            transform.position = _player.transform.position + new Vector3(0, YOnMovementDown, 0);
        else if (dir == Direction.North)
            transform.position = _player.transform.position + new Vector3(0, YOnMovementUp, 0);
        else if (dir == Direction.West)
            transform.position = _player.transform.position + new Vector3(-XOnMovementHorizontal, YOnMovementHorizontal, 0);
        else if (dir == Direction.East)
            transform.position = _player.transform.position + new Vector3(XOnMovementHorizontal, YOnMovementHorizontal, 0);

    }

    private void Ineract()
    {
        if (!Input.GetButtonDown("Jump") || _interaction == null)
            return;
        _interaction.WhenPlayerInteracts();
    }

    private ObjectInteractable _interaction;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _interaction = collision.gameObject.GetComponent<ObjectInteractable>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _interaction = null;
    }
}