var map = null;
var geocoder;
var points = [];
var shapes = [];
var center = null;

function LoadMap(lng, lat) {
    var latitude = lat;
    var longitude = lng;
    geocoder = new google.maps.Geocoder();

    map = new google.maps.Map(document.getElementById('mapDiv'), {
        center: { lat: latitude, lng: longitude },
        zoom: 14
    });
}

/*
 * For setting markers on map with name and description.
 */
function LoadPin(longitude, latitude, name, description) {
    var latLng = new google.maps.LatLng(latitude, longitude);

    var infoWindow = new google.maps.InfoWindow({
        content: description
    });

    var point = new google.maps.Marker({
        position: latLng,
        map: map,
        title: name
    });

    point.addListener('click', function() {
        infoWindow.open(map, point);
    });

    point.setMap(map);
}

function FindAddressOnMap(where) {
    getCoordinates(where,
        function(coordinates) {
            map.setCenter(coordinates);
        });
}

function getCoordinates(where, callback) {
    var coordinates;

    geocoder.geocode({ address: where },
        function(results, status) {
            coordinates = results[0].geometry.location;
            callback(coordinates);
        });
}

function callbackForLocation(layer, resultsArray, places,
    hasMore, VEErrorMessage) {
    clearMap();

    if (places === null)
        return;

    //Make a pushpin for each place we find
    $.each(places, function (i, item) {
        description = "";
        if (item.Description !== undefined) {
            description = item.Description;
        }
        var LL = new VELatLong(item.LatLong.Latitude,
            item.LatLong.Longitude);

        LoadPin(LL, item.Name, description);
    });

    //Make sure all pushpins are visible
    if (points.length > 1) {
        map.SetMapView(points);
    }

    //If we've found exactly one place, that's our address.
    if (points.length === 1) {
        $("#Latitude").val(points[0].Latitude);
        $("#Longitude").val(points[0].Longitude);
    }
}

function clearMap() {
    map.Clear();
    points = [];
    shapes = [];
}