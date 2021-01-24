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

    //Sibscribes to events for usersignin
    postman.subscribe("userSignIn", username);

    // defines ko.observables
    // Array with menu elements
    let signInComponent = ko.observable(signInComp.fileName);
    let currentComponent = ko.observable(titleSearchComp.fileName);
    let menuElements = [titleSearchComp, nameSearchComp, profileComp];
    let selectedComponent = ko.observable();

    //Subscribes to events chagecurrentcomp
    postman.subscribe("changeCurrentComp", currentComponent);

    //Chages the content
    let changeContent = element => {
        currentComponent(element.fileName.toLowerCase());

    }
    // Return element der er aktivt, hvis element = currentcomponent ellers return 0 (Tjekker components)
    let isActive = element => {
        return element.fileName.toLowerCase() === currentComponent() ? "active" : "";
    }
    //Skifter til titlesearch component og publisher event
    let clickSearch = function() {
        changeContent(titleSearchComp);
        postman.publish('changeSearchFromNav', { searchFromNav: true, searchterm: searchterm() });
        searchterm("");
    }
    //Aktiv hvis feltet ikke er tomt. 
    let enableSearch = ko.computed(() => searchterm() !== "");

    //logger ud, publish event. 
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