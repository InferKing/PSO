using System;
using System.Collections.Generic;
using System.Numerics;

namespace cstest
{
    public class Drone
    {
        public Vector3 Position;
        public Vector3 Direction;
        const int COLLISION_THRESHOLD = 10, COLLISION_ANGLE_THRESHOLD = 10;

        // Checks for collisions with other drones and changes direction if necessary
        public void CheckCollision(List<Drone> drones)
        {
            foreach (Drone otherDrone in drones)
            {
                if (otherDrone == this) { continue; } // Skip self

                // Get distance between positions of two drones
                float distance = (Position - otherDrone.Position).magnitude;

                // If distance is less than some threshold value, potential collision detected
                if (distance < COLLISION_THRESHOLD)
                {
                    // Get angle between two drone directions
                    float angle = Vector3.Angle(Direction, otherDrone.Direction);

                    if (angle < COLLISION_ANGLE_THRESHOLD)
                    {
                        // Calculate new direction to avoid collision
                        Vector3 newDirection = Vector3.Cross(Direction, otherDrone.Direction);

                        // Update drone direction
                        Direction = Vector3.Normalize(newDirection);

                        // Call function recursively to make sure drone does not collide with any other nearby drone in new direction
                        CheckCollision(drones);
                    }
                }
            }
        }

        // Move drone based on direction and speed
        public void Move(float speed)
        {
            Position += Direction * speed;
        }
    }

    class Variant2
    {
        // Example usage:
        public void Main()
        {
            // Create list of drones
            List<Drone> drones = new List<Drone>();

            // Add some drones
            drones.Add(new Drone() { Position = new Vector3(0, 0, 0), Direction = new Vector3(1, 0, 0) });
            drones.Add(new Drone() { Position = new Vector3(10, 0, 0), Direction = new Vector3(-1, 0, 0) });
            drones.Add(new Drone() { Position = new Vector3(20, 0, 0), Direction = new Vector3(1, 0, 0) });

            // Move drones and check for collisions
            foreach (Drone drone in drones)
            {
                drone.Move(1f);
                drone.CheckCollision(drones);
            }
        }
    }
}

///////////////////////////////////////////////////////////////////////////////////////////

namespace second
{
    public struct Vector3
    {
        public float X, Y, Z;

        public float Magnitude()
        {
            return (float)Math.Sqrt(X * X + Y * Y + Z * Z);
        }

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        // Метод для получения нормализованного вектора
        public Vector3 Normalized()
        {
            float magnitude = Magnitude();
            return new Vector3
            {
                X = X / magnitude,
                Y = Y / magnitude,
                Z = Z / magnitude
            };
        }

        public static float Distance(Vector3 v1, Vector3 v2)
        {
            return (float)Math.Sqrt(Math.Pow(v1.X - v2.X, 2) + Math.Pow(v1.Y - v2.Y, 2) + Math.Pow(v1.Z - v2.Z, 2));
        }

        public static Vector3 operator*(Vector3 v, float c)
        {
            return new Vector3(v.X * c, v.Y * c, v.Z * c);
        }
        public static Vector3 operator *(float c, Vector3 v)
        {
            return new Vector3(v.X * c, v.Y * c, v.Z * c);
        }
        public static Vector3 operator +(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        }
        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }
    }
    public class Drone
    {
        public Vector3 position;
        public float speed;
        public float radius;

        // Конструктор для создания дрона с заданной позицией, скоростью и радиусом
        public Drone(Vector3 position, float speed, float radius)
        {
            this.position = position;
            this.speed = speed;
            this.radius = radius;
        }
        

        // Метод для вычисления угла между двумя векторами
        public float Angle(Vector3 other)
        {
            float dotProduct = position.X * other.X + position.Y * other.Y + position.Z * other.Z;
            float magnitudeProduct = position.Magnitude() * other.Magnitude();
            return (float)Math.Acos(dotProduct / magnitudeProduct);
        }

        // Метод для перемещения дрона на новую позицию на основе текущей скорости
        public void Move(Vector3 direction, float deltaTime)
        {
            position += direction * speed * deltaTime;
        }


        // Метод для обнаружения столкновений между двумя дронами
        public static bool DetectCollision(Drone drone1, Drone drone2, float deltaTime)
        {
            // Вычисляем расстояние между центрами дронов
            float distance = Vector3.Distance(drone1.position, drone2.position);

            // Если расстояние меньше чем сумма радиусов дронов, значит они столкнутся в следующий кадр
            if (distance < drone1.radius + drone2.radius) return true;

            // Рассчитываем будущую позицию дрона 1
            Vector3 futurePosition1 = drone1.position + drone1.speed * 
                deltaTime * (drone2.position - drone1.position).Normalized();

            // Рассчитываем будущую позицию дрона 2
            Vector3 futurePosition2 = drone2.position + drone2.speed * 
                deltaTime * (drone1.position - drone2.position).Normalized();

            // Вычисляем расстояние между будущими позициями дронов
            float futureDistance = Vector3.Distance(futurePosition1, futurePosition2);

            // Если расстояние меньше чем сумма радиусов дронов, значит они столкнутся в следующий кадр
            if (futureDistance < drone1.radius + drone2.radius) return true;

            // Если столкновение не обнаружено, возвращаем false
            return false;
        }

        // Метод для избежания столкновений с другими дронами и нулевой высотой
        public static Vector3 AvoidCollisions(List<Drone> drones, Vector3 targetPosition, float deltaTime, float safeDistance)
        {
            // Создаем переменную для новой позиции дрона
            Vector3 newPosition = targetPosition;

            // Проходимся по каждому дрону в списке
            foreach (Drone drone in drones)
            {
                // Если дрон находится на нулевой высоте, то пропускаем его
                if (drone.position.Y <= 0) continue;

                // Если дрон находится достаточно близко, чтобы столкнуться, выполняем маневр для его избежания
                if (DetectCollision(drone, new Drone(targetPosition, 0, safeDistance), deltaTime))
                {
                    // Рассчитываем направление, в котором нужно двигаться, чтобы избежать столкновения с текущим дроном
                    Vector3 direction = (targetPosition - drone.position).Normalized();

                    // Вычисляем новую позицию, уклоняясь от текущего дрона в направлении, противоположном от его текущего движения
                    newPosition += direction * (safeDistance - Vector3.Distance(drone.position, targetPosition)) * 2;
                }
            }

            // Возвращаем новую позицию дрона
            return newPosition;
        }
    }

    
}