using UnityEngine;
using System.Collections;

/*
 * Esta classe é responsável pelo controle dos movimentos que a base efetua.
 */

public class MovimentoBase : MonoBehaviour {

	int turnSpeed = 50; // Velocidade do giro entorno do eixo

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		girarBase();// Responsável por controlar o giro da base robótica.
	}

	void girarBase(){

		/* Aqui abaixo é controlado o giro entorno do pivot */
		if(Input.GetKey(KeyCode.RightArrow)){// Verifica se a seta para a direita está pressionada.
            transform.Rotate(new Vector3(0, turnSpeed, 0) * Time.deltaTime);// O primeiro parametro é o eixo no qual será feito o giro
            																	// o segundo é a velocidade vezes o tempo(esse tempo é para ser constante em todos os computadores).
        /* Aqui abaixo é controlado o giro entorno do pivot */
		} else if(Input.GetKey(KeyCode.LeftArrow)){// Verifica se a seta para a esquerda está pressionada.
            transform.Rotate(new Vector3(0, -turnSpeed, 0) * Time.deltaTime);// O primeiro parametro é o eixo no qual será feito o giro
            																	// o segundo é a velocidade vezes o tempo(esse tempo é para ser constante em todos os computadores).
		}
	}
}
