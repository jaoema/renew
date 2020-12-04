define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        //private part
        let person = params.person;

        //let nconst = params.nconst || "nm0000514";

        //ds.getPerson(nconst, function(data) { person(data) });

        postman.subscribe('changePerson', person);

        //public part
        return {
           person
        }
    }
});