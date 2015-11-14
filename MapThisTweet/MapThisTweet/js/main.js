var isProdTest = false;
/**
 * TODO
 * create script
 * trigger
 * local src
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
    center: {lat: -34.397, lng: 150.644},
    zoom: 8
  });

  $('#map').trigger('init');
}
