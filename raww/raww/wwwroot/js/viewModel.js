define(['knockout'], function (ko) {
    //private part
    let name = ko.observable("Peter");
    let names = ko.observableArray(
        [{ name: "Peter" }, { name: "Rasmus" }, { name: "Jakob" }]);


    fetch("api/searchhistory")
        .then(function(response) {
            return response.json();
        })
        .then(function(data) {
            names(data);
        });

    //public part
    return {
        name,
        names
    };
});