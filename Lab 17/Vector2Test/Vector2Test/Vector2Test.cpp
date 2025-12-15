// main.cpp
#include <iostream>
#include "C:\Users\user\Desktop\Vector2\Vector2\vector2.h"  // Путь к vector2.h

int main()
{
    // Создание векторов
    Vector2 v1(3.0f, 4.0f);
    Vector2 v2(1.0f, 2.0f);

    // Сложение
    Vector2 sum = v1 + v2;
    std::cout << "Sum: (" << sum.x << ", " << sum.y << ")\n";

    // Вычитание
    Vector2 diff = v1 - v2;
    std::cout << "Diff: (" << diff.x << ", " << diff.y << ")\n";


    // Умножение на скаляр
    Vector2 scaled = v1 * 2.0f;
    std::cout << "Scaled: (" << scaled.x << ", " << scaled.y << ")\n";

    // Длина вектора
    float length = v1.Length();
    std::cout << "Length of v1: " << length << "\n";


    // Нормализация
    Vector2 normalized = v1.Normalize();
    std::cout << "Normalized v1: (" << normalized.x << ", " << normalized.y << ")\n";


    return 0;
}

