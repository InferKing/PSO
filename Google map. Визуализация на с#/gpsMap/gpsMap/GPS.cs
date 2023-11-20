using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gpsMap
{
    class GPS
    {
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }

        public GPS(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public void UpdatePosition(List<(double, double)> list, double targetLatitude, double targetLongitude, double speed)
        {
            double earthRadius = 6371; // Радиус Земли в километрах
            double lat2 = targetLatitude * Math.PI / 180; // Преобразуем широту цели из градусов в радианы
            double lon2 = targetLongitude * Math.PI / 180; // Преобразуем долготу цели из градусов в радианы

            while (Math.Abs(Latitude - targetLatitude) > 0.000001 && Math.Abs(Longitude - targetLongitude) > 0.000001)
            {
                // Рассчитываем новые координаты GPS в зависимости от координат цели и скорости движения

                double lat1 = Latitude * Math.PI / 180; // Преобразуем широту из градусов в радианы
                double lon1 = Longitude * Math.PI / 180; // Преобразуем долготу из градусов в радианы


                double dLat = lat2 - lat1;
                double dLon = lon2 - lon1;

                double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                           Math.Cos(lat1) * Math.Cos(lat2) *
                           Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
                double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
                double distance = earthRadius * c;

                // Обновляем координаты GPS
                Latitude = (targetLatitude - Latitude) / 2 + Latitude;
                Longitude = (targetLongitude - Longitude) / 2 + Longitude;

                list.Add((Latitude, Longitude));
            }

        }
    }
}
