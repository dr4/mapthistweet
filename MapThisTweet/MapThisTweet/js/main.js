var DATAPATH = 'data/';

var isProdTest = false;

var map;

var listOfCities
    , stackOfTweets;

/**
 * TODO
 * parse places +
 * parse payload +
 * show balloons
 * stream
 */

$(function () {

  function getCities(argument) {
    var url = isDev ? DATAPATH + 'cities.json' : 'citys';

    $.getJSON(url, function (data) {
      listOfCities = data;
    })
  }

  function getTweets() {
    var url = isDev ? DATAPATH + 'tweets.json' : 'tweets';

    $.getJSON(url, function (data) {
      stackOfTweets = data;

      $('#map').trigger('show');
    });
  }

  $('#map').on('init', function () {
    getCities();
    getTweets();
  })

  $('#map').on('show', function () {

    if (stackOfTweets.length) {

      $(stackOfTweets).each(function (index, item) {
        var place_id = item.place_id;
        console.log(place_id);
        var coords = listOfCities[place_id].location;
        console.log(coords);
        var content = item.tweet;

        var realCoords = new google.maps.LatLng(coords.lat, coords.lng);

        var infowindow = new google.maps.InfoWindow({
          content: content,
          maxWidth: 200
        });

        var marker = new google.maps.Marker({
          position: realCoords,
          map: map
        });

        setTimeout(function () {
          infowindow.open(map, marker);
        }, 1000 + 1000 * index);
      })

    }


  })
})



function initMap() {
  map = new google.maps.Map(document.getElementById('map'), {
    center: {lat: 34.34436, lng: 108.92035}, // xi'an
    zoom: 5
  });

  $('#map').trigger('init');
}
