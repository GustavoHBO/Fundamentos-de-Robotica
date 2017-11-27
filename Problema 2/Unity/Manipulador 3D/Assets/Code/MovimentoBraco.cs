using UnityEngine;
using System.Collections;

/*
 *Esta classe é responsável pelo controle dos movimentos que o braço efetua.
 *
 * É responsável também por monitorar o movimento que a base faz, acompanhando-a.
 */

public class MovimentoBraco : MonoBehaviour {

	int turnSpeed = 5;// Velocidade do giro
	float xRotation = 0;
	float yRotation = 0;
	float zRotation = 0;

	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		girarBraco();// Responsável por controlar o giro do braço robótico.
	}

	void girarBraco(){

		//var pivot = Transform = transform.Find("pivo");
		//this.transform.RotateAround(transform.Find("robotic_arm_rotate").position, Vector3.up, -20); 

		/* Aqui abaixo é controlado o giro entorno da base */
		if(Input.GetKey(KeyCode.RightArrow)){// Verifica se a seta para a direita está pressionada.
			//transform.Rotate(new Vector3(0, turnSpeed, 0) * Time.deltaTime);// O vetor é o eixo no qual será feito o giro
            																	// o TIme.deltaTime é a velocidade vezes o tempo(esse tempo é para ser constante em todos os computadores).
			this.transform.RotateAround(transform.Find("robotic_arm_rotate").position, Vector3.up, turnSpeed);
		} else if(Input.GetKey(KeyCode.LeftArrow)){// Verifica se a seta para a esquerda está pressionada.
            //transform.Rotate(new Vector3(0, -turnSpeed, 0) * Time.deltaTime);// O vetor é o eixo no qual será feito o giro
            																	// o segundo é a velocidade vezes o tempo(esse tempo é para ser constante em todos os computadores).
			this.transform.RotateAround(transform.Find("robotic_arm_rotate").position, Vector3.up, -turnSpeed);
		}

		/* Aqui abaixo é controlado o giro entorno do pivot */
		if(Input.GetKey(KeyCode.UpArrow)){// Verifica se a seta para cima está pressionada.
			//yRotation += Input.GetAxis("Horizontal");
        	//transform.eulerAngles = new Vector3(0, 0, yRotation);
        	this.transform.RotateAround(transform.Find("robotic_arm_rotate").position, Vector3.forward, turnSpeed);
		} else if(Input.GetKey(KeyCode.DownArrow)){// Verifica se a seta para baixo está pressionada.
			//transform.Rotate(new Vector3(0, 0, turnSpeed) * Time.deltaTime);// O vetor é o eixo no qual será feito o giro
            																	// o segundo é a velocidade vezes o tempo(esse tempo é para ser constante em todos os computadores).
			this.transform.RotateAround(transform.Find("robotic_arm_rotate").position, Vector3.forward, -turnSpeed);
		}

		if(Input.GetKey("a")){
			
			this.transform.RotateAround(transform.Find("robotic_arm_rotate").position, Vector3.forward, -20); 

			//transform.Rotate(new Vector3(0, 0, 1), turnSpeed * Time.deltaTime);// O vetor é o eixo no qual será feito o giro
			//yRotation += Input.GetAxis("Horizontal");
        	//transform.eulerAngles = new Vector3(0, 0, yRotation);
		}
	}
}
