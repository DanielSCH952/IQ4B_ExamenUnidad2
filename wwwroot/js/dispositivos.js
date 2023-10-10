//mapas
//Objetos a utilizar
const body = document.getElementById("generalBody");
//Estancia para iconos personalizados
let LeafIcon = L.Icon.extend({
    options: {
        iconSize: [50, 50],
        iconAnchor: [10, 10],
        popupAnchor: [10, 1]
    }
});
//Iconos Personalizados
let addIcon = new LeafIcon({ iconUrl: '../lib/leaflet/images/marker-icon-add.png' });
let enableIcon = new LeafIcon({ iconUrl: '../lib/leaflet/images/marker-icon-enable.png' });
let disableIcon = new LeafIcon({ iconUrl: '../lib/leaflet/images/marker-icon-disable.png' });
let activos = [];
let inactivos = [];
//layer tipo mapa
let osm = L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 20,
    attribution: '© OpenStreetMap'
});
let map = L.map('map', {
    doubleClickZoom: false,
    layers: [osm]
});
function c() {
    console.log("hola");
}
//Marcadores default
function cargaInicial() {
    fetch("http://localhost:5199/Dispositivos/ObtenerDispositivos")
        .then(respuesta => respuesta.json()).then(json => {
            json.forEach((item) => {

                if (item.estatus) {
                    let marker = L.marker([item.latitud, item.longitud], { icon: enableIcon }).bindPopup(`<b>${item.descripcion} </b><br>Estado: Abierto<br><a href='#'></a>`);
                    marker.on('click', c)
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
function addMarker(e) {
    L.marker([e.latlng.lat, e.latlng.lng], { icon: addIcon }).addTo(map);
    window.location.href = `http://localhost:5199/Dispositivos/Registrar?lat=${e.latlng.lat}&lng=${e.latlng.lng}`;
}

body.onload = cargaInicial();
map.on('dblclick', addMarker);