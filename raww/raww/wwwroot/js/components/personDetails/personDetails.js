define(['knockout', 'dataservice'], (ko, ds) => {
    return function (params) {
        //private part
        let person = params.person;

        //let nconst = params.nconst || "nm0000514";

        //ds.getPerson(nconst, function(data) { person(data) });

        //public part
        return {
           person
        }
    }
});