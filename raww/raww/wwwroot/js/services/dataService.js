define([], () => {


    let getSearchHistory = (callback) => {
        fetch("api/searchhistory")
            .then(response => response.json())
            .then(callback);
    }

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

    return {
        getSearchHistory,
        searchName
    }

});