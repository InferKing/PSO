using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AvoidCollisions
{
    public struct Vector3
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public Vector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public static Vector3 operator -(Vector3 v1, Vector3 v2)
        {
            return new Vector3(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        }
        public override string ToString()
        {
            return $"({X},{Y},{Z})";
        }
    }
    public class Drone // класс для представления дрона в виде сферы
    {
        public bool IsLeader { get; set; } // свойство для определения является ли дрон лидером
        public Vector3 _center; // вектор координат, центр "сферы"
        public float _radius = 5; // радиус "сферы" - минимальное расстояние между дронами
        public Vector3 Center { get { return _center; } set {  _center = value; } }
        public float Radius { get { return _radius; } set { _radius = value; } }

        // функция для обнаружения возможности столкновения дронов
        // вызывается при каждой итерации при обновлении координат всех дронов
        // на каждой итерации текущий дрон проверяет всех остальных дронов
        public bool CollidesWith(Drone obstacle, double tolerance = 0.0d)
        {
            Vector3 diff = Center - obstacle.Center;
            double distance = Math.Sqrt(Math.Pow(diff.X, 2) + Math.Pow(diff.Y, 2) + Math.Pow(diff.Z, 2));
            double sumRadius = Radius + obstacle.Radius;
            return distance < (sumRadius + tolerance);
        }
        
        
    }
}