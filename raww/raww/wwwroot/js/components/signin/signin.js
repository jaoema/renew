define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        //private part

        //Defines observalbes. 
        let username = ko.observable("");
        let password = ko.observable("");
        let showIncorrect = ko.observable(false);
        let userNotCreated = ko.observable(false);
        let userCreated = ko.observable(false);

        
        let clickSignIn = function () {
            showIncorrect(false);
            //fetcher API + username og password. 
            fetch("api/login/" + username() + "/" + password())
                //håndter errors, hvis nogle. 
                .then(ds.handleErrors)
                //publish event usersignin og changecurrentcomp
                .then(response => {
                    postman.publish("userSignIn", username()); 
                    postman.publish('changeCurrentComp', "titlesearch");
                    //username/pass sættes til blankt
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
                //poster til databasen 
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
        //Tjekker om der står noget i user/pass, hvis ja, kan man trykke på knapperne. 
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