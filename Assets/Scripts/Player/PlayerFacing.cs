using UnityEngine;

public class PlayerFacing : MonoBehaviour
{
    private GameObject _player;
    private float _vectorDistance = 1f;
    private Animator _plrAnim;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _plrAnim = _player.GetComponent<Animator>();
    }

    private void Update()
    {
        MoveFacingBlock();
        Ineract();
    }

    private void MoveFacingBlock()
    {
        var anim = _plrAnim.GetCurrentAnimatorStateInfo(0);

        if (anim.IsName("DownMove") || anim.IsName("DownIdle"))
            transform.position = _player.transform.position + new Vector3(0, -_vectorDistance, 0);
        else if (anim.IsName("UpMove") || anim.IsName("UpIdle"))
            transform.position = _player.transform.position + new Vector3(-0, _vectorDistance, 0);
        else if (anim.IsName("LeftMove") || anim.IsName("LeftIdle"))
            transform.position = _player.transform.position + new Vector3(-_vectorDistance, 0, 0);
        else if (anim.IsName("RightMove") || anim.IsName("RightIdle"))
            transform.position = _player.transform.position + new Vector3(_vectorDistance, 0, 0);
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