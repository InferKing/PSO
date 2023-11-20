using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gpsMap
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Создаем объект GPS с начальными координатами
            double startX = 55.7558;
            double startY = 37.6176;

            GPS gps = new GPS(startX,startY); // Координаты Москвы

            double targetX = 59.9343;
            double targetY = 30.3351;
            double speed = 100;
            List<(double, double)> coordinaties = new List<(double, double)>();

            // Обновляем координаты GPS в зависимости от координат цели и скорости движения
            gps.UpdatePosition(coordinaties, targetX, targetY, speed); // Координаты Санкт-Петербурга, скорость 100 км/ч

            // Выводим новые координаты GPS
            Application.Run(new gpsMap(coordinaties, startX, startY, targetX, targetY));
        }
    }
}
