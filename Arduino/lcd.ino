#include <LiquidCrystal.h> //Подключаем библиотеку для работы с LCD
LiquidCrystal lcd(12, 11, 10, 5, 4, 3, 2); // инициализируем LCD, указывая управляющие контакты
void setup() {
  lcd.begin(16, 2);// задаем размерность дисплея
  lcd.print("PaduoTex"); // выводим на дисплей традиционную фразу
}
void loop() {
}
