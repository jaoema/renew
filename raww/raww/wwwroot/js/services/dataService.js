define(['knockout', 'postman'], (ko, postman) => {
    const searchhistoryApiUrl = "api/searchhistory/";
    const ratinghistoryApiUrl = "api/ratinghistory/";
    const bookmarkhistoryApiUrl = "api/bookmarked/";
    const namesearchApiUrl = "api/namesearch/";
    const titlesearchApiUrl = "api/simplesearch/";
    const personApiUrl = "api/person/";
    const titleApiUrl = "api/movie/";

    let username = ko.observable()

    postman.subscribe('userSignIn', username)


    let handleErrors = (response) => {
        if (!response.ok) {
            throw Error(response.statusText);
        }
        return response;
    };

    let getJson = (url, callback) => {
        fetch(url)
            .then(handleErrors)
            .then(response => response.json())
            .then(callback)
            .catch(error => console.log(error));
    };

    let getSearchhistory = (url, callback) => {
        if (url === undefined) {
            url = searchhistoryApiUrl + username();
        }
        getJson(url, callback);
    };

    let getRatinghistory = (url, callback) => {
        if (url === undefined) {
            url = ratinghistoryApiUrl + username();
        }
        getJson(url, callback);
    };

    let getBookmarked = (url, callback) => {
        if (url === undefined) {
            url = bookmarkhistoryApiUrl + username();
        }
        getJson(url, callback);
    };

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
            url = personApiUrl + nconst();
        }
        getJson(url, callback);
    };

    let getTitle = (tconst, url, callback) => {
        if (url === undefined) {
            url = titleApiUrl + tconst();
        }
        getJson(url, callback);
    };

    //let getPerson = (nconst, callback) => {
    //    fetch("api/person/" + nconst)
    //        .then(response => response.json())
    //        .then(callback);
    //}

    //let getTitle = (tconst, callback) => {
    //    fetch("api/movie/" + tconst)
    //        .then(response => response.json())
    //        .then(callback);
    //}
    

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
        handleErrors
    }

});