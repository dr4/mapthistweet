var DATAPATH = 'data/';

var isProdTest = false;

/**
 * TODO
 * parse places
 * parse payload
 * show balloons
 *
 */

$(function () {
  // example of response

  // [
  //   {
  //     'id': '123', // place_id
  //     'tweet': '', // html
  //   },
  //   ...
  // ]

  function getRequestUrl() {
    var url = '';

    if (isDev) {
      url += DATAPATH + 'tweets.json'
    } else {
      url += 'tweets'
    }

    return url;
  }

  function requestData() {
    var requestUrl = getRequestUrl();

    $.getJSON(requestUrl, function (data) {
      // show data with setTimeout

    });
  }

  $('#map').on('init', function () {
    requestData();
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
