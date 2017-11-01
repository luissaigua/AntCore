
var retorno = "0";
$(document).ready(function () {
  
    
    var selectsv = $('#ddl_subtipo_vhl');
    selectsv.find('option').remove().end().append('<option value="-1">SELECCIONAR</option>').val('-1');

    $('#myTab li:eq("1") a').attr("disabled", "disabled");
    /****************************************************
    * CARGA TABLA DE GEOREFERENCIAS
    ****************************************************/
    $('#lbCodigoGeo').css("display", "none");
    $('#lbLatitudGeo').css("display", "none");
    $('#lbLonguitudGeo').css("display", "none");
    $('#dt_georeferencias tr').on('click', function () {
        var codGeo = $(this).find('td:nth-child(2)').html();
        var LonguitudGeo = $(this).find('td:nth-child(3)').html();
        var LatitudGeo= $(this).find('td:nth-child(4)').html();
        $('#lbCodigoGeo').html(codGeo.toString());
        $('#lbLatitudGeo').html(LatitudGeo.toString());
        $('#lbLonguitudGeo').html(LonguitudGeo.toString());
        $('#tbLatitudSiniestro').val(LatitudGeo.toString());
        $('#tbLonguitudSiniestro').val(LonguitudGeo.toString());
        console.log(codGeo);
        console.log(LatitudGeo);
        console.log(LonguitudGeo);
        
    });
   
    $('#spanSiniestro').attr("disabled", "disabled");
    $('#btnVerMapaSin').click(function () {
        // alert('entra mapa');
        VerMapa();
        var x = $('#tbLonguitudSiniestro').val();
        var y = $('#tbLatitudSiniestro').val();
        
        $('#lat').val(y);
        $('#lng').val(x);
    });
    //$('#btnGuardaSiniestro').attr("disabled", "disabled");
    /***************************************************
   * FUNCIONES PARA CONTROLES POR DEFECTO
   ****************************************************/
    
    // $('#lbCodigoSiniestroGuardado').html('siniestro g');
    

    var select = $('#ddl_ciudad');
    select.find('option').remove().end().append('<option value="-1">SELECCIONAR</option>').val('-1');
    var selectparroquia = $('#ddl_parroquia');
    //    selectparroquia.find('option').remove().end().append('<option value="-1">SELECCIONAR</option>').val('-1');
    //selectparroquia.find('option').append('<option value="-1">SELECCIONAR</option>').val('-1');
    selectparroquia.val('-1');
    var selectsubcircuito = $('#ddl_circuito');
    selectsubcircuito.val('-1');
    //selectsubcircuito.find('option').remove().end().append('<option value="-1">SELECCIONAR</option>').val('-1');
    
    var selectdis = $('#ddl_distritoSiniestro');
    selectdis.val('-1');
    var tiempo = new Date();
    var tiempo2 = new Date().getDate();
    var hora = tiempo.getHours();
    var minuto = tiempo.getMinutes();
    var horaSiniestro = hora.toString() + ':' + minuto.toString();
    var anio = tiempo.getUTCDate();
    var mes = tiempo.getMonth();
    var dia = tiempo.getDay();
    var fechaSiniestro = anio.toString() + '-' + mes.toString() + '-' + dia.toString();
    console.log('hora' +' ' +horaSiniestro.toString());
    // ($('#tbHoraSiniestro').val(horaSiniestro.toString())); // CONTROL DE HORA FORMATO HH:MM
    //($('#lbCodigoSiniestroGuardadoOculto').html('16'));
    $("#tbfechaSiniestro").datepicker({ dateFormat: "yy-mm-dd", maxDate: '+0d' }).mask("9999-99-99"); // CONTROL DE FECHA  (AÑO-MES-DIA)
    // Traducción al español
    $(function ($) {
        $.datepicker.regional['es'] = {
            closeText: 'Cerrar',
            prevText: '<Ant',
            nextText: 'Sig>',
            currentText: 'Hoy',
            monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
            monthNamesShort: ['Ene', 'Feb', 'Mar', 'Abr', 'May', 'Jun', 'Jul', 'Ago', 'Sep', 'Oct', 'Nov', 'Dic'],
            dayNames: ['Domingo', 'Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado'],
            dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mié', 'Juv', 'Vie', 'Sáb'],
            dayNamesMin: ['Do', 'Lu', 'Ma', 'Mi', 'Ju', 'Vi', 'Sá'],
            weekHeader: 'Sm',
            dateFormat: 'yy-mm-dd',
            
            isRTL: false,
            showMonthAfterYear: false,
            yearSuffix: ''
        };
        $.datepicker.setDefaults($.datepicker.regional['es']);

    });
    $("#tbfechaSiniestro").datepicker({
        showButtonPanel: true,
        maxDate: '+0d'
    });
   
    jQuery(function ($) {
        $.mask.definitions['H'] = '[0123]';
        $.mask.definitions['N'] = '[012345]';
        $.mask.definitions['n'] = '[0123456789]';
        $("#tbHoraSiniestro").mask("Hn:Nn");
    });

   
    ($('#panelMensajeSiniestro').css("display", "none"));
    //($('#divBotones').css("display", "none"));
    ($('#divBotonesSecundarios').css("display", "none"));
    ($('#divMensajeCodigoSiniestro').css("display", "none"));
    //$('#tbfechaSiniestro').attr("disabled", "disabled");
    ($('#tbnumlessin').attr("disabled", "disabled"));
    ($('#tbnumfalsin').attr("disabled", "disabled"));

    var horaIN = $("#tbHoraSiniestro").val();
    var horas = horaIN.split(":")[0];
    console.log('horas in' + ' ' + horas.toString());
    if (parseInt(horas) === 18 || parseInt(horas) === 19 || parseInt(horas) === 20 || parseInt(horas) === 21 || parseInt(horas) === 22 || parseInt(horas) === 23 || parseInt(horas) === 24 || horas.toString() === '01' || (horas.toString()) === '02' || (horas.toString()) === '03' || (horas.toString()) === '04' || (horas.toString()) === '05' || (horas.toString()) === '06' || (horas.toString()) === '00') {
        ($('#ddl_luzArtificial').removeAttr("disabled"));
        console.log(horas.toString());
    }
    //$("#tbHoraSiniestro").keypress(function () {
    //    valida_hora()
    //});
   

    /**************************************
    * GUARDA SINIESTRO
    ***************************************/
    $('#btnGuardaSiniestro').click(function () {
        
        ($('#panelMensajeSiniestro').css("display", "none"));
        var mensaje = '';
        var fechaSin = ($('#tbfechaSiniestro').val());
        var horaSin = ($('#tbHoraSiniestro').val());
       
        var provinciaSin = ($('#ddl_provincia').val());
        var cantonSin = ($('#ddl_ciudad').val());
        var parroquiaSin = ($('#ddl_parroquia').val());
        var distritoSin = ($('#ddl_distritoSiniestro').val());
        var circuitoSin = ($('#ddl_circuito').val());
        var subCircuitoSin = ($('#ddl_SubCircuito option:selected').text());
        var ZonaSin = ($('#ddl_tipo_zona option:selected').text());
        var direccionSin = ($('#tbdireccionSiniestro').val());
        var numFallecidosSin = ($('#tbnumfalsin').val());
        var numLesionadosSin = ($('#tbnumlessin').val());
        var condicionAtmosfericaSin = ($('#ddl_condAtmosferica option:selected').text());
        var condicionViaSin = ($('#ddl_condVia option:selected').text());
        var luzArteficialSin = ($('#ddl_luzArtificial option:selected').text());
        var trabajosViaSin = $("#ddl_trabajosVia option:selected").text();
        var tipoViaSin = ($('#ddl_tipoVia option:selected').text());
        var limVelocidadSin = ($('#ddl_limVelocidad option:selected').text());
        var controlInterseccionSin = ($('#ddl_controlInterseccion option:selected').text());
        var materialSuperficielSin = ($('#ddl_materialSuperficie option:selected').text());
        var obstaculosViaSin = ($('#ddl_obstVia option:selected').text());
        var lugarViaSin = ($('#ddl_lugarVia option:selected').text());
        var numCarrilesSin = ($('#ddl_numCarriles option:selected').text());
        var curvaExistenteSin = ($('#ddl_curvaExistente option:selected').text());
        var senializacionExistenteSin = ($('#ddl_senializacionExistente option:selected').text());
        var tipoSin = ($('#ddl_tipoSiniestro').val());
        var causaProbableSin = ($('#ddl_causaProbableSiniestro').val());
        var causaRealSin = ($('#ddl_causaRealSiniestro').val());
        var isVisibleLuzArtificial = $("#ddl_luzArtificial").is('enabled');
        var isVisibleControlInterseccion = $("#ddl_controlInterseccion").is('enabled');
        if ($("#ddl_luzArtificial").prop('disabled') == false) {
            isVisibleLuzArtificial = true;
        }
        else { isVisibleLuzArtificial = false; }
        if ($("#ddl_controlInterseccion").prop('disabled') == false) {
            isVisibleControlInterseccion = true;
        }
        else { isVisibleControlInterseccion = false; }

        var codGeo = ($('#lbCodigoGeo').html());
        var latsinGeo = ($('#lbLatitudGeo').html());
        var lonsinGeo = ($('#lbLonguitudGeo').html());
        var latitudSin = ($('#tbLatitudSiniestro').val());
        var longuitudSin = ($('#tbLonguitudSiniestro').val());
        //
        //console.log(latitudSin.toString());
        //console.log(longuitudSin.toString());
        //console.log(codGeo.toString());
        console.log('isVisibleLuzArtificial' + ' ' + isVisibleLuzArtificial);
        console.log('controlInterseccionSin' + ' ' + controlInterseccionSin);
        console.log('isVisibleControlInterseccion' + ' ' + isVisibleControlInterseccion);
        
        //console.log('parroquiaSin' + ' ' + parroquiaSin);
        fechaSin = ($('#tbfechaSiniestro').val());
        if (parroquiaSin === 'undefined') {
            alert('Seleccione la parroquia del siniestro.')
            }
       
        else if (fechaSin.trim() === "" || fechaSin.length < 10)
        {
            mensaje = 'Ingrese la fecha del siniestro.';
        }
        else if (horaSin.trim() === "" || horaSin.length < 5) {
            mensaje = 'Ingrese la hora del siniestro.';
        }//
            //
        else if (longuitudSin.trim() === "" ) {
            mensaje = 'Ingrese la longitud(x) del siniestro.';
        }//
        else if (latitudSin.trim() === "") {
            mensaje = 'Ingrese la latitud(y) del  siniestro.';
        }
        else if (provinciaSin.trim() === "" || provinciaSin.trim() === '-1') {
            mensaje = 'Seleccione la provincia del siniestro.';
        }
        else if (cantonSin.trim() === "" || cantonSin.trim() === '-1') {
            mensaje = 'Seleccione el cantón del siniestro.';
        }
        else if (parroquiaSin.trim() === "" || parroquiaSin.trim() === '-1' || typeof parroquiaSin === 'null') {
            mensaje = 'Seleccione la parroquia del siniestro.';
        }
        //else if (distritoSin.trim() === "" || distritoSin.trim() === '-1') {
        //    mensaje = 'Seleccione el distrito del siniestro.';
        //}
        //else if (circuitoSin.trim() === "" || circuitoSin.trim() === '-1') {
        //    mensaje = 'Seleccione el circuito del siniestro.';
        //}
        //else if (ZonaSin.trim() === "" || ZonaSin.trim() === 'SELECCIONAR') {
        //    mensaje = 'Seleccione la zona del siniestro.';
        //}
        else if (direccionSin.trim() === "" || direccionSin.trim() === '') {
            mensaje = 'Ingrese la dirección del siniestro.';
        }
        
        else if (numFallecidosSin === '')
        {
            mensaje = 'Ingrese el número de fallecidos.';
        }
        else if (numLesionadosSin === '') {
            mensaje = 'Ingrese el número de lesionados.';
        }//
        else if (condicionAtmosfericaSin.trim() === "" || condicionAtmosfericaSin.trim() === 'SELECCIONAR') {
            mensaje = 'Seleccione la Condición Atmosférica del siniestro.';
        }
        else if (condicionViaSin.trim() === "" || condicionViaSin.trim() === 'SELECCIONAR') {
            mensaje = 'Seleccione la Condición de la Vía del siniestro.';
        }
        
        else if (tipoViaSin.trim() === "" || tipoViaSin.trim() === 'SELECCIONAR') {
            mensaje = 'Seleccione el Tipo de la vía  del siniestro.';
        }
        else if (numCarrilesSin.trim() === "" || numCarrilesSin.trim() === 'SELECCIONAR') {
            mensaje = 'Seleccione el numero de carrilles.';
        }
        else if (materialSuperficielSin.trim() === "" || materialSuperficielSin.trim() === 'SELECCIONAR') {
            mensaje = 'Seleccione el Material de Superficie del siniestro.';
        }
        else if (limVelocidadSin.trim() === "" || limVelocidadSin.trim() === 'SELECCIONAR') {
            mensaje = 'Seleccione el Lim.Velocidad del siniestro.';
        }
        else if (trabajosViaSin == "SELECCIONAR")
        {
            mensaje = 'Seleccione si existe trabajos en la vía.';
        }
        else if (lugarViaSin.trim() === "" || lugarViaSin.trim() === 'SELECCIONAR') {
            mensaje = 'Seleccione el Lugar de la vía del siniestro.';
        }
        else if ((controlInterseccionSin.trim() === "" || controlInterseccionSin.trim() === 'SELECCIONAR') && isVisibleControlInterseccion === true) {
            mensaje = 'Seleccione el Control de intersección  del siniestro.';
        }
        else if (senializacionExistenteSin.trim() === "" || senializacionExistenteSin.trim() === 'SELECCIONAR') {
            mensaje = 'Seleccione Señalización Existente.';
        }
        else if (curvaExistenteSin.trim() === "" || curvaExistenteSin.trim() === 'SELECCIONAR') {
            mensaje = 'Seleccione Curva Existente.';
        }
        else if (obstaculosViaSin.trim() === "" || obstaculosViaSin.trim() === 'SELECCIONAR') {
            mensaje = 'Seleccione los Obstáculos de la Vía  del siniestro.';
        }
        else if (causaProbableSin.trim() === "" || causaProbableSin.trim() === '-1') {
            mensaje = 'Seleccione la Causa Probable Siniestro del Siniestro.';
        }
        else if (tipoSin.trim() === "" || tipoSin.trim() === '-1') {
            mensaje = 'Seleccione el Tipo de Siniestro.';
        }
        else if ((luzArteficialSin.trim() === "" || luzArteficialSin.trim() === 'SELECCIONAR') && isVisibleLuzArtificial === true) {
            mensaje = 'Seleccione la Luz Artificial  del siniestro.';
        }
        else {
            var fecha_actual = Date.toLocaleString();
            var d = new Date();
            var dd = d.getDate() - 40;
            var mm = d.getMonth() + 1; //hoy es 0!
            var yyyy = d.getFullYear();
            //console.log('fecha_actual ' + '' + dd + '-' + mm + '-' + yyyy);
            fecha_actual = new Date().toJSON().slice(0, 10)
            console.log('fecha_actual ' + '' + fecha_actual);
            console.log('fechaSin ' + '' + fechaSin);
            var anio_max = fechaSin.split('-')[0];
            var mes_max = parseInt(fechaSin.split('-')[1]) + 1;
            var dia_max = '10';
            var dia_min = '01';
            var fecha_max = anio_max + '-' + mes_max + '-' + dia_max;
            var fecha_min = anio_max + '-' + fechaSin.split('-')[1] + '-' + dia_min;
            //console.log('fecha_max ' + '' + fecha_max);
            //console.log('fecha_min ' + '' + fecha_min);
            fecha_max = fecha_max.split('-');
            fecha_actual = fecha_actual.split('-');
            fechaSin = fechaSin.split('-');
            fecha_min = fecha_min.split('-');
            fecha_max = new Date(fecha_max[0], fecha_max[1] - 1, fecha_max[2]).valueOf();
            fecha_actual = new Date(fecha_actual[0], fecha_actual[1] - 1, fecha_actual[2]).valueOf();
            fechaSin = new Date(fechaSin[0], fechaSin[1] - 1, fechaSin[2]).valueOf();
            fecha_min = new Date(fecha_min[0], fecha_min[1] - 1, fecha_min[2]).valueOf();
          
            var _fecha_actual_sistema = new Date().toJSON().slice(0, 10);
            _fecha_actual_sistema = new Date(_fecha_actual_sistema).valueOf();
            if (fecha_max >= _fecha_actual_sistema && fechaSin <= fecha_max && fechaSin >= fecha_min ) {
                console.log('confirmarDireccionesMapaConfirmar()' + ' ' + confirmarDireccionesMapaConfirmar() + 'provinciaSin' + provinciaSin);
                if (confirmarDireccionesMapaConfirmar() !== provinciaSin)
                {
                    fecsin = ($('#tbfechaSiniestro').val());
                    console.log(fecsin);
                    $.ajax({
                        type: "get",
                        url: 'JsonGuardaSiniestro',
                        data: { fecsin: fecsin, horsin: horaSin, latsin: latitudSin, lonsin: longuitudSin, dirsin: direccionSin, numfalsin: numFallecidosSin, numlessin: numLesionadosSin, zonsin: ZonaSin, traviasin: trabajosViaSin, conatmsin: condicionAtmosfericaSin, conviasin: condicionViaSin, luzartsin: luzArteficialSin, desviasin: tipoViaSin, limvelsin: limVelocidadSin, intsin: controlInterseccionSin, matsupviasin: materialSuperficielSin, obsviasin: obstaculosViaSin, lugviasin: lugarViaSin, cursin: curvaExistenteSin, numcarsin: numCarrilesSin, sensin: senializacionExistenteSin, codtipsin: tipoSin, codpar: parroquiaSin, codsubcir: subCircuitoSin, codcant: cantonSin, codprov: provinciaSin, codcaupro: causaProbableSin, codcaurea: causaRealSin, codcir: circuitoSin, coddis: distritoSin, codgeo: codGeo, latsinGeo: latsinGeo, lonsinGeo: lonsinGeo },
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (jsonmensaje) {
                            console.log('retorno' + ' ' + jsonmensaje.toString());
                            if (jsonmensaje.toString() === '0') {
                                mensaje = 'Error al grabar los datos del siniestro.';
                                ($('#mensajeLabelSiniestro').html(mensaje.toString()));
                                ($('#divMensajeSiniestro').attr("class", "alert alert-block alert-danger fade in smaller"));
                            }
                            else {
                                mensaje = 'SINIESTRO INGRESADO CORRECTAMENTE: ' + ' ' + '   # ' + ' ' + jsonmensaje.toString() + '   PROCEDA A INGRESAR LOS VEHÍCULOS INVOLUCRADOS ';

                                $('#myTab li:eq("1") a').tab('show');
                                ($('#mensajeLabelSiniestro').html(mensaje.toString()));
                                ($('#divMensajeVHL').attr("class", "alert alert-block alert-success fade in smaller"));
                                //($('#divMensajeSiniestro').attr("class", "alert alert-block alert-success fade in smaller"));
                                ($('#mensajeLabelVHL').html(mensaje.toString()));
                                ($('#panelMensajeVHL').css("display", "block"));
                                //($('#divMensajeCodigoSiniestro').css("display", "block"));
                                ($('#lbCodigoSiniestroGuardado').html(mensaje.toString()));
                                ($('#lbCodigoSiniestroGuardadoOculto').html(jsonmensaje.toString()));


                                $('#btnGuardaSiniestro').attr("disabled", "disabled");
                                $('#btnGuardaSiniestro').css("display", "none");
                                $('#tbfechaSiniestro').attr("disabled", "disabled");
                                $('#tbHoraSiniestro').attr("disabled", "disabled");
                                $('#tbLonguitudSiniestro').attr("disabled", "disabled");
                                $('#tbLatitudSiniestro').attr("disabled", "disabled");
                                $('#ddl_provincia').attr("disabled", "disabled");
                                $('#ddl_parroquia').attr("disabled", "disabled");

                                $('#ddl_ciudad').attr("disabled", "disabled");
                                $('#ddl_distritoSiniestro').attr("disabled", "disabled");
                                $('#ddl_circuito').attr("disabled", "disabled");
                                $('#ddl_tipo_zona').attr("disabled", "disabled");
                                $('#tbdireccionSiniestro').attr("disabled", "disabled");
                                $('#ddl_condAtmosferica').attr("disabled", "disabled");
                                $('#ddl_condVia').attr("disabled", "disabled");
                                $('#ddl_tipoVia').attr("disabled", "disabled");
                                $('#ddl_numCarriles').attr("disabled", "disabled");
                                $('#ddl_materialSuperficie').attr("disabled", "disabled");
                                $('#ddl_limVelocidad').attr("disabled", "disabled");
                                $('#ddl_trabajosVia').attr("disabled", "disabled");
                                $('#ddl_lugarVia').attr("disabled", "disabled");
                                $('#ddl_controlInterseccion').attr("disabled", "disabled");
                                $('#ddl_senializacionExistente').attr("disabled", "disabled");
                                $('#ddl_curvaExistente').attr("disabled", "disabled");
                                $('#ddl_obstVia').attr("disabled", "disabled");
                                $('#ddl_causaProbableSiniestro').attr("disabled", "disabled");
                                $('#ddl_tipoSiniestro').attr("disabled", "disabled");
                                $('#ddl_luzArtificial').attr("disabled", "disabled");

                                ($('#panelMensajeSiniestro').css("display", "none"));


                                $("#aLiVehiculos").css("display", "block");
                                $("#liingreprog").addClass("completed", "completed");

                                //$("#alivictimas").css("display", "block");
                                //$("#aliDanios").css("display", "block");
                                //$("#alifinProceso").css("display", "block");


                                InicializaVehiculos();
                                inicializaDaniosTerceros();

                            }
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus + ": " + XMLHttpRequest.responseText);
                        }

                    });
                }
                else {
                    mensaje = 'La provincia seleccionada no se encuentra dentro del punto de georeferencia, favor verificar las coordenadas y dirección.';
                }
            }
            else {
                mensaje = 'La fecha del siniestro se ecuentra fuera del rango permitido.';
            }
        }

        ($('#mensajeLabelSiniestro').html(mensaje.toString()));
        ($('#panelMensajeSiniestro').css("display", "block"));
       
    });


    /**************************************
    * GUARDA VICTIMAS INVOLUCRADOS
    ***************************************/
    $('#btnModalVictimas').click(function () {
        var codsinVictimas = ($('#lbCodigoSiniestroGuardadoOculto').html());
        ($('#tb_codsinVictimas').val(codsinVictimas.toString()));
        $('#tb_codsinVictimas').attr("disabled", "disabled");
        // ($('#btnBuscarVict').css("display", "none"));
        //($('#tb_codsinVictimas').val('0'));
        ($('#ddl_tipoIdentificacion').val('-1'));
        ($('#tb_IdentificacionVictima').val(''));
        ($('#tb_edadVictima').val('0'));
        ($('#ddl_generoVictima').val('-1'));
        ($('#ddl_condicionVictimas24').val('-1'));
        ($('#ddl_condicionVictimas30').val('4'));
        ($('#ddl_tipoParticipante').val('-1'));
        //($('#ddl_posicionPlaza').val('-1'));
        ($('#panelMensajeVictimas').css("display", "none"));
        $("#cb_usoCascoVictima").prop("checked", false);
        $("#cb_usoCinturonVictima").prop("checked", false);
        $('#cb_consumoAlcoholVictima').prop("checked", false);
        $('#ddl_accionesPeaton').css("display", "none");
        $('#divAccionesPeaton').css("display", "none");
        $('#divAccionesAgregar').css("display", "none");
        ($('#divPanelelGridAccionesPeaton').css("display", "none"));
        ($('#divVerVictimasSiniestro').css("display", "none"));
        $("#cb_1").prop("checked", false);
        $("#cb_2").prop("checked", false);
        $("#cb_3").prop("checked", false);
        $("#cb_4").prop("checked", false);
        $("#cb_5").prop("checked", false);
        $("#cb_6").prop("checked", false);
        $("#cb_7").prop("checked", false);
        $("#cb_8").prop("checked", false);
        $("#cb_9").prop("checked", false);
        $("#cb_10").prop("checked", false);
        $("#cb_11").prop("checked", false);
        $("#cb_12").prop("checked", false);
        $("#cb_13").prop("checked", false);
        codsinVictimas = ($('#tb_codsinVictimas').val());
        console.log(codsinVictimas.toString());
        var select = $('#ddl_vhlVictimas');
        select.find('option').remove().end().append('<option value="-1">SELECCIONAR</option>').val('-1');
        $.ajax({
            type: "get",
            url: 'traerDatosVehiculosInvolucradosVictimas',
            data: { codsin: codsinVictimas },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (jsonResult) {
                console.log(jsonResult);
                $.each(jsonResult, function (key, value) {
                    select.append('<option value="' + value.value + '">' + value.text + '</option>').val(value.value);
                });
                select.val('-1');
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(textStatus + ": " + XMLHttpRequest.responseText);
            }

        });
    });

    //ObtenerTipoVehiculoVictimas
    $('#ddl_vhlVictimas').change(function () {
        if ($('#ddl_vhlVictimas').val() !== '-1')
        {
            var codVehiculoVictima = ($('#ddl_vhlVictimas').val());
            $.ajax({
                type: "get",
                url: 'ObtenerTipoVehiculoVictimas',
                data: { codVehiculo: codVehiculoVictima},
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (retorno) {
                    console.log('retorno tipo vhl victimas' + ' ' + retorno.toString());
                    if (retorno.toString() === '0') {
                        mensaje = 'Error al verificar el tipo de vehiculo.';
                        ($('#mensajeLabelVictimas').html(mensaje.toString()));
                        ($('#divMensajeVictimas').attr("class", "alert alert-block alert-danger fade in smaller"));
                       
                    }
                    else {
                        
                        if (retorno.toString() === '8' || retorno.toString() === '16') {
                            $("#ddl_uso_casco").val('1');
                            $("#ddl_uso_cinturon").val("2");
                            $("#cb_2S").attr("disabled", "disabled");
                            $("#cb_3S").attr("disabled", "disabled");
                            $("#cb_5S").attr("disabled", "disabled");
                            $("#cb_6S").attr("disabled", "disabled");
                            $("#cb_8S").attr("disabled", "disabled");
                            $("#cb_9S").attr("disabled", "disabled");
                            $("#cb_10S").attr("disabled", "disabled");
                            $("#cb_11S").attr("disabled", "disabled");
                            $("#cb_12S").attr("disabled", "disabled");
                            $("#cb_13S").attr("disabled", "disabled");
                            $("#ddl_uso_casco").removeAttr('disabled');
                            $("#ddl_uso_cinturon").attr("disabled", "disabled");
                        } 
                        else if (retorno.toString() === '16') {
                            $("#ddl_uso_casco").val('1');
                            $("#ddl_uso_cinturon").val("2");
                            $("#cb_2S").attr("disabled", "disabled");
                            $("#cb_3S").attr("disabled", "disabled");
                            $("#cb_5S").attr("disabled", "disabled");
                            $("#cb_6S").attr("disabled", "disabled");
                            $("#cb_8S").attr("disabled", "disabled");
                            $("#cb_9S").attr("disabled", "disabled");
                            $("#cb_10S").attr("disabled", "disabled");
                            $("#cb_11S").attr("disabled", "disabled");
                            $("#cb_12S").attr("disabled", "disabled");
                            $("#cb_13S").attr("disabled", "disabled");
                            $("#ddl_uso_casco").removeAttr('disabled');
                            $("#ddl_uso_cinturon").attr("disabled", "disabled");
                        }
                        else {
                            $("#ddl_uso_cinturon").val('-1')
                            $("#ddl_uso_casco").val('-1');
                            $("#cb_1S").removeAttr('disabled');
                            $("#cb_2S").removeAttr('disabled');
                            $("#cb_3S").removeAttr('disabled');
                            $("#cb_4S").removeAttr('disabled');
                            $("#cb_5S").removeAttr('disabled');
                            $("#cb_6S").removeAttr('disabled');
                            $("#cb_7S").removeAttr('disabled');
                            $("#cb_8S").removeAttr('disabled');
                            $("#cb_9S").removeAttr('disabled');
                            $("#cb_10S").removeAttr('disabled');
                            $("#cb_11S").removeAttr('disabled');
                            $("#cb_12S").removeAttr('disabled');
                            $("#cb_13S").removeAttr('disabled');
                            $("#ddl_uso_casco").val('2');
                            $("#ddl_uso_casco").attr("disabled", "disabled");
                            $("#ddl_uso_cinturon").removeAttr('disabled');

                        }
                        ($('#tbTipoVhlVictima').val(retorno.toString()));

                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus + ": " + XMLHttpRequest.responseText);
                }

            });
        }
    });

    $('#ddl_tipoIdentificacion').change(function () {
        if ($('#ddl_tipoIdentificacion').val() === '1') {
            ($('#tb_IdentificacionVictima').removeAttr('disabled'));
            ($('#tb_IdentificacionVictima').val('0'));
            $('#btnBuscarVict').css('display', 'block');
        }
        
        else if ($('#ddl_tipoIdentificacion').val() === '4') {
            ($('#tb_IdentificacionVictima').val('0'));
            ($('#tb_IdentificacionVictima').attr('disabled', 'disabled'));
            ($('#tb_edadVictima').val('-1'));
            ($('#ddl_generoVictima').val('M'));
            // ($('#btnBuscarVict').css("display", "none"));
            ($('#ddl_tipoParticipante').val('-1'));
            $('#btnBuscarVict').css('display', 'none');
        }
        else if ($('#ddl_tipoIdentificacion').val() === '2') {
            ($('#ddl_tipoParticipante').val('-1'));
            ($('#tb_IdentificacionVictima').removeAttr('disabled'));
            ($('#tb_edadVictima').val('0'));
            ($('#ddl_generoVictima').val('-1'));
            $('#btnBuscarVict').css('display', 'block');
        }
        else if ($('#ddl_tipoIdentificacion').val() === '-1') {
            ($('#ddl_tipoParticipante').val('-1'));
            ($('#tb_IdentificacionVictima').removeAttr('disabled'));
            $('#btnBuscarVict').css('display', 'none');
        }
        else {
            ($('#tb_IdentificacionVictima').val(''));
            ($('#tb_edadVictima').val('0'));
            ($('#ddl_generoVictima').val('-1'));
            ($('#tb_IdentificacionVictima').removeAttr('disabled'));
            ($('#ddl_tipoParticipante').val('-1'));
            $('#btnBuscarVict').css('display', 'none');
    
        }
    });

    $('#tb_IdentificacionVictima').keypress(function (event) {
       
        if ($('#ddl_tipoIdentificacion').val() === '1') {
        
            $("#tb_IdentificacionVictima").attr('maxlength', 10);
            $('#tb_IdentificacionVictima').validCampoFranz('01234567890');
        }
        else if ($('#ddl_tipoIdentificacion').val() === '2') {
      
            $("#tb_IdentificacionVictima").attr('maxlength', 10);
            $('#tb_IdentificacionVictima').validCampoFranz('01234567890');
        }
        else if ($('#ddl_tipoIdentificacion').val() === '3') {
            $("#tb_IdentificacionVictima").attr('maxlength', 20);
            if ((event.charCode < 97 || event.charCode > 122) && (event.charCode < 65 || event.charCode > 90) )
            {
                console.log('entra if');
                return true;
            }
            if (tecla.charCode < 48 || tecla.charCode > 57) {
                console.log('entra if 2');
                return true;
            }
            
           // $('#tb_IdentificacionVictima').validCampoFranz('abcdefghijklmnñopqrstuvwxyz01234567890');
        }
        //else {
        //    ($('#tb_IdentificacionVictima').val(''));
           
        //    $("#tb_IdentificacionVictima").attr('maxlength', 10);
        //    $('#tb_IdentificacionVictima').validCampoFranz('01234567890');
        //}
    });


    $("#btnBuscarVict").click(function () {
        
        if ($('#ddl_tipoIdentificacion').val() === '1') {
        
            ObtenerDatosConsultaPorCedula();
        }
        else if ($('#ddl_tipoIdentificacion').val() === '2') {
        
            ObtenerDatosConsultaPorLicencia();

        }
    });


    $('#btnSiguienteVhl').click(function () {
        if ($('#dt_vehiculosSiniestro >tbody >tr').length == 0) {
            alert ( "Ingresar al menos un vehículo involucrado para continuar..!!" );
        }
        else if ($('#dt_vehiculosSiniestro >tbody >tr').length === 1 && ( $('#ddl_tipoSiniestro').val() === '13' || $('#ddl_tipoSiniestro').val() === '14'  || $('#ddl_tipoSiniestro').val() === '16' || $('#ddl_tipoSiniestro').val() === '15' || $('#ddl_tipoSiniestro').val() === '17') ) {
            alert("Ingresar mas de  un vehículo involucrado para el tipo de siniestro " + $('#ddl_tipoSiniestro option:selected').text() + " para continuar..!!");
        }else
        {
            
            InicializaVictimas();
            $('#myTab li:eq("2") a').tab('show');
            $('#alivictimas').css("display", "block");
            $("#livicprog").addClass("completed", "completed");
           // traerVehiculosInvolucradosSin();
        }
        
    });
  

    $('#btnLimpiarVictimas').click(function () {
        $('#tb_codsinVictimas').attr("disabled", "disabled");
        $('#respuesta-ej3').toggle();
        $('#respuesta-ej4').toggle();
        ($('#ddl_tipoIdentificacion').val('-1'));
        ($('#tb_IdentificacionVictima').val(''));
        ($('#tb_IdentificacionVictima').removeAttr('disabled', 'disabled'));
        ($('#tb_edadVictima').val('0'));
        ($('#ddl_generoVictima').val('-1'));
        ($('#ddl_condicionVictimas24').val('-1'));
        ($('#ddl_condicionVictimas30').val('4'));
        ($('#ddl_tipoParticipante').val('-1'));
        // ($('#ddl_posicionPlaza').val('-1'));
        ($('#panelMensajeVictimas').css("display", "none"));
        ($('#ddl_uso_casco').removeAttr('disabled', 'disabled'));
        ($('#ddl_uso_cinturon').removeAttr('disabled', 'disabled'));
        ($('#ddl_consumo_alcohol').removeAttr('disabled', 'disabled'));
        ($('#ddl_uso_casco').val('-1'));
        ($('#ddl_uso_cinturon').val('-1'));
        ($('#ddl_consumo_alcohol').val('-1'));
        $('#ddl_accionesPeaton').css("display", "none");
        $('#divAccionesPeaton').css("display", "none");
        $('#divAccionesAgregar').css("display", "none");
        ($('#divPanelelGridAccionesPeaton').css("display", "none"));
        ($('#ddl_accionesPeaton').val('-1'));
        $("#cb_1S").prop("checked", false);
        $("#cb_2S").prop("checked", false);
        $("#cb_3S").prop("checked", false);
        $("#cb_4S").prop("checked", false);
        $("#cb_5S").prop("checked", false);
        $("#cb_6S").prop("checked", false);
        $("#cb_7S").prop("checked", false);
        $("#cb_8S").prop("checked", false);
        $("#cb_9S").prop("checked", false);
        $("#cb_10S").prop("checked", false);
        $("#cb_11S").prop("checked", false);
        $("#cb_12S").prop("checked", false);
        $("#cb_13S").prop("checked", false);
            
    });
    $('#tbGudarVictimas').click(function () {
        ($('#divMensajeVictimas').attr("class", "alert alert-block alert-danger fade in smaller"));
        var codsinVicitma = ($('#tb_codsinVictimas').val());
        var codVhlVictima = ($('#ddl_vhlVictimas').val());
        var tipoIdentificacion = ($('#ddl_tipoIdentificacion option:selected').text());
        var numeroIdentificacion = ($('#tb_IdentificacionVictima').val());
        var edadVictima = ($('#tb_edadVictima').val());
        var generoVictima = ($('#ddl_generoVictima').val());
        var condicionVictima24 = ($('#ddl_condicionVictimas24 option:selected').text());
        var condicionVictima30 = ($('#ddl_condicionVictimas30 option:selected').text());
        var tipoParticipanteVictima = ($('#ddl_tipoParticipante option:selected').text());
        var posicionPlazaVictima = "";// ($('#ddl_posicionPlaza option:selected').text());
        var usoCasoVictima = $("#ddl_uso_casco").val();
        var usoCinturonVictima = $("#ddl_uso_cinturon").val();
        var consumoAlcoholVictima = $("#ddl_consumo_alcohol").val();
        var accionesPeaton = ($('#ddl_accionesPeaton option:selected').text());
        var nombreVictima = $('#tb_NombresVictima').val();
        if (tipoParticipanteVictima === 'PEATÓN' || tipoParticipanteVictima === 'CONDUCTOR')
        {
            $('#ddl_accionesPeaton').each(function () {

                accionesPeaton = ($('#ddl_accionesPeaton option:selected').text());
            });
            //------SELECCIONAR------
        }
        if ($('#cb_1S').prop("checked") === true) posicionPlazaVictima = "FRONTAL IZQUIERDO";
        if ($('#cb_2S').prop("checked") === true) posicionPlazaVictima = "FRONTAL CENTRAL";
        if ($('#cb_3S').prop("checked") === true) posicionPlazaVictima = "FRONTAL DERECHO";
        if ($('#cb_4S').prop("checked") === true) posicionPlazaVictima = "CENTRAL IZQUIERDO";
        if ($('#cb_5S').prop("checked") === true) posicionPlazaVictima = "CENTRAL";
        if ($('#cb_6S').prop("checked") === true) posicionPlazaVictima = "CENTRAL DERECHO";
        if ($('#cb_7S').prop("checked") === true) posicionPlazaVictima = "TRASERO IZQUIERDO";
        if ($('#cb_8S').prop("checked") === true) posicionPlazaVictima = "TRASERO CENTRAL";
        if ($('#cb_9S').prop("checked") === true) posicionPlazaVictima = "TRASERO DERECHO";
        if ($('#cb_10S').prop("checked") === true) posicionPlazaVictima = "BALDE";
        if ($('#cb_11S').prop("checked") === true) posicionPlazaVictima = "DE PIE";
        if ($('#cb_12S').prop("checked") === true) posicionPlazaVictima = "OTROS";
        if ($('#cb_13S').prop("checked") === true) posicionPlazaVictima = "NIÑO EN BRAZOS";
        var mensaje = "";
        if (codsinVicitma === '0' || codsinVicitma.toString().trim() === '')
        {
            mensaje = 'Debe ingresar un siniestro.';
            ($('#divMensajeVictimas').attr("class", "alert alert-block alert-danger fade in smaller"));
        }
        else if (codVhlVictima === '' || codVhlVictima === '-1' || codVhlVictima === '0' || codVhlVictima === null) {
            mensaje = 'Seleccione el Vehiculo involucrado.';
            ($('#divMensajeVictimas').attr("class", "alert alert-block alert-danger fade in smaller"));
        }
        else if (tipoIdentificacion === '' || tipoIdentificacion === 'SELECCIONAR' || tipoIdentificacion === '-1') {
            mensaje = 'Seleccione el tipo de identificacion.';
            ($('#divMensajeVictimas').attr("class", "alert alert-block alert-danger fade in smaller"));
        }
        else if ((numeroIdentificacion.trim() === '' || parseInt(numeroIdentificacion) < 10) && tipoIdentificacion !== 'NO IDENTIFICADO') {
            mensaje = 'Ingrese el numero de identificacion.';
            ($('#divMensajeVictimas').attr("class", "alert alert-block alert-danger fade in smaller"));
        }
        else if (edadVictima.trim() === '' ) {
            mensaje = 'Ingrese la edad de la víctima.';
            ($('#divMensajeVictimas').attr("class", "alert alert-block alert-danger fade in smaller"));
        }
        else if ((generoVictima.trim() === '' || generoVictima === '-1') ) {
            mensaje = 'Seleccione el genero de la víctima.';
            ($('#divMensajeVictimas').attr("class", "alert alert-block alert-danger fade in smaller"));
        }
        else if ((condicionVictima24.trim() === '' || condicionVictima24 === 'SELECCIONAR')) {
            mensaje = 'Seleccione la Condición Víctima 24h.';
            ($('#divMensajeVictimas').attr("class", "alert alert-block alert-danger fade in smaller"));
        }
       
        else if ((tipoParticipanteVictima.trim() === '' || tipoParticipanteVictima === 'SELECCIONAR')) {
            mensaje = 'Seleccione el tipo participante';
            ($('#divMensajeVictimas').attr("class", "alert alert-block alert-danger fade in smaller"));
        }
        else if ((usoCasoVictima.trim() === '' || usoCasoVictima === '-1')) {
            mensaje = 'Seleccione el uso del casco';
            ($('#divMensajeVictimas').attr("class", "alert alert-block alert-danger fade in smaller"));
        }
        else if ((usoCinturonVictima.trim() === '' || usoCinturonVictima === '-1')) {
            mensaje = 'Seleccione el uso del cinturon';
            ($('#divMensajeVictimas').attr("class", "alert alert-block alert-danger fade in smaller"));
        }
        else if ((consumoAlcoholVictima.trim() === '' || consumoAlcoholVictima === '-1')) {
            mensaje = 'Seleccione si tiene o no cunsumo de alcohol';
            ($('#divMensajeVictimas').attr("class", "alert alert-block alert-danger fade in smaller"));
        }
        else if ((posicionPlazaVictima.trim() === '' || posicionPlazaVictima === 'SELECCIONAR')) {
            mensaje = 'Seleccione la Posición de la Plaza.';
            ($('#divMensajeVictimas').attr("class", "alert alert-block alert-danger fade in smaller"));
        }
        else if ((tipoParticipanteVictima === 'PEATÓN' ||  tipoParticipanteVictima === 'CONDUCTOR') && ( accionesPeaton === '------SELECCIONAR------'  || accionesPeaton === '')) {
            mensaje = 'Seleccione las acciónes del peatón.';
            ($('#divMensajeVictimas').attr("class", "alert alert-block alert-danger fade in smaller"));
        }
       
        else {
            //var accionesPeatones = "";
            if (tipoParticipanteVictima === 'PEATÓN' || tipoParticipanteVictima === 'CONDUCTOR') {
                $('#ddl_accionesPeaton').each(function () {
                    accionesPeaton = $('#ddl_accionesPeaton').val();
                   
                });
               
            }
            console.log(accionesPeaton);
            var ccc =  accionesPeaton.toString();
            var jsonStringAccion = JSON.stringify(accionesPeaton);
           // $("#dt_victimasSiniestro").find("tr:gt(0)").remove();
            $.ajax({
                type: "get",
                url: 'GuardarVictimasInvolucradas',
                data: { tipidenvicinv: tipoIdentificacion, numidenvicinv: numeroIdentificacion, edavicinv: edadVictima, genvicinv: generoVictima, convicinv24: condicionVictima24, convicinv30: condicionVictima30, tipparvicinv: tipoParticipanteVictima, casvicinv: usoCasoVictima, cinvicinv: usoCinturonVictima, posvicinv: posicionPlazaVictima, conalcvicinv: consumoAlcoholVictima, codsin: codsinVicitma, codveh: codVhlVictima, desaccpea: jsonStringAccion ,ccc : ccc,nombre_victima : nombreVictima},
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (jsonResult) {
                    console.log('retorno victimas' + ' ' + jsonResult);
                    if (jsonResult.length === 0) {
                        mensaje = 'Error al grabar los datos.';
                        ($('#mensajeLabelVictimas').html(mensaje.toString()));
                        ($('#divMensajeVictimas').attr("class", "alert alert-block alert-danger fade in smaller"));
                    }
                    else if (jsonResult.length > 0) {
                        //($('#tb_codsinVictimas').val('0'));
                        ($('#ddl_tipoIdentificacion').val('-1'));
                        ($('#tb_IdentificacionVictima').val(''));
                        ($('#tb_edadVictima').val('0'));
                        ($('#ddl_generoVictima').val('-1'));
                        ($('#ddl_condicionVictimas24').val('-1'));
                        ($('#ddl_condicionVictimas30').val('-1'));
                        ($('#ddl_tipoParticipante').val('-1'));
                        $("#cb_1S").prop("checked", false);
                        $("#cb_2S").prop("checked", false);
                        $("#cb_3S").prop("checked", false);
                        $("#cb_4S").prop("checked", false);
                        $("#cb_5S").prop("checked", false);
                        $("#cb_6S").prop("checked", false);
                        $("#cb_7S").prop("checked", false);
                        $("#cb_8S").prop("checked", false);
                        $("#cb_9S").prop("checked", false);
                        $("#cb_10S").prop("checked", false);
                        $("#cb_11S").prop("checked", false);
                        $("#cb_12S").prop("checked", false);
                        $("#cb_13S").prop("checked", false);
                        ($('#ddl_vhlVictimas').val('-1'));
                        ($('#ddl_accionesPeaton').val('-1'));
                        $('#divAccionesPeaton').css("display", "none");
                        $('#divAccionesAgregar').css("display", "none");
                        $("#cb_usoCascoVictima").prop("checked", false);
                        $("#cb_usoCinturonVictima").prop("checked", false);
                        $('#cb_consumoAlcoholVictima').prop("checked", false);
                        ($('#divPanelelGridAccionesPeaton').css("display", "none"));
                        ($('#divVerVictimasSiniestro').css("display", "block"));
                        
                        mensaje = 'Víctima involucrada ingresada correctamente.';
                        ($('#divMensajeVictimas').attr("class", "alert alert-success smaller"));
                        ($('#mensajeLabelVictimas').html(mensaje.toString()));
                        $("#dt_victimasSiniestro").find("tr:gt(0)").remove();
                        $.each(jsonResult, function (key, value) {
                            /* Vamos agregando a nuestra tabla las filas necesarias */
                            $("#dt_victimasSiniestro").append("<tr><td>" + value.tipidenvicinv + "</td><td>" + value.numidenvicinv + "</td><td>" + value.edavicinv + "</td><td>" + value.sexo + "</td><td>" + value.convicinv24 + "</td><td>" + value.tipparvicinv + "</td><td>" + value.usO_CASO + "</td><td>" + value.usO_CINTU + "</td><td>" + value.posvicinv + "</td><td>" + value.conS_ALCOHOL + "</td></tr>");
                        });
                       // $('#myTab li:eq("3") a').tab('show');
                        InicializaVictimas();
                        ($('#panelMensajeVictimas').css("display", "block"));
                        $('#respuesta-ej3').toggle();
                        $('#respuesta-ej4').toggle();
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("Error en la conexión con la base de datos, vuelva guardar la víctima");
                }

            });
        }
        console.log(mensaje.toString());
        ($('#mensajeLabelVictimas').html(mensaje.toString()));
        ($('#panelMensajeVictimas').css("display", "block"));
    });

    $('#btnCerrarVictimas').click(function () {
        var codsin = ($('#tb_codsinVictimas').val());
        $.ajax({
            type: "get",
            url: 'ObtenerTraerNumLesionadosFallecidos',
            data: { codsin: codsin },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (jsonResult) {
                console.log(jsonResult);
                $.each(jsonResult, function (key, value) {
                    ($('#tbnumlessin').val(value.value.toString()));
                    ($('#tbnumfalsin').val(value.text.toString()));
                });
                
                

            },

            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(textStatus + ": " + XMLHttpRequest.responseText);
            }

        });
    });
    $('#ddl_tipoParticipante').change(function () {
        var retorno = $('#tbTipoVhlVictima').val();
       // alert(retorno.toString());
        if ($('#ddl_tipoParticipante option:selected').text() === 'PEATÓN') {
            $("#cb_1S").prop("checked", false);
            $("#cb_2S").prop("checked", false);
            $("#cb_3S").prop("checked", false);
            $("#cb_4S").prop("checked", false);
            $("#cb_5S").prop("checked", false);
            $("#cb_6S").prop("checked", false);
            $("#cb_7S").prop("checked", false);
            $("#cb_8S").prop("checked", false);
            $("#cb_9S").prop("checked", false);
            $("#cb_10S").prop("checked", false);
            $("#cb_12S").prop("checked", false);
            $("#cb_13S").prop("checked", false);
            $("#cb_11S").prop("checked", true);
            $("#cb_1S").attr("disabled", "disabled");
            $("#cb_2S").attr("disabled", "disabled");
            $("#cb_3S").attr("disabled", "disabled");
            $("#cb_4S").attr("disabled", "disabled");
            $("#cb_5S").attr("disabled", "disabled");
            $("#cb_6S").attr("disabled", "disabled");
            $("#cb_7S").attr("disabled", "disabled");
            $("#cb_8S").attr("disabled", "disabled");
            $("#cb_9S").attr("disabled", "disabled");
            $("#cb_10S").attr("disabled", "disabled");
            $("#cb_12S").attr("disabled", "disabled");
            $("#cb_13S").attr("disabled", "disabled");
            $('#ddl_accionesPeaton').css("display", "block");
            $('#divAccionesPeaton').css("display", "block");
            $('#divAccionesAgregar').css("display", "block");
            ($('#divPanelelGridAccionesPeaton').css("display", "block"));
            ($('#ddl_accionesPeaton').val('-1'));
            $("#ddl_uso_casco").attr("disabled", "disabled");
            $("#ddl_uso_cinturon").attr("disabled", "disabled");
            $("#ddl_consumo_alcohol").attr("disabled", "disabled");
            $("#ddl_uso_casco").val("2");
            $("#ddl_uso_cinturon").val("2");
            $("#ddl_consumo_alcohol").val("2");
            $('#divAccionesPeaton').css("display", "block");
            $("#ddl_consumo_alcohol").removeAttr("disabled");
            
            
        }

        else if ($('#ddl_tipoParticipante option:selected').text() === 'CONDUCTOR' && (retorno.toString() !== "8" && retorno.toString() !== "16")) {
            $('#ddl_accionesPeaton').css("display", "block");
            $('#divAccionesPeaton').css("display", "block");
            $('#divAccionesAgregar').css("display", "block");
            ($('#divPanelelGridAccionesPeaton').css("display", "block"));
            $('#divAccionesPeaton').css("display", "block");
            ($('#ddl_accionesPeaton').val('-1'));
            //$("#cb_usoCascoVictima").prop("checked", false);
            //$("#cb_usoCinturonVictima").prop("checked", true);
            //$("#cb_consumoAlcoholVictima").prop("checked", false);
            $("#cb_1S").prop("checked", true);
            $("#cb_2S").prop("checked", false);
            $("#cb_3S").prop("checked", false);
            $("#cb_4S").prop("checked", false);
            $("#cb_5S").prop("checked", false);
            $("#cb_6S").prop("checked", false);
            $("#cb_7S").prop("checked", false);
            $("#cb_8S").prop("checked", false);
            $("#cb_9S").prop("checked", false);
            $("#cb_10S").prop("checked", false);
            $("#cb_11S").prop("checked", false);
            $("#cb_12S").prop("checked", false);
            $("#cb_13S").prop("checked", false);

            $("#cb_1S").removeAttr("disabled");
            $("#cb_2S").attr("disabled", "disabled");
            $("#cb_3S").attr("disabled", "disabled");
            $("#cb_4S").attr("disabled", "disabled");
            $("#cb_5S").attr("disabled", "disabled");
            $("#cb_6S").attr("disabled", "disabled");
            $("#cb_7S").attr("disabled", "disabled");
            $("#cb_8S").attr("disabled", "disabled");
            $("#cb_9S").attr("disabled", "disabled");
            $("#cb_10S").attr("disabled", "disabled");
            $("#cb_11S").attr("disabled", "disabled");
            $("#cb_12S").attr("disabled", "disabled");
            $("#cb_13S").attr("disabled", "disabled");

            $("#ddl_uso_casco").removeAttr("disabled");
            $("#ddl_uso_casco").val("2");
            $("#ddl_uso_cinturon").val("-1");
            $("#ddl_consumo_alcohol").val("-1");
            if (retorno.toString() === "8" && retorno.toString() === "16")
            {
                //alert('cinturon')
                $("#ddl_uso_cinturon").val("2");
                $("#ddl_uso_cinturon").attr("disabled", "disabled");
            }
            else
                $("#ddl_uso_cinturon").removeAttr("disabled");

            
            $("#ddl_uso_casco").attr("disabled", "disabled");
            $("#ddl_uso_cinturon").removeAttr("disabled");
            $("#ddl_uso_cinturon").val("-1");
            $("#ddl_uso_casco").val("2");
        }
        else if ($('#ddl_tipoParticipante option:selected').text() === 'CONDUCTOR'  &&( retorno.toString() ==="8" || retorno.toString() ==="16")) {
            $('#ddl_accionesPeaton').css("display", "block");
            $('#divAccionesPeaton').css("display", "block");
            $('#divAccionesAgregar').css("display", "block");
            ($('#divPanelelGridAccionesPeaton').css("display", "block"));
            $('#divAccionesPeaton').css("display", "block");
            ($('#ddl_accionesPeaton').val('-1'));
            //$("#cb_usoCascoVictima").prop("checked", false);
            //$("#cb_usoCinturonVictima").prop("checked", true);
            //$("#cb_consumoAlcoholVictima").prop("checked", false);
            $("#cb_1S").prop("checked", true);
            $("#cb_2S").prop("checked", false);
            $("#cb_3S").prop("checked", false);
            $("#cb_4S").prop("checked", false);
            $("#cb_5S").prop("checked", false);
            $("#cb_6S").prop("checked", false);
            $("#cb_7S").prop("checked", false);
            $("#cb_8S").prop("checked", false);
            $("#cb_9S").prop("checked", false);
            $("#cb_10S").prop("checked", false);
            $("#cb_11S").prop("checked", false);
            $("#cb_12S").prop("checked", false);
            $("#cb_13S").prop("checked", false);

            $("#cb_1S").removeAttr("disabled");
            $("#cb_2S").attr("disabled", "disabled");
            $("#cb_3S").attr("disabled", "disabled");
            $("#cb_4S").attr("disabled", "disabled");
            $("#cb_5S").attr("disabled", "disabled");
            $("#cb_6S").attr("disabled", "disabled");
            $("#cb_7S").attr("disabled", "disabled");
            $("#cb_8S").attr("disabled", "disabled");
            $("#cb_9S").attr("disabled", "disabled");
            $("#cb_10S").attr("disabled", "disabled");
            $("#cb_11S").attr("disabled", "disabled");
            $("#cb_12S").attr("disabled", "disabled");
            $("#cb_13S").attr("disabled", "disabled");

            $("#ddl_uso_casco").removeAttr("disabled");
            $("#ddl_uso_casco").val("2");
            $("#ddl_uso_cinturon").val("-1");
            $("#ddl_consumo_alcohol").val("-1");
            $("#ddl_uso_cinturon").val("2");
            $("#ddl_uso_cinturon").attr("disabled", "disabled");

            $("#ddl_consumo_alcohol").removeAttr("disabled");
        }
        else if ($('#ddl_tipoParticipante option:selected').text() === 'PASAJERO' && (retorno.toString() !== "8" && retorno.toString() !== "16")) {
           // alert('pasajero');
            $('#ddl_accionesPeaton').css("display", "none");
            $('#divAccionesPeaton').css("display", "none");
            $('#divAccionesAgregar').css("display", "none");
            ($('#divPanelelGridAccionesPeaton').css("display", "none"));
            $('#divAccionesPeaton').css("display", "none");
            ($('#ddl_accionesPeaton').val('-1'));
            $("#ddl_uso_casco").attr("disabled","disabled");
            $("#ddl_uso_casco").val('2');
            $("#ddl_consumo_alcohol").removeAttr("disabled");
            $("#ddl_uso_cinturon").removeAttr("disabled");
            //$("#cb_usoCascoVictima").prop("checked", false);
            //$("#cb_usoCinturonVictima").prop("checked", false);
            //$("#cb_consumoAlcoholVictima").prop("checked", false);
            $("#cb_1S").prop("checked", false);
            $("#cb_2S").prop("checked", false);
            $("#cb_3S").prop("checked", false);
            $("#cb_4S").prop("checked", false);
            $("#cb_5S").prop("checked", false);
            $("#cb_6S").prop("checked", false);
            $("#cb_7S").prop("checked", false);
            $("#cb_8S").prop("checked", false);
            $("#cb_9S").prop("checked", false);
            $("#cb_10S").prop("checked", false);
            $("#cb_11S").prop("checked", false);
            $("#cb_12S").prop("checked", false);
            $("#cb_13S").prop("checked", false);

            $("#cb_1S").attr("disabled", "disabled");
            $("#cb_2S").removeAttr("disabled");
            $("#cb_3S").removeAttr("disabled");
            $("#cb_4S").removeAttr("disabled");
            $("#cb_5S").removeAttr("disabled");
            $("#cb_6S").removeAttr("disabled");
            $("#cb_7S").removeAttr("disabled");
            $("#cb_8S").removeAttr("disabled");
            $("#cb_9S").removeAttr("disabled");
            $("#cb_10S").removeAttr("disabled");
            $("#cb_11S").removeAttr("disabled");
            $("#cb_12S").removeAttr("disabled");
            $("#cb_13S").removeAttr("disabled");
        }
        else if ($('#ddl_tipoParticipante option:selected').text() === 'PASAJERO' && (retorno.toString() === "8" || retorno.toString() === "16")) {
          //  alert(retorno.toString());
            $('#ddl_accionesPeaton').css("display", "none");
            $('#divAccionesPeaton').css("display", "none");
            $('#divAccionesAgregar').css("display", "none");
            ($('#divPanelelGridAccionesPeaton').css("display", "none"));
            $('#divAccionesPeaton').css("display", "none");
            ($('#ddl_accionesPeaton').val('-1'));
            ($('#ddl_uso_cinturon').val('2'));
            $("#ddl_uso_cinturon").attr("disabled", "disabled");
            //$("#cb_usoCascoVictima").prop("checked", false);
            //$("#cb_usoCinturonVictima").prop("checked", false);
            //$("#cb_consumoAlcoholVictima").prop("checked", false);
            $("#cb_1S").prop("checked", false);
            $("#cb_2S").prop("checked", false);
            $("#cb_3S").prop("checked", false);
            $("#cb_4S").prop("checked", false);
            $("#cb_5S").prop("checked", false);
            $("#cb_6S").prop("checked", false);
            $("#cb_7S").prop("checked", false);
            $("#cb_8S").prop("checked", false);
            $("#cb_9S").prop("checked", false);
            $("#cb_10S").prop("checked", false);
            $("#cb_11S").prop("checked", false);
            $("#cb_12S").prop("checked", false);
            $("#cb_13S").prop("checked", false);

            $("#cb_1S").attr("disabled", "disabled");
            $("#cb_2S").attr("disabled", "disabled");
            $("#cb_3S").attr("disabled", "disabled");
            $("#cb_4S").removeAttr("disabled");
            $("#cb_5S").attr("disabled", "disabled");
            $("#cb_6S").attr("disabled", "disabled");
            $("#cb_7S").removeAttr("disabled");
            $("#cb_8S").attr("disabled", "disabled");
            $("#cb_9S").attr("disabled", "disabled");
            $("#cb_10S").attr("disabled", "disabled");
            $("#cb_11S").attr("disabled", "disabled");
            $("#cb_12S").removeAttr("disabled");
            $("#cb_13S").removeAttr("disabled");
            $("#ddl_uso_casco").removeAttr("disabled");
            $("#ddl_consumo_alcohol").removeAttr("disabled");
            
        }
      
    });


    /**************************************
    * GUARDA VHL INVOLUCRADOS
    ***************************************/
    

    ($('#tb_matpelvehinv').css("display", "none"));
    ($('#tb_codser').css("display", "none"));
    ($('#tb_codtipve').css("display", "none"));

    $('#ddl_tipo_servicio_vhl').change(function () {
        var tipoSerVhl = ($('#ddl_tipo_servicio_vhl').val());
        ($('#tb_codser').val(tipoSerVhl.toString()))
    });
    $('#ddl_tipo_vhl').change(function () {
        var tipoVhl = ($('#ddl_tipo_vhl').val());
        ($('#tb_codtipve').val(tipoVhl.toString()))
    });
    $('#ddl_vhl_materialPeligroso').change(function () {
        var matPeligroso = ($('#ddl_vhl_materialPeligroso  option:selected').text());
        ($('#tb_matpelvehinv').val(matPeligroso.toString()))
    });

    $('#ddl_tipo_vhl').change(function () {
        if ($('#ddl_tipo_vhl').val().toString() === '16') {
            $("#tb_placvehinv").val('ND');
            $("#tb_chavehinv").val('ND');
            $("#tb_marvehinv").val('ND');
            $("#tb_modvehinv").val('ND');
            $("#tb_anivehinv").val('0');
            $("#tb_cilvehinv").val('0');
            $("#ddl_vhl_materialPeligroso").val('0');
            $("#ddl_tipo_servicio_vhl").val('3');
            $("#btnBuscarVhl").css("display", "none");
            $("#divBuscarPlaca").css("display", "none");

            $('#tb_placvehinv').attr('disabled', 'disabled');
            $('#tb_chavehinv').attr('disabled', 'disabled');
            $('#tb_marvehinv').attr('disabled', 'disabled');
            $('#tb_modvehinv').attr('disabled', 'disabled');
            $('#tb_anivehinv').attr('disabled', 'disabled');
            $('#tb_cilvehinv').attr('disabled', 'disabled');
            $('#ddl_seguro_privado_vhl').val('2');
            $('#ddl_danio_material').val('2');
            $('#ddl_matricula_vigente').val('2');

            $('#ddl_tipo_servicio_vhl').attr('disabled', 'disabled');
            $('#ddl_seguro_privado_vhl').attr('disabled', 'disabled');
            $('#ddl_danio_material').attr('disabled', 'disabled');
            $('#ddl_matricula_vigente').attr('disabled', 'disabled');

        }
        else if ($('#ddl_tipo_vhl').val().toString() !== '16' && $('#ddl_vehiculo_identificado').val() == '1') {
            $("#tb_placvehinv").val('');
            $("#tb_chavehinv").val('');
            $("#tb_marvehinv").val('');
            $("#tb_modvehinv").val('');
            $("#tb_anivehinv").val('0');
            $("#tb_cilvehinv").val('0');
            $("#ddl_vhl_materialPeligroso").val('0');
            $("#ddl_tipo_servicio_vhl").val('-1');
            $("#btnBuscarVhl").css("display", "block");
            $("#divBuscarPlaca").css("display", "block");
            $('#tb_placvehinv').removeAttr('disabled');
            $('#tb_chavehinv').removeAttr('disabled');
            $('#tb_marvehinv').removeAttr('disabled');
            $('#tb_modvehinv').removeAttr('disabled');
            $('#tb_anivehinv').removeAttr('disabled');
            $('#tb_cilvehinv').removeAttr('disabled');

            $('#ddl_seguro_privado_vhl').val('-1');
            $('#ddl_danio_material').val('-1');
            $('#ddl_matricula_vigente').val('-1');
            $('#ddl_tipo_servicio_vhl').val('-1');
            $('#ddl_tipo_servicio_vhl').removeAttr('disabled');
            $('#ddl_seguro_privado_vhl').removeAttr('disabled');
            $('#ddl_danio_material').removeAttr('disabled');
            $('#ddl_matricula_vigente').removeAttr('disabled');
        }

        if ($('#ddl_tipo_vhl').val().toString() !== '-1')
        {
            var select = $('#ddl_subtipo_vhl');
            select.find('option').remove().end().append('<option value="-1">SELECCIONAR</option>').val('-1');
            var codTipoVhls = $('#ddl_tipo_vhl').val();
            $.ajax({
                type: "GET",
                url: "ObtenerlistaSubTipoVehiculo",
                data: { codTipoVhl: codTipoVhls },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (jsonResult) {
                    if (jsonResult.length >0) {
                        $.each(jsonResult, function (key, value) {
                            select.append('<option value="' + value.value + '">' + value.text + '</option>').val(value.value);
                        });
                        if ($('#ddl_tipo_vhl').val() === '6')
                        {
                            select.val('17');
                            $('#ddl_subtipo_vhl').attr('disabled', 'disabled');
                        }
                        else if ($('#ddl_tipo_vhl').val() === '16')
                        {
                            select.val('54');
                            $('#ddl_subtipo_vhl').attr('disabled', 'disabled');
                        }
                        else if ($('#ddl_tipo_vhl').val() === '18') {
                            select.val('16');
                            $('#ddl_subtipo_vhl').attr('disabled', 'disabled');
                        }
                        else {
                            select.val('-1');
                            $('#ddl_subtipo_vhl').removeAttr('disabled');
                        }
                        
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("Error al cargar los sub tipo de vehiculos.");
                }
            });
        }


    });

    $('#btnBuscarVhl').click(function () {
        var placa = $('#tb_placvehinv').val();
        var chasis = $('#tb_chavehinv').val();
        var mensaje = "";
        var parametro = "";
        var opcion = "";
        if (placa !== '' || chasis !== '') {
            if (placa !== '') {
                opcion = "1";
                // alert(opcion);
                parametro = placa;
            }
            if (chasis.trim() !== '') {
                opcion = "2";
                parametro = chasis;
            }
            if (opcion !== '') {
                $('#divCargaVhlImagen').css("display", "block");
                $.ajax({
                    type: "GET",
                    url: "ObtenerPlacaVehiculo",
                    data: { parametro: parametro, opcion: opcion },
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (jsonResult) {
                        console.log('busqueda placa' + '' + jsonResult);
                        if (jsonResult.length > 0) {
                            $.each(jsonResult, function (key, value) {
                                if (value.chavehinv.toString() !== '') {
                                    $("#tb_placvehinv").val(value.placvehinv);
                                    $('#tb_placvehinv').attr("disabled", "disabled");
                                    $("#tb_chavehinv").val(value.chavehinv);
                                    $('#tb_chavehinv').attr("disabled", "disabled");
                                    $("#tb_marvehinv").val(value.marvehinv);
                                    $('#tb_marvehinv').attr("disabled", "disabled");
                                    $("#tb_modvehinv").val(value.modvehinv);
                                    $('#tb_modvehinv').attr("disabled", "disabled");
                                    $("#tb_anivehinv").val(value.anivehinv);
                                    $('#tb_anivehinv').attr("disabled", "disabled");
                                    $("#tb_cilvehinv").val(value.cilvehinv);
                                    $('#tb_cilvehinv').attr("disabled", "disabled");
                                    $('#cb_matvigvehinv').prop("checked", false);
                                    $("ddl_tipo_vhl option").each(function () { this.selected = (this.text === value.tipoVehiculo); });
                                    //($('#ddl_tipo_vhl option:selected').text(value.tipoVehiculo));
                                    mensaje = "Busqueda exitosa.";
                                    alert(mensaje.toString());
                                    $('#divCargaVhlImagen').css("display", "none");
                                    ($('#panelMensajeVHL').css("display", "none"));
                                }
                                else {
                                    mensaje = "No existe resultados en la busqueda.";
                                    alert(mensaje.toString());
                                }

                            });
                        }
                        else {
                            mensaje = "No existe resultados en la busqueda.";
                            alert(mensaje.toString());
                        }
                        
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("Error en la búsqueda.");
                    }
                });
            }
            else {
                mensaje = "Ingrese una opcion para la búsqueda (PLACA ó CHASIS).";
                alert(mensaje.toString());

            }


        }
        else {
            mensaje = "Ingrese una opcion para la búsqueda (PLACA ó CHASIS).";
            alert(mensaje.toString());
        }


    });

    //var togglers = $('#menulateral ul.acordeon');
    //var triggers = $('#menulateral span.despliega');
    //triggers.click(function () {
    //    togglers.slideUp('slow');
    //    $(this).next('ul.acordeon').slideDown('slow');
    //});
    //triggers.first().click();
    $('#alternar-respuesta-ej1').on('click', function () {
        $('#respuesta-ej1').toggle();
        $('#respuesta-ej2').toggle();
    });
    $('#alternar-respuesta-ej2').on('click', function () {
        $('#respuesta-ej1').toggle();
        $('#respuesta-ej2').toggle();
    });
    $('#alternar-respuesta-ej3').on('click', function () {
        $('#respuesta-ej3').toggle();
        $('#respuesta-ej4').toggle();
    });
    $('#alternar-respuesta-ej4').on('click', function () {
        $('#respuesta-ej3').toggle();
        $('#respuesta-ej4').toggle();
    });
    $('#alternar-respuesta-ej5S').on('click', function () {
        $('#respuesta-ej5S').toggle();
        $('#respuesta-ej6S').toggle();
       
    });

    $('#alternar-respuesta-ej6S').on('click', function () {
        $('#respuesta-ej5S').toggle();
        $('#respuesta-ej6S').toggle();
    });


    $('#btLimpiarVHL').click(function () {
       
        $('#respuesta-ej1').toggle();
        $('#respuesta-ej2').toggle();

        var selectsv = $('#ddl_subtipo_vhl');
        selectsv.find('option').remove().end().append('<option value="-1">SELECCIONAR</option>').val('-1');
        $("#tb_placvehinv").val('');
        $("#tb_chavehinv").val('');
        $("#tb_marvehinv").val('');
        $("#tb_modvehinv").val('');
        $("#tb_anivehinv").val('');
        $("#tb_cilvehinv").val('');
        $("#ddl_tipo_vhl").val('-1');
        $("#ddl_tipo_servicio_vhl").val('-1');
        $('#ddl_subtipo_vhl').val('-1');
        $("#ddl_vhl_materialPeligroso").val('0');
        $('#tb_placvehinv').removeAttr('disabled');
        $('#tb_chavehinv').removeAttr('disabled');
        $('#tb_marvehinv').removeAttr('disabled');
        $('#tb_modvehinv').removeAttr('disabled');
        $('#tb_anivehinv').removeAttr('disabled');
        $('#tb_cilvehinv').removeAttr('disabled');
        $('#ddl_seguro_privado_vhl').val('-1');
        $('#ddl_danio_material').val('-1');
        $('#ddl_matricula_vigente').val('-1');
      //  $("#btnBuscarVhl").css("display", "none");
        $("#panelMensajeVHL").css("display", "none");
    });

  
    $('#tbGudarVhlInvolucrados').click(function () {
        ($('#divMensajeVHL').attr("class", "alert alert-block alert-danger fade in smaller"));
        var mensaje = '';
        var codsiniestroVHL = $('#lbCodigoSiniestroGuardadoOculto').html();
        var placaVHL = ($('#tb_placvehinv').val());
        var chasisVHL = ($('#tb_chavehinv').val());
        var marcaVHL = ($('#tb_marvehinv').val());
        var modeloVHL = ($('#tb_modvehinv').val());
        var anioVHL = ($('#tb_anivehinv').val());
        var cilindrajeVHL = ($('#tb_cilvehinv').val());
        var materialPeligrosoVHL = ($('#ddl_vhl_materialPeligroso option:selected').text());
        var codtipoServVHL = $('#ddl_tipo_servicio_vhl').val();
        var codtipoVHL = ($('#ddl_tipo_vhl').val());
        var codSubtipoVHL = ($('#ddl_subtipo_vhl').val());
        var seguroPrivado = $("#ddl_seguro_privado_vhl").val();
        var danioMaterial = $("#ddl_danio_material").val();
        var matriculaVigente = $('#ddl_matricula_vigente').val();
        console.log(codtipoServVHL);
        console.log(danioMaterial);
        console.log(matriculaVigente);
        if (codsiniestroVHL === '' || codsiniestroVHL === '0' || parseInt(codsiniestroVHL) < -1)
        {
            mensaje = 'Debe ingresar un siniestro';
            
        }
        else if (codtipoVHL.toString().trim() === "" || codtipoVHL.toString().trim() === "-1") {
            mensaje = 'Seleccione el tipo de vehículo.';
        }
        else if (codSubtipoVHL.toString().trim() === "" || codSubtipoVHL.toString().trim() === "-1") {
            mensaje = 'Seleccione el sub tipo de vehículo.';
        }
        else if (placaVHL.toString().trim() === "" && codtipoVHL.toString() !== "16"  )
        {
            mensaje = 'Ingrese la placa del vehículo';
        }
        else if (chasisVHL.toString().trim() === "" && codtipoVHL.toString() !== "16") {
            mensaje = 'Ingrese el chasis del vehículo';
        }
        else if (marcaVHL.toString().trim() === "" && codtipoVHL.toString() !== "16") {
            mensaje = 'Ingrese la marca del Vehiculo del vehículo';
        }
        else if (modeloVHL.toString().trim() === "" && codtipoVHL.toString() !== "16") {
            mensaje = 'Ingrese el modelo del Vehiculo del vehículo';
        }
        else if (anioVHL.toString().trim() === "" && codtipoVHL.toString() !== "16") {
            mensaje = 'Ingrese el año del Vehiculo del vehículo';
        }
        else if (cilindrajeVHL.toString().trim() === "" && codtipoVHL.toString() !== "16") {
            mensaje = 'Ingrese el cilindraje del Vehiculodel vehículo';
        }
        else if (materialPeligrosoVHL.toString().trim() === "SELECCIONAR" && codtipoVHL.toString() !== "16") {
            mensaje = 'Seleccione si el vehiculo contenia material peligroso.';
        }
        else if (codtipoServVHL.toString().trim() === "-1" && codtipoVHL.toString() !== "16") {
            mensaje = 'Seleccione el tipo de servicio del vehículo.';
        }
        else if (codtipoServVHL.toString().trim() === "-1" && codtipoVHL.toString() !== "16") {
            mensaje = 'Seleccione el tipo de servicio del vehículo.';
        }

        else if (seguroPrivado.toString() === "-1" || seguroPrivado.toString() === "0")
        {
            mensaje = 'Seleccione si posee seguro privado.';
        }
        else if (danioMaterial.toString() === "-1" || danioMaterial.toString() === "0") {
            mensaje = 'Seleccione si ocasiono daños materiales.';
        }
        else if (matriculaVigente.toString() === "-1" || matriculaVigente.toString() === "0") {
            mensaje = 'Seleccione si tiene la matrícula vigente.';
        }
            // $("#dt_vehiculosSiniestro").is(":visible")
        else {
           $("#dt_vehiculosSiniestro").find("tr:gt(0)").remove();
            $.ajax({
                type: "get",
                url: 'JsoninsertarDatosVehiculosInvolucrados',
                data: { codsin: codsiniestroVHL, placa: placaVHL, chavehinv: chasisVHL, marvehinv: marcaVHL, modvehinv: modeloVHL, cilvehinv: cilindrajeVHL, matpelvehinv: materialPeligrosoVHL, codser: codtipoServVHL, codtipve: codtipoVHL, seguroPrivado: seguroPrivado, danioMaterial: danioMaterial, matriculaVigente: matriculaVigente, anivehinv: anioVHL,codsubtipoVHL: codSubtipoVHL},
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (jsonResult) {
                    console.log('retorno vhl inv' + ' ' + jsonResult);
                    if (jsonResult.length === 0) {
                        mensaje = 'Error al grabar los datos.';
                        ($('#mensajeLabelVHL').html(mensaje.toString()));
                    }
                    else if (jsonResult.length > 0) { 

                        //($('#tb_codsinVHL').val('0'));
                        ($('#tb_placvehinv').val(' '));
                        ($('#tb_chavehinv').val(' '));
                        ($('#tb_marvehinv').val(' '));
                        ($('#tb_modvehinv').val(' '));
                        ($('#tb_anivehinv').val(' '));
                        ($('#tb_cilvehinv').val(' '));
                        ($('#tb_matpelvehinv').val('-1'));
                        ($('#ddl_vhl_materialPeligroso').val('-1'));
                        ($('#tb_matpelvehinv').val('-1'));
                        ($('#ddl_tipo_servicio_vhl').val('-1'));
                        ($('#tb_codser').val('-1'));
                        ($('#ddl_tipo_vhl').val('-1'));
                        $('#ddl_subtipo_vhl').val('-1');
                        ($('#tb_codtipve').val('-1'));
                        $("#cb_segprivehinv").prop("checked", false);
                        $("#cb_danmatvehinv").prop("checked", true);
                        $('#cb_matvigvehinv').prop("checked", false);
                        var selectsv = $('#ddl_subtipo_vhl');
                        selectsv.find('option').remove().end().append('<option value="-1">SELECCIONAR</option>').val('-1');
                        $("#divVehiculosInvolucradosSiniestro").css("display", "block");
                        mensaje = 'Vehículo ingresado correctamente.';
                        ($('#divMensajeVHL').attr("class", "alert alert-success smaller"));
                        ($('#mensajeLabelVHL').html(mensaje.toString()));
                        $.each(jsonResult, function (key, value) {
                            /* Vamos agregando a nuestra tabla las filas necesarias */
                            var segpri = "";
                            var daniomat = "";//danmatvehinv
                            var mat = "";//danmatvehinv
                            if (value.segprivehinv === true) { segpri = 'SI' } else if (value.segprivehinv === false) segpri = 'NO'
                            if (value.danmatvehinv === true) { daniomat = 'SI' } else if (value.danmatvehinv === false) daniomat = 'NO'
                            if (value.matvigvehinv === true) { mat = 'SI' } else if (value.matvigvehinv === false) mat = 'NO'

                            //<button type="button" class="btn btn-default active" id="btnModalVhlInvolucrados" data-toggle="modal" data-target="#myModal">Vehículos Involucrados</button> <td><button type=\"button\" class=\"btn\" id=\"btnModificarVhlSiniestro\" data-id=" + value.codvehinv + "><span class=\"glyphicon glyphicon-edit\"></span>Modificar</button></td><
                            $("#dt_vehiculosSiniestro").append("<tr><td>" + value.placvehinv + "</td><td>" + value.chavehinv + "</td><td>" + value.marvehinv + "</td><td>" + value.anivehinv + "</td><td>" + value.desser + "</td><td>" + value.destipveh + "</td><td>" + value.dessubveh + "</td><td>" + segpri + "</td><td>" + daniomat + "</td><td>" + mat + "</td></tr>");
                        });
                        InicializaVictimas();
                        $('#respuesta-ej2').toggle();
                        $('#respuesta-ej1').toggle();
                        
                       
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert('Error en la conexión a la base de datos, Vuelva a ingresar el vehículo');
                }

            });
        }
        
        console.log(mensaje.toString());
        ($('#mensajeLabelVHL').html(mensaje.toString()));
        ($('#panelMensajeVHL').css("display", "block"));
    });

    $(document).on('click', '.btnModificarVhlSiniestro', function () {
        alert('mensaje alert');
    });
    //$('#td btnModificarVhlSiniestro').click(function () {
    //    alert("mensaje de vvvvvv");
    //});
    
    /**************************************
    * GUARDA ACCIONES PEATON
    ***************************************/
    $('#btnModalAccionesPeaton').click(function () {
        ($('#panelMensajePeaton').css("display", "none"));

        
       // ($('#ddl_victimasAccionesPeaton').val('-1'));
        var codsinAccionPeaton = ($('#lbCodigoSiniestroGuardadoOculto').html());
        ($('#tb_codsinAccionesPeaton').val(codsinAccionPeaton.toString()));
        $('#tb_codsinAccionesPeaton').attr("disabled", "disabled");

        //   var codsinVictimas = ($('#tb_codsinVictimas').val());
        console.log(codsinAccionPeaton.toString());
        
        var select = $('#ddl_victimasAccionesPeaton');
        select.find('option').remove().end().append('<option value="-1">SELECCIONAR</option>').val('-1');
        $.ajax({
            type: "get",
            url: 'ObtenerlistaPeaton',
            data: { codsin: codsinAccionPeaton },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (jsonResult) {
                console.log(jsonResult);
                $.each(jsonResult, function (key, value) {
                    select.append('<option value="' + value.value + '">' + value.text + '</option>').val(value.value);
                });
                select.val('-1');
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(textStatus + ": " + XMLHttpRequest.responseText);
            }

        });

    });

    $('#ddl_tipoParticipante').change(function () {
        if ($('#ddl_tipoParticipante').val() === '1')
        {

            $('#ddl_posicionPlaza').val('1');
        }
        else if ($('#ddl_tipoParticipante').val() === '2') {
            $('#ddl_posicionPlaza').val('-1');
        }
        else if ($('#ddl_tipoParticipante').val() === '3') {
            $('#cb_11').prop("checked", true);
        }
        

    });

    $('#tbGudarAccionesPeaton').click(function (){
            var codsinAccionPeaton = ($('#lbCodigoSiniestroGuardadoOculto').html());
            var codPeaton = $('#ddl_victimasAccionesPeaton').val();
            var codAccion = ($('#ddl_accionPeaton option:selected').text());
            ($('#divMensajePeaton').attr("class", "alert alert-block alert-danger fade in smaller"));
            var mensaje = '';
            if (codPeaton === '' || codPeaton === '-1')
            {
                mensaje = 'Seleccione el peaton';
                ($('#mensajeLabelPeaton').html(mensaje.toString()));
                ($('#panelMensajePeaton').css("display", "block"));
            }
            else if (codAccion === '' || codAccion === 'SELECCIONAR') {
                mensaje = 'Seleccione la accion del peaton.';
                  ($('#mensajeLabelPeaton').html(mensaje.toString()));
                ($('#panelMensajePeaton').css("display", "block"));
            }
            else {
                var params = new Object();
                params.codsin = codsinAccionPeaton;
                params.desaccpea = codAccion;
                params.codvicinv = codPeaton;


                $.ajax({

                    type: "get",
                    url: 'GuardarAccionesPeaton',
                    data: { desaccpea: codAccion, codvicinv: codPeaton },
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (retorno) {
                        console.log('retorno' + ' ' + retorno.toString());
                        if (retorno.toString() === "0") {
                            mensaje = 'No se puede registrar la infromación';
                            ($('#mensajeLabelPeaton').html(mensaje.toString()));

                            ($('#panelMensajePeaton').css("display", "block"));

                        
                        }
                        else if (retorno.toString() !== "0") {
                            mensaje = "Información registrada correctamente.";
                            console.log(mensaje.toString());
                            ($('#mensajeLabelPeaton').html(mensaje.toString()));
                            ($('#divMensajePeaton').attr("class", "alert alert-success smaller"));
                            ($('#ddl_accionPeaton').val('-1'));
                            ($('#panelMensajePeaton').css("display", "block"));
                            //  ($('#btnNuevoSiniestro').css("display", "block"));
                        }

                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("Error en la conexión con la base de datos, vuelva a grabar la acción al peatón");
                    }
                });
            }
    });


    /**************************************
    * GUARDA DANIOS A TERCEROS
    ***************************************/
    $('#btnModalDaniosTerceros').click(function () {
        ($('#panelMensaje').css("display", "none"));
        ($('#divVerDaniostercerosSiniestros').css("display", "none"));
        //($('#tb_codsinDanioTercero').val('0'));
        ($('#tb_obsdater').val(' '));
        ($('#tb_coddater').val('-1'));
        ($('#ddl_tipoDanioTercero').val('-1'));
        var codsinDanioTercero = ($('#lbCodigoSiniestroGuardadoOculto').html());
       ($('#tb_codsinDanioTercero').val(codsinDanioTercero.toString()));
        $('#tb_codsinDanioTercero').attr("disabled", "disabled");
    });

    

    $('#ddl_tipoDanioTercero').change(function () {
        var tipoDanioTercero = ($('#ddl_tipoDanioTercero').val());
        ($('#tb_coddater').val(tipoDanioTercero.toString()));
    });
    ($('#tb_coddater').css("display", "none"));
    ($('#panelMensaje').css("display", "none"));
    $('#btnLimpiarDanios').click(function () {
        ($('#panelMensaje').css("display", "none"));
       // ($('#divVerDaniostercerosSiniestros').css("display", "none"));
        //($('#tb_codsinDanioTercero').val('0'));
        ($('#tb_obsdater').val(''));
        ($('#tb_coddater').val('-1'));
        ($('#ddl_tipoDanioTercero').val('-1'));

        $('#respuesta-ej5S').toggle();
        $('#respuesta-ej6S').toggle();
    });

    $('#tbGudarDanioTercero').click(function () {
        ($('#divMensaje').attr("class", "alert alert-block alert-danger fade in smaller"));
        var mensaje = '';
        var codsiniestroDanioTercero = ($('#tb_codsinDanioTercero').val());
        var codTipoDanioTercero = ($('#tb_coddater').val());
        var observacionDanioTercero = ($('#tb_obsdater').val());
        if (codsiniestroDanioTercero === '' || codsiniestroDanioTercero === '0')
        {
            mensaje = 'Debe ingresar un siniestro';
            ($('#mensajeLabel').html(mensaje.toString()));

            ($('#panelMensaje').css("display", "block"));
        }
        else if (codTipoDanioTercero === '' || codTipoDanioTercero === '0' || codTipoDanioTercero === '-1') {
            mensaje = 'Seleccione el tipo de daño material';
            ($('#mensajeLabel').html(mensaje.toString()));

            ($('#panelMensaje').css("display", "block"));
        }
        else {
            var datoRetornoCodigoDanioMaterial = '0';
            var params = new Object();
            params.codsin = codsiniestroDanioTercero;
            params.obsdater = observacionDanioTercero;
            params.codtipdater = codTipoDanioTercero;
            //console.log(params);
            //console.log(($('#tb_codsinDanioTercero').val()));
            $("#dt_DaniosTercerosSiniestros").find("tr:gt(0)").remove();
            $.ajax({
                
                type: "get",
                url: 'GuardarDanioTercero',
                data: { codsin: ($('#tb_codsinDanioTercero').val()), obsdater: ($('#tb_obsdater').val()), codtipdater: ($('#tb_coddater').val()) },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (jsonResult) {
                    // datoRetornoCodigoDanioMaterial = jsonResult.toString();
                    console.log('retorno danio tercero');
                    console.log( jsonResult);
                    //console.log('datoRetornoCodigoDanioMaterial' + ' ' + datoRetornoCodigoDanioMaterial.toString());
                    if (jsonResult.length === 0) {
                        mensaje = 'No se puede registrar la información de daños a terceros';
                        ($('#mensajeLabel').html(mensaje.toString()));

                        ($('#panelMensaje').css("display", "block"));
                    }
                    else if (jsonResult.length > 0) {
                        mensaje = "Daño a tercero ingresado  correctamente.";
                        console.log(mensaje.toString());
                        ($('#mensajeLabel').html(mensaje.toString()));
                        ($('#divMensaje').attr("class", "alert alert-success smaller"));
                        //($('#tb_codsinDanioTercero').val('0'));
                        ($('#tb_obsdater').val(''));
                        ($('#tb_coddater').val('-1'));
                        ($('#ddl_tipoDanioTercero').val('-1'));
                        ($('#panelMensaje').css("display", "block"));
                        $.each(jsonResult, function (key, value) {
                            /* Vamos agregando a nuestra tabla las filas necesarias */
                            $("#dt_DaniosTercerosSiniestros").append("<tr><td style=\"table-layout:fixed\">" + value.destipdater + "</td><td  class=\"ajustar\">" + value.obsdater + "</td><td>" + value.codsin + "</td></tr>");
                        });
                        ($('#divVerDaniostercerosSiniestros').css("display", "block"));
                        $('#respuesta-ej5S').toggle();
                        $('#respuesta-ej6S').toggle();
                 
                        
                        
                       
                    }

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("Error en la conexión con la base de datos, vuelva a grabar el daño a terceros");
                }
            });
           

        }
        
    });

});


/*******************************************************
* FUNCIONES PARA CARGAR LOS CANTONES Y PARROQUIAS
****************************************************/
$(document).ready(function () {





    $('#ddl_provincia').click(function () {
        
        $('#ddl_ciudad').val('-1');
        $('#ddl_parroquia').val('-1');
        $('#ddl_distritoSiniestro').val('-1');
        $('#ddl_circuito').val('-1');
        $('#ddl_tipo_zona').val('0');

        $('#ddl_ciudad').attr('disabled', 'disabled');
        $('#ddl_parroquia').attr('disabled', 'disabled');
        $('#ddl_distritoSiniestro').attr('disabled', 'disabled');
        $('#ddl_circuito').attr('disabled', 'disabled');
        $('#ddl_tipo_zona').attr('disabled', 'disabled');

        $('#lbcod_circ_1').html('');
        $('#lbdpa_canton').html('');
        $('#lbdpa_parroq').html('');
        $('#lbdpa_provin').html('');
        $('#lbdpa_coddist').html('');
        CantonesPorProvincia();
    });

    $('#ddl_ciudad').click(function () {
        $('#lbcod_circ_1').html('');
        $('#lbdpa_canton').html('');
        $('#lbdpa_parroq').html('');
        $('#lbdpa_provin').html('');
        $('#lbdpa_coddist').html('');
        $('#ddl_distritoSiniestro').attr('disabled', 'disabled');
        $('#ddl_parroquia').attr('disabled', 'disabled');
        $('#ddl_circuito').attr('disabled', 'disabled');
        $('#ddl_tipo_zona').attr('disabled', 'disabled');
        $('#ddl_distritoSiniestro').val('-1');
        $('#ddl_circuito').val('-1');
        $('#ddl_tipo_zona').val('0');
        $('#ddl_parroquia').val('-1');
        ParroquiasPorCantones();
    
    });
  
    $('#ddl_parroquia').click(function () {
        $('#lbcod_circ_1').html('');
       // $('#lbdpa_canton').html('');
        $('#lbdpa_parroq').html('');
       // $('#lbdpa_provin').html('');
        $('#lbdpa_coddist').html('');
        $('#ddl_distritoSiniestro').val('-1');
        $('#ddl_circuito').val('-1');
        $('#ddl_tipo_zona').val('0');
        $('#ddl_distritoSiniestro').attr('disabled', 'disabled');
        $('#ddl_circuito').attr('disabled', 'disabled');
        $('#ddl_tipo_zona').attr('disabled', 'disabled');

        //CargarDistritos();

      
        

    });
    $('#ddl_distritoSiniestro').click(function () {
        $('#lbcod_circ_1').html('');
        $('#lbdpa_canton').html('');
        $('#lbdpa_parroq').html('');
        $('#lbdpa_provin').html('');
        $('#lbdpa_coddist').html('');
        CargaCircuitos();
    });
   

    $('#ddl_tipoVia').change(function () {
        //alert($('#ddl_tipoVia').val().toString());
        if ($('#ddl_tipoVia').val() === '1' || $('#ddl_tipoVia').val() === '2' || $('#ddl_tipoVia').val() === '3') {
            $('#ddl_materialSuperficie').val('2');
        }
        else if (  $('#ddl_tipoVia').val() === '4' ) {
            $('#ddl_materialSuperficie').val('1');
        }
        else if ($('#ddl_tipoVia').val() === '5') {
            $('#ddl_materialSuperficie').val('4');
        }
        else {
            $('#ddl_materialSuperficie').val('0');
        }
    });

    $('#ddl_tipoVia').change(function () {
        //alert($('#ddl_tipoVia').val().toString());
        if ($('#ddl_tipoVia').val() === '1' || $('#ddl_tipoVia').val() === '2') {
            $('#ddl_numCarriles').val('4');
        }
        else if ($('#ddl_tipoVia').val() === '3' || $('#ddl_tipoVia').val() === '4' || $('#ddl_tipoVia').val() === '5') {
            $('#ddl_numCarriles').val('2');
        }
        else if ($('#ddl_tipoVia').val() === '6' || $('#ddl_tipoVia').val() === '7' || $('#ddl_tipoVia').val() === '8') {
            $('#ddl_numCarriles').val('1');
        }
        else {
            $('#ddl_numCarriles').val('-1');
        }
    });


    $("#cb_1S").click(function () {

        $("#cb_2S").prop("checked", false);
        $("#cb_3S").prop("checked", false);
        $("#cb_4S").prop("checked", false);
        $("#cb_5S").prop("checked", false);
        $("#cb_6S").prop("checked", false);
        $("#cb_7S").prop("checked", false);
        $("#cb_8S").prop("checked", false);
        $("#cb_9S").prop("checked", false);
        $("#cb_10S").prop("checked", false);
        $("#cb_11S").prop("checked", false);
        $("#cb_12S").prop("checked", false);
        $("#cb_13S").prop("checked", false);
    });
    $("#cb_2S").click(function () {
        $("#cb_1S").prop("checked", false);
        $("#cb_3S").prop("checked", false);
        $("#cb_4S").prop("checked", false);
        $("#cb_5S").prop("checked", false);
        $("#cb_6S").prop("checked", false);
        $("#cb_7S").prop("checked", false);
        $("#cb_8S").prop("checked", false);
        $("#cb_9S").prop("checked", false);
        $("#cb_10S").prop("checked", false);
        $("#cb_11S").prop("checked", false);
        $("#cb_12S").prop("checked", false);
        $("#cb_13S").prop("checked", false);
    });
    $("#cb_3S").click(function () {
        $("#cb_1S").prop("checked", false);
        $("#cb_2S").prop("checked", false);
        $("#cb_4S").prop("checked", false);
        $("#cb_5S").prop("checked", false);
        $("#cb_6S").prop("checked", false);
        $("#cb_7S").prop("checked", false);
        $("#cb_8S").prop("checked", false);
        $("#cb_9S").prop("checked", false);
        $("#cb_10S").prop("checked", false);
        $("#cb_11S").prop("checked", false);
        $("#cb_12S").prop("checked", false);
        $("#cb_13S").prop("checked", false);
    });
    $("#cb_4S").click(function () {
        $("#cb_1S").prop("checked", false);
        $("#cb_2S").prop("checked", false);
        $("#cb_3S").prop("checked", false);
        $("#cb_5S").prop("checked", false);
        $("#cb_6S").prop("checked", false);
        $("#cb_7S").prop("checked", false);
        $("#cb_8S").prop("checked", false);
        $("#cb_9S").prop("checked", false);
        $("#cb_10S").prop("checked", false);
        $("#cb_11S").prop("checked", false);
        $("#cb_12S").prop("checked", false);
        $("#cb_13S").prop("checked", false);
    });
    $("#cb_5S").click(function () {
        $("#cb_1S").prop("checked", false);
        $("#cb_2S").prop("checked", false);
        $("#cb_3S").prop("checked", false);
        $("#cb_4S").prop("checked", false);
        $("#cb_6S").prop("checked", false);
        $("#cb_7S").prop("checked", false);
        $("#cb_8S").prop("checked", false);
        $("#cb_9S").prop("checked", false);
        $("#cb_10S").prop("checked", false);
        $("#cb_11S").prop("checked", false);
        $("#cb_12S").prop("checked", false);
        $("#cb_13S").prop("checked", false);
    });
    $("#cb_6S").click(function () {
        $("#cb_1S").prop("checked", false);
        $("#cb_2S").prop("checked", false);
        $("#cb_3S").prop("checked", false);
        $("#cb_4S").prop("checked", false);
        $("#cb_5S").prop("checked", false);
        $("#cb_7S").prop("checked", false);
        $("#cb_8S").prop("checked", false);
        $("#cb_9S").prop("checked", false);
        $("#cb_10S").prop("checked", false);
        $("#cb_11S").prop("checked", false);
        $("#cb_12S").prop("checked", false);
        $("#cb_13S").prop("checked", false);
    });
    $("#cb_7S").click(function () {
        $("#cb_1S").prop("checked", false);
        $("#cb_2S").prop("checked", false);
        $("#cb_3S").prop("checked", false);
        $("#cb_4S").prop("checked", false);
        $("#cb_5S").prop("checked", false);
        $("#cb_6S").prop("checked", false);
        $("#cb_8S").prop("checked", false);
        $("#cb_9S").prop("checked", false);
        $("#cb_10S").prop("checked", false);
        $("#cb_11S").prop("checked", false);
        $("#cb_12S").prop("checked", false);
        $("#cb_13S").prop("checked", false);
    });
    $("#cb_8S").click(function () {
        $("#cb_1S").prop("checked", false);
        $("#cb_2S").prop("checked", false);
        $("#cb_3S").prop("checked", false);
        $("#cb_4S").prop("checked", false);
        $("#cb_5S").prop("checked", false);
        $("#cb_6S").prop("checked", false);
        $("#cb_7S").prop("checked", false);
        $("#cb_9S").prop("checked", false);
        $("#cb_10S").prop("checked", false);
        $("#cb_11S").prop("checked", false);
        $("#cb_12S").prop("checked", false);
        $("#cb_13S").prop("checked", false);
    });
    $("#cb_9").click(function () {
        $("#cb_1S").prop("checked", false);
        $("#cb_2S").prop("checked", false);
        $("#cb_3S").prop("checked", false);
        $("#cb_4S").prop("checked", false);
        $("#cb_5S").prop("checked", false);
        $("#cb_6S").prop("checked", false);
        $("#cb_8S").prop("checked", false);
        $("#cb_7S").prop("checked", false);
        $("#cb_10S").prop("checked", false);
        $("#cb_11S").prop("checked", false);
        $("#cb_12S").prop("checked", false);
        $("#cb_13S").prop("checked", false);
    });
    $("#cb_10S").click(function () {
        $("#cb_1S").prop("checked", false);
        $("#cb_2S").prop("checked", false);
        $("#cb_3S").prop("checked", false);
        $("#cb_4S").prop("checked", false);
        $("#cb_5S").prop("checked", false);
        $("#cb_6S").prop("checked", false);
        $("#cb_7S").prop("checked", false);
        $("#cb_8S").prop("checked", false);
        $("#cb_9S").prop("checked", false);
        $("#cb_11S").prop("checked", false);
        $("#cb_12S").prop("checked", false);
        $("#cb_13S").prop("checked", false);
    });
    $("#cb_11S").click(function () {
        $("#cb_1S").prop("checked", false);
        $("#cb_2S").prop("checked", false);
        $("#cb_3S").prop("checked", false);
        $("#cb_4S").prop("checked", false);
        $("#cb_5S").prop("checked", false);
        $("#cb_6S").prop("checked", false);
        $("#cb_7S").prop("checked", false);
        $("#cb_8S").prop("checked", false);
        $("#cb_9S").prop("checked", false);
        $("#cb_10S").prop("checked", false);
        $("#cb_12S").prop("checked", false);
        $("#cb_13S").prop("checked", false);
    });

    $("#cb_12S").click(function () {
        $("#cb_1S").prop("checked", false);
        $("#cb_2S").prop("checked", false);
        $("#cb_3S").prop("checked", false);
        $("#cb_4S").prop("checked", false);
        $("#cb_5S").prop("checked", false);
        $("#cb_6S").prop("checked", false);
        $("#cb_7S").prop("checked", false);
        $("#cb_8S").prop("checked", false);
        $("#cb_9S").prop("checked", false);
        $("#cb_10S").prop("checked", false);
        $("#cb_11S").prop("checked", false);
        $("#cb_13S").prop("checked", false);
    });
    $("#cb_13S").click(function () {
        $("#cb_1S").prop("checked", false);
        $("#cb_2S").prop("checked", false);
        $("#cb_3S").prop("checked", false);
        $("#cb_4S").prop("checked", false);
        $("#cb_5S").prop("checked", false);
        $("#cb_6S").prop("checked", false);
        $("#cb_7S").prop("checked", false);
        $("#cb_8S").prop("checked", false);
        $("#cb_9S").prop("checked", false);
        $("#cb_10S").prop("checked", false);
        $("#cb_11S").prop("checked", false);
        $("#cb_12S").prop("checked", false);
    });

   
   
    

});


$('#btnTraerInfoCoor').click(function () { location.reload();});

$('#btnCargarDireccionesMapa').click(function () {
    cargarDireccionesMapaConfirmar();
    cargando();

});

$('#ddl_lugarVia').change(function () {
    $("#ddl_senializacionExistente").val('0');
    if ($('#ddl_lugarVia option:selected').text() === 'INTERSECCIÓN EN T' || $('#ddl_lugarVia option:selected').text() === 'INTERSECCIÓN EN CRUZ' || $('#ddl_lugarVia option:selected').text() === 'INTERSECCIÓN EN Y') {
        $("#ddl_controlInterseccion").removeAttr('disabled');
        $("#ddl_controlInterseccion").val('0');
        $("#ddl_curvaExistente").val('4');
        $("#ddl_curvaExistente").attr('disabled', 'disabled');
    }
    else if ($('#ddl_lugarVia option:selected').text() === 'CURVA') {
        $("#ddl_curvaExistente").val('-1');
        $("#ddl_curvaExistente").removeAttr('disabled');
        $("#ddl_controlInterseccion").attr('disabled', 'disabled');
        $("#ddl_controlInterseccion").val('8');
    }
    else {
        $("#ddl_controlInterseccion").attr('disabled', 'disabled');
        $("#ddl_controlInterseccion").val('8');
        $("#ddl_curvaExistente").val('3');
        $("#ddl_curvaExistente").attr('disabled', 'disabled');
    }
    
});


$('#ddl_controlInterseccion').change(function () {
    if ($('#ddl_controlInterseccion option:selected').text() === 'SEÑALIZACIÓN HORIZONTAL') {
        $("#ddl_senializacionExistente").val('1');
    }
    else if ($('#ddl_controlInterseccion option:selected').text() === 'SEÑALIZACIÓN VERTICAL') {
        $("#ddl_senializacionExistente").val('2');
    }
    else if ($('#ddl_controlInterseccion option:selected').text() === 'NINGUNA') {
        $("#ddl_senializacionExistente").val('4');
    }
    else { $("#ddl_senializacionExistente").val('0'); }
});

$('#ddl_vehiculo_identificado').change(function () {
    if ($('#ddl_vehiculo_identificado option:selected').text() === 'SI')
    {
        $('#divTipoVhl').css('display', 'block');
        $('#divSubTipoIdent').css('display', 'block');
        $('#divPlaca').css('display', 'block');
        $('#divbotonBusuqeda').css('display', 'block');
        $('#divDatosChasis').css('display', 'block');
        $('#divTranspMaterialPeligroso').css('display', 'block');
        $('#divdaniomaterial').css('display', 'block');
        $('#divInsertarVhl').css('display', 'block');
        $('#ddl_vhl_materialPeligroso').removeAttr('disabled');
        $('#ddl_tipo_servicio_vhl').removeAttr('disabled');
        $('#ddl_seguro_privado_vhl').removeAttr('disabled');
        $('#ddl_danio_material').removeAttr('disabled');
        $('#ddl_matricula_vigente').removeAttr('disabled');
        $('#tb_placvehinv').removeAttr('disabled');
        $('#tb_chavehinv').removeAttr('disabled');
        $('#tb_marvehinv').removeAttr('disabled');
        $('#tb_modvehinv').removeAttr('disabled');
        $('#tb_anivehinv').removeAttr('disabled');
        $('#tb_cilvehinv').removeAttr('disabled');
        $('#tb_placvehinv').val('');
        $('#tb_chavehinv').val('');
        $('#tb_marvehinv').val('');
        $('#tb_modvehinv').val('');
        $('#tb_anivehinv').val('');
        $('#tb_cilvehinv').val('');
        $('#ddl_vhl_materialPeligroso').val('0');
        $('#ddl_tipo_servicio_vhl').val('-1');
        $('#ddl_seguro_privado_vhl').val('-1');
        $('#ddl_danio_material').val('-1');
        $('#ddl_matricula_vigente').val('-1');
        $('#ddl_tipo_vhl').val('-1');
        $('#ddl_subtipo_vhl').val('-1');
    }
    else if ($('#ddl_vehiculo_identificado option:selected').text() === 'NO')
    {

        $('#divTipoVhl').css('display', 'block');
        $('#divSubTipoIdent').css('display', 'block');
        $('#divPlaca').css('display', 'block');
        $('#divbotonBusuqeda').css('display', 'block');
        $('#divDatosChasis').css('display', 'block');
        $('#divTranspMaterialPeligroso').css('display', 'block');
        $('#divdaniomaterial').css('display', 'block');
        $('#divInsertarVhl').css('display', 'block');
        $('#ddl_tipo_vhl').val('-1');
        $('#ddl_subtipo_vhl').val('-1');
        $('#tb_placvehinv').attr('disabled', 'disabled')
        $('#tb_chavehinv').attr('disabled', 'disabled')
        $('#tb_marvehinv').attr('disabled', 'disabled')
        $('#tb_modvehinv').attr('disabled', 'disabled')
        $('#tb_anivehinv').attr('disabled', 'disabled')
        $('#tb_cilvehinv').attr('disabled', 'disabled')
        $('#ddl_vhl_materialPeligroso').attr('disabled', 'disabled')
        $('#ddl_tipo_servicio_vhl').attr('disabled', 'disabled')
        $('#ddl_seguro_privado_vhl').attr('disabled', 'disabled')
        $('#ddl_danio_material').removeAttr('disabled')
        $('#ddl_matricula_vigente').attr('disabled', 'disabled')
       
        $('#tb_placvehinv').val('NI');
        $('#tb_chavehinv').val('NI');
        $('#tb_marvehinv').val('NI');
        $('#tb_modvehinv').val('NI');
        $('#tb_anivehinv').val('0');
        $('#tb_cilvehinv').val('0');
        $('#ddl_vhl_materialPeligroso').val('0');
        $('#ddl_tipo_servicio_vhl').val('3');
        $('#ddl_seguro_privado_vhl').val('2');
        $('#ddl_danio_material').val('-1');
        $('#ddl_matricula_vigente').val('2');
    }
    else
    {
        $('#divTipoVhl').css('display', 'none');
        $('#divSubTipoIdent').css('display', 'none');
        $('#divPlaca').css('display', 'none');
        $('#divbotonBusuqeda').css('display', 'none');
        $('#divDatosChasis').css('display', 'none');
        $('#divTranspMaterialPeligroso').css('display', 'none');
        $('#divdaniomaterial').css('display', 'none');
        $('#divInsertarVhl').css('display', 'none');
        $('#divTipoVhl').val('-1');
        $('#ddl_subtipo_vhl').val('-1');
    }
});

$('#btnSiguienteVDanioTercero').click(function () {

    if ($('#dt_victimasSiniestro >tbody >tr').length == 0) {
        alert('Ingrese al menos una víctima para continuar con el proceso');
    }
    else {
        ($('#panelMensaje').css("display", "none"));
        ($('#divVerDaniostercerosSiniestros').css("display", "none"));
        //($('#tb_codsinDanioTercero').val('0'));
        ($('#tb_obsdater').val(''));
        ($('#tb_coddater').val('-1'));
        ($('#ddl_tipoDanioTercero').val('-1'));
        var codsinDanioTercero = ($('#lbCodigoSiniestroGuardadoOculto').html());
        ($('#tb_codsinDanioTercero').val(codsinDanioTercero.toString()));
        $('#tb_codsinDanioTercero').attr("disabled", "disabled");

        
        $("#aliDanios").css("display", "block");
       
        $('#myTab li:eq("3") a').tab('show');
        $("#lidanioprog").addClass("completed", "completed");

      //  $('#respuesta-ej6S').toggle();
        $('#respuesta-ej5S').toggle();
    }
    

    

});

$('#btnSiguienteFinalizarProceso').click(function () {
    var codsiniestro = $('#lbCodigoSiniestroGuardadoOculto').html();


    if ($('#dt_DaniosTercerosSiniestros >tbody >tr').length == 0) {
        alert('Ingrese al menos una daño a tercero para continuar con el proceso');
    }
    else if (confirm("Va  a proceder a finalizar el proceso del creación del siniestro #" + codsiniestro + ".  Desea continuar? "))
    {
        tarerInformacionSiniestroProcersoSin(codsiniestro);
        $("#alifinProceso").css("display", "block");
        
        $('#myTab li:eq("4") a').tab('show');
        $("#lifinaprog").addClass("completed", "completed");
        $("#livictimas").addClass("disabled", "disabled");
        $("#liVehiculos").addClass("disabled", "disabled");
        $("#liDanios").addClass("disabled", "disabled");

    }
        



});

$('#btnFinalizarProcesoSiniestro').click(function () {

    var codsiniestro = $('#lbCodigoSiniestroGuardadoOculto').html();
    if (confirm("Va  a proceder a cerrar el proceso del siniestro #"+codsiniestro+" . Desea continuar? ")) {

        //JsonModificarSiniestroFinProceso
        $.ajax({
            type: "get",
            url: 'JsonModificarSiniestroFinProceso',
            data: { codsin: codsiniestro },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (jsonmensaje) {
                if (jsonmensaje !== '0') {
                    $('#mensajeLabelFinalizar').html("Siniestro #" + codsiniestro + " generado correctamente, presione sobre el botón Ingresar nuevo siniestro si desea registrar nuevos casos.");
                    $('#panelMensajeFinalizar').css('display', 'block')
                    $('#btnFinalizarProcesoSiniestro').css('display', 'none')
                    $('#btnNuevoSiniestro').css('display', 'block')
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(textStatus + ": " + XMLHttpRequest.responseText);
            }

        });
       
    } else {
        $('#panelMensajeFinalizar').css('display', 'none')
        
    }


});


function tarerInformacionSiniestroProcersoSin(codsiniestro)
{
    $("#dt_siniestros_procesos").find("tr:gt(0)").remove();
    $.ajax({
        type: "get",
        url: 'ObtenertarerInformacionSiniestroProcersosSin',
        data: { codsin: codsiniestro },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (jsonResult) {
            if (jsonResult.length > 0) {
                
                $.each(jsonResult, function (key, value) {
                    /* Vamos agregando a nuestra tabla las filas necesarias */
                    $("#dt_siniestros_procesos").append("<tr><td>" + value.codsin + "</td><td>" + value.fecsin + "</td><td>" + value.numfalsin + "</td><td>" + value.numlessin + "</td><td>" + value.nomprov + "</td><td>" + value.descant + "</td><td>" + value.zonsin + "</td><td class=\"ajustar\">" + value.dirsin + "</td></tr>");
                });
               
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(textStatus + ": " + XMLHttpRequest.responseText);
        }

    });
}

function traerDatosDireccionGeo() {
    var codcir = $('#lbcod_circ_1').html();
    var codcant = $('#lbdpa_canton').html();
    var codpar = $('#lbdpa_parroq').html();
    var codprov = $('#lbdpa_provin').html();
    var codistri = $('#lbdpa_coddist').html();
    if (codcir !== '') {

        $('#ddl_provincia').val(codprov);
        if ($('#ddl_provincia').val() !== '-1') {
            CantonesPorProvincia();
            console.log('imprime '+''+ codpar.toString().trim());
            $('#ddl_parroquia').val(codpar.toString().trim());
            $('#ddl_circuito').val(codcir.toString().trim());
            $('#ddl_distritoSiniestro').val(codistri.toString().trim());
            $('#ddl_provincia').removeAttr("disabled");

            // ParroquiasPorCantones();
        }
        //if ($('#ddl_ciudad').val !== '-1') {
        //    CargaCircuitos();
        //

        // $('#ddl_ciudad').val('1701');
    }
};




function CantonesPorProvincia() {
    var codprov = $('#ddl_provincia').val();
    var params = new Object();
    params.codprov = $('#ddl_provincia').val();
    params = JSON.stringify(params);
    var select = $('#ddl_ciudad');
    var codcant = $('#lbdpa_canton').html();
    select.find('option').remove().end().append('<option value="-1">SELECCIONAR</option>').val('-1');
    $.ajax({
        type: "get",
        url: 'CargarCantonesPorProvincia',
        data: { codprov: codprov },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (jsonResult) {
            console.log(jsonResult);
            $.each(jsonResult, function (key, value) {
                select.append('<option value="' + value.value + '">' + value.text + '</option>').val(value.value);
                $("#ddl_ciudad").removeAttr('disabled');
            });
            
            
            if (codcant !== '')
            {
                select.val(codcant.toString().trim());
            }
            else
            {
                select.val('-1');
               
            }
            
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert('Vuelva a seleccionar la provincia');
        }

    });

};

function ParroquiasPorCantones()
{
    var codprov = $('#ddl_provincia').val();
    var codcant = $('#ddl_ciudad').val();
    var codparoquia = $('#lbdpa_parroq').html();
    var params = new Object();
    params.codprov = $('#ddl_provincia').val();
    params.codcant = $('#ddl_ciudad').val();
    params = JSON.stringify(params);
    var select = $('#ddl_parroquia');
    select.find('option').remove().end().append('<option value="-1">SELECCIONAR</option>').val('-1');
    $.ajax({
        type: "get",
        url: 'CargarParroquiasPorCantones',
        data: { codcant: codcant, codprov: codprov },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (jsonResult) {
            console.log(jsonResult);
            $.each(jsonResult, function (key, value) {
                //var nombre = value.text.split('-');
                select.append('<option value="' + value.value + '">' + value.text + '</option>').val(value.value);
                $("#ddl_parroquia").removeAttr('disabled');
               
            });
            if (codparoquia !== '') {
                select.val(codparoquia);
            }
            else {
                select.val('-1');
            }
            
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert('Vuelva a seleccionar el cantón');
        }

    });
}
//


function CargarDistritos ()
{
    var codprov = $('#ddl_provincia').val();
    var codcant = $('#ddl_ciudad').val();
    var codpar = $('#ddl_parroquia').val();
    var coddistrito = $('#lbdpa_coddist').html();
    var params = new Object();
    
    params.codprov = $('#ddl_provincia').val();
    params.codcant = $('#ddl_ciudad').val();
    params = JSON.stringify(params);

    var selectD = $('#ddl_distritoSiniestro');
    //ddl_distritoSiniestro
    //selectD.find('option').remove().end().append('<option value="-1">SELECCIONAR</option>').val('-1');
    $.ajax({
        type: "get",
        url: 'ObtenerlistaDistritos',
        data: { codProv: codprov, codcant: codcant, codpar: codpar },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (jsonResult) {
            console.log(jsonResult);
            $.each(jsonResult, function (key, value) {
               // selectD.append('<option value="' + value.value + '">' + value.text + '</option>').val(value.value);
                $("#ddl_distritoSiniestro").val(value.zona.trim());
                $("#ddl_circuito").val(value.codigo.trim());
                console.log(value.nombre);
                //$('#ddl_tipo_zona option:selected').text("URBANA");
                $('#ddl_tipo_zona option:selected').text(value.nombre);
              
            });
            if (coddistrito !== '') {
                selectD.val(coddistrito);
            }
           
            
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }

    });

    //var zona_desc = new Object();
    //zona_desc = $('#ddl_parroquia option:selected').text().split('-')[1];
    //console.log(zona_desc.toString().trim().replace('-', ''));
    ////console.log(zona_desc);
   
    //if (zona_desc.toString().trim().replace('-', '').toString() === "RURAL")
    //{
    //    $('#ddl_tipo_zona option:selected').text("RURAL");
    //}
    //else if (zona_desc.toString().trim().replace('-', '').toString() === "URBANA")
    //{
    //    $('#ddl_tipo_zona option:selected').text("URBANA");
    //}
    //else (zona_desc.toString().trim().replace('-', '').toString() === "")
    //{
    //   ( $('#ddl_tipo_zona').val("0"));
    //}
    //$('#ddl_tipo_zona').removeAttr('disabled');
    
};

function CargaCircuitos1() {
    var codpar = $('#ddl_parroquia').val();
    var codcant = $('#ddl_ciudad').val();
    var codcircuito = $('#lbcod_circ_1').html();
    var params = new Object();
    params.codpar = $('#ddl_parroquia').val();
    params.codcant = $('#ddl_ciudad').val();
    params = JSON.stringify(params);

    var selectD = $('#ddl_circuito');
    //ddl_distritoSiniestro
    selectD.find('option').remove().end().append('<option value="-1">SELECCIONAR</option>').val('-1');
    $.ajax({
        type: "get",
        url: 'ObtenerlistaCircuitos',
        data: { codCant: codcant, codPar: codpar },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (jsonResult) {
            console.log(jsonResult);
            $.each(jsonResult, function (key, value) {
                selectD.append('<option value="' + value.value + '">' + value.text + '</option>').val(value.value);
                $("#ddl_circuito").removeAttr('disabled');
            });
            if (codcircuito !== '') {
                selectD.val(codcircuito);
            }
            else {
                selectD.val('-1');
            }

        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }

    });
};
function CargaCircuitos() {
    var codpar = $('#ddl_parroquia').val();
    var codcant = $('#ddl_ciudad').val();
    var codcircuito = $('#lbcod_circ_1').html();
    var params = new Object();
    params.codpar = $('#ddl_parroquia').val();
    params.codcant = $('#ddl_ciudad').val();
    params = JSON.stringify(params);

    var selectD = $('#ddl_circuito');
    //ddl_distritoSiniestro
    selectD.find('option').remove().end().append('<option value="-1">SELECCIONAR</option>').val('-1');
    $.ajax({
        type: "get",
        url: 'ObtenerlistaCircuitos',
        data: { codCant: codcant, codPar: codpar },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (jsonResult) {
            console.log(jsonResult);
            $.each(jsonResult, function (key, value) {
                selectD.append('<option value="' + value.value + '">' + value.text + '</option>').val(value.value);
                $("#ddl_circuito").removeAttr('disabled');
               // if (value.value !== '-1')
                 selectD.val(value.value);
            });
            if (codcircuito !== '') {
                selectD.val(codcircuito);
            }
            //else {
            //    selectD.val('-1');
            //}
            
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
        }

    });
};

function NumCheckEdad(e, field) {
    key = e.keyCode ? e.keyCode : e.which
    // backspace
    if (key == 8) return true
    if (key == 9) return true
    if (key == 45) return true
    if (key == 16) return true
    if (key > 47 && key < 58) return true
    return false

};

function NumCheck(e, field) {
    key = e.keyCode ? e.keyCode : e.which
    // backspace
    if (key == 8) return true
    if (key == 9) return true
    if (key == 45) return true
    if (key == 16) return true
    if (key > 47 && key < 58) return true
    // 0-9

    // .
    if (key == 46) return true


    // other key
    return false

};
function aMayusculas(obj, id) {
    obj = obj.toUpperCase();
    document.getElementById(id).value = obj;
};
function ObtenerTraerNumLesionadosFallecido(_codsin) {
    console.log('entro funcion' + ' ' + _codsin.toString());
    var codsin = _codsin;
    var params = new Object();
    params.codsin = _codsin;
    $.ajax({
        type: "get",
        url: '../ObtenerTraerNumLesionadosFallecidos',
        data: { codsin: _codsin },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (jsonResult) {
            console.log('ObtenerTraerNumLesionadosFallecido' + ' ' + jsonResult);
            
                //select.append('<option value="' + value.value + '">' + value.text + '</option>').val(value.value);
                console.log(value.value.toString());
                ($('#tbnumlessin').val(value.value.toString()));
                ($('#tbnumfalsin').val(value.text.toString()));
                
            },
       
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(textStatus + ": " + XMLHttpRequest.responseText);
        }

    });


   
};

function uploadCustomerForm()
{
    var _id = "0";
          $.ajax( {
                 type: 'POST',
                 data: "{'id':" + "'" + _id + "'}", //datos o parametros enviados al servidor
                 dataType: 'html', //el tipo de dato que nos regresara el servidor en este caso regresa html
                 url: 'CargaDatosView',
                 //URL del action result que cargara la vista parcial
                 success: function (result) {
                     // si la funcion se ejecuta lanzara un alert
                     alert('Success');
                     //cuando se ejecuta bien la funcion agregara al div vistaParcial el contenido
                     //que recibio del servidor
                    // $("#pvistaParcial").html(result);
                 },

                 error: function (error) {
                     // si hay un error lanzara el mensaje de error
                     alert(error);
                 }
             });
    //Enviar por post
  //  $.post('<% =Url.Action("CargaDatosView") %>');
}


function marcar(obj) {
    obj.style.background = (obj.style.background === '') ? 'silver' : '';
   
};

function valida_hora(valor) {
    //que no existan elementos sin escribir
    if (valor.indexOf(":") !== -1) {
        var hora = valor.split(":")[0];

        if (parseInt(hora) > 24) {
            $("#tbHoraSiniestro").val("");
            ($('#ddl_luzArtificial').attr("disabled", "disabled"));
        }
        if ($("#tbHoraSiniestro").val().trim() === "__:__" || $("#tbHoraSiniestro").val().trim() === "")
        {
            ($('#ddl_luzArtificial').attr("disabled", "disabled"));
        }
        if (parseInt(hora) === 18 || parseInt(hora) === 19 || parseInt(hora) === 20 || parseInt(hora) === 21 || parseInt(hora) === 22 || parseInt(hora) === 23 || parseInt(hora) === 24 || hora.toString() === '01' || (hora.toString()) === '02' || (hora.toString()) === '03' || (hora.toString()) === '04' || (hora.toString()) === '05' || (hora.toString()) === '06') {
            ($('#ddl_luzArtificial').removeAttr("disabled"));
            
        }
        else {
            ($('#ddl_luzArtificial').attr("disabled", "disabled"));
        }
        //end if
    }//end if
};  //end function 




function VerMapa() {
    var map, gsvc, pt;

    require([
      "esri/map", "esri/graphic", "esri/symbols/SimpleMarkerSymbol", "esri/tasks/GeometryService", "esri/tasks/ProjectParameters",
      "esri/SpatialReference", "esri/InfoTemplate", "dojo/dom", "dojo/on", "esri/geometry/webMercatorUtils",
      "esri/symbols/PictureMarkerSymbol", "dojo/dom-attr", "esri/geometry/Point",
      "dojo/domReady!"
    ], function (
      Map, Graphic, SimpleMarkerSymbol, GeometryService, ProjectParameters,
      SpatialReference, InfoTemplate, dom, on, webMercatorUtils, PictureMarkerSymbol, domAttr, Point
    ) {
        var spatialReferenceMain = new SpatialReference({ "wkid": 4326 });
        map = new Map("map", {
            basemap: "osm",
            center: [-78.5320102361967, -0.77685],
            zoom: 7
        });

        map.on("click", Coordenadas);

        function Coordenadas(evt) {
            map.graphics.clear();

            var point = evt.mapPoint;

            var symbol = new PictureMarkerSymbol({
                "url": "http://static.arcgis.com/images/Symbols/Basic/YellowStickpin.png",
                "height": 25,
                "width": 25,
                "type": "esriPMS"
            });
            var graphic = new Graphic(point, symbol);

            map.graphics.add(graphic);
            var mp = webMercatorUtils.webMercatorToGeographic(evt.mapPoint);
            domAttr.set(dom.byId("lat"), "value", mp.y.toFixed(8)); // set
            domAttr.set(dom.byId("lng"), "value", mp.x.toFixed(8)); // set*/

            domAttr.set(dom.byId("tbLatitudSiniestro"), "value", mp.y.toFixed(8)); // set
            domAttr.set(dom.byId("tbLonguitudSiniestro"), "value", mp.x.toFixed(8)); // set*/

        }
        document.getElementById("acercarBtn").addEventListener("click", function () {

            var lat = domAttr.get("lat", "value");
            var lng = domAttr.get("lng", "value");
            
            //$('#tbLonguitudSiniestro').val(lng);
            //$('#tbLatitudSiniestro').val(lat);
            acercar(lat, lng);
        });
        function acercar(lat, lng) {
            var point = new Point(lng, lat, map.spatialReference.wkid);
            var projectPoint = webMercatorUtils.geographicToWebMercator(point);
            map.centerAndZoom(projectPoint, 15);

        }
    });


};
function InicializaVehiculos() {
    var selectsv = $('#ddl_subtipo_vhl');
    selectsv.find('option').remove().end().append('<option value="-1">SELECCIONAR</option>').val('-1');
    var codsinVHL1 = ($('#lbCodigoSiniestroGuardadoOculto').html());
    ($('#tb_codsinVHL').val(codsinVHL1.toString()));
    $('#tb_codsinVHL').attr("disabled", "disabled");
    ($('#tb_placvehinv').val(''));
    ($('#tb_chavehinv').val(''));
    ($('#tb_marvehinv').val(''));
    ($('#tb_modvehinv').val(''));
    ($('#tb_anivehinv').val(''));
    ($('#tb_cilvehinv').val(''));
    ($('#tb_matpelvehinv').val('-1'));
    ($('#ddl_vhl_materialPeligroso').val('-1'));
    ($('#tb_matpelvehinv').val('-1'));
    ($('#ddl_tipo_servicio_vhl').val('-1'));
    $('#ddl_subtipo_vhl').val('-1');
    ($('#tb_codser').val('-1'));
    ($('#ddl_tipo_vhl').val('-1'));
    ($('#tb_codtipve').val('-1'));
    //($('#panelMensajeVHL').css("display", "none"));
    $("#cb_segprivehinv").prop("checked", false);
    $("#cb_danmatvehinv").prop("checked", true);
    $('#cb_matvigvehinv').prop("checked", false);
    $("#ddl_vhl_materialPeligroso").val('0');
    //$("#divVehiculosInvolucradosSiniestro").css("display", "none");
   
};

function InicializaVictimas() {
    var codsinVictimas = ($('#lbCodigoSiniestroGuardadoOculto').html());
    ($('#tb_codsinVictimas').val(codsinVictimas.toString()));
    $('#tb_codsinVictimas').attr("disabled", "disabled");
    // ($('#btnBuscarVict').css("display", "none"));
    //($('#tb_codsinVictimas').val('0'));
    ($('#ddl_tipoIdentificacion').val('-1'));
    ($('#tb_IdentificacionVictima').val(''));
    ($('#tb_NombresVictima').val(''));
    ($('#tb_edadVictima').val('0'));
    ($('#ddl_generoVictima').val('-1'));
    ($('#ddl_condicionVictimas24').val('-1'));
    ($('#ddl_condicionVictimas30').val('4'));
    ($('#ddl_tipoParticipante').val('-1'));
    //($('#ddl_posicionPlaza').val('-1'));
    ($('#panelMensajeVictimas').css("display", "none"));
    $("#cb_usoCascoVictima").prop("checked", false);
    $("#cb_usoCinturonVictima").prop("checked", false);
    $('#cb_consumoAlcoholVictima').prop("checked", false);
    $('#ddl_accionesPeaton').css("display", "none");
    $('#divAccionesPeaton').css("display", "none");
    $('#divAccionesAgregar').css("display", "none");
    ($('#divPanelelGridAccionesPeaton').css("display", "none"));
    ($('#divVerVictimasSiniestro').css("display", "none"));
    ($('#ddl_uso_casco').removeAttr('disabled', 'disabled'));
    ($('#ddl_uso_cinturon').removeAttr('disabled', 'disabled'));
    ($('#ddl_consumo_alcohol').removeAttr('disabled', 'disabled'));
    ($('#ddl_uso_casco').val('-1'));
    ($('#ddl_uso_cinturon').val('-1'));
    ($('#ddl_consumo_alcohol').val('-1'));
    $("#cb_1S").prop("checked", false);
    $("#cb_2S").prop("checked", false);
    $("#cb_3S").prop("checked", false);
    $("#cb_4S").prop("checked", false);
    $("#cb_5S").prop("checked", false);
    $("#cb_6S").prop("checked", false);
    $("#cb_7S").prop("checked", false);
    $("#cb_8S").prop("checked", false);
    $("#cb_9S").prop("checked", false);
    $("#cb_10S").prop("checked", false);
    $("#cb_11S").prop("checked", false);
    $("#cb_12S").prop("checked", false);
    $("#cb_13S").prop("checked", false);
    codsinVictimas = ($('#tb_codsinVictimas').val());
    var select = $('#ddl_vhlVictimas');
    select.find('option').remove().end().append('<option value="-1">SELECCIONAR</option>').val('-1');
    $.ajax({
        type: "get",
        url: 'traerDatosVehiculosInvolucradosVictimas',
        data: { codsin: codsinVictimas },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (jsonResult) {
            console.log(jsonResult);
            $.each(jsonResult, function (key, value) {
                select.append('<option value="' + value.value + '">' + value.text + '</option>').val(value.value);
            });
            select.val('-1');
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert('Error con la conexón a la base de datos.');
        }

    });
    $('#divTipoVhl').css('display', 'none');
    $('#divSubTipoIdent').css('display', 'none');
    $('#divPlaca').css('display', 'none');
    $('#divbotonBusuqeda').css('display', 'none');
    $('#divDatosChasis').css('display', 'none');
    $('#divTranspMaterialPeligroso').css('display', 'none');
    $('#divdaniomaterial').css('display', 'none');
    
    $('#divInsertarVhl').css('display', 'none');
    $('#ddl_vehiculo_identificado').val('-1')

};


function inicializaDaniosTerceros() {
    ($('#panelMensaje').css("display", "none"));

    //($('#tb_codsinDanioTercero').val('0'));
    ($('#tb_obsdater').val(' '));
    ($('#tb_coddater').val('-1'));
    ($('#ddl_tipoDanioTercero').val('-1'));
    var codsinDanioTercero = ($('#lbCodigoSiniestroGuardadoOculto').html());
    ($('#tb_codsinDanioTercero').val(codsinDanioTercero.toString()));
    $('#tb_codsinDanioTercero').attr("disabled", "disabled");

    $('#respuesta-ej5S').toggle();


};


function cargarDireccionesMapaConfirmar() {

    //alert('xy');
    //uploadCustomerForm();
    if ($('#tbLatitudSiniestro').val() !== '' && $('#tbLonguitudSiniestro').val() !== '') {
        var x = $('#tbLatitudSiniestro').val();
        var y = $('#tbLonguitudSiniestro').val();

        var punto1 = "{\"displayFieldName\":\"\",\"fields\":[{\"name\":\"objectid\",\"type\":\"esriFieldTypeOID\",\"alias\":\"OBJECTID\"},{\"name\":\"x\",\"type\":\"esriFieldTypeDouble\",\"alias\":\"x\"},{\"name\":\"y\",\"type\":\"esriFieldTypeDouble\",\"alias\":\"y\"}],\"features\":[{\"attributes\":{\"objectid\":1,\"x\":" + y + ",\"y\":" + x + "}}],\"exceededTransferLimit\":false}";
        console.log('impremime paramtreo envio');
        console.log(punto1);
        $.ajax({
            type: "GET",
            url: "http://geoestadisticas.ant.gob.ec:6080/arcgis/rest/services/GeoProcesos/GetDivisionPoliticaGP/GPServer/GetDivisionPolitica/execute",
            data: {
                punto: punto1,
                returnZ: "false",
                returnM: "false",
                f: "pjson"
            },
            //   contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                //JSON.stringify(data);
                JSON.stringify(data);
                //  console.log(response[0].features);
                $.each(data, function (i, item) {
                    console.log(data);
                    if (item[0].value.features[0].attributes.DPA_CANTON !== "") {
                        var datocant = item[0].value.features[0].attributes.DPA_CANTON;
                        console.log(datocant);
                        console.log('pasa a item data');
                        console.log("cod_circ_1" + " " + item[0].value.features[0].attributes.COD_CIRC_1);
                        console.log("dpa_canton" + " " + parseInt(datocant));
                        console.log("dpa_parroq" + " " + parseInt(item[0].value.features[0].attributes.DPA_PARROQ));
                        console.log("dpa_provin" + " " + parseInt(item[0].value.features[0].attributes.DPA_PROVIN));
                        console.log("COD_DISTRI" + " " + item[0].value.features[0].attributes.COD_DISTRI);
                        //$('#ddl_parroquia').val(item[0].value.features[0].attributes.DPA_PARROQ);
                        $('#lbcod_circ_1').html(item[0].value.features[0].attributes.COD_CIRC_1);
                        $('#lbdpa_canton').html(parseInt(datocant));
                        $('#lbdpa_parroq').html(parseInt(item[0].value.features[0].attributes.DPA_PARROQ.toString()));
                        $('#lbdpa_provin').html(parseInt(item[0].value.features[0].attributes.DPA_PROVIN.toString()));
                        $('#lbdpa_coddist').html(item[0].value.features[0].attributes.COD_DISTRI.toString());
                        if ($('#lbcod_circ_1').html() !== '') {
                            traerDatosDireccionGeo();
                        };
                    }



                });
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
               alert("No se puede conectar con el servidor de georeferencias,Intente nuevamente..!!")
                    $('#ddl_provincia').removeAttr("disabled");
            }

        });

        //promise.then(function () {
        //    //
        //    ParroquiasPorCantones();
        //    //
        //});

    }
  
};




function traerVehiculosInvolucradosSin() {
    codsinVictimas = ($('#tb_codsinVictimas').val());
    console.log(codsinVictimas.toString());
    var select = $('#ddl_vhlVictimas');
    select.find('option').remove().end().append('<option value="-1">SELECCIONAR</option>').val('-1');
    $.ajax({
        type: "get",
        url: 'traerDatosVehiculosInvolucradosVictimas',
        data: { codsin: codsinVictimas },
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (jsonResult) {
            console.log(jsonResult);
            $.each(jsonResult, function (key, value) {
                select.append('<option value="' + value.value + '">' + value.text + '</option>').val(value.value);
            });
            select.val('-1');
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(textStatus + ": " + XMLHttpRequest.responseText);
        }

    });
};

function cargando() {
    $('#fotocargandoSin').css('display', 'block');
    $('#fotocargandoSin').hide();
};

//$('#btnBuscarVict1').click(function () {
    
//    fillData();
//    });

function fillData() {
    $.ajax({
        type: "Post",
        url: "../myService.asmx/getStudent",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (msg) {
            //var nMsg = (typeof msg.d) == 'string' ? eval('(' + msg.d + ')') : msg.d;
            var t = "<table width='80%' id='resTab'> <tr>" +
                  "<td colspan='5' style='text-align:center'><font size='3'><strong>Your Search Result......</strong></font></td></tr> <tr><td style='text-align:left' colspan='5'><hr></td></tr> "
                  + " <tr><td style='text-align:center'>Student ID</td><td style='text-align:center'>Student Name</td><td style='text-align:center'>Student Course</td><td style='text-align:center'>Student USN</td></tr>"
                  + " <tr><td style='text-align:left' colspan='5'><hr><br></td></tr> ";
            $.each(msg.d, function (index, item) {
                t = t + " <tr><td style='text-align:center'>" + item.studId + "</td><td style='text-align:center'>" + item.studName + "</td><td style='text-align:center'>" + item.studCourse + "</td><td style='text-align:center'>" + item.studUsn + "</td><td><input type='button' ID='btn-" + item.studId + "' value='Delete' class='new-button' />&nbsp;&nbsp;&nbsp;<input type='button' ID='upd-" + item.studId + "' value='Update' class='upd-button' /></td></tr>";
                t = t + " <tr><td style='text-align:left' colspan='5'><hr></td></tr> ";
            });
            t = t + " </table> ";
            $("#stdData").html(t);
        },
        error: function (msg) { }
    });
};

function checkOptions(selector, prop) {
    var options = selector.find('option').toArray();
    var result = options.some(function (currentValue) {
        return (currentValue.text == prop); //using type coercion
    });
    return result;
}
function confirmarDireccionesMapaConfirmar() {

    
    if ($('#tbLatitudSiniestro').val() !== '' && $('#tbLonguitudSiniestro').val() !== '') {
        var x = $('#tbLatitudSiniestro').val();
        var y = $('#tbLonguitudSiniestro').val();

        var punto1 = "{\"displayFieldName\":\"\",\"fields\":[{\"name\":\"objectid\",\"type\":\"esriFieldTypeOID\",\"alias\":\"OBJECTID\"},{\"name\":\"x\",\"type\":\"esriFieldTypeDouble\",\"alias\":\"x\"},{\"name\":\"y\",\"type\":\"esriFieldTypeDouble\",\"alias\":\"y\"}],\"features\":[{\"attributes\":{\"objectid\":1,\"x\":" + y + ",\"y\":" + x + "}}],\"exceededTransferLimit\":false}";
        console.log('impremime paramtreo envio');
        console.log(punto1);
        $.ajax({
            type: "GET",
            url: "http://geoestadisticas.ant.gob.ec:6080/arcgis/rest/services/GeoProcesos/GetDivisionPoliticaGP/GPServer/GetDivisionPolitica/execute",
            data: {
                punto: punto1,
                returnZ: "false",
                returnM: "false",
                f: "pjson"
            },
            //   contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (data) {
                //JSON.stringify(data);
                JSON.stringify(data);
                //  console.log(response[0].features);
                $.each(data, function (i, item) {
                    console.log(data);
                    console.log("dpa_provin" + " " + parseInt(item[0].value.features[0].attributes.DPA_PROVIN));
                    retorno = parseInt(item[0].value.features[0].attributes.DPA_PROVIN.toString());
                    console.log('retorno1' + ' ' + retorno);
                    //if (item[0].value.features[0].attributes.DPA_PROVIN !== "") {
                    //    console.log("dpa_provin2" + " " + parseInt(item[0].value.features[0].attributes.DPA_PROVIN));
                    //    return retorno = parseInt(item[0].value.features[0].attributes.DPA_PROVIN.toString());
                    //    console.log('retorno1' + ' ' + retorno);
                    //}
                    //else
                    //{
                    //    return retorno = "0";
                    //    console.log('retorno' + '' + retorno);
                    //}

                });
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("No se puede conectar con el servidor de georeferencias, Intente nuevamente..!!");
            }

        });

      //  console.log('retorno2' + ' ' + retorno);
    }
   return retorno;
    
};

