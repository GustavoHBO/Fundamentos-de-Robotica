#pragma config(Sensor, S3,		 lightSensor,					sensorLightActive)
#pragma config(Sensor, S1,		 touchSensor,		 sensorTouch)
//*!!Code automatically generated by 'ROBOTC' configuration wizard							 !!*//
#pragma platform(NXT)
#define speed 30
#define branco 45
#define prata 66

task main(){
  bool fromLeft = true;
  wait1Msec(300);

  //sensor lendo preto
  while(true){

    if(SensorValue(lightSensor) < branco){//mantem direcao reta

      motor[motorB] = speed;
      motor[motorC] = speed;
    }else if (SensorValue(lightSensor) >= branco && SensorValue(lightSensor) < prata){//branco
      if(fromLeft){//se veio da esquerda tenta a 1 correcao pra direita
        motor[motorB] = speed-20;//direita
        motor[motorC] = speed;
        fromLeft =false;
      }else{//se veio da direita tenta a 1 correcao pra esquerda
        motor[motorB] = speed;//esquerda
        motor[motorC] = speed-20;
        fromLeft =true;
      }
      wait1Msec(200);
      //se apos um tempo, continuar no branco
      if(SensorValue(lightSensor) >= branco && SensorValue(lightSensor)< prata){//continua no branco
        if(fromLeft){//tenta pro lado oposto ao da 1 tentativa
          motor[motorB] = speed-20;//vira para a direita
          motor[motorC] = speed;
          fromLeft =true;
        }else{
          motor[motorB] = speed;//vira para a esquerda
          motor[motorC] = speed-20;
          fromLeft =false;
        }
        wait1Msec(500);
      }
    }else if(SensorValue(lightSensor) >= prata){//prata
      motor[motorB] = speed;
	    motor[motorC] = speed;
	    wait1Msec(400);
  		motor[motorB] = speed;
	    motor[motorC] = -speed;
	    wait1Msec(520);
	    motor[motorB] = 0;
	    motor[motorC] = 0;
	    wait1Msec(5000);

      while(true){
      	if (SensorValue(lightSensor) >= prata){
		      motor[motorB] = speed;
		      motor[motorC] = speed;
	      }else if (SensorValue(lightSensor) >= branco && SensorValue(lightSensor) < prata){//branco
		      if(fromLeft){//se veio da esquerda tenta a 1 correcao pra direita
		        motor[motorB] = speed-20;//direita
		        motor[motorC] = speed;
		        fromLeft =false;
		      }else{//se veio da direita tenta a 1 correcao pra esquerda
		        motor[motorB] = speed;//esquerda
		        motor[motorC] = speed-20;
		        fromLeft =true;
		      }
		      wait1Msec(200);
		      //se apos um tempo, continuar no branco
		      if(SensorValue(lightSensor) >= branco && SensorValue(lightSensor)< prata){//continua no branco
		        if(fromLeft){//tenta pro lado oposto ao da 1 tentativa
		          motor[motorB] = speed-20;//vira para a direita
		          motor[motorC] = speed;
		          fromLeft =true;
		        }else{
		          motor[motorB] = speed;//vira para a esquerda
		          motor[motorC] = speed-20;
		          fromLeft =false;
		        }
		        wait1Msec(500);
		        if(SensorValue(lightSensor) >= branco && SensorValue(lightSensor) < prata){
				      //curva, rotaciona ate achar preto ou prata
				      while(SensorValue(lightSensor) >= branco && SensorValue(lightSensor)< prata){
				        //o sentido da rotacao eh dado pelo vetor rote
				        motor[motorB] = speed;//curva
				        motor[motorC] = -speed;
				      }
				      wait1Msec(100);
				      if(SensorValue(lightSensor) < branco){
				      	break;
				      }
				      if(SensorValue(touchSensor)){
					      while(SensorValue(touchSensor)){
					      	motor[motorB] = 0;
			      			motor[motorC] = 0;
				        }
				      }else{
				        while(!SensorValue(touchSensor)){
					      	motor[motorB] = 0;
			      			motor[motorC] = 0;
				        }
				      }
				    }
		      }
		    }
	    }
	  }
	}
}




// task main(){
// wait1Msec(1000);
// motor[motorB] =0;
// motor[motorC] =0;
// nMotorEncoder(motorB) = 0;
// nMotorEncoder(motorC) = 0;
// while(nMotorEncoder(motorB) < (160*360)/17.3){
// motor[motorB] = 20;
// motor[motorC] = 20;
// }
// motor[motorB] =0;
// motor[motorC] =0;
// }
//17.3cm circunferencia
