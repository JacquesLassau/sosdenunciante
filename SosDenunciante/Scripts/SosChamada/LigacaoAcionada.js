function ClickSocorro(telDiscado) {

    event.preventDefault();
    var item = telDiscado;

    $.post("/Denunciante/LigacaoAcionada?telDiscado=" + item, function (response) {
        if (response)
            console.log("Registro de socorro enviado com sucesso.");
        else
            console.log("Houve um problema interno no envio de notificação para a central de ajuda.");
    });

    window.location.href = "tel:" + telDiscado;
}