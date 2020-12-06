define(['knockout', 'dataservice'], (ko, ds) => {
    return function(params) {
        //private part

        let searchhistory = ko.observableArray([]);
        let prev = ko.observable();
        let next = ko.observable();
        let pageSizes = [10, 20, 30];
        let selectedPageSize = ko.observableArray([10]);


        let getData = url => {
            ds.getSearchhistory(url, data => {
                prev(data.prev);
                next(data.next);
                searchhistory(data.mappedhistory);
            });
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
            getData(ds.getSearchhistoryUrlWithPageSize(size));
        });

        getData();


        //public part
        return {
            searchhistory,
            showPrev,
            enableNext,
            enablePrev,
            pageSizes,
            selectedPageSize,
            showNext
        };
    }
});