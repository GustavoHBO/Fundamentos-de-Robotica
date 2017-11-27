using UnityEngine;
using System.Collections;

/*
 *Esta classe é responsável pelo controle dos movimentos que o braço efetua.
 *
 * É responsável também por monitorar o movimento que a base faz, acompanhando-a.
 */

public class MovimentoBraco : MonoBehaviour {

	int turnSpeed = 50;// Velocidade do giro

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		girarBraco();// Responsável por controlar o giro do braço robótico.
	}

	void girarBraco(){

		/* Aqui abaixo é controlado o giro entorno do pivot */
		if(Input.GetKey(KeyCode.UpArrow)){// Verifica se a seta para cima está pressionada.
			transform.Rotate(new Vector3(0, 0, turnSpeed) * Time.deltaTime);// O vetor é o eixo no qual será feito o giro
            																	// o Time.deltaTime é a velocidade vezes o tempo(esse tempo é para ser constante em todos os computadores).
		} else if(Input.GetKey(KeyCode.DownArrow)){// Verifica se a seta para baixo está pressionada.
			transform.Rotate(new Vector3(0, 0, -turnSpeed) * Time.deltaTime);// O vetor é o eixo no qual será feito o giro
            																	// o segundo é a velocidade vezes o tempo(esse tempo é para ser constante em todos os computadores).
		}
	}
}
