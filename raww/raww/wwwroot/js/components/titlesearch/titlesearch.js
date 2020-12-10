define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        //private part
        let searchresults = ko.observableArray([]);
        let searchterm = ko.observable("");
        var currentSearchterm = ("");
        let selectedTconst = ko.observable();
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

        let selectTconst = tconst => {
            postman.publish('changeCurrentComp', "titledetails");
            selectedTconst(tconst.tconst);
            postman.publish('changeTconst', {tconst: selectedTconst(), showDetails: true });
         
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
            selectTconst,
            selectedTconst,
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