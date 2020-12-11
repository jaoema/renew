define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        //private part
        //let title = params.title;
        let title = ko.observableArray([]);
        let tconst = ko.observable("");
        let originaltitle = ko.observable();
        let startyear = ko.observable();
        let showDetails = ko.observable(false);
        let enableBookmark = ko.observable(true);
        let alreadyBookmarked = ko.observable(false);
        let addedBookmark = ko.observable(false);
        let enableRate = ko.observable(true);
        let alreadyRated = ko.observable(false);
        let addedRating = ko.observable(false);
        let ratings = [1,2,3,4,5,6,7,8,9,10];
        let selectedRating = ko.observableArray([5]);

        //empty after testing:
        let username = ko.observable("");

        postman.subscribe('userSignIn', username)


        let getData = url => {
            ds.getTitle(tconst(), url, data => {
                title(data);
                originaltitle(data.originaltitle);
                startyear(data.startyear);
            });
        }

        postman.subscribe('changeTconst', data => {
            tconst(data.tconst);
            showDetails(data.showDetails);
            getData();
        });

        let clickBookmark = function () {
            alreadyBookmarked(false);
            addedBookmark(false);
            fetch("api/bookmark/" + username() + "/" + tconst() + "/true" , {
                method: 'POST'
            })
                .then(ds.handleErrors)
                .then(response => {
                    enableBookmark(false);
                    addedBookmark(true);
                }).catch(error => {
                    console.log(error);
                    alreadyBookmarked(true);
                });
        }

        let clickRate = function () {
            alreadyRated(false);
            addedRating(false);
            fetch("api/movie/rate/" + username() + "/" + tconst() + "/" + selectedRating()[0], {
                method: 'POST'
            })
                .then(ds.handleErrors)
                .then(response => {
                    enableRate(false);
                    addedRating(true);
                }).catch(error => {
                    console.log(error);
                    alreadyRated(true);
                });
        }
       

        //public part
        return {
            tconst,
            clickBookmark,
            title,
            originaltitle,
            startyear,
            showDetails,
            enableBookmark,
            alreadyBookmarked,
            addedBookmark,
            clickRate,
            enableRate,
            alreadyRated,
            addedRating,
            ratings,
            selectedRating
        }
    }
});