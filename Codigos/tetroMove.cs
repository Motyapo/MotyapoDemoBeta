using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tetroMove : MonoBehaviour
{
    public bool podeRodar;
    public bool roda360;

    public float queda;
    public float velocidade;
    public float timer;
    

    gameManager gManager;
    spawnTetro gSpawner;

    // Start is called before the first frame update
    void Start()
    {
        velocidade = 0.2f;
        timer = velocidade;

        gManager = GameObject.FindObjectOfType<gameManager>();
        gSpawner = GameObject.FindObjectOfType<spawnTetro>();


    }

    // Update is called once per frame
    void Update()
    {
        if(gManager.pontoDificuldade >= 500)
        {
            gManager.dificuldade += .25f;
            gManager.Nivel += 1;

            gManager.pontoDificuldade -= 250;
            
        }
       


        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S))
        {
            timer = velocidade;
        }
            if (Input.GetKey(KeyCode.D))
        {
            timer += Time.deltaTime;
            if (timer > velocidade) 
            { 
                transform.position += new Vector3(1, 0, 0);
                timer = 0;
            }
           
            if (posicaoValida())
            {
                gManager.atualizaGrade(this);
            }
            else
            {
                transform.position += new Vector3(-1, 0, 0);
            }
         
        }

        if (Input.GetKey(KeyCode.A))
        {
            timer += Time.deltaTime;
            if (timer > velocidade)
            {
                transform.position += new Vector3(-1, 0, 0);
                timer = 0;
            }
            if (posicaoValida())
            {
                gManager.atualizaGrade(this);
            }
            else
            {
                transform.position += new Vector3(1, 0, 0);
            }
        }


        


        if (Input.GetKey(KeyCode.S))
        {
            timer += Time.deltaTime;
            if (timer > velocidade)
            {
                transform.position += new Vector3(0, -1, 0);
                timer = 0;
            }
            QuedaConfig();
        }

        if (Time.time - queda >= ( 1 / gManager.dificuldade) && !Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, -1, 0);
            queda = Time.time;
            QuedaConfig();
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            ChecaRoda();
        }
    }

    void ChecaRoda()
    {
        if (Time.timeScale != 0)
        {
            if (podeRodar)
            {
                if (!roda360)
                {
                    if (transform.rotation.z < 0)
                    {
                        
                        transform.Rotate(0, 0, 90);
                        if (posicaoValida())
                        {

                        }
                        else
                        {
                            transform.Rotate(0, 0, -90);
                        }
                    }


                    else
                    {
                        transform.Rotate(0, 0, -90);
                        if (posicaoValida())
                        {
                            gManager.atualizaGrade(this);
                        }
                        else
                        {
                            transform.Rotate(0, 0, 90);
                        }
                    }
                }

                else
                {
                    transform.Rotate(0, 0, -90);
                    if (posicaoValida())
                    {
                        gManager.atualizaGrade(this);
                    }
                    else
                    {
                        transform.Rotate(0, 0, 90);
                    }

                }
            }
        }
        
    }

    void QuedaConfig()
    {
        if (posicaoValida())
        {
            gManager.atualizaGrade(this);
        }
        else
        {
            transform.position += new Vector3(0, 1, 0);
            gManager.apagaLinha();

            if (gManager.acimaGrade(this))
            {
                gManager.gameOver();
            }

            enabled = false;
            gManager.ContaLinhas();
            gSpawner.proximaPeca();

        }
    }

    bool posicaoValida()
    {
        foreach(Transform child in transform)
        {
            Vector2 posBloco = gManager.arredonda(child.position);
            if (gManager.dentroGrade(posBloco) == false)
            {
                return false;
            }

            if (gManager.posicaoTransformGrade(posBloco) != null && gManager.posicaoTransformGrade(posBloco).parent != transform)
            {
                return false;
            }
        }
        return true;
    }
    
}