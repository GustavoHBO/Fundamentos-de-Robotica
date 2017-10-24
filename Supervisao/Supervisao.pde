import processing.serial.*;

Serial myPort; 
PImage img;
int luz = 0, toque = 0, distancia = 0;
int iniciarX = 680, iniciarY= 110, iniciarH = 140, iniciarW = 90;
int pararX = 820, pararY= 110, pararH = 90, pararW = 90;
String status = "Parado";
void setup() {
  size(1000, 600); // Creating the display window and defining its' size.
  smooth();

  img = loadImage("map.png");
  println(Serial.list());
  String portName = Serial.list()[2]; 
  myPort = new Serial(this, portName, 9600); // Initializing the serial port.
}

void draw() {
  background(240, 240, 240);    
  textSize (20);
  
  fill (0, 0, 0);
  text("Supervisão:", 265, 60); //titulo
  image(img, 40, 100);

  fill (30, 140, 40); //verde
  //rect ( x, y, largura, altura, arredondamento)
  rect(iniciarX, iniciarY, iniciarH+40, iniciarW, 20); //start
  fill(255);
  text("INICIAR", 689, 160); 
  fill (160, 50, 20); //vermelho
  rect(pararX, pararY, pararH, pararW, 20); //stop
  fill(255);
  text("PARAR", 835, 160);

  fill(0);
  text("Status:               " + status, 670, 280);
  text("Sensor de Luz:             " + luz, 670, 340);
  text("Sensor de Carga:          " + toque, 670, 380);
  text("Dist. percorrida (m):     " + distancia, 670, 420);
  //println(inByte);
  
  //gps
  fill (70, 230, 70); //verde
  rect(307, 165, 20, 20, 50); //gps
  //base: 307, 165
  
  //Sensor communication

  while (myPort.available() > 0) {
    luz = myPort.read();
    println("Luz: "+luz);
    toque = myPort.read();
    println("Toque: "+toque);    
  }
}

//botoes iniciar e parar
void mousePressed() {
    if(mouseX>iniciarX && mouseX <iniciarX+iniciarW && mouseY>iniciarY && mouseY <iniciarY+iniciarH){
      println("kkkkk");
    }
    if(mouseX>pararX && mouseX <pararX+pararW && mouseY>pararY && mouseY <pararY+pararH){
      println("uuuuuu");
    }
}
//void keyPressed(){
  
//  switch (keyCode) { 
//  case UP: 
//    myPort.write('w'); 
//    println("UP!"); 
//    fill(255, 0, 0); 
//    triangle(750, 235, 800, 160, 850, 235);

//    break;
//  case DOWN:
//    myPort.write('s');
//    println("DOWN!");
//    fill(255, 0, 0);
//    rect(750, 250, 100, 100, 7);

//    break; 
//  case LEFT:
//    myPort.write('a');
//    println("LEFT!");
//    fill(255, 0, 0);
//    triangle(735, 350, 660, 300, 735, 250);

//    break;
//  case RIGHT:
//    myPort.write('d');
//    println("RIGHT!");
//    fill(255, 0, 0);
//    triangle(865, 250, 940, 300, 865, 350);

//    break;

//  case ' ' :
//    myPort.write ('z');
//    println("STOP!");
//    fill(255, 0, 0);
//    rect(50, 250, 400, 100, 5);

//    break;
//  default:
//    break;
//  }
//}