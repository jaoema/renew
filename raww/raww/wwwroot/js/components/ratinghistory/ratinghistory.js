define(['knockout', 'dataservice'], (ko, ds) => {
    return function(params) {
        //private part

        let ratinghistory = ko.observableArray([]);
        let prev = ko.observable();
        let next = ko.observable();
        let pageSizes = [10, 20, 30];
        let selectedPageSize = ko.observableArray([10]);


        let getData = url => {
            ds.getRatinghistory(url, data => {
                prev(data.prev);
                next(data.next);
                ratinghistory(data.mappedhistory);
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
            getData(ds.getRatinghistoryUrlWithPageSize(size));
        });

        getData();


        //public part
        return {
            ratinghistory,
            showPrev,
            enableNext,
            enablePrev,
            pageSizes,
            selectedPageSize,
            showNext
        };
    }
});