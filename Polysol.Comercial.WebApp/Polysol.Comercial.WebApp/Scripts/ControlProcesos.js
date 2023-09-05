function MostrarMensaje(pTipo, pMensaje) {

    var pIcono;
    var pTitulo;

    if (pTipo == 'INFO') {
        pTipo = 'info';
        pTitulo = 'INFORMACIÓN';
        pIcono = 'info-circle';
    }
    
    if (pTipo == 'WARNING') {
        pTipo = 'warning';
        pTitulo = 'ADVERTENCIA';
        pIcono = 'exclamation-triangle'
    }

    if (pTipo == 'DANGER') {
        pTipo = 'danger';
        pTitulo = 'ERROR';
        pIcono = 'window-close'
    }

    if (pTipo == 'SUCCESS') {
        pTipo = 'success';
        pTitulo = 'ÉXITO';
        pIcono = 'check-circle-o'
    }

    var notify = $.notify({
        icon: 'glyphicon glyphicon-warning-sign',
        title: '<b>' + pTitulo + ' : </b>',
        message: pMensaje
    },
    {
        delay: 2000,
        z_index: 20000,
        type: pTipo,
        element: 'body',
        placement: {
            from: "top",
            align: "center"
        },
        allow_dismiss: true,
        timer: 5000,
        animate: {
            enter: 'animated bounceIn',
            exit: 'animated bounceOut'
        },
        template: '<div data-notify="container" class="col-xs-11 col-sm-4 alert alert-{0}" role="alert">' +
                    '<button type="button" aria-hidden="true" class="close" data-notify="dismiss"><i class="fa fa-times" aria-hidden="true"></i></button>' +
                    '<i class="fa fa-' + pIcono + '" aria-hidden="true"></i> <span data-notify="title">{1}</span>' +
                    '<span data-notify="message">{2}</span>' +
                    '<div class="progress" data-notify="progressbar">' +
                        '<div class="progress-bar progress-bar-{0}" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div>' +
                    '</div>' +
                    '<a href="{3}" target="{4}" data-notify="url"></a>' +
                    '</div>' +
                    '</div>'
    });
}
