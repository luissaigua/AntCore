$(document).ready(function () {
    /**************************************************************
    *PARA FIND SINIESTROS
    ***************************************************************/
    var codsinAccionPeaton = $('#tbSiniestroMod').val();
    var codsin = codsinAccionPeaton;
    var params1 = new Object();
    params1.codsin = codsinAccionPeaton;
    $('#panelMensajeSiniestroFind').css("display", "none");
    $('#divVehiculosInvolucrados').css("display", "none");
    $('#panelDatosFind').attr("disabled", "disabled");
    
    
    //($('#divCamposFind').css("display", "none"));
    $('#divVerVictimas').css("display", "none");
    $('#divVerAccionesPeaton').css("display", "none");
    $('#divVerDaniosterceros').css("display", "none");
       
       
        
        if ($('#tbSiniestroMod').val() !== '' || $('#tbSiniestroMod').val() !== '0') {
            console.log('paso aqui' + ' ' + codsinAccionPeaton.toString());
            $.ajax({
                type: "get",
                url: '../ObtenerlistaVistaSiniestros',
                data: { codsin: codsinAccionPeaton },
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (jsonResult) {
                    console.log(jsonResult);
                    $.each(jsonResult, function (key, value) {
                        /* Vamos agregando a nuestra tabla las filas necesarias */
                        $('#tbSiniestroMod').attr("disabled", "disabled");
                        $("#tbfechaSiniestroMod").val(value.fecsin);
                        $('#tbfechaSiniestroMod').attr("disabled", "disabled");
                        $("#tbHoraSiniestroMod").val(value.horsin);
                        $('#tbHoraSiniestroMod').attr("disabled", "disabled");
                        $("#tbXSinMod").val(value.latsin);
                        $("#tbYSinMod").val(value.lonsin);
                        $("#ddl_distritoSiniestroMod").append('<option value="' + value.distrito + '">' + value.distrito + '</option>').val(value.distrito);
                        $("#ddl_provinciaFindMod").append('<option value="' + value.provincia + '">' + value.provincia + '</option>').val(value.provincia);
                        $("#ddl_ciudadFindMod").append('<option value="' + value.canton + '">' + value.canton + '</option>').val(value.canton);
                        $("#ddl_parroquiaMod").append('<option value="' + value.parroquia + '">' + value.parroquia + '</option>').val(value.parroquia);
                        $("#ddlCircuitoSinMod").append('<option value="' + value.circuito + '">' + value.circuito + '</option>').val(value.circuito);
                        $("#ddl_SubCircuitoMod").append('<option value="' + value.subcircuito + '">' + value.subcircuito + '</option>').val(value.subcircuito);
                        ($("#ddl_tipo_zonaMod").append('<option value="' + value.zonsin + '">' + value.zonsin + '</option>').val(value.zonsin));
                        $("#tbdireccionSiniestroMod").val(value.dirsin);
                        $('#tbdireccionSiniestroMod').attr("disabled", "disabled");
                        $("#tbnumfalsinMod").val(value.numfalsin);
                        $('#tbnumfalsinMod').attr("disabled", "disabled");
                        $("#tbnumlessinMod").val(value.numlessin);
                        $('#tbnumlessinMod').attr("disabled", "disabled");
                        ($("#ddl_condAtmosfericaMod").append('<option value="' + value.conatmsin + '">' + value.conatmsin + '</option>').val(value.conatmsin));
                        ($("#ddl_condViaMod").append('<option value="' + value.conviasin + '">' + value.conviasin + '</option>').val(value.conviasin));
                        if (value.luzartsin.toString() !== '' && value.luzartsin.toString() !== 'SELECCIONAR')
                            ($("#ddl_luzArtificialMod").append('<option value="' + value.luzartsin + '">' + value.luzartsin + '</option>').val(value.luzartsin));
                        else
                            $("#ddl_luzArtificialMod").css("display","none");

                        ($("#ddl_tipoViaMod").append('<option value="' + value.desviasin + '">' + value.desviasin + '</option>').val(value.desviasin));
                        ($("#ddl_limVelocidadMod").append('<option value="' + value.limvelsin + '">' + value.limvelsin + '</option>').val(value.limvelsin));
                        ($("#ddl_controlInterseccionMod").append('<option value="' + value.intsin + '">' + value.intsin + '</option>').val(value.intsin));

                        ($("#ddl_materialSuperficieMod").append('<option value="' + value.matsupviasin + '">' + value.matsupviasin + '</option>').val(value.matsupviasin));
                        ($("#ddl_obstViaMod").append('<option value="' + value.obsviasin + '">' + value.obsviasin + '</option>').val(value.obsviasin));
                        ($("#ddl_lugarViaMod").append('<option value="' + value.lugviasin + '">' + value.lugviasin + '</option>').val(value.lugviasin));
                        ($("#ddl_numCarrilesMod").append('<option value="' + value.numcarsin + '">' + value.numcarsin + '</option>').val(value.numcarsin));
                        ($("#ddl_curvaExistenteMod").append('<option value="' + value.cursin + '">' + value.cursin + '</option>').val(value.cursin));
                        ($("#ddl_senializacionExistenteMod").append('<option value="' + value.sensin + '">' + value.sensin + '</option>').val(value.sensin));
                        ($("#ddl_tipoSiniestroMod").append('<option value="' + value.tiposiniestro + '">' + value.tiposiniestro + '</option>').val(value.tiposiniestro));
                        ($("#ddl_causaProbableSiniestroMod").append('<option value="' + value.causa_probable + '">' + value.causa_probable + '</option>').val(value.causa_probable));
                        ($("#ddl_causaRealSiniestroMod").append('<option value="' + value.causa_real + '">' + value.causa_real + '</option>').val(value.causa_real));
                        //alert(value.traviasin);
                        if (value.traviasin === true)
                        {
                            $('#ddl_trabajosViaEdit').append('<option value="SI">SI</option>').val('SI');
                        }
                        else
                            $('#ddl_trabajosViaEdit').append('<option value="NO">NO</option>').val('NO');
                        
                      //  $("#divCamposFind").children().attr("disabled", "disabled");
                    });

                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus + ": " + XMLHttpRequest.responseText);
                }

            });
        }
       
        $('#btnVerVistaVhl').click(function () {
            var esVisible = $("#divVehiculosInvolucrados").is(":visible");
           // alert(esVisible);
            if (esVisible) {
                ($('#divVehiculosInvolucrados').css("display", "none"));
            }
            else {
                ($('#divVehiculosInvolucrados').css("display", "block"));
                $('#divVehiculosInvolucrados').slideDown(100000);
            }
            
        });
        $('#btnVerVictimas').click(function () {
            var esVisible = $("#divVerVictimas").is(":visible");
            $("#dt_victimas").find("tr:gt(0)").remove();
            // alert(esVisible);
            if (esVisible) {
                ($('#divVerVictimas').css("display", "none"));
            }
            else {
                ($('#divVerVictimas').css("display", "block"));
                $('#divVerVictimas').slideDown(10);
                if ($('#tbSiniestroMod').val() !== '' || $('#tbSiniestroMod').val() !== '0') {
                    console.log('cod vic' + ' ' + codsinAccionPeaton.toString());
                    $.ajax({
                        type: "get",
                        url: '../ObtenerlistaVictimasInvolucrados',
                        data: { codsin: codsinAccionPeaton },
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (jsonResult) {
                            console.log(jsonResult);
                            $.each(jsonResult, function (key, value) {
                                /* Vamos agregando a nuestra tabla las filas necesarias */
                                $("#dt_victimas").append("<tr><td>" + value.tipidenvicinv + "</td><td>" + value.numidenvicinv + "</td><td>" + value.edavicinv + "</td><td>" + value.sexo + "</td><td>" + value.convicinv24 + "</td><td>" + value.tipparvicinv + "</td><td>" + value.usO_CASO + "</td><td>" + value.usO_CINTU + "</td><td>" + value.posvicinv + "</td><td>" + value.conS_ALCOHOL + "</td><td>" + value.placavhl + "</td></tr>");
                            });

                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus + ": " + XMLHttpRequest.responseText);
                        }

                    });
                }
            }

        });
        $('#btnVerAccionesPeaton').click(function () {
            var esVisible = $("#divVerAccionesPeaton").is(":visible");
            // alert(esVisible);
            if (esVisible) {
                ($('#divVerAccionesPeaton').css("display", "none"));
                
            }
            else {
                ($('#divVerAccionesPeaton').css("display", "block"));
                $('#divVerAccionesPeaton').slideDown(10);
                if ($('#tbSiniestroMod').val() !== '' || $('#tbSiniestroMod').val() !== '0') {
                    console.log('cod peaton' + ' ' + codsinAccionPeaton.toString());
                    /******************************
                    *VISTA ACCIONES PEATON
                    *******************************/
                    $("#dt_AccionesPeaton").find("tr:gt(0)").remove();
                    $.ajax({
                        type: "get",
                        url: '../ObtenerlistaVistaAccionesPeatones',
                        data: { codsin: codsinAccionPeaton },
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (jsonResult) {
                            console.log(jsonResult);
                            $.each(jsonResult, function (key, value) {
                                /* Vamos agregando a nuestra tabla las filas necesarias */
                                $("#dt_AccionesPeaton").append("<tr><td>" + value.numidenvicinv + "</td><td>" + value.desaccpea + "</td><td>" + value.convicinv24 + "</td><td>" + value.placvehinv + "</td></tr>");
                            });

                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus + ": " + XMLHttpRequest.responseText);
                        }

                    });
                }
            }

        });
  
        $('#btnVerDaniosterceros').click(function () {
            var esVisible = $("#divVerDaniosterceros").is(":visible");
            // alert(esVisible);
            if (esVisible) {
                ($('#divVerDaniosterceros').css("display", "none"));
            }
            else {
                ($('#divVerDaniosterceros').css("display", "block"));
                $('#divVerDaniosterceros').slideDown(10);
                if ($('#tbSiniestroMod').val() !== '' || $('#tbSiniestroMod').val() !== '0') {
                    console.log('cod materiales' + ' ' + codsinAccionPeaton.toString());
                    /******************************
                  *VISTA DANIOS TERCEROS
                  *******************************/
                    
                    
                    $("#dt_DaniosTerceros").find("tr:gt(0)").remove();
                    $.ajax({
                        type: "get",
                        url: '../ObtenerlistaVistaDaniosTerceros',
                        data: { codsin: codsinAccionPeaton },
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (jsonResult) {
                            console.log(jsonResult);
                            $.each(jsonResult, function (key, value) {
                                /* Vamos agregando a nuestra tabla las filas necesarias */
                                $("#dt_DaniosTerceros").append("<tr><td>" + value.destipdater + "</td><td>" + value.obsdater + "</td><td>" + value.codsin + "</td></tr>");
                            });

                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            alert(textStatus + ": " + XMLHttpRequest.responseText);
                        }

                    });
                }
            }

        });


        $('#btnModificaSiniestroValidaEstadis').click(function () {
            ($('#panelMensajeSiniestroFind').css("display", "none"));
            if ($('#tbSiniestroMod').val() !== '' || $('#tbSiniestroMod').val() !== '0') {
                console.log('cod modif' + ' ' + codsinAccionPeaton.toString());
                //

                $.ajax({
                    type: "get",
                    url: '../modificaRegistroValidadoParaEstadisticas',
                    data: { codsin:codsinAccionPeaton },
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (retorno) {
                        console.log('retorno' + ' ' + retorno.toString());
                        if (retorno.toString() === '0') {
                            mensaje = 'Error al grabar los datos.';
                            ($('#mensajeLabelSiniestroFind').html(mensaje.toString()));
                            ($('#divMensajeSiniestroFind').attr("class", "alert alert-block alert-danger fade in smaller"));
                            ($('#panelMensajeSiniestroFind').css("display", "block"));
                        }
                        else {
                            
                            mensaje = 'Siniestro # ' + codsinAccionPeaton + ' validado correctamente.';
                            ($('#divMensajeSiniestroFind').attr("class", "alert alert-success smaller"));
                            ($('#mensajeLabelSiniestroFind').html(mensaje.toString()));
                            ($('#panelMensajeSiniestroFind').css("display", "block"));
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert(textStatus + ": " + XMLHttpRequest.responseText);
                    }

                });
            }
        });


    /************************************************
    *Descarga de excel siniestro
    ***********************************************/
        //$('#btnDescargarExcelSin').click(function (e) {
        //  ///  alert('descarga');
        //    var codsinP = $('#tbSiniestroMod').val();
        //    console.log(codsinP);
        //    if (codsinP !== '0' || codsinP !== '')
        //    {
        //        $.ajax({
        //            type: "get",
        //            url: 'JsonDescargaExcel',
        //            data: { codsin: codsinP },
        //            contentType: "application/json; charset=utf-8",
        //            dataType: "json",
        //            success: function (retorno) {
        //                console.log('retorno' + ' ' + retorno.toString());
        //                if (retorno.toString() === '0') {
        //                    mensaje = 'Error al generar Excel. ' + ' ' + retorno;
        //                    ($('#mensajeLabelSiniestroFind').html(mensaje.toString()));
        //                    ($('#divMensajeSiniestroFind').attr("class", "alert alert-block alert-danger fade in smaller"));
        //                    ($('#panelMensajeSiniestroFind').css("display", "block"));
        //                }
        //                else {

        //                    mensaje = 'Archivo descargado correctamente.';
        //                    ($('#divMensajeSiniestroFind').attr("class", "alert alert-success smaller"));
        //                    ($('#mensajeLabelSiniestroFind').html(mensaje.toString()));
        //                    ($('#panelMensajeSiniestroFind').css("display", "block"));
        //                 //   e.preventDefault();  //stop the browser from following
        //                  //  window.location.href = 'http://localhost:11559/Siniestro_1_2017.xlsx';
        //                }
        //            },
        //            error: function (XMLHttpRequest, textStatus, errorThrown) {
        //                alert(textStatus + ": " + errorThrown.responseText);
        //            }

        //        });
        //    }
        //});
        

});



