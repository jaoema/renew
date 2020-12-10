define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function(params) {
        //private part

        let bookmark = ko.observableArray([]);
        let prev = ko.observable();
        let next = ko.observable();
        let pageSizes = [10, 20, 30];
        let selectedPageSize = ko.observableArray([10]);

        //empty after testing:
        let username = ko.observable("hans1");

        postman.subscribe("UserSignIn", username);


        let getData = url => {
            ds.getBookmarked(url, data => {
                prev(data.prev);
                next(data.next);
                bookmark(data.personlist);
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
            getData(ds.getBookmarkUrlWithPageSize(size));
        });

        let deleteBookmark = bookmark => {
            console.log(bookmark);
            if (bookmark.nconst === null) {
                fetch("api/bookmark/remove/" + username() + "/" + bookmark.tconst + "/true", {
                    method: 'POST'
                })
                    .then(ds.handleErrors)
                    .then(response => {
                        getData();
                    }).catch(error => {
                        console.log(error);
                    });
            } else {
                fetch("api/bookmark/remove/" + username() + "/" + bookmark.nconst + "/false", {
                    method: 'POST'
                })
                    .then(ds.handleErrors)
                    .then(response => {
                        getData();
                    }).catch(error => {
                        console.log(error);
                    });
            }
        }

        getData();


        //public part
        return {
            bookmark,
            showPrev,
            enableNext,
            enablePrev,
            pageSizes,
            selectedPageSize,
            showNext,
            deleteBookmark
        };
    }
});