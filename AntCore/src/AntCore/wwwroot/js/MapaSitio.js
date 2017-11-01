require([
    "esri/Map",
    "esri/views/SceneView",
  "esri/layers/GraphicsLayer",
   "esri/Graphic",

   "esri/geometry/Point",
   "esri/symbols/SimpleMarkerSymbol",


    "dojo/query",
    "dojo/on",
    "dojo/domReady!"
],
  function (
    Map, SceneView, GraphicsLayer, Graphic, Point, SimpleMarkerSymbol, query, on
  ) {

      var map = new Map({
          basemap: "streets" //"dark-gray"
      });

      var view = new SceneView({
          container: "viewDiv",
          map: map,

          camera: { // autocasts as new Camera()
              position: { // autocasts as new Point()
                  x: -78.490698,
                  y: -0.202612,
                  z: 50000000
              },

          }
      });
      var graphicsLayer = new GraphicsLayer();
      map.add(graphicsLayer);

      /*****************************************************************
       * 
       * Add event listeners to go to a target point using animation options
       *
       *****************************************************************/


      // Define your own function for the easing option
      function customEasing(t) {
          return 1 - Math.abs(Math.sin(-1.7 + t * 4.5 * Math.PI)) * Math.pow(
            0.5, t * 10);
      }

      on(dojo.query("#bounceQuito"), "click", function () {
          // London

          var latitud = $("#lbLatitudGeo").html();
          var longitud = $("#lbLonguitudGeo").html();
          //alert(latitud + ' ' + longitud);

          var point = new Point({
              x: longitud,
              y: latitud,
              z: 120
          }),

            markerSymbol = new SimpleMarkerSymbol({
                color: [226, 119, 40],

                outline: { // autocasts as new SimpleLineSymbol()
                    color: [255, 255, 255],
                    width: 2
                }
            });

          var pointGraphic = new Graphic({
              geometry: point,
              symbol: markerSymbol
          });

          graphicsLayer.add(pointGraphic);
          view.goTo({
              position: {
                  x: longitud,
                  y: latitud,
                  z: 1000,
                  spatialReference: {
                      wkid: 4326
                  }
              },
              heading: 0,
              tilt: 0
          }, {
              speedFactor: 0.3,
              easing: customEasing
          });
      });

  });



