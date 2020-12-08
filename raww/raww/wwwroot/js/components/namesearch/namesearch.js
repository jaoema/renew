define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        //private part
        let searchresults = ko.observableArray([]);
        let searchterm = ko.observable("");
        var currentSearchterm = ("");
        let selectedPerson = ko.observable();
        let prev = ko.observable();
        let next = ko.observable();
        let pageSizes = [10, 20, 30];
        let selectedPageSize = ko.observableArray([10]);


        let getData = url => {
            ds.getSearchName(searchterm(), url, data => {
                prev(data.prev);
                next(data.next);
                searchresults(data.personlist);
                console.log(data.count);
            });
        }

        let selectPerson = person => {
            selectedPerson(person);
            postman.publish('changePerson', person);
        }

        let clickSearch = function () {
            getData();
            currentSearchterm = searchterm();
            searchterm("");
        }

        let showPrev = history => {
            getData(prev());
        }
        let showNext = history => {
            getData(next());
        }

        let enablePrev = ko.computed(() => prev() !== null);
        let enableNext = ko.computed(() => next() !== null);

        selectedPageSize.subscribe(() => {
            var size = selectedPageSize()[0];
            getData(ds.getNamesearchUrlWithPageSize(size, currentSearchterm));
        });

       

        let enableSearch = ko.computed(() => searchterm() !== "");

        //public part
        return {
            searchresults,
            searchterm,
            clickSearch,
            selectPerson,
            selectedPerson,
            enableSearch,
            showPrev,
            enableNext,
            enablePrev,
            pageSizes,
            selectedPageSize,
            showNext
        }
    }
});