// vector2.h
#pragma once


#ifdef VECTOR2_EXPORT
#define VECTOR2_API __declspec(dllexport)
#else
#define VECTOR2_API __declspec(dllimport)
#endif

struct VECTOR2_API Vector2
{
    float x;
    float y;

    // Конструкторы
    Vector2() : x(0.0f), y(0.0f) {}
    Vector2(float x, float y) : x(x), y(y) {}

    // Методы (уже были)
    Vector2 Add(const Vector2& other) const;
    Vector2 Subtract(const Vector2& other) const;

    // Новые методы
    Vector2 Multiply(float scalar) const;
    float Length() const;
    Vector2 Normalize() const;

    // Перегруженные операторы
    Vector2 operator+(const Vector2& other) const;
    Vector2 operator-(const Vector2& other) const;
    Vector2 operator*(float scalar) const;
};
