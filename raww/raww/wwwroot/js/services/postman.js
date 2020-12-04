define([] , () => {
    let subscribers = [];

    let publish = (event, data) => {

        subscribers.filter(x => x.event === event)
            .forEach(x => x.callback(data));
    }

    let subscribe = (event, callback) => {
        let subscriber = { event, callback };
        subscribers.push(subscriber);
    }


    return {
        publish,
        subscribe
    }
});