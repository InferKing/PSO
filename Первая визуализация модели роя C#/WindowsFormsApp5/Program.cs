using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp5
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Application.Run(new addDrone());

            List<Drone> listDrones = new List<Drone>();
            DroneSwarm swarm = new DroneSwarm();
            listDrones.Add(new Drone { positionX = 130, positionY = 50 });
            listDrones.Add(new Drone { positionX = 60, positionY = 100 });
            listDrones.Add(new Drone { positionX = 10, positionY = 40 });
            listDrones.Add(new Drone { positionX = 356, positionY = 498 });
            listDrones.Add(new Drone { positionX = 72, positionY = 469, IsLeader = true });
            listDrones.Add(new Drone { positionX = 550, positionY = 80, });
            listDrones.Add(new Drone { positionX = 700, positionY = 290 });
            listDrones.Add(new Drone { positionX = 650, positionY = 310 });

           //swarm.drones = DronesArray.list_drones;
            swarm.drones = listDrones;

            int targetX = 0;// Заданная точка X
            int targetY = 0; // Заданная точка Y

            Application.Run(new target(targetX, targetY));
            targetX = globalTarget.target[0];
            targetY = globalTarget.target[1];

            // Позиции дронов относительно заданной точки
            int[] dronePositionsX = new int[swarm.drones.Count()];
            int[] dronePositionsY = new int[swarm.drones.Count()];


            // запоминаем стартовые позиции дронов
            int[] startPositionsX = new int[swarm.drones.Count()];
            int[] startPositionsY = new int[swarm.drones.Count()];

            for (int i = 0; i < swarm.drones.Count(); i++)
            {
                startPositionsX[i] = swarm.drones[i].positionX;
                startPositionsY[i] = swarm.drones[i].positionY;
            }

            //массив для хранения позиции дронов от начальной до точки сбора
            int[] newPositionsX;
            int[] newPositionsY;

            //массив для хранения  позиции дронов от точки сбора до цели
            int[] droneToTargetPositionsX;
            int[] droneToTargetPositionsY;

            List<(int[], int[])> list = new List<(int[], int[])>();
            List<(int[], int[])> path = new List<(int[], int[])>();


            int maxSpeed = 16; // Максимальная скорость дрона

            int k = 1;
            //находим лидера в рое и делаем его первым в массиве
            //остальных добавляем следом
            for (int i = 0; i < swarm.drones.Count(); i++)
            {
                if (swarm.drones[i].IsLeader)
                {
                    dronePositionsX[0] = swarm.drones[i].positionX;
                    dronePositionsY[0] = swarm.drones[i].positionY;
                }
                else
                {
                    dronePositionsX[k] = swarm.drones[i].positionX;
                    dronePositionsY[k] = swarm.drones[i].positionY;
                    k++;
                }
            }

            int centerX = 0;//точка сбора по X
            int centerY = 0;//точка сбора по Y
            for (int i = 0; i < dronePositionsX.Length; i++)
            {
                centerX += dronePositionsX[i];
                centerY += dronePositionsY[i];

            }
            centerX /= dronePositionsX.Length;
            centerY /= dronePositionsX.Length;

            Application.Run(new point(centerX, centerY));

            centerX = GlobalVariables.globalArray[0];
            centerY = GlobalVariables.globalArray[1];

            //пока все дроны не достигнут точки сбора, пересчитываем их координаты
            while (!AllDronesReachedTarget(dronePositionsX, dronePositionsY, centerX, centerY))
            {
                newPositionsX = new int[swarm.drones.Count()];
                newPositionsY = new int[swarm.drones.Count()];
                swarm.updateCoordinaties(dronePositionsX, dronePositionsY, maxSpeed, centerX, centerY, newPositionsX, newPositionsY);
                list.Add((newPositionsX, newPositionsY));
            }
            //формируем координаты точек передвижения всех дронов
            int[] newpointsX = new int[list.Count * dronePositionsX.Length];
            int[] newpointsY = new int[list.Count * dronePositionsX.Length];

            getFromList(list, dronePositionsX, newpointsX, newpointsY);

            //пока все дроны не достигнут достигнут цели, пересчитываем их координаты
            while (!AllDronesReachedTarget(dronePositionsX, dronePositionsY, targetX, targetY))
            {
                droneToTargetPositionsX = new int[dronePositionsX.Length];
                droneToTargetPositionsY = new int[dronePositionsY.Length];
                swarm.updateCoordinaties(dronePositionsX, dronePositionsY, maxSpeed, targetX, targetY, droneToTargetPositionsX, droneToTargetPositionsY);
                path.Add((droneToTargetPositionsX, droneToTargetPositionsY));
            }

            int[] newpoints2X = new int[path.Count * dronePositionsX.Length];
            int[] newpoints2Y = new int[path.Count * dronePositionsX.Length];

            getFromList(path, dronePositionsX, newpoints2X, newpoints2Y);

            Application.Run(new main(targetX, targetY, centerX, centerY, startPositionsX, startPositionsY, newpointsX, newpointsY, newpoints2X, newpoints2Y));
        }

        static bool AllDronesReachedTarget(int[] positionsX, int[] positionsY, int targetX, int targetY)
        {
            for (int i = 0; i < positionsX.Length; i++)
            {
                if (Math.Abs(positionsX[i] - targetX) > 0.01 || Math.Abs(positionsY[i] - targetY)>0.01)
                {
                    return false;
                }
            }
            return true;
        }
        static void getFromList(List<(int[], int[])> list, int[] positions, int[] posX, int[] posY)
        {
            int n = 0;
            for (int j = 0; j < list.Count; j++)
            {
                for (int i = 0; i < positions.Length; i++)
                {
                    posX[n] = list[j].Item1[i];
                    posY[n] = list[j].Item2[i];
                    n++;
                }
            }
        }
    }
}
    
