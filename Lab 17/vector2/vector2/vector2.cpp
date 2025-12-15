// vector2.cpp
#define VECTOR2_EXPORT
#include "vector2.h"
#include <cmath>  // Для sqrt

// Реализация существующих методов
Vector2 Vector2::Add(const Vector2& other) const
{
    return Vector2(x + other.x, y + other.y);
}

Vector2 Vector2::Subtract(const Vector2& other) const
{
    return Vector2(x - other.x, y - other.y);
}

// Новые реализации
Vector2 Vector2::Multiply(float scalar) const
{
    return Vector2(x * scalar, y * scalar);
}

float Vector2::Length() const
{
    return std::sqrt(x * x + y * y);
}

Vector2 Vector2::Normalize() const
{
    float len = Length();
    if (len == 0.0f)
        return Vector2(0.0f, 0.0f);
    return Vector2(x / len, y / len);
}

// Перегруженные операторы
Vector2 Vector2::operator+(const Vector2& other) const
{
    return Add(other);
}

Vector2 Vector2::operator-(const Vector2& other) const
{
    return Subtract(other);
}

Vector2 Vector2::operator*(float scalar) const
{
    return Multiply(scalar);
}