function ObtenerDatosConsultaPorLicencia() {
    var tipoI = '';
    ($('#tb_NombresVictima').val(''));
    var cedula = $("#tb_IdentificacionVictima").val();
    if ($("#tb_IdentificacionVictima").val().length === 10) {
        if ($('#ddl_tipoIdentificacion').val() === '1' || $('#ddl_tipoIdentificacion').val() === '3') {
            tipoI = '1';
        }
        else if ($('#ddl_tipoIdentificacion').val() === '2') {
            tipoI = '2';
        }
        $("#divLoaderSin").css("display", "block");
        $.ajax({
            type: "get",
            url: 'ObtenerInformacionVictima',
            data: { cedula: cedula, tipoI: tipoI },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (jsonResult) {
                console.log('retorno datos vicitmas' + ' ' + jsonResult);
                if (jsonResult.length > 0) {
                    $.each(jsonResult, function (key, value) {

                        $('#tb_NombresVictima').val(value.nombre_completo);
                        $("#tb_edadVictima").val(value.edavicinv);
                        if (value.sexo.toString() === 'MASCULINO') {
                            $('#ddl_generoVictima').val('H');
                        }
                        else if (value.sexo.toString() === 'FEMENINO') {
                            $('#ddl_generoVictima').val('M');
                        }
                        else {
                            $('#ddl_generoVictima').val('N');
                        }

                        mensaje = "Busqueda exitosa.";
                        alert(mensaje.toString() + ' ' + value.nombre_completo);
                        $("#divLoaderSin").css("display", "none");
                    });
                }
                else {
                    alert("No existen resultados en  la búsqueda.");
                    $('#ddl_generoVictima').val('-1');
                    $("#tb_edadVictima").val('-1');
                    $("#divLoaderSin").css("display", "none");
                }

            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("No existen resultados en  la búsqueda.");
                $('#ddl_generoVictima').val('-1');
                $("#tb_edadVictima").val('-1');
                $("#divLoaderSin").css("display", "none");
            }

        });

    } else {
        alert("Ingrese un número de licencia correcto. Recuerde que debe contener 10 dígitos");
    }
};

