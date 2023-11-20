using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.MapProviders;
using System.Threading;
using System;
using System.Windows.Forms;
using GMap.NET.WindowsForms.Markers;
using System.Collections.Generic;

namespace gpsMap
{
    public partial class gpsMap : Form
    {
        public gpsMap(List<(double, double)> coordinaties, double startX, double startY, double targetX, double targetY)
        {
            InitializeComponent();

            // Создаем новую карту
            GMapControl gmap = new GMapControl();

            // Устанавливаем провайдер карты 
            gmap.MapProvider = GMapProviders.GoogleMap;
            GMaps.Instance.Mode = AccessMode.ServerOnly;

            // Устанавливаем начальные координаты и масштаб
            gmap.Position = new PointLatLng(startX, startY); 
            gmap.MinZoom = 1;
            gmap.MaxZoom = 18;
            gmap.Zoom = 10;

            // Добавляем точку цели на карту
            GMapOverlay markersOverlay = new GMapOverlay("markers");
            PointLatLng target = new PointLatLng(targetX, targetY);
            PointLatLng start = new PointLatLng(startX, startY);

            GMapMarker markerTarget = new GMarkerGoogle(target, GMarkerGoogleType.red);
            GMapMarker markerStart = new GMarkerGoogle(start, GMarkerGoogleType.blue);

            markersOverlay.Markers.Add(markerTarget);
            markersOverlay.Markers.Add(markerStart);
            gmap.Overlays.Add(markersOverlay);


            // Добавляем карту на форму
            this.Controls.Add(gmap);
            gmap.Dock = DockStyle.Fill;
        }
    }
}
