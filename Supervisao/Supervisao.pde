import processing.serial.*;

Serial myPort; 
PImage img;
//dados recebidos por bluetooth
int luz = 0, toque = 0;
int eixoX = 0, eixoY = 0;
boolean decremento =false, incremento=true;
//gps
//base: x = 307, y = 165 map1
//base: x = 112, y = 288 map2
//canto inf/esq: x = 84, y = 496;
int xIni = 112, yIni = 288, x=0, y=0, xAnt = 0, yAnt = 0, xDecrement = 0, yDecrement = 0,sentido =1;
//botoes
int iniciarX = 640, iniciarY= 110, iniciarH = 140, iniciarW = 90;
int pararX = 780, pararY= 110, pararH = 90, pararW = 90;
//labels
int  distancia = 0;
String status = "Parado";
void setup() {
  size(1000, 620); // Creating the display window and defining its' size.
  smooth();
  x=xIni; y=yIni;
  img = loadImage("map2.png");
  println(Serial.list());
  String portName = Serial.list()[2]; 
  myPort = new Serial(this, portName, 9600); // Initializing the serial port.
}

void draw() {
  background(240, 240, 240);    
  textSize (20);
  
  fill (0, 0, 0);
  text("Supervisão:", 690, 80); //titulo
  image(img, 50, 30);

  fill (30, 140, 40); //verde
  //rect ( x, y, largura, altura, arredondamento)
  rect(iniciarX, iniciarY, iniciarH+40, iniciarW, 20); //start
  fill(255);
  text("INICIAR", 669, 160); 
  fill (160, 50, 20); //vermelho
  rect(pararX, pararY, pararH, pararW, 20); //stop
  fill(255);
  text("PARAR", 795, 160);

  fill(0);
  text("Status:               " + status, 630, 280);
  text("Sensor de Luz:             " + luz, 630, 340);
  text("Sensor de Carga:          " + toque, 630, 380);
  distancia = eixoX+eixoY;
  text("Dist. percorrida (cm):    " + distancia, 630, 420);
  //println(inByte);
  
  //gps
  fill (70, 230, 70); //verde
  rect(x, y, 20, 20, 50); //gps  
  
  //Sensor communication
  while (myPort.available() > 0) {
    luz = myPort.read();
    println("Luz: "+luz);
    toque = myPort.read();
    println("Toque: "+toque);
    eixoX = myPort.read();    
    println("Eixo X: "+eixoX);
    eixoY = myPort.read();    
    println("Eixo Y: "+eixoY);
    sentido = myPort.read();
    println("Sentido: "+sentido);
    //recalculo do gps
    if(sentido == -1 && !decremento){
      decremento =true;
      incremento =false;
      xAnt = x;
      yAnt = y;
    }else if (!incremento){
      decremento =false;
      incremento = true;
      xDecrement = xDecrement + x - xAnt;
      yDecrement = yDecrement + y - yAnt;
    }
    x = eixoX*2 - xDecrement + xIni;
    y = eixoY*2 - yDecrement + yIni;
  }
}

//botoes iniciar e parar
void mousePressed() {
    if(mouseX>iniciarX && mouseX <iniciarX+iniciarH && mouseY>iniciarY && mouseY <iniciarY+iniciarW){
      println("INICIAR!"); 
      myPort.write('i'); 
    }
    if(mouseX>pararX && mouseX <pararX+pararH && mouseY>pararY && mouseY <pararY+pararW){
      println("PARAR!"); 
      myPort.write('p');
    }
}