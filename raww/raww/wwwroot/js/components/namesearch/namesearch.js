﻿define(['knockout', 'dataservice'], (ko, ds) => {
    return function (params) {
        //private part
        let searchresults = ko.observableArray([]);
        let searchterm = ko.observable();
        let selectedPerson = ko.observable();


        let selectPerson = person => {
            console.log(person);
            selectedPerson(person);
        }

        let clickSearch = function () {
            ds.searchName(searchterm(), function (data) { searchresults(data.personlist) });
            searchterm("");
        }

        //public part
        return {
            searchresults,
            searchterm,
            clickSearch,
            selectPerson,
            selectedPerson
        }
    }
});