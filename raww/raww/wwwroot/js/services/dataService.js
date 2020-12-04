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


    return {
        getSearchHistory,
        searchName
    }

});