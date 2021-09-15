using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystic_touch : MonoBehaviour
{
    public float speedMove; //Скорость псины
    private float gravityForce; //Гравитация пдения
    private Vector3 moveVector;
    private CharacterController ch_controller;
    private MobileController mContr;
    Animator DogAnimator;
    void Start()
    {
        DogAnimator = GameObject.Find("dog_model_step4_animation_static").GetComponent<Animator>();
        ch_controller = GetComponent<CharacterController>();
        mContr = GameObject.FindGameObjectWithTag("Joystick").GetComponent<MobileController>();
    }

    // Update is called once per frame
    void Update()
    {
        CharacterMove();
        GamingGravity();
        Moving();
    }
    private void CharacterMove() //Метод передвижения
    {
        moveVector = Vector3.zero;
        moveVector.x = mContr.Horizontal() * speedMove;
        moveVector.z = mContr.Vertical() * speedMove;

        if (Vector3.Angle(Vector3.forward, moveVector) > 1f || Vector3.Angle(Vector3.forward, moveVector) == 0) //Поворачиваем в ту сторону в которую идем
        {
            Vector3 direct = Vector3.RotateTowards(transform.forward, moveVector, speedMove, 0.0f);
            transform.rotation = Quaternion.LookRotation(direct);
        }
        moveVector.y = gravityForce;
        ch_controller.Move(moveVector * Time.deltaTime);
    }
    private void GamingGravity() //Метод пдения
    {
        if (!ch_controller.isGrounded)
            gravityForce -= 40f * Time.deltaTime;
        else
            gravityForce = -1f;
    }
        void Moving()
    {

        if ((moveVector.z != 0 || moveVector.x != 0) && speedMove >= 45)
        {
            DogAnimator.SetBool("Walking", false);
            DogAnimator.SetBool("Running", true);
        }
        else if ((moveVector.z != 0 || moveVector.x != 0) && speedMove < 45)
        {

            DogAnimator.SetBool("Running", false);
            DogAnimator.SetBool("Walking", true);
        }
        else
        {
            DogAnimator.SetBool("Walking", false);
            DogAnimator.SetBool("Running", false);
        }
    }
}
    

