#include "HID-Project.h"
bool isOutSide = false;
const unsigned long debounceDelay = 15;
const int numPins = 22;
bool lastButtonState[numPins] = { LOW };
uint8_t currentButtonSate[numPins] = { 0 };
unsigned long lastDebounceTime[numPins] = { 0 };
bool isHolding[numPins] = { false };

uint8_t buttonArrayIn[22] = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
uint8_t buttonArrayOut[22] = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
int data[13] = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
void setup() {
  for (int i = 0; i < numPins; i++) {
    pinMode(i, INPUT);
  }
  Serial.begin(115200);
  AbsoluteMouse.begin();
  Keyboard.begin();
}

void loop() {
  for (int i = 0; i < numPins; i++) {
    HandleInput(i, i);
  }
  // use serial input to control the mouse:
  if (Serial.available() > 0) {
    String incomingData = "";
    incomingData = Serial.readStringUntil('\n');
    incomingData.trim();
    int index = 0;
    int stringCount = 0;
    while ((index = incomingData.indexOf(' ')) != -1) {
      data[stringCount++] = incomingData.substring(0, index).toInt();
      incomingData = incomingData.substring(index + 1);
    }
    data[stringCount++] = incomingData.toInt();
    incomingData = "";
    stringCount = 0;
    ProcessData();
  }
}

void HandleInput(int pin, uint8_t indexButton) {
  unsigned long currentMillis = millis();
  bool reading = digitalRead(pin);

  if (reading != lastButtonState[pin]) {
    lastDebounceTime[pin] = currentMillis;
  }

  if ((currentMillis - lastDebounceTime[pin]) > debounceDelay) {
    uint8_t button = isOutSide ? buttonArrayOut[indexButton] : buttonArrayIn[indexButton];
    if (reading == HIGH && !isHolding[pin]) {
      currentButtonSate[indexButton] = button;
      if (button == 254) {
        Keyboard.press(KEY_LEFT_CTRL);
        Keyboard.press('b');
      }

      if (button == MOUSE_LEFT || button == MOUSE_RIGHT || button == 0x03) {
        if (button != 0x03) {
          Mouse.press(button);

        } else {
          Mouse.press(MOUSE_MIDDLE);
        }

      } else {
        Keyboard.press(KeyboardKeycodeConverter(button));
      }
      isHolding[pin] = true;
    } else if (reading == LOW && isHolding[pin]) {
      currentButtonSate[indexButton] = 0;
      if (button == 254) {
        Keyboard.release('b');
        Keyboard.release(KEY_LEFT_CTRL);
      }
      if (button == MOUSE_LEFT || button == MOUSE_RIGHT || button == 0x03) {
        if (button != 0x03) {
          Mouse.release(button);

        } else
          Mouse.press(MOUSE_MIDDLE);
      } else {
        Keyboard.release(KeyboardKeycodeConverter(button));
      }
      isHolding[pin] = false;
    }
  }

  lastButtonState[pin] = reading;
}

void ReleaseCurrentButton() {
  for (int i = 0; i < numPins; i++) {
    if (isHolding[i]) {
      uint8_t button = isOutSide ?  buttonArrayIn[i]: buttonArrayOut[i];
      if (button == 254) {
        Keyboard.release('b');
        Keyboard.release(KEY_LEFT_CTRL);
      } else if (button == MOUSE_LEFT || button == MOUSE_RIGHT || button == 0x03) {
        if (button != 0x03) {
          Mouse.release(button);
        } else {
          Mouse.release(MOUSE_MIDDLE);
        }
      } else {
        Keyboard.release(KeyboardKeycodeConverter(button));
      }
      isHolding[i] = false;
    }
  }
}
void ProcessData() {
  switch (data[0]) {
    case 0:
      ProcessMouseData();
      break;
    case 1:
      AssignNewButton();
      break;
  }
}
void AssignNewButton() {
  if (data[1] == 0)
    buttonArrayIn[data[2]] = data[3];

  if (data[1] == 1)
    buttonArrayOut[data[2]] = data[3];
}

