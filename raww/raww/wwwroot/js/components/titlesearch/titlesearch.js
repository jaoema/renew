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


        let getData = url => {
            ds.getSearchTitle(searchterm(), url, data => {
                //prev(data.prev);
                //next(data.next);
                searchresults(data.titlelist);
            });
        }

        let selectTitle = title => {
            selectedTitle(title);
            postman.publish('changeTitle', title);
        }

        let clickSearch = function () {
            getData();
            //debugger;
            searchterm("");
            searchFromComp(true);
            searchFromNav(false);
        }

        let enableSearch = ko.computed(() => searchterm() !== "");

        searchFromNav.subscribe(function (newValue) {
            if (newValue === true) {
                getData();

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