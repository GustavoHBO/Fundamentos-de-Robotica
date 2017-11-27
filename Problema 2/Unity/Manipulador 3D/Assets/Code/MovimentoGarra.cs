using UnityEngine;
using System.Collections;

public class MovimentoGarra : MonoBehaviour {

	int turnSpeed = 50;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		girarGarra();
	}

	void girarGarra(){

		/* Aqui abaixo é controlado o giro entorno da base */
		if(Input.GetKey("e")){// Verifica se a seta para a direita está pressionada.
            transform.Rotate(new Vector3(0, 0, 1), -turnSpeed * Time.deltaTime);// O primeiro parametro é o eixo no qual será feito o giro
            																	// o segundo é a velocidade vezes o tempo(esse tempo é para ser constante em todos os computadores).
		} else if(Input.GetKey("d")){// Verifica se a seta para a esquerda está pressionada.
            transform.Rotate(new Vector3(0, 0, 1), turnSpeed * Time.deltaTime);// O primeiro parametro é o eixo no qual será feito o giro
            																	// o segundo é a velocidade vezes o tempo(esse tempo é para ser constante em todos os computadores).
		}
	}
}
