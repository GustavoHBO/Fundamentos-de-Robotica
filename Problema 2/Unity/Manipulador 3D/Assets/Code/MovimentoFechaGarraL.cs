using UnityEngine;
using System.Collections;

public class MovimentoFechaGarraL : MonoBehaviour {

	int turnSpeed = 50;
	int turnTime = 0; // Quantidade de giros efetuados, para controle do limite físico.

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		fecharGarra();
	}

	void fecharGarra(){

		/* Aqui abaixo é controlado o giro entorno da base */
		if(Input.GetKey("t") && turnTime < 0){// Verifica se a seta para a direita está pressionada.
            transform.Rotate(new Vector3(1, 0, 0), turnSpeed * Time.deltaTime);// O primeiro parametro é o eixo no qual será feito o giro
            																	// o segundo é a velocidade vezes o tempo(esse tempo é para ser constante em todos os computadores).
			turnTime++;
		} else if(Input.GetKey("g") && turnTime > -20){// Verifica se a seta para a esquerda está pressionada.
            transform.Rotate(new Vector3(1, 0, 0), -turnSpeed * Time.deltaTime);// O primeiro parametro é o eixo no qual será feito o giro
            																	// o segundo é a velocidade vezes o tempo(esse tempo é para ser constante em todos os computadores).
			turnTime--;
		}

	}
}
