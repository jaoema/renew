define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        //private part
        let searchresults = ko.observableArray([]);
        let searchterm = ko.observable();
        let selectedTitle = ko.observable();


        let selectTitle = title => {
            selectedTitle(title);
            postman.publish('changeTitle', title);
        }

        let clickSearch = function () {
            ds.searchTitle(searchterm(), function (data) { searchresults(data.titlelist) });
            //debugger;
            searchterm("");
        }

        //public part
        return {
            searchresults,
            searchterm,
            clickSearch,
            selectTitle,
            selectedTitle
        }
    }
});