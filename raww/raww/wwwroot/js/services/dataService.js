define([], () => {
    const searchhistoryApiUrl = "api/searchhistory";
    const ratinghistoryApiUrl = "api/ratinghistory";
    const bookmarkhistoryApiUrl = "api/bookmarked";
    const namesearchApiUrl = "api/namesearch/";
    const titlesearchApiUrl = "api/simplesearch/";
    const signInApiUrl = "api/login/";


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

  
    let checkSignIn = (username, password, url, callback) => {
        if (url == undefined) {
            url = signInApiUrl + username + "/" + password;
        }
        getJson(url, callback);
    };

    let getSearchhistory = (url, callback) => {
        if (url === undefined) {
            url = searchhistoryApiUrl;
        }
        getJson(url, callback);
    };

    let getRatinghistory = (url, callback) => {
        if (url === undefined) {
            url = ratinghistoryApiUrl;
        }
        getJson(url, callback);
    };

    let getBookmarked = (url, callback) => {
        if (url === undefined) {
            url = bookmarkhistoryApiUrl;
        }
        getJson(url, callback);
    };

    let getSearchhistoryUrlWithPageSize = size => searchhistoryApiUrl + "?pagesize=" + size;

    let getRatinghistoryUrlWithPageSize = size => ratinghistoryApiUrl + "?pagesize=" + size;

    let getBookmarkUrlWithPageSize = size => bookmarkhistoryApiUrl + "?pagesize=" + size;

    let getNamesearchUrlWithPageSize = (size, searchterm) => namesearchApiUrl + searchterm + "?pagesize=" + size;

    let getTitlesearchUrlWithPageSize = (size, searchterm) => titlesearchApiUrl + searchterm + "?pagesize=" + size;


    let getSearchName = (searchterm, url, callback) => {
        if (url === undefined) {
            url = namesearchApiUrl + searchterm;
        }
        getJson(url, callback);
    };

    let getSearchTitle = (searchterm, url, callback) => {
        if (url === undefined) {
            url = titlesearchApiUrl + searchterm;
        }
        getJson(url, callback);
    };


    let getPerson = (nconst, callback) => {
        fetch("api/person/" + nconst)
            .then(response => response.json())
            .then(callback);
    }

    let getTitle = (tconst, callback) => {
        fetch("api/movie/" + tconst)
            .then(response => response.json())
            .then(callback);
    }
    

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
        checkSignIn,
        handleErrors
    }

});