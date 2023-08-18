using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//FINAL - Caamano Romina - requiere un rigidbody
[RequireComponent(typeof(Collider))]

//FINAL - Caamano Romina - hereda de kinematic movement controller
public class ReactionKinematicMovementController : KinematicMovementController
{
    //FINAL - Caamano Romina - sobreescribe el start heredado con un start vacio
    //para que NO comience su movimiento al iniciar si no cuando colisione con el player
    protected override void Start()
    {
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        // Ver una mejor forma de detectar el objecto que activa el movimiento
        if (collision.gameObject.name == "Char")
            StartMovement();
    }
}
