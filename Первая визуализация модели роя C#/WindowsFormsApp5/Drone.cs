using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp5
{
    public class Drone // Класс для представления дрона
    {
        public int positionX { get; set; } // Свойство для хранения позиции дрона
        public int positionY { get; set; } // Свойство для хранения позиции дрона
        //public double Speed { get; set; } // Свойство для хранения скорости дрона
        public bool IsLeader { get; set; } // Свойство для определения является ли дрон лидером

    }
    public class DroneSwarm
    {
        public  List<Drone> drones; // Список дронов
        public Drone leader; // Лидер дронов

        public List<Drone> GetDrones()
        {
            return drones;
        }

        // Метод для установки нового лидера
        public void SetLeader(Drone newLeader)
        {
            leader = newLeader; // Устанавливаем нового лидера
            leader.IsLeader = true;// Устанавливаем статус лидера
        }
        public Drone GetLeader()
        {
            return leader;
        }
        
        public void updateCoordinaties(int[] dronePositionsX, int[] dronePositionsY, int maxSpeed, int centerX, int centerY, int[] newPositionsX, int[] newPositionsY)
        {
            //расстояние для лидера до цели (лидер первый в массиве)
            double distanceToPoint = Math.Sqrt(Math.Pow(dronePositionsX[0] - centerX, 2) + Math.Pow(dronePositionsY[0] - centerY, 2));
            if (distanceToPoint > maxSpeed)
            {
                double angle = Math.Atan2(centerY - dronePositionsY[0], centerX - dronePositionsX[0]);
                dronePositionsX[0] += (int)(maxSpeed * Math.Cos(angle));
                dronePositionsY[0] += (int)(maxSpeed * Math.Sin(angle));
            }
            else
            {
                dronePositionsX[0] = centerX;
                dronePositionsY[0] = centerY;
            }
            newPositionsX[0] = dronePositionsX[0];
            newPositionsY[0] = dronePositionsY[0];

            //расстояние отсальных дронов до лидера (они не знают координаты точки сбора)
            for (int i = 1; i < dronePositionsX.Length; i++)
            {
                distanceToPoint = Math.Sqrt(Math.Pow(dronePositionsX[i] - dronePositionsX[0], 2) + Math.Pow(dronePositionsY[i] - dronePositionsY[0], 2));
                if (distanceToPoint > maxSpeed)
                {
                    double angle = Math.Atan2(dronePositionsY[0] - dronePositionsY[i], dronePositionsX[0] - dronePositionsX[i]);
                    dronePositionsX[i] += (int)(maxSpeed * Math.Cos(angle));
                    dronePositionsY[i] += (int)(maxSpeed * Math.Sin(angle));

                }
                else
                {
                    dronePositionsX[i] = dronePositionsX[0];
                    dronePositionsY[i] = dronePositionsY[0];
                }
                newPositionsX[i] = dronePositionsX[i];
                newPositionsY[i] = dronePositionsY[i];
            }
        }
    }
}