using UnityEngine;
using System.Collections;

public class MovimentoGarra : MonoBehaviour {

	int turnSpeed = 50;
	int turnTime = 0; // Quantidade de giros efetuados, para controle do limite físico.

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		girarGarra();
	}

	void girarGarra(){

		/* Aqui abaixo é controlado o giro entorno da base */
		if(Input.GetKey("e") && turnTime < 60){// Verifica se a seta para a direita está pressionada.
            transform.Rotate(new Vector3(0, 0, 1), -turnSpeed * Time.deltaTime);// O primeiro parametro é o eixo no qual será feito o giro
            																	// o segundo é a velocidade vezes o tempo(esse tempo é para ser constante em todos os computadores).
			turnTime++;
		} else if(Input.GetKey("d") && turnTime > -20){// Verifica se a seta para a esquerda está pressionada.
            transform.Rotate(new Vector3(0, 0, 1), turnSpeed * Time.deltaTime);// O primeiro parametro é o eixo no qual será feito o giro
            																	// o segundo é a velocidade vezes o tempo(esse tempo é para ser constante em todos os computadores).
			turnTime--;
		}
	}
}
