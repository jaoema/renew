define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        //private part
        let title = params.title;

        //let nconst = params.nconst || "nm0000514";

        //ds.getPerson(nconst, function(data) { person(data) });

        postman.subscribe('changeTitle', title);

        //public part
        return {
            title
        }
    }
});