define(['knockout', 'postman'], function (ko, postman) {
    //private part

    //components with a title and a filename
    let nameSearchComp = { titleName: "Find Person", fileName: "namesearch" }
    let searchHistoryComp = { titleName: "Search History", fileName: "searchhistory" }
    let ratingHistoryComp = { titleName: "Your Ratings", fileName: "ratinghistory" }
    let titleSearchComp = { titleName: "Find Title", fileName: "titlesearch" }
    let signInComp = { titleName: "Sign In", fileName: "signin" }
    let bookmarkComp = { titleName: "Bookmarks", fileName: "bookmark" }
    let profileComp = { titleName: "Profile", fileName: "profile"}
    
    let searchterm = ko.observable("");

    // empty after testing
    let username = ko.observable("");

    postman.subscribe("userSignIn", username);

    let signInComponent = ko.observable(signInComp.fileName);
    let currentComponent = ko.observable(titleSearchComp.fileName);
    let menuElements = [titleSearchComp, nameSearchComp, searchHistoryComp, profileComp];
    let selectedComponent = ko.observable();

    postman.subscribe("changeCurrentComp", currentComponent);


    let changeContent = element => {
        currentComponent(element.fileName.toLowerCase());

    }

    let isActive = element => {
        return element.fileName.toLowerCase() === currentComponent() ? "active" : "";
        debugger;
    }

    let clickSearch = function() {
        changeContent(titleSearchComp);

        setTimeout(() => {
            postman.publish('changeSearchFromNav', false);

            postman.publish('changeSearchterm', searchterm());
            searchterm("");
            postman.publish('changeSearchFromNav', true);
        }, 100);
    }

    let enableSearch = ko.computed(() => searchterm() !== "");

    let clickSignout = function() {
        postman.publish("userSignIn", "");
    }


    //public part
    return {
        currentComponent,
        menuElements,
        selectedComponent,
        changeContent,
        isActive,
        searchterm,
        clickSearch,
        enableSearch,
        username,
        signInComponent,
        clickSignout
    };
});