function descargarArchivo(contenidoEnBlob, nombreArchivo) {
    var reader = new FileReader();
    reader.onload = function (event) {
        var save = document.createElement('a');
        save.href = event.target.result;
        save.target = '_blank';
        save.download = nombreArchivo || 'archivo.dat';
        var clicEvent = new MouseEvent('click', {
            'view': window,
            'bubbles': true,
            'cancelable': true
        });
        save.dispatchEvent(clicEvent);
        (window.URL || window.webkitURL).revokeObjectURL(save.href);
    };
    reader.readAsDataURL(contenidoEnBlob);
};

//Función de ayuda: reúne los datos a exportar en un solo objeto
function obtenerDatos() {
    return {
        nombre: document.getElementById('textNombre').value,
        telefono: document.getElementById('textTelefono').value,
        fecha: (new Date()).toLocaleDateString()
    };
};

//Función de ayuda: "escapa" las entidades XML necesarias
//para los valores (y atributos) del archivo XML
function escaparXML(cadena) {
    if (typeof cadena !== 'string') {
        return '';
    };
    cadena = cadena.replace('&', '&amp;')
        .replace('<', '&lt;')
        .replace('>', '&gt;')
        .replace('"', '&quot;');
    return cadena;
};

//Genera un objeto Blob con los datos en un archivo TXT
function generarTexto(datos) {
    var texto = [];
    texto.push('Datos Personales:\n');
    texto.push('Nombre: ');
    texto.push(datos.nombre);
    texto.push('\n');
    texto.push('Teléfono: ');
    texto.push(datos.telefono);
    texto.push('\n');
    texto.push('Fecha: ');
    texto.push(datos.fecha);
    texto.push('\n');
    //El contructor de Blob requiere un Array en el primer parámetro
    //así que no es necesario usar toString. el segundo parámetro
    //es el tipo MIME del archivo
    return new Blob(texto, {
        type: 'text/plain'
    });
};


//Genera un objeto Blob con los datos en un archivo XML
function generarXml(datos) {
    var texto = [];
    texto.push('<?xml version="1.0" encoding="UTF-8" ?>\n');
    texto.push('<datos>\n');
    texto.push('\t<nombre>');
    texto.push(escaparXML(datos.nombre));
    texto.push('</nombre>\n');
    texto.push('\t<telefono>');
    texto.push(escaparXML(datos.telefono));
    texto.push('</telefono>\n');
    texto.push('\t<fecha>');
    texto.push(escaparXML(datos.fecha));
    texto.push('</fecha>\n');
    texto.push('</datos>');
    //No olvidemos especificar el tipo MIME correcto :)
    return new Blob(texto, {
        type: 'application/xml'
    });
};

document.getElementById('boton-xml').addEventListener('click', function () {
    var datos = obtenerDatos();
    descargarArchivo(generarXml(datos), 'archivo.xml');
}, false);

document.getElementById('boton-txt').addEventListener('click', function () {
    var datos = obtenerDatos();
    descargarArchivo(generarTexto(datos), 'archivo.xlsx');
}, false);