using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nejikocontroller : MonoBehaviour
{
    // Start is called before the first frame update

    //1 �v���C���[�̃L�[���͂��󂯎��
    //2 �L�[���͂̕����Ɉړ�����
    //3 �ړ������ɍ��킹�ăA�j���[�V�������Đ�����
    CharacterController controller;

    Vector3 moveDirection = Vector3.zero;
    
    public float speed = 0f;

    Animator animator;

    //�W�����v�̍����Əd�͂̋�����ϐ��ɂ���
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
        //�����˂������n�ʂɂ��Ă���Ȃ�
        if(controller.isGrounded)
        {
            //�˂������W�����v���s������
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

        //Hprozontal(���E����)������΁A�˂�������]������
        transform.Rotate(0, Input.GetAxis("Horizontal") * 3f, 0);

        //�L�����N�^�[���d�͂ŗ������鏈��
        moveDirection.y = moveDirection.y - gravityPowaer * Time.deltaTime;

        //�ړ��ʂ�transform�ɕϊ�����
        Vector3 globalDirection = transform.TransformDirection(moveDirection);

        //controller�Ɉړ��ʂ�n��
        controller.Move(globalDirection * Time.deltaTime);

        //�˂����̑���A�j���[�V�������Đ�����
        animator.SetBool("run", moveDirection.z > 0f);
    }
}
