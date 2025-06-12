using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;


public class MapTransistion : MonoBehaviour
{
    [SerializeField] PolygonCollider2D mapBoundry;
    CinemachineConfiner confiner;

    [SerializeField] Direction direction;
    enum Direction {Up, Down, Left, Right}
    [SerializeField] float addativePos = 2f;

    private void Awake()
    {
        confiner = FindObjectOfType<CinemachineConfiner>();

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player")){
            confiner.m_BoundingShape2D = mapBoundry;
            UpdatePlayerPosition(collision.gameObject);
        }
    }

    private void UpdatePlayerPosition(GameObject player)
    {
        Vector3 newPos = player.transform.position;
        switch (direction) {
            case Direction.Up:
                newPos.y += addativePos;
                break;
            case Direction.Down:
                newPos.y -= addativePos;
                break;
            case Direction.Left:
                newPos.x -= addativePos;
                break;
            case Direction.Right:
                newPos.x += addativePos;
                break;
        }
    player.transform.position = newPos;
    }
}
