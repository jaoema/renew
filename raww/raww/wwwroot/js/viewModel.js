﻿define(['knockout'], function (ko) {
    //private part
    let name = ko.observable("Peter");
    let names = ko.observableArray();


    fetch("api/searchhistory")
        .then(function(response) {
            return response.json();
        })
        .then(function(data) {
            names(data.mappedhistory);
            debugger;
            console.log(names());
        });

    //public part
    return {
        name,
        names
    };
});