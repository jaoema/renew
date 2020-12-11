define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        //private part

        let username = ko.observable("");
        let password = ko.observable("");
        let showIncorrect = ko.observable(false);
        let userNotCreated = ko.observable(false);
        let userCreated = ko.observable(false);


        let clickSignIn = function () {
            showIncorrect(false);

            fetch("api/login/" + username() + "/" + password())
                .then(ds.handleErrors)
                .then(response => {
                    console.log(username());
                    postman.publish("userSignIn", username());
                    username("");
                    password("");
                }).catch( error => {
                    console.log(error);
                    username("");
                    password("");
                    showIncorrect(true);
                });
        }

        let clickSignUp = function () {
            fetch("api/signup/" + username() + "/" + password(), {
                method: 'POST'

            })
                .then(ds.handleErrors)
                .then(response => {
                    userCreated(true);
                    username("");
                    password("");
                }).catch(error => {
                    console.log(error);
                    username("");
                    password("");
                    userNotCreated(true);
                });
        }

        let enableButtons = ko.computed(() => username() !== "" && password() !== "");


        //public part
        return {
            username,
            password,
            clickSignIn,
            showIncorrect,
            enableButtons,
            clickSignUp,
            userCreated,
            userNotCreated
        };
    }
});