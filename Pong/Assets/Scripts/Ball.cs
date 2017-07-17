﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public float speed = 30;

    private Rigidbody2D rigidBody;

    private AudioSource audioSource;

	// Use this for initialization
	void Start () {

        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = Vector2.right * speed;

    }
	
	void OnCollisionEnter2D(Collision2D col) {
	    
        // left or right paddle
        if((col.gameObject.name == "LeftPaddle") || (col.gameObject.name == "RightPaddle")) {

            HandlePaddleHit(col);

        }

        // walls
        if ((col.gameObject.name == "WallBottom") || (col.gameObject.name == "WallTop")) {

            SoundManager.Instance.PlayOneShot(SoundManager.Instance.wallBloop);

        }

        // goals
        if ((col.gameObject.name == "LeftGoal") || (col.gameObject.name == "RightGoal")) {

            SoundManager.Instance.PlayOneShot(SoundManager.Instance.goalBloop);

            //TODO update score UI

            transform.position = new Vector2(0, 0);

        }

    }

    void HandlePaddleHit(Collision2D col) {

        float y = BallHitPaddleWhere(transform.position, col.transform.position, col.collider.bounds.size.y);

        Vector2 dir = new Vector2();

        if(col.gameObject.name == "LeftPaddle") {
            dir = new Vector2(1, y);
        }

        if (col.gameObject.name == "RightPaddle") {
            dir = new Vector2(-1, y);
        }

        rigidBody.velocity = dir * speed;

        SoundManager.Instance.PlayOneShot(SoundManager.Instance.hitPaddleBloop);

    }

    float BallHitPaddleWhere(Vector2 ball, Vector2 paddle, float paddleHeight) {

        return (ball.y - paddle.y) / paddleHeight;

    }

}
