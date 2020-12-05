define(['knockout', 'dataservice'], (ko, ds) => {
    return function(params) {
        //private part

        let searchhistory = ko.observableArray([]);
        let prev = ko.observable();
        let next = ko.observable();


        let getData = url => {
            ds.getSearchhistory(url, data => {
                prev(data.prev);
                next(data.next);
                searchhistory(data.mappedhistory);
            });
        }

        let showPrev = history => {
            console.log(prev);
            getData(prev());
        }
        let showNext = history => {
            console.log(next());
            getData(next());
        }

        getData();


        //public part
        return {
            searchhistory,
            showPrev,
            showNext
        };
    }
});