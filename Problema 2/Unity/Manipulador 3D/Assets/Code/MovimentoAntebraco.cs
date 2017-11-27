using UnityEngine;
using System.Collections;

public class MovimentoAntebraco : MonoBehaviour {

	int turnSpeed = 50;// Velocidade do giro

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		girarAntebraco();
	}

	void girarAntebraco(){
		
		/* Aqui abaixo é controlado o giro entorno da base */
		if(Input.GetKey("w")){// Verifica se a seta para a direita está pressionada.
            transform.Rotate(new Vector3(0, 0, 1), -turnSpeed * Time.deltaTime);// O primeiro parametro é o eixo no qual será feito o giro
            																	// o segundo é a velocidade vezes o tempo(esse tempo é para ser constante em todos os computadores).
		} else if(Input.GetKey("s")){// Verifica se a seta para a esquerda está pressionada.
            transform.Rotate(new Vector3(0, 0, 1), turnSpeed * Time.deltaTime);// O primeiro parametro é o eixo no qual será feito o giro
            																	// o segundo é a velocidade vezes o tempo(esse tempo é para ser constante em todos os computadores).
		}
	}
}
