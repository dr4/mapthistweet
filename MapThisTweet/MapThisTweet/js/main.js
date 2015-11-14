var DATAPATH = 'data/';

var isProdTest = false;

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

      $('#map').trigger('snow');
    });
  }

  $('#map').on('init', function () {
    getCities();
    getTweets();
  })

  #('#map').on('show', function () {

  })
})

var map;

function initMap() {
  map = new google.maps.Map(document.getElementById('map'), {
    center: {lat: 34.34436, lng: 108.92035}, // xi'an
    zoom: 5
  });

  $('#map').trigger('init');
}
