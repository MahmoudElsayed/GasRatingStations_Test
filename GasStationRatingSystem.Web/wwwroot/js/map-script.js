
function getLocation() {
    if (($("#txtLat").val().length) && ($("#txtLng").val().length)) {
        myLatLng = { Lat: parseFloat(document.getElementById("txtLat" ).value), Long: parseFloat(document.getElementById("txtLng").value) };

        initMap(myLatLng)
    }

    else if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(function (position) {
            showPosition(position);

        })
    } else {
        document.getElementById("txtLat").value = parseFloat(21.485811);
        document.getElementById("txtLng").value = parseFloat(39.19250479999999);

        myLatLng = { Lat: parseFloat(document.getElementById("txtLat").value), Long: parseFloat(document.getElementById("txtLng").value) };
        initMap(myLatLng)

    }
}
function showPosition(position) {
    document.getElementById("txtLat").value = position.coords.latitude;
    document.getElementById("txtLng").value = position.coords.longitude;
    myLatLng = { Lat: position.coords.latitude, Long: position.coords.longitude };
    initMap(myLatLng)
}
let elementMap = document.getElementById("@@MapId");
var mainMap, mainmarker;
let markers = [];

function initMap(xop) {

    var sl = document.getElementById('mapenterlocation');

    if (sl != undefined) {
        sl.innerHTML = (`<input id="pac-input" class="controls" type="text" placeholder="Enter a location" />`);

    }

    let defaultLong,
        defaultLat;
    if (!xop) {
        defaultLong = parseFloat('@@Lng');
        defaultLat = parseFloat('@@Lat');
    }
    else {
        defaultLong = xop.Long;
        defaultLat = xop.Lat;
    }
    const map = mainMap = new google.maps.Map(elementMap, {
        center: { lat: defaultLat, lng: defaultLong },
        zoom: 17,
    });
    var input = document.getElementById("pac-input");
    var autocomplete = new google.maps.places.Autocomplete(input);
    autocomplete.bindTo("bounds", map);

    // Specify just the place data fields that you need.
    autocomplete.setFields(["place_id", "geometry", "name"]);
    map.controls[google.maps.ControlPosition.TOP_LEFT].push(input);
    var myLatLng = { lat: defaultLat, lng: defaultLong };
    let geocoder = new google.maps.Geocoder();
    let infowindow = new google.maps.InfoWindow();
    addMarker(myLatLng);

    var aShowOnMap = document.querySelector('#aShowOnMap');
    if (aShowOnMap != undefined) {
        aShowOnMap.setAttribute('href', 'https://www.google.com/maps/@@' + defaultLat + ',' + defaultLong + ',15z');

    }


    autocomplete.addListener("place_changed", () => {
        var place = autocomplete.getPlace();

        if (!place.geometry) {
            return;
        }
        document.getElementById("txtLat").value = place.geometry.location.lat();
        document.getElementById("txtLng").value = place.geometry.location.lng();


        if (place.geometry.viewport) {
            map.fitBounds(place.geometry.viewport);
        } else {
            map.setCenter(place.geometry.location);
            map.setZoom(17);
        }
        var myLatLng = { lat: place.geometry.location.lat(), lng: place.geometry.location.lng() };
        clearMarkers();
        addMarker(myLatLng);
        // Set the position of the marker using the place ID and location.
    });

    map.addListener("click", (mapsMouseEvent) => {
        document.getElementById("txtLat").value = mapsMouseEvent.latLng.lat();
        document.getElementById("txtLng").value = mapsMouseEvent.latLng.lng();
        myLatLng = { lat: mapsMouseEvent.latLng.lat(), lng: mapsMouseEvent.latLng.lng() };
        clearMarkers();
        addMarker(myLatLng);

    });


    function addMarker(location) {
        const marker = new google.maps.Marker({
            position: location,
            map: map,
        });
        markers.push(marker);
        infowindow.close();
        geocoder.geocode({ location: location }, (results, status) => {
            if (status === "OK") {
                if (results[0]) {
                    infowindow.setContent(results[0].formatted_address);
                    infowindow.open(map, marker);
                }
            }
        })

    }

    // Sets the map on all markers in the array.
    function setMapOnAll(map) {
        for (let i = 0; i < markers.length; i++) {
            markers[i].setMap(map);
        }
    }

    // Removes the markers from the map, but keeps them in the array.
    function clearMarkers() {
        setMapOnAll(null);
    }

    // Shows any markers currently in the array.
    function showMarkers() {
        setMapOnAll(map);
    }

    // Deletes all markers in the array by removing references to them.
    function deleteMarkers() {
        clearMarkers();
        markers = [];
    }
}


let appstyle = document.createElement('style');

appstyle.append(`<style>
        #map {
            height: 400px;
 width: 100%
 }

/* Optional: Makes the sample page fill the window. */

 .controls {
            background - color: #fff;
  border-radius: 2px;
  border: 1px solid transparent;
  box-shadow: 0 2px 6px rgba(0, 0, 0, 0.3);
   box-sizing: border-box;
   font-family: Roboto;
   font-size: 15px;
  font-weight: 300;
   height: 29px;
  margin-left: 17px;
  margin-top: 10px;
  outline: none;
  padding: 0 11px 0 13px;
  text-overflow: ellipsis;
  width: 400px;
    @@MapSearch
  }

  .controls:focus {
            border - color: #4d90fe;
  }

.title {
            font - weight: bold;
      }

  #infowindow-content {
            display: none;
   }

 #map #infowindow-content {
            display: inline;
 }
     .pac-container {
            z-index: 99999;
  }



</style>`);
document.body.append(appstyle);
//getLocation('source');
//$("#showmodalmapclient").on("click", function () {
//    $('#modalmap').modal('show');
//    getLocation('client');

//});

//$("#showmodalmapsource").on("click", function () {
//    $('#modalmap').modal('show');
//    getLocation('source');
//});