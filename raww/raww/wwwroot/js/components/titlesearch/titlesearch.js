define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        //private part
        let searchresults = ko.observableArray([]);
        let searchterm = ko.observable("");
        var currentSearchterm = ("");
        let selectedTitle = ko.observable();
        let searchFromNav = ko.observable(false);
        let searchFromComp = ko.observable(false);
        let prev = ko.observable();
        let next = ko.observable();
        let pageSizes = [10, 20, 30];
        let selectedPageSize = ko.observableArray([10]);

        postman.subscribe('changeSearchterm', searchterm);
        postman.subscribe('changeSearchFromNav', searchFromNav);


        let getData = url => {
            ds.getSearchTitle(searchterm(), url, data => {
                prev(data.prev);
                next(data.next);
                searchresults(data.titlelist);
            });
        }

        let selectTitle = title => {
            selectedTitle(title);
            postman.publish('changeTitle', title);
        }

        let clickSearch = function () {
            getData();
            currentSearchterm = searchterm();
            searchterm("");
            searchFromComp(true);
            searchFromNav(false);
        }

        let showPrev = () => {
            getData(prev());
        }
        let showNext = () => {
            getData(next());
        }

        let enablePrev = ko.computed(() => prev() !== null);
        let enableNext = ko.computed(() => next() !== null);

        selectedPageSize.subscribe(() => {
            var size = selectedPageSize()[0];
            getData(ds.getTitlesearchUrlWithPageSize(size, currentSearchterm));
        });

        let enableSearch = ko.computed(() => searchterm() !== "");

        searchFromNav.subscribe(function (newValue) {
            if (newValue === true) {
                getData();
                currentSearchterm = searchterm();

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
            showPrev,
            showNext,
            enablePrev,
            enableNext,
            pageSizes,
            selectedPageSize,
            searchFromNav,
            searchFromComp
        }
    }
});