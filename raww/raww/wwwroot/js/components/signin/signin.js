define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        //private part

        let username = ko.observable("");
        let password = ko.observable("");
        let showIncorrect = ko.observable(false);


        let clickSubmit = function () {
            showIncorrect(false);

            fetch("api/login/" + username() + "/" + password())
                .then(ds.handleErrors)
                .then(response => {
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


        //public part
        return {
            username,
            password,
            clickSubmit,
            showIncorrect
        };
    }
});