function ObtenerDatosConsultaPorCedula() {
    var tipoI = '';
    ($('#tb_NombresVictima').val(''));
    var cont = 0;
    var cedula = $("#tb_IdentificacionVictima").val();
    console.log("ingresa WS1");
    if ($("#tb_IdentificacionVictima").val().length === 10) {
        if ($('#ddl_tipoIdentificacion').val() === '1' || $('#ddl_tipoIdentificacion').val() === '3') {
            tipoI = '1';
        }
        else if ($('#ddl_tipoIdentificacion').val() === '2') {
            tipoI = '2';
        }
        $("#divLoaderSin").css("display", "block");
        $.ajax({
            type: "get",
            url: 'ObtenerDatosWSRC',
            data: { numeroIdentificacion: cedula },
            contentType: "application/json;",
            dataType: "json",
            success: function (jsonResult) {
                console.log("ingresa WS2 S");
                console.log(jsonResult);
                if (jsonResult.length > 0) {
                    $.each(jsonResult, function (key, value) {
                        cont = cont + 1;
                        if (cont === 1)
                        {
                            $('#tb_NombresVictima').val(value.nombre);
                            $("#tb_edadVictima").val(value.edad);
                            if (value.sexo === 'HOMBRE') {
                                $('#ddl_generoVictima').val('H');
                            }
                            else if (value.sexo === 'MUJER') {
                                $('#ddl_generoVictima').val('M');
                            }
                            else {
                                $('#ddl_generoVictima').val('-1');
                            }

                            mensaje = "Búsqueda exitosa.";
                            alert(mensaje.toString() + ' ' + value.nombre);
                            $("#divLoaderSin").css("display", "none");
                        }

                    });
                }
                else {
                    alert("No existen resultados en  la búsqueda.");
                    $('#ddl_generoVictima').val('-1');
                    $("#tb_edadVictima").val('-1');
                    $("#divLoaderSin").css("display", "none");

                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("No existen resultados en  la búsqueda.");
                $('#ddl_generoVictima').val('-1');
                $("#tb_edadVictima").val('-1');
                $("#divLoaderSin").css("display", "none");
            }

        });

    }
    else {
        alert("Ingrese un número de identificación correcto. Recuerde que debe contener 10 dígitos");
    }
};