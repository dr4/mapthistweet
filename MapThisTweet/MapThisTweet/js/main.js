var isDev = true;

$(function () {
  console.log('test jquery');

  if (isDev) {
    $.getJSON('data/test.json', function (res) {
      console.log('test', res.test);
    })
  } else {
    $.getJSON('api/tweets', function (res) {
      console.log('tweets', res);
    })
  }

})
