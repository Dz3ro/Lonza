using UnityEngine;

public class SortingOrderManager : MonoBehaviour
{
    private int _positionAccuracy = 5;

    private GameObject _plr;
    private SpriteRenderer _plrSprRen;
    private Movement _plrMov;
    private Direction _plrDir;

    private void Awake()
    {
        _plr = GameObject.FindGameObjectWithTag("Player");
        _plrMov = _plr.GetComponent<Movement>();
        _plrSprRen = _plr.GetComponent<SpriteRenderer>();
    }


    public void SetSortingOrderForPlayer(SpriteRenderer spriteRenderer)
    {
        spriteRenderer.sortingOrder = (int)(100 - transform.position.y)
            * _positionAccuracy;
    }
    public void SetSortingOrderForPlayerItem(SpriteRenderer spriteRenderer)
    {
        _plrDir = _plrMov.PlayerFacing;

        if (_plrDir == Direction.North)
            spriteRenderer.sortingOrder = _plrSprRen.sortingOrder - 1;
        else
            spriteRenderer.sortingOrder = _plrSprRen.sortingOrder + 1;
    }
    public void SetSortingOrderForTree(SpriteRenderer spriteRenderer)
    {
        var offset = 0.5f;

        SpriteRenderer sprParent = spriteRenderer;
        SpriteRenderer sprChild = spriteRenderer.gameObject.transform
            .GetChild(0).GetComponent<SpriteRenderer>();

        sprChild.sortingOrder = (int)(100 - (transform.position.y + offset)) * _positionAccuracy;
        sprParent.sortingOrder = sprChild.sortingOrder - 1;
    }
}