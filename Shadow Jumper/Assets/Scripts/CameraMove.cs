using System;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject Camera;

    public GameObject Character;

    private float cameraMovement;



    public Vector3 offset; // Offset from target
    public float followSpeed = 5f; // Speed of following

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Area")
        {
            ChangeArea();
        }
    }

    private void ChangeArea()
    {
        cameraMovement = Character.transform.position.x - 1;
        offset = new Vector3(cameraMovement, 0, 0);
        Vector3 targetPos = Camera.transform.position + offset;
        Camera.transform.position = targetPos;
    }
}