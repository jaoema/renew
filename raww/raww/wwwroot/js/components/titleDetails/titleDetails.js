define(['knockout', 'dataservice', 'postman'], (ko, ds, postman) => {
    return function (params) {
        //private part
        //let title = params.title;
        let tconst = params.tconst;

        postman.subscribe('changeTconst', tconst);



        let clickBookmark = function () {

        }

        //public part
        return {
            tconst,
            clickBookmark
        }
    }
});