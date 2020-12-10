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

        //empty after testing:
        let username = ko.observable("hans1");

        postman.subscribe("UserSignIn", username);

        let getData = url => {
            console.log(tconst());
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
            console.log("clicked bookmark");
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
            addedBookmark
        }
    }
});