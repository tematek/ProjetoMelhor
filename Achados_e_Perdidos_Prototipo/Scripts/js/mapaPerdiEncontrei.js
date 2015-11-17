/// <reference path="../Scripts/jquery-2.1.4.intellisense.js" />
/// <reference path="../Scripts/jquery-2.1.4.js" />
/// <reference path="../Scripts/jquery-2.1.4.min.js" />http://localhost:5136/Scripts/jquery-2.1.4.min.map
var map;
var idInfoBoxAberto;
var infoBox = [];
var markers = [];

function initialize() {
    var latlng = new google.maps.LatLng(-18.8800397, -47.05878999999999);

    var options = {
        zoom: 5,
        center: latlng,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    map = new google.maps.Map(document.getElementById("mapa"), options);
}

initialize();

function abrirInfoBox(id, marker) {
    if (typeof (idInfoBoxAberto) == 'number' && typeof (infoBox[idInfoBoxAberto]) == 'object') {
        infoBox[idInfoBoxAberto].close();
    }

    infoBox[id].open(map, marker);
    idInfoBoxAberto = id;
}

function carregarPontos() {


    //$.getJSON("/Cadastro/GetItemMapa?tipoItems=" + "Todos", function (data) {
    //    console.log(data);

    //    var Arraydata = [];
    //    //você pode fazer -se um lado do servidor lista JSON, ou chamá-lo de um controlador usando JsonResult

    //    for (var i = 0; i < data.length; i++) {
    //        console.log("estou dentro");

    //        Arraydata.push({ "Id": data[i].idItem, "Categoria": data[i].Categoria, "SubCategoria": data[i].SubCategoria, "Contato": data[i].email, "GeoLong": data[i].Longitude, "GeoLat": data[i].Latitude, "Nome": data[i].Nome, "Sobrenome": data[i].Sobrenome, "Tel": data[i].Tel, "Celular": data[i].Celular });

    //    }

    //    // Usando o JQuery " cada " seletor para percorrer a lista de JSON e soltar os pinos do marcador
    //    $.each(Arraydata, function (i, item) {
    //        var marker = new google.maps.Marker({
    //            'position': new google.maps.LatLng(item.GeoLong, item.GeoLat),
    //            'map': map,
    //            'title': item.Categoria
    //        });

    //        // Faça o marcador azul -pin !
    //        marker.setIcon('http://maps.google.com/mapfiles/ms/icons/blue-dot.png')

    //        // colocar em algum informações sobre cada objeto JSON - neste caso , o horário de funcionamento .
    //        var infowindow = new google.maps.InfoWindow({
    //            content: "<div class='infoDiv'><h2>" + item.Categoria + "</h2>" + "<div><h4>SubCategora: " + item.SubCategoria + "</h2>" + "<div><h4>Contato: " + item.Contato + "<div><h4>Nome: " + item.Nome + "<div><h4>Sobrenome: " + item.Sobrenome + "<div><h4>Telefone: " + item.Tel + "<div><h4>Celular: " + item.Celular + " </h4></div></div>"
    //        });

    //        // finalmente ligar um " OnClick " ouvinte para o mapa para que ele aparece fora info- janela quando o marcador de pino é clicado !
    //        google.maps.event.addListener(marker, 'click', function () {
    //            infowindow.open(map, marker);
    //        });

    //    })


    //});

    ////$.getJSON('js/pontos.json', function (pontos) {

    ////    var latlngbounds = new google.maps.LatLngBounds();

    ////    $.each(pontos, function (index, ponto) {

    ////        var marker = new google.maps.Marker({
    ////            position: new google.maps.LatLng(ponto.Latitude, ponto.Longitude),
    ////            title: "Meu ponto personalizado! :-D",
    ////            icon: 'img/marcador.png'
    ////        });

    ////        var myOptions = {
    ////            content: "<p>" + ponto.Descricao + "</p>",
    ////            pixelOffset: new google.maps.Size(-150, 0)
    ////        };

    ////        infoBox[ponto.Id] = new InfoBox(myOptions);
    ////        infoBox[ponto.Id].marker = marker;

    ////        infoBox[ponto.Id].listener = google.maps.event.addListener(marker, 'click', function (e) {
    ////            abrirInfoBox(ponto.Id, marker);
    ////        });

    ////        markers.push(marker);

    ////        latlngbounds.extend(marker.position);

    ////    });

    ////    var markerCluster = new MarkerClusterer(map, markers);

    ////    map.fitBounds(latlngbounds);

    ////});

}

carregarPontos();