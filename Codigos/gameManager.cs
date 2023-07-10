using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class gameManager : MonoBehaviour
{

    public static int altura = 20;
    public static int largura = 10;

    public int linhas=0;
    public float score = 0;

    public TMPro.TMP_Text textoScore;
    public TMPro.TMP_Text textoNivel;

    public float pontoDificuldade;
    public float dificuldade = 1;
    public float Nivel = 1;

    public static Transform[,] grade = new Transform[largura, altura];



    void Update()
    {
        
        textoScore.text = "Pontos: " + score.ToString();
        textoNivel.text = "Nivel: " + Nivel.ToString();
    }

    public bool dentroGrade(Vector2 posicao)
    {
        return ((int)posicao.x >= 0 && (int)posicao.x < largura && (int)posicao.y >= 0);
    }

    public Vector2 arredonda(Vector2 nA)//nA numero arredondado
    {
        return new Vector2(Mathf.Round(nA.x), Mathf.Round(nA.y));
    }

    public void atualizaGrade(tetroMove pecaTetris)
    {
        for (int y = 0; y < altura; y++)
        {
            for (int x = 0; x < largura; x++)
            {
                if (grade[x, y] != null)
                {
                    if (grade[x, y].parent == pecaTetris.transform)
                    {
                        grade[x, y] = null;
                    }
                }
            }
        }

        foreach (Transform peca in pecaTetris.transform)
        {
            Vector2 posicao = arredonda(peca.position);

            if (posicao.y < altura)
            {
                grade[(int)posicao.x, (int)posicao.y] = peca;
            }


        }
    }

    public Transform posicaoTransformGrade(Vector2 posicao)
    {
        if (posicao.y > altura - 1)
        {
            return null;
        }
        else
        {
            return grade[(int)posicao.x, (int)posicao.y];
        }

    }

    public bool linhaCheia(int y)
    {
        for (int x = 0; x < largura; x++)
        {
            if (grade[x, y] == null)
            {
                return false;
            }
        }
        return true;
       

    }

    public void deletaQuadrado(int y)
    {
        for (int x = 0; x < largura; x++)
        {
            Destroy(grade[x, y].gameObject);

            grade[x, y] = null;
        }
    }

    public void moveLinhaBaixo(int y)
    {
        for (int x = 0; x < largura; x++)
        {
            if (grade[x, y] != null)
            {
                grade[x, y - 1] = grade[x, y];
                grade[x, y] = null;

                grade[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public void moveTodasLinhasBaixo(int y)
    {
        for (int i = y; i < altura; i++)
        {
         moveLinhaBaixo(i);

        }
    }

    public void apagaLinha()
    {
        for(int y = 0;y < altura; y++)
        {
            if (linhaCheia(y))
            {
                deletaQuadrado(y);
                moveTodasLinhasBaixo(y + 1);
                y--;
                linhas += 1;
                Debug.Log("linhas: " + linhas) ;


            }
        }
    }

    public float ContaLinhas()
    {
        switch (linhas)
        {
            case 1:
                score += 40 * dificuldade;
                pontoDificuldade += 40 * dificuldade;
                break;

            case 2:
                score += 100 * dificuldade;
                pontoDificuldade += 100 * dificuldade; 
                break;

            case 3:
                score += 300 * dificuldade;
                pontoDificuldade += 300 * dificuldade;
                break;

            case 4:
                score += 1200 * dificuldade;
                pontoDificuldade += 1200 * dificuldade;
                break;
        }
       linhas = 0;
       
        return score;
    }

    public bool acimaGrade(tetroMove pecaTetroMino)
    {
        for (int x  = 0; x < largura; x++)
        {
            foreach(Transform quadrado in pecaTetroMino.transform)
            {
               Vector2 posicao = arredonda(quadrado.position);

                if (posicao.y > altura - 1)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public void gameOver()
    {
        //GameObject.FindObjectOfType<PauseMenu>().Pause();
        SceneManager.LoadScene(1);
    }
}