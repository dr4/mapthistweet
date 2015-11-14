var DATAPATH = 'data/';

var isProdTest = false;

var map;

var listOfCities = {}
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
      data.forEach(function (item) {
        var cityId = item.id;

        listOfCities[cityId] = item;
      });

      console.log(listOfCities);
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
      console.log('tweet');

      $(stackOfTweets).each(function (index, item) {

        console.log(index);
        var cityId = item.cityId;
        var coords = listOfCities[cityId]['location'];
        var content = item.text;

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
