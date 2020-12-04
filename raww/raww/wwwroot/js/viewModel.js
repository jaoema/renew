define(['knockout'], function (ko) {
    //private part
    let currentComponent = ko.observable("home");
    let menuElements = ["Home", "Contact"];

    //let names = ko.observableArray([]);


    //fetch("api/searchhistory")
    //    .then(function(response) {
    //        return response.json();
    //    })
    //    .then(function(data) {
    //        names(data.mappedhistory);
    //    });

    //public part
    return {
        currentComponent,
        menuElements
    };
});