int16_t currentX = 0;
int16_t currentY = 0;
void ProcessMouseData() {
  // Screen resolution
  const int screenWidth = 640;
  const int screenHeight = 480;
  bool previousIsOutSide = isOutSide;

  if (data[1] <= 0 || data[1] >= screenWidth || data[2] <= 0 || data[2] >= screenHeight) {
    isOutSide = true;
  } else {
    isOutSide = false;
  }

  if (previousIsOutSide != isOutSide) {
    ReleaseCurrentButton();
  }

  if (isOutSide) {
    return;
  }

  int16_t posX = data[1] - (screenWidth / 2);
  int16_t posY = data[2] - (screenHeight / 2);

  posX = static_cast<int16_t>(posX * (32767.0 / (screenWidth / 2)));
  posY = static_cast<int16_t>(posY * (32767.0 / (screenHeight / 2)));

  MoveCursor(posX, posY);
}

void MoveCursor(int16_t posX, int16_t posY) {

  long targetX = static_cast<long>(posX);
  long targetY = static_cast<long>(posY);


  long moveX = targetX - static_cast<long>(currentX);
  long moveY = targetY - static_cast<long>(currentY);


  if (moveX > INT16_MAX || moveX < INT16_MIN || moveY > INT16_MAX || moveY < INT16_MIN) {
    int16_t halfMoveX = static_cast<int16_t>(moveX / 2);
    int16_t halfMoveY = static_cast<int16_t>(moveY / 2);

    // Move the cursor in two steps if > max int
    AbsoluteMouse.move(halfMoveX, halfMoveY);
    AbsoluteMouse.move(static_cast<int16_t>(moveX - halfMoveX), static_cast<int16_t>(moveY - halfMoveY));
  } else {
    AbsoluteMouse.move(static_cast<int16_t>(moveX), static_cast<int16_t>(moveY));
  }

  // Update the current position
  currentX = static_cast<int16_t>(targetX);
  currentY = static_cast<int16_t>(targetY);
}
KeyboardKeycode KeyboardKeycodeConverter(uint8_t value) {
  switch (value) {
    case 4: return KEY_A;
    case 5: return KEY_B;
    case 6: return KEY_C;
    case 7: return KEY_D;
    case 8: return KEY_E;
    case 9: return KEY_F;
    case 10: return KEY_G;
    case 11: return KEY_H;
    case 12: return KEY_I;
    case 13: return KEY_J;
    case 14: return KEY_K;
    case 15: return KEY_L;
    case 16: return KEY_M;
    case 17: return KEY_N;
    case 18: return KEY_O;
    case 19: return KEY_P;
    case 20: return KEY_Q;
    case 21: return KEY_R;
    case 22: return KEY_S;
    case 23: return KEY_T;
    case 24: return KEY_U;
    case 25: return KEY_V;
    case 26: return KEY_W;
    case 27: return KEY_X;
    case 28: return KEY_Y;
    case 29: return KEY_Z;
    case 30: return KEY_1;
    case 31: return KEY_2;
    case 32: return KEY_3;
    case 33: return KEY_4;
    case 34: return KEY_5;
    case 35: return KEY_6;
    case 36: return KEY_7;
    case 37: return KEY_8;
    case 38: return KEY_9;
    case 39: return KEY_0;
    case 40: return KEY_RETURN;
    case 41: return KEY_ESC;
    case 42: return KEY_BACKSPACE;
    case 43: return KEY_TAB;
    case 44: return KEY_SPACE;
    case 58: return KEY_F1;
    case 59: return KEY_F2;
    case 60: return KEY_F3;
    case 61: return KEY_F4;
    case 62: return KEY_F5;
    case 63: return KEY_F6;
    case 64: return KEY_F7;
    case 65: return KEY_F8;
    case 66: return KEY_F9;
    case 67: return KEY_F10;
    case 68: return KEY_F11;
    case 69: return KEY_F12;
    case 79: return KEY_RIGHT_ARROW;
    case 80: return KEY_LEFT_ARROW;
    case 81: return KEY_DOWN_ARROW;
    case 82: return KEY_UP_ARROW;
    default: return 0;  // No key
  }
}