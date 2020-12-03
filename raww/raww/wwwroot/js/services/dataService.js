define([], () => {


    let getSearchHistory = (callback) => {
        fetch("api/searchhistory")
            .then(response => response.json())
            .then(callback);
    }


    return {
        getSearchHistory
    }

});