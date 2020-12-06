define([], () => {
    const searchhistoryApiUrl = "api/searchhistory";

    let getJson = (url, callback) => {
        fetch(url).then(response => response.json()).then(callback);
    };

    let getSearchhistory = (url, callback) => {
        if (url === undefined) {
            url = searchhistoryApiUrl;
        }
        getJson(url, callback);
    };

    let getSearchhistoryUrlWithPageSize = size => searchhistoryApiUrl + "?pagesize=" + size;

    //let getSearchHistory = (callback) => {
    //    fetch("api/searchhistory")
    //        .then(response => response.json())
    //        .then(callback);
    //}

    let searchName = (searchterm, callback) => {
        fetch("api/namesearch/" + searchterm)
            .then(response => response.json())
            .then(callback);
    }

    let getPerson = (nconst, callback) => {
        fetch("api/person/" + nconst)
            .then(response => response.json())
            .then(callback);
    }

    let searchTitle = (searchterm, callback) => {
        fetch("api/simplesearch/" + searchterm)
            .then(response => response.json())
            .then(callback);
        //debugger;
    }

    let getTitle = (tconst, callback) => {
        fetch("api/movie/" + tconst)
            .then(response => response.json())
            .then(callback);
    }
    

    return {
        //getSearchHistory,
        getSearchhistory,
        getSearchhistoryUrlWithPageSize,
        searchName,
        getPerson,
        searchTitle,
        getTitle
    }

});