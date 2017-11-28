using UnityEngine;
using System.Collections;

public class MovimentoGarraRotacao : MonoBehaviour {

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
		/* Aqui abaixo é controlado o giro entorno do pivot */
		if(Input.GetKey("r") && turnTime < 60){// Verifica se a seta para cima está pressionada.
			transform.Rotate(new Vector3(0, turnSpeed, 0) * Time.deltaTime);// O vetor é o eixo no qual será feito o giro
            																	// o Time.deltaTime é a velocidade vezes o tempo(esse tempo é para ser constante em todos os computadores).
			turnTime++;
		} else if(Input.GetKey("f") && turnTime > -60){// Verifica se a seta para baixo está pressionada.
			transform.Rotate(new Vector3(0, -turnSpeed, 0) * Time.deltaTime);// O vetor é o eixo no qual será feito o giro
            																	// o segundo é a velocidade vezes o tempo(esse tempo é para ser constante em todos os computadores).
			turnTime--;
		}
	}
}
