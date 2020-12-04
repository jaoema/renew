define(['knockout', 'dataservice'], (ko, ds) => {
    return function (params) {
        //private part
        let person = params.person;


        //public part
        return {
           person
        }
    }
});