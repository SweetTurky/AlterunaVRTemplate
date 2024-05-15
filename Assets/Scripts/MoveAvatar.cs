using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Avatar = Alteruna.Avatar;

public class MoveAvatar : MonoBehaviour
{
    private Avatar _avatar;
    private Transform p1Spawn;
    private Transform p2Spawn;

    private int avatarNumber;
    private bool hasMoved = false; // Flag to track whether the avatar has moved

    private void Awake()
    {
        _avatar = GetComponent<Avatar>();
    }

    void Start()
    {
        if (!_avatar.IsMe)
        {
            return;
        }
        avatarNumber = _avatar.Possessor.Index;
        Debug.Log("" + avatarNumber);
    }

    private void OnEnable()
    {
        if (!_avatar.IsMe)
        {
            return;
        }
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        if (!_avatar.IsMe)
        {
            return;
        }
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!_avatar.IsMe)
        {
            return;
        }
        if (scene.name == "Koncert" && !hasMoved)
        {
            MoveAvatarPosition();
            hasMoved = true;
        }
    }

    public void MoveAvatarPosition()
    {
        p1Spawn = GameObject.Find("P1Spawn").transform;
        p2Spawn = GameObject.Find("P2Spawn").transform;

        if (avatarNumber == 0)
        {
            transform.position = p1Spawn.position;
        }
        else if (avatarNumber >= 1)
        {
            transform.position = p2Spawn.position;
        }
    }
}