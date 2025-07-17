using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nejikocontroller : MonoBehaviour
{
    // Start is called before the first frame update

    //1 プレイヤーのキー入力を受け取る
    //2 キー入力の方向に移動する
    //3 移動方向に合わせてアニメーションを再生する
    CharacterController controller;

    Vector3 moveDirection = Vector3.zero;
    
    public float speed = 0f;

    Animator animator;

    //ジャンプの高さと重力の強さを変数にする
    public float jumpPowaer = 0f;
    public float gravityPowaer = 0;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //もしねじこが地面についているなら
        if(controller.isGrounded)
        {
            //ねじこがジャンプを行う処理
            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDirection.y = jumpPowaer;
            }
        }

        if(Input.GetAxis("Vertical") > 0.0f)
        {
            moveDirection.z = Input.GetAxis("Vertical") * speed;
        }
        else
        {
            moveDirection.z = 0.0f;
        }

        if (Input.GetKey(KeyCode.W))
        {
            moveDirection.z = speed;
        }

        //Hprozontal(左右入力)があれば、ねじこを回転させる
        transform.Rotate(0, Input.GetAxis("Horizontal") * 3f, 0);

        //キャラクターが重力で落下する処理
        moveDirection.y = moveDirection.y - gravityPowaer * Time.deltaTime;

        //移動量をtransformに変換する
        Vector3 globalDirection = transform.TransformDirection(moveDirection);

        //controllerに移動量を渡す
        controller.Move(globalDirection * Time.deltaTime);

        //ねじこの走るアニメーションを再生する
        animator.SetBool("run", moveDirection.z > 0f);
    }
}
