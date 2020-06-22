using UnityEngine;

public class PlayerFacing : MonoBehaviour
{
    public float AdditionalDownFacingDist = 0.3f;
    private GameObject _player;
    private float _vectorDistance = 0.7f;
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

        if (anim.IsName("v3-Front_Move") || anim.IsName("v3-Front_Idle"))
            transform.position = _player.transform.position + new Vector3(0, -_vectorDistance - AdditionalDownFacingDist, 0);
        else if (anim.IsName("v3-Back_Move") || anim.IsName("v3-Back_Idle"))
            transform.position = _player.transform.position + new Vector3(-0, _vectorDistance, 0);
        else if (anim.IsName("v3_Left_Move") || anim.IsName("v3-Left_Idle"))
            transform.position = _player.transform.position + new Vector3(-_vectorDistance, 0, 0);
        else if (anim.IsName("v3_Right_Move") || anim.IsName("v3_Right_Idle"))
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