define(['knockout', 'dataservice'], (ko, ds) => {
    return function(params) {

        let names = ko.observableArray([]);

        ds.getSearchHistory(function(data) { names(data.mappedhistory) });


        //public part
        return {
            names
        }
    }
});