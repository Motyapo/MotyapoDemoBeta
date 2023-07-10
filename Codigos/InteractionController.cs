using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class InteractionController : MonoBehaviour
{
    public float interactionDistance = 1.4f;
    public KeyCode interactionKey = KeyCode.E;

    public GameObject currentInteractable;
    public GameObject KeyUI;


    private void Update()
    {
        // Verifique se a tecla "E" est� sendo pressionada
       if (currentInteractable != null && Vector3.Distance(transform.position, currentInteractable.transform.position) <= interactionDistance)
            {
            // Verifique se o jogador est� perto de algum objeto interativo
            if (Input.GetKeyDown(interactionKey))
            {
                // Execute a a��o de intera��o
                Interact();
               

            }
            KeyUI.SetActive(true);
            
        }else KeyUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verifique se o objeto que entrou na �rea de intera��o � interativo
        if (other.CompareTag("Interactable"))
        {
            currentInteractable = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Verifique se o objeto que saiu da �rea de intera��o � o mesmo objeto interativo
        if (other.gameObject == currentInteractable)
        {
            currentInteractable = null;
        }
    }

    private void Interact()
    {
        // Implemente a a��o de intera��o aqui
        Debug.Log("Interacted with: " + currentInteractable.name);

        if (currentInteractable.name == "Tetris")
        {
            SceneManager.LoadScene("Tetris");

        }
    }
}
