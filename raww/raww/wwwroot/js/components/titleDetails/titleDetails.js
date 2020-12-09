define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        //private part
        //let title = params.title;
        let title = ko.observableArray([]);
        let tconst = ko.observable("");
        let originaltitle = ko.observable();
        let startyear = ko.observable();
        let showDetails = ko.observable(false);
         

        postman.subscribe('changeTconst', tconst);
        postman.subscribe('changeShowDetails', showDetails);

        let getData = url => {
            console.log(tconst());
            ds.getTitle(tconst(), url, data => {
                title(data);
                originaltitle(data.originaltitle);
                startyear(data.startyear);
                debugger;
            });
        }

        let clickBookmark = function () {
            console.log("clicked bookmark");
            //fetch("api/bookmark/" + username() + "/" + tconst() + "/true" , {
            //    method: 'POST'

            //})
            //    .then(ds.handleErrors)
            //    .then(response => {
            //        userCreated(true);
            //    }).catch(error => {
            //        console.log(error);
            //        userNotCreated(true);
            //    });
        }

        showDetails.subscribe(function (newValue) {
            if (newValue === true) {
                getData();
            }
                         
        });
       
       

        //public part
        return {
            tconst,
            clickBookmark,
            title,
            originaltitle,
            startyear,
            showDetails
        }
    }
});