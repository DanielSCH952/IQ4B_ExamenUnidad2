//  Chart Pie
const canva = document.getElementById("chart");
const contextChart = canva.getContext("2d");
const ctxGrafica = document.getElementById("grafica").getContext("2d");
const body = document.getElementById("generalBody");
dataChart = {
    datasets: [{
        data: [10, 20, 30]
    }],
    labels: [
        'Red',
        'Yellow',
        'Blue'
    ]
};
const configChart = {
    type: 'pie',
    data: dataChart,
};
const pieChart = new Chart(contextChart, configChart);
//ChartBar
const labelsGrafica = ["Total"];
const dataGrafica = {
    labels: labelsGrafica,
    datasets: [{
        axis: 'y',
        label: 'Dispositivo 1',
        data: [65],
        fill: false,
        backgroundColor: [
            'rgba(255, 99, 132, 0.2)'
        ],
        borderColor: [
            'rgb(255, 99, 132)',
        ],
        borderWidth: 1
    },
    {
        axis: 'y',
        label: 'Dispositivo 2',
        data: [78],
        fill: false,
        backgroundColor: [
            'rgba(255, 159, 64, 0.2)',
        ],
        borderColor: [
            'rgb(255, 159, 64)'
        ],
        borderWidth: 1
    },
    {
        axis: 'y',
        label: 'Dispositivo 3',
        data: [39],
        fill: false,
        backgroundColor: [
            'rgba(75, 192, 192, 0.2)'
        ],
        borderColor: [
            'rgb(75, 192, 192)',
        ],
        borderWidth: 1
    }
    ]
};
const configGrafica = {
    type: 'bar',
    data: dataGrafica,
    options: {
        indexAxis: 'y',
    }
};
const graficaBarras = new Chart(ctxGrafica, configGrafica);


//mapas  
let activos = [];
let inactivos = [];
//Estancia para iconos personalizados
let LeafIcon = L.Icon.extend({
    options: {
        iconSize: [50, 50],
        iconAnchor: [10, 10],
        popupAnchor: [10, 1]
    }
});
//layer tipo mapa
let osm = L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 20,
    attribution: '© OpenStreetMap'
});
//mapa1
let map = L.map('map', {
    doubleClickZoom: false,
    layers: [osm]
}).setView([19.793962217898983, -97.3239755630], 15);
//Iconos Personalizados
let addIcon = new LeafIcon({ iconUrl: '../lib/leaflet/images/marker-icon-add.png' });
let enableIcon = new LeafIcon({ iconUrl: '../lib/leaflet/images/marker-icon-enable.png' });
let disableIcon = new LeafIcon({ iconUrl: '../lib/leaflet/images/marker-icon-disable.png' });

//Marcadores
function inyeccion() {
    fetch("http://localhost:5199/Dispositivos/ObtenerDispositivos")
        .then(respuesta => respuesta.json()).then(json => {
            json.forEach((item) => {

                if (item.estatus) {
                    let marker = L.marker([item.latitud, item.longitud], { icon: enableIcon }).bindPopup(`<b>${item.descripcion} </b><br>Estado: Abierto<br><a href='#'></a>`);
                    activos.push(marker);
                } else {
                    let marker = L.marker([item.latitud, item.longitud], { icon: disableIcon }).bindPopup(`<b>${item.descripcion} </b><br>Estado: Cerrado`);
                    inactivos.push(marker);
                }

            });

            //grupo de capas de markers
            let onDevices = L.layerGroup(activos);
            let offDevices = L.layerGroup(inactivos);
            map.addLayer(offDevices);
            map.addLayer(onDevices);
            let baseMaps = {
                "OpenStreetMap": osm
            };
            let overlayMaps = {
                "Cerradas": offDevices,
                "Abiertas": onDevices
            };
            map.setView([19.793962217898983, -97.3239755630], 15);
            osm.addTo(map);
            //Creacion de layerControl
            L.control.layers(baseMaps, overlayMaps).addTo(map);
        });
}
body.onload = inyeccion();
//map.on('dblclick', addMarker);