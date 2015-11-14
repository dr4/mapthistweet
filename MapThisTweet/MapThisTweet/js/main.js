var isProdTest = false;
/**
 * TODO
 * create script
 * trigger
 * local src
 * set center to china
 */

$(function () {
  if (isDev) {
    $.getJSON('data/test.json', function (res) {
      console.log('test', res.test);
    })
  } else {
    $.getJSON('api/tweets', function (res) {
      console.log('tweets', res);
    })
  }

  $('#map').on('init', function () {
    console.log('map init');
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
