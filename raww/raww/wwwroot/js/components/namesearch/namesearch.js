define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        //private part
        let searchresults = ko.observableArray([]);
        let searchterm = ko.observable("");
        let selectedPerson = ko.observable();


        let selectPerson = person => {
            selectedPerson(person);
            postman.publish('changePerson', person);
        }

        let clickSearch = function () {
            ds.searchName(searchterm(), function (data) { searchresults(data.personlist) });
            searchterm("");
        }

        let enableSearch = ko.computed(() => searchterm() !== "");

        //public part
        return {
            searchresults,
            searchterm,
            clickSearch,
            selectPerson,
            selectedPerson,
            enableSearch
        }
    }
});