//$('#ddl_accionesPeatonEdit').multiselect({
//    //  includeSelectAllOption: true,
//    language: {

//        noResults: function () {

//            return "No hay resultado";
//        },
//        searching: function () {

//            return "Buscando..";
//        }
//    }
    
//});
$(document).ready(function () {
    //VerMapa();
   
    $('#tb_codVHLEdit').val('0');
    $("#tbfechaSiniestroEdit").datepicker({ dateFormat: "yy-mm-dd",maxDate:'+0d' }).mask("9999-99-99"); // CONTROL DE FECHA  (AÑO-MES-DIA)
    jQuery(function ($) {
        $.mask.definitions['H'] = '[0123]';
        $.mask.definitions['N'] = '[012345]';
        $.mask.definitions['n'] = '[0123456789]';
        $("#tbHoraSiniestroEdit").mask("Hn:Nn");
    });
    var pathname = window.location.pathname; // Returns path only
    $('#btnVerMapaSinEdit').click(function () {
        VerMapa2();
        var x = $('#tbLonguitudSiniestroSin').val();
        var y = $('#tbLatitudSiniestroEdit').val();

        $('#lat').val(y);
        $('#lng').val(x);
    });
    
    var codsin = ($('#tbSiniestroModEdit').val());
    var codprocesoAnterior = $('#lbCodigoProceso').val();
    if ($('#tbSiniestroModEdit').val() !== '' || $('#tbSiniestroModEdit').val() !== '0') {
        console.log('paso aqui' + ' ' + codsin.toString());
        $.ajax({
            type: "get",
            url: '../JsonDatosSiniestroPorCodigo',
            data: { id: codsin },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (jsonResult) {
                console.log(jsonResult);
                $.each(jsonResult, function (key, value) {
                    /* Vamos agregando a nuestra tabla las filas necesarias */
                    $('#tbSiniestroModEdit').attr("disabled", "disabled");
                    $("#tbfechaSiniestroEdit").val(value.fecsin);
                    $("#tbHoraSiniestroEdit").val(value.horsin);
                    $("#tbLatitudSiniestroEdit").val(value.latsin);
                    $("#tbLonguitudSiniestroSin").val(value.lonsin);
                    $("#ddl_provinciaEdit").val(value.codprov);
                    $("#ddl_cantonEdit").val(value.codcant);
                    $("#ddl_parroquiaEdit").val(value.codpar);
                    $("#ddl_distritoSiniestroEdit").val(value.coddis);
                    $("#ddl_circuitoEdit").val(value.codcir);
                    $("#ddl_tipo_zonaEdit").find('option').each(function (i, opt) {
                        if (opt.text.trim() === value.zonsin.trim()) {
                            $("#ddl_tipo_zonaEdit").val(opt.value);
                        }
                    });
                    $("#tbdireccionSiniestroEdit").val(value.dirsin);
                    $("#tbnumfalsinEdit").val(value.numfalsin);
                    $('#tbnumfalsinEdit').attr("disabled", "disabled");
                    $("#tbnumlessinEdit").val(value.numlessin);
                    $('#tbnumlessinEdit').attr("disabled", "disabled");
                    $("#ddl_condAtmosfericaEdit").find('option').each(function (i, opt) {
                        if (opt.text.trim() === value.conatmsin.trim()) {
                            $("#ddl_condAtmosfericaEdit").val(opt.value);
                        }
                    });
                    $("#ddl_condViaEdit").find('option').each(function (i, opt) {
                        if (opt.text.trim() === value.conviasin.trim()) {
                            $("#ddl_condViaEdit").val(opt.value);
                        }
                    });
                    $("#ddl_tipoViaEdit").find('option').each(function (i, opt) {
                        if (opt.text.trim() === value.desviasin.trim()) {
                            $("#ddl_tipoViaEdit").val(opt.value);
                        }
                    });
                    $("#ddl_limVelocidadEdit").find('option').each(function (i, opt) {
                        if (opt.text.trim().toString() === value.limvelsin.toString().trim()) {
                            $("#ddl_limVelocidadEdit").val(opt.value);
                        }
                    });
                    $("#ddl_numCarrilesEdit").find('option').each(function (i, opt) {
                        if (opt.text.trim().toString() === value.numcarsin.toString().trim()) {
                            $("#ddl_numCarrilesEdit").val(opt.value);
                        }
                    });
                    $("#ddl_materialSuperficieEdit").find('option').each(function (i, opt) {
                        if (opt.text.trim() === value.matsupviasin.trim()) {
                            $("#ddl_materialSuperficieEdit").val(opt.value);
                        }
                    });
                    $("#ddl_controlInterseccionEdit").find('option').each(function (i, opt) {
                        if (opt.text.trim() === value.intsin.trim()) {
                            $("#ddl_controlInterseccionEdit").val(opt.value);
                        }
                    });
                    $("#ddl_obstViaEdit").find('option').each(function (i, opt) {
                        if (opt.text.trim() === value.obsviasin.trim()) {
                            $("#ddl_obstViaEdit").val(opt.value);
                        }
                    });
                    $("#ddl_lugarViaEdit").find('option').each(function (i, opt) {
                        if (opt.text.trim() === value.lugviasin.trim()) {
                            $("#ddl_lugarViaEdit").val(opt.value);
                        }
                    });
                    $("#ddl_curvaExistenteEdit").find('option').each(function (i, opt) {
                        if (opt.text.trim() === value.cursin.trim()) {
                            $("#ddl_curvaExistenteEdit").val(opt.value);
                        }
                    });
                    $("#ddl_senializacionExistenteEdit").find('option').each(function (i, opt) {
                        if (opt.text.trim().toString() === value.sensin.toString().trim()) {
                            $("#ddl_senializacionExistenteEdit").val(opt.value);
                        }
                    });
                    $("#ddl_tipoSiniestroEdit").val(value.codtipsin);
                    $("#ddl_causaProbableSiniestroEdit").val(value.codcaupro);
                    if (value.luzartsin.trim() !== 'SELECCIONAR' && (value.luzartsin.trim() !== ''))
                    {
                        ($('#divLuzArtificialEdit').css("display", "block"));
                    }
                    $("#ddl_luzArtificialEdit").find('option').each(function (i, opt) {
                        if (value.luzartsin.trim() !== '' && value.luzartsin.trim() !== 'SELECCIONAR')
                        {
                            if (opt.text.trim() === value.luzartsin.trim()) {
                                $("#ddl_luzArtificialEdit").val(opt.value);
                                $("#ddl_luzArtificialEdit").css("display", "block");
                            }
                            
                        }
                        
                    });
                    
                    if (value.traviasin == true)
                    {
                        $('#ddl_trabajosViaEdit').val("1");
                    }
                    else if (value.traviasin == false) {
                        $('#ddl_trabajosViaEdit').val("2");
                    }
                    

                });

            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(textStatus + ": " + XMLHttpRequest.responseText);
            }

        });
    }


    /****************************************************************************
    * INICIO VEHICULOS
    *************************************************************************/
    $('.btnModalVehiculoEdit').click(function (eve) {
        console.log(pathname);
        var codsubtipoVhl = "-1";
        //$("#panelMensajeVHL").css("display", "none");
        $("#ddl_vhl_materialPeligrosoEdit").val('-1');
        if ($(this).data('id') !== '')
        {
            var id = $(this).data('id');
            $.ajax({
                type: "get",
                url: '../JsonDatosVhlPorCodigo',
                data: { id: id },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (jsonResult) {
                    console.log(jsonResult);
                    $.each(jsonResult, function (key, value) {
                        if (value.chavehinv.toString() !== '') {
                            $("#tb_codVHLEdit").val(value.codvehinv);
                            $('#tb_codVHLEdit').attr("disabled", "disabled");
                            $("#ddl_tipo_vhlEdit").val(value.codtipve);
                            $("#tb_placvehinvEdit").val(value.placvehinv);
                            $("#tb_chavehinvEdit").val(value.chavehinv);
                            $("#tb_marvehinvEdit").val(value.marvehinv);
                            $("#tb_modvehinvEdit").val(value.modvehinv);
                            $("#tb_anivehinvEdit").val(value.anivehinv);
                            $("#tb_cilvehinvEdit").val(value.cilvehinv);
                            ///$("#ddl_vhl_materialPeligrosoEdit").val(value.cilvehinv);
                            $("#ddl_tipo_servicio_vhlEdit").val(value.codser);
                            if (value.danmatvehinv === true) 
                                $('#ddl_danio_materialEdit').val('1');
                            else
                                $('#ddl_danio_materialEdit').val('2');
                            if (value.matvigvehinv === true)
                                $('#ddl_matricula_vigenteEdit').val('1');
                            else
                                $('#ddl_matricula_vigenteEdit').val('2');
                            if (value.segprivehinv === true)
                                $('#ddl_seguro_privado_vhlEdit').val('1');
                            else
                                $('#ddl_seguro_privado_vhlEdit').val('2');

                            $("#ddl_vhl_materialPeligrosoEdit").find('option').each(function (i, opt) {
                                if (opt.text.trim() === value.matpelvehinv.trim())
                                {
                                    $("#ddl_vhl_materialPeligrosoEdit").val(opt.value);
                                }
                            });

                            cargarSubTipoVhl(value.codsubveh);
                            

                            $("#ddl_subtipo_vhlEdit option:selected").text(value.dessubveh);

                            $('#respuesta-ej2').toggle();
                            $('#respuesta-ej1').toggle();
                            ($('#btnModificarVhlInvolucrados').css("display", "block"));
                            ($('#tbGudarVhlInvolucradosEdit').css("display", "none"));
                            
                            
                        }
                        else {
                            mensaje = "No existe resultados en la busqueda.";
                            alert(mensaje.toString());
                        }

                    });

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus + ": " + XMLHttpRequest.responseText);
                }

            });
        }
        //if ($("#ddl_tipo_vhlEdit").val() !== '-1')
        //{
        //    console.log ('pasa a cargar subtipo')
        //    var select = $('#ddl_subtipo_vhlEdit');
        //    select.find('option').remove().end().append('<option value="-1">SELECCIONAR</option>').val('-1');
        //    var codtipoVhl = $("#ddl_tipo_vhlEdit").val();
        //    if (codtipoVhl !== '' && codtipoVhl !== '0')
        //    {
        //        $.ajax({
        //            type: "GET",
        //            url: "ObtenerlistaSubTipoVehiculo",
        //            data: { codTipoVhl: codtipoVhl },
        //            contentType: "application/json; charset=utf-8",
        //            dataType: "json",
        //            success: function (jsonResult) {
        //                if (jsonResult.length > 0) {
        //                    $.each(jsonResult, function (key, value) {
        //                        select.append('<option value="' + value.value + '">' + value.text + '</option>').val(value.value);
        //                    });
        //                    select.val(codsubtipoVhl);
        //                }
        //            },
        //            error: function (XMLHttpRequest, textStatus, errorThrown) {
        //                alert("Error al cargar los sub tipo de vehiculos.");
        //            }
        //        });
        //    }
        //}
    });

    $('.btnModalVehiculoDelete').click(function () {
        
        ($('#divMensajeVHLEdit').attr("class", "alert alert-danger smaller"));
        ($('#panelMensajeVHLEdit').css('display', 'none'));
        var codSin = $('#tbSiniestroModEdit').val();
        var id = $(this).data('id');
        
        if (codSin !== '' && id !== '')
        {
            if (confirm('Va a eliminar el vehículo involucrado. Desea continuar !') == true) {
                $.ajax({
                    type: "GET",
                    url: "../eliminaVehiculos",
                    data: { codVehiculo: id, codsin: codSin },
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (jsonResult) {

                       // console.log("elimonacion vh" + " " + jsonResult);

                        if (jsonResult === "-1") {
                            alert("Para poder eliminar el vehículo involucrado debe primero eliminar las VÍCTIMAS INVOLUCRADAS asociadas al vehículo.");
                        }
                        if (jsonResult.length > -1 && jsonResult !== "-1") {
                            var mensaje = 'Vehiculo eliminado correctamente correctamente.';
                            ($('#divMensajeVHLEdit').attr("class", "alert alert-success smaller"));
                            ($('#mensajeLabelVHLEdit').html(mensaje.toString()));

                            ($('#panelMensajeVHLEdit').css("display", "block"));
                            TablaVehiculos(codSin);
                        }
                        //else {
                        //    alert("Error al eliminar el registro del vehículo involucrado.");
                        //}
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("Error al eliminar el registro del vehículo involucrado.");
                    }

                });

              
            }
        }
      
   

    });

    $('#ddl_tipo_vhlEdit').change(function () {
        if ($('#ddl_tipo_vhlEdit').val().toString() === '16') {
            $("#tb_placvehinvEdit").val('ND');
            $("#tb_chavehinvEdit").val('ND');
            $("#tb_marvehinvEdit").val('ND');
            $("#tb_modvehinvEdit").val('ND');
            $("#tb_anivehinvEdit").val('0');
            $("#tb_cilvehinvEdit").val('0');
            $("#ddl_vhl_materialPeligrosoEdit").val('0');
            $("#ddl_tipo_servicio_vhlEdit").val('3');
            //$("#btnBuscarVhl").css("display", "none");
            //$("#divBuscarPlaca").css("display", "none");

            $('#tb_placvehinvEdit').removeAttr('disabled');
            $('#tb_chavehinvEdit').removeAttr('disabled');
            $('#tb_marvehinvEdit').removeAttr('disabled');
            $('#tb_modvehinvEdit').removeAttr('disabled');
            $('#tb_anivehinvEdit').removeAttr('disabled');
            $('#tb_cilvehinvEdit').removeAttr('disabled');

        }
        else if ($('#ddl_tipo_vhlEdit').val().toString() !== '16' && $('#ddl_vehiculo_identificadoEdit').val() == '1') {
            $("#tb_placvehinvEdit").val('');
            $("#tb_chavehinvEdit").val('');
            $("#tb_marvehinvEdit").val('');
            $("#tb_modvehinvEdit").val('');
            $("#tb_anivehinvEdit").val('0');
            $("#tb_cilvehinvEdit").val('0');
            $("#ddl_vhl_materialPeligrosoEdit").val('0');
            $("#ddl_tipo_servicio_vhlEdit").val('-1');
            //$("#btnBuscarVhl").css("display", "block");
            //$("#divBuscarPlaca").css("display", "block");
            $('#tb_placvehinvEdit').removeAttr('disabled');
            $('#tb_chavehinvEdit').removeAttr('disabled');
            $('#tb_marvehinvEdit').removeAttr('disabled');
            $('#tb_modvehinvEdit').removeAttr('disabled');
            $('#tb_anivehinvEdit').removeAttr('disabled');
            $('#tb_cilvehinvEdit').removeAttr('disabled');
        }

        if ($('#ddl_tipo_vhlEdit').val().toString() !== '-1') {
            var select = $('#ddl_subtipo_vhlEdit');
            select.find('option').remove().end().append('<option value="-1">SELECCIONAR</option>').val('-1');
            var codTipoVhls = $('#ddl_tipo_vhlEdit').val();
            $.ajax({
                type: "GET",
                url: "../ObtenerlistaSubTipoVehiculo",
                data: { codTipoVhl: codTipoVhls },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (jsonResult) {
                    if (jsonResult.length > 0) {
                        $.each(jsonResult, function (key, value) {
                            select.append('<option value="' + value.value + '">' + value.text + '</option>').val(value.value);
                        });

                        if ($('#ddl_tipo_vhlEdit').val() === '16') {
                            select.val('54');
                        }
                        else if ($('#ddl_tipo_vhlEdit').val() === '18') {
                            select.val('16');
                        }
                        else {
                            select.val('-1');
                        }
                        //select.val('-1');
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("Error al cargar los sub tipo de vehiculos.");
                }
            });
        }
    });

    $('#btnModificarVhlInvolucrados').click(function () {
       
        ($('#divMensajeVHLEdit').attr("class", "alert alert-danger smaller"));
        ($('#panelMensajeVHLEdit').css('display', 'none'));

        var mensaje = '';
        var codVhl = ($('#tb_codVHLEdit').val());
        var placaVHL = ($('#tb_placvehinvEdit').val());
        var chasisVHL = ($('#tb_chavehinvEdit').val());
        var marcaVHL = ($('#tb_marvehinvEdit').val());
        var modeloVHL = ($('#tb_modvehinvEdit').val());
        var anioVHL = ($('#tb_anivehinvEdit').val());
        var cilindrajeVHL = ($('#tb_cilvehinvEdit').val());
        var materialPeligrosoVHL = ($('#ddl_vhl_materialPeligrosoEdit option:selected').text());
        var codtipoServVHL = ($('#ddl_tipo_servicio_vhlEdit').val());
        var codtipoVHL = ($('#ddl_tipo_vhlEdit').val());
        var codSubtipoVHL = ($('#ddl_subtipo_vhlEdit').val());
        var seguroPrivado = $("#ddl_seguro_privado_vhlEdit").val();
        var danioMaterial = $("#ddl_danio_materialEdit").val();
        var matriculaVigente = $('#ddl_matricula_vigenteEdit').val();

        console.log('codVhl' + '' + codVhl);
        if (codVhl === '' && codVhl === '0')
        {
            mensaje = 'Debe seleccionar un vehículo';
            ($('#mensajeLabelVHLEdit').html(mensaje.toString()));
            ($('#panelMensajeVHLEdit').css("display", "block"));
        }
        else if (codtipoVHL.toString().trim() === "" || codtipoVHL.toString().trim() === "-1") {
            mensaje = 'Seleccione el tipo de vehiculo.';
            ($('#mensajeLabelVHLEdit').html(mensaje.toString()));
            ($('#panelMensajeVHLEdit').css("display", "block"));
        }
        else if (codSubtipoVHL.toString().trim() === "-1" || codSubtipoVHL.toString().trim() === "0") {
            mensaje = 'Seleccione el sub tipo de vehiculo.';
            ($('#mensajeLabelVHLEdit').html(mensaje.toString()));
            ($('#panelMensajeVHLEdit').css("display", "block"));
        }
        else if (placaVHL.toString().trim() === "" && codtipoVHL.toString() !== "16") {
            mensaje = 'Ingrese la placa';
            ($('#mensajeLabelVHLEdit').html(mensaje.toString()));
            ($('#panelMensajeVHLEdit').css("display", "block"));
        }
        else if (chasisVHL.toString().trim() === "" && codtipoVHL.toString() !== "16") {
            mensaje = 'Ingrese el chasis';
            ($('#mensajeLabelVHLEdit').html(mensaje.toString()));
            ($('#panelMensajeVHLEdit').css("display", "block"));
        }
        else if (marcaVHL.toString().trim() === "" && codtipoVHL.toString() !== "16") {
            mensaje = 'Ingrese la marca del Vehiculo';
            ($('#mensajeLabelVHLEdit').html(mensaje.toString()));
            ($('#panelMensajeVHLEdit').css("display", "block"));
        }
        else if (modeloVHL.toString().trim() === "" && codtipoVHL.toString() !== "16") {
            mensaje = 'Ingrese el modelo del Vehiculo';
            ($('#mensajeLabelVHLEdit').html(mensaje.toString()));
            ($('#panelMensajeVHLEdit').css("display", "block"));
        }
        else if (anioVHL.toString().trim() === "" && codtipoVHL.toString() !== "16") {
            mensaje = 'Ingrese el año del Vehiculo';
            ($('#mensajeLabelVHLEdit').html(mensaje.toString()));
            ($('#panelMensajeVHLEdit').css("display", "block"));
        }
        else if (cilindrajeVHL.toString().trim() === "" && codtipoVHL.toString() !== "16") {
            mensaje = 'Ingrese el cilindraje del Vehiculo';
            ($('#mensajeLabelVHLEdit').html(mensaje.toString()));
            ($('#panelMensajeVHLEdit').css("display", "block"));
        }
        else if (materialPeligrosoVHL.toString().trim() === "SELECCIONAR" && codtipoVHL.toString() !== "16") {
            mensaje = 'Seleccione si el vehiculo contenia material peligroso.';
            ($('#mensajeLabelVHLEdit').html(mensaje.toString()));
            ($('#panelMensajeVHLEdit').css("display", "block"));
        }
        else if (codtipoServVHL.toString().trim() === "" && codtipoVHL.toString() !== "16") {
            mensaje = 'Seleccione el tipo de servicio del vehiculo.';
            ($('#mensajeLabelVHLEdit').html(mensaje.toString()));
            ($('#panelMensajeVHLEdit').css("display", "block"));
        }
        else if (seguroPrivado.toString() === "-1" || seguroPrivado.toString() === "0") {
            mensaje = 'Seleccione si posee seguro privado.';
            ($('#mensajeLabelVHLEdit').html(mensaje.toString()));
            ($('#panelMensajeVHLEdit').css("display", "block"));
        }
        else if (danioMaterial.toString() === "-1" || danioMaterial.toString() === "0") {
            mensaje = 'Seleccione si ocasiono daños materiales.';
            ($('#mensajeLabelVHLEdit').html(mensaje.toString()));
            ($('#panelMensajeVHLEdit').css("display", "block"));
        }
        else if (matriculaVigente.toString() === "-1" || matriculaVigente.toString() === "0") {
            mensaje = 'Seleccione si tiene la matrícula vigente.';
            ($('#mensajeLabelVHLEdit').html(mensaje.toString()));
            ($('#panelMensajeVHLEdit').css("display", "block"));
        }

       
        else {
            var fecha_actual = Date.toLocaleString();
            var fechaSin = ($('#tbfechaSiniestroEdit').val());
            fecha_actual = new Date().toJSON().slice(0, 10)
            console.log('fecha_actual ' + '' + fecha_actual);
            console.log('fechaSin ' + '' + fechaSin);
            var anio_max = fechaSin.split('-')[0];
            var mes_max = parseInt(fechaSin.split('-')[1]) + 1;
            var dia_max = '10';
            var dia_min = '01';
            var fecha_max = anio_max + '-' + mes_max + '-' + dia_max;
            var fecha_min = anio_max + '-' + fechaSin.split('-')[1] + '-' + dia_min;
            console.log('fecha_max ' + '' + fecha_max);
            console.log('fecha_min ' + '' + fecha_min);
            fecha_max = fecha_max.split('-');
            fecha_actual = fecha_actual.split('-');
            fechaSin = fechaSin.split('-');
            fecha_min = fecha_min.split('-');
            fecha_max = new Date(fecha_max[0], fecha_max[1] - 1, fecha_max[2]).valueOf();
            fecha_actual = new Date(fecha_actual[0], fecha_actual[1] - 1, fecha_actual[2]).valueOf();
            fechaSin = new Date(fechaSin[0], fechaSin[1] - 1, fechaSin[2]).valueOf();
            fecha_min = new Date(fecha_min[0], fecha_min[1] - 1, fecha_min[2]).valueOf();
            console.log('------------------------------------------');
            console.log('fecha_max ' + '' + fecha_max);
            console.log('fecha_actual ' + '' + fecha_actual);
            console.log('fechaSin ' + '' + fechaSin);
            console.log('fecha_min ' + '' + fecha_min);
            var _fecha_actual_sistema = new Date().toJSON().slice(0, 10);
            _fecha_actual_sistema = new Date(_fecha_actual_sistema).valueOf();
            if (fecha_max >= _fecha_actual_sistema && fechaSin <= fecha_max && fechaSin >= fecha_min ) {
                if (confirm('Va a modificar los datos del vehículo. Desea continuar !') == true) {
                    $.ajax({
                        type: "get",
                        url: '../JsonModificarDatosVehiculosInvolucrados',
                        data: { codVhl: codVhl, placa: placaVHL, chavehinv: chasisVHL, marvehinv: marcaVHL, modvehinv: modeloVHL, cilvehinv: cilindrajeVHL, matpelvehinv: materialPeligrosoVHL, codser: codtipoServVHL, codtipve: codtipoVHL, seguroPrivado: seguroPrivado, danioMaterial: danioMaterial, matriculaVigente: matriculaVigente, anivehinv: anioVHL, codsubtipoVhl: codSubtipoVHL },
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (jsonResult) {
                            console.log('retorno vhl inv' + ' ' + jsonResult);
                            if (jsonResult.toString ==="0") {
                                mensaje = 'Error al grabar los datos.';
                                ($('#mensajeLabelVHLEdit').html(mensaje.toString()));
                            }
                            else if (jsonResult.toString !== "0") {


                                mensaje = 'Datos modificados correctamente.';
                                ($('#divMensajeVHLEdit').attr("class", "alert alert-success smaller"));
                                ($('#mensajeLabelVHLEdit').html(mensaje.toString()));

                                ($('#panelMensajeVHLEdit').css("display", "block"));
                                inicializaVhlEdit();
                                $('#respuesta-ej2').toggle();
                                $('#respuesta-ej1').toggle();
                                TablaVehiculos(codsin);

                            }
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus + ": " + XMLHttpRequest.responseText);
                        }

                    });
                }
                else {
                    ($('#mensajeLabelVHLEdit').html(mensaje.toString()));
                    ($('#panelMensajeVHLEdit').css("display", "none"));
                }
                
            }
            else {
                mensaje = "No puede modificar el registro, la fecha del siniestro esta fuera del rango permitido.";
                ($('#mensajeLabelVHLEdit').html(mensaje.toString()));
                ($('#panelMensajeVHLEdit').css("display", "block"));
            }
        }
        
       

    });


    $('#btLimpiarVHL').click(function () {

        $('#respuesta-ej1').toggle();
        $('#respuesta-ej2').toggle();

        var selectsv = $('#ddl_subtipo_vhlEdit');
        selectsv.find('option').remove().end().append('<option value="-1">SELECCIONAR</option>').val('-1');
        $("#tb_placvehinvEdit").val('');
        $("#tb_chavehinvEdit").val('');
        $("#tb_marvehinvEdit").val('');
        $("#tb_modvehinvEdit").val('');
        $("#tb_anivehinvEdit").val('');
        $("#tb_cilvehinvEdit").val('');
        $("#ddl_tipo_vhlEdit").val('-1');
        $("#ddl_tipo_servicio_vhlEdit").val('-1');
        $('#ddl_subtipo_vhlEdit').val('-1');
        $("#ddl_vhl_materialPeligrosoEdit").val('0');
        $('#tb_placvehinvEdit').removeAttr('disabled');
        $('#tb_chavehinvEdit').removeAttr('disabled');
        $('#tb_marvehinvEdit').removeAttr('disabled');
        $('#tb_modvehinvEdit').removeAttr('disabled');
        $('#tb_anivehinvEdit').removeAttr('disabled');
        $('#tb_cilvehinvEdit').removeAttr('disabled');
        $('#ddl_seguro_privado_vhlEdit').val('-1');
        $('#ddl_danio_materialEdit').val('-1');
        $('#ddl_matricula_vigenteEdit').val('-1');
        //  $("#btnBuscarVhl").css("display", "none");
        $("#panelMensajeVHLEdit").css("display", "none");
    });
     


    $('#ddl_vehiculo_identificadoEdit').change(function () {
        if ($('#ddl_vehiculo_identificadoEdit option:selected').text() === 'SI') {
      
            $('#ddl_vhl_materialPeligrosoEdit').removeAttr('disabled');
            $('#ddl_tipo_servicio_vhlEdit').removeAttr('disabled');
            $('#ddl_seguro_privado_vhlEdit').removeAttr('disabled');
            $('#ddl_danio_materialEdit').removeAttr('disabled');
            $('#ddl_matricula_vigenteEdit').removeAttr('disabled');
            $('#tb_placvehinvEdit').removeAttr('disabled');
            $('#tb_chavehinvEdit').removeAttr('disabled');
            $('#tb_marvehinvEdit').removeAttr('disabled');
            $('#tb_modvehinvEdit').removeAttr('disabled');
            $('#tb_anivehinvEdit').removeAttr('disabled');
            $('#tb_cilvehinvEdit').removeAttr('disabled');
            $('#tb_placvehinvEdit').val('');
            $('#tb_chavehinvEdit').val('');
            $('#tb_marvehinvEdit').val('');
            $('#tb_modvehinvEdit').val('');
            $('#tb_anivehinvEdit').val('');
            $('#tb_cilvehinvEdit').val('');
            $('#ddl_vhl_materialPeligrosoEdit').val('0');
            $('#ddl_tipo_servicio_vhlEdit').val('-1');
            $('#ddl_seguro_privado_vhlEdit').val('-1');
            $('#ddl_danio_materialEdit').val('-1');
            $('#ddl_matricula_vigenteEdit').val('-1');
            $('#ddl_tipo_vhlEdit').val('-1');
            $('#ddl_subtipo_vhlEdit').val('-1');
        }
        else if ($('#ddl_vehiculo_identificadoEdit option:selected').text() === 'NO') {

           
            $('#ddl_tipo_vhlEdit').val('-1');
            $('#ddl_subtipo_vhlEdit').val('-1');
            $('#tb_placvehinvEdit').attr('disabled', 'disabled')
            $('#tb_chavehinvEdit').attr('disabled', 'disabled')
            $('#tb_marvehinvEdit').attr('disabled', 'disabled')
            $('#tb_modvehinvEdit').attr('disabled', 'disabled')
            $('#tb_anivehinvEdit').attr('disabled', 'disabled')
            $('#tb_cilvehinvEdit').attr('disabled', 'disabled')
            $('#ddl_vhl_materialPeligrosoEdit').attr('disabled', 'disabled')
            $('#ddl_tipo_servicio_vhlEdit').attr('disabled', 'disabled')
            $('#ddl_seguro_privado_vhlEdit').attr('disabled', 'disabled')
            $('#ddl_danio_materialEdit').removeAttr('disabled')
            $('#ddl_matricula_vigenteEdit').attr('disabled', 'disabled')

            $('#tb_placvehinvEdit').val('NI');
            $('#tb_chavehinvEdit').val('NI');
            $('#tb_marvehinvEdit').val('NI');
            $('#tb_modvehinvEdit').val('NI');
            $('#tb_anivehinvEdit').val('0');
            $('#tb_cilvehinvEdit').val('0');
            $('#ddl_vhl_materialPeligrosoEdit').val('0');
            $('#ddl_tipo_servicio_vhlEdit').val('3');
            $('#ddl_seguro_privado_vhlEdit').val('2');
            $('#ddl_danio_materialEdit').val('-1');
            $('#ddl_matricula_vigenteEdit').val('2');
        }
        
    });
    /****************************************************************************
    * FIN VEHICULOS
    *************************************************************************/

/****************************************************************************
* INICIO VICTIMAS
*************************************************************************/
    $('.btnModalVictimaEdit').click(function (eve) {
        console.log(pathname);
        inicializaVictimasEdit();
        ($('#panelMensajeVictimasEdit').css("display", "none"));
        $("#ddl_tipoIdentificacionEdit").val('-1');
      //  $("#ddl_accionesPeatonEdit").val('-1');
        $('#divAccionesPeatonEdit').css("display", "none");
        $("#cb_1E").prop("checked", false);
        $("#cb_2E").prop("checked", false);
        $("#cb_3E").prop("checked", false);
        $("#cb_4E").prop("checked", false);
        $("#cb_5E").prop("checked", false);
        $("#cb_6E").prop("checked", false);
        $("#cb_7E").prop("checked", false);
        $("#cb_8E").prop("checked", false);
        $("#cb_9E").prop("checked", false);
        $("#cb_10E").prop("checked", false);
        $("#cb_11E").prop("checked", false);
        $("#cb_12E").prop("checked", false);
        $("#cb_13E").prop("checked", false);
        $('#ddl_consumo_alcoholEdit').removeAttr("disabled");
        $('#ddl_uso_cascoEdit').removeAttr("disabled");
        $('#ddl_uso_cinturonEdit').removeAttr("disabled")
        
         console.log( $(this).data('id'));
        if ($(this).data('id') !== '') {
            var idVict = $(this).data('id');
            $.ajax({
                type: "get",
                url: '../JsonDatosVictimasPorCodigo',
                data: { id: idVict },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (jsonResult) {
                    console.log(jsonResult);
                   $.each(jsonResult, function (key, value) {
                        if (value.codvicinv.toString() !== '') {
                            
                            $("#tb_codVictimasEdit").val(value.codvicinv);
                            $('#tb_codVictimasEdit').attr("disabled", "disabled");
                            $("#ddl_tipoIdentificacionEdit").find('option').each(function (i, opt) {
                                if (opt.text.trim() === value.tipidenvicinv.trim()) {
                                    $("#ddl_tipoIdentificacionEdit").val(opt.value);
                                }
                            });
                            $("#tb_IdentificacionVictimaEdit").val(value.numidenvicinv);
                            $("#tb_edadVictimaEdit").val(value.edavicinv);
                            $("#ddl_generoVictimaEdit").val(value.sexo);
                            $("#ddl_condicionVictimas24Edit").find('option').each(function (i, opt) {
                                if (opt.text.trim() === value.convicinv24.trim()) {
                                    $("#ddl_condicionVictimas24Edit").val(opt.value);
                                }
                            });
                            $("#ddl_condicionVictimas30Edit").find('option').each(function (i, opt) {
                                if (opt.text.trim() === value.convicinv30.trim()) {
                                    $("#ddl_condicionVictimas30Edit").val(opt.value);
                                }
                            });
                            $("#ddl_tipoParticipanteEdit").find('option').each(function (i, opt) {
                                if (opt.text.trim() === value.tipparvicinv.trim()) {
                                    $("#ddl_tipoParticipanteEdit").val(opt.value);
                                }
                            });
                            
                           
                         

                            console.log(value.tipparvicinv);

                            var retorno = $('#tbTipoVhlVictimaEdit').val();
                            console.log(retorno.toString());

                            if ($('#ddl_tipoParticipanteEdit option:selected').text() === 'PEATÓN') {
                               // $('#ddl_accionesPeatonEdit').css("display", "block");
                               // $('#divAccionesPeaton').css("display", "block");

                              //  ($('#ddl_accionesPeatonEdit').val('-1'));
                                $("#ddl_uso_cascoEdit").val('2');
                                $("#ddl_uso_cinturonEdit").val('2');
                                $("#ddl_consumo_alcoholEdit").val('2');
                                $("#cb_1E").prop("checked", false);
                                $("#cb_2E").prop("checked", false);
                                $("#cb_3E").prop("checked", false);
                                $("#cb_4E").prop("checked", false);
                                $("#cb_5E").prop("checked", false);
                                $("#cb_6E").prop("checked", false);
                                $("#cb_7E").prop("checked", false);
                                $("#cb_8E").prop("checked", false);
                                $("#cb_9E").prop("checked", false);
                                $("#cb_10E").prop("checked", false);
                                $("#cb_11E").prop("checked", true);
                                $("#cb_12E").prop("checked", false);
                                $("#cb_13E").prop("checked", false);


                                $("#cb_1E").prop("checked", false);
                                $("#cb_2E").prop("checked", false);
                                $("#cb_3E").prop("checked", false);
                                $("#cb_4E").prop("checked", false);
                                $("#cb_5E").prop("checked", false);
                                $("#cb_6E").prop("checked", false);
                                $("#cb_7E").prop("checked", false);
                                $("#cb_8E").prop("checked", false);
                                $("#cb_9E").prop("checked", false);
                                $("#cb_10E").prop("checked", false);
                                $("#cb_12E").prop("checked", false);
                                $("#cb_13E").prop("checked", false);
                                $("#cb_11E").prop("checked", true);
                                $("#cb_1E").attr("disabled", "disabled");
                                $("#cb_2E").attr("disabled", "disabled");
                                $("#cb_3E").attr("disabled", "disabled");
                                $("#cb_4E").attr("disabled", "disabled");
                                $("#cb_5E").attr("disabled", "disabled");
                                $("#cb_6E").attr("disabled", "disabled");
                                $("#cb_7E").attr("disabled", "disabled");
                                $("#cb_8E").attr("disabled", "disabled");
                                $("#cb_9E").attr("disabled", "disabled");
                                $("#cb_10E").attr("disabled", "disabled");
                                $("#cb_12E").attr("disabled", "disabled");
                                $("#cb_13E").attr("disabled", "disabled");
                              
                            }
                            else if ($('#ddl_tipoParticipanteEdit option:selected').text() === 'CONDUCTOR' && (retorno.toString() !== "8" && retorno.toString() !== "16")) {


                              
                                $("#cb_1E").prop("checked", true);
                                $("#cb_2E").prop("checked", false);
                                $("#cb_3E").prop("checked", false);
                                $("#cb_4E").prop("checked", false);
                                $("#cb_5E").prop("checked", false);
                                $("#cb_6E").prop("checked", false);
                                $("#cb_7E").prop("checked", false);
                                $("#cb_8E").prop("checked", false);
                                $("#cb_9E").prop("checked", false);
                                $("#cb_10E").prop("checked", false);
                                $("#cb_11E").prop("checked", false);
                                $("#cb_12E").prop("checked", false);
                                $("#cb_13E").prop("checked", false);

                                $("#cb_1E").removeAttr("disabled");
                                $("#cb_2E").attr("disabled", "disabled");
                                $("#cb_3E").attr("disabled", "disabled");
                                $("#cb_4E").attr("disabled", "disabled");
                                $("#cb_5E").attr("disabled", "disabled");
                                $("#cb_6E").attr("disabled", "disabled");
                                $("#cb_7E").attr("disabled", "disabled");
                                $("#cb_8E").attr("disabled", "disabled");
                                $("#cb_9E").attr("disabled", "disabled");
                                $("#cb_10E").attr("disabled", "disabled");
                                $("#cb_11E").attr("disabled", "disabled");
                                $("#cb_12E").attr("disabled", "disabled");
                                $("#cb_13E").attr("disabled", "disabled");

                                
                            }
                            else if ($('#ddl_tipoParticipanteEdit option:selected').text() === 'CONDUCTOR' && (retorno.toString() === "8" || retorno.toString() === "16")) {
                             
                                $("#cb_1E").prop("checked", true);
                                $("#cb_2E").prop("checked", false);
                                $("#cb_3E").prop("checked", false);
                                $("#cb_4E").prop("checked", false);
                                $("#cb_5E").prop("checked", false);
                                $("#cb_6E").prop("checked", false);
                                $("#cb_7E").prop("checked", false);
                                $("#cb_8E").prop("checked", false);
                                $("#cb_9E").prop("checked", false);
                                $("#cb_10E").prop("checked", false);
                                $("#cb_11E").prop("checked", false);
                                $("#cb_12E").prop("checked", false);
                                $("#cb_13E").prop("checked", false);


                                $("#cb_4E").attr("disabled", "disabled");
                                $("#cb_7E").attr("disabled", "disabled");
                                $("#cb_8E").attr("disabled", "disabled");
                                $("#cb_9E").attr("disabled", "disabled");
                                $("#cb_10E").attr("disabled", "disabled");
                                $("#cb_11E").attr("disabled", "disabled");
                                $("#cb_12E").attr("disabled", "disabled");
                                $("#cb_13E").attr("disabled", "disabled");
                            }

                            else if ($('#ddl_tipoParticipanteEdit option:selected').text() === 'PASAJERO' && (retorno.toString() !== "8" && retorno.toString() !== "16")) {


                                $("#cb_1E").prop("checked", false);
                                $("#cb_2E").prop("checked", false);
                                $("#cb_3E").prop("checked", false);
                                $("#cb_4E").prop("checked", false);
                                $("#cb_5E").prop("checked", false);
                                $("#cb_6E").prop("checked", false);
                                $("#cb_7E").prop("checked", false);
                                $("#cb_8E").prop("checked", false);
                                $("#cb_9E").prop("checked", false);
                                $("#cb_10E").prop("checked", false);
                                $("#cb_11E").prop("checked", false);
                                $("#cb_12E").prop("checked", false);
                                $("#cb_13E").prop("checked", false);

                                $("#cb_1E").attr("disabled", "disabled");
                                $("#cb_2E").removeAttr("disabled");
                                $("#cb_3E").removeAttr("disabled");
                                $("#cb_4E").removeAttr("disabled");
                                $("#cb_5E").removeAttr("disabled");
                                $("#cb_6E").removeAttr("disabled");
                                $("#cb_7E").removeAttr("disabled");
                                $("#cb_8E").removeAttr("disabled");
                                $("#cb_9E").removeAttr("disabled");
                                $("#cb_10E").removeAttr("disabled");
                                $("#cb_11E").removeAttr("disabled");
                                $("#cb_12E").removeAttr("disabled");
                                $("#cb_13E").removeAttr("disabled");
                            }
                            else if ($('#ddl_tipoParticipanteEdit option:selected').text() === 'PASAJERO' && (retorno.toString() === "8" || retorno.toString() === "16")) {
                             
                                $("#cb_1E").prop("checked", false);
                                $("#cb_2E").prop("checked", false);
                                $("#cb_3E").prop("checked", false);
                                $("#cb_4E").prop("checked", false);
                                $("#cb_5E").prop("checked", false);
                                $("#cb_6E").prop("checked", false);
                                $("#cb_7E").prop("checked", false);
                                $("#cb_8E").prop("checked", false);
                                $("#cb_9E").prop("checked", false);
                                $("#cb_10E").prop("checked", false);
                                $("#cb_11E").prop("checked", false);
                                $("#cb_12E").prop("checked", false);
                                $("#cb_13E").prop("checked", false);

                                $("#cb_1E").attr("disabled", "disabled");
                                $("#cb_2E").attr("disabled", "disabled");
                                $("#cb_3E").attr("disabled", "disabled");
                                $("#cb_4E").removeAttr("disabled");
                                $("#cb_5E").attr("disabled", "disabled");
                                $("#cb_6E").attr("disabled", "disabled");
                                $("#cb_7E").removeAttr("disabled");
                                $("#cb_8E").attr("disabled", "disabled");
                                $("#cb_9E").attr("disabled", "disabled");
                                $("#cb_10E").attr("disabled", "disabled");
                                $("#cb_11E").attr("disabled", "disabled");
                                $("#cb_12E").removeAttr("disabled");
                                $("#cb_13E").removeAttr("disabled");
                            }

                            if (value.posvicinv.trim() === 'FRONTAL IZQUIERDO')
                                $('#cb_1E').prop("checked", true);
                            if (value.posvicinv.trim() === 'FRONTAL CENTRAL')
                                $('#cb_2E').prop("checked", true);
                            if (value.posvicinv.trim() === 'FRONTAL DERECHO')
                                $('#cb_3E').prop("checked", true);
                            if (value.posvicinv.trim() === 'CENTRAL IZQUIERDO')
                                $('#cb_4E').prop("checked", true);
                            if (value.posvicinv.trim() === 'CENTRAL')
                                $('#cb_5E').prop("checked", true);
                            if (value.posvicinv.trim() === 'CENTRAL DERECHO')
                                $('#cb_6E').prop("checked", true);
                            if (value.posvicinv.trim() === 'TRASERO IZQUIERDO')
                                $('#cb_7E').prop("checked", true);
                            if (value.posvicinv.trim() === 'TRASERO CENTRAL')
                                $('#cb_8E').prop("checked", true);
                            if (value.posvicinv.trim() === 'TRASERO DERECHO')
                                $('#cb_9E').prop("checked", true);
                            if (value.posvicinv.trim() === 'BALDE')
                                $('#cb_10E').prop("checked", true);
                            if (value.posvicinv.trim() === 'DE PIE')
                                $('#cb_11E').prop("checked", true);
                            if (value.posvicinv.trim() === 'OTROS')
                                $('#cb_12E').prop("checked", true);
                            if (value.posvicinv.trim() === 'NIÑO EN BRAZOS')
                                $('#cb_13E').prop("checked", true);

                            if (value.tipparvicinv.trim() === 'PEATÓN' || value.tipparvicinv.trim() === 'CONDUCTOR')
                            {
                                $('#divAccionesPeatonEdit').css("display", "block");
                                $('#ddl_accionesPeatonEdit').css("display", "block");
                                
                            }
                            //$('#ddl_accionesPeatonEdit > option[value="4"]').attr('selected', 'selected');
                            //  $('#ddl_accionesPeatonEdit > option[value="2"]').attr('selected', 'selected');
                            console.log("value.desaccpea " + ' ' + value.codaccpea)
                            var _acciones_peaton = value.codaccpea.split(',');
                           // $('#ddl_accionesPeatonEdit').val('-1').attr('selected', false);
                            $.each(_acciones_peaton, function (key1, value1) {
                                console.log('aciones' + '' + value1);
                                if (value1 == 'USO DEL CELULAR')
                                    $('#ddl_accionesPeatonEdit > option[value="1"]').attr('selected', 'selected');
                                //$('#ddl_accionesPeatonEdit').val('1').attr('selected', 'selected');
                                if (value1 == "USO DE ELEMENTOS DISTRACTORES")
                                    //$('#ddl_accionesPeatonEdit').val('2').prop("checked", true);
                                    $('#ddl_accionesPeatonEdit > option[value="2"]').attr('selected', 'selected');
                                if (value1 == 'CRUCE DE VÍA A LUGARES NO AUTORIZADO')
                                    $('#ddl_accionesPeatonEdit > option[value="3"]').attr('selected', 'selected');
                                if (value1 == "PRESUNCIÓN DE INGESTA DE ALCOHOL")
                                    $('#ddl_accionesPeatonEdit > option[value="4"]').attr('selected', 'selected');
                                if (value1 == "CRUCE DE VÍA SIN PREFERENCIA")
                                    $('#ddl_accionesPeatonEdit > option[value="5"]').attr('selected', 'selected');
                                if (value1 == "PRESUNCIÓN DE INGESTA  DE SUSTANCIAS ESTUPERFACIENTES O PSICOTRÓPICAS Y/O MEDICAMENTOS")
                                    $('#ddl_accionesPeatonEdit > option[value="6"]').attr('selected', 'selected');

                            });
                           
                            
                            if (value.tipidenvicinv.trim() === 'CÉDULA')
                                $('#btnBuscarVictEdit').css("display", "block");
                            else
                                $('#btnBuscarVictEdit').css("display", "none");

                            $("#ddl_vhlVictimasEdit").val(value.codveh);
                            ///$("#ddl_vhl_materialPeligrosoEdit").val(value.cilvehinv);
                            $("#ddl_tipo_servicio_vhlEdit").val(value.codser);
                            if (value.casvicinv === true)
                                $('#ddl_uso_cascoEdit').val('1');
                            else
                                $('#ddl_uso_cascoEdit').val('2');
                            if (value.cinvicinv === true)
                                $('#ddl_uso_cinturonEdit').val('1');
                            else
                                $('#ddl_uso_cinturonEdit').val('2');
                            if (value.conalcvicinv === true)
                                $('#ddl_consumo_alcoholEdit').val('1');
                            else
                                $('#ddl_consumo_alcoholEdit').val('2');
                           
                          
                            ($('#tbGudarVictimasEdit').css("display", "none"));
                            ($('#tbModificarVictimas').css("display", "block"));

                            
                            
                        }
                        else {
                            mensaje = "No existe resultados en la busqueda.";
                            alert(mensaje.toString());
                        }

                    });

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus + ": " + XMLHttpRequest.responseText);
                }

            });
        }
        $('#respuesta-ej4').toggle();
        $('#respuesta-ej3').toggle();
    });



    $('.btnModalVictimaDelete').click(function (eve) {
        
        inicializaVictimasEdit();
        ($('#panelMensajeVictimasEdit').css("display", "none"));
        $("#ddl_tipoIdentificacionEdit").val('-1');
        //  $("#ddl_accionesPeatonEdit").val('-1');
        $('#divAccionesPeatonEdit').css("display", "none");
        $("#cb_1E").prop("checked", false);
        $("#cb_2E").prop("checked", false);
        $("#cb_3E").prop("checked", false);
        $("#cb_4E").prop("checked", false);
        $("#cb_5E").prop("checked", false);
        $("#cb_6E").prop("checked", false);
        $("#cb_7E").prop("checked", false);
        $("#cb_8E").prop("checked", false);
        $("#cb_9E").prop("checked", false);
        $("#cb_10E").prop("checked", false);
        $("#cb_11E").prop("checked", false);
        $("#cb_12E").prop("checked", false);
        $("#cb_13E").prop("checked", false);
        $('#ddl_consumo_alcoholEdit').removeAttr("disabled");
        $('#ddl_uso_cascoEdit').removeAttr("disabled");
        $('#ddl_uso_cinturonEdit').removeAttr("disabled")
        var codSin = $('#tbSiniestroModEdit').val();
        console.log($(this).data('id'));
        if ($(this).data('id') !== '') {
            var idVict = $(this).data('id');
            if (confirm('Va a eliminar la víctima involucrada. Desea continuar !') == true) {
                $.ajax({
                    type: "GET",
                    url: "../eliminaVictimas",
                    data: { codVictima: idVict, codsin: codSin },
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (jsonResult) {
                        if (jsonResult === "-1") {
                            alert("Error al eliminar el registro de la víctma involucrada. Intente nuevamente.");
                        }
                        if (jsonResult.length > -1 && jsonResult !== "-1") {
                            var mensaje = 'Víctima eliminada correctamente.';
                            ($('#divMensajeVictimasEdit').attr("class", "alert alert-success smaller"));
                            ($('#mensajeLabelVictimasEdit').html(mensaje.toString()));

                            ($('#panelMensajeVictimasEdit').css("display", "block"));
                            TablaVictimas(codSin);
                        }
                        else {
                            alert("Error al eliminar el registro de la víctma involucrada. Intente nuevamente.");
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("Error al eliminar el registro de la víctma involucrada. Intente nuevamente.");
                    }

                });


            }
        }
    
    });
   
    $('#ddl_tipoIdentificacionEdit').change(function () {
        if ($('#ddl_tipoIdentificacionEdit').val() === '1') {
            ($('#tb_IdentificacionVictimaEdit').removeAttr('disabled'));
            ($('#tb_IdentificacionVictimaEdit').val('0'));
            $('#btnBuscarVictEdit').css('display', 'block');
        }

        else if ($('#ddl_tipoIdentificacionEdit').val() === '4') {
            ($('#tb_IdentificacionVictimaEdit').val('0'));
            ($('#tb_IdentificacionVictimaEdit').attr('disabled', 'disabled'));
            ($('#tb_edadVictimaEdit').val('-1'));
            ($('#ddl_generoVictimaEdit').val('M'));
            // ($('#btnBuscarVict').css("display", "none"));
            ($('#ddl_tipoParticipanteEdit').val('-1'));
            $('#btnBuscarVictEdit').css('display', 'none');


        }
        else if ($('#ddl_tipoIdentificacionEdit').val() === '2') {
            ($('#ddl_tipoParticipanteEdit').val('-1'));
            ($('#ddl_tipoIdentificacionEdit').removeAttr('disabled'));
            ($('#tb_edadVictimaEdit').val('0'));
            ($('#ddl_generoVictimaEdit').val('-1'));
            $('#btnBuscarVictEdit').css('display', 'block');
        }
        else if ($('#ddl_tipoIdentificacionEdit').val() === '-1') {
            ($('#ddl_tipoParticipanteEdit').val('-1'));
            ($('#tb_IdentificacionVictimaEdit').removeAttr('disabled'));
            $('#btnBuscarVictEdit').css('display', 'none');
        }
        else {
            ($('#tb_IdentificacionVictimaEdit').val(''));
            ($('#tb_edadVictimaEdit').val('0'));
            ($('#ddl_generoVictimaEdit').val('-1'));
            ($('#tb_IdentificacionVictimaEdit').removeAttr('disabled'));
            ($('#ddl_tipoParticipanteEdit').val('-1'));
            $('#btnBuscarVictEdit').css('display', 'none');
        }
    });


    $('#tb_IdentificacionVictimaEdit').keypress(function (event) {

        if ($('#ddl_tipoIdentificacionEdit').val() === '1') {

            $("#tb_IdentificacionVictimaEdit").attr('maxlength', 10);
            $('#tb_IdentificacionVictimaEdit').validCampoFranz('01234567890');
        }
        else if ($('#ddl_tipoIdentificacionEdit').val() === '2') {

            $("#tb_IdentificacionVictimaEdit").attr('maxlength', 10);
            $('#tb_IdentificacionVictimaEdit').validCampoFranz('01234567890');
        }
        else if ($('#ddl_tipoIdentificacionEdit').val() === '3') {
            $("#tb_IdentificacionVictimaEdit").attr('maxlength', 20);
            if ((event.charCode < 97 || event.charCode > 122) && (event.charCode < 65 || event.charCode > 90)) {
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

    $('#ddl_vhlVictimasEdit').change(function () {
        
        if ($('#ddl_vhlVictimasEdit').val() !== '-1') {
            var codVehiculoVictima = ($('#ddl_vhlVictimasEdit').val());
            $.ajax({
                type: "get",
                url: '../ObtenerTipoVehiculoVictimas',
                data: { codVehiculo: codVehiculoVictima },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (retorno) {
                    console.log('retorno tipo vhl victimas' + ' ' + retorno.toString());
                    if (retorno.toString() === '0') {
                        mensaje = 'Error al verificar el tipo de vehiculo.';
                        ($('#mensajeLabelVictimasEdit').html(mensaje.toString()));
                        ($('#divMensajeVictimasEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
                    }
                    else {
                        if (retorno.toString() === '8') {
                            $("#ddl_uso_cascoEdit").val('1');
                            $("#ddl_uso_cinturonEdit").val('2');
                            $("#cb_2E").attr("disabled", "disabled");
                            $("#cb_3E").attr("disabled", "disabled");
                            $("#cb_5E").attr("disabled", "disabled");
                            $("#cb_6E").attr("disabled", "disabled");
                            $("#cb_8E").attr("disabled", "disabled");
                            $("#cb_9E").attr("disabled", "disabled");
                            $("#cb_10E").attr("disabled", "disabled");
                            $("#cb_11E").attr("disabled", "disabled");
                            $("#cb_12E").attr("disabled", "disabled");
                            $("#cb_13E").attr("disabled", "disabled");
                            $("#ddl_uso_cascoEdit").removeAttr('disabled');
                            $("#ddl_uso_cinturonEdit").attr("disabled", "disabled");
                        }
                        else if (retorno.toString() === '16') {
                            
                            $("#ddl_uso_cascoEdit").val('1');
                            $("#ddl_uso_cinturonEdit").val('2');
                            $("#cb_2E").attr("disabled", "disabled");
                            $("#cb_3E").attr("disabled", "disabled");
                            $("#cb_5E").attr("disabled", "disabled");
                            $("#cb_6E").attr("disabled", "disabled");
                            $("#cb_8E").attr("disabled", "disabled");
                            $("#cb_9E").attr("disabled", "disabled");
                            $("#cb_10E").attr("disabled", "disabled");
                            $("#cb_11E").attr("disabled", "disabled");
                            $("#cb_12E").attr("disabled", "disabled");
                            $("#cb_13E").attr("disabled", "disabled");
                            $("#ddl_uso_cascoEdit").removeAttr('disabled');
                            $("#ddl_uso_cinturonEdit").attr("disabled", "disabled");
                        }
                        else {
                            
                            $("#ddl_uso_cascoEdit").val('2');
                            $("#ddl_uso_cinturonEdit").val('1');
                            $("#cb_1E").removeAttr('disabled');
                            $("#cb_2E").removeAttr('disabled');
                            $("#cb_3E").removeAttr('disabled');
                            $("#cb_4E").removeAttr('disabled');
                            $("#cb_5E").removeAttr('disabled');
                            $("#cb_6E").removeAttr('disabled');
                            $("#cb_7E").removeAttr('disabled');
                            $("#cb_8E").removeAttr('disabled');
                            $("#cb_9E").removeAttr('disabled');
                            $("#cb_10E").removeAttr('disabled');
                            $("#cb_11E").removeAttr('disabled');
                            $("#cb_12E").removeAttr('disabled');
                            $("#cb_13E").removeAttr('disabled');

                            $("#ddl_uso_cascoEdit").val('2');
                            $("#ddl_uso_cascoEdit").attr("disabled", "disabled");
                            $("#ddl_uso_cinturonEdit").removeAttr('disabled');
                        }
                        ($('#tbTipoVhlVictimaEdit').val(retorno.toString()));

                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus + ": " + XMLHttpRequest.responseText);
                }

            });



        }
    });
    $('#tbModificarVictimas').click(function () {

        ($('#divMensajeVictimasEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
        var codSin = ($('#tbSiniestroModEdit').val());
        var codVicitma = ($('#tb_codVictimasEdit').val());
        var codVhlVictima = ($('#ddl_vhlVictimasEdit').val());
        var tipoIdentificacion = ($('#ddl_tipoIdentificacionEdit option:selected').text());
        var numeroIdentificacion = ($('#tb_IdentificacionVictimaEdit').val());
        var edadVictima = ($('#tb_edadVictimaEdit').val());
        var generoVictima = ($('#ddl_generoVictimaEdit').val());
        var condicionVictima24 = ($('#ddl_condicionVictimas24Edit option:selected').text());
        var condicionVictima30 = 'NO DETERMINADO';
        var tipoParticipanteVictima = ($('#ddl_tipoParticipanteEdit option:selected').text());
        var posicionPlazaVictima = "";//($('#ddl_posicionPlazaEdit option:selected').text());
        var usoCasoVictimaEdit = $("#ddl_uso_cascoEdit").val();
        var usoCinturonVictimaEdit = $("#ddl_uso_cinturonEdit").val();
        var consumoAlcoholVictimaEdit = $("#ddl_consumo_alcoholEdit").val();
        var accionesPeaton = ($('#ddl_accionesPeatonEdit option:selected').text());
        var nombres_victima = ($('#tb_NombresVictimaEdit').val());
        if (tipoParticipanteVictima === 'PEATÓN' || tipoParticipanteVictima === 'CONDUCTOR') {
            $('#ddl_accionesPeatonEdit').each(function () {

                accionesPeaton = ($('#ddl_accionesPeatonEdit option:selected').text());
            });
            console.log(accionesPeaton);
        }
        if ($('#cb_1E').prop("checked") === true) posicionPlazaVictima = "FRONTAL IZQUIERDO";
        if ($('#cb_2E').prop("checked") === true) posicionPlazaVictima = "FRONTAL CENTRAL";
        if ($('#cb_3E').prop("checked") === true) posicionPlazaVictima = "FRONTAL DERECHO";
        if ($('#cb_4E').prop("checked") === true) posicionPlazaVictima = "CENTRAL IZQUIERDO";
        if ($('#cb_5E').prop("checked") === true) posicionPlazaVictima = "CENTRAL";
        if ($('#cb_6E').prop("checked") === true) posicionPlazaVictima = "CENTRAL DERECHO";
        if ($('#cb_7E').prop("checked") === true) posicionPlazaVictima = "TRASERO IZQUIERDO";
        if ($('#cb_8E').prop("checked") === true) posicionPlazaVictima = "TRASERO CENTRAL";
        if ($('#cb_9E').prop("checked") === true) posicionPlazaVictima = "TRASERO DERECHO";
        if ($('#cb_10E').prop("checked") === true) posicionPlazaVictima = "BALDE";
        if ($('#cb_11E').prop("checked") === true) posicionPlazaVictima = "DE PIE";
        if ($('#cb_12E').prop("checked") === true) posicionPlazaVictima = "OTROS";
        if ($('#cb_13E').prop("checked") === true) posicionPlazaVictima = "NIÑO EN BRAZOS";
        console.log(usoCinturonVictimaEdit);
        var mensaje = "";
        if (codVicitma === '0' || codVicitma.toString().trim() === '') {
            mensaje = 'Debe seleccioinar un registro para editar.';
            ($('#divMensajeVictimasEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
        }
        else if (codVhlVictima === '' || codVhlVictima === '-1' || codVhlVictima === '0') {
            mensaje = 'Seleccione el Vehiculo involucrado.';
            ($('#divMensajeVictimasEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
        }
        else if (tipoIdentificacion === '' || tipoIdentificacion === 'SELECCIONAR' || tipoIdentificacion === '-1') {
            mensaje = 'Seleccione el tipo de identificacion.';
            ($('#divMensajeVictimasEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
        }
        else if ((numeroIdentificacion.trim() === '' || parseInt(numeroIdentificacion) < 10) && tipoIdentificacion !== 'NO IDENTIFICADO') {
            mensaje = 'Ingrese el numero de identificacion.';
            ($('#divMensajeVictimasEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
        }
        else if ((edadVictima.trim() === '' ) ) {
            mensaje = 'Ingrese la edad de la víctima.';
            ($('#divMensajeVictimasEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
        }
        else if ((generoVictima.trim() === '' || generoVictima === '-1' || generoVictima === null)) {
            mensaje = 'Seleccione el sexo de la víctima.';
            ($('#divMensajeVictimasEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
        }
        else if ((condicionVictima24.trim() === '' || condicionVictima24 === 'SELECCIONAR')) {
            mensaje = 'Seleccione la Condición Víctima 24h.';
            ($('#divMensajeVictimasEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
        }
        else if ((usoCasoVictimaEdit.trim() === '' || usoCasoVictimaEdit === '-1')) {
            mensaje = 'Seleccione el uso del casco';
            ($('#divMensajeVictimas').attr("class", "alert alert-block alert-danger fade in smaller"));
        }
        else if ((usoCinturonVictimaEdit.trim() === '' || usoCinturonVictimaEdit === '-1')) {
            mensaje = 'Seleccione el uso del cinturon';
            ($('#divMensajeVictimas').attr("class", "alert alert-block alert-danger fade in smaller"));
        }
        else if ((consumoAlcoholVictimaEdit.trim() === '' || consumoAlcoholVictimaEdit === '-1')) {
            mensaje = 'Seleccione si tiene o no cunsumo de alcohol';
            ($('#divMensajeVictimas').attr("class", "alert alert-block alert-danger fade in smaller"));
        }
        else if ((tipoParticipanteVictima.trim() === '' || tipoParticipanteVictima === 'SELECCIONAR')) {
            mensaje = 'Seleccione el tipo participante';
            ($('#divMensajeVictimasEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
        }
        else if ((posicionPlazaVictima.trim() === '' || posicionPlazaVictima === 'SELECCIONAR')) {
            mensaje = 'Seleccione la Posición de la Plaza.';
            ($('#divMensajeVictimasEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
        }
        else if ((tipoParticipanteVictima === 'PEATÓN' ||  tipoParticipanteVictima === 'CONDUCTOR') && (accionesPeaton === '------SELECCIONAR------' || accionesPeaton === '') && ($("#divAccionesPeatonEdit").is(":visible") === true)) {
            mensaje = 'Seleccione las acciónes del peatón.';
            ($('#divMensajeVictimasEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
            console.log(accionesPeaton);
        }

        else {
                      //var accionesPeatones = "";$("#divAccionesPeaton").is(":visible")
                    if (tipoParticipanteVictima === 'PEATÓN' || tipoParticipanteVictima === 'CONDUCTOR') {
                        $('#ddl_accionesPeatonEdit').each(function () {
                            accionesPeaton = $('#ddl_accionesPeatonEdit').val();

                        });

                    }
            console.log(accionesPeaton);
            var ccc = accionesPeaton.toString();
            var jsonStringAccion = JSON.stringify(accionesPeaton);
            
            var fecha_actual = Date.toLocaleString();
            var fechaSin = ($('#tbfechaSiniestroEdit').val());
            fecha_actual = new Date().toJSON().slice(0, 10)
            console.log('fecha_actual ' + '' + fecha_actual);
            console.log('fechaSin ' + '' + fechaSin);
            var anio_max = fechaSin.split('-')[0];
            var mes_max = parseInt(fechaSin.split('-')[1]) + 1;
            var dia_max = '10';
            var dia_min = '01';
            var fecha_max = anio_max + '-' + mes_max + '-' + dia_max;
            var fecha_min = anio_max + '-' + fechaSin.split('-')[1] + '-' + dia_min;
            console.log('fecha_max ' + '' + fecha_max);
            console.log('fecha_min ' + '' + fecha_min);
            fecha_max = fecha_max.split('-');
            fecha_actual = fecha_actual.split('-');
            fechaSin = fechaSin.split('-');
            fecha_min = fecha_min.split('-');
            fecha_max = new Date(fecha_max[0], fecha_max[1] - 1, fecha_max[2]).valueOf();
            fecha_actual = new Date(fecha_actual[0], fecha_actual[1] - 1, fecha_actual[2]).valueOf();
            fechaSin = new Date(fechaSin[0], fechaSin[1] - 1, fechaSin[2]).valueOf();
            fecha_min = new Date(fecha_min[0], fecha_min[1] - 1, fecha_min[2]).valueOf();
            console.log('------------------------------------------');
            console.log('fecha_max ' + '' + fecha_max);
            console.log('fecha_actual ' + '' + fecha_actual);
            console.log('fechaSin ' + '' + fechaSin);
            console.log('fecha_min ' + '' + fecha_min);
            var _fecha_actual_sistema = new Date().toJSON().slice(0, 10);
            _fecha_actual_sistema = new Date(_fecha_actual_sistema).valueOf();
            if (fecha_max >= _fecha_actual_sistema && fechaSin <= fecha_max && fechaSin >= fecha_min) {


                $.ajax({
                    type: "get",
                    url: '../JsonModificaVictimasInvolucradas',
                    data: { codVictima: codVicitma, tipidenvicinv: tipoIdentificacion, numidenvicinv: numeroIdentificacion, edavicinv: edadVictima, genvicinv: generoVictima, convicinv24: condicionVictima24, convicinv30: condicionVictima30, tipparvicinv: tipoParticipanteVictima, casvicinv: usoCasoVictimaEdit, cinvicinv: usoCinturonVictimaEdit, posvicinv: posicionPlazaVictima, conalcvicinv: consumoAlcoholVictimaEdit, codveh: codVhlVictima, desaccpea: jsonStringAccion, ccc: ccc, codSiniestro: codSin, nombreVictima: nombres_victima },
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (jsonResult) {
                        console.log('modificacion victimas' + ' ' + jsonResult);
                        if (jsonResult.length === 0) {
                            mensaje = 'Error al modificar los datos.';
                            ($('#mensajeLabelVictimasEdit').html(mensaje.toString()));
                            ($('#divMensajeVictimasEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
                        }
                        else if (jsonResult.length > 0) {
                            mensaje = 'Víctima modificada correctamente.';
                            ($('#mensajeLabelVictimasEdit').html(mensaje.toString()));
                            ($('#divMensajeVictimasEdit').attr("class", "alert alert-success fade in smaller"));
                            inicializaVictimasEdit();
                            $('#respuesta-ej3').toggle();
                            $('#respuesta-ej4').toggle();
                            ($('#panelMensajeVictimasEdit').css("display", "block"));

                            TablaVictimas(codsin);
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert(textStatus + ": " + XMLHttpRequest.responseText);
                    }

                });
            }
            else {
                mensaje = "No puede modificar el registro,la fecha del  siniestro se encuentra fuera del rango permitido.";
            }
        }
        console.log(mensaje.toString());
        ($('#mensajeLabelVictimasEdit').html(mensaje.toString()));
        ($('#panelMensajeVictimasEdit').css("display", "block"));
        //console.log('para ingresar a la funcion');
        // var _codsin = ($('#tb_codsinVictimas').val());
        //ObtenerTraerNumLesionadosFallecido(_codsin);
    });
    
    
 /****************************************************************************
* FIN VICTIMAS
*************************************************************************/



    $('#ddl_provinciaEdit').change(function () {
        var codprov = $('#ddl_provinciaEdit').val();
        var params = new Object();
        params.codprov = $('#ddl_provinciaEdit').val();
        params = JSON.stringify(params);
        var select = $('#ddl_cantonEdit');
        select.find('option').remove().end().append('<option value="-1">SELECCIONAR</option>').val('-1');
        $.ajax({
            type: "get",
            url: '../CargarCantonesPorProvincia',
            data: { codprov: codprov },
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

    $('#ddl_cantonEdit').change(function () {
        var codprov = $('#ddl_provinciaEdit').val();
        var codcant = $('#ddl_cantonEdit').val();
        //var params = new Object();
        //params.codprov = $('#ddl_provinciaEdit').val();
        //params.codcant = $('#ddl_cantonEdit').val();
        //params = JSON.stringify(params);
        var select = $('#ddl_parroquiaEdit');
        select.find('option').remove().end().append('<option value="-1">SELECCIONAR</option>').val('-1');
        console.log(codprov + ' ' + codcant);
        $.ajax({
            type: "get",
            url: '../CargarParroquiasPorCantonesEdit',
            data: { codcant: codcant, codprov: codprov },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (jsonResult) {
                console.log('paso para  parroquia');
                console.log(jsonResult);
                $.each(jsonResult, function (key, value) {
                    select.append('<option value="' + value.value + '">' + value.text + '</option>').val(value.value);
                });
                select.val('-1');
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(errorThrown + ' :' + textStatus);
            }

        });

    });

    $('#ddl_parroquiaEdit').click(function () {
        var codprov = $('#ddl_provinciaEdit').val();
        var codcant = $('#ddl_cantonEdit').val();
        var params = new Object();
        params.codprov = $('#ddl_provinciaEdit').val();
        params.codcant = $('#ddl_cantonEdit').val();
        params = JSON.stringify(params);

        var selectD = $('#ddl_distritoSiniestroEdit');
        //ddl_distritoSiniestro
        selectD.find('option').remove().end().append('<option value="-1">SELECCIONAR</option>').val('-1');
        $.ajax({
            type: "get",
            url: '../ObtenerlistaDistritosEdit',
            data: { codProv: codprov, codcant: codcant },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (jsonResult) {
                console.log(jsonResult);
                $.each(jsonResult, function (key, value) {
                    selectD.append('<option value="' + value.value + '">' + value.text + '</option>').val(value.value);
                });
                selectD.val('-1');
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(errorThrown);
            }

        });

    });
    $('#ddl_distritoSiniestroEdit').click(function () {
        var codpar = $('#ddl_parroquiaEdit').val();
        var codcant = $('#ddl_cantonEdit').val();
        var params = new Object();
        params.codpar = $('#ddl_parroquiaEdit').val();
        params.codcant = $('#ddl_cantonEdit').val();
        params = JSON.stringify(params);

        var selectD = $('#ddl_circuitoEdit');
        //ddl_distritoSiniestro
        selectD.find('option').remove().end().append('<option value="-1">SELECCIONAR</option>').val('-1');
        $.ajax({
            type: "get",
            url: '../ObtenerlistaCircuitosEdit',
            data: { codCant: codcant, codPar: codpar },
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (jsonResult) {
                console.log(jsonResult);
                $.each(jsonResult, function (key, value) {
                    selectD.append('<option value="' + value.value + '">' + value.text + '</option>').val(value.value);
                });
                selectD.val('-1');
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert(errorThrown);
            }

        });

    });


    $('#ddl_tipoViaEdit').change(function () {
        //alert($('#ddl_tipoVia').val().toString());
        if ($('#ddl_tipoViaEdit').val() === '1' || $('#ddl_tipoViaEdit').val() === '2' || $('#ddl_tipoViaEdit').val() === '3') {
            $('#ddl_materialSuperficieEdit').val('2');
        }
        else if ($('#ddl_tipoViaEdit').val() === '4') {
            $('#ddl_materialSuperficieEdit').val('1');
        }
        else if ($('#ddl_tipoViaEdit').val() === '5') {
            $('#ddl_materialSuperficieEdit').val('4');
        }
        else {
            $('#ddl_materialSuperficieEdit').val('0');
        }
    });

    $('#ddl_tipoViaEdit').change(function () {
        //alert($('#ddl_tipoVia').val().toString());
        if ($('#ddl_tipoViaEdit').val() === '1' || $('#ddl_tipoViaEdit').val() === '2') {
            $('#ddl_numCarrilesEdit').val('4');
        }
        else if ($('#ddl_tipoViaEdit').val() === '3' || $('#ddl_tipoViaEdit').val() === '4' || $('#ddl_tipoViaEdit').val() === '5') {
            $('#ddl_numCarrilesEdit').val('2');
        }
        else if ($('#ddl_tipoViaEdit').val() === '6' || $('#ddl_tipoViaEdit').val() === '7' || $('#ddl_tipoViaEdit').val() === '8') {
            $('#ddl_numCarrilesEdit').val('1');
        }
        else {
            $('#ddl_numCarrilesEdit').val('-1');
        }
    });


    /****************************************************************************
    * INICIO ACCIONES PEATON
    *************************************************************************/
    $('.btnModalAccionesPeatonEdit').click(function (eve) {
        console.log(pathname);
        ($('#panelMensajePeatonEdit').css("display", "none"));
        $("#ddl_accionPeatonEdit").val('-1');
        console.log('codigo ACCION');
        console.log($(this).data('id'));
        if ($(this).data('id') !== '') {
            var idAccionPeaton = $(this).data('id');
            $.ajax({
                type: "get",
                url: '../JsonAccionesPeatonPorCodigo',
                data: { id: idAccionPeaton },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (jsonResult) {
                    console.log(jsonResult);
                    $.each(jsonResult, function (key, value) {
                        if (value.codaccpea.toString() !== '') {
                            $("#tb_codAccionesPeatonEdit").val(value.codaccpea);
                            $('#tb_codAccionesPeatonEdit').attr("disabled", "disabled");
                            $("#ddl_accionPeatonEdit").find('option').each(function (i, opt) {
                                if (opt.text.trim() === value.desaccpea.trim()) {
                                    $("#ddl_accionPeatonEdit").val(opt.value);
                                }
                            });
                            $("#ddl_victimasAccionesPeatonEdit").val(value.codvicinv);




                        }
                        else {
                            mensaje = "No existe resultados en la busqueda.";
                            alert(mensaje.toString());
                        }

                    });

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus + ": " + XMLHttpRequest.responseText);
                }

            });
        }
    });

    $('#tbModificarAccionesPeaton').click(function () {

        //($('#panelMensajePeatonEdit').css("display", "none"));


        var codAccionPeaton = ($('#tb_codAccionesPeatonEdit').val());
        var codPeaton = $('#ddl_victimasAccionesPeatonEdit').val();
        var codAccion = ($('#ddl_accionPeatonEdit option:selected').text());
        ($('#divMensajePeatonEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
        var mensaje = '';
        if (codPeaton === '' || codPeaton === '-1') {
            mensaje = 'Seleccione el peaton';
            ($('#mensajeLabelPeatonEdit').html(mensaje.toString()));
            ($('#mensajeLabelPeatonEdit').css("display", "block"));
        }
        else if (codAccion === '' || codAccion === 'SELECCIONAR') {
            mensaje = 'Seleccione la accion del peaton.';
            ($('#mensajeLabelPeatonEdit').html(mensaje.toString()));
            ($('#mensajeLabelPeatonEdit').css("display", "block"));
        }
        else {
            var fecha_actual = Date.toLocaleString();
            var fechaSin = ($('#tbfechaSiniestroEdit').val());
            fecha_actual = new Date().toJSON().slice(0, 10)
            console.log('fecha_actual ' + '' + fecha_actual);
            console.log('fechaSin ' + '' + fechaSin);
            var anio_max = fechaSin.split('-')[0];
            var mes_max = parseInt(fechaSin.split('-')[1]) + 1;
            var dia_max = '10';
            var dia_min = '01';
            var fecha_max = anio_max + '-' + mes_max + '-' + dia_max;
            var fecha_min = anio_max + '-' + fechaSin.split('-')[1] + '-' + dia_min;
            console.log('fecha_max ' + '' + fecha_max);
            console.log('fecha_min ' + '' + fecha_min);
            fecha_max = fecha_max.split('-');
            fecha_actual = fecha_actual.split('-');
            fechaSin = fechaSin.split('-');
            fecha_min = fecha_min.split('-');
            fecha_max = new Date(fecha_max[0], fecha_max[1] - 1, fecha_max[2]).valueOf();
            fecha_actual = new Date(fecha_actual[0], fecha_actual[1] - 1, fecha_actual[2]).valueOf();
            fechaSin = new Date(fechaSin[0], fechaSin[1] - 1, fechaSin[2]).valueOf();
            fecha_min = new Date(fecha_min[0], fecha_min[1] - 1, fecha_min[2]).valueOf();
            console.log('------------------------------------------');
            console.log('fecha_max ' + '' + fecha_max);
            console.log('fecha_actual ' + '' + fecha_actual);
            console.log('fechaSin ' + '' + fechaSin);
            console.log('fecha_min ' + '' + fecha_min);
            var _fecha_actual_sistema = new Date().toJSON().slice(0, 10);
            _fecha_actual_sistema = new Date(_fecha_actual_sistema).valueOf();
           // if (fecha_max >= _fecha_actual_sistema && fechaSin <= fecha_max && fechaSin >= fecha_min) {


                $.ajax({

                    type: "get",
                    url: '../JsonModificarAccionesPeaton',
                    data: { codaccion: codAccionPeaton, desaccpea: codAccion, codvicinv: codPeaton },
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (retorno) {
                        console.log('retorno' + ' ' + retorno.toString());
                        if (retorno.toString() === "0") {
                            mensaje = 'No se puede registrar la infromación';
                            ($('#mensajeLabelPeatonEdit').html(mensaje.toString()));

                            ($('#panelMensajePeatonEdit').css("display", "block"));


                        }
                        else if (retorno.toString() !== "0") {

                            mensaje = "Información registrada correctamente.";
                            console.log(mensaje.toString());
                            ($('#mensajeLabelPeatonEdit').html(mensaje.toString()));
                            ($('#divMensajePeatonEdit').attr("class", "alert alert-success smaller"));
                            ($('#ddl_accionPeatonEdit').val('-1'));
                            ($('#panelMensajePeatonEdit').css("display", "block"));
                            //location.reload();
                        }

                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert(textStatus + ": " + XMLHttpRequest.responseText);
                    }
                });
            //}
            //else {
            //    mensaje = "No puede modificar el registro, la fecha del siniestro se encuentra fuera del rango permitido.";
            //    ($('#mensajeLabelPeatonEdit').html(mensaje.toString()));

            //    ($('#panelMensajePeatonEdit').css("display", "block"));
            //}
        }
    });
    /****************************************************************************
    *FIN ACCIONES PEATON
    *************************************************************************/
    //$('.btnModalAccionesPeatonEdit').click(function (eve) {
    //    console.log(pathname);
    //    ($('#panelMensajePeatonEdit').css("display", "none"));
    //    $("#ddl_accionPeatonEdit").val('-1');
    //    console.log('codigo ACCION');
    //    console.log($(this).data('id'));
    //    if ($(this).data('id') !== '') {
    //        var idAccionPeaton = $(this).data('id');
    //        $.ajax({
    //            type: "get",
    //            url: '../JsonAccionesPeatonPorCodigo',
    //            data: { id: idAccionPeaton },
    //            contentType: "application/json; charset=utf-8",
    //            dataType: "json",
    //            success: function (jsonResult) {
    //                console.log(jsonResult);
    //                $.each(jsonResult, function (key, value) {
    //                    if (value.codaccpea.toString() !== '') {
    //                        $("#tb_codAccionesPeatonEdit").val(value.codaccpea);
    //                        $('#tb_codAccionesPeatonEdit').attr("disabled", "disabled");
    //                        $("#ddl_accionPeatonEdit").find('option').each(function (i, opt) {
    //                            if (opt.text.trim() === value.desaccpea.trim()) {
    //                                $("#ddl_accionPeatonEdit").val(opt.value);
    //                            }
    //                        });
    //                        $("#ddl_victimasAccionesPeatonEdit").val(value.codvicinv);




    //                    }
    //                    else {
    //                        mensaje = "No existe resultados en la busqueda.";
    //                        alert(mensaje.toString());
    //                    }

    //                });

    //            },
    //            error: function (XMLHttpRequest, textStatus, errorThrown) {
    //                alert(textStatus + ": " + XMLHttpRequest.responseText);
    //            }

    //        });
    //    }
    //});

    /****************************************************************************
    * DANIOS TERCEROS
    *************************************************************************/
    $('.btnModalDaniosTercerosEdit').click(function (eve) {
        console.log(pathname);
        ($('#panelMensajeEdit').css("display", "none"));
        $("#ddl_tipoDanioTerceroEdit").val('-1');
        console.log('codigo DANIO');
        console.log($(this).data('id'));
        if ($(this).data('id') !== '') {
            var codAccion = $(this).data('id');
            $.ajax({
                type: "get",
                url: '../JsonlistaVistaDaniosTercerosPorCodigo',
                data: { id: codAccion },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (jsonResult) {
                    console.log(jsonResult);
                    $.each(jsonResult, function (key, value) {
                        if (value.coddater.toString() !== '' && value.coddater.toString() !== '0') {
                            $("#tb_codDanioTercero").val(value.coddater);
                            $('#tb_codDanioTercero').attr("disabled", "disabled");
                            $("#ddl_tipoDanioTerceroEdit").val(value.codtipdater)
                            $("#tb_obsdaterEdit").val(value.obsdater);
                           
                            ($('#tbGudarDanioTerceroEdit').css("display", "none"));
                            ($('#tbModificaDanioTercero').css("display", "block"));

                        }
                        else {
                            mensaje = "No existe resultados en la busqueda.";
                            alert(mensaje.toString());
                        }

                    });
                   
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus + ": " + XMLHttpRequest.responseText);
                }

            });
            $('#respuesta-ej5').toggle();
            $('#respuesta-ej6').toggle();
            ($('#panelMensaje').css("display", "none"));
            //($('#tb_codsinDanioTercero').val('0'));
            ($('#tb_obsdaterEdit').val(''));
            ($('#ddl_tipoDanioTerceroEdit').val('-1'));
            var codsinDanioTercero = ($('#tbSiniestroModEdit').val());
         
        }
    });

    $('.btnModalDaniosTercerosDelete').click(function (eve) {
        var codSin = ($('#tbSiniestroModEdit').val());
        ($('#panelMensaje').css("display", "block"));
        $("#ddl_tipoDanioTerceroEdit").val('-1');
        console.log('codigo DANIO');
        console.log($(this).data('id'));
        if ($(this).data('id') !== '') {
            if (confirm('Va a eliminar el registro de daño a tercero. Desea continuar!') === true) {
                var _codDanio = $(this).data('id');
                $.ajax({
                    type: "get",
                    url: '../eliminaDaniosTerceros',
                    data: { codDanio: _codDanio, codsin: codSin },
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (jsonResult) {
                        //console.log('entre eliminacion'+" "+jsonResult);
                        if (jsonResult === '1') {
                            mensaje = "Daño a tercero eliminado correctamente.";
                            ($('#mensajeLabel').html(mensaje.toString()));
                            ($('#divMensaje').attr("class", "alert alert-success smaller"));
                            ($('#panelMensaje').css("display", "block"));
                            TablaDanios(codSin);
                        }
                        else {
                            mensaje = "Error al eliminar el registro, Intente nuevamente.";
                            alert(mensaje.toString());
                        }
                       


                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert(textStatus + ": " + XMLHttpRequest.responseText);
                    }

                });
            }
            ($('#panelMensaje').css("display", "none"));
            ($('#tb_obsdaterEdit').val(''));
            ($('#ddl_tipoDanioTerceroEdit').val('-1'));
    

        }
    });



    $('#tbModificaDanioTercero').click(function () {
        //alert('modificar');
        ($('#divMensaje').attr("class", "alert alert-block alert-danger fade in smaller"));
        ($('#panelMensaje').css("display", "none"));
        var mensaje = '';
        var codDanioTercero = ($('#tb_codDanioTercero').val());
        var codTipoDanioTercero = ($('#ddl_tipoDanioTerceroEdit').val());
        var observacionDanioTercero = ($('#tb_obsdaterEdit').val());
        if (codDanioTercero === '' || codDanioTercero === '0') {
            mensaje = 'Debe seleccionar un registro a modificar';
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

            //console.log(params);
            //console.log(($('#tb_codsinDanioTercero').val()));

            var fecha_actual = Date.toLocaleString();
            var fechaSin = ($('#tbfechaSiniestroEdit').val());
            fecha_actual = new Date().toJSON().slice(0, 10)
            console.log('fecha_actual ' + '' + fecha_actual);
            console.log('fechaSin ' + '' + fechaSin);
            var anio_max = fechaSin.split('-')[0];
            var mes_max = parseInt(fechaSin.split('-')[1]) + 1;
            var dia_max = '10';
            var dia_min = '01';
            var fecha_max = anio_max + '-' + mes_max + '-' + dia_max;
            var fecha_min = anio_max + '-' + fechaSin.split('-')[1] + '-' + dia_min;
            console.log('fecha_max ' + '' + fecha_max);
            console.log('fecha_min ' + '' + fecha_min);
            fecha_max = fecha_max.split('-');
            fecha_actual = fecha_actual.split('-');
            fechaSin = fechaSin.split('-');
            fecha_min = fecha_min.split('-');
            fecha_max = new Date(fecha_max[0], fecha_max[1] - 1, fecha_max[2]).valueOf();
            fecha_actual = new Date(fecha_actual[0], fecha_actual[1] - 1, fecha_actual[2]).valueOf();
            fechaSin = new Date(fechaSin[0], fechaSin[1] - 1, fechaSin[2]).valueOf();
            fecha_min = new Date(fecha_min[0], fecha_min[1] - 1, fecha_min[2]).valueOf();
            console.log('------------------------------------------');
            console.log('fecha_max ' + '' + fecha_max);
            console.log('fecha_actual ' + '' + fecha_actual);
            console.log('fechaSin ' + '' + fechaSin);
            console.log('fecha_min ' + '' + fecha_min);
            var _fecha_actual_sistema = new Date().toJSON().slice(0, 10);
            _fecha_actual_sistema = new Date(_fecha_actual_sistema).valueOf();
          //  if (fecha_max >= _fecha_actual_sistema && fechaSin <= fecha_max && fechaSin >= fecha_min ) {



             //   $("#dt_DaniosTercerosSiniestros").find("tr:gt(0)").remove();
                $.ajax({

                    type: "get",
                    url: '../JsonModificarDaniosTerceros',
                    data: { codDanio: codDanioTercero, obsdater: observacionDanioTercero, codtipdater: codTipoDanioTercero },
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (jsonResult) {
                        // datoRetornoCodigoDanioMaterial = jsonResult.toString();
                        console.log('retorno danio tercero');
                        console.log(jsonResult);
                        //console.log('datoRetornoCodigoDanioMaterial' + ' ' + datoRetornoCodigoDanioMaterial.toString());
                        if (jsonResult.length === 0) {
                            mensaje = 'No se puede registrar la infromación';
                            ($('#mensajeLabel').html(mensaje.toString()));

                            ($('#panelMensaje').css("display", "block"));
                        }
                        else if (jsonResult.length > 0) {
                            mensaje = "Daño a tercero modificado correctamente.";
                            console.log(mensaje.toString());
                            ($('#mensajeLabel').html(mensaje.toString()));
                            ($('#divMensaje').attr("class", "alert alert-success smaller"));
                            //($('#tb_codsinDanioTercero').val('0'));
                            ($('#tb_obsdaterEdit').val(''));
                            ($('#tb_coddater').val('-1'));
                            ($('#ddl_tipoDanioTerceroEdit').val('-1'));
                            
                            // location.reload();
                            $('#respuesta-ej6').toggle();
                            $('#respuesta-ej5').toggle();
                            ($('#panelMensaje').css("display", "block"));
                            TablaDanios(codsin);
                        }

                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert(textStatus + ": " + XMLHttpRequest.responseText);
                    }
                });
            //}
            //else {
            //    mensaje = "No puede modificar el registro la fecha del siniestro se encuentra del rango permitido.";
            //    ($('#mensajeLabel').html(mensaje.toString()));

            //    ($('#panelMensaje').css("display", "block"));
            //}
            //($('#panelMensajeEdit').css("display", "block"));

        }

    });

    $('#btnSiguienteVDanioTerceroEdit').click(function () {

        if ($('#dt_victimasSiniestroEdit >tbody >tr').length == 0) {
            alert('Ingrese al menos una víctima para continuar con el proceso');
        }
        else {
            ($('#panelMensajeEdit').css("display", "none"));
            ($('#divMensajeEdit').css("display", "none"));
            //($('#tb_codsinDanioTercero').val('0'));
            ($('#tb_obsdaterEdit').val(''));
            ($('#tb_codDanioTercero').val('0'));
            ($('#ddl_tipoDanioTerceroEdit').val('-1'));
           

            $('#respuesta-ej5').toggle();
            $('#respuesta-ej6').toggle();
            $("#aliDanios").css("display", "block");

            $('#myTab li:eq("3") a').tab('show');
            $("#lidanioprog").addClass("completed", "completed");
        }




    });

    /****************************************************************************
    * FIN DANIO TERCEROS
    *************************************************************************/

    $('#btnVerVistaVhlEdit').click(function () {
        var esVisible = $("#divVehiculosInvolucradosEdit").is(":visible");
        // alert(esVisible);
        if (esVisible) {
            ($('#divVehiculosInvolucradosEdit').css("display", "none"));
        }
        else {
            ($('#divVehiculosInvolucradosEdit').css("display", "block"));
            $('#divVehiculosInvolucradosEdit').slideDown(100000);
        }

    });

    $('#btnVerVictimasEdit').click(function () {
        var esVisible = $("#divVictimasInvolucradosEdit").is(":visible");
        // alert(esVisible);
        if (esVisible) {
            ($('#divVictimasInvolucradosEdit').css("display", "none"));
        }
        else {
            ($('#divVictimasInvolucradosEdit').css("display", "block"));
            $('#divVictimasInvolucradosEdit').slideDown(100000);
        }

    });
    $('#btnVerAccionesPeatonEdit').click(function () {
        var esVisible = $("#divVerAccionesPeatonEdit").is(":visible");
        // alert(esVisible);
        if (esVisible) {
            ($('#divVerAccionesPeatonEdit').css("display", "none"));
        }
        else {
            ($('#divVerAccionesPeatonEdit').css("display", "block"));
            $('#divVerAccionesPeatonEdit').slideDown(100000);
        }

    });

    $('#btnVerDaniostercerosEdit').click(function () {
        var esVisible = $("#divVerDaniostercerosEdit").is(":visible");
        // alert(esVisible);
        if (esVisible) {
            ($('#divVerDaniostercerosEdit').css("display", "none"));
        }
        else {
            ($('#divVerDaniostercerosEdit').css("display", "block"));
            $('#divVerDaniostercerosEdit').slideDown(100000);
        }

    });

    $('#btnSiguienteFinalizarProcesoEdit').click(function () {
        var codsiniestro = $('#tbSiniestroModEdit').val();
        if ($('#dt_DaniosTercerosEdit >tbody >tr').length == 0) {
            alert('Ingrese al menos una daño a tercero para continuar con el proceso.');
        }
        else if (confirm("Va  a proceder a finalizar el proceso del modificación del siniestro #" + codsiniestro + ".  Desea continuar? ")) {
            tarerInformacionSiniestroProcerso(codsiniestro);
            $("#alifinProceso").css("display", "block");

            $('#myTab li:eq("4") a').tab('show');

            $("#lifinaprog").addClass("completed", "completed");
            $("#livictimas").addClass("disabled", "disabled");
            $("#liVehiculos").addClass("disabled", "disabled");
            $("#liDanios").addClass("disabled", "disabled");
        }




    });
    $('#btnFinalizarProcesoSiniestroEdit').click(function () {

        var codsiniestro = $('#tbSiniestroModEdit').val();
        if (confirm("Va  a proceder a cerrar el proceso del siniestro #" + codsiniestro + " . Desea continuar? ")) {
            $('#mensajeLabelFinalizar').html("Siniestro #" + codsiniestro + " modificado correctamente, presione sobre el botón Buscar otro siniestro si desea modificar mas registros.");
            $('#panelMensajeFinalizar').css('display', 'block')
            $('#btnFinalizarProcesoSiniestroEdit').css('display', 'none')
            $('#btnNuevoSiniestroEdit').css('display', 'block')
            $.ajax({
                type: "get",
                url: '../FinalizarProcesoSiniestroEdit',
                data: { codsin: codsiniestro, codproceso: codprocesoAnterior },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (retorno) {
                    console.log('retorno' + ' ' + retorno.toString());
                    if (retorno.toString() === '0') {
                        mensaje = 'ERROR EN LA BASE DE DATOS: NO SE PUEDE MODIFICAR EL SINIESTRO';
                        ($('#divMensajeSiniestroEdit').css("display", "block"));
                        ($('#panelMensajeVHLEdit').css("display", "block"));
                        ($('#divMensajeSiniestroEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
                        ($('#mensajeLabelSiniestroEdit').html(mensaje.toString()))
                    }
                    else {
                        mensaje = 'SINIESTRO MODIFICADO CORRECTAMENTE: ' + ' ' + '#SINIESTRO' + ' ' + retorno.toString();
                        ($('#divMensajeSiniestroEdit').css("display", "block"));

                        ($('#panelMensajeSiniestroEdit').css("display", "block"));
                        ($('#mensajeLabelSiniestroEdit').html(mensaje.toString()));
                        ($('#divMensajeSiniestroEdit').attr("class", "alert alert-block alert-success fade in smaller"));
                        ($('#mensajeLabelSiniestroEdit').html(mensaje.toString()));
                        ($('#panelMensajeSiniestroEdit').css("display", "block"));
                      



                        //$("#alivictimas").css("display", "block");
                        //$("#aliDanios").css("display", "block");
                        //$("#alifinProceso").css("display", "block");
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



    $('#ddl_lugarViaEdit').change(function () {
        $("#ddl_senializacionExistenteEdit").val('0');
        if ($('#ddl_lugarViaEdit option:selected').text() === 'INTERSECCIÓN EN T' || $('#ddl_lugarViaEdit option:selected').text() === 'INTERSECCIÓN EN CRUZ' || $('#ddl_lugarViaEdit option:selected').text() === 'INTERSECCIÓN EN Y') {
           
            $("#ddl_controlInterseccionEdit").removeAttr('disabled');
            $("#ddl_controlInterseccionEdit").val('0');
            $("#ddl_curvaExistenteEdit").val('4');
            $("#ddl_curvaExistenteEdit").attr('disabled', 'disabled');
           // alert('INTERSECCIÓN EN T');
        }
        else if ($('#ddl_lugarViaEdit option:selected').text() === 'CURVA') {
            $("#ddl_curvaExistenteEdit").val('-1');
            $("#ddl_curvaExistenteEdit").removeAttr('disabled');
            $("#ddl_controlInterseccionEdit").attr('disabled', 'disabled');
            $("#ddl_controlInterseccionEdit").val('8');
        }
        else {
            $("#ddl_controlInterseccionEdit").attr('disabled', 'disabled');
            $("#ddl_controlInterseccionEdit").val('8');
            $("#ddl_curvaExistenteEdit").val('3');
            $("#ddl_curvaExistenteEdit").attr('disabled', 'disabled');
        }

    });

    /**********************************************************
    * MODOFICA SINIESTRO
    ***********************************************************/

    $('#btnModificarSiniestro').click(function () {
        //($('#panelMensajeSiniestroEdit').css("display", "none"));
        var _codprocesoAnterior = $('#lbCodigoProceso').val();
        var mensaje = '';
        
        var codSiniestro = $('#tbSiniestroModEdit').val();
        var fechaSin = ($('#tbfechaSiniestroEdit').val());
        var horaSin = ($('#tbHoraSiniestroEdit').val());

        var provinciaSin = ($('#ddl_provinciaEdit').val());
        var cantonSin = ($('#ddl_cantonEdit').val());
        var parroquiaSin = ($('#ddl_parroquiaEdit').val());
        var distritoSin = ($('#ddl_distritoSiniestroEdit').val());
        var circuitoSin = ($('#ddl_circuitoEdit').val());
        var subCircuitoSin = ($('#ddl_circuitoEdit option:selected').text());
        var ZonaSin = ($('#ddl_tipo_zonaEdit option:selected').text());
        var direccionSin = ($('#tbdireccionSiniestroEdit').val());
        var numFallecidosSin = ($('#tbnumfalsinEdit').val());
        var numLesionadosSin = ($('#tbnumlessinEdit').val());
        var condicionAtmosfericaSin = ($('#ddl_condAtmosfericaEdit option:selected').text());
        var condicionViaSin = ($('#ddl_condViaEdit option:selected').text());
        var luzArteficialSin = ($('#ddl_luzArtificialEdit option:selected').text());
        var trabajosViaSin = $('#ddl_trabajosViaEdit option:selected').text();
        var tipoViaSin = ($('#ddl_tipoViaEdit option:selected').text());
        var limVelocidadSin = ($('#ddl_limVelocidadEdit option:selected').text());
        var controlInterseccionSin = ($('#ddl_controlInterseccionEdit option:selected').text());
        var materialSuperficielSin = ($('#ddl_materialSuperficieEdit option:selected').text());
        var obstaculosViaSin = ($('#ddl_obstViaEdit option:selected').text());
        var lugarViaSin = ($('#ddl_lugarViaEdit option:selected').text());
        var numCarrilesSin = ($('#ddl_numCarrilesEdit option:selected').text());
        var curvaExistenteSin = ($('#ddl_curvaExistenteEdit option:selected').text());
        var senializacionExistenteSin = ($('#ddl_senializacionExistenteEdit option:selected').text());
        var tipoSin = ($('#ddl_tipoSiniestroEdit').val());
        var causaProbableSin = ($('#ddl_causaProbableSiniestroEdit').val());
        var causaRealSin = ($('#ddl_causaRealSiniestroEdit').val());
        var isVisibleLuzArtificial = $("#divLuzArtificialEdit").is(":visible");
        var latitudSin = ($('#tbLatitudSiniestroEdit').val());
        var longuitudSin = ($('#tbLonguitudSiniestroSin').val());
        var isVisibleControlInterseccion = $("#ddl_controlInterseccionEdit").is('enabled');
        var codGeo = '0';
        var latsinGeo = '0';
        var lonsinGeo = '0';
        console.log(latitudSin.toString());
        console.log(longuitudSin.toString());
        console.log(codGeo.toString());
        console.log('luz artificial' + ' ' + isVisibleLuzArtificial);
        console.log(luzArteficialSin);
        if ($("#ddl_controlInterseccionEdit").prop('disabled') == false) {
            isVisibleControlInterseccion = true;
        }
        else { isVisibleControlInterseccion = false; }
        if (fechaSin.trim() === "" || fechaSin.length < 10) {
            mensaje = 'Ingrese la fecha del siniestro.';
            ($('#divMensajeSiniestroEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
            ($('#mensajeLabelSiniestroEdit').html(mensaje.toString()));
            ($('#panelMensajeSiniestroEdit').css("display", "block"));
        }
        else if (horaSin.trim() === "" || horaSin.length < 5) {
            mensaje = 'Ingrese la hora del siniestro.';
            ($('#divMensajeSiniestroEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
            ($('#mensajeLabelSiniestroEdit').html(mensaje.toString()));
            ($('#panelMensajeSiniestroEdit').css("display", "block"));
        }//
        else if (latitudSin.trim() === "") {
            mensaje = 'Ingrese la latitud(x) del  siniestro.';
            ($('#divMensajeSiniestroEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
            ($('#mensajeLabelSiniestroEdit').html(mensaje.toString()));
            ($('#panelMensajeSiniestroEdit').css("display", "block"));
        }//
        else if (longuitudSin.trim() === "") {
            mensaje = 'Ingrese la longuitud(y) del siniestro.';
            ($('#divMensajeSiniestroEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
            ($('#mensajeLabelSiniestroEdit').html(mensaje.toString()));
            ($('#panelMensajeSiniestroEdit').css("display", "block"));
        }//

        else if (provinciaSin.trim() === "" || provinciaSin.trim() === '-1') {
            mensaje = 'Seleccione la provincia del siniestro.';
            ($('#divMensajeSiniestroEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
            ($('#mensajeLabelSiniestroEdit').html(mensaje.toString()));
            ($('#panelMensajeSiniestroEdit').css("display", "block"));
        }
        else if (cantonSin.trim() === "" || cantonSin.trim() === '-1') {
            mensaje = 'Seleccione el cantón del siniestro.';
            ($('#divMensajeSiniestroEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
            ($('#mensajeLabelSiniestroEdit').html(mensaje.toString()));
            ($('#panelMensajeSiniestroEdit').css("display", "block"));
        }
        else if (parroquiaSin.trim() === "" || parroquiaSin.trim() === '-1') {
            mensaje = 'Seleccione la parroquia del siniestro.';
            ($('#divMensajeSiniestroEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
            ($('#mensajeLabelSiniestroEdit').html(mensaje.toString()));
            ($('#panelMensajeSiniestroEdit').css("display", "block"));
        }
        //else if (distritoSin.trim() === "" || distritoSin.trim() === '-1') {
        //    mensaje = 'Seleccione el distrito del siniestro.';
        //}
        //else if (circuitoSin.trim() === "" || circuitoSin.trim() === '-1' || circuitoSin == null) {
        //    mensaje = 'Seleccione el circuito del siniestro.';
        //}
        //else if (ZonaSin.trim() === "" || ZonaSin.trim() === 'SELECCIONAR') {
        //    mensaje = 'Seleccione la zona del siniestro.';
        //}
        else if (direccionSin.trim() === "" || direccionSin.trim() === '') {
            mensaje = 'Ingrese la dirección del siniestro.';
            ($('#divMensajeSiniestroEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
            ($('#mensajeLabelSiniestroEdit').html(mensaje.toString()));
            ($('#panelMensajeSiniestroEdit').css("display", "block"));
        }

        else if (numFallecidosSin === '') {
            mensaje = 'Ingrese el número de fallecidos.';
            ($('#divMensajeSiniestroEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
            ($('#mensajeLabelSiniestroEdit').html(mensaje.toString()));
            ($('#panelMensajeSiniestroEdit').css("display", "block"));
        }
        else if (numLesionadosSin === '') {
            mensaje = 'Ingrese el número de lesionados.';
            ($('#divMensajeSiniestroEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
            ($('#mensajeLabelSiniestroEdit').html(mensaje.toString()));
            ($('#panelMensajeSiniestroEdit').css("display", "block"));
        }//
        else if (condicionAtmosfericaSin.trim() === "" || condicionAtmosfericaSin.trim() === 'SELECCIONAR') {
            mensaje = 'Seleccione la Condición Atmosférica del siniestro.';
            ($('#divMensajeSiniestroEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
            ($('#mensajeLabelSiniestroEdit').html(mensaje.toString()));
            ($('#panelMensajeSiniestroEdit').css("display", "block"));
        }
        else if (condicionViaSin.trim() === "" || condicionViaSin.trim() === 'SELECCIONAR') {
            mensaje = 'Seleccione la Condición de la Vía del siniestro.';
            ($('#divMensajeSiniestroEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
            ($('#mensajeLabelSiniestroEdit').html(mensaje.toString()));
            ($('#panelMensajeSiniestroEdit').css("display", "block"));
        }
        else if ((luzArteficialSin.trim() === "" || luzArteficialSin.trim() === 'SELECCIONAR') && isVisibleLuzArtificial === true) {
            mensaje = 'Seleccione la Luz Artificial  del siniestro.';
            ($('#divMensajeSiniestroEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
            ($('#mensajeLabelSiniestroEdit').html(mensaje.toString()));
            ($('#panelMensajeSiniestroEdit').css("display", "block"));
        }
        else if (tipoViaSin.trim() === "" || tipoViaSin.trim() === 'SELECCIONAR') {
            mensaje = 'Seleccione el Tipo de la vía  del siniestro.';
            ($('#divMensajeSiniestroEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
            ($('#mensajeLabelSiniestroEdit').html(mensaje.toString()));
            ($('#panelMensajeSiniestroEdit').css("display", "block"));
        }
        else if (limVelocidadSin.trim() === "" || limVelocidadSin.trim() === 'SELECCIONAR') {
            mensaje = 'Seleccione el Lim.Velocidad del siniestro.';
            ($('#divMensajeSiniestroEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
            ($('#mensajeLabelSiniestroEdit').html(mensaje.toString()));
            ($('#panelMensajeSiniestroEdit').css("display", "block"));
        }
        else if ((controlInterseccionSin.trim() === "" || controlInterseccionSin.trim() === 'SELECCIONAR') && isVisibleControlInterseccion === true) {
            mensaje = 'Seleccione el Control Intersección  del siniestro.';
            ($('#divMensajeSiniestroEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
            ($('#mensajeLabelSiniestroEdit').html(mensaje.toString()));
            ($('#panelMensajeSiniestroEdit').css("display", "block"));
        }
        //else if (controlInterseccionSin.trim() === "" || controlInterseccionSin.trim() === 'SELECCIONAR') {
          
        //}
        else if (materialSuperficielSin.trim() === "" || materialSuperficielSin.trim() === 'SELECCIONAR') {
            mensaje = 'Seleccione el Material de Superficie del siniestro.';
            ($('#divMensajeSiniestroEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
            ($('#mensajeLabelSiniestroEdit').html(mensaje.toString()));
            ($('#panelMensajeSiniestroEdit').css("display", "block"));
        }
        else if (obstaculosViaSin.trim() === "" || obstaculosViaSin.trim() === 'SELECCIONAR') {
            mensaje = 'Seleccione los Obstáculos de la Vía  del siniestro.';
            ($('#divMensajeSiniestroEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
            ($('#mensajeLabelSiniestroEdit').html(mensaje.toString()));
            ($('#panelMensajeSiniestroEdit').css("display", "block"));
        }
        else if (trabajosViaSin.trim() === "" || trabajosViaSin.trim() === 'SELECCIONAR') {
            mensaje = 'Seleccione si existe  trabajos en la vía.';
            ($('#divMensajeSiniestroEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
            ($('#mensajeLabelSiniestroEdit').html(mensaje.toString()));
            ($('#panelMensajeSiniestroEdit').css("display", "block"));
        }
        
        else if (lugarViaSin.trim() === "" || lugarViaSin.trim() === 'SELECCIONAR') {
            mensaje = 'Seleccione el Lugar de la vía del siniestro.';
            ($('#divMensajeSiniestroEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
            ($('#mensajeLabelSiniestroEdit').html(mensaje.toString()));
            ($('#panelMensajeSiniestroEdit').css("display", "block"));
        }
        else if (numCarrilesSin.trim() === "" || numCarrilesSin.trim() === 'SELECCIONAR') {
            mensaje = 'Seleccione el numero de carrilles.';
            ($('#divMensajeSiniestroEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
            ($('#mensajeLabelSiniestroEdit').html(mensaje.toString()));
            ($('#panelMensajeSiniestroEdit').css("display", "block"));
        }
        else if (curvaExistenteSin.trim() === "" || curvaExistenteSin.trim() === 'SELECCIONAR') {
            mensaje = 'Seleccione Curva Existente.';
            ($('#divMensajeSiniestroEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
            ($('#mensajeLabelSiniestroEdit').html(mensaje.toString()));
            ($('#panelMensajeSiniestroEdit').css("display", "block"));
        }
        else if (senializacionExistenteSin.trim() === "" || senializacionExistenteSin.trim() === 'SELECCIONAR') {
            mensaje = 'Seleccione Señalización Existente.';
            ($('#divMensajeSiniestroEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
            ($('#mensajeLabelSiniestroEdit').html(mensaje.toString()));
            ($('#panelMensajeSiniestroEdit').css("display", "block"));
        }
        else if (tipoSin.trim() === "" || tipoSin.trim() === '-1') {
            mensaje = 'Seleccione el Tipo de Siniestro.';
            ($('#divMensajeSiniestroEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
            ($('#mensajeLabelSiniestroEdit').html(mensaje.toString()));
            ($('#panelMensajeSiniestroEdit').css("display", "block"));
        }
        else if (causaProbableSin.trim() === "" || causaProbableSin.trim() === '-1') {
            mensaje = 'Seleccione la Causa Probable Siniestro del Siniestro.';
            ($('#divMensajeSiniestroEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
            ($('#mensajeLabelSiniestroEdit').html(mensaje.toString()));
            ($('#panelMensajeSiniestroEdit').css("display", "block"));
        }
        else if (causaRealSin.trim() === "" || causaRealSin.trim() === '-1') {
            mensaje = 'Seleccione la Causa Real Siniestro del Siniestro.';
            ($('#divMensajeSiniestroEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
            ($('#mensajeLabelSiniestroEdit').html(mensaje.toString()));
            ($('#panelMensajeSiniestroEdit').css("display", "block"));
        }
        else {
            var fecha_actual = Date.toLocaleString();
            var d = new Date();
            var dd = d.getDate() - 40;
            var mm = d.getMonth() + 1; //hoy es 0!
            var yyyy = d.getFullYear();
            //console.log('fecha_actual ' + '' + dd + '-' + mm + '-' + yyyy);
            fecha_actual = new Date().toJSON().slice(0, 10)
            var _fecha_actual_sistema = new Date().toJSON().slice(0, 10);
            console.log('fecha_actual ' + '' + fecha_actual);
            console.log('fechaSin ' + '' + fechaSin);
            var anio_max = fechaSin.split('-')[0];
            var mes_max = parseInt(fechaSin.split('-')[1]) + 1;
            var dia_max = '10';
            var dia_min = '01';
            var fecha_max = anio_max + '-' + mes_max + '-' + dia_max;
            var fecha_min = anio_max + '-' + fechaSin.split('-')[1] + '-' + dia_min;
            console.log('fecha_max ' + '' + fecha_max);
            console.log('fecha_min ' + '' + fecha_min);
            fecha_max = fecha_max.split('-');
            fecha_actual = fecha_actual.split('-');
            fechaSin = fechaSin.split('-');
            fecha_min = fecha_min.split('-');
            fecha_max = new Date(fecha_max[0], fecha_max[1] - 1, fecha_max[2]).valueOf();
            fecha_actual = new Date(fecha_actual[0], fecha_actual[1] - 1, fecha_actual[2]).valueOf();
            fechaSin = new Date(fechaSin[0], fechaSin[1] - 1, fechaSin[2]).valueOf();
            fecha_min = new Date(fecha_min[0], fecha_min[1] - 1, fecha_min[2]).valueOf();
            _fecha_actual_sistema = new Date(_fecha_actual_sistema).valueOf();
            console.log('------------------------------------------');
            console.log('fecha_max ' + '' + fecha_max);
            console.log('fecha_actual ' + '' + _fecha_actual_sistema);
            console.log('fechaSin ' + '' + fechaSin);
            console.log('fecha_min ' + '' + fecha_min);
           if (fecha_max >= _fecha_actual_sistema && fechaSin <= fecha_max && fechaSin >= fecha_min) {
                if (confirm("Va a proceder a  modificar los datos generales del siniestro. Desea Continuar!") === true) {
                    fecsin = ($('#tbfechaSiniestroEdit').val());
                    console.log("codprocesoAnterior" + "" + codprocesoAnterior);
                    $.ajax({
                        type: "get",
                        url: '../JsonModificarSiniestro',
                        data: { codsin: codSiniestro, fecsin: fecsin, horsin: horaSin, latsin: latitudSin, lonsin: longuitudSin, dirsin: direccionSin, numfalsin: numFallecidosSin, numlessin: numLesionadosSin, zonsin: ZonaSin, traviasin: trabajosViaSin, conatmsin: condicionAtmosfericaSin, conviasin: condicionViaSin, luzartsin: luzArteficialSin, desviasin: tipoViaSin, limvelsin: limVelocidadSin, intsin: controlInterseccionSin, matsupviasin: materialSuperficielSin, obsviasin: obstaculosViaSin, lugviasin: lugarViaSin, cursin: curvaExistenteSin, numcarsin: numCarrilesSin, sensin: senializacionExistenteSin, codtipsin: tipoSin, codpar: parroquiaSin, codsubcir: subCircuitoSin, codcant: cantonSin, codprov: provinciaSin, codcaupro: causaProbableSin, codcaurea: causaRealSin, codcir: circuitoSin, coddis: distritoSin, codgeo: codGeo, latsinGeo: latsinGeo, lonsinGeo: lonsinGeo, codproceso: _codprocesoAnterior },
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (jsonmensaje) {
                            console.log('retorno' + ' ' + jsonmensaje.toString());
                            if (jsonmensaje.toString() === '0') {
                                mensaje = 'Error al modificar los datos del siniestro.';
                                ($('#mensajeLabelSiniestroEdit').html(mensaje.toString()));
                                ($('#divMensajeSiniestroEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
                                ($('#mensajeLabelSiniestroEdit').html(mensaje.toString()));
                                ($('#panelMensajeSiniestroEdit').css("display", "block"));
                            }
                            else {
                                mensaje = 'SINIESTRO MODIFICADO CORRECTAMENTE: ' + ' ' + '#SINIESTRO' + ' ' + jsonmensaje.toString();
                                ($('#divMensajeSiniestroEdit').css("display", "block"));

                                ($('#panelMensajeSiniestroEdit').css("display", "block"));
                                ($('#mensajeLabelSiniestroEdit').html(mensaje.toString()));
                                ($('#divMensajeSiniestroEdit').attr("class", "alert alert-block alert-success fade in smaller"));
                                ($('#mensajeLabelSiniestroEdit').html(mensaje.toString()));
                                ($('#panelMensajeSiniestroEdit').css("display", "block"));
                                inicializaVhlEdit();
                                ($('#panelMensajeVHLEdit').css("display", "block"));
                                ($('#mensajeLabelVHLEdit').html(mensaje.toString()));
                                ($('#divMensajeVHLEdit').attr("class", "alert alert-block alert-success fade in smaller"));
                                $("#aLiVehiculos").css("display", "block");
                                $('#myTab li:eq("1") a').tab('show');
                                $('#respuesta-ej2').toggle();
                                $('#respuesta-ej1').toggle();
                                $("#liingreprog").addClass("completed", "completed");

                                
                                
                                //$("#alivictimas").css("display", "block");
                                //$("#aliDanios").css("display", "block");
                                //$("#alifinProceso").css("display", "block");
                            }
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus + ": " + XMLHttpRequest.responseText);
                        }

                    });
                }
                else {
                    ($('#panelMensajeSiniestroEdit').css("display", "none"));
                    ($('#mensajeLabelSiniestroEdit').html(mensaje.toString()));
                    ($('#panelMensajeSiniestroEdit').css("display", "none"));
                }
              
            }
            else {
                mensaje = 'La fecha del siniestro se ecuentra fuera del rango permitido.';
                ($('#divMensajeSiniestroEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
                ($('#mensajeLabelSiniestroEdit').html(mensaje.toString()));
                ($('#panelMensajeSiniestroEdit').css("display", "block"));
            }

        }

        

    });


    /**********************************************************
    * FIN MODOFICA SINIESTRO
    ***********************************************************/

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
    $('#alternar-respuesta-ej5').on('click', function () {
        $('#respuesta-ej6').toggle();
        $('#respuesta-ej5').toggle();

    });

    $('#alternar-respuesta-ej6').on('click', function () {
        $('#respuesta-ej5').toggle();
        $('#respuesta-ej6').toggle();
    });


    /**************************************
* GUARDA VHL INVOLUCRADOS
***************************************/
    $('#btnModalVhlInvolucradosEdit').click(function () {
        
    });
    $('#ddl_tipo_vhl').change(function () {
        if ( $('#ddl_tipo_vhl').val().toString() === '16') {
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

            $('#tb_placvehinv').removeAttr('disabled');
            $('#tb_chavehinv').removeAttr('disabled');
            $('#tb_marvehinv').removeAttr('disabled');
            $('#tb_modvehinv').removeAttr('disabled');
            $('#tb_anivehinv').removeAttr('disabled');
            $('#tb_cilvehinv').removeAttr('disabled');

        }
        else {
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
        }

        if ($('#ddl_tipo_vhl').val().toString() !== '-1') {
            var select = $('#ddl_subtipo_vhl');
            select.find('option').remove().end().append('<option value="-1">SELECCIONAR</option>').val('-1');
            var codTipoVhls = $('#ddl_tipo_vhl').val();
            $.ajax({
                type: "GET",
                url: "../ObtenerlistaSubTipoVehiculo",
                data: { codTipoVhl: codTipoVhls },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (jsonResult) {
                    if (jsonResult.length > 0) {
                        cosole.log(jsonResult);
                        $.each(jsonResult, function (key, value) {
                            select.append('<option value="' + value.value + '">' + value.text + '</option>').val(value.value);
                        });
                        if ($('#ddl_tipo_vhl').val() === '16') {
                            select.val('54');
                        }
                        else if ($('#ddl_tipo_vhl').val() === '18') {
                            select.val('16');
                        }
                        else {
                            select.val('-1');
                        }
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("Error al cargar los sub tipo de vehiculos.");
                }
            });
        }
    });

    $('#btnBuscarVhlEdit').click(function () {
        var placa = $('#tb_placvehinvEdit').val();
        var chasis = $('#tb_chavehinvEdit').val();
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
              //  $('#divCargaVhlImagen').css("display", "block");
                $.ajax({
                    type: "GET",
                    url: "../ObtenerPlacaVehiculo",
                    data: { parametro: parametro, opcion: opcion },
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (jsonResult) {
                        console.log(jsonResult);
                        if (jsonResult.length > 0) {
                            $.each(jsonResult, function (key, value) {
                                if (jsonResult.length > 0) {
                                    $("#tb_placvehinvEdit").val(value.placvehinv);
                                    $('#tb_placvehinvEdit').attr("disabled", "disabled");
                                    $("#tb_chavehinvEdit").val(value.chavehinv);
                                    $('#tb_chavehinvEdit').attr("disabled", "disabled");
                                    $("#tb_marvehinvEdit").val(value.marvehinv);
                                    $('#tb_marvehinvEdit').attr("disabled", "disabled");
                                    $("#tb_modvehinvEdit").val(value.modvehinv);
                                    $('#tb_modvehinvEdit').attr("disabled", "disabled");
                                    $("#tb_anivehinvEdit").val(value.anivehinv);
                                    $('#tb_anivehinvEdit').attr("disabled", "disabled");
                                    $("#tb_cilvehinvEdit").val(value.cilvehinv);
                                    $('#tb_cilvehinvEdit').attr("disabled", "disabled");
                                    $('#ddl_matricula_vigenteEdit').val('2');
                                    $('#ddl_vhl_materialPeligrosoEdit').val('0');
                                    $("ddl_tipo_vhlEdit option").each(function () { this.selected = (this.text === value.tipoVehiculo); });
                                    //($('#ddl_tipo_vhl option:selected').text(value.tipoVehiculo));
                                    mensaje = "Busqueda exitosa.";
                                    alert(mensaje.toString());
                                    // $('#divCargaVhlImagen').css("display", "none");
                                }
                                else {
                                    mensaje = "No existe resultados en la búsqueda.";
                                    alert(mensaje.toString());
                                }



                            });
                        }
                        else {
                            mensaje = "No existe resultados en la búsqueda.";
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


    $('#btLimpiarVHL').click(function () {
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
        $("#ddl_vhl_materialPeligroso").val('0');
        $('#tb_placvehinv').removeAttr('disabled');
        $('#tb_chavehinv').removeAttr('disabled');
        $('#tb_marvehinv').removeAttr('disabled');
        $('#tb_modvehinv').removeAttr('disabled');
        $('#tb_anivehinv').removeAttr('disabled');
        $('#tb_cilvehinv').removeAttr('disabled');
        $('#cb_matvigvehinv').prop("checked", false);
        $('#cb_danmatvehinv').prop("checked", true);
        $('#cb_segprivehinv').prop("checked", false);
        //  $("#btnBuscarVhl").css("display", "none");
        $("#panelMensajeVHL").css("display", "none");
    });

    $('#tbGudarVhlInvolucradosEdit').click(function () {
        ($('#divMensajeVHLEdit').attr("class", "alert alert-danger smaller"));
        ($('#panelMensajeVHLEdit').css('display', 'none'));

        var mensaje = '';
        
        var codsiniestroVHL = ($('#tbSiniestroModEdit').val());
        console.log(codsiniestroVHL);
        var codVhl = ($('#tb_codVHLEdit').val());
        var placaVHL = ($('#tb_placvehinvEdit').val());
        var chasisVHL = ($('#tb_chavehinvEdit').val());
        var marcaVHL = ($('#tb_marvehinvEdit').val());
        var modeloVHL = ($('#tb_modvehinvEdit').val());
        var anioVHL = ($('#tb_anivehinvEdit').val());
        var cilindrajeVHL = ($('#tb_cilvehinvEdit').val());
        var materialPeligrosoVHL = ($('#ddl_vhl_materialPeligrosoEdit option:selected').text());
        var codtipoServVHL = ($('#ddl_tipo_servicio_vhlEdit').val());
        var codtipoVHL = ($('#ddl_tipo_vhlEdit').val());
        var codSubtipoVHL = ($('#ddl_subtipo_vhlEdit').val());
        var seguroPrivado = $("#ddl_seguro_privado_vhlEdit").val();
        var danioMaterial = $("#ddl_danio_materialEdit").val();
        var matriculaVigente = $('#ddl_matricula_vigenteEdit').val();

        console.log(seguroPrivado);
        console.log("danioMaterial" + " " + danioMaterial);
        console.log(matriculaVigente);
        if (codsiniestroVHL === '' || codsiniestroVHL === '0' || parseInt(codsiniestroVHL) < -1) {
            mensaje = 'Debe ingresar un siniestro';
            ($('#mensajeLabelVHLEdit').html(mensaje.toString()));
            ($('#panelMensajeVHLEdit').css("display", "block"));

        }
        else if (codtipoVHL.toString().trim() === "" || codtipoVHL.toString().trim() === "-1") {
            mensaje = 'Seleccione el tipo de vehiculo.';
            ($('#mensajeLabelVHLEdit').html(mensaje.toString()));
            ($('#panelMensajeVHLEdit').css("display", "block"));
        }
        else if (codSubtipoVHL.toString().trim() === "" || codSubtipoVHL.toString().trim() === "-1") {
            mensaje = 'Seleccione el sub tipo de vehiculo.';
            ($('#mensajeLabelVHLEdit').html(mensaje.toString()));
            ($('#panelMensajeVHLEdit').css("display", "block"));
        }
        else if (placaVHL.toString().trim() === "" && codtipoVHL.toString() !== "16") {
            mensaje = 'Ingrese la placa';
            ($('#mensajeLabelVHLEdit').html(mensaje.toString()));
            ($('#panelMensajeVHLEdit').css("display", "block"));
        }
        else if (chasisVHL.toString().trim() === "" && codtipoVHL.toString() !== "16") {
            mensaje = 'Ingrese el chasis';
            ($('#mensajeLabelVHLEdit').html(mensaje.toString()));
            ($('#panelMensajeVHLEdit').css("display", "block"));
        }
        else if (marcaVHL.toString().trim() === "" && codtipoVHL.toString() !== "16") {
            mensaje = 'Ingrese la marca del Vehiculo';
            ($('#mensajeLabelVHLEdit').html(mensaje.toString()));
            ($('#panelMensajeVHLEdit').css("display", "block"));
        }
        else if (modeloVHL.toString().trim() === "" && codtipoVHL.toString() !== "16") {
            mensaje = 'Ingrese el modelo del Vehiculo';
            ($('#mensajeLabelVHLEdit').html(mensaje.toString()));
            ($('#panelMensajeVHLEdit').css("display", "block"));
        }
        else if (anioVHL.toString().trim() === "" && codtipoVHL.toString() !== "16") {
            mensaje = 'Ingrese el año del Vehiculo';
            ($('#mensajeLabelVHLEdit').html(mensaje.toString()));
            ($('#panelMensajeVHLEdit').css("display", "block"));
        }
        else if (cilindrajeVHL.toString().trim() === "" && codtipoVHL.toString() !== "16") {
            mensaje = 'Ingrese el cilindraje del Vehiculo';
            ($('#mensajeLabelVHLEdit').html(mensaje.toString()));
            ($('#panelMensajeVHLEdit').css("display", "block"));
        }
        else if (materialPeligrosoVHL.toString().trim() === "SELECCIONAR" && codtipoVHL.toString() !== "16") {
            mensaje = 'Seleccione si el vehiculo contenia material peligroso.';
            ($('#mensajeLabelVHLEdit').html(mensaje.toString()));
            ($('#panelMensajeVHLEdit').css("display", "block"));
        }
        else if (danioMaterial.toString().trim() === "-1") {
            mensaje = 'Seleccione si el vehiculo tiene o no un daño material.';
            ($('#mensajeLabelVHLEdit').html(mensaje.toString()));
            ($('#panelMensajeVHLEdit').css("display", "block"));
        }
        else if (seguroPrivado.toString().trim() === "-1") {
            mensaje = 'Seleccione si el vehiculo tiene o no seguro privado.';
            ($('#mensajeLabelVHLEdit').html(mensaje.toString()));
            ($('#panelMensajeVHLEdit').css("display", "block"));
        }
        else if (matriculaVigente.toString().trim() === "-1") {
            mensaje = 'Seleccione si el vehiculo tiene o no una matrícula vigente.';
            ($('#mensajeLabelVHLEdit').html(mensaje.toString()));
            ($('#panelMensajeVHLEdit').css("display", "block"));
        }
        else if (codtipoServVHL.toString().trim() === "" && codtipoVHL.toString() !== "16") {
            mensaje = 'Seleccione el tipo de servicio del vehiculo.';
            ($('#mensajeLabelVHLEdit').html(mensaje.toString()));
            ($('#panelMensajeVHLEdit').css("display", "block"));
        }
            // $("#divVehiculosInvolucrados").is(":visible")
        else {

            var fecha_actual = Date.toLocaleString();
            var fechaSin = ($('#tbfechaSiniestroEdit').val());
            fecha_actual = new Date().toJSON().slice(0, 10)
            console.log('fecha_actual ' + '' + fecha_actual);
            console.log('fechaSin ' + '' + fechaSin);
            var anio_max = fechaSin.split('-')[0];
            var mes_max = parseInt(fechaSin.split('-')[1]) + 1;
            var dia_max = '10';
            var dia_min = '01';
            var fecha_max = anio_max + '-' + mes_max + '-' + dia_max;
            var fecha_min = anio_max + '-' + fechaSin.split('-')[1] + '-' + dia_min;
            var _fecha_actual_sistema = new Date().toJSON().slice(0, 10);
            console.log('fecha_max ' + '' + fecha_max);
            console.log('fecha_min ' + '' + fecha_min);
            fecha_max = fecha_max.split('-');
            fecha_actual = fecha_actual.split('-');
            fechaSin = fechaSin.split('-');
            fecha_min = fecha_min.split('-');
            fecha_max = new Date(fecha_max[0], fecha_max[1] - 1, fecha_max[2]).valueOf();
            fecha_actual = new Date(fecha_actual[0], fecha_actual[1] - 1, fecha_actual[2]).valueOf();
            fechaSin = new Date(fechaSin[0], fechaSin[1] - 1, fechaSin[2]).valueOf();
            fecha_min = new Date(fecha_min[0], fecha_min[1] - 1, fecha_min[2]).valueOf();
            _fecha_actual_sistema = new Date(_fecha_actual_sistema).valueOf();
            console.log('------------------------------------------');
            console.log('fecha_max ' + '' + fecha_max);
            console.log('fecha_actual ' + '' + fecha_actual);
            console.log('fechaSin ' + '' + fechaSin);
            console.log('fecha_min ' + '' + fecha_min);
          //  if (fecha_max >= _fecha_actual_sistema && fechaSin <= fecha_max && fechaSin >= fecha_min ) {
                if ( confirm( 'Va a ingresar un nuevo vehículo. Desea continuar!') === true)
                {
                    // $("#dt_vehiculosSiniestro").find("tr:gt(0)").remove();
                    $.ajax({
                        type: "get",
                        url: '../JsoninsertarDatosVehiculosInvolucrados',
                        data: { codsin: codsiniestroVHL, placa: placaVHL, chavehinv: chasisVHL, marvehinv: marcaVHL, modvehinv: modeloVHL, cilvehinv: cilindrajeVHL, matpelvehinv: materialPeligrosoVHL, codser: codtipoServVHL, codtipve: codtipoVHL, seguroPrivado: seguroPrivado, danioMaterial: danioMaterial, matriculaVigente: matriculaVigente, anivehinv: anioVHL, codsubtipoVhl: codSubtipoVHL ,codproceso:codprocesoAnterior},
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (jsonResult) {
                            console.log('retorno vhl inv' + ' ' + jsonResult);
                            if (jsonResult.length === 0) {
                                mensaje = 'Error al grabar los datos.';
                                
                                ($('#mensajeLabelVHLEdit').html(mensaje.toString()));
                                ($('#panelMensajeVHLEdit').css("display", "block"));
                            }
                            else if (jsonResult.length > 0) {

                                inicializaVhlEdit();
                                
                              
                                mensaje = 'Datos registrados correctamente.';
                                ($('#divMensajeVHLEdit').attr("class", "alert alert-success smaller"));
                                ($('#mensajeLabelVHLEdit').html(mensaje.toString()));
                                //$.each(jsonResult, function (key, value) {
                                //    /* Vamos agregando a nuestra tabla las filas necesarias */
                                //    var segpri = "";
                                //    var daniomat = "";//danmatvehinv
                                //    var mat = "";//danmatvehinv
                                //    if (value.segprivehinv === true) { segpri = 'SI' } else if (value.segprivehinv === false) segpri = 'NO'
                                //    if (value.danmatvehinv === true) { daniomat = 'SI' } else if (value.danmatvehinv === false) daniomat = 'NO'
                                //    if (value.matvigvehinv === true) { mat = 'SI' } else if (value.matvigvehinv === false) mat = 'NO'

                                //    $("#dt_vehiculosSiniestro").append("<tr><td>" + value.placvehinv + "</td><td>" + value.chavehinv + "</td><td>" + value.marvehinv + "</td><td>" + value.anivehinv + "</td><td>" + value.desser + "</td><td>" + value.destipveh + "</td><td>" + value.dessubveh + "</td><td>" + segpri + "</td><td>" + daniomat + "</td><td>" + mat + "</td></tr>");
                                //});
                                ($('#mensajeLabelVHLEdit').html(mensaje.toString()));
                                ($('#panelMensajeVHLEdit').css("display", "block"));
                                ($('#tbGudarVhlInvolucradosEdit').css("display", "none"));
                               

                                TablaVehiculos(codsin);
                                $('#respuesta-ej2').toggle();
                                $('#respuesta-ej1').toggle();
                                ($('#btnModificarVhlInvolucrados').css("display", "block"));
                                ($('#tbGudarVhlInvolucradosEdit').css("display", "none"));

                            }
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus + ": " + XMLHttpRequest.responseText);
                        }

                    });
                }
               
            //}
            //else {
            //    mensaje = "No puede ingresar nuevos vehiculos involucrados, la fecha del siniestro esta fuera del rango permitido. ";
            //    ($('#mensajeLabelVHLEdit').html(mensaje.toString()));
            //    ($('#panelMensajeVHLEdit').css("display", "block"));
            //}
        }
        

       
    });
    $('#btLimpiarVHLEdit').click(function () {

        $('#respuesta-ej1').toggle();
        $('#respuesta-ej2').toggle();

        var selectsv = $('#ddl_subtipo_vhlEdit');
        selectsv.find('option').remove().end().append('<option value="-1">SELECCIONAR</option>').val('-1');
        $("#tb_placvehinvEdit").val('');
        $("#tb_chavehinvEdit").val('');
        $("#tb_marvehinvEdit").val('');
        $("#tb_modvehinvEdit").val('');
        $("#tb_anivehinvEdit").val('');
        $("#tb_cilvehinvEdit").val('');
        $("#ddl_tipo_vhlEdit").val('-1');
        $("#ddl_tipo_servicio_vhlEdit").val('-1');
        $('#ddl_subtipo_vhlEdit').val('-1');
        $("#ddl_vhl_materialPeligrosoEdit").val('0');
        $('#tb_placvehinvEdit').removeAttr('disabled');
        $('#tb_chavehinvEdit').removeAttr('disabled');
        $('#tb_marvehinvEdit').removeAttr('disabled');
        $('#tb_modvehinvEdit').removeAttr('disabled');
        $('#tb_anivehinvEdit').removeAttr('disabled');
        $('#tb_cilvehinvEdit').removeAttr('disabled');
        $('#ddl_seguro_privado_vhlEdit').removeAttr('disabled');
        $('#ddl_danio_materialEdit').removeAttr('disabled');
        $('#ddl_matricula_vigenteEdit').removeAttr('disabled');
        $("#ddl_vehiculo_identificadoEdit").removeAttr('disabled');
        $('#ddl_seguro_privado_vhlEdit').val('-1');
        $('#ddl_danio_materialEdit').val('-1');
        $('#ddl_matricula_vigenteEdit').val('-1');
        $("#ddl_vehiculo_identificadoEdit").val('-1');
        //  $("#btnBuscarVhl").css("display", "none");
        $("#panelMensajeVHLEdit").css("display", "none");
        ($('#tbGudarVhlInvolucradosEdit').css("display", "block"));
        ($('#btnModificarVhlInvolucrados').css("display", "none"));


    });
    $('#btnCerrarVhlEdit').click(function () {
        location.reload();
    });



    $('#btnSiguienteVhlEdit').click(function () {
  
        if ($('#dt_vehiculosEdit >tbody >tr').length == 0) {
            alert("Ingresar al menos un vehículo involucrado para continuar..!!");
        }
        else {

            //InicializaVictimas();
            $('#myTab li:eq("2") a').tab('show');
            $('#alivictimas').css("display", "block");
            $('#respuesta-ej3').toggle();
            $('#respuesta-ej4').toggle();
            $("#livicprog").addClass("completed", "completed");

            traerVehiculosInvolucradosSinEdit();



        }

    });
    /**************************************
       * GUARDA VICTIMAS INVOLUCRADOS
       ***************************************/
    $('#btnModalVictimasEdit').click(function () {
        var codsinVictimas = ($('#tbSiniestroModEdit').val());
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
        ($('#ddl_posicionPlaza').val('-1'));
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
            url: '../traerDatosVehiculosInvolucradosVictimas',
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
        if ($('#ddl_vhlVictimas').val() !== '-1') {
            var codVehiculoVictima = ($('#ddl_vhlVictimas').val());
            $.ajax({
                type: "get",
                url: '../ObtenerTipoVehiculoVictimas',
                data: { codVehiculo: codVehiculoVictima },
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
                        if (retorno.toString() === '8') {
                            $("#cb_usoCascoVictima").prop("checked", true);
                            $("#cb_usoCinturonVictima").prop("checked", false);
                            $("#cb_2").attr("disabled", "disabled");
                            $("#cb_3").attr("disabled", "disabled");
                            $("#cb_5").attr("disabled", "disabled");
                            $("#cb_6").attr("disabled", "disabled");
                            $("#cb_8").attr("disabled", "disabled");
                            $("#cb_9").attr("disabled", "disabled");
                            $("#cb_10").attr("disabled", "disabled");
                            $("#cb_11").attr("disabled", "disabled");
                            $("#cb_12").attr("disabled", "disabled");
                            $("#cb_13").attr("disabled", "disabled");
                        }
                        else if (retorno.toString() === '16') {
                            $("#cb_usoCascoVictima").prop("checked", true);
                            $("#cb_usoCinturonVictima").prop("checked", false);
                            $("#cb_2").attr("disabled", "disabled");
                            $("#cb_3").attr("disabled", "disabled");
                            $("#cb_5").attr("disabled", "disabled");
                            $("#cb_6").attr("disabled", "disabled");
                            $("#cb_8").attr("disabled", "disabled");
                            $("#cb_9").attr("disabled", "disabled");
                            $("#cb_10").attr("disabled", "disabled");
                            $("#cb_11").attr("disabled", "disabled");
                            $("#cb_12").attr("disabled", "disabled");
                            $("#cb_13").attr("disabled", "disabled");
                        }
                        else {
                            $("#cb_usoCinturonVictima").prop("checked", true);
                            $("#cb_usoCascoVictima").prop("checked", false);
                            $("#cb_1").removeAttr('disabled');
                            $("#cb_2").removeAttr('disabled');
                            $("#cb_3").removeAttr('disabled');
                            $("#cb_4").removeAttr('disabled');
                            $("#cb_5").removeAttr('disabled');
                            $("#cb_6").removeAttr('disabled');
                            $("#cb_7").removeAttr('disabled');
                            $("#cb_8").removeAttr('disabled');
                            $("#cb_9").removeAttr('disabled');
                            $("#cb_10").removeAttr('disabled');
                            $("#cb_11").removeAttr('disabled');
                            $("#cb_12").removeAttr('disabled');
                            $("#cb_13").removeAttr('disabled');
                        }


                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus + ": " + XMLHttpRequest.responseText);
                }

            });
        }
    });

    //$('#ddl_tipoIdentificacion').change(function () {
    //    if ($('#ddl_tipoIdentificacion').val() === '4') {
    //        ($('#tb_IdentificacionVictima').val('NI'));
    //        ($('#tb_edadVictima').val('-1'));
    //        ($('#ddl_generoVictima').val('N'));
    //        // ($('#btnBuscarVict').css("display", "none"));
    //        ($('#ddl_tipoParticipante').val('-1'));


    //    }
    //    else if ($('#ddl_tipoIdentificacion').val() === '2') {
    //        ($('#ddl_tipoParticipante').val('-1'));
    //        //  ($('#btnBuscarVict').css("display", "none"));
    //        ($('#tb_edadVictima').val('0'));
    //        ($('#ddl_generoVictima').val('-1'));
    //    }
    //    else if ($('#ddl_tipoIdentificacion').val() === '-1') {
    //        ($('#ddl_tipoParticipante').val('-1'));
    //    }
    //    else {
    //        ($('#tb_IdentificacionVictima').val(''));
    //        ($('#tb_edadVictima').val('0'));
    //        ($('#ddl_generoVictima').val('-1'));
    //        //  ($('#btnBuscarVict').css("display", "block"));
    //        ($('#ddl_tipoParticipante').val('-1'));
    //    }
    //});

    $("#btnBuscarVictEdit").click(function () {
        if ($('#ddl_tipoIdentificacionEdit').val() === '1') {

            ObtenerDatosConsultaPorCedulaEdit();
        }
        else if ($('#ddl_tipoIdentificacionEdit').val() === '2') {

            ObtenerDatosConsultaPorLicenciaEdit();

        }
        
        
    });




    $('#btnLimpiarVictimasEdit').click(function () {
        inicializaVictimasEdit();
        $('#ddl_consumo_alcoholEdit').removeAttr("disabled");
        $('#ddl_uso_cascoEdit').removeAttr("disabled");
        $('#ddl_uso_cinturonEdit').removeAttr("disabled");
        ddl_uso_cinturonEdit
        $('#respuesta-ej3').toggle();
        $('#respuesta-ej4').toggle();
        
        ($('#tbModificarVictimas').css("display", "none"));
        ($('#tbGudarVictimasEdit').css("display", "block"));
    });

    $('#tbGudarVictimasEdit').click(function () {
        ($('#divMensajeVictimasEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
        ($('#panelMensajeVictimasEdit').css("display", "none"));
        var codsinVicitma = ($('#tbSiniestroModEdit').val());
        var codVhlVictima = ($('#ddl_vhlVictimasEdit').val());
        var tipoIdentificacion = ($('#ddl_tipoIdentificacionEdit option:selected').text());
        var numeroIdentificacion = ($('#tb_IdentificacionVictimaEdit').val());
        var edadVictima = ($('#tb_edadVictimaEdit').val());
        var generoVictima = ($('#ddl_generoVictimaEdit').val());
        var condicionVictima24 = ($('#ddl_condicionVictimas24Edit option:selected').text());
        var condicionVictima30 = ($('#ddl_condicionVictimas24Edit option:selected').text());
        var tipoParticipanteVictima = ($('#ddl_tipoParticipanteEdit option:selected').text());
        var posicionPlazaVictima = "";//($('#ddl_posicionPlaza option:selected').text());
        var usoCasoVictimaEdit = $("#ddl_uso_cascoEdit").val();
        var usoCinturonVictimaEdit = $("#ddl_uso_cinturonEdit").val();
        var consumoAlcoholVictimaEdit = $("#ddl_consumo_alcoholEdit").val();
        var accionesPeaton = ($('#ddl_accionesPeatonEdit option:selected').text());

        if (tipoParticipanteVictima === 'PEATÓN' || tipoParticipanteVictima === 'PEATÓN') {
            $('#ddl_accionesPeaton').each(function () {

                accionesPeaton = ($('#ddl_accionesPeatonEdit option:selected').text());
            });
            //------SELECCIONAR------$("#cb_trabajosVia").prop("checked")
        }
        if ($('#cb_1E').prop("checked") === true) posicionPlazaVictima = "FRONTAL IZQUIERDO";
        if ($('#cb_2E').prop("checked") === true) posicionPlazaVictima = "FRONTAL CENTRAL";
        if ($('#cb_3E').prop("checked") === true) posicionPlazaVictima = "FRONTAL DERECHO";
        if ($('#cb_4E').prop("checked") === true) posicionPlazaVictima = "CENTRAL IZQUIERDO";
        if ($('#cb_5E').prop("checked") === true) posicionPlazaVictima = "CENTRAL";
        if ($('#cb_6E').prop("checked") === true) posicionPlazaVictima = "CENTRAL DERECHO";
        if ($('#cb_7E').prop("checked") === true) posicionPlazaVictima = "TRASERO IZQUIERDO";
        if ($('#cb_8E').prop("checked") === true) posicionPlazaVictima = "TRASERO CENTRAL";
        if ($('#cb_9E').prop("checked") === true) posicionPlazaVictima = "TRASERO DERECHO";
        if ($('#cb_10E').prop("checked") === true) posicionPlazaVictima = "BALDE";
        if ($('#cb_11E').prop("checked") === true) posicionPlazaVictima = "DE PIE";
        if ($('#cb_12E').prop("checked") === true) posicionPlazaVictima = "OTROS";
        if ($('#cb_13E').prop("checked") === true) posicionPlazaVictima = "NIÑO EN BRAZOS";
        console.log(posicionPlazaVictima);
        var mensaje = "";
        if (codsinVicitma === '0' || codsinVicitma.toString().trim() === '') {
            mensaje = 'Debe ingresar un siniestro.';
            ($('#divMensajeVictimasEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
        }
        else if (codVhlVictima === '' || codVhlVictima === '-1' || codVhlVictima === '0') {
            mensaje = 'Seleccione el Vehiculo involucrado.';
            ($('#divMensajeVictimasEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
        }
        else if (tipoIdentificacion === '' || tipoIdentificacion === 'SELECCIONAR' || tipoIdentificacion === '-1') {
            mensaje = 'Seleccione el tipo de identificacion.';
            ($('#divMensajeVictimasEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
        }
        else if ((numeroIdentificacion.trim() === '' || parseInt(numeroIdentificacion) < 10) && tipoIdentificacion !== 'NO IDENTIFICADO') {
            mensaje = 'Ingrese el numero de identificacion.';
            ($('#divMensajeVictimasEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
        }
        else if (edadVictima.trim() === '') {
            mensaje = 'Ingrese la edad de la víctima.';
            ($('#divMensajeVictimasEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
        }
        else if ((generoVictima.trim() === '' || generoVictima === '-1')) {
            mensaje = 'Seleccione el genero de la víctima.';
            ($('#divMensajeVictimasEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
        }
        else if ((condicionVictima24.trim() === '' || condicionVictima24 === 'SELECCIONAR')) {
            mensaje = 'Seleccione la Condición Víctima 24h.';
            ($('#divMensajeVictimasEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
        }
        else if ((condicionVictima30.trim() === '' || condicionVictima30 === 'SELECCIONAR') && tipoIdentificacion !== 'NO IDENTIFICADO') {
            mensaje = 'Seleccione la Condición Víctima 30d.';
            ($('#divMensajeVictimasEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
        }
        else if ((tipoParticipanteVictima.trim() === '' || tipoParticipanteVictima === 'SELECCIONAR')) {
            mensaje = 'Seleccione el tipo participante';
            ($('#divMensajeVictimasEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
        }
        else if ((usoCasoVictimaEdit.trim() === '' || usoCasoVictimaEdit === '-1')) {
            mensaje = 'Seleccione el uso del casco';
            ($('#divMensajeVictimas').attr("class", "alert alert-block alert-danger fade in smaller"));
        }
        else if ((usoCinturonVictimaEdit.trim() === '' || usoCinturonVictimaEdit === '-1')) {
            mensaje = 'Seleccione el uso del cinturon';
            ($('#divMensajeVictimas').attr("class", "alert alert-block alert-danger fade in smaller"));
        }
        else if ((consumoAlcoholVictimaEdit.trim() === '' || consumoAlcoholVictimaEdit === '-1')) {
            mensaje = 'Seleccione si tiene o no cunsumo de alcohol';
            ($('#divMensajeVictimas').attr("class", "alert alert-block alert-danger fade in smaller"));
        }
        else if ((posicionPlazaVictima.trim() === '' || posicionPlazaVictima === 'SELECCIONAR')) {
            mensaje = 'Seleccione la Posición de la Plaza.';
            ($('#divMensajeVictimasEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
        }
        else if ((tipoParticipanteVictima === 'PEATÓN' || tipoParticipanteVictima === 'PEATÓN') && (accionesPeaton === '------SELECCIONAR------' || accionesPeaton === '')) {
            mensaje = 'Seleccione las acciónes del peatón.';
            ($('#divMensajeVictimasEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
        }

        else {
                    //var accionesPeatones = "";
                    if (tipoParticipanteVictima === 'PEATÓN' || tipoParticipanteVictima === 'CONDUCTOR') {
                        $('#ddl_accionesPeatonEdit').each(function () {
                            accionesPeaton = $('#ddl_accionesPeatonEdit').val();

                        });

                    }
            console.log(accionesPeaton);
            var ccc = accionesPeaton.toString();
            var jsonStringAccion = JSON.stringify(accionesPeaton);


            var fecha_actual = Date.toLocaleString();
            var fechaSin = ($('#tbfechaSiniestroEdit').val());
            fecha_actual = new Date().toJSON().slice(0, 10)
            console.log('fecha_actual ' + '' + fecha_actual);
            console.log('fechaSin ' + '' + fechaSin);
            var anio_max = fechaSin.split('-')[0];
            var mes_max = parseInt(fechaSin.split('-')[1]) + 1;
            var dia_max = '10';
            var dia_min = '01';
            var fecha_max = anio_max + '-' + mes_max + '-' + dia_max;
            var fecha_min = anio_max + '-' + fechaSin.split('-')[1] + '-' + dia_min;
            console.log('fecha_max ' + '' + fecha_max);
            console.log('fecha_min ' + '' + fecha_min);
            fecha_max = fecha_max.split('-');
            fecha_actual = fecha_actual.split('-');
            fechaSin = fechaSin.split('-');
            fecha_min = fecha_min.split('-');
            fecha_max = new Date(fecha_max[0], fecha_max[1] - 1, fecha_max[2]).valueOf();
            fecha_actual = new Date(fecha_actual[0], fecha_actual[1] - 1, fecha_actual[2]).valueOf();
            fechaSin = new Date(fechaSin[0], fechaSin[1] - 1, fechaSin[2]).valueOf();
            fecha_min = new Date(fecha_min[0], fecha_min[1] - 1, fecha_min[2]).valueOf();
            console.log('------------------------------------------');
            console.log('fecha_max ' + '' + fecha_max);
            console.log('fecha_actual ' + '' + fecha_actual);
            console.log('fechaSin ' + '' + fechaSin);
            console.log('fecha_min ' + '' + fecha_min);
            var _fecha_actual_sistema = new Date().toJSON().slice(0, 10);
            _fecha_actual_sistema = new Date(_fecha_actual_sistema).valueOf();
          //  if (fecha_max >= _fecha_actual_sistema && fechaSin <= fecha_max && fechaSin >= fecha_min ) {

                //$("#dt_victimasSiniestro").find("tr:gt(0)").remove();
                $.ajax({
                    type: "get",
                    url: '../GuardarVictimasInvolucradas',
                    data: { tipidenvicinv: tipoIdentificacion, numidenvicinv: numeroIdentificacion, edavicinv: edadVictima, genvicinv: generoVictima, convicinv24: condicionVictima24, convicinv30: condicionVictima30, tipparvicinv: tipoParticipanteVictima, casvicinv: usoCasoVictimaEdit, cinvicinv: usoCinturonVictimaEdit, posvicinv: posicionPlazaVictima, conalcvicinv: consumoAlcoholVictimaEdit, codsin: codsinVicitma, codveh: codVhlVictima, desaccpea: jsonStringAccion, ccc: ccc, codproceso:codprocesoAnterior },
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (jsonResult) {
                        console.log('retorno victimas' + ' ' + jsonResult);
                        if (jsonResult.length === 0) {
                            mensaje = 'Error al grabar los datos.';
                            ($('#mensajeLabelVictimasEdit').html(mensaje.toString()));
                            ($('#divMensajeVictimasEdit').attr("class", "alert alert-block alert-danger fade in smaller"));
                        }
                        else if (jsonResult.length > 0) {
                            //($('#tb_codsinVictimas').val('0'));
                            inicializaVictimasEdit();

                            mensaje = 'Víctima insertada correctamente.';
                            ($('#divMensajeVictimasEdit').attr("class", "alert alert-success smaller"));
                            ($('#mensajeLabelVictimasEdit').html(mensaje.toString()));
                            ($('#panelMensajeVictimasEdit').css("display", "block"));
                            $('#respuesta-ej3').toggle();
                            $('#respuesta-ej4').toggle();
                            $.each(jsonResult, function (key, value) {
                                /* Vamos agregando a nuestra tabla las filas necesarias */
                              //  $("#dt_victimasSiniestro").append("<tr><td>" + value.tipidenvicinv + "</td><td>" + value.numidenvicinv + "</td><td>" + value.edavicinv + "</td><td>" + value.sexo + "</td><td>" + value.convicinv24 + "</td><td>" + value.tipparvicinv + "</td><td>" + value.usO_CASO + "</td><td>" + value.usO_CINTU + "</td><td>" + value.posvicinv + "</td><td>" + value.conS_ALCOHOL + "</td></tr>");
                            });

                            TablaVictimas(codsin);
                        }

                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert(textStatus + ": " + XMLHttpRequest.responseText);
                    }

                });
          //  }
            //else {
            //    mensaje = "No puede agregar nuevas victimas, la fecha del siniestro esta fuera del rango permitido.";
            //}
        }
        console.log(mensaje.toString());
        ($('#mensajeLabelVictimasEdit').html(mensaje.toString()));
        ($('#panelMensajeVictimasEdit').css("display", "block"));
        //console.log('para ingresar a la funcion');
        // var _codsin = ($('#tb_codsinVictimas').val());
        //ObtenerTraerNumLesionadosFallecido(_codsin);
    });
    //ObtenerTraerNumLesionadosFallecido
    $('#btnCerrarVictimasEdit').click(function () {
        //var codsin = ($('#tb_codsinVictimas').val());
        //$.ajax({
        //    type: "get",
        //    url: '../ObtenerTraerNumLesionadosFallecidos',
        //    data: { codsin: codsin },
        //    contentType: "application/json; charset=utf-8",
        //    dataType: "json",
        //    success: function (jsonResult) {
        //        console.log(jsonResult);
        //        if (jsonResult.length > 0) {
        //            $.each(jsonResult, function (key, value) {
        //                ($('#tbnumlessinEdit').val(value.value.toString()));
        //                ($('#tbnumfalsinEdit').val(value.text.toString()));
        //            });

        //        }
        //    },
        //    error: function (XMLHttpRequest, textStatus, errorThrown) {
        //        alert(textStatus + ": " + XMLHttpRequest.responseText);
        //    }
            
        //});
        location.reload();
    });
   
    $('#ddl_tipoParticipanteEdit').change(function () {
        var retorno = $('#tbTipoVhlVictimaEdit').val();
       

        if ($('#ddl_tipoParticipanteEdit option:selected').text() === 'PEATÓN') {
            $('#ddl_accionesPeatonEdit').css("display", "block");
            $('#divAccionesPeatonEdit').css("display", "block");
            
            ($('#ddl_accionesPeatonEdit').val('-1'));
            $("#ddl_uso_cascoEdit").val('2');
            $("#ddl_uso_cinturonEdit").val('2');
            $("#ddl_consumo_alcoholEdit").val('2');
            $("#cb_1E").prop("checked", false);
            $("#cb_2E").prop("checked", false);
            $("#cb_3E").prop("checked", false);
            $("#cb_4E").prop("checked", false);
            $("#cb_5E").prop("checked", false);
            $("#cb_6E").prop("checked", false);
            $("#cb_7E").prop("checked", false);
            $("#cb_8E").prop("checked", false);
            $("#cb_9E").prop("checked", false);
            $("#cb_10E").prop("checked", false);
            $("#cb_11E").prop("checked", true);
            $("#cb_12E").prop("checked", false);
            $("#cb_13E").prop("checked", false);


            $("#cb_1E").prop("checked", false);
            $("#cb_2E").prop("checked", false);
            $("#cb_3E").prop("checked", false);
            $("#cb_4E").prop("checked", false);
            $("#cb_5E").prop("checked", false);
            $("#cb_6E").prop("checked", false);
            $("#cb_7E").prop("checked", false);
            $("#cb_8E").prop("checked", false);
            $("#cb_9E").prop("checked", false);
            $("#cb_10E").prop("checked", false);
            $("#cb_12E").prop("checked", false);
            $("#cb_13E").prop("checked", false);
            $("#cb_11E").prop("checked", true);
            $("#cb_1E").attr("disabled", "disabled");
            $("#cb_2E").attr("disabled", "disabled");
            $("#cb_3E").attr("disabled", "disabled");
            $("#cb_4E").attr("disabled", "disabled");
            $("#cb_5E").attr("disabled", "disabled");
            $("#cb_6E").attr("disabled", "disabled");
            $("#cb_7E").attr("disabled", "disabled");
            $("#cb_8E").attr("disabled", "disabled");
            $("#cb_9E").attr("disabled", "disabled");
            $("#cb_10E").attr("disabled", "disabled");
            $("#cb_12E").attr("disabled", "disabled");
            $("#cb_13E").attr("disabled", "disabled");
            $('#ddl_accionesPeatonEdit').css("display", "block");
            $('#divAccionesPeatonEdit').css("display", "block");
            ($('#ddl_accionesPeatonEdit').val('-1'));
            $("#ddl_uso_cascoEdit").attr("disabled", "disabled");
            $("#ddl_uso_cinturonEdit").attr("disabled", "disabled");
            $("#ddl_consumo_alcoholEdit").attr("disabled", "disabled");
            $("#ddl_uso_cascoEdit").val("2");
            $("#ddl_uso_cinturonEdit").val("2");
            $("#ddl_consumo_alcoholEdit").val("2");
            $('#divAccionesPeatonEdit').css("display", "block");
            $("#ddl_consumo_alcoholEdit").removeAttr("disabled");
        }
        else if ($('#ddl_tipoParticipanteEdit option:selected').text() === 'CONDUCTOR' && (retorno.toString() !== "8" && retorno.toString() !== "16")) {
            

            /////////////////////////////////////


            $('#ddl_accionesPeatonEdit').css("display", "block");
            $('#divAccionesPeatonEdit').css("display", "block");
            $('#divAccionesPeatonEdit').css("display", "block");
            ($('#ddl_accionesPeatonEdit').val('-1'));
            //$("#cb_usoCascoVictima").prop("checked", false);
            //$("#cb_usoCinturonVictima").prop("checked", true);
            //$("#cb_consumoAlcoholVictima").prop("checked", false);
            $("#cb_1E").prop("checked", true);
            $("#cb_2E").prop("checked", false);
            $("#cb_3E").prop("checked", false);
            $("#cb_4E").prop("checked", false);
            $("#cb_5E").prop("checked", false);
            $("#cb_6E").prop("checked", false);
            $("#cb_7E").prop("checked", false);
            $("#cb_8E").prop("checked", false);
            $("#cb_9E").prop("checked", false);
            $("#cb_10E").prop("checked", false);
            $("#cb_11E").prop("checked", false);
            $("#cb_12E").prop("checked", false);
            $("#cb_13E").prop("checked", false);

            $("#cb_1E").removeAttr("disabled");
            $("#cb_2E").attr("disabled", "disabled");
            $("#cb_3E").attr("disabled", "disabled");
            $("#cb_4E").attr("disabled", "disabled");
            $("#cb_5E").attr("disabled", "disabled");
            $("#cb_6E").attr("disabled", "disabled");
            $("#cb_7E").attr("disabled", "disabled");
            $("#cb_8E").attr("disabled", "disabled");
            $("#cb_9E").attr("disabled", "disabled");
            $("#cb_10E").attr("disabled", "disabled");
            $("#cb_11E").attr("disabled", "disabled");
            $("#cb_12E").attr("disabled", "disabled");
            $("#cb_13E").attr("disabled", "disabled");

            $("#ddl_uso_cascoEdit").removeAttr("disabled");
            $("#ddl_uso_cascoEdit").val("2");
            $("#ddl_uso_cinturonEdit").val("-1");
            $("#ddl_consumo_alcoholEdit").val("-1");
            $("#ddl_uso_cinturonEdit").removeAttr("disabled");
            if (retorno.toString() === "8" && retorno.toString() === "16") {
             
                $("#ddl_uso_cinturonEdit").val("2");
                $("#ddl_uso_cinturonEdit").attr("disabled", "disabled");
            }
            else
                $("#ddl_uso_cinturonEdit").removeAttr("disabled");

            $("#ddl_consumo_alcoholEdit").val("2");
            

            $("#ddl_uso_cascoEdit").attr("disabled", "disabled");

            $('#ddl_accionesPeatonEdit').css("display", "block");
            $('#divAccionesPeatonEdit').css("display", "block");
        }
        else if ($('#ddl_tipoParticipanteEdit option:selected').text() === 'CONDUCTOR' && (retorno.toString() === "8" || retorno.toString() === "16")) {
            $('#ddl_accionesPeatonEdit').css("display", "block");
            $('#divAccionesPeatonEdit').css("display", "block");

            ($('#ddl_accionesPeatonEdit').val('-1'));
            $("#ddl_uso_cascoEdit").val('1');
            $("#ddl_uso_cinturonEdit").val('2');
            $("#ddl_consumo_alcoholEdit").val('2');
            $("#cb_1E").prop("checked", true);
            $("#cb_2E").prop("checked", false);
            $("#cb_3E").prop("checked", false);
            $("#cb_4E").prop("checked", false);
            $("#cb_5E").prop("checked", false);
            $("#cb_6E").prop("checked", false);
            $("#cb_7E").prop("checked", false);
            $("#cb_8E").prop("checked", false);
            $("#cb_9E").prop("checked", false);
            $("#cb_10E").prop("checked", false);
            $("#cb_11E").prop("checked", false);
            $("#cb_12E").prop("checked", false);
            $("#cb_13E").prop("checked", false);


            $("#cb_4E").attr("disabled", "disabled");
            $("#cb_7E").attr("disabled", "disabled");
            $("#cb_8E").attr("disabled", "disabled");
            $("#cb_9E").attr("disabled", "disabled");
            $("#cb_10E").attr("disabled", "disabled");
            $("#cb_11E").attr("disabled", "disabled");
            $("#cb_12E").attr("disabled", "disabled");
            $("#cb_13E").attr("disabled", "disabled");

            $("#ddl_uso_cinturonEdit").val("2");
            $("#ddl_uso_cinturonEdit").attr("disabled", "disabled");

            $("#ddl_consumo_alcoholEdit").removeAttr("disabled");
            $('#ddl_accionesPeatonEdit').css("display", "block");
            $('#divAccionesPeatonEdit').css("display", "block");
        }

        else if ($('#ddl_tipoParticipanteEdit option:selected').text() === 'PASAJERO' && (retorno.toString() !== "8" && retorno.toString() !== "16")) {
     


            
            $('#ddl_accionesPeatonEdit').css("display", "none");
            $('#divAccionesPeatonEdit').css("display", "none");
            $('#divAccionesPeatonEdit').css("display", "none");
            ($('#ddl_accionesPeatonEdit').val('-1'));
            $("#ddl_uso_cascoEdit").attr("disabled", "disabled");
            $("#ddl_uso_cascoEdit").val('2');
            //$("#cb_usoCascoVictima").prop("checked", false);
            //$("#cb_usoCinturonVictima").prop("checked", false);
            //$("#cb_consumoAlcoholVictima").prop("checked", false);
            $("#cb_1E").prop("checked", false);
            $("#cb_2E").prop("checked", false);
            $("#cb_3E").prop("checked", false);
            $("#cb_4E").prop("checked", false);
            $("#cb_5E").prop("checked", false);
            $("#cb_6E").prop("checked", false);
            $("#cb_7E").prop("checked", false);
            $("#cb_8E").prop("checked", false);
            $("#cb_9E").prop("checked", false);
            $("#cb_10E").prop("checked", false);
            $("#cb_11E").prop("checked", false);
            $("#cb_12E").prop("checked", false);
            $("#cb_13E").prop("checked", false);

            $("#cb_1E").attr("disabled", "disabled");
            $("#cb_2E").removeAttr("disabled");
            $("#cb_3E").removeAttr("disabled");
            $("#cb_4E").removeAttr("disabled");
            $("#cb_5E").removeAttr("disabled");
            $("#cb_6E").removeAttr("disabled");
            $("#cb_7E").removeAttr("disabled");
            $("#cb_8E").removeAttr("disabled");
            $("#cb_9E").removeAttr("disabled");
            $("#cb_10E").removeAttr("disabled");
            $("#cb_11E").removeAttr("disabled");
            $("#cb_12E").removeAttr("disabled");
            $("#cb_13E").removeAttr("disabled");
            $("#ddl_uso_cinturonEdit").removeAttr("disabled");

            $("#ddl_consumo_alcoholEdit").removeAttr("disabled");
        }
        else if ($('#ddl_tipoParticipanteEdit option:selected').text() === 'PASAJERO' && (retorno.toString() === "8" || retorno.toString() === "16")) {
          //  alert(retorno.toString());
            $('#ddl_accionesPeatonEdit').css("display", "none");
            $('#divAccionesPeatonEdit').css("display", "none");
            $('#divAccionesPeatonEdit').css("display", "none");
            ($('#ddl_accionesPeatonEdit').val('-1'));
            ($('#ddl_uso_cinturonEdit').val('2'));
            $("#ddl_uso_cinturonEdit").attr("disabled", "disabled");
            //$("#cb_usoCascoVictima").prop("checked", false);
            //$("#cb_usoCinturonVictima").prop("checked", false);
            //$("#cb_consumoAlcoholVictima").prop("checked", false);
            $("#cb_1E").prop("checked", false);
            $("#cb_2E").prop("checked", false);
            $("#cb_3E").prop("checked", false);
            $("#cb_4E").prop("checked", false);
            $("#cb_5E").prop("checked", false);
            $("#cb_6E").prop("checked", false);
            $("#cb_7E").prop("checked", false);
            $("#cb_8E").prop("checked", false);
            $("#cb_9E").prop("checked", false);
            $("#cb_10E").prop("checked", false);
            $("#cb_11E").prop("checked", false);
            $("#cb_12E").prop("checked", false);
            $("#cb_13E").prop("checked", false);

            $("#cb_1E").attr("disabled", "disabled");
            $("#cb_2E").attr("disabled", "disabled");
            $("#cb_3E").attr("disabled", "disabled");
            $("#cb_4E").removeAttr("disabled");
            $("#cb_5E").attr("disabled", "disabled");
            $("#cb_6E").attr("disabled", "disabled");
            $("#cb_7E").removeAttr("disabled");
            $("#cb_8E").attr("disabled", "disabled");
            $("#cb_9E").attr("disabled", "disabled");
            $("#cb_10E").attr("disabled", "disabled");
            $("#cb_11E").attr("disabled", "disabled");
            $("#cb_12E").removeAttr("disabled");
            $("#cb_13E").removeAttr("disabled");

            ////////////////////////////////////

            $('#ddl_accionesPeatonEdit').css("display", "none");
            $('#divAccionesPeatonEdit').css("display", "none");
            $('#divAccionesPeatonEdit').css("display", "none");
            ($('#ddl_accionesPeatonEdit').val('-1'));
            ($('#ddl_uso_cinturonEdit').val('2'));
            $("#ddl_uso_cinturonEdit").attr("disabled", "disabled");
            //$("#cb_usoCascoVictima").prop("checked", false);
            //$("#cb_usoCinturonVictima").prop("checked", false);
            //$("#cb_consumoAlcoholVictima").prop("checked", false);
            $("#cb_1E").prop("checked", false);
            $("#cb_2E").prop("checked", false);
            $("#cb_3E").prop("checked", false);
            $("#cb_4E").prop("checked", false);
            $("#cb_5E").prop("checked", false);
            $("#cb_6E").prop("checked", false);
            $("#cb_7E").prop("checked", false);
            $("#cb_8E").prop("checked", false);
            $("#cb_9E").prop("checked", false);
            $("#cb_10E").prop("checked", false);
            $("#cb_11E").prop("checked", false);
            $("#cb_12E").prop("checked", false);
            $("#cb_13E").prop("checked", false);

            $("#cb_1E").attr("disabled", "disabled");
            $("#cb_2E").attr("disabled", "disabled");
            $("#cb_3E").attr("disabled", "disabled");
            $("#cb_4E").removeAttr("disabled");
            $("#cb_5E").attr("disabled", "disabled");
            $("#cb_6E").attr("disabled", "disabled");
            $("#cb_7E").removeAttr("disabled");
            $("#cb_8E").attr("disabled", "disabled");
            $("#cb_9E").attr("disabled", "disabled");
            $("#cb_10E").attr("disabled", "disabled");
            $("#cb_11E").attr("disabled", "disabled");
            $("#cb_12E").removeAttr("disabled");
            $("#cb_13E").removeAttr("disabled");

            $("#ddl_uso_cascoEdit").removeAttr("disabled");
            $("#ddl_consumo_alcoholEdit").removeAttr("disabled");
        }
    });



    /**************************************
 * GUARDA DANIOS A TERCEROS
 ***************************************/
    $('#btnModalDaniosTercerosEdit').click(function () {
      
    });


   
   
    //($('#panelMensaje').css("display", "none"));
    $('#btnLimpiarDaniosEdit').click(function () {
        ($('#panelMensaje').css("display", "none"));
        ($('#tb_obsdaterEdit').val(''));
        ($('#ddl_tipoDanioTerceroEdit').val('-1'));
        $('#respuesta-ej5').toggle();
        $('#respuesta-ej6').toggle();
        ($('#tbGudarDanioTerceroEdit').css("display", "block"));
        ($('#tbModificaDanioTercero').css("display", "none"));
    });


    $('#tbGudarDanioTerceroEdit').click(function () {
        ($('#divMensaje').attr("class", "alert alert-block alert-danger fade in smaller"));
        var mensaje = '';
        var codsiniestroDanioTercero = ($('#tbSiniestroModEdit').val());
        var codTipoDanioTercero = ($('#ddl_tipoDanioTerceroEdit').val());
        var observacionDanioTercero = ($('#tb_obsdaterEdit').val());
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

            var fecha_actual = Date.toLocaleString();
            var fechaSin = ($('#tbfechaSiniestroEdit').val());
            fecha_actual = new Date().toJSON().slice(0, 10)
            console.log('fecha_actual ' + '' + fecha_actual);
            console.log('fechaSin ' + '' + fechaSin);
            var anio_max = fechaSin.split('-')[0];
            var mes_max = parseInt(fechaSin.split('-')[1]) + 1;
            var dia_max = '10';
            var dia_min = '01';
            var fecha_max = anio_max + '-' + mes_max + '-' + dia_max;
            var fecha_min = anio_max + '-' + fechaSin.split('-')[1] + '-' + dia_min;
            console.log('fecha_max ' + '' + fecha_max);
            console.log('fecha_min ' + '' + fecha_min);
            fecha_max = fecha_max.split('-');
            fecha_actual = fecha_actual.split('-');
            fechaSin = fechaSin.split('-');
            fecha_min = fecha_min.split('-');
            fecha_max = new Date(fecha_max[0], fecha_max[1] - 1, fecha_max[2]).valueOf();
            fecha_actual = new Date(fecha_actual[0], fecha_actual[1] - 1, fecha_actual[2]).valueOf();
            fechaSin = new Date(fechaSin[0], fechaSin[1] - 1, fechaSin[2]).valueOf();
            fecha_min = new Date(fecha_min[0], fecha_min[1] - 1, fecha_min[2]).valueOf();
            console.log('------------------------------------------');
            console.log('fecha_max ' + '' + fecha_max);
            console.log('fecha_actual ' + '' + fecha_actual);
            console.log('fechaSin ' + '' + fechaSin);
            console.log('fecha_min ' + '' + fecha_min);
            var _fecha_actual_sistema = new Date().toJSON().slice(0, 10);
            _fecha_actual_sistema = new Date(_fecha_actual_sistema).valueOf();
          //  if (fecha_max >= _fecha_actual_sistema && fechaSin <= fecha_max && fechaSin >= fecha_min) {


               // $("#dt_DaniosTercerosEdit").find("tr:gt(0)").remove();
                $.ajax({

                    type: "get",
                    url: '../GuardarDanioTercero',
                    data: { codsin: ($('#tbSiniestroModEdit').val()), obsdater: ($('#tb_obsdaterEdit').val()), codtipdater: ($('#ddl_tipoDanioTerceroEdit').val()) },
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (jsonResult) {
                        // datoRetornoCodigoDanioMaterial = jsonResult.toString();
                        console.log('retorno danio tercero');
                        console.log(jsonResult);
                        //console.log('datoRetornoCodigoDanioMaterial' + ' ' + datoRetornoCodigoDanioMaterial.toString());
                        if (jsonResult.length === 0) {
                            mensaje = 'No se puede registrar la infromación';
                            ($('#mensajeLabel').html(mensaje.toString()));

                            ($('#panelMensaje').css("display", "block"));
                        }
                        else if (jsonResult.length > 0) {
                            mensaje = "Información registrada correctamente.";
                            console.log(mensaje.toString());
                            ($('#mensajeLabel').html(mensaje.toString()));
                            ($('#divMensaje').attr("class", "alert alert-success smaller"));

                            ($('#tb_obsdater').val(''));
                            ($('#ddl_tipoDanioTercero').val('-1'));
                            ($('#panelMensaje').css("display", "block"));
                            //$.each(jsonResult, function (key, value) {
                            //    /* Vamos agregando a nuestra tabla las filas necesarias coddater*/
                            //    $("#dt_DaniosTercerosEdit").append("<tr><td>" + value.codsin + "</td><td>" + value.destipdater + "</td><td>" + value.obsdater + "</td><td><a class=\"btnModalDaniosTercerosEdit btn\" data-id=" + value.coddater + " ><span class=\"glyphicon glyphicon-edit\"></span>Modificar</a></td></tr>");
                            //});
                            ($('#panelMensaje').css("display", "block"));
                            $('#respuesta-ej5').toggle();
                            $('#respuesta-ej6').toggle();
                            TablaDanios(codsin);
                        }

                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert(textStatus + ": " + XMLHttpRequest.responseText);
                    }
                });
            //}
            //else {
            //    mensaje = "No puede ingresar nuevos daños a terceros, la  fecha del siniestro se encuentra fuera del rango permitido";
                
            //    ($('#mensajeLabel').html(mensaje.toString()));

            //    ($('#panelMensaje').css("display", "block"));
            //}
           

        }
        //($('#panelMensaje').css("display", "block"));
        
    });


    //$('#ddl_lugarViaEdit').change(function () {
    //    $("#ddl_senializacionExistenteEdit").val('0');
    //    if ($('#ddl_lugarViaEdit option:selected').text() === 'INTERSECCIÓN') {
    //        $("#ddl_controlInterseccionEdit").removeAttr('disabled');
    //        $("#ddl_controlInterseccionEdit").val('0');
    //        $("#ddl_curvaExistenteEdit").val('4');
    //        $("#ddl_curvaExistenteEdit").attr('disabled', 'disabled');
    //    }
    //    else if ($('#ddl_lugarViaEdit option:selected').text() === 'CURVA') {
    //        $("#ddl_curvaExistenteEdit").val('-1');
    //        $("#ddl_curvaExistenteEdit").removeAttr('disabled');
    //        $("#ddl_controlInterseccionEdit").attr('disabled', 'disabled');
    //        $("#ddl_controlInterseccionEdit").val('8');
    //    }
    //    else {
    //        $("#ddl_controlInterseccionEdit").attr('disabled', 'disabled');
    //        $("#ddl_controlInterseccionEdit").val('8');
    //        $("#ddl_curvaExistenteEdit").val('3');
    //        $("#ddl_curvaExistenteEdit").attr('disabled', 'disabled');
    //    }

    //});
    $('#ddl_controlInterseccionEdit').change(function () {
        if ($('#ddl_controlInterseccionEdit option:selected').text() === 'SEÑALIZACIÓN HORIZONTAL') {
            $("#ddl_senializacionExistenteEdit").val('1');
        }
        else if ($('#ddl_controlInterseccionEdit option:selected').text() === 'SEÑALIZACIÓN VERTICAL') {
            $("#ddl_senializacionExistenteEdit").val('2');
        }
        else if ($('#ddl_controlInterseccionEdit option:selected').text() === 'NINGUNA') {
            $("#ddl_senializacionExistenteEdit").val('4');
        }
        else { $("#ddl_senializacionExistenteEdit").val('0'); }
    });


    $('#btnCerrarDaniosEdit').click(function (){
        location.reload();
    });

    $('#btnCargarDireccionesMapaEdit').click(function () {
        cargarDireccionesMapaConfirmarEdit();
    });

    $("#cb_1").click(function () {
      
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
    });
    $("#cb_2").click(function () {
        $("#cb_1").prop("checked", false);
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
    });
    $("#cb_3").click(function () {
        $("#cb_1").prop("checked", false);
        $("#cb_2").prop("checked", false);
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
    });
    $("#cb_4").click(function () {
        $("#cb_1").prop("checked", false);
        $("#cb_2").prop("checked", false);
        $("#cb_3").prop("checked", false);
        $("#cb_5").prop("checked", false);
        $("#cb_6").prop("checked", false);
        $("#cb_7").prop("checked", false);
        $("#cb_8").prop("checked", false);
        $("#cb_9").prop("checked", false);
        $("#cb_10").prop("checked", false);
        $("#cb_11").prop("checked", false);
        $("#cb_12").prop("checked", false);
        $("#cb_13").prop("checked", false);
    });
    $("#cb_5").click(function () {
        $("#cb_1").prop("checked", false);
        $("#cb_2").prop("checked", false);
        $("#cb_3").prop("checked", false);
        $("#cb_4").prop("checked", false);
        $("#cb_6").prop("checked", false);
        $("#cb_7").prop("checked", false);
        $("#cb_8").prop("checked", false);
        $("#cb_9").prop("checked", false);
        $("#cb_10").prop("checked", false);
        $("#cb_11").prop("checked", false);
        $("#cb_12").prop("checked", false);
        $("#cb_13").prop("checked", false);
    });
    $("#cb_6").click(function () {
        $("#cb_1").prop("checked", false);
        $("#cb_2").prop("checked", false);
        $("#cb_3").prop("checked", false);
        $("#cb_4").prop("checked", false);
        $("#cb_5").prop("checked", false);
        $("#cb_7").prop("checked", false);
        $("#cb_8").prop("checked", false);
        $("#cb_9").prop("checked", false);
        $("#cb_10").prop("checked", false);
        $("#cb_11").prop("checked", false);
        $("#cb_12").prop("checked", false);
        $("#cb_13").prop("checked", false);
    });
    $("#cb_7").click(function () {
        $("#cb_1").prop("checked", false);
        $("#cb_2").prop("checked", false);
        $("#cb_3").prop("checked", false);
        $("#cb_4").prop("checked", false);
        $("#cb_5").prop("checked", false);
        $("#cb_6").prop("checked", false);
        $("#cb_8").prop("checked", false);
        $("#cb_9").prop("checked", false);
        $("#cb_10").prop("checked", false);
        $("#cb_11").prop("checked", false);
        $("#cb_12").prop("checked", false);
        $("#cb_13").prop("checked", false);
    });
    $("#cb_8").click(function () {
        $("#cb_1").prop("checked", false);
        $("#cb_2").prop("checked", false);
        $("#cb_3").prop("checked", false);
        $("#cb_4").prop("checked", false);
        $("#cb_5").prop("checked", false);
        $("#cb_6").prop("checked", false);
        $("#cb_7").prop("checked", false);
        $("#cb_9").prop("checked", false);
        $("#cb_10").prop("checked", false);
        $("#cb_11").prop("checked", false);
        $("#cb_12").prop("checked", false);
        $("#cb_13").prop("checked", false);
    });
    $("#cb_9").click(function () {
        $("#cb_1").prop("checked", false);
        $("#cb_2").prop("checked", false);
        $("#cb_3").prop("checked", false);
        $("#cb_4").prop("checked", false);
        $("#cb_5").prop("checked", false);
        $("#cb_6").prop("checked", false);
        $("#cb_7").prop("checked", false);
        $("#cb_8").prop("checked", false);
        $("#cb_10").prop("checked", false);
        $("#cb_11").prop("checked", false);
        $("#cb_12").prop("checked", false);
        $("#cb_13").prop("checked", false);
    });
    $("#cb_10").click(function () {
        $("#cb_1").prop("checked", false);
        $("#cb_2").prop("checked", false);
        $("#cb_3").prop("checked", false);
        $("#cb_4").prop("checked", false);
        $("#cb_5").prop("checked", false);
        $("#cb_6").prop("checked", false);
        $("#cb_7").prop("checked", false);
        $("#cb_8").prop("checked", false);
        $("#cb_9").prop("checked", false);
        $("#cb_11").prop("checked", false);
        $("#cb_12").prop("checked", false);
        $("#cb_13").prop("checked", false);
    });
    $("#cb_11").click(function () {
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
        $("#cb_12").prop("checked", false);
        $("#cb_13").prop("checked", false);
    });

    $("#cb_12").click(function () {
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
        $("#cb_13").prop("checked", false);
    });
    $("#cb_13").click(function () {
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
    });


    //////EDIT
    $("#cb_1E").click(function () {

        $("#cb_2E").prop("checked", false);
        $("#cb_3E").prop("checked", false);
        $("#cb_4E").prop("checked", false);
        $("#cb_5E").prop("checked", false);
        $("#cb_6E").prop("checked", false);
        $("#cb_7E").prop("checked", false);
        $("#cb_8E").prop("checked", false);
        $("#cb_9E").prop("checked", false);
        $("#cb_10E").prop("checked", false);
        $("#cb_11E").prop("checked", false);
        $("#cb_12E").prop("checked", false);
        $("#cb_13E").prop("checked", false);
    });
    $("#cb_2E").click(function () {
        $("#cb_1E").prop("checked", false);
        $("#cb_3E").prop("checked", false);
        $("#cb_4E").prop("checked", false);
        $("#cb_5E").prop("checked", false);
        $("#cb_6E").prop("checked", false);
        $("#cb_7E").prop("checked", false);
        $("#cb_8E").prop("checked", false);
        $("#cb_9E").prop("checked", false);
        $("#cb_10E").prop("checked", false);
        $("#cb_11E").prop("checked", false);
        $("#cb_12E").prop("checked", false);
        $("#cb_13E").prop("checked", false);
    });
    $("#cb_3E").click(function () {
        $("#cb_1E").prop("checked", false);
        $("#cb_2E").prop("checked", false);
        $("#cb_4E").prop("checked", false);
        $("#cb_5E").prop("checked", false);
        $("#cb_6E").prop("checked", false);
        $("#cb_7E").prop("checked", false);
        $("#cb_8E").prop("checked", false);
        $("#cb_9E").prop("checked", false);
        $("#cb_10E").prop("checked", false);
        $("#cb_11E").prop("checked", false);
        $("#cb_12E").prop("checked", false);
        $("#cb_13E").prop("checked", false);
    });
    $("#cb_4E").click(function () {
        $("#cb_1E").prop("checked", false);
        $("#cb_2E").prop("checked", false);
        $("#cb_3E").prop("checked", false);
        $("#cb_5E").prop("checked", false);
        $("#cb_6E").prop("checked", false);
        $("#cb_7E").prop("checked", false);
        $("#cb_8E").prop("checked", false);
        $("#cb_9E").prop("checked", false);
        $("#cb_10E").prop("checked", false);
        $("#cb_11E").prop("checked", false);
        $("#cb_12E").prop("checked", false);
        $("#cb_13E").prop("checked", false);
    });
    $("#cb_5E").click(function () {
        $("#cb_1E").prop("checked", false);
        $("#cb_2E").prop("checked", false);
        $("#cb_3E").prop("checked", false);
        $("#cb_4E").prop("checked", false);
        $("#cb_6E").prop("checked", false);
        $("#cb_7E").prop("checked", false);
        $("#cb_8E").prop("checked", false);
        $("#cb_9E").prop("checked", false);
        $("#cb_10E").prop("checked", false);
        $("#cb_11E").prop("checked", false);
        $("#cb_12E").prop("checked", false);
        $("#cb_13E").prop("checked", false);
    });
    $("#cb_6E").click(function () {
        $("#cb_1E").prop("checked", false);
        $("#cb_2E").prop("checked", false);
        $("#cb_3E").prop("checked", false);
        $("#cb_4E").prop("checked", false);
        $("#cb_5E").prop("checked", false);
        $("#cb_7E").prop("checked", false);
        $("#cb_8E").prop("checked", false);
        $("#cb_9E").prop("checked", false);
        $("#cb_10E").prop("checked", false);
        $("#cb_11E").prop("checked", false);
        $("#cb_12E").prop("checked", false);
        $("#cb_13E").prop("checked", false);
    });
    $("#cb_7E").click(function () {
        $("#cb_1E").prop("checked", false);
        $("#cb_2E").prop("checked", false);
        $("#cb_3E").prop("checked", false);
        $("#cb_4E").prop("checked", false);
        $("#cb_5E").prop("checked", false);
        $("#cb_6E").prop("checked", false);
        $("#cb_8E").prop("checked", false);
        $("#cb_9E").prop("checked", false);
        $("#cb_10E").prop("checked", false);
        $("#cb_11E").prop("checked", false);
        $("#cb_12E").prop("checked", false);
        $("#cb_13E").prop("checked", false);
    });
    $("#cb_8E").click(function () {
        $("#cb_1E").prop("checked", false);
        $("#cb_2E").prop("checked", false);
        $("#cb_3E").prop("checked", false);
        $("#cb_4E").prop("checked", false);
        $("#cb_5E").prop("checked", false);
        $("#cb_6E").prop("checked", false);
        $("#cb_7E").prop("checked", false);
        $("#cb_9E").prop("checked", false);
        $("#cb_10E").prop("checked", false);
        $("#cb_11E").prop("checked", false);
        $("#cb_12E").prop("checked", false);
        $("#cb_13E").prop("checked", false);
    });
    $("#cb_9E").click(function () {
        $("#cb_1E").prop("checked", false);
        $("#cb_2E").prop("checked", false);
        $("#cb_3E").prop("checked", false);
        $("#cb_4E").prop("checked", false);
        $("#cb_5E").prop("checked", false);
        $("#cb_6E").prop("checked", false);
        $("#cb_7E").prop("checked", false);
        $("#cb_8E").prop("checked", false);
        $("#cb_10E").prop("checked", false);
        $("#cb_11E").prop("checked", false);
        $("#cb_12E").prop("checked", false);
        $("#cb_13E").prop("checked", false);
    });
    $("#cb_10E").click(function () {
        $("#cb_1E").prop("checked", false);
        $("#cb_2E").prop("checked", false);
        $("#cb_3E").prop("checked", false);
        $("#cb_4E").prop("checked", false);
        $("#cb_5E").prop("checked", false);
        $("#cb_6E").prop("checked", false);
        $("#cb_7E").prop("checked", false);
        $("#cb_8E").prop("checked", false);
        $("#cb_9E").prop("checked", false);
        $("#cb_11E").prop("checked", false);
        $("#cb_12E").prop("checked", false);
        $("#cb_13E").prop("checked", false);
    });
    $("#cb_11E").click(function () {
        $("#cb_1E").prop("checked", false);
        $("#cb_2E").prop("checked", false);
        $("#cb_3E").prop("checked", false);
        $("#cb_4E").prop("checked", false);
        $("#cb_5E").prop("checked", false);
        $("#cb_6E").prop("checked", false);
        $("#cb_7E").prop("checked", false);
        $("#cb_8E").prop("checked", false);
        $("#cb_9E").prop("checked", false);
        $("#cb_10E").prop("checked", false);
        $("#cb_12E").prop("checked", false);
        $("#cb_13E").prop("checked", false);
    });

    $("#cb_12E").click(function () {
        $("#cb_1E").prop("checked", false);
        $("#cb_2E").prop("checked", false);
        $("#cb_3E").prop("checked", false);
        $("#cb_4E").prop("checked", false);
        $("#cb_5E").prop("checked", false);
        $("#cb_6E").prop("checked", false);
        $("#cb_7E").prop("checked", false);
        $("#cb_8E").prop("checked", false);
        $("#cb_9E").prop("checked", false);
        $("#cb_10E").prop("checked", false);
        $("#cb_11E").prop("checked", false);
        $("#cb_13E").prop("checked", false);
    });
    $("#cb_13E").click(function () {
        $("#cb_1E").prop("checked", false);
        $("#cb_2E").prop("checked", false);
        $("#cb_3E").prop("checked", false);
        $("#cb_4E").prop("checked", false);
        $("#cb_5E").prop("checked", false);
        $("#cb_6E").prop("checked", false);
        $("#cb_7E").prop("checked", false);
        $("#cb_8E").prop("checked", false);
        $("#cb_9E").prop("checked", false);
        $("#cb_10E").prop("checked", false);
        $("#cb_11E").prop("checked", false);
        $("#cb_12E").prop("checked", false);
    });
    



});


function valida_horaEdit(valor) {
    //que no existan elementos sin escribir
    if (valor.indexOf(":") !== -1) {
        var hora = valor.split(":")[0];

        if (parseInt(hora) > 24) {
            $("#tbHoraSiniestroEdit").val("");
            ($('#divLuzArtificialEdit').css("display", "none"));
        }
        if ($("#tbHoraSiniestroEdit").val().trim() === "__:__" || $("#tbHoraSiniestroEdit").val().trim() === "") {
            ($('#divLuzArtificialEdit').css("display", "none"));
        }
        if (parseInt(hora) === 18 || parseInt(hora) === 19 || parseInt(hora) === 20 || parseInt(hora) === 21 || parseInt(hora) === 22 || parseInt(hora) === 23 || parseInt(hora) === 24 || hora.toString() === '01' || (hora.toString()) === '02' || (hora.toString()) === '03' || (hora.toString()) === '04' || (hora.toString()) === '05' || (hora.toString()) === '06') {
            ($('#divLuzArtificialEdit').css("display", "block"));

        }
        else {
            ($('#divLuzArtificialEdit').css("display", "none"));
        }
        //end if
    }//end if
};  //end function 


    function SeleccionarItemSelect(select, valor) {

        select.find('option').each(function (i, opt) {
            if (opt.value === valor)
                $(opt).attr('selected', 'selected');
        });
    }






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

    }
    function aMayusculas(obj, id) {
        obj = obj.toUpperCase();
        document.getElementById(id).value = obj;
    }

    function NumCheckEdad(e, field) {
        key = e.keyCode ? e.keyCode : e.which
        // backspace
        if (key == 8) return true
        if (key == 9) return true
        if (key == 45) return true
        if (key == 16) return true
        if (key > 47 && key < 58) return true
        return false

    }


    function VerMapa2() {
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
            map = new Map("map1", {
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

                domAttr.set(dom.byId("tbLatitudSiniestroEdit"), "value", mp.y.toFixed(8)); // set
                domAttr.set(dom.byId("tbLonguitudSiniestroEdit"), "value", mp.x.toFixed(8)); // set*/

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

    function inicializaVhlEdit() {
        var selectsv = $('#ddl_subtipo_vhl');
        selectsv.find('option').remove().end().append('<option value="-1">SELECCIONAR</option>').val('-1');
        var codsinVHL1 = ($('#tbSiniestroModEdit').val());
        ($('#tb_codsinVHL').val(codsinVHL1.toString()));
        $('#tb_codsinVHL').attr("disabled", "disabled");
        ($('#tb_placvehinv').val(''));
        ($('#tb_chavehinv').val(''));
        ($('#tb_marvehinv').val(''));
        ($('#tb_modvehinv').val(''));
        ($('#tb_anivehinv').val(''));
        ($('#tb_cilvehinv').val(''));
        ($('#tb_matpelvehinv').val('-1'));
        $('#ddl_subtipo_vhl').val('-1');
        ($('#ddl_vhl_materialPeligroso').val('-1'));
        ($('#tb_matpelvehinv').val('-1'));
        ($('#ddl_tipo_servicio_vhl').val('-1'));
        ($('#tb_codser').val('-1'));
        ($('#ddl_tipo_vhl').val('-1'));
        ($('#tb_codtipve').val('-1'));
        ($('#panelMensajeVHL').css("display", "none"));
        $("#cb_segprivehinv").prop("checked", false);
        $("#cb_danmatvehinv").prop("checked", true);
        $('#cb_matvigvehinv').prop("checked", false);
        $("#ddl_vhl_materialPeligroso").val('0');
        $("#divVehiculosInvolucradosSiniestro").css("display", "none");
        //$("#divBuscarPlaca").css("display", "none");
    };

    function inicializaVictimasEdit()
    {
        $('#tb_codsinVictimas').attr("disabled", "disabled");
        // ($('#btnBuscarVict').css("display", "none"));
        $('#tb_NombresVictimaEdit').val('');
        ($('#ddl_tipoIdentificacionEdit').val('-1'));
        ($('#tb_IdentificacionVictimaEdit').val(''));
        ($('#tb_edadVictimaEdit').val('0'));
        ($('#ddl_generoVictimaEdit').val('-1'));
        ($('#ddl_condicionVictimas24Edit').val('-1'));
        
        ($('#ddl_tipoParticipanteEdit').val('-1'));
        
        ($('#panelMensajeVictimasEdit').css("display", "none"));
        $("#ddl_uso_cascoEdit").val(-1);
        $("#ddl_uso_cinturonEdit").val(-1);
        $('#ddl_consumo_alcoholEdit').val(-1);
        $('#ddl_accionesPeatonEdit').css("display", "none");
        $('#divAccionesPeaton').css("display", "none");
        ($('#ddl_accionesPeatonEdit').val('-1'));
        $("#cb_1E").prop("checked", false);
        $("#cb_2E").prop("checked", false);
        $("#cb_3E").prop("checked", false);
        $("#cb_4E").prop("checked", false);
        $("#cb_5E").prop("checked", false);
        $("#cb_6E").prop("checked", false);
        $("#cb_7E").prop("checked", false);
        $("#cb_8E").prop("checked", false);
        $("#cb_9E").prop("checked", false);
        $("#cb_10E").prop("checked", false);
        $("#cb_11E").prop("checked", false);
        $("#cb_12E").prop("checked", false);
        $("#cb_13E").prop("checked", false);
    };




    function tarerInformacionSiniestroProcerso(codsiniestro) {
        $("#dt_siniestros_procesos").find("tr:gt(0)").remove();
        $.ajax({
            type: "get",
            url: '../ObtenertarerInformacionSiniestroProcersos',
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
    };


    function cargarDireccionesMapaConfirmarEdit() {

        //alert('xy');
        //uploadCustomerForm();
        if ($('#tbLatitudSiniestroEdit').val() !== '' && $('#tbLonguitudSiniestroSin').val() !== '') {
            var x = $('#tbLatitudSiniestroEdit').val();
            var y = $('#tbLonguitudSiniestroSin').val();

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
                                traerDatosDireccionGeoEdit();
                            };
                        }



                    });
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("No se puede conectar con el servidor de georeferencias, Intente nuevamente..!!");
                }

            });

            //promise.then(function () {
            //    //
            //    ParroquiasPorCantones();
            //    //
            //});

        }
    };


    function traerDatosDireccionGeoEdit() {
        var codcir = $('#lbcod_circ_1').html();
        var codcant = $('#lbdpa_canton').html();
        var codpar = $('#lbdpa_parroq').html();
        var codprov = $('#lbdpa_provin').html();
        var codistri = $('#lbdpa_coddist').html();
        if (codcir !== '') {

            $('#ddl_provinciaEdit').val(codprov);
            if ($('#ddl_provinciaEdit').val() !== '-1') {
                CantonesPorProvinciaEdit();
                console.log('imprime ' + '' + codpar.toString().trim());
                $('#ddl_parroquiaEdit').val(codpar.toString().trim());
                $('#ddl_circuitoEdit').val(codcir.toString().trim());
                $('#ddl_distritoSiniestroEdit').val(codistri.toString().trim());


                // ParroquiasPorCantones();
            }
            //if ($('#ddl_ciudad').val !== '-1') {
            //    CargaCircuitos();
            //

            // $('#ddl_ciudad').val('1701');
        }
    };



    function CantonesPorProvinciaEdit() {
        var codprov = $('#ddl_provinciaEdit').val();
        var params = new Object();
        params.codprov = $('#ddl_provinciaEdit').val();
        params = JSON.stringify(params);
        var select = $('#ddl_cantonEdit');
        select.find('option').remove().end().append('<option value="-1">SELECCIONAR</option>').val('-1');
        $.ajax({
            type: "get",
            url: '../CargarCantonesPorProvincia',
            data: { codprov: codprov },
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

    function TablaVehiculos(codsin) {
        $("#tbodyid").empty();
        $.get("../cargaVhlEdit", { codsin: codsin }, function (data) {
            console.log(data);
            var j = data.length;
            console.log(j);

            var tabla = "";


            var danmatvehinv = "";
            var matvigvehinv = "";
            var segprivehinv = "";
            for (var i = 0; i < j; i++) {

                if (data[i].danmatvehinv === true) {
                    danmatvehinv = '<td> SI </td>';
                } else {
                    danmatvehinv = '<td> NO </td>';
                }

                if (data[i].matvigvehinv === true) {

                    matvigvehinv = '<td> SI </td>';
                } else {
                    matvigvehinv = '<td> NO </td>';
                }
                if (data[i].segprivehinv === true) {

                    segprivehinv = '<td> SI </td>';
                } else {
                    segprivehinv = '<td> NO </td>';
                }
                tabla += '<tr class="small">' +
                        '<td>' + data[i].placvehinv + '</td>' +
                        danmatvehinv + matvigvehinv +
                        '<td>' + data[i].chavehinv + '</td>' +
                        '<td>' + data[i].marvehinv + '</td>' +
                        '<td>' + data[i].modvehinv + '</td>' +
                        '<td>' + data[i].anivehinv + '</td>' +
                        '<td>' + data[i].cilvehinv + '</td>' +
                        segprivehinv +
                        '<td>' + data[i].matpelvehinv + '</td>' +
                        '<td>' + data[i].desser + '</td>' +
                        '<td>' + data[i].destipveh + '</td>' +
                        '<td>' + data[i].dessubveh + '</td>' +
                        ' <td style="display:none">' + data[i].codvehinv + '</td>' +
                        '<td>' +
                            '<a class="btnModalVehiculoEdit btn" data-id="' + data[i].codvehinv + '" data-target="#myModalVehiculoEdit" data-toggle="modal"><span class="glyphicon glyphicon-edit" onclick=" PruebaVehiculo(' + data[i].codvehinv + ')"></a>' +
                        '</td>' +
                        '<td>' +
                            '<a class="btnModalVehiculoDelete btn" data-id="' + data[i].codvehinv + '" data-target="#myModalVehiculoEdit" data-toggle="modal"><span class="glyphicon glyphicon-trash" onclick=" EliminaVehiculo(' + data[i].codvehinv + ')"></a>' +
                        '</td>' +
                        '</tr>';


                //console.log(data[i].placvehinv);



            }

           // console.log(tabla);
            $("#dt_vehiculosEdit > tbody").append(tabla);


        }).fail(function () {



            alert("Hubo problemas con su conexión a internet");

        });

   //     TablaVictimas(codsin);


    };





    function PruebaVehiculo(Id) {
      //  alert(Id);
        console.log(pathname);
        var codsubtipoVhl = "-1";
        //$("#panelMensajeVHL").css("display", "none");
        $("#ddl_vhl_materialPeligrosoEdit").val('-1');
        if (Id !== '') {

            $.ajax({
                type: "get",
                url: '../JsonDatosVhlPorCodigo',
                data: { id: Id },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (jsonResult) {
                    console.log(jsonResult);
                    $.each(jsonResult, function (key, value) {
                        if (value.chavehinv.toString() !== '') {
                            $("#tb_codVHLEdit").val(value.codvehinv);
                            $('#tb_codVHLEdit').attr("disabled", "disabled");
                            $("#ddl_tipo_vhlEdit").val(value.codtipve);
                            $("#tb_placvehinvEdit").val(value.placvehinv);
                            $("#tb_chavehinvEdit").val(value.chavehinv);
                            $("#tb_marvehinvEdit").val(value.marvehinv);
                            $("#tb_modvehinvEdit").val(value.modvehinv);
                            $("#tb_anivehinvEdit").val(value.anivehinv);
                            $("#tb_cilvehinvEdit").val(value.cilvehinv);
                            ///$("#ddl_vhl_materialPeligrosoEdit").val(value.cilvehinv);
                            $("#ddl_tipo_servicio_vhlEdit").val(value.codser);
                            if (value.danmatvehinv === true)
                                $('#ddl_danio_materialEdit').val('1');
                            else
                                $('#ddl_danio_materialEdit').val('2');
                            if (value.matvigvehinv === true)
                                $('#ddl_matricula_vigenteEdit').val('1');
                            else
                                $('#ddl_matricula_vigenteEdit').val('2');
                            if (value.segprivehinv === true)
                                $('#ddl_seguro_privado_vhlEdit').val('1');
                            else
                                $('#ddl_seguro_privado_vhlEdit').val('2');

                            $("#ddl_vhl_materialPeligrosoEdit").find('option').each(function (i, opt) {
                                if (opt.text.trim() === value.matpelvehinv.trim()) {
                                    $("#ddl_vhl_materialPeligrosoEdit").val(opt.value);
                                }
                            });
                            cargarSubTipoVhl(value.codsubveh);
                           // console.log(value.codsubveh.toString());
                            $("#ddl_subtipo_vhlEdit option_selected").text(value.dessubveh);
                           
                            $('#respuesta-ej2').toggle();
                            $('#respuesta-ej1').toggle();


                        }
                        else {
                            mensaje = "No existe resultados en la busqueda.";
                            alert(mensaje.toString());
                        }

                    });

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus + ": " + XMLHttpRequest.responseText);
                }

            });




        }


    };


    function EliminaVehiculo(Id) {
        ($('#divMensajeVHLEdit').attr("class", "alert alert-danger smaller"));
        ($('#panelMensajeVHLEdit').css('display', 'none'));
        console.log(Id);
        var codsubtipoVhl = "-1";
        var codSin = $('#tbSiniestroModEdit').val();
        $("#ddl_vhl_materialPeligrosoEdit").val('-1');
        if (Id !== '') {
            if (confirm('Va a eliminar el vehículo involucrado. Desea continuar !') == true) {
                $.ajax({
                    type: "GET",
                    url: "../eliminaVehiculos",
                    data: { codVehiculo: Id, codsin: codSin },
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (jsonResult) {
                        //console.log("ERliminar" + " " + jsonResult)
                        if (jsonResult === "-1" )
                        {
                            alert("Para poder eliminar el vehículo involucrado debe primero eliminar las VÍCTIMAS INVOLUCRADAS asociadas al vehículo.");
                        }
                        else if (jsonResult.length > -1 && jsonResult !== "-1") {
                            var mensaje = 'Vehiculo eliminado correctamente correctamente.';
                            ($('#divMensajeVHLEdit').attr("class", "alert alert-success smaller"));
                            ($('#mensajeLabelVHLEdit').html(mensaje.toString()));

                            ($('#panelMensajeVHLEdit').css("display", "block"));
                            TablaVehiculos(codSin);
                        }
                        //else {
                        //    alert("Error al eliminar el registro del vehículo involucrado.");
                        //}
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("Error al eliminar el registro del vehículo involucrado.");
                    }
                });
            }
        }

      

    };


    function cargarSubTipoVhl(codsubtipoVhl) {
       
        var codtipoVhl = $("#ddl_tipo_vhlEdit").val();
        var select = $('#ddl_subtipo_vhlEdit');
        select.find('option').remove().end().append('<option value="-1">SELECCIONAR</option>').val('-1');
            if (codtipoVhl !== '' && codtipoVhl !== '0')
            {
                $.ajax({
                    type: "GET",
                    url: "../ObtenerlistaSubTipoVehiculo",
                    data: { codTipoVhl: codtipoVhl },
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (jsonResult) {
                        if (jsonResult.length > 0) {
                            $.each(jsonResult, function (key, value) {
                                select.append('<option value="' + value.value + '">' + value.text + '</option>').val(value.value);
                            });
                            select.val(codsubtipoVhl);
                            console.log("cargarSubTipoVhl funcion" + ' ' + codsubtipoVhl)
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("Error al cargar los sub tipo de vehiculos.");
                    }
                });
            }
    };

    function TablaVictimas(codsin) {
        $("#tbodyidVictimas").empty();
        $.get("../cargaVictimasEdit", { codsin: codsin }, function (data) {
            console.log(data);
            var j = data.length;
            console.log(j);

            var tabla = "";


            var usocasco = "";
            var usocinturon = "";
            var consumoalcohol = "";
            for (var i = 0; i < j; i++) {

                if (data[i].usO_CASO === "SI") {
                    usocasco = '<td> SI </td>';
                } else {
                    usocasco = '<td> NO </td>';
                }

                if (data[i].usO_CINTU === "SI") {

                    usocinturon = '<td> SI </td>';
                } else {
                    usocinturon = '<td> NO </td>';
                }
                if (data[i].conS_ALCOHOL === "SI") {

                    consumoalcohol = '<td> SI </td>';
                } else {
                    consumoalcohol = '<td> NO </td>';
                }
                tabla += '<tr class="small">' +
                        '<td>' + data[i].tipidenvicinv + '</td>' +
                        '<td>' + data[i].numidenvicinv + '</td>' +
                        '<td>' + data[i].edavicinv + '</td>' +
                        '<td>' + data[i].sexo + '</td>' +
                        '<td>' + data[i].convicinv24 + '</td>' +
                        '<td>' + data[i].tipparvicinv + '</td>' +
                        usocasco + usocinturon +
                        '<td>' + data[i].posvicinv + '</td>' +
                        consumoalcohol+
                        '<td>' + data[i].placavhl + '</td>' +
                        ' <td style="display:none">' + data[i].codvicinv + '</td>' +
                        '<td>' +
                            '<a class="btnModalVictimaEdit btn" data-id="' + data[i].codvicinv + '" data-target="#myModalVictimaEdit" data-toggle="modal"><span class="glyphicon glyphicon-edit" onclick=" PruebaVictima(' + data[i].codvicinv + ')"></a>' +
                        '</td>' +
                        '<td>' +
                            '<a class="btnModalVictimaDelete btn" data-id="' + data[i].codvicinv + '" data-target="#myModalVictimaEdit" data-toggle="modal"><span class="glyphicon glyphicon-trash" onclick=" EliminarVictima(' + data[i].codvicinv + ')"></a>' +
                        '</td>' +
                        '</tr>';


                //console.log(data[i].placvehinv);



            }

            // console.log(tabla);
            $("#dt_victimasSiniestroEdit > tbody").append(tabla);


        }).fail(function () {



           alert("Hubo problemas con su conexión a internet");

        });




    };

    function PruebaVictima(Id) {
     
      //  inicializaVictimasEdit();
        ($('#panelMensajeVictimasEdit').css("display", "none"));
        $("#ddl_tipoIdentificacionEdit").val('-1');
       // $("#ddl_accionesPeatonEdit").val('-1');
        $('#divAccionesPeatonEdit').css("display", "none");
        $("#cb_1E").prop("checked", false);
        $("#cb_2E").prop("checked", false);
        $("#cb_3E").prop("checked", false);
        $("#cb_4E").prop("checked", false);
        $("#cb_5E").prop("checked", false);
        $("#cb_6E").prop("checked", false);
        $("#cb_7E").prop("checked", false);
        $("#cb_8E").prop("checked", false);
        $("#cb_9E").prop("checked", false);
        $("#cb_10E").prop("checked", false);
        $("#cb_11E").prop("checked", false);
        $("#cb_12E").prop("checked", false);
        $("#cb_13E").prop("checked", false);

        inicializaVictimasEdit();
        console.log(Id);
        if (Id !== '') {
            var idVict = Id;
            $.ajax({
                type: "get",
                url: '../JsonDatosVictimasPorCodigo',
                data: { id: idVict },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (jsonResult) {
                    console.log(jsonResult);
                    //console.log('codvicinv' + ' ' + value.codvicinv.toString() );
                    $.each(jsonResult, function (key, value) {
                        if (value.codvicinv.toString() !== '') {

                            $("#tb_codVictimasEdit").val(value.codvicinv);
                            $('#tb_codVictimasEdit').attr("disabled", "disabled");
                            $("#ddl_tipoIdentificacionEdit").find('option').each(function (i, opt) {
                                if (opt.text.trim() === value.tipidenvicinv.trim()) {
                                    $("#ddl_tipoIdentificacionEdit").val(opt.value);
                                }
                            });
                            $("#tb_IdentificacionVictimaEdit").val(value.numidenvicinv);
                            $("#tb_edadVictimaEdit").val(value.edavicinv);
                            $("#ddl_generoVictimaEdit").val(value.sexo);
                            $("#ddl_condicionVictimas24Edit").find('option').each(function (i, opt) {
                                if (opt.text.trim() === value.convicinv24.trim()) {
                                    $("#ddl_condicionVictimas24Edit").val(opt.value);
                                }
                            });
                            $("#ddl_condicionVictimas30Edit").find('option').each(function (i, opt) {
                                if (opt.text.trim() === value.convicinv30.trim()) {
                                    $("#ddl_condicionVictimas30Edit").val(opt.value);
                                }
                            });
                            $("#ddl_tipoParticipanteEdit").find('option').each(function (i, opt) {
                                if (opt.text.trim() === value.tipparvicinv.trim()) {
                                    $("#ddl_tipoParticipanteEdit").val(opt.value);
                                }
                            });




                            console.log(value.tipparvicinv);

                            var retorno = $('#tbTipoVhlVictimaEdit').val();
                            console.log(retorno.toString());

                            if ($('#ddl_tipoParticipanteEdit option:selected').text() === 'PEATÓN') {
                                // $('#ddl_accionesPeatonEdit').css("display", "block");
                                // $('#divAccionesPeaton').css("display", "block");

                                //  ($('#ddl_accionesPeatonEdit').val('-1'));
                                $("#ddl_uso_cascoEdit").val('2');
                                $("#ddl_uso_cinturonEdit").val('2');
                                $("#ddl_consumo_alcoholEdit").val('2');
                                $("#cb_1E").prop("checked", false);
                                $("#cb_2E").prop("checked", false);
                                $("#cb_3E").prop("checked", false);
                                $("#cb_4E").prop("checked", false);
                                $("#cb_5E").prop("checked", false);
                                $("#cb_6E").prop("checked", false);
                                $("#cb_7E").prop("checked", false);
                                $("#cb_8E").prop("checked", false);
                                $("#cb_9E").prop("checked", false);
                                $("#cb_10E").prop("checked", false);
                                $("#cb_11E").prop("checked", true);
                                $("#cb_12E").prop("checked", false);
                                $("#cb_13E").prop("checked", false);


                                $("#cb_1E").prop("checked", false);
                                $("#cb_2E").prop("checked", false);
                                $("#cb_3E").prop("checked", false);
                                $("#cb_4E").prop("checked", false);
                                $("#cb_5E").prop("checked", false);
                                $("#cb_6E").prop("checked", false);
                                $("#cb_7E").prop("checked", false);
                                $("#cb_8E").prop("checked", false);
                                $("#cb_9E").prop("checked", false);
                                $("#cb_10E").prop("checked", false);
                                $("#cb_12E").prop("checked", false);
                                $("#cb_13E").prop("checked", false);
                                $("#cb_11E").prop("checked", true);
                                $("#cb_1E").attr("disabled", "disabled");
                                $("#cb_2E").attr("disabled", "disabled");
                                $("#cb_3E").attr("disabled", "disabled");
                                $("#cb_4E").attr("disabled", "disabled");
                                $("#cb_5E").attr("disabled", "disabled");
                                $("#cb_6E").attr("disabled", "disabled");
                                $("#cb_7E").attr("disabled", "disabled");
                                $("#cb_8E").attr("disabled", "disabled");
                                $("#cb_9E").attr("disabled", "disabled");
                                $("#cb_10E").attr("disabled", "disabled");
                                $("#cb_12E").attr("disabled", "disabled");
                                $("#cb_13E").attr("disabled", "disabled");

                            }
                            else if ($('#ddl_tipoParticipanteEdit option:selected').text() === 'CONDUCTOR' && (retorno.toString() !== "8" && retorno.toString() !== "16")) {



                                $("#cb_1E").prop("checked", true);
                                $("#cb_2E").prop("checked", false);
                                $("#cb_3E").prop("checked", false);
                                $("#cb_4E").prop("checked", false);
                                $("#cb_5E").prop("checked", false);
                                $("#cb_6E").prop("checked", false);
                                $("#cb_7E").prop("checked", false);
                                $("#cb_8E").prop("checked", false);
                                $("#cb_9E").prop("checked", false);
                                $("#cb_10E").prop("checked", false);
                                $("#cb_11E").prop("checked", false);
                                $("#cb_12E").prop("checked", false);
                                $("#cb_13E").prop("checked", false);

                                $("#cb_1E").removeAttr("disabled");
                                $("#cb_2E").attr("disabled", "disabled");
                                $("#cb_3E").attr("disabled", "disabled");
                                $("#cb_4E").attr("disabled", "disabled");
                                $("#cb_5E").attr("disabled", "disabled");
                                $("#cb_6E").attr("disabled", "disabled");
                                $("#cb_7E").attr("disabled", "disabled");
                                $("#cb_8E").attr("disabled", "disabled");
                                $("#cb_9E").attr("disabled", "disabled");
                                $("#cb_10E").attr("disabled", "disabled");
                                $("#cb_11E").attr("disabled", "disabled");
                                $("#cb_12E").attr("disabled", "disabled");
                                $("#cb_13E").attr("disabled", "disabled");


                            }
                            else if ($('#ddl_tipoParticipanteEdit option:selected').text() === 'CONDUCTOR' && (retorno.toString() === "8" || retorno.toString() === "16")) {

                                $("#cb_1E").prop("checked", true);
                                $("#cb_2E").prop("checked", false);
                                $("#cb_3E").prop("checked", false);
                                $("#cb_4E").prop("checked", false);
                                $("#cb_5E").prop("checked", false);
                                $("#cb_6E").prop("checked", false);
                                $("#cb_7E").prop("checked", false);
                                $("#cb_8E").prop("checked", false);
                                $("#cb_9E").prop("checked", false);
                                $("#cb_10E").prop("checked", false);
                                $("#cb_11E").prop("checked", false);
                                $("#cb_12E").prop("checked", false);
                                $("#cb_13E").prop("checked", false);


                                $("#cb_4E").attr("disabled", "disabled");
                                $("#cb_7E").attr("disabled", "disabled");
                                $("#cb_8E").attr("disabled", "disabled");
                                $("#cb_9E").attr("disabled", "disabled");
                                $("#cb_10E").attr("disabled", "disabled");
                                $("#cb_11E").attr("disabled", "disabled");
                                $("#cb_12E").attr("disabled", "disabled");
                                $("#cb_13E").attr("disabled", "disabled");
                            }

                            else if ($('#ddl_tipoParticipanteEdit option:selected').text() === 'PASAJERO' && (retorno.toString() !== "8" && retorno.toString() !== "16")) {


                                $("#cb_1E").prop("checked", false);
                                $("#cb_2E").prop("checked", false);
                                $("#cb_3E").prop("checked", false);
                                $("#cb_4E").prop("checked", false);
                                $("#cb_5E").prop("checked", false);
                                $("#cb_6E").prop("checked", false);
                                $("#cb_7E").prop("checked", false);
                                $("#cb_8E").prop("checked", false);
                                $("#cb_9E").prop("checked", false);
                                $("#cb_10E").prop("checked", false);
                                $("#cb_11E").prop("checked", false);
                                $("#cb_12E").prop("checked", false);
                                $("#cb_13E").prop("checked", false);

                                $("#cb_1E").attr("disabled", "disabled");
                                $("#cb_2E").removeAttr("disabled");
                                $("#cb_3E").removeAttr("disabled");
                                $("#cb_4E").removeAttr("disabled");
                                $("#cb_5E").removeAttr("disabled");
                                $("#cb_6E").removeAttr("disabled");
                                $("#cb_7E").removeAttr("disabled");
                                $("#cb_8E").removeAttr("disabled");
                                $("#cb_9E").removeAttr("disabled");
                                $("#cb_10E").removeAttr("disabled");
                                $("#cb_11E").removeAttr("disabled");
                                $("#cb_12E").removeAttr("disabled");
                                $("#cb_13E").removeAttr("disabled");
                            }
                            else if ($('#ddl_tipoParticipanteEdit option:selected').text() === 'PASAJERO' && (retorno.toString() === "8" || retorno.toString() === "16")) {

                                $("#cb_1E").prop("checked", false);
                                $("#cb_2E").prop("checked", false);
                                $("#cb_3E").prop("checked", false);
                                $("#cb_4E").prop("checked", false);
                                $("#cb_5E").prop("checked", false);
                                $("#cb_6E").prop("checked", false);
                                $("#cb_7E").prop("checked", false);
                                $("#cb_8E").prop("checked", false);
                                $("#cb_9E").prop("checked", false);
                                $("#cb_10E").prop("checked", false);
                                $("#cb_11E").prop("checked", false);
                                $("#cb_12E").prop("checked", false);
                                $("#cb_13E").prop("checked", false);

                                $("#cb_1E").attr("disabled", "disabled");
                                $("#cb_2E").attr("disabled", "disabled");
                                $("#cb_3E").attr("disabled", "disabled");
                                $("#cb_4E").removeAttr("disabled");
                                $("#cb_5E").attr("disabled", "disabled");
                                $("#cb_6E").attr("disabled", "disabled");
                                $("#cb_7E").removeAttr("disabled");
                                $("#cb_8E").attr("disabled", "disabled");
                                $("#cb_9E").attr("disabled", "disabled");
                                $("#cb_10E").attr("disabled", "disabled");
                                $("#cb_11E").attr("disabled", "disabled");
                                $("#cb_12E").removeAttr("disabled");
                                $("#cb_13E").removeAttr("disabled");
                            }

                            if (value.posvicinv.trim() === 'FRONTAL IZQUIERDO')
                                $('#cb_1E').prop("checked", true);
                            if (value.posvicinv.trim() === 'FRONTAL CENTRAL')
                                $('#cb_2E').prop("checked", true);
                            if (value.posvicinv.trim() === 'FRONTAL DERECHO')
                                $('#cb_3E').prop("checked", true);
                            if (value.posvicinv.trim() === 'CENTRAL IZQUIERDO')
                                $('#cb_4E').prop("checked", true);
                            if (value.posvicinv.trim() === 'CENTRAL')
                                $('#cb_5E').prop("checked", true);
                            if (value.posvicinv.trim() === 'CENTRAL DERECHO')
                                $('#cb_6E').prop("checked", true);
                            if (value.posvicinv.trim() === 'TRASERO IZQUIERDO')
                                $('#cb_7E').prop("checked", true);
                            if (value.posvicinv.trim() === 'TRASERO CENTRAL')
                                $('#cb_8E').prop("checked", true);
                            if (value.posvicinv.trim() === 'TRASERO DERECHO')
                                $('#cb_9E').prop("checked", true);
                            if (value.posvicinv.trim() === 'BALDE')
                                $('#cb_10E').prop("checked", true);
                            if (value.posvicinv.trim() === 'DE PIE')
                                $('#cb_11E').prop("checked", true);
                            if (value.posvicinv.trim() === 'OTROS')
                                $('#cb_12E').prop("checked", true);
                            if (value.posvicinv.trim() === 'NIÑO EN BRAZOS')
                                $('#cb_13E').prop("checked", true);

                            if (value.tipparvicinv.trim() === 'PEATÓN' || value.tipparvicinv.trim() === 'CONDUCTOR') {
                                $('#divAccionesPeatonEdit').css("display", "block");
                                $('#ddl_accionesPeatonEdit').css("display", "block");

                            }
                            //$('#ddl_accionesPeatonEdit > option[value="4"]').attr('selected', 'selected');
                            //  $('#ddl_accionesPeatonEdit > option[value="2"]').attr('selected', 'selected');
                            console.log("value.desaccpea " + ' ' + value.codaccpea)
                            var _acciones_peaton = value.codaccpea.split(',');

                            $.each(_acciones_peaton, function (key1, value1) {
                                console.log('aciones' + '' + value1);
                                //$('#ddl_accionesPeatonEdit > option[value="1"]').attr('checked', true);
                                if (value1 == 'USO DEL CELULAR')
                                    $('#ddl_accionesPeatonEdit').val(1).attr('selected', 'selected');
                                if (value1 == "USO DE ELEMENTOS DISTRACTORES")
                                    $('#ddl_accionesPeatonEdit').val(2).attr('selected', 'selected');
                                if (value1 == 'CRUCE DE VÍA A LUGARES NO AUTORIZADO')
                                    $('#ddl_accionesPeatonEdit').val(3).attr('selected', 'selected');
                                if (value1 == "PRESUNCIÓN DE INGESTA DE ALCOHOL")
                                    $('#ddl_accionesPeatonEdit').val(4).attr('selected', 'selected');
                                if (value1 == "CRUCE DE VÍA SIN PREFERENCIA")
                                    $('#ddl_accionesPeatonEdit').val(5).attr('selected', 'selected');
                                   // $('#ddl_accionesPeatonEdit option:selected').text("CRUCE DE VÍA SIN PREFERENCIA").attr('selected', 'selected');
                                if (value1 == "PRESUNCIÓN DE INGESTA  DE SUSTANCIAS ESTUPERFACIENTES O PSICOTRÓPICAS Y/O MEDICAMENTOS")
                                    $('#ddl_accionesPeatonEdit').val(6).attr('selected', 'selected');

                            });


                            if (value.tipidenvicinv.trim() === 'CÉDULA')
                                $('#btnBuscarVictEdit').css("display", "block");
                            else
                                $('#btnBuscarVictEdit').css("display", "none");

                            $("#ddl_vhlVictimasEdit").val(value.codveh);
                            ///$("#ddl_vhl_materialPeligrosoEdit").val(value.cilvehinv);
                            $("#ddl_tipo_servicio_vhlEdit").val(value.codser);
                            if (value.casvicinv === true)
                                $('#ddl_uso_cascoEdit').val('1');
                            else
                                $('#ddl_uso_cascoEdit').val('2');
                            if (value.cinvicinv === true)
                                $('#ddl_uso_cinturonEdit').val('1');
                            else
                                $('#ddl_uso_cinturonEdit').val('2');
                            if (value.conalcvicinv === true)
                                $('#ddl_consumo_alcoholEdit').val('1');
                            else
                                $('#ddl_consumo_alcoholEdit').val('2');


                            ($('#tbGudarVictimasEdit').css("display", "none"));
                            ($('#tbModificarVictimas').css("display", "block"));



                        }
                        else {
                            mensaje = "No existe resultados en la busqueda.";
                            alert(mensaje.toString());
                        }

                    });

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert('Error de conexión con la base de datos, vuelva a seleccionar el registro de la víctima');
                }

            });
        }
        $('#respuesta-ej4').toggle();
        $('#respuesta-ej3').toggle();
    };






    function EliminarVictima(Id) {

        //  inicializaVictimasEdit();
        ($('#panelMensajeVictimasEdit').css("display", "none"));
        $("#ddl_tipoIdentificacionEdit").val('-1');
        // $("#ddl_accionesPeatonEdit").val('-1');
        $('#divAccionesPeatonEdit').css("display", "none");
        $("#cb_1E").prop("checked", false);
        $("#cb_2E").prop("checked", false);
        $("#cb_3E").prop("checked", false);
        $("#cb_4E").prop("checked", false);
        $("#cb_5E").prop("checked", false);
        $("#cb_6E").prop("checked", false);
        $("#cb_7E").prop("checked", false);
        $("#cb_8E").prop("checked", false);
        $("#cb_9E").prop("checked", false);
        $("#cb_10E").prop("checked", false);
        $("#cb_11E").prop("checked", false);
        $("#cb_12E").prop("checked", false);
        $("#cb_13E").prop("checked", false);

        inicializaVictimasEdit();
   
       

        var codSin = $('#tbSiniestroModEdit').val();
       // var id = $(this).data('id');

        if (codSin !== '' && Id !== '') {
            if (confirm('Va a eliminar la víctima involucrada. Desea continuar !') == true) {
                $.ajax({
                    type: "GET",
                    url: "../eliminaVictimas",
                    data: { codVictima: Id, codsin: codSin },
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (jsonResult) {
                        if (jsonResult === "-1") {
                            alert("Error al eliminar el registro de la víctma involucrado. Intente nuevamente.");
                        }
                        if (jsonResult.length > -1 && jsonResult !== "-1") {
                            var mensaje = 'Víctima eliminada correctamente.';
                            ($('#divMensajeVictimasEdit').attr("class", "alert alert-success smaller"));
                            ($('#mensajeLabelVictimasEdit').html(mensaje.toString()));

                            ($('#panelMensajeVictimasEdit').css("display", "block"));
                            TablaVictimas(codSin);

                        }
                        else {
                            alert("Error al eliminar el registro de la víctma involucrado. Intente nuevamente.");
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("Error al eliminar el registro de la víctma involucrado. Intente nuevamente.");
                    }

                });


            }
        }
      
    };


    function TablaDanios(codsin) {
        $("#tbodyidDanios").empty();
        $.get("../cargaDaniosEdit", { codsin: codsin }, function (data) {
            console.log(data);
            var j = data.length;
            console.log(j);

            var tabla = "";


            var usocasco = "";
            var usocinturon = "";
            var consumoalcohol = "";
            for (var i = 0; i < j; i++) {

             
                tabla += '<tr class="small">' +
                        '<td>' + data[i].codsin + '</td>' +
                        '<td>' + data[i].destipdater + '</td>' +
                        '<td>' + data[i].obsdater + '</td>' +
                        ' <td style="display:none">' + data[i].coddater + '</td>' +
                        '<td class=\"ajustar\">' +
                            '<a class="btnModalDaniosTercerosEdit btn" data-id="' + data[i].coddater + '" data-target="#myModalVictimaEdit" data-toggle="modal"><span class="glyphicon glyphicon-edit" onclick=" PruebaDanio(' + data[i].coddater + ')"></a>' +
                        '</td>' +
                        '<td class=\"ajustar\">' +
                            '<a class="btnModalDaniosTercerosDelete btn" data-id="' + data[i].coddater + '" data-target="#myModalVictimaEdit" data-toggle="modal"><span class="glyphicon glyphicon-trash" onclick=" EliminaDanioTercero(' + data[i].coddater + ')"></a>' +
                        '</td>' +
                        '</tr>';
            }
            $("#dt_DaniosTercerosEdit > tbody").append(tabla);

        }).fail(function () {



            alert("Hubo problemas con su conexión a internet");

        });




    };


    function PruebaDanio(Id) {
        console.log(pathname);
        ($('#panelMensajeEdit').css("display", "none"));
        $("#ddl_tipoDanioTerceroEdit").val('-1');
        console.log('codigo DANIO');
        console.log(Id);
        if (Id) {
            var codAccion = Id;
            $.ajax({
                type: "get",
                url: '../JsonlistaVistaDaniosTercerosPorCodigo',
                data: { id: codAccion },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (jsonResult) {
                    console.log(jsonResult);
                    $.each(jsonResult, function (key, value) {
                        if (value.coddater.toString() !== '' && value.coddater.toString() !== '0') {
                            $("#tb_codDanioTercero").val(value.coddater);
                            $('#tb_codDanioTercero').attr("disabled", "disabled");
                            $("#ddl_tipoDanioTerceroEdit").val(value.codtipdater)
                            $("#tb_obsdaterEdit").val(value.obsdater);
                           
                            ($('#tbGudarDanioTerceroEdit').css("display", "none"));
                            ($('#tbModificaDanioTercero').css("display", "block"));
                            
                        }
                        else {
                            mensaje = "No existe resultados en la busqueda.";
                            alert(mensaje.toString());
                        }

                    });
                

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus + ": " + XMLHttpRequest.responseText);
                }

            });
            $('#respuesta-ej5').toggle();
            $('#respuesta-ej6').toggle();
            ($('#panelMensaje').css("display", "none"));
            //($('#tb_codsinDanioTercero').val('0'));
            ($('#tb_obsdaterEdit').val(''));
            ($('#ddl_tipoDanioTerceroEdit').val('-1'));
            var codsinDanioTercero = ($('#tbSiniestroModEdit').val());
           
        }
    };


    
    function EliminaDanioTercero(Id) {
        console.log(pathname);
        var mensaje = "";
        ($('#panelMensajeEdit').css("display", "none"));
        $("#ddl_tipoDanioTerceroEdit").val('-1');
        var codSin = $('#tbSiniestroModEdit').val();
        console.log(Id);
        if (Id) {
            if (confirm('Va a eliminar el registro de daño a tercero. Desea continuar!') === true)
            {
                var _codDanio = Id;
                $.ajax({
                    type: "get",
                    url: '../eliminaDaniosTerceros',
                    data: { codDanio: _codDanio, codsin: codSin },
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (jsonResult) {
                        console.log(jsonResult);
                        if (jsonResult === '1') {
                            mensaje = "Daño a tercero eliminado correctamente.";
                            ($('#mensajeLabel').html(mensaje.toString()));
                            ($('#divMensaje').attr("class", "alert alert-success smaller"));
                            ($('#panelMensaje').css("display", "block"));
                            TablaDanios(codSin);
                        }
                        else {
                            mensaje = "Error al eliminar el registro, Intente nuevamente.";
                            alert(mensaje.toString());
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert("Error al eliminar. Intente nuevamente.");
                    }

                });
            }
           
           

        }
    };

    function traerVehiculosInvolucradosSinEdit() {
        var codsinVictimas = ($('#tbSiniestroModEdit').val());

        var select = $('#ddl_vhlVictimasEdit');
        select.find('option').remove().end().append('<option value="-1">SELECCIONAR</option>').val('-1');
        $.ajax({
            type: "get",
            url: '../traerDatosVehiculosInvolucradosVictimas',
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

 
    function ObtenerDatosConsultaPorLicenciaEdit() {
        var tipoI = '';
        var cedula = $("#tb_IdentificacionVictimaEdit").val();
        $('#tb_NombresVictimaEdit').val('');
        if ($("#tb_IdentificacionVictimaEdit").val().length > 5) {
            if ($('#ddl_tipoIdentificacionEdit').val() === '1' || $('#ddl_tipoIdentificacionEdit').val() === '3') {
                tipoI = '1';
            }
            else if ($('#ddl_tipoIdentificacionEdit').val() === '2') {
                tipoI = '2';
            }
            $("#divLoaderSinEdit").css("display", "block");
            $.ajax({
                type: "get",
                url: '../ObtenerInformacionVictima',
                data: { cedula: cedula, tipoI: tipoI },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (jsonResult) {
                    console.log('retorno datos vicitmas' + ' ' + jsonResult);
                    if (jsonResult.length > 0) {
                        $.each(jsonResult, function (key, value) {
                            $('#tb_NombresVictimaEdit').val(value.nombre_completo);
                            $("#tb_edadVictimaEdit").val(value.edavicinv);
                            if (value.sexo.toString() === 'MASCULINO') {
                                $('#ddl_generoVictimaEdit').val('H');
                            }
                            else if (value.sexo.toString() === 'FEMENINO') {
                                $('#ddl_generoVictimaEdit').val('M');
                            }
                            else {
                                $('#ddl_generoVictimaEdit').val('N');
                            }

                            mensaje = "Búsqueda exitosa.";
                            alert(mensaje.toString() + ' ' + value.nombre_completo);
                            $("#divLoaderSinEdit").css("display", "none");
                        });
                    }
                    else {
                        alert("No existen resultados en  la búsqueda.");
                        alert(mensaje.toString());
                        $('#ddl_generoVictimaEdit').val('-1');
                        $("#tb_edadVictimaEdit").val('-1');
                        $("#divLoaderSinEdit").css("display", "none");
                    }

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("No existen resultados en  la búsqueda.");
                    $('#ddl_generoVictimaEdit').val('-1');
                    $("#tb_edadVictimaEdit").val('-1');
                    $("#divLoaderSinEdit").css("display", "none");
                }

            });

        }
    };

    function ObtenerDatosConsultaPorCedulaEdit() {
        var tipoI = '';
        var cont = 0;
        $('#tb_NombresVictimaEdit').val('');
        var cedula = $("#tb_IdentificacionVictimaEdit").val();
        console.log("ingresa WS1EDIT");
        if ($("#tb_IdentificacionVictimaEdit").val().length === 10) {
            if ($('#ddl_tipoIdentificacionEdit').val() === '1' || $('#ddl_tipoIdentificacionEdit').val() === '3') {
                tipoI = '1';
            }
            else if ($('#ddl_tipoIdentificacionEdit').val() === '2') {
                tipoI = '2';
            }
            $("#divLoaderSinEdit").css("display", "block");
            $.ajax({
                type: "get",
                url: '../ObtenerDatosWSRC',
                data: { numeroIdentificacion: cedula },
                contentType: "application/json;",
                dataType: "json",
                success: function (jsonResult) {
                    console.log("ingresa WS2 EDIT");
                    console.log( jsonResult);
                    if (jsonResult.length > 0) {
                        $.each(jsonResult, function (key, value) {
                            cont = cont + 1;
                            if (cont === 1)
                            {
                                $('#tb_NombresVictimaEdit').val(value.nombre);
                                $("#tb_edadVictimaEdit").val(value.edad);
                                if (value.sexo === 'HOMBRE') {
                                    $('#ddl_generoVictimaEdit').val('H');
                                }
                                else if (value.sexo === 'MUJER') {
                                    $('#ddl_generoVictimaEdit').val('M');
                                }
                                else {
                                    $('#ddl_generoVictimaEdit').val('-1');
                                }

                                mensaje = "Búsqueda exitosa.";
                                alert(mensaje.toString() + ' ' + value.nombre);
                                $("#divLoaderSinEdit").css("display", "none");
                            }
                        });
                    }
                    else {
                        alert("No existen resultados en  la búsqueda.");
                        $('#ddl_generoVictimaEdit').val('-1');
                        $("#tb_edadVictimaEdit").val('-1');
                        $("#divLoaderSinEdit").css("display", "none");

                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("No existen resultados en  la búsqueda.");
                    $("#divLoaderSinEdit").css("display", "none");
                    $('#ddl_generoVictimaEdit').val('-1');
                    $("#tb_edadVictimaEdit").val('-1');
                }

            });

        }
        else {
            alert("Ingrese un número de identificación correcto. Recuerde que debe contener 10 dígitos");
        }
    };