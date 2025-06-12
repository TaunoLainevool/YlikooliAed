using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class interactionDetector : MonoBehaviour
{
    private IInteractible interactibleInRange = null;
    public GameObject interactionIcon;
    // Start is called before the first frame update
    void Start()
    {
        interactionIcon.SetActive(false);
    }

    public void OnInteract(InputAction.CallbackContext context){
        if(context.performed){
            interactibleInRange?.Interact();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out IInteractible interactible) && interactible.CanInteract()){
            interactibleInRange = interactible;
            interactionIcon.SetActive(true);
            Debug.Log(interactibleInRange);
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
         if(collision.TryGetComponent(out IInteractible interactible) && interactible == interactibleInRange){
            interactibleInRange = null;
            interactionIcon.SetActive(false);
        }
    }
}
