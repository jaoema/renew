define(['knockout'], function (ko) {
    //private part
    let name = ko.observable("Peter");
    let names = ko.observableArray([]);


    fetch("api/searchhistory")
        .then(function(response) {
            return response.json();
        })
        .then(function(data) {
            names(data.mappedhistory);
        });

    //public part
    return {
        name,
        names
    };
});