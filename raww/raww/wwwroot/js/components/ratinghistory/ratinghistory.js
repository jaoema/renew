define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function(params) {
        //private part

        let ratinghistory = ko.observableArray([]);
        let prev = ko.observable();
        let next = ko.observable();
        let pageSizes = [10, 20, 30];
        let selectedPageSize = ko.observableArray([10]);

        //empty after testing:
        let username = ko.observable("hans1");

        postman.subscribe("UserSignIn", username);

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

        let deleteRating = rating => {
            fetch("api/ratinghistory/remove/" + username() + "/" + rating.tconst , {
                method: 'POST'
            })
                .then(ds.handleErrors)
                .then(response => {
                    getData();
                }).catch(error => {
                    console.log(error);
                });
        }

        getData();


        //public part
        return {
            ratinghistory,
            showPrev,
            enableNext,
            enablePrev,
            pageSizes,
            selectedPageSize,
            showNext,
            deleteRating
        };
    }
});