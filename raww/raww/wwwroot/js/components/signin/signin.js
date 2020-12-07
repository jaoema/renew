define(['knockout', 'dataservice'], (ko, ds) => {
    return function (params) {
        //private part

        let username = ko.observable("");
        let password = ko.observable("");

        let clickSubmit = function () {
            console.log(username());
            console.log(password());
        }


        //public part
        return {
            username,
            password,
            clickSubmit
        };
    }
});