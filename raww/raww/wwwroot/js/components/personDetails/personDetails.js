﻿define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        //private part
        let person = ko.observableArray([]);
        let nconst = ko.observable("");
        let name = ko.observable();
        let showDetails = ko.observable(false);
        let enableBookmark = ko.observable(true);
        let alreadyBookmarked = ko.observable(false);
        let addedBookmark = ko.observable(false);


        //empty after testing
        let username = ko.observable("");

        postman.subscribe("userSignIn", username);


        let getData = url => {
            ds.getPerson(nconst(), url, data => {
                person(data);
                name(data.primaryname);
            });
        }

        postman.subscribe('changeNconst', data => {
            nconst(data.nconst);
            showDetails(data.showDetails);
            getData();
        });

        let clickBookmark = function () {
            alreadyBookmarked(false);
            addedBookmark(false);
            fetch("api/bookmark/" + username() + "/" + nconst() + "/false", {
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

        postman.subscribe('changePerson', person);

        //public part
        return {
            person,
            showDetails,
            enableBookmark,
            alreadyBookmarked,
            addedBookmark,
            clickBookmark,
            name
        }
    }
});