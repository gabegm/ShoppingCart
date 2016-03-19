function isEmailAvailable() {
    $.ajax({
        type: "POST",
        url: "CarService.asmx/GetAllCars",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var cars = response.d;
            $('#output').empty();
            $.each(cars, function (index, car) {
                $('#output').append('<p><strong>' + car.Make + ' ' +
                                      car.Model + '</strong><br /> Year: ' +
                                      car.Year + '<br />Doors: ' +
                                      car.Doors + '<br />Colour: ' +
                                      car.Colour + '<br />Price: £' +
                                      car.Price + '</p>');
            });
        },
        failure: function (msg) {
            $('#output').text(msg);
        }
    });
}