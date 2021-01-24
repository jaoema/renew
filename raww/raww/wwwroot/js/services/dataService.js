//Defines the URL to APIs
define(['knockout', 'postman'], (ko, postman) => {
    const searchhistoryApiUrl = "api/searchhistory/";
    const ratinghistoryApiUrl = "api/ratinghistory/";
    const bookmarkhistoryApiUrl = "api/bookmarked/";
    const namesearchApiUrl = "api/namesearch/";
    const titlesearchApiUrl = "api/simplesearch/";
    const personApiUrl = "api/person/";
    const titleApiUrl = "api/movie/";


    //empty after testing
    let username = ko.observable("")
    //Subscriber til userSignIn
    postman.subscribe('userSignIn', username)

    // Sender en fejl tekts hvis der opstår fejl. Hvis respons ikke er ok, post fejl. 
    let handleErrors = (response) => {
        if (!response.ok) {
            throw Error(response.statusText);
        }
        return response;
    };
    //tar URL og et callback, håndtere errors, tar respons, sender det tilbage til som json
    let getJson = (url, callback) => {
        fetch(url)
            .then(handleErrors)
            .then(response => response.json())
            .then(callback)
            .catch(error => console.log(error));
    };
    //Hvis url = undefined, bruges standard url, publish username, kalder vi vores json funktion for at få json tilbage. 
    let getSearchhistory = (url, callback) => {
        if (url === undefined) {
            url = searchhistoryApiUrl + username();
        }
        postman.publish("userSignIn", username());

        getJson(url, callback);
    };

    let getRatinghistory = (url, callback) => {
        if (url === undefined) {
            url = ratinghistoryApiUrl + username();
        }
        postman.publish("userSignIn", username());

        getJson(url, callback);
    };

    let getBookmarked = (url, callback) => {
        if (url === undefined) {
            url = bookmarkhistoryApiUrl + username();
        }
        postman.publish("userSignIn", username());

        getJson(url, callback);
    };

    //Tar standard url med username og indsætter pagesize.
    let getSearchhistoryUrlWithPageSize = size => searchhistoryApiUrl + username() + "?pagesize=" + size;

    let getRatinghistoryUrlWithPageSize = size => ratinghistoryApiUrl + username() + "?pagesize=" + size;

    let getBookmarkUrlWithPageSize = size => bookmarkhistoryApiUrl + username() + "?pagesize=" + size;

    let getNamesearchUrlWithPageSize = (size, searchterm) => namesearchApiUrl + username() + "/" + searchterm + "?pagesize=" + size;

    let getTitlesearchUrlWithPageSize = (size, searchterm) => titlesearchApiUrl + username() + "/" + searchterm + "?pagesize=" + size;

    
    let getSearchName = (searchterm, url, callback) => {
        if (url === undefined) {
            url = namesearchApiUrl + username() + "/" + searchterm;
        }
        getJson(url, callback);
    };

    let getSearchTitle = (searchterm, url, callback) => {
        if (url === undefined) {
            url = titlesearchApiUrl + username() + "/" + searchterm;
        }
        getJson(url, callback);
    };

    let getPerson = (nconst, url, callback) => {
        if (url === undefined) {
            url = personApiUrl + nconst;
        }
        postman.publish("userSignIn", username());
        getJson(url, callback);
    };

    let getTitle = (tconst, url, callback) => {
        if (url === undefined) {
            url = titleApiUrl + tconst;
        }
        postman.publish("userSignIn", username());
        getJson(url, callback);
    };

    return {
        getSearchhistory,
        getRatinghistory,
        getBookmarked,
        getSearchhistoryUrlWithPageSize,
        getRatinghistoryUrlWithPageSize,
        getBookmarkUrlWithPageSize,
        getNamesearchUrlWithPageSize,
        getTitlesearchUrlWithPageSize,
        getSearchName,
        getSearchTitle,
        getPerson,
        getTitle,
        handleErrors,
    }

});