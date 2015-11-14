var API = isDev ? 'data' : 'api';
API += '/';

var CLEAR_DELAY = 2800;

var isProdTest = false;

var map;

var listOfCities = {}
    , stackOfTweets
    , points = []
    , tweetIds = {};

$(function () {

  function _displayPreviousPointsLower() {
    points.forEach(function (point) {
      point.marker.setZIndex(3);
      point.infowindow.setZIndex(3);
    });
  }

  function _isTweetExist(tweetId) {
    var result = false;

    points.forEach(function (point) {
      if (point.tweet === point) {
        result = true;
      }
    });

    return result;
  }

  function _getTemplate(tweet, content) {
    var template = '';

    template += '<div class="mtt-content">';
      template += '<div class="mtt-header">';
        template += '<div class="mtt-avatar">';
          template += '<img class="avatar" src="' + tweet.avatar + '" alt="">';
        template += '</div>';
        template += '<p>';
          template += '<strong class="mtt-fullname">';
            template += tweet.userFullName;
            // template += 'Alexander Constantinopolsky'; // TODO name
          template += '</strong>';
          template += '<span class="mtt-username">';
            template += '@' + tweet.user;
            // template += '@johnsnow'; // TODO userName
          template += '</span>';
        template += '</p>';
      template += '</div>';
      template += '<div class="mtt-body">';
        template += '<p>';
          template += tweet.text;
        template += '</p>';
      template += '</div>';

    template += '</div>';

    return template;
  }

  function getCities(argument) {
    var url = API + 'cities';
    url += isDev ? '.json' : '';

    $.getJSON(url, function (data) {
      data.forEach(function (item) {
        var cityId = item.id;

        listOfCities[cityId] = item;
      });

      getTweets();
    })
  }

  function getTweets() {
    var url = API + 'tweets';
    url += isDev ? '.json' : '';

    $.getJSON(url, function (data) {
      stackOfTweets = data;

      $('#map').trigger('show');
    });
  }

  $('#map').on('init', function () {
    getCities();

    setInterval(function () { // clear up
      if (points.length) {
        var shifted = points.shift();
        shifted.infowindow.close();
        shifted.marker.setMap(null);

        if (points.length < 3) {
          getTweets();
        }
      }
    }, CLEAR_DELAY);
  })

  $('#map').on('show', function () {
    $(stackOfTweets).each(function (index, tweet) {

      if (!_isTweetExist()) {
        var cityId = tweet.cityId
            , city = listOfCities[cityId]
            , location = city['location']
            , coords = new google.maps.LatLng(location.lat, location.lng)
            , template = _getTemplate(tweet);

        var infowindow = new google.maps.InfoWindow({
          content: template,
          maxWidth: 300
        });

        var marker = new google.maps.Marker({
          position: coords,
          map: map,
          visible: false
        });

        points.push({
          marker: marker,
          infowindow: infowindow,
          tweet: tweet.id
        });

        // TODO control timeout;
        setTimeout(function () {
          _displayPreviousPointsLower();

          marker.setVisible(true);
          marker.setZIndex(10);

          infowindow.setZIndex(10);
          infowindow.open(map, marker);
        }, CLEAR_DELAY + 1000 * index);
      }
    });
  })
})

// TODO move
function initMap() {
  // var customMapType = new google.maps.StyledMapType([
  //     {
  //       stylers: [
  //         {hue: '#890000'},
  //         {visibility: 'simplified'},
  //         {gamma: 0.5},
  //         {weight: 0.5}
  //       ]
  //     },
  //     {
  //       elementType: 'labels',
  //       stylers: [{visibility: 'off'}]
  //     },
  //     {
  //       featureType: 'water',
  //       stylers: [{color: '#890000'}]
  //     }
  //   ], {
  //     name: 'Custom Style'
  // });
  // var customMapTypeId = 'custom_style';

  map = new google.maps.Map(document.getElementById('map'), {
    center: {lat: 34.34436, lng: 108.92035}, // xi'an
    zoom: 5
    // , mapTypeControlOptions: {
    //   mapTypeIds: [google.maps.MapTypeId.ROADMAP, customMapTypeId]
    // }
  });

  // map.mapTypes.set(customMapTypeId, customMapType);
  // map.setMapTypeId(customMapTypeId);

  $('#map').trigger('init');
}
