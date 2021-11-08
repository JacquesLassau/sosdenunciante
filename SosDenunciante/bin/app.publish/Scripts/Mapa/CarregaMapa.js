window.onload = function () {
    LoadMap();
}

function LoadMap() {
    var mapOptions = {
        center: new google.maps.LatLng(-8.043686, -34.884303),
        zoom: 10,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    };

    var map = new google.maps.Map(document.getElementById("googleMap"), mapOptions);
    var iconMaker = "../Images/icone-mapa.png";

    $.get("/Denunciante/CarregaMapa", function (data) {

        for (var i = 0; i < data.LocalApoio.length; i++) {

            var localApoio = data.LocalApoio[i];

            var coordenadas = new google.maps.LatLng(localApoio.Latitude, localApoio.Longitude);
            var marker = new google.maps.Marker({
                position: coordenadas,
                map: map,
                icon: iconMaker
            });

            (function (marker, localApoio) {
                google.maps.event.addListener(marker, "click", function (e) {

                    if (localApoio.NumeroEndereco == 'undefined' || localApoio.NumeroEndereco == null)
                        localApoio.NumeroEndereco = " s/n";

                    $("#titulo-endereco").text("Endereco:");
                    $("#descricao-endereco").text(localApoio.Endereco + ", " + localApoio.NumeroEndereco);

                    $("#titulo-bairro").text("Bairro:");
                    $("#descricao-bairro").text(localApoio.Bairro);

                    $("#titulo-cidade").text("Cidade:");
                    $("#descricao-cidade").text(localApoio.Cidade);

                    $("#titulo-estado").text("Estado:");
                    $("#descricao-estado").text(localApoio.Estado);

                    if (localApoio.Telefone == 'undefined' || localApoio.Telefone == null || localApoio.Telefone == '')
                        localApoio.Telefone = " s/n";

                    $("#titulo-telefone").text("Telefone:");
                    $("#descricao-telefone").text(localApoio.Telefone);

                    $("#abrir-google").attr("href", "http://maps.google.com/maps?q=" + localApoio.Latitude + "," + localApoio.Longitude + "");
                    $("#abrir-google").attr("target", "_blank");

                    $("#modalLocalApoio").modal('show');
                });
            })(marker, localApoio);
        }
    });
}