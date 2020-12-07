define(['knockout', 'postman'], function (ko, postman) {
    //private part

    //components with a title and a filename
    let nameSearchComp = { titleName: "Find Person", fileName: "namesearch" }
    let searchHistoryComp = { titleName: "Search History", fileName: "searchhistory" }
    let ratingHistoryComp = { titleName: "Rating History", fileName: "ratinghistory" }
    let titleSearchComp = { titleName: "Find Title", fileName: "titlesearch" }
    let signInComp = { titleName: "Sign In", fileName: "signin" }
    let searchterm = ko.observable("");



    let currentComponent = ko.observable(signInComp.fileName);
    let menuElements = [signInComp, nameSearchComp, titleSearchComp, searchHistoryComp, ratingHistoryComp];
    let selectedComponent = ko.observable('namesearch');

    let changeContent = element => {
        currentComponent(element.fileName.toLowerCase());

    }

    let isActive = element => {
        return element.fileName.toLowerCase() === currentComponent() ? "active" : "";
    }

    let clickSearch = function() {
        //currentComponent(simpleSearchComp.fileName.toLowerCase());
        changeContent(titleSearchComp);
        setTimeout(() => {
            postman.publish('changeSearchFromNav', false);

            postman.publish('changeSearchterm', searchterm());
            searchterm("");
            postman.publish('changeSearchFromNav', true);
        }, 100);
        
        
    }

    let enableSearch = ko.computed(() => searchterm() !== "");


    //public part
    return {
        currentComponent,
        menuElements,
        selectedComponent,
        changeContent,
        isActive,
        searchterm,
        clickSearch,
        enableSearch
    };
});