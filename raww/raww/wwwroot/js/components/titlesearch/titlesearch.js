define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        //private part
        let searchresults = ko.observableArray([]);
        let searchterm = ko.observable("");
        let selectedTitle = ko.observable();
        let searchFromNav = ko.observable(false);
        let searchFromComp = ko.observable(false);

        postman.subscribe('changeSearchterm', searchterm);
        postman.subscribe('changeSearchFromNav', searchFromNav);


        let selectTitle = title => {
            selectedTitle(title);
            postman.publish('changeTitle', title);
        }

        let clickSearch = function () {
            ds.searchTitle(searchterm(), function (data) { searchresults(data.titlelist) });
            searchterm("");
            searchFromComp(true);
            searchFromNav(false);
        }

        let enableSearch = ko.computed(() => searchterm() !== "");

        searchFromNav.subscribe(function (newValue) {
            if (newValue === true) {
                ds.searchTitle(searchterm(), function (data) { searchresults(data.titlelist) });

                searchFromComp(false);
                searchterm("");
            }
        });


        //public part
        return {
            searchresults,
            searchterm,
            clickSearch,
            selectTitle,
            selectedTitle,
            enableSearch,
            searchFromNav,
            searchFromComp
        }
    }
});